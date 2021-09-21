using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Digitbridge.QuickbooksOnline.AzureQueue.Infrastructure;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.CentralToDbMdl;
using Digitbridge.QuickbooksOnline.CentralToDbMdl.Model;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UneedgoHelper.DotNet.Common;
using static Digitbridge.QuickbooksOnline.AzureQueue.QueueConnection.QueueCommunicator;

namespace Digitbridge.QuickbooksOnline.CentralToDbTestCon
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(Test_ImportCentralOrders());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {

            }
        }
        private static async Task Test_ImportCentralOrders()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            IMessageSerializer messageSerializer = new JsonMessageSerializer();

            var path = @"C:\Users\WillHuang\Documents\OrderImportCommandTest.json";
            List<Command> commands = messageSerializer.Desrialize<List<Command>>(File.ReadAllText(path));
            MyAppSetting appSetting = new MyAppSetting();

            QboDbConfig dbConfig = new QboDbConfig(
               appSetting.DBConnectionValue,
               appSetting.AzureUseManagedIdentity,
               appSetting.AzureTenantId,
               appSetting.AzureTokenProviderConnectionString,
               appSetting.CentralOrderTableName,
               appSetting.CentralItemLineTableName,
               appSetting.QuickBooksIntegrationSettingTableName
               );

            CentralOrdersImport centralOrdersImport = 
                await CentralOrdersImport.CreatAsync(dbConfig, 
                appSetting.CentralInApiEndPoint + appSetting.CentralInOrderExtensionFlagsApiName,
                appSetting.CentralIntegrationApiCode);

            QboOrderDb qboOrderDb = 
                new QboOrderDb(dbConfig.QuickBooksDbOrderTableName, centralOrdersImport._msSql);
            QboIntegrationSettingDb qboIntegrationDb =
                new QboIntegrationSettingDb(dbConfig.QuickBooksIntegrationSettingTableName, centralOrdersImport._msSql);

            //var pathTest = @"C:\Users\WillHuang\Documents\ResponseForFulfilledOrderUpdateTest.json";

            //CentralOrderResult rootObject = messageSerializer.Desrialize<CentralOrderResult>(File.ReadAllText(pathTest));
            //List<QboSalesOrderWrapper> qboOrders = QuickBooksDbUtility.GetMappedQboSalesOrdersFromCentral(rootObject);
            //DetailTransferResult transferResult = await centralOrdersImport.ImportOrdersAsync(qboOrders);
            //DetailTransferResult updateResult = await centralOrdersImport.UpdateOrdersAsync(qboOrders);
            List<Command> toExportCommands = new List<Command>();

            List<Command> toImportCommands = 
                commands.ConvertAll(command_ => new Command(command_.MasterAccountNum, command_.ProfileNum));

            foreach (Command command in commands)
            {
                Boolean hasCreateResult = false;
                Boolean hasUpdateResult = false;

                // Get Order Import Range
                DateTime importOrderFromDate =
                    await QuickBooksDbUtility.GetImportOrderFromDate(command, qboOrderDb, qboIntegrationDb);

                DateTime importOrderToDate =
                    importOrderFromDate.AddDays(appSetting.RollingImportDayRange);

                /////////
                //importOrderFromDate = importOrderFromDate.AddDays(-3);
                //importOrderToDate = importOrderFromDate.AddDays(appSetting.RollingImportDayRange);
                ///////// 

                if (importOrderFromDate >= importOrderToDate || importOrderFromDate == default)
                {
                    Console.WriteLine($"Get Order Import Range Error for Central Order Import.");
                    toImportCommands.RemoveAll(c =>
                               c.MasterAccountNum == command.MasterAccountNum &&
                               c.ProfileNum == command.ProfileNum
                    );
                    continue;
                }

                int orderCreateCount = 0;
                int orderUpdateCount = 0;
                bool isFirstLoop = true;

                // Watch time and end loop if close to instancse life time to prevent lost flag
                int azureInstanceLifeTime = 300000;
                int expectRunTimeForLoop = appSetting.CentralGetOrderApiPageSize * 1000;

                while (azureInstanceLifeTime - watch.ElapsedMilliseconds > expectRunTimeForLoop &&
                    orderCreateCount == 0 && orderUpdateCount == 0 && importOrderToDate < DateTime.Now || isFirstLoop)
                {
                    /////////
                    Console.WriteLine($"importFrom {importOrderFromDate} to {importOrderToDate}");
                    /////////
                    Boolean hasCreateNextPage = true;
                    Boolean hasUpdateNextPage = true;
                    isFirstLoop = false;

                    // Create orders from Central
                    while (hasCreateNextPage == true && 
                        azureInstanceLifeTime - watch.ElapsedMilliseconds > expectRunTimeForLoop)
                    {
                        string orderApiResponseJsonStr =
                            await QuickBooksDbUtility.GetSalesOrdersFromCentral(
                                GetOrderDateFilterType.LastUpdateDate,
                                appSetting.CentralInApiEndPoint + appSetting.CentralInOrdersApiName,
                                appSetting.CentralIntegrationApiCode,
                                command.MasterAccountNum,
                                command.ProfileNum,
                                appSetting.CentralGetOrderApiPageSize,
                                orderCreateCount,
                                new CentralOrderStatus[] {
                                CentralOrderStatus.Processing,
                                CentralOrderStatus.Shipped,
                                CentralOrderStatus.PartiallyShipped,
                                CentralOrderStatus.PendingShipment,
                                CentralOrderStatus.ReadyToPickup,
                                CentralOrderStatus.OnHold
                                },
                                new OrderExtensionFlag[] {
                                },
                                new OrderExtensionFlag[] {
                                OrderExtensionFlag.Qbo_Initial_Downloaded,
                                OrderExtensionFlag.Qbo_Tracking_Downloaded,
                                OrderExtensionFlag.Synced_Wtih_Error
                                },
                                importOrderFromDate,
                                importOrderToDate
                            );

                        CentralOrderResult rootObject = messageSerializer.Desrialize<CentralOrderResult>(orderApiResponseJsonStr);
                        if (rootObject.Orders == null)
                        {
                            Console.WriteLine($"Central get Api Error." + orderApiResponseJsonStr);
                            hasCreateNextPage = false;
                            toImportCommands.RemoveAll(c =>
                               c.MasterAccountNum == command.MasterAccountNum &&
                               c.ProfileNum == command.ProfileNum
                            );
                            continue;
                        }
                        if (rootObject.Orders.Count > 0)
                        {
                            List<QboSalesOrderWrapper> qboOrders = QuickBooksDbUtility.GetMappedQboSalesOrdersFromCentral(rootObject);
                            DetailTransferResult transferResult = await centralOrdersImport.ImportOrdersAsync(qboOrders);
                            if (transferResult.ProcessedCount > 0) hasCreateResult = true;
                            orderCreateCount += qboOrders.Count;
                        }
                        hasCreateNextPage = rootObject.HasNextPage;
                    }

                    // Update orders from central
                    while (hasUpdateNextPage == true &&
                        azureInstanceLifeTime - watch.ElapsedMilliseconds > expectRunTimeForLoop)
                    {
                        string orderApiResponseJsonStr =
                            await QuickBooksDbUtility.GetSalesOrdersFromCentral(
                                GetOrderDateFilterType.LastUpdateDate,
                                appSetting.CentralInApiEndPoint + appSetting.CentralInOrdersApiName,
                                appSetting.CentralIntegrationApiCode,
                                command.MasterAccountNum,
                                command.ProfileNum,
                                appSetting.CentralGetOrderApiPageSize,
                                orderUpdateCount,
                                new CentralOrderStatus[]
                                {
                                CentralOrderStatus.Shipped
                                },
                                new OrderExtensionFlag[]
                                {
                                OrderExtensionFlag.Qbo_Initial_Downloaded
                                },
                                new OrderExtensionFlag[]
                                {
                                OrderExtensionFlag.Qbo_Tracking_Downloaded,
                                OrderExtensionFlag.Synced_Wtih_Error
                                },
                                importOrderFromDate,
                                importOrderToDate
                            );

                        CentralOrderResult rootObject = messageSerializer.Desrialize<CentralOrderResult>(orderApiResponseJsonStr);
                        if (rootObject.Orders == null)
                        {
                            Console.WriteLine($"Central get Api Error." + orderApiResponseJsonStr);
                            hasUpdateNextPage = false;
                            toImportCommands.RemoveAll(c =>
                               c.MasterAccountNum == command.MasterAccountNum &&
                               c.ProfileNum == command.ProfileNum
                            );
                            continue;
                        }
                        if (rootObject.Orders.Count > 0)
                        {
                            List<QboSalesOrderWrapper> qboOrders = QuickBooksDbUtility.GetMappedQboSalesOrdersFromCentral(rootObject);
                            DetailTransferResult transferResult = await centralOrdersImport.UpdateOrdersAsync(qboOrders);
                            if (transferResult.ProcessedCount > 0) hasUpdateResult = true;
                            orderUpdateCount += qboOrders.Count;
                        }
                        hasUpdateNextPage = rootObject.HasNextPage;
                    }
                    /////////
                    Console.WriteLine($"Import get {orderCreateCount} Update get {orderUpdateCount}");
                    /////////
                    importOrderFromDate = importOrderFromDate.AddDays(1);
                    importOrderToDate = importOrderToDate.AddDays(1);
                }

                if(importOrderToDate >= DateTime.Now)
                {
                    toImportCommands.RemoveAll( c => 
                        c.MasterAccountNum == command.MasterAccountNum && 
                        c.ProfileNum == command.ProfileNum
                    );
                }

                if (hasCreateResult || hasUpdateResult)
                {
                    toExportCommands.Add(command);
                }

            }
            if (toImportCommands.Count > 0 )
            {
                QueueConfig queueConfig = new QueueConfig(appSetting.AzureWebJobsStorage
                  , appSetting.AzureWebJobsStorageAccountName
                  , appSetting.AzureWebJobsStorageEndpointSuffix
                  , appSetting.AzureWebJobsStorageAccountKey
                  , appSetting.AzureWebJobsStorageUseHttps
                  , appSetting.AzureWebJobsStorageUseManagedIdentity
                  , appSetting.AzureWebJobsStorageTokenProviderConnectionString
                  , appSetting.AzureWebJobsStorageTenantId);

                IQueueCommunicator queueCommunicator = new MsAzureQueueCommunicator(
                    messageSerializer, queueConfig
                    );

                if (toImportCommands.Count > 0)
                {
                    await queueCommunicator.AddQueueAsync(RouteNames.QuickBooksOrderImportQueue, toImportCommands);
                    //await outputQueueItem.AddAsync(messageSerializer.Serialize(new List<Command>() { command }));
                }

                if (toExportCommands.Count > 0)
                {
                    await queueCommunicator.AddQueueAsync(RouteNames.QuickBooksOrderExportQueue, toExportCommands);
                }

            }
        }

    }
}
  
using System;
using System.Collections.Generic;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.CentralToDbMdl;
using Digitbridge.QuickbooksOnline.CentralToDbMdl.Model;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Digitbridge.QuickbooksOnline.CentralToDbFunc
{
    public static class CentralToDbQueueTrigger
    {
        [FunctionName("CentralToDbQueueTrigger")]
        [return: Queue(RouteNames.QuickBooksOrderExportQueue)]
        public static async System.Threading.Tasks.Task<string> RunAsync(
            [QueueTrigger(RouteNames.QuickBooksOrderImportQueue, Connection = "AzureWebJobsStorage")]
            string message, ILogger log, ExecutionContext context, 
            [Queue(RouteNames.QuickBooksOrderImportQueue)] IAsyncCollector<string> outputQueueItem,
            [Table("QuickbooksOnlineOrderImportErrorLog", Connection = "AzureLogConn")] CloudTable logTable)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            log.LogInformation($"C# Queue trigger CentralToDbQueueTrigger processed: {message}");

            // record the first MasterAccNum and ProfileNum in order to locate the database when there are exceptions
            int firstMasterAccNum = 0;
            int firstProfileNum = 0;

            IMessageSerializer messageSerializer = new JsonMessageSerializer();

            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);
                List<Command> commands = messageSerializer.Desrialize<List<Command>>(message);
                firstMasterAccNum = commands.Count > 0 ? commands[0].MasterAccountNum : 0;
                firstProfileNum = commands.Count > 0 ? commands[0].ProfileNum : 0;

                QboDbConfig dbConfig = new QboDbConfig(
                   appSetting.DBConnectionValue,
                   appSetting.AzureUseManagedIdentity,
                   appSetting.AzureTenantId,
                   appSetting.AzureTokenProviderConnectionString,
                   appSetting.CentralOrderTableName,
                   appSetting.CentralItemLineTableName,
                   appSetting.QuickBooksIntegrationSettingTableName
                   );

                CentralOrdersImport centralOrdersImport = await CentralOrdersImport.CreatAsync(dbConfig,
                    appSetting.CentralInApiEndPoint + appSetting.CentralInOrderExtensionFlagsApiName,
                    appSetting.CentralIntegrationApiCode);

                QboOrderDb qboOrderDb =
                    new QboOrderDb(dbConfig.QuickBooksDbOrderTableName, centralOrdersImport._msSql);
                QboIntegrationSettingDb qboSettingDb =
                    new QboIntegrationSettingDb(dbConfig.QuickBooksIntegrationSettingTableName, centralOrdersImport._msSql);

                // List for commands to offer to qbo-order-export-queue
                List<Command> toExportCommands = new List<Command>();
                // List for commands to offer to qbo-order-import-queue
                List<Command> toImportCommands =
                    commands.ConvertAll(command_ => new Command(command_.MasterAccountNum, command_.ProfileNum));


                foreach (Command command in commands)
                {
                    // TBD: Remove from ToImportCommands and Continue if SaleOrderQboType = DoNotExport

                    Boolean hasCreateResult = false;
                    Boolean hasUpdateResult = false;

                    // Azure Table Objects for Error Log Table Insertion
                    ErrorLogAzureTableEntity orderCreateErrorLog = new ErrorLogAzureTableEntity(
                        $"{command.MasterAccountNum}_{command.ProfileNum}:{Guid.NewGuid()}",
                        $"OrderCreate_{DateTime.UtcNow.ToString("yyyy-MM-dd")}");

                    ErrorLogAzureTableEntity orderUpdateErrorLog = new ErrorLogAzureTableEntity(
                        $"{command.MasterAccountNum}_{command.ProfileNum}:{Guid.NewGuid()}",
                        $"OrderUpdate_{DateTime.UtcNow.ToString("yyyy-MM-dd")}");

                    // Get Order Import Range based on the User's setting
                    DateTime importOrderFromDate = await QuickBooksDbUtility.GetImportOrderFromDate(command, qboOrderDb, qboSettingDb);
                    DateTime importOrderToDate = importOrderFromDate.AddDays(appSetting.RollingImportDayRange);
                    DateTime exportOrderToDate = await qboSettingDb.GetExportOrderToDate(command);

                    OrderExportRule rule = await qboSettingDb.GetOrderExportRule(command);

                    // Stop if importOrderToDate reach UtcNow or exportOrderToDate(from setting)
                    DateTime importOrderStopDate = DateTime.UtcNow < exportOrderToDate ? DateTime.UtcNow : exportOrderToDate;

                    bool isDateRangeValid = importOrderFromDate < importOrderToDate && importOrderFromDate != default;
                    bool isExportRuleValid = !rule.Equals(OrderExportRule.Null) && !rule.Equals(OrderExportRule.DoNotExport);

                    // importOrderFromDate and importOrderToDate is for each round, so fromDate will always be earlier than toDate
                    if (!isDateRangeValid || !isExportRuleValid)
                    {
                        if (!isDateRangeValid)
                        {
                            log.LogInformation(QboOrderImportErrorMsgs.OrderImportDateRangeError);
                            orderCreateErrorLog.ErrorMsg += QboOrderImportErrorMsgs.OrderImportDateRangeError;
                        }
                        toImportCommands.RemoveAll(c =>
                               c.MasterAccountNum == command.MasterAccountNum &&
                               c.ProfileNum == command.ProfileNum
                        );
                        continue;
                    }

                    //////////////////////////////
                    //importOrderFromDate = importOrderFromDate.AddDays(-30);
                    //importOrderToDate =
                    //importOrderFromDate.AddDays(appSetting.RollingImportDayRange);
                    //////////////////////////////

                    int orderCreateCount = 0;
                    int orderUpdateCount = 0;

                    Boolean isCreateSkippedForTimeGap = false;
                    Boolean isUpdateSkippedForTimeGap = false;

                    bool isFirstLoop = true;

                    // Watch time and end loop if close to instancse life time to prevent lost flag
                    int azureInstanceLifeTime = 300000;
                    int expectRunTimeForLoop = appSetting.CentralGetOrderApiPageSize * 1000;

                    while (azureInstanceLifeTime - watch.ElapsedMilliseconds > expectRunTimeForLoop &&
                        !isCreateSkippedForTimeGap && !isUpdateSkippedForTimeGap &&
                        orderCreateCount == 0 && orderUpdateCount == 0 && importOrderToDate < importOrderStopDate || isFirstLoop)
                    {
                        /////////
                        log.LogInformation($"importFrom {importOrderFromDate} to {importOrderToDate}");
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
                            if (rootObject == null || rootObject.Orders == null)
                            {
                                log.LogInformation($"Central get Api Error." + orderApiResponseJsonStr);

                                orderCreateErrorLog.ErrorMsg += orderApiResponseJsonStr + ", ";

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

                                if (!String.IsNullOrEmpty(transferResult.Message))
                                {
                                    orderCreateErrorLog.ErrorMsg += transferResult.Message + ", ";
                                }

                                if (transferResult.ProcessedCount > 0)
                                {
                                    hasCreateResult = true;
                                }
                                else if (transferResult.Status == DetailResultStatus.SkipForTimeGap)
                                {
                                    isCreateSkippedForTimeGap = true;
                                    break;
                                }
                                orderCreateCount += qboOrders.Count;
                            }
                            hasCreateNextPage = rootObject.HasNextPage;
                        }

                        // Update orders from central
                        while (hasUpdateNextPage == true && azureInstanceLifeTime - watch.ElapsedMilliseconds > expectRunTimeForLoop)
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
                            if (rootObject == null || rootObject.Orders == null)
                            {
                                log.LogInformation($"Central get Api Error." + orderApiResponseJsonStr);
                                orderUpdateErrorLog.ErrorMsg += "Central get Api Error: " + orderApiResponseJsonStr + ", ";

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

                                if (!String.IsNullOrEmpty(transferResult.Message))
                                {
                                    orderUpdateErrorLog.ErrorMsg += transferResult.Message + ", ";
                                }

                                if (transferResult.ProcessedCount > 0)
                                {
                                    hasUpdateResult = true;
                                }
                                else if(transferResult.Status == DetailResultStatus.SkipForTimeGap)
                                {
                                    isUpdateSkippedForTimeGap = true;
                                    break;
                                }
                                orderUpdateCount += qboOrders.Count;
                            }
                            hasUpdateNextPage = rootObject.HasNextPage;
                        }
                        /////////
                        log.LogInformation($"Import get {orderCreateCount} Update get {orderUpdateCount}");
                        /////////
                        importOrderFromDate = importOrderFromDate.AddDays(appSetting.RollingImportDayRange);
                        importOrderToDate = importOrderToDate.AddDays(appSetting.RollingImportDayRange);
                    }

                    // Add Error Logs into Azure Table 
                    if (!String.IsNullOrEmpty(orderCreateErrorLog.ErrorMsg))
                    {
                        TableOperation insertCreateErrorLog = TableOperation.InsertOrMerge(orderCreateErrorLog);
                        await logTable.ExecuteAsync(insertCreateErrorLog);
                    }

                    if (!String.IsNullOrEmpty(orderUpdateErrorLog.ErrorMsg))
                    {
                        TableOperation insertUpdateErrorLog = TableOperation.InsertOrMerge(orderUpdateErrorLog);
                        await logTable.ExecuteAsync(insertUpdateErrorLog);
                    }
                    // Check if all the orders in time intervals until now are imported, if so then regard the command as done
                    if ( !hasCreateResult && !hasUpdateResult && importOrderToDate >= DateTime.Now
                        || isCreateSkippedForTimeGap || isUpdateSkippedForTimeGap )
                    {
                        toImportCommands.RemoveAll(c =>
                           c.MasterAccountNum == command.MasterAccountNum &&
                           c.ProfileNum == command.ProfileNum
                        );
                    }
                    // Trigger DbToQuickBooksFunc if there were orders created or updated
                    if (hasCreateResult || hasUpdateResult)
                    {
                        toExportCommands.Add(command);
                    }
                    
                }

                if (toImportCommands.Count > 0)
                {
                    await outputQueueItem.AddAsync(messageSerializer.Serialize(toImportCommands));
                }

                return toExportCommands.Count > 0 ? messageSerializer.Serialize(toExportCommands) : null;
            }

            catch (Exception ex)
            {
                try
                {
                    // Azure Table Object for Exceptioni Log Table Insertion
                    ErrorLogAzureTableEntity orderImportExceptionLog = new ErrorLogAzureTableEntity(
                         $"{firstMasterAccNum}_{firstProfileNum}:{Guid.NewGuid()}",
                            $"OrderImport_{DateTime.UtcNow.ToString("yyyy-MM-dd")}");

                    orderImportExceptionLog.ExceptionMsg = ex.Message;

                    TableOperation insertImportExceptionLog = TableOperation.InsertOrMerge(orderImportExceptionLog);
                    await logTable.ExecuteAsync(insertImportExceptionLog);
                }
                catch
                {
                    log.LogError(ex, $"Something went wrong when logged Exception in table, {message}");
                }

                log.LogError(ex, $"Something went wrong with the CentralToDbQueueTrigger {message}");
                throw;
            }

        }
    }
}

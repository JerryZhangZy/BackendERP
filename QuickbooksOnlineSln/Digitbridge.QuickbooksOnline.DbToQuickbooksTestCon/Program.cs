using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksTestCon
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Task.WaitAll(Test_ExportOrders());
                Task.WaitAll(Test_ExportSummaries());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally 
            {

            }
        }

        private static async Task Test_ExportOrders()
        {
            var instanceWatch = new System.Diagnostics.Stopwatch();
            instanceWatch.Start();

            IMessageSerializer messageSerializer = new JsonMessageSerializer();

            var path = @"C:\Users\WillHuang\Documents\OrderExportCommandTest.json";
            List<Command> commands = messageSerializer.Desrialize<List<Command>>(File.ReadAllText(path));

            MyAppSetting appSetting = new MyAppSetting();
            QboDbConfig dbConfig = new QboDbConfig(
                appSetting.DBConnectionValue,
                appSetting.AzureUseManagedIdentity,
                appSetting.AzureTokenProviderConnectionString,
                appSetting.AzureTenantId,
                appSetting.CentralOrderTableName,
                appSetting.CentralItemLineTableName,
                appSetting.QuickBooksConnectionInfoTableName,
                appSetting.QuickBooksIntegrationSettingTableName,
                appSetting.QuickBooksChannelAccSettingTableName,
                appSetting.CryptKey
                );
            //QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
            //    appSetting.RedirectUrl,
            //    appSetting.Environment,
            //    appSetting.BaseUrl,
            //    appSetting.MinorVersion
            //    );
            QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                appSetting.RedirectUrl,
                appSetting.Environment,
                appSetting.BaseUrl,
                appSetting.MinorVersion,
                appSetting.AppClientId,
                appSetting.AppClientSecret);
            //string test = CryptoUtility.EncrypTextTripleDES("AB11606352464cznEBcVkg5il7anWZB34qkW2sAADWBdQiQTbe", appSetting.CryptKey);
            foreach ( Command command in commands )
            {
                CentralOrdersExport centralOrdersEmport = await CentralOrdersExport.CreatAsync(dbConfig, qboConnectionConfig, command, instanceWatch);
                if (centralOrdersEmport != null)
                {
                    DetailTransferResult result = await centralOrdersEmport.HandleOrderExportToQboAsync();

                    //if (result.PendingOrderCount == 0)
                    //{
                    //    toExportCommands.RemoveAll(c =>
                    //       c.MasterAccountNum == command.MasterAccountNum &&
                    //       c.ProfileNum == command.ProfileNum
                    //    );
                    //}else
                    //{
                    //    // Prevent next loop got stop because of short time interval.
                    //    Thread.Sleep(20000);
                    //}
                }
                else
                {
                    Console.WriteLine($"QBO Connection Profile is incompleted for : " +
                        $"MasterAccNum: {command.MasterAccountNum} ProfileNum : {command.ProfileNum}" +
                        $", please check the OAuth 2.0 with QBO");
                }
            }

            //return toExportCommands.Count > 0 ? messageSerializer.Serialize(toExportCommands) : null;
        }

        private static async Task Test_ExportSummaries()
        {
            var instanceWatch = new System.Diagnostics.Stopwatch();
            instanceWatch.Start();

            MyAppSetting appSetting = new MyAppSetting();
            QboDbConfig dbConfig = new QboDbConfig(
                appSetting.DBConnectionValue,
                appSetting.AzureUseManagedIdentity,
                appSetting.AzureTokenProviderConnectionString,
                appSetting.AzureTenantId,
                appSetting.CentralOrderTableName,
                appSetting.CentralItemLineTableName,
                appSetting.QuickBooksConnectionInfoTableName,
                appSetting.QuickBooksIntegrationSettingTableName,
                appSetting.QuickBooksChannelAccSettingTableName,
                appSetting.CryptKey
                );
            QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                appSetting.RedirectUrl,
                appSetting.Environment,
                appSetting.BaseUrl,
                appSetting.MinorVersion,
                appSetting.AppClientId,
                appSetting.AppClientSecret
                );

            MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                 dbConfig.QuickBooksDbConnectionString
                 , dbConfig.UseAzureManagedIdentity
                 , dbConfig.TokenProviderConnectionString
                 , dbConfig.AzureTenantId
                 );
            QboIntegrationSettingDb qboSettingDb = new QboIntegrationSettingDb(
                    dbConfig.QuickBooksDbIntegrationSettingTableName, msSql);
            DataTable settingsTb = await qboSettingDb.GetSummarySettingsAsync();
            List<QboIntegrationSetting> qboIntergrationSettings = ComlexTypeConvertExtension.
                                                            DatatableToList<QboIntegrationSetting>(settingsTb);

            foreach (QboIntegrationSetting integrationSetting in qboIntergrationSettings)
            {
                CentralOrdersExport centralOrdersEmport = await CentralOrdersExport.CreatAsync(
                    dbConfig,
                    qboConnectionConfig,
                    new Command(integrationSetting.MasterAccountNum, integrationSetting.ProfileNum),
                    instanceWatch, msSql
                    );

                if (centralOrdersEmport != null)
                {
                    await centralOrdersEmport.HandleOrderExportToQboAsync(OrderTransferType.CreateDailySummary);
                }
                else
                {
                    Console.WriteLine($"QBO Connection Profile is incompleted for : " +
                        $"MasterAccNum: {integrationSetting.MasterAccountNum} ProfileNum : {integrationSetting.ProfileNum}" +
                        $", please check the OAuth 2.0 with QBO");
                }
            }

        }
    }
}

using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Digitbridge.QuickbooksOnline.QuickBooksConnection.Model;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
                System.Threading.Tasks.Task.WaitAll(Test_QboConnections());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {

            }
        }

        private static async System.Threading.Tasks.Task Test_QboConnections()
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

            QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                appSetting.RedirectUrl,
                appSetting.Environment,
                appSetting.BaseUrl,
                appSetting.MinorVersion,
                appSetting.AppClientId,
                appSetting.AppClientSecret);

            //String clientId_ = CryptoUtility.EncrypTextTripleDES("ABmVuN7U5BH51F5tT0ThP3Ucx0B39Vav1QoQdBgPoaz84aCrEt", appSetting.CryptKey);
            //String clientSecret_ = CryptoUtility.EncrypTextTripleDES("8Jan4PpfvcdZAccXKn234g0n70Ar2dDzVCsfV2Iw", appSetting.CryptKey);

            //String clientId = CryptoUtility.DecrypTextTripleDES(appSetting.AppClientId, appSetting.CryptKey);
            //String clientSecret = CryptoUtility.DecrypTextTripleDES(appSetting.AppClientSecret, appSetting.CryptKey);

            //string test = CryptoUtility.EncrypTextTripleDES("AB11606352464cznEBcVkg5il7anWZB34qkW2sAADWBdQiQTbe", appSetting.CryptKey);
            foreach (Command command in commands)
            {
                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                        dbConfig.QuickBooksDbConnectionString
                        , dbConfig.UseAzureManagedIdentity
                        , dbConfig.TokenProviderConnectionString
                        , dbConfig.AzureTenantId
                        );

                QboConnectionInfoDb qboConnectionInfoDb = new QboConnectionInfoDb(
                   dbConfig.QuickBooksDbConnectionInfoTableName, msSql, dbConfig.CryptKey);

                // Get user's Qbo connection informations from Database.
                QboConnectionInfo qboConnectionInfo = await DbToQuickbooksUtility.GetQboConnectionInfoAndDecrypt(
                    qboConnectionInfoDb, dbConfig.CryptKey, command);

                // Get User Export Settings.
                QboIntegrationSettingDb _qboSettingDb = new QboIntegrationSettingDb(dbConfig.QuickBooksDbIntegrationSettingTableName, msSql);
                DataTable settingTb = await _qboSettingDb.GetIntegrationSettingAsync(command);
                QboIntegrationSetting qboIntergrationSetting = ComlexTypeConvertExtension.
                                                                DatatableToList<QboIntegrationSetting>(settingTb).FirstOrDefault();

                // To be change to !String.isNullOrEmpty( qboConnectionInfo.authCode )
               if (qboIntergrationSetting != null && qboConnectionInfo.ClientId != null && qboConnectionInfo.ClientSecret != null)
                {
                    // If the ExportOrderToDate is default then handle all orders before this moment.
                    if (qboIntergrationSetting.ExportOrderToDate.Equals(DateTime.MinValue))
                    {
                        qboIntergrationSetting.ExportOrderToDate = DateTime.UtcNow;
                    }
                    // Initialize Qbo connection.
                    QboUniversal qboUniversal = await QboUniversal.CreateAsync(qboConnectionInfo, qboConnectionConfig);

                    // Udpate Refresh Token in Database if it got updated during Qbo connection initailizaion.
                    if (qboUniversal._qboConnectionTokenStatus.RefreshTokenStatus == ConnectionTokenStatus.Updated)
                    {
                        await qboConnectionInfoDb.UpdateQboRefreshTokenAsync(qboUniversal._qboConnectionInfo.RefreshToken,
                            qboUniversal._qboConnectionInfo.LastRefreshTokUpdate, command);
                    }
                    // Udpate Access Token in Database if it got refreshed during Qbo connection initailizaion.
                    if (qboUniversal._qboConnectionTokenStatus.AccessTokenStatus == ConnectionTokenStatus.Updated)
                    {
                        await qboConnectionInfoDb.UpdateQboAccessTokenAsync(qboUniversal._qboConnectionInfo.AccessToken,
                            qboUniversal._qboConnectionInfo.LastAccessTokUpdate, command);
                    }

                    QboOrderDb qboOrderDb = new QboOrderDb(dbConfig.QuickBooksDbOrderCentralTableName, msSql);
                    QboOrderItemLineDb qboOrderItemLineDb = new QboOrderItemLineDb(dbConfig.QuickBooksDbItemLineCentralTableName, msSql);

                    //CompanyInfo companyInfo = await qboUniversal.GetCompanyInfo();
                    bool exist = await qboUniversal.SalesReceiptExist("1003-100026133");
                    Console.WriteLine(QboSyncStatus.PendingSummary);


                }



                //return toExportCommands.Count > 0 ? messageSerializer.Serialize(toExportCommands) : null;
            }
        }
    }
}

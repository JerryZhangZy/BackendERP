using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksFunc
{
    public static class DbToQuickbooksSummaryTimeTrigger
    {
        [FunctionName("DbToQuickbooksSummaryTimeTrigger")]
        [return: Queue(RouteNames.QuickBooksSummaryExportQueue)]
        public static async System.Threading.Tasks.Task<string> RunAsync(
            //[TimerTrigger("1 0 0 ? * * *")] TimerInfo myTimer, string message, ILogger log, ExecutionContext context)
            [TimerTrigger("01 00 00 * * *")] TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            IMessageSerializer messageSerializer = new JsonMessageSerializer();
            log.LogInformation($"C# Time trigger DbToQuickbooksSummaryTimeTrigger excuted at: {DateTime.Now}");
            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);
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

                MsSqlUniversal msSql = await MsSqlUniversal.CreateAsync(
                    dbConfig.QuickBooksDbConnectionString
                    , dbConfig.UseAzureManagedIdentity
                    , dbConfig.TokenProviderConnectionString
                    , dbConfig.AzureTenantId
                    );

                QboIntegrationSettingDb qboSettingDb = new QboIntegrationSettingDb(dbConfig.QuickBooksDbIntegrationSettingTableName, msSql);
                DataTable settingsTb = await qboSettingDb.GetSummarySettingsAsync();
                List<QboIntegrationSetting> qboIntergrationSettings = 
                    ComlexTypeConvertExtension.DatatableToList<QboIntegrationSetting>(settingsTb);

                List<Command> commands = qboIntergrationSettings.Select(x => new Command(x.MasterAccountNum, x.ProfileNum)).ToList();
                return messageSerializer.Serialize(commands);
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Something went wrong with the DbToQuickbooksSummaryTimeTrigger");
                throw;
            }

        }
    }
}

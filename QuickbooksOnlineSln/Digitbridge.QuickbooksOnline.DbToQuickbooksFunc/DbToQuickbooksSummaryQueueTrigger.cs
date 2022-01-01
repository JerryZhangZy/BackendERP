using System;
using System.Collections.Generic;
using System.Threading;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure;
using Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Model;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksFunc
{
    public static class DbToQuickbooksSummaryQueueTrigger
    {
        [FunctionName("DbToQuickbooksSummaryQueueTrigger")]
        [return: Queue(RouteNames.QuickBooksSummaryExportQueue)]
        public static async System.Threading.Tasks.Task<string> RunAsync(
            [QueueTrigger(RouteNames.QuickBooksSummaryExportQueue, Connection = "AzureWebJobsStorage")] 
            string summaryCommands, ILogger log, Microsoft.Azure.WebJobs.ExecutionContext context,
            [Table("QuickbooksOnlineSummaryExportErrorLog", Connection = "AzureLogConn")] CloudTable logTable)
        {
            var instanceWatch = new System.Diagnostics.Stopwatch();
            instanceWatch.Start();

            // record the first MasterAccNum and ProfileNum in order to locate the database when there are exceptions
            int firstMasterAccNum = 0;
            int firstProfileNum = 0;

            IMessageSerializer messageSerializer = new JsonMessageSerializer();

            log.LogInformation($"C# Queue trigger function processed: {summaryCommands}");

            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);

                List<Command> commands = messageSerializer.Desrialize<List<Command>>(summaryCommands);
                List<Command> toExportCommands =
                    commands.ConvertAll(command_ => new Command(command_.MasterAccountNum, command_.ProfileNum));

                firstMasterAccNum = commands.Count > 0 ? commands[0].MasterAccountNum : 0;
                firstProfileNum = commands.Count > 0 ? commands[0].ProfileNum : 0;

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

                foreach (Command command in commands)
                {
                    CentralOrdersExport centralOrdersEmport =
                        await CentralOrdersExport.CreatAsync(dbConfig, qboConnectionConfig, command, instanceWatch);
                    if (centralOrdersEmport != null)
                    {
                        DetailTransferResult result = await centralOrdersEmport.HandleOrderExportToQboAsync(OrderTransferType.CreateDailySummary);

                        if (result.Status != DetailResultStatus.SkipForTimeGap)
                        {
                            // Has Errors while handling, log them in table
                            if (!String.IsNullOrEmpty(result.Message))
                            {
                                ErrorLogAzureTableEntity summaryExportErrorLog = new ErrorLogAzureTableEntity(
                                $"{command.MasterAccountNum}_{command.ProfileNum}:{Guid.NewGuid()}",
                                $"SummaryExport_{DateTime.UtcNow.ToString("yyyy-MM-dd")}");

                                summaryExportErrorLog.ErrorMsg = result.Message;

                                TableOperation insertExportErrorLog = TableOperation.InsertOrMerge(summaryExportErrorLog);
                                await logTable.ExecuteAsync(insertExportErrorLog);
                            }
                            // No orders left then exclude it in the toExportCommands
                            if (result.PendingCount == 0)
                            {
                                toExportCommands.RemoveAll(c =>
                                   c.MasterAccountNum == command.MasterAccountNum &&
                                   c.ProfileNum == command.ProfileNum
                                );
                            }
                        }
                        else
                        {
                            // Prevent next loop got stop because of short time interval.
                            Thread.Sleep(10000);
                        }
                    }
                    else
                    {
                        log.LogInformation($"QBO Connection Profile is incompleted for : " +
                            $"MasterAccNum: {command.MasterAccountNum} ProfileNum : {command.ProfileNum}" +
                            $", please check the OAuth 2.0 with QBO");
                        toExportCommands.RemoveAll(c =>
                               c.MasterAccountNum == command.MasterAccountNum &&
                               c.ProfileNum == command.ProfileNum
                            );
                    }
                }

                return toExportCommands.Count > 0 ? messageSerializer.Serialize(toExportCommands) : null;

            }
            catch (Exception ex)
            {
                try
                {
                    // Azure Table Object for Exceptioni Log Table Insertion
                    ErrorLogAzureTableEntity summaryExportExceptionLog = new ErrorLogAzureTableEntity(
                         $"{firstMasterAccNum}_{firstProfileNum}:{Guid.NewGuid()}",
                            $"SummaryExport_{DateTime.UtcNow.ToString("yyyy-MM-dd")}");

                    summaryExportExceptionLog.ExceptionMsg = ex.Message;

                    TableOperation insertExportExceptionLog = TableOperation.InsertOrMerge(summaryExportExceptionLog);
                    await logTable.ExecuteAsync(insertExportExceptionLog);
                }
                catch
                {
                    log.LogError(ex, $"Something went wrong when logged Exception in table, {summaryCommands}");
                    throw;
                }

                log.LogError(ex, $"Something went wrong with the DbToQuickbooksSummaryQueueTrigger, {summaryCommands}");
                throw;
            }
        }
    }
}

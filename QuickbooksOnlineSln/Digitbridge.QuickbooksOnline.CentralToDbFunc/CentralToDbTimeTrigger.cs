using System;
using System.Collections.Generic;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using Digitbridge.QuickbooksOnline.Db.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using UneedgoHelper.DotNet.Common;

namespace Digitbridge.QuickbooksOnline.CentralToDbFunc
{
    public static class CentralToDbTimeTrigger
    {
        [FunctionName("CentralToDbTimeTrigger")]
        [return: Queue(RouteNames.QuickBooksOrderImportQueue)]
        public static async System.Threading.Tasks.Task<string> RunAsync(
            [Queue(RouteNames.QuickBooksOrderExportQueue)] IAsyncCollector<string> outputQueueItem,
            [TimerTrigger("0 */25 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);

                log.LogInformation
                (
                    $"CentralToDbTimeTrigger function executed at: {DateTime.Now}" +
                    "Sending a Command Message [{\"MasterAccountNum\":" + 
                    appSetting.TimeTriggerEnQueueMasterAccountNumber + ",\"ProfileNum\":" +
                    appSetting.TimeTriggerEnQueueProfileNumber + "}] into " +
                    $"Queue: {RouteNames.QuickBooksOrderImportQueue} and " +
                    $"{RouteNames.QuickBooksOrderExportQueue}"
                );

                IMessageSerializer messageSerializer = new JsonMessageSerializer();
                List<Command> commands = new List<Command>()
                {
                    new Command
                    (
                         appSetting.TimeTriggerEnQueueMasterAccountNumber.ForceToInt(),
                         appSetting.TimeTriggerEnQueueProfileNumber.ForceToInt()
                    )
                };
                // Also add commands to orderExport Queue to trigger orderExport from time to time
                await outputQueueItem.AddAsync(messageSerializer.Serialize(commands));

                return messageSerializer.Serialize(commands);

            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Something went wrong with the CentralToDbTimeTrigger");
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPEventSDK;
using DigitBridge.CommerceCentral.ERPEventSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(SalesOrderBroker))]
    public static class SalesOrderBroker
    {
        [FunctionName("CreateSalesOrderByCentralOrder")]
        public static async Task CreateSalesOrderByCentralOrder([QueueTrigger("erp-create-salesorder-by-centralorder")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var salesOrderClient = new SalesOrderClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new SalesOrderPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new SalesOrderManager(dbFactory);
                (bool ret, List<string> salesOrderUuids) = await svc.CreateSalesOrderByChannelOrderIdAsync(message.ProcessUuid);

                eventDto.ActionStatus = ret ? 0 : 1;
                eventDto.EventMessage = svc.Messages.ObjectToString();
            }
            catch (Exception e)
            {
                eventDto.ActionStatus = 1;
                eventDto.EventMessage = e.ObjectToString();
            }
            finally
            {
                await salesOrderClient.SendActionResultAsync(eventDto);
            }
        }
    }
}

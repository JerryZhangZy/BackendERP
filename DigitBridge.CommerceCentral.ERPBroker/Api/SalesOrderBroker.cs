using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    public static class SalesOrderBroker
    {
        [FunctionName("CreateSalesOrderByCentralOrder")]
        public static async Task CreateSalesOrderByCentralOrder([QueueTrigger("erp-create-salesorder-by-centralorder")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new SalesOrderPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new SalesOrderManager(dbFactory);
                (bool ret, List<string> salesOrderUuids) = await svc.CreateSalesOrderByChannelOrderIdAsync(message.ProcessUuid);

                ErpInvoiceEventClientHelper.UpdateEventERPAsync(ret, message, svc.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                ErpInvoiceEventClientHelper.UpdateEventERPAsync(false, message, e.ObjectToString());
            }
        }
    }
}

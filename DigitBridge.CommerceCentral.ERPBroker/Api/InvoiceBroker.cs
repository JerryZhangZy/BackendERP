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
    public static class InvoiceBroker
    {
        [FunctionName("CreateInvoiceByOrderShipment")]
        public static async Task CreateInvoiceByOrderShipment([QueueTrigger("erp-create-invoice-by-ordershipment")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new InvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new InvoiceManager(dbFactory);
                (bool ret, string invoiceUuid) = await svc.CreateInvoiceByOrderShipmentIdAsync(message.ProcessUuid);

                ErpEventClientHelper.UpdateEventERPAsync(ret, message, svc.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                ErpEventClientHelper.UpdateEventERPAsync(false, message, e.ObjectToString());
            }
        }

    }
}

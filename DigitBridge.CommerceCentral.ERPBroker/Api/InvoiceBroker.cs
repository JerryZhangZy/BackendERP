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
    [ApiFilter(typeof(InvoiceBroker))]
    public static class InvoiceBroker
    {
        [FunctionName("CreateInvoiceByOrderShipment")]
        public static async Task CreateInvoiceByOrderShipment([QueueTrigger("erp-create-invoice-by-ordershipment")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var invoiceClient = new InvoiceClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new InvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new InvoiceManager(dbFactory);
                (bool ret, string invoiceUuid) = await svc.CreateInvoiceByOrderShipmentIdAsync(message.ProcessUuid);
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
                await invoiceClient.SendActionResultAsync(eventDto);
            }

        }

    }
}

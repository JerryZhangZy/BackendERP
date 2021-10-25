using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPEventSDK;
using DigitBridge.CommerceCentral.ERPEventSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.QuickBooks.Integration;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPQuickbooksBroker
{
    public static class InvoiceBroker
    {
        #region invoice
        /// <summary>
        /// consume message then add/update erp invoice to qbo invoice.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ExportErpInvoiceToQbo")]
        public static async Task ExportErpInvoiceToQbo([QueueTrigger(QueueName.Erp_Qbo_Invoice_Queue)] string myQueueItem, ILogger log)
        {
            var invoiceClient = new QboInvoiceClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboInvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboInvoiceService(payload, dataBaseFactory);
                var success = await service.ExportByUuidAsync(message.ProcessUuid);

                eventDto.ActionStatus = success ? 0 : 1;
                eventDto.EventMessage = service.Messages.ObjectToString();
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

        /// <summary>
        /// consume message then void qbo invoice.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("VoidQboInvoice")]
        public static async Task VoidQboInvoice([QueueTrigger(QueueName.Erp_Qbo_Invoice_Void_Queue)] string myQueueItem, ILogger log)
        {
            var invoiceClient = new QboInvoiceClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboInvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboInvoiceService(payload, dataBaseFactory);
                var success = await service.VoidQboInvoiceByUuidAsync(message.ProcessUuid);

                eventDto.ActionStatus = success ? 0 : 1;
                eventDto.EventMessage = service.Messages.ObjectToString();
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

        #endregion 
    }
}

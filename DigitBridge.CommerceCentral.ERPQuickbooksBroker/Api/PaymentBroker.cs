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
    [ApiFilter(typeof(PaymentBroker))]
    public static class PaymentBroker
    {
        #region payment
        /// <summary>
        /// consume message then add/update erp payment to qbo payment
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ExportErpPaymentToQbo")]
        public static async Task ExportErpPaymentToQbo([QueueTrigger(QueueName.Erp_Qbo_Payment_Queue)] string myQueueItem, ILogger log)
        {
            var paymentClient = new QboPaymentClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboPaymentPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboPaymentService(payload, dataBaseFactory); 

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
                await paymentClient.SendActionResultAsync(eventDto);
            }
        }

        /// <summary>
        /// consume message then delete qbo payment.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("DeleteQboPayment")]
        public static async Task DeleteQboPayment([QueueTrigger(QueueName.Erp_Qbo_Payment_Delete_Queue)] string myQueueItem, ILogger log)
        {
            var paymentClient = new QboPaymentClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboPaymentPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboPaymentService(payload, dataBaseFactory);
                 
                var success = await service.DeleteQboPaymentByUuidAsync(message.ProcessUuid);


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
                await paymentClient.SendActionResultAsync(eventDto);
            }
        }

        #endregion
    }
}

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
    [ApiFilter(typeof(RefundBroker))]
    public static class RefundBroker
    {
        #region quick books Refund
        /// <summary>
        /// consume message then add/update erp return to qbo refund.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ExportErpReturnToQbo")]
        public static async Task ExportErpReturnToQbo([QueueTrigger(QueueName.Erp_Qbo_Return_Queue)] string myQueueItem, ILogger log)
        {
            var returnClient = new QboReturnClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboRefundPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboRefundService(payload, dataBaseFactory);

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
                await returnClient.SendActionResultAsync(eventDto);
            }
        }

        /// <summary>
        /// consume message then delete qbo refund.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("DeleteQboRefund")]
        public static async Task DeleteQboRefund([QueueTrigger(QueueName.Erp_Qbo_Return_Delete_Queue)] string myQueueItem, ILogger log)
        {
            var returnClient = new QboReturnClient();
            var eventDto = new UpdateErpEventDto();
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboRefundPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                eventDto.EventUuid = message.EventUuid;
                eventDto.MasterAccountNum = message.MasterAccountNum;
                eventDto.ProfileNum = message.ProfileNum;

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboRefundService(payload, dataBaseFactory);

                var success = await service.DeleteQboRefundByUuidAsync(message.ProcessUuid);

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
                await returnClient.SendActionResultAsync(eventDto);
            }
        }

        #endregion
    }
}

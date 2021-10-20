using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.QuickBooks.Integration;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPQuickbooksBroker
{
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
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);

                var payload = new QboRefundPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboRefundService(payload, dataBaseFactory);

                var arrs = message.ProcessSource.Split("_");
                var invoiceNumber = arrs[0];
                var tranNumber = arrs[1];

                var success = await service.ExportAsync(invoiceNumber, int.Parse(tranNumber));

                ErpEventClientHelper.UpdateEventERPAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                ErpEventClientHelper.UpdateEventERPAsync(false, message, e.ObjectToString());
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
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);

                var payload = new QboRefundPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboRefundService(payload, dataBaseFactory);

                var arrs = message.ProcessSource.Split("_");
                var invoiceNumber = arrs[0];
                var tranNumber = arrs[1];

                var success = await service.ExportAsync(invoiceNumber, int.Parse(tranNumber));

                ErpEventClientHelper.UpdateEventERPAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                ErpEventClientHelper.UpdateEventERPAsync(false, message, e.ObjectToString());
            }
        }

        #endregion
    }
}

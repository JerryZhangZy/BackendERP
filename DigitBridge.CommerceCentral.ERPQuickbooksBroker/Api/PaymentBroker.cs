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
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);

                var payload = new QboPaymentPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboPaymentService(payload, dataBaseFactory);

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
        /// consume message then delete qbo payment.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("DeleteQboPayment")]
        public static async Task DeleteQboPayment([QueueTrigger(QueueName.Erp_Qbo_Payment_Delete_Queue)] string myQueueItem, ILogger log)
        {
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);

                var payload = new QboPaymentPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboPaymentService(payload, dataBaseFactory);

                var arrs = message.ProcessSource.Split("_");
                var invoiceNumber = arrs[0];
                var tranNumber = arrs[1];

                var success = await service.DeleteQboPaymentAsync(invoiceNumber, int.Parse(tranNumber));

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

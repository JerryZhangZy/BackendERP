using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.QuickBooks.Integration;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    public static class ERPQuickBooksBroker
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
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new QboInvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboInvoiceService(payload, dataBaseFactory);
                var success = await service.ExportAsync(message.ProcessSource);

                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
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
            ERPQueueMessage message = null;
            try
            {
                message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);

                var payload = new QboInvoicePayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };

                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new QboInvoiceService(payload, dataBaseFactory);
                var success = await service.VoidQboInvoiceAsync(message.ProcessSource);

                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
            }
        }

        #endregion


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


                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
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

                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
            }
        }

        #endregion


        #region return
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

                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
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

                EventServieHelper.UpdateEventAsync(success, message, service.Messages.ObjectToString());
            }
            catch (Exception e)
            {
                EventServieHelper.UpdateEventAsync(false, message, e.ObjectToString());
            }
        }

        #endregion

    }
}

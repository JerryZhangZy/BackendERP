using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;
using System.Collections.Generic;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboPaymentService : QboPaymentApi, IQboPaymentService
    {
        private List<InvoiceTransaction> paymentData;
        private QboIntegrationSetting _setting;
        private QboPaymentPayload _payload;
        private InvoiceData invoiceData;
        private string invoiceTxtId;

        #region Service Property 
        private InvoicePaymentService _PaymentService;
        protected InvoicePaymentService PaymentService => _PaymentService ??= new InvoicePaymentService(dbFactory);

        #endregion

        public QboPaymentService(QboPaymentPayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }

        #region prepare data 
        protected async Task<bool> LoadPaymentData(string invoiceNumber)
        {
            (paymentData, invoiceData) = await PaymentService.GetPaymentsWithInvoice(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber);
            var success = paymentData != null && paymentData.Count > 0;
            if (!success)
            {
                AddInfo($"Payments not found for invoiceNumber:{invoiceNumber}");
            }
            _payload.Success = success;
            _payload.Messages = this.Messages;
            return success;
        }
        private async Task<bool> LoadInvoiceTxtId()
        {
            var success = await LoadExportLog(invoiceData.InvoiceHeader.InvoiceUuid);
            if (success)
            {
                invoiceTxtId = _exportLog.TxnId;
            }
            else
            {
                AddError($"Quick books invoice not exist for erp invoice number {invoiceData.InvoiceHeader.InvoiceNumber}");
            }
            _payload.Success = success;
            _payload.Messages = this.Messages;
            return success;
        }
        protected async Task<bool> LoadSetting()
        {
            var srv = new QuickBooksSettingInfoService(dbFactory);
            _payload.Success = await srv.GetByPayloadAsync(_payload);
            _payload.Messages = srv.Messages;
            if (_payload.Success)
                _setting = srv.Data.QuickBooksSettingInfo.Setting;
            return _payload.Success;
        }
        #endregion

        #region qbo Payment back to erp 
        /// <summary>
        /// Write qboPayment to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboPayment"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboPaymentToExportLog(Payment qboPayment, string paymentTransUuid)
        {
            var log = new QuickBooksExportLog
            {
                DatabaseNum = _payload.DatabaseNum,
                MasterAccountNum = _payload.MasterAccountNum,
                ProfileNum = _payload.ProfileNum,
                QuickBooksExportLogUuid = Guid.NewGuid().ToString(),
                BatchNum = 0,
                LogType = "Payment",
                LogUuid = paymentTransUuid,
                DocNumber = qboPayment.DocNumber,
                TxnId = qboPayment.Id,
                DocStatus = (int)qboPayment.status
            };
            _payload.Success = await AddExportLogAsync(log);
            return _payload.Success;
        }

        #endregion

        #region Qbo Payment operation.
        /// <summary>
        /// Export erp Payment to qbo Payment by erp Payment number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> Export(string invoiceNumber)
        {
            var success = await LoadPaymentData(invoiceNumber);
            success = success && await LoadInvoiceTxtId();
            success = success && await LoadSetting();
            if (!success) return success;

            foreach (var payment in paymentData)
            {
                success = success && await LoadExportLog(payment.TransUuid);
                if (!success) return success;

                var mapper = new QboPaymentMapper(_setting, _exportLog);
                var qboPayment = mapper.ToPayment(payment, invoiceTxtId, invoiceData);
                try
                {
                    qboPayment = await CreateOrUpdatePayment(qboPayment);
                    _payload.QboPayment = qboPayment;
                }
                catch (Exception e)
                {
                    throw new Exception(qboPayment.ObjectToString(), e);
                }

                success = success && await WriteQboPaymentToExportLog(qboPayment, payment.TransUuid);
            }

            return success;
        }
        #endregion
    }
}

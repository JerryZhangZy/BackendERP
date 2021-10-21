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
        private InvoiceTransactionData paymentData;
        private QboPaymentPayload _payload;

        #region Service Property 
        private InvoicePaymentService _paymentService;
        protected InvoicePaymentService paymentService => _paymentService ??= new InvoicePaymentService(dbFactory);

        #endregion

        public QboPaymentService(QboPaymentPayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }


        #region qbo Payment back to erp 
        /// <summary>
        /// Write qboPayment to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboPayment"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboPaymentToExportLog(Payment qboPayment)
        {
            var exportLog = new QuickBooksExportLog();

            exportLog.LogType = QboLogType.Payment;
            exportLog.LogUuid = paymentData.InvoiceTransaction.TransUuid;
            exportLog.DocNumber = qboPayment.DocNumber ?? string.Empty;
            exportLog.TxnId = qboPayment.Id;
            exportLog.DocStatus = (int)qboPayment.status;
            return await AddExportLogAsync(exportLog);
        }

        #endregion

        #region Handle by number. This for api

        protected async Task<bool> LoadPaymentDataByNumber(string invoiceNumber, int transNum)
        {
            var success = await paymentService.GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            paymentData = paymentService.Data;
            if (!success)
            {
                this.Messages = this.Messages.Concat(paymentService.Messages).ToList();
            }
            return success;
        }

        /// <summary>
        /// Export erp Payment to qbo Payment by erp Payment number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByNumber(invoiceNumber, transNum);

                success = success && await GetSetting();

                success = success && (qboPayment = await GetQboPayment()) != null;
                success = success && (qboPayment = await CreateOrUpdatePayment(qboPayment)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        }

        public async Task<bool> VoidQboPaymentByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByNumber(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid));

                success = success && (qboPayment = await VoidPaymentAsync(txnId)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        }

        public async Task<bool> DeleteQboPaymentByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByNumber(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid));

                success = success && (qboPayment = await DeletePaymentAsync(txnId)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        }

        public async Task<bool> GetQboPaymentByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            try
            {
                success = await LoadPaymentDataByNumber(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid));

                success = success && (_payload.QboPayment = await GetPaymentAsync(txnId)) != null;
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }
            return success;
        }
        #endregion

        #region Handle by uuid. This for internal

        protected async Task<bool> LoadPaymentDataByUuid(string transUuid)
        {
            var success = await paymentService.GetDataByIdAsync(transUuid);
            paymentData = paymentService.Data;
            if (!success)
            {
                this.Messages = this.Messages.Concat(paymentService.Messages).ToList();
            }
            return success;
        }


        /// <summary>
        /// Export erp Payment to qbo Payment by erp Payment number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByUuidAsync(string tranUuid)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByUuid(tranUuid);

                success = success && await GetSetting();

                success = success && (qboPayment = await GetQboPayment()) != null;
                success = success && (qboPayment = await CreateOrUpdatePayment(qboPayment)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        }

        public async Task<bool> VoidQboPaymentByUuidAsync(string tranUuid)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByUuid(tranUuid);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid));

                success = success && (qboPayment = await VoidPaymentAsync(txnId)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        }

        public async Task<bool> DeleteQboPaymentByUuidAsync(string tranUuid)
        {
            var success = false;
            Payment qboPayment = null;
            try
            {
                success = await LoadPaymentDataByUuid(tranUuid);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid));

                success = success && (qboPayment = await DeletePaymentAsync(txnId)) != null;

                success = success && await WriteQboPaymentToExportLog(qboPayment);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboPayment);

            return success;
        } 

        #endregion

        #region private method 

        private async System.Threading.Tasks.Task WriteResult(bool success, Payment qboPayment)
        {
            if (success)
            {
                _payload.QboPayment = qboPayment;
            }
            else
            {
                AddInfo(qboPayment.ObjectToString());
                await SaveExportErrorLogAsync(QboLogType.Payment);
            }
        }

        private async Task<Payment> GetQboPayment()
        {
            Payment qboPayment = null;

            var txnId = await GetTxnId(paymentData.InvoiceTransaction.TransUuid);

            if (!string.IsNullOrEmpty(txnId))
            {
                qboPayment = await GetPaymentAsync(txnId);// when qbo deleted this will return null.
            }
            if (qboPayment == null)
                qboPayment = new Payment();

            //get invoice txnid;
            var invoiceTxnId = await GetTxnId(paymentData.InvoiceTransaction.InvoiceUuid);
            var mapper = new QboPaymentMapper(_setting, invoiceTxnId);
            qboPayment = mapper.ToPayment(paymentData, qboPayment);
            return qboPayment;
        }

        #endregion
    }
}

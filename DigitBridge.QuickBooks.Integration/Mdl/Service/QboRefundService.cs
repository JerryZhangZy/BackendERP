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
    public class QboRefundService : QboRefundApi, IQboRefundService
    {
        private InvoiceTransactionData RefundData;
        private QboRefundPayload _payload;

        #region Service Property 
        private InvoiceReturnService _ReturnService;
        protected InvoiceReturnService ReturnService => _ReturnService ??= new InvoiceReturnService(dbFactory);

        #endregion

        public QboRefundService(QboRefundPayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }

        #region prepare data 
        protected async Task<bool> LoadRefundData(string invoiceNumber, int transNum)
        {
            var success = await ReturnService.GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            RefundData = ReturnService.Data;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ReturnService.Messages).ToList();
            }
            return success;
        }
        #endregion

        #region qbo Refund back to erp 
        /// <summary>
        /// Write qboRefund to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboRefund"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboRefundToExportLog(RefundReceipt qboRefund)
        {
            var exportLog = new QuickBooksExportLog();

            exportLog.LogType = "Refund";
            exportLog.LogUuid = RefundData.InvoiceTransaction.TransUuid;
            exportLog.DocNumber = qboRefund.DocNumber ?? string.Empty;
            exportLog.TxnId = qboRefund.Id;
            exportLog.DocStatus = (int)qboRefund.status;
            return await AddExportLogAsync(exportLog);
        }

        #endregion

        #region Qbo Refund operation.
        /// <summary>
        /// Export erp Refund to qbo Refund by erp Refund number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundData(invoiceNumber, transNum);

                success = success && await GetSetting();

                success = success && (qboRefund = await GetQboRefund()) != null;
                success = success && (qboRefund = await CreateOrUpdateRefund(qboRefund)) != null;

                success = success && await WriteQboRefundToExportLog(qboRefund);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            if (success)
            {
                _payload.QboRefund = qboRefund;
            }
            else
            {
                AddInfo(qboRefund.ObjectToString());
                await SaveExportErrorLogAsync();
            }

            return success;
        }

        private async Task<RefundReceipt> GetQboRefund()
        {
            RefundReceipt qboRefund = null;

            var txnId = await GetTxnId(RefundData.InvoiceTransaction.TransUuid);

            if (!string.IsNullOrEmpty(txnId))
            {
                qboRefund = await GetRefundAsync(txnId);// when qbo deleted this will return null.
            }
            if (qboRefund == null)
                qboRefund = new RefundReceipt();

            //get invoice txnid;
            var invoiceTxnId = await GetTxnId(RefundData.InvoiceTransaction.InvoiceUuid);
            var mapper = new QboRefundMapper(_setting);
            qboRefund = mapper.ToRefund(RefundData, qboRefund);
            return qboRefund;
        }

        public async Task<bool> VoidQboRefundAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundData(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(RefundData.InvoiceTransaction.TransUuid));

                success = success && (qboRefund = await VoidRefundAsync(txnId)) != null;

                success = success && await WriteQboRefundToExportLog(qboRefund);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            if (success)
            {
                _payload.QboRefund = qboRefund;
            }
            else
            {
                AddInfo(qboRefund.ObjectToString());
                await SaveExportErrorLogAsync();
            }

            return success;
        }

        public async Task<bool> DeleteQboRefundAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundData(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(RefundData.InvoiceTransaction.TransUuid));

                success = success && (qboRefund = await DeleteRefundAsync(txnId)) != null;

                success = success && await WriteQboRefundToExportLog(qboRefund);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            if (success)
            {
                _payload.QboRefund = qboRefund;
            }
            else
            {
                AddInfo(qboRefund.ObjectToString());
                await SaveExportErrorLogAsync();
            }

            return success;
        }

        public async Task<bool> GetQboRefundAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            try
            {
                success = await LoadRefundData(invoiceNumber, transNum);

                string txnId = null;
                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(RefundData.InvoiceTransaction.TransUuid));

                success = success && (_payload.QboRefund = await GetRefundAsync(txnId)) != null;
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }
            return success;
        }
        #endregion
    }
}

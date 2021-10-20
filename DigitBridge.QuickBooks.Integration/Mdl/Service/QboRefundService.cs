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

        #region Handle by number. This for api

        protected async Task<bool> LoadRefundDataByNumber(string invoiceNumber, int transNum)
        {
            var success = await ReturnService.GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transNum);
            RefundData = ReturnService.Data;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ReturnService.Messages).ToList();
            }
            return success;
        }

        /// <summary>
        /// Export erp Refund to qbo Refund by erp Refund number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByNumber(invoiceNumber, transNum);

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

            await WriteResult(success, qboRefund);

            return success;
        }



        public async Task<bool> VoidQboRefundByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByNumber(invoiceNumber, transNum);

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

            await WriteResult(success, qboRefund);

            return success;
        }

        public async Task<bool> DeleteQboRefundByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByNumber(invoiceNumber, transNum);

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

            await WriteResult(success, qboRefund);

            return success;
        }

        public async Task<bool> GetQboRefundByNumberAsync(string invoiceNumber, int transNum)
        {
            var success = false;
            try
            {
                success = await LoadRefundDataByNumber(invoiceNumber, transNum);

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

        #region Handle by uuid. This for internal

        protected async Task<bool> LoadRefundDataByUuid(string transUuid)
        {
            var success = await ReturnService.GetDataByIdAsync(transUuid);
            RefundData = ReturnService.Data;
            if (!success)
            {
                this.Messages = this.Messages.Concat(ReturnService.Messages).ToList();
            }
            return success;
        }

        /// <summary>
        /// Export erp Refund to qbo Refund by erp Refund number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByUuidAsync(string transUuid)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByUuid(transUuid);

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

            await WriteResult(success, qboRefund);

            return success;
        }



        public async Task<bool> VoidQboRefundByUuidAsync(string transUuid)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByUuid(transUuid);

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

            await WriteResult(success, qboRefund);

            return success;
        }

        public async Task<bool> DeleteQboRefundByUuidAsync(string transUuid)
        {
            var success = false;
            RefundReceipt qboRefund = null;
            try
            {
                success = await LoadRefundDataByUuid(transUuid);

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

            await WriteResult(success, qboRefund);

            return success;
        }
        #endregion

        #region private method 

        private async System.Threading.Tasks.Task WriteResult(bool success, RefundReceipt qboRefund)
        {
            if (success)
            {
                _payload.QboRefund = qboRefund;
            }
            else
            {
                AddInfo(qboRefund.ObjectToString());
                await SaveExportErrorLogAsync();
            }
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

        #endregion
    }
}

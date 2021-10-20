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
    public class QboInvoiceService : QboInvoiceApi, IQboInvoiceService
    {
        private InvoiceData _invoiceData;
        private QboInvoicePayload _payload;

        #region Service Property 
        private InvoiceService _invoiceService;
        protected InvoiceService invoiceService => _invoiceService ??= new InvoiceService(dbFactory);

        #endregion

        public QboInvoiceService(QboInvoicePayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }


        #region qbo invoice back to erp 
        /// <summary>
        /// Write qboInvoice to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboInvoiceToExportLog(Invoice qboInvoice)
        {
            var exportLog = new QuickBooksExportLog();
            exportLog.LogType = "Invoice";
            exportLog.LogUuid = _invoiceData.UniqueId;
            exportLog.DocNumber = qboInvoice.DocNumber;
            exportLog.TxnId = qboInvoice.Id;
            exportLog.DocStatus = (int)qboInvoice.status;
            return await AddExportLogAsync(exportLog);
        }
        /// <summary>
        ///  Write docnumber to erp invoice.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteDocNumberToErpInvoice(string docNumber)
        {
            var success = await invoiceService.UpdateInvoiceDocNumberAsync(_invoiceData.UniqueId, docNumber);
            if (!success)
                this.Messages.Concat(invoiceService.Messages);
            return success;
        }
        #endregion

        #region Handle by number. This for api

        protected async Task<bool> LoadInvoiceDataByNumber(string invoiceNumber)
        {
            var success = await invoiceService.GetByNumberAsync(_payload, invoiceNumber);
            if (success)
                _invoiceData = invoiceService.Data;
            else
                this.Messages = this.Messages.Concat(invoiceService.Messages).ToList();
            return success;
        }

        /// <summary>
        /// get qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> GetQboInvoiceByNumberAsync(string invoiceNumber)
        {
            var success = false;
            try
            {
                success = await LoadInvoiceDataByNumber(invoiceNumber);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);

                success = success && (_payload.QboInvoice = await GetInvoiceAsync(txnId)) != null;
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }
            return success;
        }

        /// <summary>
        /// Export erp invoice to qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByNumberAsync(string invoiceNumber)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByNumber(invoiceNumber);
                success = success && await GetSetting();

                success = success && (qboInvoice = await GetQboInvoice()) != null;
                success = success && (qboInvoice = await CreateOrUpdateInvoice(qboInvoice)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(qboInvoice.DocNumber);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }

        /// <summary>
        /// delete qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteQboInvoiceByNumberAsync(string invoiceNumber)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByNumber(invoiceNumber);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);

                success = success && (qboInvoice = await DeleteInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }

        /// <summary>
        /// void qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> VoidQboInvoiceByNumberAsync(string invoiceNumber)
        {
            bool success;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByNumber(invoiceNumber);

                string txnId = null;

                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid));

                success = success && (qboInvoice = await VoidInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);

                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }

        #endregion

        #region Handle by uuid. This for internal

        protected async Task<bool> LoadInvoiceDataByUuid(string invoiceUuid)
        {
            var success = await invoiceService.GetDataByIdAsync(invoiceUuid);
            if (success)
                _invoiceData = invoiceService.Data;
            else
                this.Messages = this.Messages.Concat(invoiceService.Messages).ToList();
            return success;
        }

        /// <summary>
        /// Export erp invoice to qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportByUuidAsync(string invoiceUuid)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByUuid(invoiceUuid);
                success = success && await GetSetting();

                success = success && (qboInvoice = await GetQboInvoice()) != null;
                success = success && (qboInvoice = await CreateOrUpdateInvoice(qboInvoice)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(qboInvoice.DocNumber);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }

        /// <summary>
        /// delete qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteQboInvoiceByUuidAsync(string invoiceUuid)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByUuid(invoiceUuid);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);

                success = success && (qboInvoice = await DeleteInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }

        /// <summary>
        /// void qbo invoice by erp invoiceUuid. this method for internal.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> VoidQboInvoiceByUuidAsync(string invoiceUuid)
        {
            bool success;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceDataByUuid(invoiceUuid);

                string txnId = null;

                success = success && !string.IsNullOrEmpty(txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid));

                success = success && (qboInvoice = await VoidInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);

                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                success = false;
                AddError(e.ObjectToString());
            }

            await WriteResult(success, qboInvoice);

            return success;
        }
        #endregion

        #region private method 

        private async System.Threading.Tasks.Task WriteResult(bool success, Invoice qboInvoice)
        {
            if (success)
            {
                _payload.QboInvoice = qboInvoice;
            }
            else
            {
                AddInfo(qboInvoice.ObjectToString());
                await SaveExportErrorLogAsync();
            }
        }

        private async Task<Invoice> GetQboInvoice()
        {
            var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);

            Invoice qboInvoice = null;
            if (!string.IsNullOrEmpty(txnId))
            {
                qboInvoice = await GetInvoiceAsync(txnId);// when qbo deleted this will return null.
            }
            if (qboInvoice == null)
                qboInvoice = new Invoice();

            var mapper = new QboInvoiceMapper(_setting);
            return mapper.ToInvoice(_invoiceData, qboInvoice);
        }

        #endregion
    }
}

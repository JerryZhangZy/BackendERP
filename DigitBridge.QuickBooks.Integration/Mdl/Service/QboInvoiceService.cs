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
        private QboIntegrationSetting _setting;
        private QboInvoicePayload _payload;

        #region Service Property 
        private InvoiceService _invoiceService;
        protected InvoiceService invoiceService => _invoiceService ??= new InvoiceService(dbFactory);

        #endregion

        public QboInvoiceService(QboInvoicePayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }

        #region prepare data 
        protected async Task<bool> LoadInvoiceData(string invoiceNumber)
        {
            var success = await invoiceService.GetDataAsync(_payload, invoiceNumber);
            if (success)
                _invoiceData = invoiceService.Data;
            else
                this.Messages.Concat(invoiceService.Messages);
            return success;
        }

        protected async Task<bool> LoadSetting()
        {
            var srv = new QuickBooksSettingInfoService(dbFactory);
            var success = await srv.GetByPayloadAsync(_payload);
            if (success)
                _setting = srv.Data.QuickBooksSettingInfo.Setting;
            else
                this.Messages.Concat(srv.Messages);
            return success;
        }
        #endregion

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

        #region Qbo invoice operation
        /// <summary>
        /// Export erp invoice to qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> ExportAsync(string invoiceNumber)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceData(invoiceNumber);
                success = success && await LoadSetting();

                success = success && (qboInvoice = await GetQboInvoice()) != null;
                success = success && (qboInvoice = await CreateOrUpdateInvoice(qboInvoice)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(qboInvoice.DocNumber);
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }

            if (success)
            {
                _payload.QboInvoice = qboInvoice;
            }
            else
            {
                //todo write qboInvoice and message to error log.
            }

            return success;
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

        /// <summary>
        /// get qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> GetQboInvoiceAsync(string invoiceNumber)
        {
            var success = false;
            try
            {
                success = await LoadInvoiceData(invoiceNumber);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);

                success = success && (_payload.QboInvoice = await GetInvoiceAsync(txnId)) != null;
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }
            return success;
        }

        /// <summary>
        /// delete qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteQboInvoiceAsync(string invoiceNumber)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceData(invoiceNumber);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);

                success = success && (qboInvoice = await DeleteInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }
            if (success)
            {
                _payload.QboInvoice = qboInvoice;
            }
            else
            {
                //todo write _payload.QboInvoice and message to error log.
            }
            return success;
        }

        /// <summary>
        /// void qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> VoidQboInvoiceAsync(string invoiceNumber)
        {
            var success = false;
            Invoice qboInvoice = null;
            try
            {
                success = await LoadInvoiceData(invoiceNumber);

                var txnId = await GetTxnId(_invoiceData.InvoiceHeader.InvoiceUuid);
                success = !string.IsNullOrEmpty(txnId);
                success = success && (qboInvoice = await VoidInvoiceAsync(txnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(qboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }
            if (success)
            {
                _payload.QboInvoice = qboInvoice;
            }
            else
            {
                //todo write _payload.QboInvoice and message to error log.
            }
            return success;
        }

        #endregion
    }
}

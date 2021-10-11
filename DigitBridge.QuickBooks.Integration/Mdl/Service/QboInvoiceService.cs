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
                AddError(invoiceService.Messages.ObjectToString());
            return success;
        }

        protected async Task<bool> LoadSetting()
        {
            var srv = new QuickBooksSettingInfoService(dbFactory);
            var success = await srv.GetByPayloadAsync(_payload);
            if (success)
                _setting = srv.Data.QuickBooksSettingInfo.Setting;
            else
                AddError(srv.Messages.ObjectToString());
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
            if (_exportLog == null)
            {
                _exportLog = new QuickBooksExportLog();
            }
            _exportLog.LogType = "Invoice";
            _exportLog.LogUuid = _invoiceData.UniqueId;
            _exportLog.DocNumber = qboInvoice.DocNumber;
            _exportLog.TxnId = qboInvoice.Id;
            _exportLog.DocStatus = (int)qboInvoice.status;
            _exportLog.SyncToken = int.Parse(qboInvoice.SyncToken);
            return await AddExportLogAsync();
        }
        /// <summary>
        ///  Write docnumber to erp invoice.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteDocNumberToErpInvoice(string docNumber)
        {
            _payload.Success = await invoiceService.UpdateInvoiceDocNumberAsync(_invoiceData.UniqueId, docNumber);
            _payload.Messages = invoiceService.Messages;
            return _payload.Success;
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
                success = success && await LoadExportLog(_invoiceData.InvoiceHeader.InvoiceUuid);

                if (success)
                {
                    qboInvoice = await GetQboInvoice();
                    qboInvoice = await CreateOrUpdateInvoice(qboInvoice);
                }

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
                //todo write error log.
            }

            return success;
        }
        protected async Task<Invoice> GetQboInvoice()
        {
            Invoice qboInvoice = null;
            if (!string.IsNullOrEmpty(_exportLog.TxnId))
            {
                qboInvoice = await GetInvoiceAsync(_exportLog.TxnId);// when qbo deleted this will return null.
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
                success = success && await LoadExportLog(_invoiceData.InvoiceHeader.InvoiceUuid);
                if (!success) return success;
                _payload.QboInvoice = await GetInvoiceAsync(_exportLog.TxnId);
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
            try
            {
                success = await LoadInvoiceData(invoiceNumber);
                success = success && await LoadExportLog(_invoiceData.InvoiceHeader.InvoiceUuid);

                success = success && (_payload.QboInvoice = await DeleteInvoiceAsync(_exportLog.TxnId)) != null;
               
                success = success && await WriteQboInvoiceToExportLog(_payload.QboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }
            if (!success)
            {
                //todo write error log. 
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
            try
            {
                success = await LoadInvoiceData(invoiceNumber);
                success = success && await LoadExportLog(_invoiceData.InvoiceHeader.InvoiceUuid);

                success = success && (_payload.QboInvoice = await VoidInvoiceAsync(_exportLog.TxnId)) != null;

                success = success && await WriteQboInvoiceToExportLog(_payload.QboInvoice);
                success = success && await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                AddError(e.ObjectToString());
            }
            if (!success)
            {
                //todo write error log. 
            }
            return success;
        }

        #endregion
    }
}

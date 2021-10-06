﻿using DigitBridge.CommerceCentral.ERPDb;
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
        private QuickBooksExportLog _exportLog;
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
            _payload.Success = await invoiceService.GetDataAsync(_payload, invoiceNumber);
            _payload.Messages = invoiceService.Messages;
            if (_payload.Success)
                _invoiceData = invoiceService.Data;
            return _payload.Success;
        }
        protected async Task<bool> LoadExportLog()
        {
            var list = await QuickBooksExportLogService.QueryExportLogByLogUuidAsync(_invoiceData.InvoiceHeader.InvoiceUuid);
            if (list != null)
                _exportLog = list.FirstOrDefault();
            if (_exportLog == null)
                _exportLog = new QuickBooksExportLog();
            return true;
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

        #region qbo invoice back to erp 
        /// <summary>
        /// Write qboInvoice to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboInvoiceToExportLog(Invoice qboInvoice)
        {
            var log = new QuickBooksExportLog
            {
                DatabaseNum = _payload.DatabaseNum,
                MasterAccountNum = _payload.MasterAccountNum,
                ProfileNum = _payload.ProfileNum,
                QuickBooksExportLogUuid = Guid.NewGuid().ToString(),
                BatchNum = 0,
                LogType = "Invoice",
                LogUuid = _invoiceData.UniqueId,
                DocNumber = qboInvoice.DocNumber,
                TxnId = qboInvoice.Id,
                DocStatus = (int)qboInvoice.status
            };
            _payload.Success = await AddExportLogAsync(log);
            return _payload.Success;
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

        #region Qbo invoice operation.
        /// <summary>
        /// Export erp invoice to qbo invoice by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> Export(string invoiceNumber)
        {
            var success = await LoadInvoiceData(invoiceNumber);
            success = success && await LoadSetting();
            success = success && await LoadExportLog();
            if (!success) return success;

            var mapper = new QboInvoiceMapper(_setting, _exportLog);
            var qboInvoice = mapper.ToInvoice(_invoiceData);
            try
            {
                qboInvoice = await CreateOrUpdateInvoice(qboInvoice);
                _payload.QboInvoice = qboInvoice;
            }
            catch (Exception e)
            {
                throw new Exception(qboInvoice.ObjectToString(), e);
            }

            success = success && await WriteQboInvoiceToExportLog(qboInvoice);
            success = success && await WriteDocNumberToErpInvoice(qboInvoice.DocNumber);
            return success;
        }

        /// <summary>
        /// get qbo invoice list by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> GetQboInvoiceList(string invoiceNumber)
        {
            var success = await LoadInvoiceData(invoiceNumber);
            if (!success) return success;
            try
            {
                _payload.QboInvoices = await GetInvoiceAsync(_invoiceData.InvoiceHeader.QboDocNumber);
            }
            catch (Exception e)
            {
                throw new Exception($"GetQboInvoiceList by erp invoiceNumber:{invoiceNumber} ", e);
            }
            return success;
        }

        /// <summary>
        /// delete qbo invoice list by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteQboInvoiceList(string invoiceNumber)
        {
            var success = await LoadInvoiceData(invoiceNumber);
            if (!success) return success;

            if (string.IsNullOrEmpty(_invoiceData.InvoiceHeader.QboDocNumber))
            {
                AddInfo("Data not found.");
                _payload.Success = false;
                _payload.Messages = this.Messages;
                return _payload.Success;
            }

            try
            {
                _payload.QboInvoices = await DeleteInvoiceAsync(_invoiceData.InvoiceHeader.QboDocNumber);
                await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                throw new Exception($"DeleteQboInvoiceList by erp invoiceNumber:{invoiceNumber} ", e);
            }
            return success;
        }

        /// <summary>
        /// void qbo invoice list by erp invoice number.
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public async Task<bool> VoidQboInvoiceList(string invoiceNumber)
        {
            var success = await LoadInvoiceData(invoiceNumber);
            if (!success) return success;

            if (string.IsNullOrEmpty(_invoiceData.InvoiceHeader.QboDocNumber))
            {
                AddInfo("Data not found.");
                _payload.Success = false;
                _payload.Messages = Messages;
                return _payload.Success;
            }

            try
            {
                _payload.QboInvoices = await VoidInvoiceAsync(_invoiceData.InvoiceHeader.QboDocNumber);
                await WriteDocNumberToErpInvoice(string.Empty);
            }
            catch (Exception e)
            {
                throw new Exception($"DeleteQboInvoiceList by erp invoiceNumber:{invoiceNumber} ", e);
            }
            return success;
        }

        #endregion
    }
}

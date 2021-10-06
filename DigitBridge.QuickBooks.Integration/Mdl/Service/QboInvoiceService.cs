using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboInvoiceService : QboInvoiceApi, IQboInvoiceService
    {
        private InvoiceData _invoiceData;
        private QboIntegrationSetting _setting;
        private QuickBooksExportLog _exportLog;
        private InvoicePayload _payload;

        public QboInvoiceService(InvoicePayload payload, IDataBaseFactory dbFactory) : base(payload, dbFactory)
        {
            this._payload = payload;
        }

        #region prepare data 
        protected async Task<bool> LoadInvoiceData(string invoiceNumber)
        {
            var srv = new InvoiceService(dbFactory);
            _payload.Success = await srv.GetDataAsync(_payload, invoiceNumber);
            _payload.Messages = srv.Messages;
            if (_payload.Success)
                _invoiceData = srv.Data;
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
                DocStatus = (int)qboInvoice.status,
                LogDate=DateTime.UtcNow.Date,
                LogTime=DateTime.UtcNow.TimeOfDay
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
            var service = new InvoiceService(dbFactory);
            _payload.Success = await service.UpdateInvoiceDocNumberAsync(_invoiceData.UniqueId, docNumber);
            _payload.Messages = service.Messages;
            return _payload.Success;
        }
        #endregion

        #region Export erp invoice to qbo
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
            }
            catch (Exception e)
            {
                throw new Exception(qboInvoice.ObjectToString(), e);
            }

            success = success && await WriteQboInvoiceToExportLog(qboInvoice);
            success = success && await WriteDocNumberToErpInvoice(qboInvoice.DocNumber);
            return success;
        }

        #endregion

        #region Query invoice from qbo

        #endregion
    }
}

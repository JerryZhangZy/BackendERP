using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using QboInvoiceApi = DigitBridge.QuickBooks.Integration.Mdl.Qbo.QboInvoiceService;
using DigitBridge.Base.Utility;
namespace DigitBridge.QuickBooks.Integration
{
    public class QboInvoiceService : IQboInvoiceService
    {
        protected IDataBaseFactory _dbFactory;
        private InvoiceData _invoiceData;
        private QboIntegrationSetting _setting;
        private QuickBooksExportLog _exportLog;

        public QboInvoiceService(IDataBaseFactory dbFactory)//:  base (  databaseFactory)
        {
            _dbFactory = dbFactory;
        }

        #region prepare data 
        protected async Task<bool> LoadInvoiceData(InvoicePayload payload, string invoiceNumber)
        {
            var srv = new InvoiceService(_dbFactory);
            payload.Success = await srv.GetDataAsync(payload, invoiceNumber);
            payload.Messages = srv.Messages;
            if (payload.Success)
                _invoiceData = srv.Data;
            return payload.Success;
        }
        protected async Task<bool> LoadExportLog()
        {
            var service = new QuickBooksExportLogService(_dbFactory);
            var list = await service.QueryExportLogByLogUuidAsync(_invoiceData.InvoiceHeader.InvoiceUuid);
            var success = list != null;
            if (success)
                _exportLog = list.FirstOrDefault();
            else
                _exportLog = new QuickBooksExportLog();
            return success;

        }
        protected async Task<bool> LoadSetting(InvoicePayload payload)
        {
            var srv = new QuickBooksSettingInfoService(_dbFactory);
            payload.Success = await srv.GetByPayloadAsync(payload);
            payload.Messages = srv.Messages;
            if (payload.Success)
                _setting = srv.Data.QuickBooksSettingInfo.SettingInfo;
            return payload.Success;
            //return new QboIntegrationSetting()
            //{
            //    QboDiscountItemId = "24",
            //    //QboDiscountItemName = "QboDiscountItemName",

            //    QboShippingItemId = "20",
            //    //QboShippingItemName = "QboShippingItemName",

            //    QboMiscItemId = "18",
            //    //QboMiscItemName = "QboMiscItemName",

            //    QboChargeAndAllowanceItemId = "22",
            //    //QboChargeAndAllowanceItemName = "QboChargeAndAllowanceItemName",

            //    QboDefaultItemId = "19",
            //    //QboDefaultItemName = "QboDefaultItemName",

            //    QboSalesTaxItemId = "23",
            //    QboSalesTaxItemName = "QboSalesTaxItemName",

            //    QboInvoiceNumberFieldID = "1",
            //    QboInvoiceNumberFieldName = "QboInvoiceNumberFieldName",

            //    QboChnlOrderIdCustFieldId = "2",
            //    QboChnlOrderIdCustFieldName = "QboChnlOrderIdCustFieldName",

            //    Qbo2ndChnlOrderIdCustFieldId = "3",
            //    Qbo2ndChnlOrderIdCustFieldName = "Qbo2ndChnlOrderIdCustFieldName",

            //};
        }
        #endregion

        //TODO move this to QboInvoiceService
        private async Task<QuickBooksConnectionInfo> GetQboConn(InvoicePayload payload)
        {
            var service = new QuickBooksConnectionInfoService(_dbFactory);
            var success = await service.GetByPayloadAsync(payload);
            return service.Data.QuickBooksConnectionInfo;
        }

        #region qbo invoice back to erp 
        /// <summary>
        /// Write qboInvoice to ExportLog
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteQboInvoiceToExportLog(PayloadBase payload, Invoice qboInvoice)
        {
            var service = new QuickBooksExportLogService(_dbFactory);
            payload.Success = service.AddExportLog(payload, 0, "Invoice", _invoiceData.UniqueId, qboInvoice.DocNumber, qboInvoice.Id, (int)qboInvoice.status);
            payload.Messages = service.Messages;
            return payload.Success;
        }
        /// <summary>
        ///  Write docnumber to erp invoice.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="qboInvoice"></param>
        /// <returns></returns>
        protected async Task<bool> WriteDocNumberToErpInvoice(PayloadBase payload, string docNumber)
        {
            var service = new InvoiceService(_dbFactory);
            payload.Success = await service.UpdateInvoiceDocNumberAsync(_invoiceData.UniqueId, docNumber);
            payload.Messages = service.Messages;
            return payload.Success;
        }
        #endregion

        #region write erp invoice to qbo
        public async Task<bool> Export(InvoicePayload payload, string invoiceNumber)
        {
            var success = await LoadInvoiceData(payload, invoiceNumber);
            success = success && await LoadSetting(payload);
            success = success && await LoadExportLog();
            if (!success) return success;

            var mapper = new InvoiceMapper(_setting, _exportLog);
            var qboInvoice = mapper.ToInvoice(_invoiceData);
            try
            {
                var qboConn = await GetQboConn(payload);
                var qboInvoiceService = new QboInvoiceApi(qboConn, _dbFactory);
                qboInvoice = await qboInvoiceService.CreateOrUpdateInvoice(qboInvoice);

            }
            catch (Exception e)
            {
                throw new Exception(qboInvoice.ObjectToString(), e);
            }

            success = success && await WriteQboInvoiceToExportLog(payload, qboInvoice);
            success = success && await WriteDocNumberToErpInvoice(payload, qboInvoice.DocNumber);
            return success;
        }

        #endregion

        #region Query invoice from qbo

        #endregion
    }
}

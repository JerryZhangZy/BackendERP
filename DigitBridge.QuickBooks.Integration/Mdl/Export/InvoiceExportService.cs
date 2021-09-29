using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration
{
    public class InvoiceExportService
    {
        protected IDataBaseFactory _dbFactory;
        public InvoiceExportService(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async System.Threading.Tasks.Task Export(InvoicePayload payload, string invoiceNumber)
        {
            var srv = new InvoiceService(_dbFactory);
            payload.Success = await srv.GetDataAsync(payload, invoiceNumber);
            if (payload.Success)
            {
                var incoiceData = srv.Data;
                var setting = GetSetting();
                var exportLog = GetExportLog(incoiceData.InvoiceHeader.InvoiceUuid);
                var mapper = new InvoiceMapper(setting, exportLog);
                var qboInvoice = mapper.ToInvoice(srv.Data);
                var qboConn = await GetQboConn(payload);
                var qboInvoiceService = new QboInvoiceService(qboConn, _dbFactory);
                qboInvoice = await qboInvoiceService.CreateOrUpdateInvoice(qboInvoice);
                UpdateQboInvoiceToErp(qboInvoice);
            }
            else
            {
                payload.Messages = srv.Messages;
            }

        }

        //TODO move this to QboInvoiceService
        private async Task<QuickBooksConnectionInfo> GetQboConn(InvoicePayload payload)
        {
            var service = new QuickBooksConnectionInfoService(_dbFactory);
            var success = await service.GetByPayloadAsync(payload);
            return service.Data.QuickBooksConnectionInfo;
        }
        protected QboIntegrationSetting GetSetting()
        {
            return new QboIntegrationSetting()
            {
                QboDiscountItemId = "1",
                QboDiscountItemName = "QboDiscountItemName",

                QboShippingItemId = "1",
                QboShippingItemName = "QboShippingItemName",

                QboMiscItemId = "1",
                QboMiscItemName = "QboMiscItemName",

                QboChargeAndAllowanceItemId = "1",
                QboChargeAndAllowanceItemName = "QboChargeAndAllowanceItemName",

                QboDefaultItemId = "1",
                QboDefaultItemName = "QboDefaultItemName",

                QboSalesTaxItemId = "1",
                QboSalesTaxItemName = "QboSalesTaxItemName",

                QboInvoiceNumberFieldID = "1",
                QboInvoiceNumberFieldName = "QboInvoiceNumberFieldName",

                QboChnlOrderIdCustFieldId = "2",
                QboChnlOrderIdCustFieldName = "QboChnlOrderIdCustFieldName",

                Qbo2ndChnlOrderIdCustFieldId = "3",
                Qbo2ndChnlOrderIdCustFieldName = "Qbo2ndChnlOrderIdCustFieldName",

            };
        }
        protected QuickBooksExportLog GetExportLog(string invoiceUuid)
        {
            return null;
        }
        protected bool UpdateQboInvoiceToErp(Invoice qboInvoice)
        {
            return true;
        }
    }
}

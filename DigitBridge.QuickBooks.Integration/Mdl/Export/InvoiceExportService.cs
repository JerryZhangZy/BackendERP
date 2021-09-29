using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;
using Intuit.Ipp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var setting =await GetSetting(payload);
                var exportLog =await GetExportLog(incoiceData.InvoiceHeader.InvoiceUuid);
                var mapper = new InvoiceMapper(setting, exportLog);
                var qboInvoice = mapper.ToInvoice(srv.Data);
                if (MyAppSetting.IsDebug)
                {
                    await QboCloudTableUniversal.CreateDebugInfo(incoiceData.UniqueId, JsonConvert.SerializeObject(qboInvoice), "Invoice", "To", payload.MasterAccountNum, payload.ProfileNum);
                }

                var qboConn = await GetQboConn(payload);
                var qboInvoiceService = new QboInvoiceService(qboConn, _dbFactory);

                qboInvoice = await qboInvoiceService.CreateOrUpdateInvoice(qboInvoice);

                if (MyAppSetting.IsDebug)
                {
                    await QboCloudTableUniversal.CreateDebugInfo(incoiceData.UniqueId, JsonConvert.SerializeObject(qboInvoice), "Invoice", "Return", payload.MasterAccountNum, payload.ProfileNum);
                }

                UpdateQboInvoiceToErp(payload,incoiceData.UniqueId,qboInvoice);

                await srv.UpdateInvoiceDocNumberAsync(incoiceData.UniqueId, qboInvoice.DocNumber);
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
        protected async Task<QboIntegrationSetting> GetSetting(InvoicePayload payload)
        {
            var service = new QuickBooksSettingInfoService(_dbFactory);
            if (await service.GetByPayloadAsync(payload))
            {
                return service.Data.QuickBooksSettingInfo.SettingInfo;
            }
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
        protected async Task<QuickBooksExportLog> GetExportLog(string invoiceUuid)
        {
            var service = new QuickBooksExportLogService(_dbFactory);
            return (await service.QueryExportLogByLogUuidAsync(invoiceUuid)).FirstOrDefault();
        }
        protected bool UpdateQboInvoiceToErp(InvoicePayload payload,string invoiceUuid,Invoice qboInvoice)
        {
            var service = new QuickBooksExportLogService(_dbFactory);
            return service.AddExportLog(payload, 0, "Invoice", invoiceUuid, qboInvoice.DocNumber, qboInvoice.Id, (int)qboInvoice.status);
        }
    }
}

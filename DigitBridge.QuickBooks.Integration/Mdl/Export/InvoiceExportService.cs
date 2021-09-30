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
    public class InvoiceExportService:QboInvoiceApi
    {
        public InvoiceExportService(InvoicePayload payload,IDataBaseFactory dbFactory):base(payload,dbFactory)
        {
        }
        public async System.Threading.Tasks.Task Export(InvoicePayload payload, string invoiceNumber)
        {
            var srv = new InvoiceService(dbFactory);
            payload.Success = await srv.GetDataAsync(payload, invoiceNumber);
            if (payload.Success)
            {
                var incoiceData = srv.Data;
                var setting =await GetSetting(payload);
                var exportLog =await GetExportLog(incoiceData.InvoiceHeader.InvoiceUuid);
                var mapper = new InvoiceMapper(setting, exportLog);
                var qboInvoice = mapper.ToInvoice(srv.Data);
                qboInvoice = await CreateOrUpdateInvoice(qboInvoice);

                await UpdateQboInvoiceToErpAsync(payload,incoiceData.UniqueId,qboInvoice);

                await srv.UpdateInvoiceDocNumberAsync(incoiceData.UniqueId, qboInvoice.DocNumber);
            }
            else
            {
                payload.Messages = srv.Messages;
            }

        }

        protected async Task<QboIntegrationSetting> GetSetting(InvoicePayload payload)
        {
            if (await QuickBooksSettingInfoService.GetByPayloadAsync(payload))
            {
                return QuickBooksSettingInfoService.Data.QuickBooksSettingInfo.SettingInfo;
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
            return (await QuickBooksExportLogService.QueryExportLogByLogUuidAsync(invoiceUuid)).FirstOrDefault();
        }
        protected async Task<bool> UpdateQboInvoiceToErpAsync(InvoicePayload payload,string invoiceUuid,Invoice qboInvoice)
        {
            var log= new QuickBooksExportLog
            {
                DatabaseNum = payload.DatabaseNum,
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                QuickBooksExportLogUuid = Guid.NewGuid().ToString(),
                BatchNum = 0,
                LogType = "Invoice",
                LogUuid = invoiceUuid,
                DocNumber = qboInvoice.DocNumber,
                TxnId = qboInvoice.Id,
                DocStatus = (int)qboInvoice.status
            };
            return await AddExportLogAsync(log);
        }
    }
}

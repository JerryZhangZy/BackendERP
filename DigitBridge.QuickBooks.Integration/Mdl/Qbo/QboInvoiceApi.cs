using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboInvoiceApi : QboServiceBase
    {
        public QboInvoiceApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<Invoice> _invoiceQueryService;

        protected async Task<QueryService<Invoice>> GetInvoiceQueryService()
        {
            if (_invoiceQueryService == null)
                _invoiceQueryService = await GetQueryServiceAsync<Invoice>();
            return _invoiceQueryService;
        }
        public async Task<Invoice> GetInvoiceAsync(string txnId)
        {
            Invoice invoice = null;
            var queryService = await GetInvoiceQueryService();
            var list = queryService.ExecuteIdsQuery($"SELECT * FROM Invoice where id = '{txnId}'").ToList();
            if (list != null)
                invoice = list.FirstOrDefault();
            return invoice;
        }
        public async Task<Invoice> CreateOrUpdateInvoice(Invoice invoice)
        {
            if (string.IsNullOrEmpty(invoice.Id))
            {
                invoice = await AddDataAsync(invoice);
            }
            else
            {
                invoice = await UpdateDataAsync(invoice);
            }
            return invoice;
        }

        public async Task<Invoice> DeleteInvoiceAsync(string txnId)
        {
            var invoice = await GetInvoiceAsync(txnId);
            if (invoice == null)
            {
                AddError($"Data not found in qbo for id : {txnId}");
                return null;
            }
            return await DeleteDataAsync(invoice);
        }

        public async Task<Invoice> VoidInvoiceAsync(string txnId)
        {
            var invoice = await GetInvoiceAsync(txnId);
            if (invoice == null)
            {
                AddError($"Data not found in qbo for id : {txnId}");
                return null;
            }
            return await VoidDataAsync(invoice);
        }

    }
}

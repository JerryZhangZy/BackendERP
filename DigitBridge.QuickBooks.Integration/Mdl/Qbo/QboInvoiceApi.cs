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

        public async Task<Invoice> CreateOrUpdateInvoice(Invoice invoice)
        {
            if (!await InvoiceExistAsync(invoice.DocNumber))
            {
                return await AddDataAsync(invoice);
            }
            else
            {
                return await UpdateDataAsync(invoice);
            }
        }

        public async Task<Invoice> CreateInvoiceIfAbsent(Invoice invoice)
        {
            if (!await InvoiceExistAsync(invoice.DocNumber))
            {
                return await AddDataAsync(invoice);
            }
            return null;
        }

        public async Task<bool> InvoiceExistAsync(string docNumber)
        {
            var queryService = await GetInvoiceQueryService();
            return queryService.ExecuteIdsQuery($"select * from Invoice Where DocNumber = '{docNumber}'").FirstOrDefault() != null;
        }

        public async Task<Invoice> DeleteInvoiceAsync(Invoice invoice)
        {
            if (invoice != null)
                return await DeleteDataAsync(invoice);
            return null;
        }

        public async Task<List<Invoice>> DeleteInvoiceAsync(string docNumber)
        {
            var results = new List<Invoice>();
            var invoices = await GetInvoiceAsync(docNumber);
            if (invoices != null && invoices.Count > 0)
            {
                foreach (var invoice in invoices)
                    results.Add(await DeleteInvoiceAsync(invoice));
            }
            return results;
        }

        public async Task<Invoice> AvoidInvoiceAsync(Invoice invoice)
        {
                return await VoidDataAsync(invoice);
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            return await UpdateDataAsync(invoice);
        }

        /// <summary>
        /// Get qbo Invoice list by DocNumber 
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<List<Invoice>> GetInvoiceAsync(string docNumber)
        {
            var queryService = await GetInvoiceQueryService();
            return queryService.ExecuteIdsQuery($"SELECT * FROM Invoice where DocNumber = '{docNumber}'").ToList();
        }

        public async Task<List<Invoice>> VoidInvoiceAsync(string docNumber)
        {
            var results = new List<Invoice>();
            var invoices = await GetInvoiceAsync(docNumber);
            if (invoices != null && invoices.Count > 0)
            {
                foreach (var invoice in invoices)
                    results.Add(await VoidDataAsync(invoice));
            }
            return results;
        }
    }
}

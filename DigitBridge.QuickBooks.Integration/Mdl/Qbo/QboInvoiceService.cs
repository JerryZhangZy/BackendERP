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
    public class QboInvoiceService : QboServiceBase
    {
        public QboInvoiceService(QuickBooksConnectionInfo qboConnectionInfo, IDataBaseFactory databaseFactory) : base(qboConnectionInfo, databaseFactory) { }
        public async Task<Invoice> CreateOrUpdateInvoice(Invoice invoice)
        {
            if (!await InvoiceExistAsync(invoice.DocNumber))
            {
                return _dataService.Add(invoice);
            }
            else
            {
                return _dataService.Update(invoice);
            }
        }

        public async Task<Invoice> CreateInvoiceIfAbsent(Invoice invoice)
        {
            if (!await InvoiceExistAsync(invoice.DocNumber))
            {
                return _dataService.Add(invoice);
            }
            return null;
        }

        public async Task<bool> InvoiceExistAsync(string docNumber)
        {
            var queryService = new QueryService<Invoice>(_serviceContext);
            return queryService.ExecuteIdsQuery($"select * from Invoice Where DocNumber = '{docNumber}'").FirstOrDefault() != null;
        }

        public async Task<Invoice> DeleteInvoiceAsync(Invoice invoice)
        {
            if (invoice != null)
                return _dataService.Delete(invoice);
            return null;
        }

        public async Task<Invoice> DeleteInvoiceAsync(string docNumber)
        {
            var invoice = await GetInvoiceAsync(docNumber);
            if (invoice != null)
            {
                return await DeleteInvoiceAsync(invoice);
            }
            return null;
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            return _dataService.Update(invoice);
        }

        /// <summary>
        /// Get Invoice by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<Invoice> GetInvoiceAsync(string docNumber)
        {
            var queryService = new QueryService<Invoice>(_serviceContext);
            return queryService.ExecuteIdsQuery($"SELECT * FROM Invoice where DocNumber = '{docNumber}'").FirstOrDefault();
        }
    }
}

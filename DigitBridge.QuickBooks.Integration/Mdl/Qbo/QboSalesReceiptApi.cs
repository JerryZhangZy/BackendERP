using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboSalesReceiptApi : QboServiceBase
    {
        public QboSalesReceiptApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }
        public async Task<bool> SalesReceiptExistAsync(string docNumber)
        {
            var queryService = await GetQueryServiceAsync<SalesReceipt>();
            return queryService.ExecuteIdsQuery($"select * from SalesReceipt Where DocNumber = '{docNumber}'").FirstOrDefault() != null;
        }

        public async Task<SalesReceipt> CreateSalesReceiptIfAbsentAsync(SalesReceipt salesReceipt)
        {
            if (!await SalesReceiptExistAsync(salesReceipt.DocNumber))
            {
                return await AddDataAsync(salesReceipt);
            }
            return null;
        }

        public async Task<SalesReceipt> UpdateSalesReceiptIfLatestAsync(SalesReceipt salesReceipt)
        {
            return await UpdateDataAsync(salesReceipt);
        }
        /// <summary>
        /// Get Sales Receipt by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<SalesReceipt> GetSalesReceiptAsync(string docNumber)
        {
            var queryService = await GetQueryServiceAsync<SalesReceipt>();
            return queryService.ExecuteIdsQuery($"SELECT * FROM SalesReceipt where DocNumber = '{docNumber}'").FirstOrDefault();
        }
    }
}

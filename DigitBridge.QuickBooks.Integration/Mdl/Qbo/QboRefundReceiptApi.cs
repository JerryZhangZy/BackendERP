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
    public class QboRefundReceiptApi:QboServiceBase
    {
        public QboRefundReceiptApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<RefundReceipt> _refundReceiptQueryService;

        protected async Task<QueryService<RefundReceipt>> GetRefundReceiptQueryService()
        {
            if (_refundReceiptQueryService == null)
                _refundReceiptQueryService = await GetQueryServiceAsync<RefundReceipt>();
            return _refundReceiptQueryService;
        }

        public async Task<RefundReceipt> CreateOrUpdateRefundReceipt(RefundReceipt refundReceipt)
        {
            if (!await RefundReceiptExistAsync(refundReceipt.DocNumber))
            {
                return await AddDataAsync(refundReceipt);
            }
            else
            {
                return await UpdateDataAsync(refundReceipt);
            }
        }

        public async Task<RefundReceipt> CreateRefundReceiptIfAbsent(RefundReceipt refundReceipt)
        {
            if (!await RefundReceiptExistAsync(refundReceipt.DocNumber))
            {
                return await AddDataAsync(refundReceipt);
            }
            return null;
        }

        public async Task<bool> RefundReceiptExistAsync(string docNumber)
        {
            var queryService = await GetRefundReceiptQueryService();
            return queryService.ExecuteIdsQuery($"select * from RefundReceipt Where DocNumber = '{docNumber}'").FirstOrDefault() != null;
        }

        public async Task<RefundReceipt> DeleteRefundReceiptAsync(RefundReceipt refundReceipt)
        {
            if (refundReceipt != null)
                return await DeleteDataAsync(refundReceipt);
            return null;
        }

        public async Task<RefundReceipt> DeleteRefundReceiptAsync(string docNumber)
        {
            var refundReceipt = await GetRefundReceiptAsync(docNumber);
            if (refundReceipt != null)
            {
                return await DeleteRefundReceiptAsync(refundReceipt);
            }
            return null;
        }

        public async Task<RefundReceipt> UpdateRefundReceiptAsync(RefundReceipt refundReceipt)
        {
            return await UpdateDataAsync(refundReceipt);
        }

        /// <summary>
        /// Get RefundReceipt by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<RefundReceipt> GetRefundReceiptAsync(string docNumber)
        {
            var queryService = await GetRefundReceiptQueryService();
            return queryService.ExecuteIdsQuery($"SELECT * FROM RefundReceipt where DocNumber = '{docNumber}'").FirstOrDefault();
        }
    }
}

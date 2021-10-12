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
    public class QboRefundApi : QboServiceBase
    {
        public QboRefundApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<RefundReceipt> _RefundQueryService;

        protected async Task<QueryService<RefundReceipt>> GetRefundQueryService()
        {
            if (_RefundQueryService == null)
                _RefundQueryService = await GetQueryServiceAsync<RefundReceipt>();
            return _RefundQueryService;
        }

        public async Task<RefundReceipt> GetRefundAsync(string txnId)
        {
            RefundReceipt refund = null;
            var queryService = await GetRefundQueryService();
            var list = queryService.ExecuteIdsQuery($"SELECT * FROM RefundReceipt where id = '{txnId}'").ToList();
            if (list != null)
                refund = list.FirstOrDefault();
            if (refund == null)
            {
                AddError($"Quick books refund not found for qbo refund id : {txnId}");
            }
            return refund;
        }
        public async Task<RefundReceipt> CreateOrUpdateRefund(RefundReceipt refund)
        {
            if (string.IsNullOrEmpty(refund.Id))
            {
                refund = await AddDataAsync(refund);
            }
            else
            {
                refund = await UpdateDataAsync(refund);
            }
            return refund;
        }

        public async Task<RefundReceipt> DeleteRefundAsync(string txnId)
        {
            var Refund = await GetRefundAsync(txnId);
            if (Refund == null)
            {
                return null;
            }
            return await DeleteDataAsync(Refund);
        }

        public async Task<RefundReceipt> VoidRefundAsync(string txnId)
        {
            var Refund = await GetRefundAsync(txnId);
            if (Refund == null)
            {
                return null;
            }
            return await VoidDataAsync(Refund);
        }
    }
}

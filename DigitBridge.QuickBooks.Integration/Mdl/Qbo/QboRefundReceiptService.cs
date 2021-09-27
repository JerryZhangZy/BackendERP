using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboRefundReceiptService:QboServiceBase
    {
        public async Task<RefundReceipt> CreateOrUpdateRefundReceipt(RefundReceipt refundReceipt)
        {
            if (!await RefundReceiptExistAsync(refundReceipt.DocNumber))
            {
                return _dataService.Add(refundReceipt);
            }
            else
            {
                return _dataService.Update(refundReceipt);
            }
        }

        public async Task<RefundReceipt> CreateRefundReceiptIfAbsent(RefundReceipt refundReceipt)
        {
            if (!await RefundReceiptExistAsync(refundReceipt.DocNumber))
            {
                return _dataService.Add(refundReceipt);
            }
            return null;
        }

        public async Task<bool> RefundReceiptExistAsync(string docNumber)
        {
            var queryService = new QueryService<RefundReceipt>(_serviceContext);
            return queryService.ExecuteIdsQuery($"select * from RefundReceipt Where DocNumber = '{docNumber}'").FirstOrDefault() != null;
        }

        public async Task<RefundReceipt> DeleteRefundReceiptAsync(RefundReceipt refundReceipt)
        {
            if (refundReceipt != null)
                return _dataService.Delete(refundReceipt);
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
            return _dataService.Update(refundReceipt);
        }

        /// <summary>
        /// Get RefundReceipt by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<RefundReceipt> GetRefundReceiptAsync(string docNumber)
        {
            var queryService = new QueryService<RefundReceipt>(_serviceContext);
            return queryService.ExecuteIdsQuery($"SELECT * FROM RefundReceipt where DocNumber = '{docNumber}'").FirstOrDefault();
        }
    }
}

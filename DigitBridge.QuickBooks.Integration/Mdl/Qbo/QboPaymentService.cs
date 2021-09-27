using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboPaymentService:QboServiceBase
    {
        public async Task<Payment> CreateOrUpdatePayment(Payment payment)
        {
            if (!await PaymentExistAsync(payment.Id))
            {
                return _dataService.Add(payment);
            }
            else
            {
                return _dataService.Update(payment);
            }
        }

        public async Task<Payment> CreatePaymentIfAbsent(Payment payment)
        {
            if (!await PaymentExistAsync(payment.Id))
            {
                return _dataService.Add(payment);
            }
            return null;
        }

        public async Task<bool> PaymentExistAsync(string id)
        {
            var queryService = new QueryService<Payment>(_serviceContext);
            return queryService.ExecuteIdsQuery($"select * from Payment Where Id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<Payment> DeletePaymentAsync(Payment payment)
        {
            if (payment != null)
                return _dataService.Delete(payment);
            return null;
        }

        public async Task<Payment> DeletePaymentAsync(string id)
        {
            var payment = await GetPaymentAsync(id);
            if (payment != null)
            {
                return await DeletePaymentAsync(payment);
            }
            return null;
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            return _dataService.Update(payment);
        }

        /// <summary>
        /// Get Payment by Id( DigibridgeOrderId )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Payment> GetPaymentAsync(string id)
        {
            var queryService = new QueryService<Payment>(_serviceContext);
            return queryService.ExecuteIdsQuery($"SELECT * FROM Payment where Id = '{id}'").FirstOrDefault();
        }
    }
}

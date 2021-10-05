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
    public class QboPaymentApi:QboServiceBase
    {
        public QboPaymentApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<Payment> _paymentQueryService;

        protected async Task<QueryService<Payment>> GetPaymentQueryService()
        {
            if (_paymentQueryService == null)
                _paymentQueryService = await GetQueryServiceAsync<Payment>();
            return _paymentQueryService;
        }

        public async Task<Payment> CreateOrUpdatePayment(Payment payment)
        {
            if (!await PaymentExistAsync(payment.Id))
            {
                return await AddDataAsync(payment);
            }
            else
            {
                return await UpdateDataAsync(payment);
            }
        }

        public async Task<Payment> CreatePaymentIfAbsent(Payment payment)
        {
            if (!await PaymentExistAsync(payment.Id))
            {
                return await AddDataAsync(payment);
            }
            return null;
        }

        public async Task<bool> PaymentExistAsync(string id)
        {
            var queryService = await GetPaymentQueryService();
            return queryService.ExecuteIdsQuery($"select * from Payment Where Id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<Payment> DeletePaymentAsync(Payment payment)
        {
            if (payment != null)
                return await DeleteDataAsync(payment);
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
            return await UpdateDataAsync(payment);
        }

        /// <summary>
        /// Get Payment by Id( DigibridgeOrderId )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Payment> GetPaymentAsync(string id)
        {
            var queryService = await GetPaymentQueryService();
            return queryService.ExecuteIdsQuery($"SELECT * FROM Payment where Id = '{id}'").FirstOrDefault();
        }
    }
}

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
    public class QboPaymentApi : QboServiceBase
    {
        public QboPaymentApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<Payment> _paymentQueryService;

        protected async Task<QueryService<Payment>> GetPaymentQueryService()
        {
            if (_paymentQueryService == null)
                _paymentQueryService = await GetQueryServiceAsync<Payment>();
            return _paymentQueryService;
        }

        public async Task<Payment> GetPaymentAsync(string txnId)
        {
            Payment payment = null;
            var queryService = await GetPaymentQueryService();
            var list = queryService.ExecuteIdsQuery($"SELECT * FROM Payment where id = '{txnId}'").ToList();
            if (list != null)
                payment = list.FirstOrDefault();
            if (payment == null)
            {
                AddError($"Quick books payment not found for qbo payment id : {txnId}");
            }
            return payment;
        }
        public async Task<Payment> CreateOrUpdatePayment(Payment payment)
        {
            if (string.IsNullOrEmpty(payment.Id))
            {
                payment = await AddDataAsync(payment);
            }
            else
            {
                payment = await UpdateDataAsync(payment);
            }
            return payment;
        }

        public async Task<Payment> DeletePaymentAsync(string txnId)
        {
            var Payment = await GetPaymentAsync(txnId);
            if (Payment == null)
            { 
                return null;
            }
            return await DeleteDataAsync(Payment);
        }

        public async Task<Payment> VoidPaymentAsync(string txnId)
        {
            var Payment = await GetPaymentAsync(txnId);
            if (Payment == null)
            { 
                return null;
            }
            return await VoidDataAsync(Payment);
        }
    }
}

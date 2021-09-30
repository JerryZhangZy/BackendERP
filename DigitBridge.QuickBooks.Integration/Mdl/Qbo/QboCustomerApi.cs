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
    public class QboCustomerApi : QboServiceBase
    {

        public QboCustomerApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customerService = await GetQueryServiceAsync<Customer>();
            return customerService.ExecuteIdsQuery("SELECT * FROM Customer").ToList();
        }

        public async Task<Customer> CreateOrUpdateCustomer(Customer customer)
        {
            if (!await CustomerExistAsync(customer.Id))
            {
                //return await AddDataAsync(customer);
                return await AddDataAsync(customer);
            }
            else
            {
                return await UpdateDataAsync(customer);
            }
        }

        public async Task<Customer> CreateCustomerIfAbsent(Customer customer)
        {
            if (!await CustomerExistAsync(customer.Id))
            {
                return await AddDataAsync(customer);
            }
            return null;
        }

        public async Task<bool> CustomerExistAsync(string id)
        {
            var queryService = await GetQueryServiceAsync<Customer>();
            return queryService.ExecuteIdsQuery($"select * from Customer Where id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            if (customer != null)
                return await DeleteDataAsync(customer);
            return null;
        }

        public async Task<Customer> DeleteCustomerAsync(string id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer != null)
            {
                return await DeleteCustomerAsync(customer);
            }
            return null;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            return await UpdateDataAsync(customer);
        }

        /// <summary>
        /// Check if Channel Customer Id from Database can be found in Quickbooks Online
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            var customerService = await GetQueryServiceAsync<Customer>();
            return customerService.ExecuteIdsQuery("SELECT * FROM Customer where id = '" + id + "'").FirstOrDefault();
        }
    }
}

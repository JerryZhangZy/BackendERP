using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboCustomerService : QboServiceBase
    {

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customerService = new QueryService<Customer>(_serviceContext);
            return customerService.ExecuteIdsQuery("SELECT * FROM Customer").ToList();
        }

        public async Task<Customer> CreateOrUpdateCustomer(Customer customer)
        {
            if (!await CustomerExistAsync(customer.Id))
            {
                return _dataService.Add(customer);
            }
            else
            {
                return _dataService.Update(customer);
            }
        }

        public async Task<Customer> CreateCustomerIfAbsent(Customer customer)
        {
            if (!await CustomerExistAsync(customer.Id))
            {
                return _dataService.Add(customer);
            }
            return null;
        }

        public async Task<bool> CustomerExistAsync(string id)
        {
            var queryService = new QueryService<Customer>(_serviceContext);
            return queryService.ExecuteIdsQuery($"select * from Customer Where id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            if (customer != null)
                return _dataService.Delete(customer);
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
            return _dataService.Update(customer);
        }

        /// <summary>
        /// Check if Channel Customer Id from Database can be found in Quickbooks Online
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            var customerService = new QueryService<Customer>(_serviceContext);
            return customerService.ExecuteIdsQuery("SELECT * FROM Customer where id = '" + id + "'").FirstOrDefault();
        }
    }
}

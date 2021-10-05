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
    public class QboAccountApi : QboServiceBase
    {

        public QboAccountApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<Account> _accountQueryService;

        protected async Task<QueryService<Account>> GetAccountQueryService()
        {
            if (_accountQueryService == null)
                _accountQueryService = await GetQueryServiceAsync<Account>();
            return _accountQueryService;
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var accountService = await GetAccountQueryService();
            return accountService.ExecuteIdsQuery("SELECT * FROM Account").ToList();
        }

        public async Task<Account> CreateOrUpdateAccount(Account account)
        {
            if (!await AccountExistAsync(account.Id))
            {
                return await AddDataAsync(account);
            }
            else
            {
                return await UpdateDataAsync(account);
            }
        }

        public async Task<Account> CreateAccountIfAbsent(Account account)
        {
            if (!await AccountExistAsync(account.Id))
            {
                return await AddDataAsync(account);
            }
            return null;
        }

        public async Task<bool> AccountExistAsync(string id)
        {
            var queryService = await GetAccountQueryService();
            return queryService.ExecuteIdsQuery($"select * from Account Where id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<Account> DeleteAccountAsync(Account account)
        {
            if (account != null)
                return await DeleteDataAsync(account);
            return null;
        }

        public async Task<Account> DeleteAccountAsync(string id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account != null)
            {
                return await DeleteAccountAsync(account);
            }
            return null;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            return await UpdateDataAsync(account);
        }

        /// <summary>
        /// Check if Channel Account Id from Database can be found in Quickbooks Online
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountByIdAsync(string id)
        {
            var accountService = await GetAccountQueryService();
            return accountService.ExecuteIdsQuery("SELECT * FROM Account where id = '" + id + "'").FirstOrDefault();
        }
    }
}

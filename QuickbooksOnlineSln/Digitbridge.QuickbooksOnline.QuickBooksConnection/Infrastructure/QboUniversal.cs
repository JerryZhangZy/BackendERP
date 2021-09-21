using Digitbridge.QuickbooksOnline.QuickBooksConnection.Infrastructure;
using Digitbridge.QuickbooksOnline.QuickBooksConnection.Model;
using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Configuration;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace Digitbridge.QuickbooksOnline.QuickBooksConnection
{
    public class QboUniversal
    {
        private OAuth2Client _auth2Client;
        private DataService _dataService;
        private ServiceContext _serviceContext;
        private OAuth2RequestValidator _oauthValidator;
        private QboConnectionConfig _qboConnectionConfig;
        public QboConnectionInfo _qboConnectionInfo;
        public QboConnectionTokenStatus _qboConnectionTokenStatus;

        public QboUniversal() { }
        private async Task<QboUniversal> InitializeAsync(
            QboConnectionInfo qboConnectionInfo, 
            QboConnectionConfig qboConnectionConfig) 
        {
            try
            {
                _qboConnectionConfig = qboConnectionConfig;
                _qboConnectionInfo = qboConnectionInfo;
                await ConnectToDataService();
                await HandleConnectivity();
                return this;
            }
            catch (Exception ex)
            {
                string additionalMsg = "Qbo Universal Initialize Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        public static Task<QboUniversal> CreateAsync(
            QboConnectionInfo qboConnectionInfo, QboConnectionConfig qboConnectionConfig)
        {
            try
            {
                var newInstance = new QboUniversal();
                return newInstance.InitializeAsync(qboConnectionInfo, qboConnectionConfig);
            }
            catch (Exception ex)
            {
                string additionalMsg = "Qbo Universal CreateAsync Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }

        }

        private async System.Threading.Tasks.Task ConnectToDataService()
        {
            try
            {
                _auth2Client = new OAuth2Client(_qboConnectionInfo.ClientId, _qboConnectionInfo.ClientSecret
                    , _qboConnectionConfig.RedirectUrl, _qboConnectionConfig.Environment);
                _oauthValidator = new OAuth2RequestValidator(_qboConnectionInfo.AccessToken);
                _serviceContext = new ServiceContext(_qboConnectionInfo.RealmId, IntuitServicesType.QBO, _oauthValidator);
                _serviceContext.IppConfiguration.MinorVersion.Qbo = _qboConnectionConfig.MinorVersion;
                _serviceContext.IppConfiguration.BaseUrl.Qbo = _qboConnectionConfig.BaseUrl;
                _dataService = new DataService(_serviceContext);
                _qboConnectionTokenStatus = new QboConnectionTokenStatus();
            }
            catch (Exception ex)
            { 
                string additionalMsg = "Qbo Connection GetServiceContext Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }

        private async System.Threading.Tasks.Task HandleConnectivity()
        {
            try
            {
                // Check if the refresh token is too old, offical set life = 100 days
                if ( (DateTime.Now.ToUniversalTime() - _qboConnectionInfo.LastRefreshTokUpdate).TotalDays > 80 )
                {
                    await UpdateRefreshToken();
                } 
                // Check if the access token is too old, offical set life = 1 hr
                if( (DateTime.Now.ToUniversalTime() - _qboConnectionInfo.LastAccessTokUpdate).TotalHours >= 0.75 )
                {
                    await RefreshAccessToken();
                }
                // Use a simple API call to see if the access token is valid ( officially recommended method )
                CompanyInfo companyInfo = await GetCompanyInfo();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == QboUniversalConsts.Connection401Error)
                {
                    await RefreshAccessToken();
                }
                else
                {
                    string additionalMsg = "Qbo Handle Connectivity Error" + CommonConst.NewLine;
                    throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
                }
            }
        }
        /// <summary>
        /// Use the Authorization Code to get the refresh token which will expire after 100 days after created
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task UpdateRefreshToken()
        {
            try
            {
                TokenResponse tokenResp = await _auth2Client.GetBearerTokenAsync(_qboConnectionInfo.AuthCode);
                _qboConnectionInfo.RefreshToken = tokenResp.RefreshToken;
                await RefreshAccessToken(true);
            }
            catch (Exception ex)
            {
                _qboConnectionTokenStatus.RefreshTokenStatus = ConnectionTokenStatus.UpdatedWithError;
                string additionalMsg = "Qbo Connection Update Refresh Token Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }
        private async System.Threading.Tasks.Task RefreshAccessToken(bool runAfterRefreshUpdated = false)
        {
            try
            {
                TokenResponse tokenResp = await _auth2Client.RefreshTokenAsync(_qboConnectionInfo.RefreshToken);
                if (tokenResp.AccessToken != null)
                {
                    _qboConnectionInfo.AccessToken = tokenResp.AccessToken;
                    await ConnectToDataService(); // reconnect to data service after token refreshing
                    // Update time to be insert into DB table (UTC)
                    _qboConnectionInfo.LastAccessTokUpdate = DateTime.Now.ToUniversalTime();
                    // Flag tokens as updated
                    _qboConnectionTokenStatus.AccessTokenStatus = ConnectionTokenStatus.Updated;
                    
                    if (!_qboConnectionInfo.RefreshToken.Equals(tokenResp.RefreshToken))
                    {
                        _qboConnectionInfo.RefreshToken = tokenResp.RefreshToken;
                        // Update time to be inserted into DB table(UTC)
                        _qboConnectionInfo.LastRefreshTokUpdate = DateTime.Now.ToUniversalTime();
                        // Flag tokens as updated
                        _qboConnectionTokenStatus.RefreshTokenStatus = ConnectionTokenStatus.Updated;
                    }
                }
                else if(runAfterRefreshUpdated == false)
                {
                    await UpdateRefreshToken();
                }else
                {
                    throw new Exception("Refresh Access Token use the Updated Refresh Token Failed, " +
                        $"Check Auth Code For User: {_qboConnectionInfo.MasterAccountNum} {_qboConnectionInfo.ProfileNum}, " +
                        $"Error Msg: {tokenResp.Error}" );
                }
            }
            catch (Exception ex)
            {
                _qboConnectionTokenStatus.AccessTokenStatus = ConnectionTokenStatus.UpdatedWithError;
                string additionalMsg = "Qbo Connection Refresh Access Token Error" + CommonConst.NewLine;
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, additionalMsg);
            }
        }
        public async Task<Invoice> CreateInvoiceIfAbsent(Invoice invoice) 
        {
            Invoice invoiceCreated = null;
            try
            {
                if( !await InvoiceExist(invoice.DocNumber) )
                {
                    invoiceCreated = _dataService.Add<Invoice>(invoice);
                }
                
                return invoiceCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            
        }

        /// <summary>
        /// Return ( isTransactionExist, isTransactioniUpToDate )
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<(bool, bool)> IsSalesTransactionExistAndUpToDate(SalesTransaction transaction)
        {
            try
            {
                QueryService<SalesTransaction> queryService = new QueryService<SalesTransaction>(_serviceContext);
                string qboQureyStr = $"select * from {transaction.GetType().Name} Where DocNumber = '{transaction.DocNumber}'";
                SalesTransaction latestTransaction = queryService.ExecuteIdsQuery(qboQureyStr).FirstOrDefault();

                return (latestTransaction != null, 
                    latestTransaction != null && latestTransaction.SyncToken.Equals( transaction.SyncToken )); 
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<bool> InvoiceExist(string docNumber)
        {
            Invoice invoice;
            try
            {
                QueryService<Invoice> queryService = new QueryService<Invoice>(_serviceContext);
                invoice = queryService.ExecuteIdsQuery($"select * from Invoice Where DocNumber = '{docNumber}'").FirstOrDefault();

                return ! (invoice == null);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<bool> SalesReceiptExist(string docNumber)
        {
            SalesReceipt salesReceipt;
            try
            {
                QueryService<SalesReceipt> queryService = new QueryService<SalesReceipt>(_serviceContext);
                salesReceipt = queryService.ExecuteIdsQuery($"select * from SalesReceipt Where DocNumber = '{docNumber}'").FirstOrDefault();

                return !(salesReceipt == null);
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        public async Task<Invoice> UpdateInvoice(Invoice invoice)
        {
            Invoice invoiceUpdated = null;
            try
            {
                invoiceUpdated = _dataService.Update<Invoice>(invoice);
                
                return invoiceUpdated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<SalesReceipt> CreateSalesReceiptIfAbsent(SalesReceipt salesReceipt)
        {
            SalesReceipt salesReceiptCreated = null;
            try
            {
                if (!await SalesReceiptExist(salesReceipt.DocNumber))
                {
                    salesReceiptCreated = _dataService.Add<SalesReceipt>(salesReceipt);
                }

                return salesReceiptCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<SalesReceipt> UpdateSalesReceiptIfLatest(SalesReceipt salesReceipt)
        {
            SalesReceipt salesReceiptUpdated = null;
            try
            {
                salesReceiptUpdated = _dataService.Update<SalesReceipt>(salesReceipt);
                
                return salesReceiptUpdated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<CompanyInfo> GetCompanyInfo()
        {
            CompanyInfo companyInfo;
            try
            {
                QueryService<CompanyInfo> queryService = new QueryService<CompanyInfo>(_serviceContext);
                companyInfo = queryService.ExecuteIdsQuery("select * from CompanyInfo").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return companyInfo;
        }
        /// <summary>
        /// Get Invoice by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<Invoice> GetInvoice(string docNumber)
        {
            Invoice invoice;
            try
            {
                QueryService<Invoice> queryService = new QueryService<Invoice>(_serviceContext);
                invoice = queryService.ExecuteIdsQuery($"SELECT * FROM Invoice where DocNumber = '{docNumber}'").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return invoice;
        }
        /// <summary>
        /// Get Sales Receipt by DocNumber( DigibridgeOrderId )
        /// </summary>
        /// <param name="docNumber"></param>
        /// <returns></returns>
        public async Task<SalesReceipt> GetSalesReceipt(string docNumber)
        {
            SalesReceipt salesReceipt;
            try
            {
                QueryService<SalesReceipt> queryService = new QueryService<SalesReceipt>(_serviceContext);
                salesReceipt = queryService.ExecuteIdsQuery($"SELECT * FROM SalesReceipt where DocNumber = '{docNumber}'").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return salesReceipt;
        }
        /// <summary>
        /// Get Item By Qbo Item Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Item> GetItemByName(String name)
        {
            Item item;
            try
            {
                QueryService<Item> itemService = new QueryService<Item>(_serviceContext);
                item = itemService.ExecuteIdsQuery("SELECT * FROM Item where Name = '" + name + "'").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return item;
        }

        public async Task<Item> CreateDefaultInventoryItem(String name)
        {
            Item item = new Item();
            try
            {
                List<Account> accounts = await GetAccounts();
                List<Account> incomeAccounts = accounts.Where(t => t.AccountType == AccountTypeEnum.Income).ToList();
                List<Account> expenseAccounts = accounts.Where(t => t.AccountType == AccountTypeEnum.CostofGoodsSold).ToList();
                List<Account> assetAccounts = accounts.Where(t => t.AccountType == AccountTypeEnum.OtherCurrentAsset).ToList();

                Account DefaultIncomeAccount = incomeAccounts.Where(
                    n => n.Name.Equals(QboUniversalConsts.DefaultIncomeAccountName)).FirstOrDefault();

                Account incomeAccount =
                    DefaultIncomeAccount != null ? DefaultIncomeAccount :
                    incomeAccounts.First();

                Account DefaultExpenseAccount = expenseAccounts.Where(
                    n => n.Name.Equals(QboUniversalConsts.DefaultExpenseAccountName)).FirstOrDefault();

                Account expenseAccount =
                    DefaultExpenseAccount != null ? DefaultExpenseAccount :
                    expenseAccounts.First();

                Account DefaultAssetAccount = assetAccounts.Where(
                    n => n.Name.Equals(QboUniversalConsts.DefaultAssetAccountName)).FirstOrDefault();

                Account assetAccount =
                    DefaultAssetAccount != null ? DefaultAssetAccount :
                    assetAccounts.First();

                if (incomeAccount == null || expenseAccount == null || assetAccount == null)
                {
                    return null;
                }

                item.Name = name;
                item.Description = QboUniversalConsts.DefaultInventoryItemDescription;
                item.Type = ItemTypeEnum.Inventory;
                item.TypeSpecified = true;

                item.Active = true;
                item.ActiveSpecified = true;

                item.Taxable = false;
                item.TaxableSpecified = true;
                // In order to make item "nontaxable"
                item.TaxClassificationRef = new ReferenceType()
                {
                    name = QboUniversalConsts.DefaultInventoryItemTaxClassificationRefName,
                    Value = QboUniversalConsts.DefaultInventoryItemTaxClassificationRefValue
                };

                item.PurchaseTaxIncluded = true;
                item.PurchaseTaxIncludedSpecified = true;

                item.UnitPrice = new Decimal(100.00);
                item.UnitPriceSpecified = true;

                item.TrackQtyOnHand = true;
                item.TrackQtyOnHandSpecified = true;

                item.QtyOnHand = new decimal(9999999999.00);
                item.QtyOnHandSpecified = true;

                item.InvStartDate = new DateTime(1900, 01, 01);
                item.InvStartDateSpecified = true;

                item.IncomeAccountRef = new ReferenceType()
                {
                    name = incomeAccount.Name,
                    Value = incomeAccount.Id
                };

                item.ExpenseAccountRef = new ReferenceType()
                {
                    name = expenseAccount.Name,
                    Value = expenseAccount.Id
                };

                item.AssetAccountRef = new ReferenceType()
                {
                    name = assetAccount.Name,
                    Value = assetAccount.Id
                };

                Item itemCreated = _dataService.Add<Item>(item);

                return itemCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<Item> CreateDefaultNonInventoryItem(String name, String taxAccName = null, int taxAccId = -1)
        {
            Item item = new Item();
            try
            {
                List<Account> accounts = await GetAccounts();
                List<Account> candidateAccount = new List<Account>();
                Account targetAccount = null;

                if( name.ToLower().Contains("discount") )
                {
                    candidateAccount = 
                        accounts.Where(
                            t => t.AccountType == AccountTypeEnum.Income && 
                            t.AccountSubType.ToLower().Contains("discount")
                        ).ToList();

                    Account DefaultDiscountAccount = candidateAccount.Where(
                    n => n.Name.Equals(QboUniversalConsts.DefaultDiscountAccountName)).FirstOrDefault();

                    targetAccount = DefaultDiscountAccount != null ? 
                        DefaultDiscountAccount : candidateAccount.First();
                }
                else if( name.ToLower().Contains("ship"))
                {
                    candidateAccount =
                        accounts.Where(
                            t => t.AccountType == AccountTypeEnum.Income &&
                            t.AccountSubType.ToLower().Contains("service")
                        ).ToList();
                    // If User dosen't have 'Shipping' option turn on then the "Shipping Income" Account won't be created automatically.
                    Account DefaultShippingAccount = candidateAccount.Where(
                    n => n.Name.Equals(QboUniversalConsts.DefaultShippingAccountName)).FirstOrDefault();

                    targetAccount = DefaultShippingAccount != null ?
                        DefaultShippingAccount : candidateAccount.First();
                }
                else if(name.ToLower().Contains("tax"))
                {
                    if (taxAccId == 0)
                    {
                        // Create Manual Tax Adjustment Account
                        Account newAccount = new Account();
                        newAccount.Name = taxAccName;
                        newAccount.AccountType = AccountTypeEnum.OtherCurrentLiability;
                        newAccount.AccountTypeSpecified = true;

                        targetAccount = _dataService.Add<Account>(newAccount);
                    }
                    else
                    {
                        targetAccount = accounts.Where( t => t.Id.Equals( taxAccId.ToString() ) ).First();
                    }
                }else
                {
                    return null;
                }

                if(targetAccount == null)
                {
                    return null;
                }

                item.Name = name;
                item.Description = QboUniversalConsts.DefaultNonInventoryItemDescription;
                item.Type = ItemTypeEnum.NonInventory;
                item.TypeSpecified = true;

                item.Active = true;
                item.ActiveSpecified = true;

                item.Taxable = false;
                item.TaxableSpecified = true;
                // In order to make item "nontaxable"
                item.TaxClassificationRef = new ReferenceType()
                {
                    name = QboUniversalConsts.DefaultInventoryItemTaxClassificationRefName,
                    Value = QboUniversalConsts.DefaultInventoryItemTaxClassificationRefValue
                };

                item.IncomeAccountRef = new ReferenceType()
                {
                    name = targetAccount.Name,
                    Value = targetAccount.Id
                };

                Item itemCreated = _dataService.Add<Item>(item);

                return itemCreated;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<List<Item>> GetItems()
        {
            List<Item> items;
            try
            {
                QueryService<Item> itemService = new QueryService<Item>(_serviceContext);
                items = itemService.ExecuteIdsQuery("SELECT * FROM Item").ToList();
                return items;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<List<Account>> GetAccounts()
        {
            List<Account> accounts;
            try
            {
                QueryService<Account> accountService = new QueryService<Account>(_serviceContext);
                accounts = accountService.ExecuteIdsQuery("SELECT * FROM Account where Active = true").ToList();
                return accounts;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers;
            try
            {
                QueryService<Customer> customerService = new QueryService<Customer>(_serviceContext);
                customers = customerService.ExecuteIdsQuery("SELECT * FROM Customer").ToList();
                return customers;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Check if Channel Customer Id from Database can be found in Quickbooks Online
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerById(String id)
        {
            Customer customer;
            try
            {
                QueryService<Customer> customerService = new QueryService<Customer>(_serviceContext);
                customer = customerService.ExecuteIdsQuery("SELECT * FROM Customer where id = '" + id + "'").FirstOrDefault();
                // if the costomer 
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return customer;
        }
    }
}

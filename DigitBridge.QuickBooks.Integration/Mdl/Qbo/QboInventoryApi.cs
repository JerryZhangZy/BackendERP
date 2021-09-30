using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboInventoryApi : QboServiceBase
    {
        public QboInventoryApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }
        /// <summary>
        /// Get Item By Qbo Item Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Item> GetItemByNameAsync(string name)
        {
            var itemService = await GetQueryServiceAsync<Item>();
            return itemService.ExecuteIdsQuery("SELECT * FROM Item where Name = '" + name + "'").FirstOrDefault();
        }

        public async Task<Item> CreateDefaultInventoryItemAsync(string name)
        {
            var item = new Item();
            List<Account> accounts = await GetAccountsAsync();
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

            item.UnitPrice = new decimal(100.00);
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

            Item itemCreated = await AddDataAsync(item);

            return itemCreated;
        }

        public async Task<Item> CreateDefaultNonInventoryItemAsync(string name, string taxAccName = null, int taxAccId = -1)
        {
            Item item = new Item();
            List<Account> accounts = await GetAccountsAsync();
            List<Account> candidateAccount = new List<Account>();
            Account targetAccount = null;

            if (name.ToLower().Contains("discount"))
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
            else if (name.ToLower().Contains("ship"))
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
            else if (name.ToLower().Contains("tax"))
            {
                if (taxAccId == 0)
                {
                    // Create Manual Tax Adjustment Account
                    Account newAccount = new Account();
                    newAccount.Name = taxAccName;
                    newAccount.AccountType = AccountTypeEnum.OtherCurrentLiability;
                    newAccount.AccountTypeSpecified = true;

                    targetAccount = await AddDataAsync(newAccount);
                }
                else
                {
                    targetAccount = accounts.Where(t => t.Id.Equals(taxAccId.ToString())).First();
                }
            }
            else
            {
                return null;
            }

            if (targetAccount == null)
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

            Item itemCreated = await AddDataAsync(item);

            return itemCreated;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
                var itemService = await GetQueryServiceAsync<Item>();
                return itemService.ExecuteIdsQuery("SELECT * FROM Item").ToList();
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            var accountService = await GetQueryServiceAsync<Account>();
                return accountService.ExecuteIdsQuery("SELECT * FROM Account where Active = true").ToList();
        }
    }
}

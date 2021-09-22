using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Connection.Infrastructure
{
    public class QboUniversalConsts
    {
        public static readonly string Connection401Error = "Unauthorized-401";
        public static readonly string DefaultIncomeAccountName = "Sales of Product Income";
        public static readonly string DefaultExpenseAccountName = "Cost of Goods Sold";
        public static readonly string DefaultAssetAccountName = "Inventory Asset";
        public static readonly string DefaultDiscountAccountName = "Discounts given";
        public static readonly string DefaultShippingAccountName = "Shipping Income";
        public static readonly string DefaultInventoryItemDescription = "Default Inventory Item created by Digitbridge, "
            + "Income, Expense and Asset Account are configurable.";
        public static readonly string DefaultNonInventoryItemDescription = "Default Non-Inventory Item created by Digitbridge, "
            + "Income account is configurable.";
        /// <summary>
        /// NonTaxable Class Value
        /// </summary>
        public static readonly string DefaultInventoryItemTaxClassificationRefValue = "EUC-99990101-V1-00020000";
        /// <summary>
        /// NonTaxable Class Name
        /// </summary>
        public static readonly string DefaultInventoryItemTaxClassificationRefName = "Product marked exempt by its seller (seller accepts full responsibility)";
    }
}

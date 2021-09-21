using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.QuickBooksConnection.Infrastructure
{
    public class QboUniversalConsts
    {
        public static readonly String Connection401Error = "Unauthorized-401";
        public static readonly String DefaultIncomeAccountName = "Sales of Product Income";
        public static readonly String DefaultExpenseAccountName = "Cost of Goods Sold";
        public static readonly String DefaultAssetAccountName = "Inventory Asset";
        public static readonly String DefaultDiscountAccountName = "Discounts given";
        public static readonly String DefaultShippingAccountName = "Shipping Income";
        public static readonly String DefaultInventoryItemDescription = "Default Inventory Item created by Digitbridge, "
            + "Income, Expense and Asset Account are configurable.";
        public static readonly String DefaultNonInventoryItemDescription = "Default Non-Inventory Item created by Digitbridge, "
            + "Income account is configurable.";
        /// <summary>
        /// NonTaxable Class Value
        /// </summary>
        public static readonly String DefaultInventoryItemTaxClassificationRefValue = "EUC-99990101-V1-00020000";
        /// <summary>
        /// NonTaxable Class Name
        /// </summary>
        public static readonly String DefaultInventoryItemTaxClassificationRefName = "Product marked exempt by its seller (seller accepts full responsibility)";
    }
}

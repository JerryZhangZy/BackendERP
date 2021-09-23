using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Infrastructure
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
    public class SqlCommandConsts
    {
        public static readonly string DateTimeFormatStringForLastUpdate = "yyyy-MM-dd HH:mm:ss.fff";
    }
    public class QboMappingConsts
    {
        public static readonly String DiscountRefValue = "86";
        public static readonly String DiscountRefName = "Discounts given";
        public static readonly String SummaryDiscountLineDescription = "Discount Line for Sales Order DigitBridge Order Id: ";
        public static readonly String SippingCostRefValue = "SHIPPING_ITEM_ID";
        public static readonly String SummaryShippingLineDescription = "Shipping Cost Line for Sales Order DigitBridge Order Id: ";
        public static readonly String SalesTaxItemRefValue = "55";
        public static readonly String SalesTaxItemDescription = "Calculated Sales Tax From DigitBridge Central for Sales Order DigitBridge Order Id: ";
        public static readonly String EndCustomerPoNumCustomFieldId = "1";
        public static readonly String ChnlOrderIdCustomFieldId = "2";
        public static readonly String SecChnlOrderIdCustomFieldId = "3";
        public static readonly String NoMatchingItemReturnString = "Skip";
        public static readonly String QboItemNullId = "0";
    }
    public class QboTempExportSettingConsts
    {
        public static readonly String WalmartChannelCutomerId = "75";
        public static readonly String DefaultItemId = "54";
        public static readonly Boolean ExportCustomerType = true;
    }
    public class QboOrderExportErrorMsgs
    {
        public static readonly String OrderUpdateErrorPrefix = "Order Update Error on DigitBridge Order Id: ";
        public static readonly String OrderTransferErrorPrefix = " Error on DigitBridge Order Id: ";
        public static readonly String OrderTransferExceptionPrefix = " Failed with exception on DigitBridge Order Id: ";
        public static readonly String OrderTransferAsDailySummeryExceptionPrefix = "Export Sales Order as Qbo Daily Summay Failed on DigitBridge Order Id: ";
        public static readonly String DailySummeryTransferExceptionPrefix = "Export Qbo Daily Summay Failed with exception on Channel Account Name: ";

        public static readonly String OrderUpdateNotFoundErrorPostfix = ", Can't find target Invoice/Sales Receipt in QBO, did you delete it in QBO?";
        public static readonly String OrderSyncStatusErrorPostfix = ", The Sync Status of this Order/ Itme Line has been modified by other instance. ";
        public static readonly String OrderTransferCustomerErrorPostfix = ", Cutomer handling failed, please check the channel customer setting.";
        public static readonly String OrderDefaultItemIdNotFoundErrorPostfix = "Default Item Id in setting not found in Quickbooks. ";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboMappingConsts
    {
        public static readonly string DiscountRefValue = "86";
        public static readonly string DiscountRefName = "Discounts given";
        public static readonly string SummaryDiscountLineDescription = "Discount Line for Invoice DigitBridge invoice number: ";
        
        public static readonly string SummaryShippingLineDescription = "Shipping Cost Line for Invoice DigitBridge invoice number: ";
        public static readonly string SalesTaxItemRefValue = "55";
        public static readonly string SalesTaxItemDescription = "Calculated Tax From DigitBridge Central for Invoice DigitBridge invoice number: "; 
        public static readonly string NoMatchingItemReturnString = "Skip";
        public static readonly string QboItemNullId = "0";

        public static readonly string SummaryMiscLineDescription = "Misc Cost Line for Invoice DigitBridge invoice number: ";
        public static readonly string SummaryChargeAndAllowanceLineDescription = "ChargeAndAllowance Cost Line for Invoice DigitBridge invoice number: ";
      
        public const string QboInvoiceNumberFieldDefaultName = "Invoice Number#"; //TODO read this from qbo
        public const string QboEndCustomerPoNumCustomFieldName = "Po Number#";//TODO read this from qbo 

        public const int CustomFieldMaxLength = 31;
        public const int ShipMethodRefMaxLength = 31;

        public const string QboDefaultItemId = "1";//TODO set this by requirement.
        public const string QboSalesTaxItemId = "1";//TODO set this by requirement.
        public const string QboChargeAndAllowanceItemId = "1";//TODO set this by requirement.
        public const string QboMiscItemId = "1";//TODO set this by requirement. 

        public const string QboInvoiceNumberFieldID = "1";
        public const string QboChnlOrderIdCustFieldId = "2";
        public const string Qbo2ndChnlOrderIdCustFieldId = "3"; 
        public const string SippingCostRefValue  = "1";//TODO "1" replace to  "SHIPPING_ITEM_ID";
    }
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

    public class ReturnMappingConsts
    {
        public static readonly string SummaryShippingLineDescription = "Shipping Cost Line for Invoice Return. Invoice number: {0}, Tran number:{1}";
        public static readonly string SalesTaxItemDescription = "Tax should return for Invoice Return. Invoice number: {0}, Tran number:{1} ";
        public static readonly string SummaryMiscLineDescription = "Misc Cost Line for Invoice Return. Invoice number: {0}, Tran number:{1}";
        public static readonly string SummaryChargeAndAllowanceLineDescription = "ChargeAndAllowance Cost Line for Invoice Return. Invoice number: {0}, Tran number:{1}";
    }

    public class PaymentMappingConsts
    {
        public const string PaymentTypeExpense = "Expense";//--Payment is reimbursement for expense paid by cash made on behalf of the customer
        //Check,//--Payment is reimbursement for expense paid by check made on behalf of the customer
        public const string PaymentTypeCreditCardCredit = "CreditCardCredit";//--Payment is reimbursement for a credit card credit made on behalf of the customer
                                                                             //JournalEntry,//--Payment is linked to the representative journal entry
                                                                             //CreditMemo,//--Payment is linked to the credit memo the customer has with the business
                                                                             //Invoice,//--The invoice to which payment is applied 
    }
}

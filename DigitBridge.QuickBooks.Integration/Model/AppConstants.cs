﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboMappingConsts
    {
        public static readonly string DiscountRefValue = "86";
        public static readonly string DiscountRefName = "Discounts given";
        public static readonly string SummaryDiscountLineDescription = "Discount Line for Invoice DigitBridge invoice number: ";
        public static readonly string SippingCostRefValue = "SHIPPING_ITEM_ID";
        public static readonly string SummaryShippingLineDescription = "Shipping Cost Line for Invoice DigitBridge invoice number: ";
        public static readonly string SalesTaxItemRefValue = "55";
        public static readonly string SalesTaxItemDescription = "Calculated Tax From DigitBridge Central for Invoice DigitBridge invoice number: ";
        public static readonly string EndCustomerPoNumCustomFieldId = "1";
        public static readonly string ChnlOrderIdCustomFieldId = "2";
        public static readonly string SecChnlOrderIdCustomFieldId = "3";
        public static readonly string NoMatchingItemReturnString = "Skip";
        public static readonly string QboItemNullId = "0";

        public static readonly string SummaryMiscLineDescription = "Misc Cost Line for Invoice DigitBridge invoice number: ";
        public static readonly string SummaryChargeAndAllowanceLineDescription = "ChargeAndAllowance Cost Line for Invoice DigitBridge invoice number: ";
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
        public const string PaymentTypeCreditCardCredit = "CreditCardCredit",//--Payment is reimbursement for a credit card credit made on behalf of the customer
            //JournalEntry,//--Payment is linked to the representative journal entry
            //CreditMemo,//--Payment is linked to the credit memo the customer has with the business
            //Invoice,//--The invoice to which payment is applied 
    }
}

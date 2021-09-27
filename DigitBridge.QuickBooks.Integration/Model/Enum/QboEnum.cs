using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public enum SaleOrderQboType
    {
        Null = -1,
        Invoice = 0,
        SalesReceipt = 1,
        DailySummarySalesReceipt = 2,
        DailySummaryInvoice = 3,
        DoNotExport = 4
    }
    public enum CustomerCreateRule
    {
        PerMarketPlace = 0,
        PerOrder = 1
    }
    public enum SalesTaxExportRule
    {
        DoNotExportSalesTaxFromDigitbridge = 0,
        ExportToDefaultSaleTaxItemAccount = 1
    }
    //public enum QboPaymentType
    //{
    //    Expense,//--Payment is reimbursement for expense paid by cash made on behalf of the customer
    //    Check,//--Payment is reimbursement for expense paid by check made on behalf of the customer
    //    CreditCardCredit,//--Payment is reimbursement for a credit card credit made on behalf of the customer
    //    JournalEntry,//--Payment is linked to the representative journal entry
    //    CreditMemo,//--Payment is linked to the credit memo the customer has with the business
    //    Invoice,//--The invoice to which payment is applied
    //}
}

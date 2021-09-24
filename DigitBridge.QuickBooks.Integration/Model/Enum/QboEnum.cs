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
}

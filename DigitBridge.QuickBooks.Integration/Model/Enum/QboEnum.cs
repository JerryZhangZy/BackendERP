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
}

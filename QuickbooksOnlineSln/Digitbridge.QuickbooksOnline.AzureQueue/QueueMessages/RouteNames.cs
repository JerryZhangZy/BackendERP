using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages
{
    public class RouteNames
    {
        public const string QuickBooksOrderImportQueue = "quickbooks-order-import";
        public const string QuickBooksOrderExportQueue = "quickbooks-order-export";
        public const string QuickBooksSummaryExportQueue = "quickbooks-summary-export";
    }
}

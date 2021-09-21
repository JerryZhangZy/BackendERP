using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksMdl
{
    public enum OrderTransferType
    {
        Undefined = 0,
        CreateTransaction = 1,
        UpdateTransaction = 2,
        PreprocessSummaryOrder = 3,
        CreateDailySummary = 4
    }
    public enum IsExportingSummary
    {
        False = 0,
        True = 1
    }
}

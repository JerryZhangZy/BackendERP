using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class DetailTransferResult
    {
        public DetailResultStatus Status { get; set; }
        public int ProcessedCount { get; set; }
        public int PendingCount { get; set; }
        public string Message { get; set; }
        
        public DetailTransferResult() { }
        public DetailTransferResult(DetailResultStatus status, string message, int successfulCount = 0)
        {
            Status = status;
            Message = message;
            ProcessedCount = successfulCount;
        }
        public DetailTransferResult(DetailResultStatus status, string message, int pendingCount, int successfulCount = 0)
        {
            Status = status;
            Message = message;
            PendingCount = pendingCount;
            ProcessedCount = successfulCount;
        }
    }

    public enum DetailResultStatus
    {
        Uninitiated = 0,
        Success = 1,
        SuccessPartial = 2,
        Unfinished = 3,
        SkipForTimeGap = 4,
        Fail = 255
    }

}

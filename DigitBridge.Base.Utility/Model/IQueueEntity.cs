using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public interface IQueueEntity
    {
        string MessageId { get; set; }
        string PopReceipt { get; set; }
    }
}

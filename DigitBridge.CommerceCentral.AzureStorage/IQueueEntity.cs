using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public interface IQueueEntity
    {
        string MessageId { get; set; }
        string PopReceipt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum EventProcessActionStatusEnum : int
    {
        Default = 0,
        Locked = 1,
        Failed = 2,
    }

    public enum EventProcessTypeEnum : int
    {
        InvoiceToChanel = 1
    }
}
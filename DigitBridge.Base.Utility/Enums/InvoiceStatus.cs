using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum InvoiceStatusEnum : int
    {
        New = 0,
        Outstanding = 1,
        Paid = 2,
        OverPaid = 3,
        PastDue = 100,
        Closed = 200,
        Void = 255,
    }
}

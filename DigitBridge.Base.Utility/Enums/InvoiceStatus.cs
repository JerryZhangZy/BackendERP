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
        Closed = 3,
        Void = 255,
    }
}

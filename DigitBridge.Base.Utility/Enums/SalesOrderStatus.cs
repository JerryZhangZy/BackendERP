using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum SalesOrderStatus : int
    {
        New = 0,
        Open = 1,
        Shipped = 2,
        Closed = 3,
        Cancelled = 255,
    }
}

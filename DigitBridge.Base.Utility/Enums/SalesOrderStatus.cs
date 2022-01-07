using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum SalesOrderStatus : int
    {
        ReadyToShip = 0,
        Open = 1,
        Approved = 2,
        Processing = 3,
        Shipped = 4,
        Closed = 5,
        Pending = 99,
        Hold = 100,
        Cancelled = 255,
    }
}

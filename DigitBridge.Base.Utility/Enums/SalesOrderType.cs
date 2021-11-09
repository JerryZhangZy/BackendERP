using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum SalesOrderType : int
    {
        [Description("Sales Order")] 
        Sales = 0,
        EcommerceOrder = 1,
        DropShipOrder = 2,
        Others = 3,
    }
}

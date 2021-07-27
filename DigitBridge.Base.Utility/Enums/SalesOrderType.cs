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
        ChannelOrder = 1,
        DropShipOrder = 2,
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum PoStatus : int
    {
        New = 0,
        Open = 1,
        Shipped = 2,
        Closed = 3,
        Cancelled = 255,
    }
    public enum PoType : int
    {
        Sales = 0,
        ChannelOrder = 1,
        DropShipOrder = 2,
    }
}

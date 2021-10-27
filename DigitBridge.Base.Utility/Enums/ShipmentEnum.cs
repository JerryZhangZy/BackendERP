using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ShipmentStatus : int
    {
        [Description("Default")]
        Default = -1,
        [Description("Pending")]
        Pending = 0,
        [Description("Shipped")]
        Shipped = 1,
        [Description("Partial Shipped")]
        PartialShipped = 2,
        [Description("Cancelled")]
        Cancelled = 3,
        [Description("Shipping")]
        Shipping = 9,
    }
    public enum ShipmentType : int
    {
        Default = 0
    }
    public enum ShipmentProcessStatus : int
    {
        Default = 0
    }

}

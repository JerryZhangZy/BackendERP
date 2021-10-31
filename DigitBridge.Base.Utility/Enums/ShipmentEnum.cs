using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum OrderShipmentStatusEnum : int
    {
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
        [Description("Canceled")]
        Canceled = 16
    }
    public enum OrderShipmentTypeEnum : int
    {
        Default = 0
    }
    public enum OrderShipmentProcessStatusEnum : int
    {
        Default = -1,
        [Description("Shipment transferred to erp invoice")]
        Transferred = 0,
    }

    public enum OrderShipmentPackageTypeEnum : int
    {
        Solid = 0,
        Mix = 1,
        PrePack = 2,
    }

    public enum VolumeUnitEnum : int
    {
        CubicInch
    }

    public enum LengthUnitEnum : int
    {
        Inch
    }

    public enum WeightUnitEnum : int
    {
        Pound
    }
}

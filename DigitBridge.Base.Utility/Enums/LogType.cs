using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum LogType:int
    {
        Sales,
        Return,
        Shipment,
        Purchase,
        Adjust,
        Damage,
        CycleCount,
        PhysicalCount,
        TransferTo,
        TransferFrom
    }
}

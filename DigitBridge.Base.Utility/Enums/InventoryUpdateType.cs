using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum InventoryUpdateType:int
    {
        Adjust=1,
        Damage,
        CycleCount,
        PhysicalCount
    }
}

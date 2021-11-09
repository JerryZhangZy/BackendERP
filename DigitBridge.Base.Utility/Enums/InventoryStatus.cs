using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum InventoryStatus : int
    {
        Active = 0,
        Inactive = 1,
        Discontinued = 2,
        Closeout = 3,
        Liquidation = 4,
        Prelimnry = 5,
        New = 11,
        Promotional = 12,
        UsedGood = 13,
        UsedFair = 14,
    }
}

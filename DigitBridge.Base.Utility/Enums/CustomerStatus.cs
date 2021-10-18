using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum CustomerStatus : int
    {
        Active = 0,
        Inactive,
        Hold,
        Potential,
        New
    }
}

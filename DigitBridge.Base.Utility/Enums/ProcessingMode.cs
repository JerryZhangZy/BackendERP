using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ProcessingMode : int
    {
        None = 0,
        Add = 1,
        Edit = 2,
        List = 3,
        Delete = 4,
        Void = 5,
        Cancel = 6,
        Post = 7,
        Exit = -1
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum InvoiceReturnStatus : int
    {
        Authorized = 0,
        Received = 1,
        Closed = 2,
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum TransStatus : int
    {
        Paid = 0,
    }

    public enum TransType : int
    {
        Payment = 1,
        Return = 2,
        CreditPayment = 3,
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum PaidByInvoice : int
    {
        Cash = 0,
        Check = 1,
        CashierCheck = 2,
        ECheck = 3,
        CreditCard = 4,
        WireTransfer = 5,
        Paypal = 6,
        AuthorizeNet = 7,
        AmazonPay = 8,
        GooglePay = 9,
        ApplePay = 10,
        Other = 20,
        CustomerCredit = 40,
        CreditMemo = 50
    }
}

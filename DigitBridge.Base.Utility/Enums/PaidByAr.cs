using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum PaidByAr : int
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

        PrePayment = 50,
        CreditMemo = 51,
        Refund = 60,
        ChargeOff = 65,
        DebitMemo = 71,
        BouncedCheck = 72,
        ReturnCredit = 201,
        ReturnCash = 202,
        Return = 203,

        Expense = 30
    }
}

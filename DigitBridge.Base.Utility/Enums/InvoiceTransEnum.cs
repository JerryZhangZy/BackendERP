using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum TransStatus : int
    {
        Draft = 0,
        Overdue = 1,
        Pending = 2,
        Payable = 3,
        Paid = 4,
        Trash = 5,
        UnPaid = 6
    }

    public enum PaidByEnum : int
    {
        Cash = 0,
        Check = 1,
        CreditCard = 2,
        Expense = 3,
        PreSales=4,
        Other = 5 
    }
}

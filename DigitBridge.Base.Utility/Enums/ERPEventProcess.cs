using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum EventProcessActionStatusEnum : int
    {
        Pending = 0,
        Downloaded = 1,
    }
    public enum EventProcessProcessStatusEnum : int
    {
        Pending = 0,
        Success = 1,
        Failed = 2,
    }
    public enum EventProcessCloseStatusEnum : int
    {
        Open = 0,
        Closed = 1,
    }


    public enum EventProcessTypeEnum : int
    {
        InvoiceToCommerceCentral = 1,
        SalesOrderToWMS = 2,
        PoToWMS = 3,
    }
}
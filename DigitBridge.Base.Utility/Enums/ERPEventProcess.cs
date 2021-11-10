using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum EventProcessActionStatusEnum : int
    {
        [Description("Data Added or updated.")]
        Default = 0,

        [Description("Data downloaded by consumer.")]
        Downloaded = 1,

        //Failed = 3,

        [Obsolete]
        [Description("This will be removed.")]
        Locked = 4,
    }

    public enum EventProcessTypeEnum : int
    {
        InvoiceToChanel = 1,
        SalesOrderToWMS = 2
    }
}
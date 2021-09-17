using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum InventoyLogType:int
    {
        Invoice,
        InvoiceReturn,

        Shipment,

        Adjust,
        Damage,
        Count,

        ToWarehouse,
        FromWarehouse,

        Assemble,
        Disassemble,

        POReceive,
        POReturn
    }
}

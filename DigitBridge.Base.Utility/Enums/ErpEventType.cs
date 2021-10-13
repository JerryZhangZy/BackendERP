using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DigitBridge.Base.Common
{
    public enum ErpEventType : int
    {
        [Description("CentralOrder To SalesOrder")]
        CentralOrderToSalesOrder = 1,
        [Description("Shipment To Invoice")]
        ShipmentToInvoice  = 1,
    }
}

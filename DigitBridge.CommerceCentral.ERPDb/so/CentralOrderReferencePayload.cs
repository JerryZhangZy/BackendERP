using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb.so
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
   public class CentralOrderReferencePayload: PayloadBase
    {
 
        public long CentralOrderNum { get; set; }

        public StringBuilder SalesOrderList { get; set; }
        public StringBuilder ShipmentList { get; set; }
        public StringBuilder InvoiceList { get; set; }
 
    }
}

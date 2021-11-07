using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Model.Payload
{
    [Serializable()]
    class WMSInventorySyncPayload
    {
        public WMSInventorySyncModel InventorySyncData { get; set; }
    }
}

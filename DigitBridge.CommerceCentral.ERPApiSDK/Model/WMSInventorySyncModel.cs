using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Model
{
   public class WMSInventorySyncModel
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }
        public IList<InventorySyncItemsModel> InventorySyncItems { get; set; }
    }

    public class InventorySyncItemsModel
    {
        public string SKU { get; set; }
        public string WarehouseCode { get; set; }
        public decimal? Qty { get; set; }
    }
}

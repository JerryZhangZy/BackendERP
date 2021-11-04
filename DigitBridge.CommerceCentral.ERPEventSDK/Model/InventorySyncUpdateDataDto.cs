using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK.Model
{

    [Serializable()]
    public class InventorySyncUpdatePayload
    {
        public InventorySyncUpdateDataDto InventorySyncData { get; set; }
    }
    [Serializable()]
    public class InventorySyncUpdateDataDto
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }
        public IList<InventorySyncItemsDto> InventorySyncItems { get; set; }
    }

    [Serializable()]
    public class InventorySyncItemsDto
    {
        public string SKU { get; set; }
        public string WarehouseCode { get; set; }
        public decimal? Qty { get; set; }
    }
}

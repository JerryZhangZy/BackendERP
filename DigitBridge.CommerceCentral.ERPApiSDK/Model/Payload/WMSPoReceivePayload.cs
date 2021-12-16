using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class WMSPoReceiveItem
    {
        /// <summary>
        /// WMS batch num
        /// </summary>
        public string WMSBatchNum { get; set; }

        /// <summary>
        /// VendorCode
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// Po unique key erp provided.
        /// </summary>
        public string PoUuid { get; set; }

        /// <summary>
        /// Po item unique key erp provided.
        /// </summary>
        public string PoItemUuid { get; set; }

        /// <summary>
        /// WMS Received WarehouseCode
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Item Sku
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// WMS Received item quantity
        /// </summary>
        public decimal Qty { get; set; }
    }
    public class WMSPoReceivePayload : ResponsePayloadBase
    {
        /// <summary>
        /// The uuid list of po item
        /// </summary>
        public List<string> PoItemUuidList { get; set; }

        /// <summary>
        /// the uuid of Po transaction
        /// </summary>
        public string TransUuid { get; set; }
    }
}

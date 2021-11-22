using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// WMS receive po item.
    /// </summary>
    public class WMSPoReceiveItem
    {
        /// <summary>
        /// Po header unique key erp provided.
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
}

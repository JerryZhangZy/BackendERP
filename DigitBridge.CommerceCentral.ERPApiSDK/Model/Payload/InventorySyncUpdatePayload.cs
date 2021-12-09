using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InventorySyncUpdatePayload : ResponsePayloadBase
    {

        /// <summary>
        /// (Request Data) InventorySync object to update.
        /// (Response Data) InventorySync object which has been updated.
        /// </summary>
        public IList<InventorySyncItem> InventorySyncItems { get; set; }
        public bool HasInventorySyncItems => InventorySyncItems != null && InventorySyncItems.Count > 0;
        public bool ShouldSerializeInventorySyncItems() => HasInventorySyncItems;


    }

    [Serializable()]
    public class InventorySyncItem
    {
        [StringLength(50, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }


        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 100 characters. ")]
        public string WarehouseCode { get; set; }


        /// <summary>
        /// Item in stock Qty. <br> Title: Instock, Display: true, Editable: false
        /// </summary>
        public decimal Qty { get; set; }
    }

}

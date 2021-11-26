using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb.inventorySync.dto
{    /// <summary>
     /// Represents a ApInvoiceDataDto Class.
     /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
     /// </summary>
    [Serializable()]
    public class InventorySyncUpdateDataDto
    {
        public IList<InventorySyncItem> InventorySyncItems { get; set; }
        public bool HasInventorySyncItems => InventorySyncItems != null;

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static  InventorySyncUpdateDataDto GetFakerData()
        {
            var data = new InventorySyncUpdateDataDto();
            data.InventorySyncItems = new List<InventorySyncItem>()
            {
                new InventorySyncItem(){ SKU="rabbimt",WarehouseCode="test", Qty=10 }
            };
 
            return data;
        }
    }
}

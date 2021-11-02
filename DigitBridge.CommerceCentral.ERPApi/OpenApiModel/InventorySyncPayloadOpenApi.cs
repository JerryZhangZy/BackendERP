 
using DigitBridge.CommerceCentral.ERPDb.inventorySync.dto;
 
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi.OpenApiModel
{
    [Serializable()]
    public class InventorySyncPayloadAdd
    {

        /// <summary>
        /// (Request Data) Inventory object to add.
        /// (Response Data) Inventory object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InventorySync object to add.")]
        public InventorySyncUpdateDataDto InventoryData { get; set; }

        public static InventorySyncPayloadAdd GetSampleData()
        {
            var data = new InventorySyncPayloadAdd();
             //data.InventoryData = new InventorySyncUpdateDataDto().GetFakerData();
            return data;
        }
    }
}

 
using DigitBridge.CommerceCentral.ERPDb.inventorySync.dto;
 
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [Serializable()]
    public class InventorySyncPayloadAdd
    {

        /// <summary>
        /// (Request Data) InventorySyncData  object to add.
        /// (Response Data) InventorySyncData object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InventorySync object to add.")]
        public InventorySyncUpdateDataDto InventorySyncData { get; set; }

        public static InventorySyncPayloadAdd GetSampleData()
        {
            var data = new InventorySyncPayloadAdd();
            data.InventorySyncData = InventorySyncUpdateDataDto.GetFakerData();
            return data;
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    [Serializable()]
    public class InventorySyncItem
    {
        [OpenApiPropertyDescription("SKU")]
        [StringLength(50, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }


        [OpenApiPropertyDescription("WarehouseCode")]
        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 100 characters. ")]
        public string WarehouseCode { get; set; }


        /// <summary>
        /// Item in stock Qty. <br> Title: Instock, Display: true, Editable: false
        /// </summary>
        [OpenApiPropertyDescription("Item in stock Qty. <br> Title: Instock, Display: true, Editable: false")]
        public decimal Qty { get; set; }
    }
}

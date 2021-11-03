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
{
   public class InventorySyncUpdateDataDto
    {
        public IList<InventorySyncItemsDto> InventorySyncItems { get; set; }
        public bool HasInventorySyncItems => InventorySyncItems != null;
    }
}

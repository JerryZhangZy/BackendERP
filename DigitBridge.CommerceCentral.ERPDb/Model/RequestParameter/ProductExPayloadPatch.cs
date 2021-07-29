using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class ProductExPayloadPatch
    {

        //public IList<InventoryDataDto> InventoryDatas { get; set; } 
        public InventoryDataDto InventoryData { get; set; }

    }
}

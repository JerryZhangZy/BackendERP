
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class SalesOrderHeader
    {
        public decimal TotalLineCommissionAmount { get; set; }

        public IList<SalesOrderHeaderInfo> SalesOrderHeaderInfoJson { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderHeaderInfoJson => SalesOrderHeaderInfoJson != null && SalesOrderHeaderInfoJson.Count > 0;
        public bool ShouldSerializeSalesOrderHeaderInfoJson() => HasSalesOrderHeaderInfoJson;

        public IList<SalesOrderHeaderAttributes> SalesOrderHeaderAttributesJson { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderHeaderAttributesJson => SalesOrderHeaderAttributesJson != null && SalesOrderHeaderAttributesJson.Count > 0;
        public bool ShouldSerializeSalesOrderHeaderAttributesJson() => HasSalesOrderHeaderAttributesJson;

        public IList<SalesOrderItems> SalesOrderItemsJson { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderItemsJson => SalesOrderItemsJson != null && SalesOrderItemsJson.Count > 0;
        public bool ShouldSerializeSalesOrderItemsJson() => HasSalesOrderItemsJson;

        public IList<SalesOrderItemsAttributes> SalesOrderItemsAttributesJson { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderItemsAttributesJson => SalesOrderItemsAttributesJson != null && SalesOrderItemsAttributesJson.Count > 0;
        public bool ShouldSerializeSalesOrderItemsAttributesJson() => HasSalesOrderItemsAttributesJson;
    }
}




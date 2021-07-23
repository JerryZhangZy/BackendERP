using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    public class SalesOrderPayload : PayloadBase
    {
        public IList<string> SalesOrderUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasSalesOrderUuids => SalesOrderUuids != null && SalesOrderUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasSalesOrderUuids;

        public IList<SalesOrderDataDto> SalesOrders { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrders => SalesOrders != null && SalesOrders.Count > 0;
        public bool ShouldSerializeSalesOrders() => HasSalesOrders;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "salesOrderUuids", val => SalesOrderUuids = val.Split(",").ToList() }
            };
        }

    }
}

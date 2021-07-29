using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class SalesOrderPayload : PayloadBase
    {
        public IList<string> SalesOrderUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasSalesOrderUuids => SalesOrderUuids != null && SalesOrderUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasSalesOrderUuids;

        public IList<SalesOrderDataDto> SalesOrders { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrders => SalesOrders != null && SalesOrders.Count > 0;
        public bool ShouldSerializeSalesOrders() => HasSalesOrders;

        public SalesOrderDataDto SalesOrder { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrder => SalesOrder != null;
        public bool ShouldSerializeSalesOrder() => HasSalesOrder;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "salesOrderUuids", val => SalesOrderUuids = val.Split(",").ToList() }
            };
        }


        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder SalesOrderList { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderList => SalesOrderList != null && SalesOrderList.Length > 0;
        public bool ShouldSerializeSalesOrderList() => HasSalesOrderList;

        public int SalesOrderListCount { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderListCount => SalesOrderListCount > 0;
        public bool ShouldSerializeSalesOrderListCount() => HasSalesOrderListCount;

    }
}

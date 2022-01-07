using DigitBridge.Base.Utility;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
   public class CentralOrderReferencePayload: PayloadBase
    {
 
        public long CentralOrderNum { get; set; }

        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder SalesOrderList { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderList => SalesOrderList != null && SalesOrderList.Length > 0;
        public bool ShouldSerializeSalesOrderList() => HasSalesOrderList;


        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ShipmentList { get; set; }
        [JsonIgnore] public virtual bool HasShipmentList => ShipmentList != null && ShipmentList.Length > 0;
        public bool ShouldSerializeShipmentList() => HasShipmentList;

        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder InvoiceList { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceList => InvoiceList != null && InvoiceList.Length > 0;
        public bool ShouldSerializeInvoiceList() => HasInvoiceList;

    }
}

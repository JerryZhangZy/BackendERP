using DigitBridge.Base.Utility;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class WarehouseTransferItemPayload : PayloadBase
    {
        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder WarehouseTransferItemList { get; set; }
        [JsonIgnore] public virtual bool HasWarehouseTransferItemList => WarehouseTransferItemList != null && WarehouseTransferItemList.Length > 0;
        public bool ShouldSerializeWarehouseTransferList() => HasWarehouseTransferItemList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int WarehouseTransferItemsCount { get; set; }
        [JsonIgnore] public virtual bool HasWarehouseTransferItemListCount => WarehouseTransferItemsCount > 0;
        public bool ShouldSerializeWarehouseTransferListCount() => HasWarehouseTransferItemListCount;
    }
}

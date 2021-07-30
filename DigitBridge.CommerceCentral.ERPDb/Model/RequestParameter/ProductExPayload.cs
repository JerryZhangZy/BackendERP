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
    public class ProductExPayload : PayloadBase
    {
        public IList<string> Skus { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasSkus => Skus != null && Skus.Count > 0;
        public bool ShouldSerializeCustomerCodes() => HasSkus;

        public IList<InventoryDataDto> InventoryDatas { get; set; } = new List<InventoryDataDto>();
        [JsonIgnore] public virtual bool HasInventoryDatas => InventoryDatas != null && InventoryDatas.Count > 0;
        public bool ShouldSerializeInventoryDatas() => HasInventoryDatas;

        public InventoryDataDto InventoryData { get; set; }
        [JsonIgnore] public virtual bool HasInventoryData => InventoryData != null;
        public bool ShouldSerializeInventoryData() => HasInventoryData;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "skus", val => Skus = val.Split(",").ToList() }
            };
        }

        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ProductList { get; set; }
        [JsonIgnore] public virtual bool HasIProductList => ProductList != null && ProductList.Length > 0;
        public bool ShouldSerializeProductList() => HasIProductList;

        public int ProductListCount { get; set; }
        [JsonIgnore] public virtual bool HasProductListCount => ProductListCount > 0;
        public bool ShouldSerializeProductListCountCount() => HasProductListCount;

    }
}

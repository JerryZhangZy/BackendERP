﻿using Newtonsoft.Json;
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
    public class ProductExPayload : PayloadBase
    {
        public IList<string> Skus { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasSkus => Skus != null && Skus.Count > 0;
        public bool ShouldSerializeCustomerCodes() => HasSkus;

        public IList<InventoryDataDto> InventoryDatas { get; set; }
        [JsonIgnore] public virtual bool HasInventoryDatas => InventoryDatas != null && InventoryDatas.Count > 0;
        public bool ShouldSerializeInventoryDatas() => HasInventoryDatas;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "skus", val => Skus = val.Split(",").ToList() }
            };
        }

    }
}

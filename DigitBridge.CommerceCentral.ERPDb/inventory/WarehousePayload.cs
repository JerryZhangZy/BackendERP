
    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class WarehousePayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "DistributionCenterUuids", val => DistributionCenterUuids = val.Split(",").ToList() },
                { "WarehouseCodes", val => WarehouseCodes = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple Warehouse dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple Warehouse dto data.")]
        public IList<string> DistributionCenterUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasDistributionCenterUuids => DistributionCenterUuids != null && DistributionCenterUuids.Count > 0;
        public bool ShouldSerializeDistributionCenterUuids() => HasDistributionCenterUuids;

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple Warehouse dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple Warehouse dto data.")]
        public IList<string> WarehouseCodes { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasWarehouseCodes => WarehouseCodes != null && WarehouseCodes.Count > 0;
        public bool ShouldSerializeWarehouseCodes() => HasWarehouseCodes;

        /// <summary>
        /// (Response Data) Array of Warehouse entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<WarehouseDataDto> Warehouses { get; set; }
        [JsonIgnore] public virtual bool HasWarehouses => Warehouses != null && Warehouses.Count > 0;
        public bool ShouldSerializeWarehouses() => HasWarehouses;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single Warehouse entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public WarehouseDataDto Warehouse { get; set; }
        [JsonIgnore] public virtual bool HasWarehouse => Warehouse != null;
        public bool ShouldSerializeWarehouse() => HasWarehouse;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder WarehouseList { get; set; }
        [JsonIgnore] public virtual bool HasWarehouseList => WarehouseList != null && WarehouseList.Length > 0;
        public bool ShouldSerializeWarehouseList() => HasWarehouseList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int WarehouseListCount { get; set; }
        [JsonIgnore] public virtual bool HasWarehouseListCount => WarehouseListCount > 0;
        public bool ShouldSerializeWarehouseListCount() => HasWarehouseListCount;

        #endregion list service
    }
}


    
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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class InventoryUpdatePayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "BatchNumbers", val => BatchNumbers = val.Split(",").ToList() }
            };
        }

        [JsonIgnore]
        public InventoryUpdateType InventoryUpdateType { get; set; } = InventoryUpdateType.Adjust;

        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple InventoryUpdate dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple InventoryUpdate dto data.")]
        public IList<string> BatchNumbers { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasBatchNumbers => BatchNumbers != null && BatchNumbers.Count > 0;
        public bool ShouldSerializeBatchNumbers() => HasBatchNumbers;

        /// <summary>
        /// (Response Data) Array of InventoryUpdate entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<InventoryUpdateDataDto> InventoryUpdates { get; set; }
        [JsonIgnore] public virtual bool HasInventoryUpdates => InventoryUpdates != null && InventoryUpdates.Count > 0;
        public bool ShouldSerializeInventoryUpdates() => HasInventoryUpdates;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single InventoryUpdate entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public InventoryUpdateDataDto InventoryUpdate { get; set; }
        [JsonIgnore] public virtual bool HasInventoryUpdate => InventoryUpdate != null;
        public bool ShouldSerializeInventoryUpdate() => HasInventoryUpdate;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder InventoryUpdateList { get; set; }
        [JsonIgnore] public virtual bool HasInventoryUpdateList => InventoryUpdateList != null && InventoryUpdateList.Length > 0;
        public bool ShouldSerializeInventoryUpdateList() => HasInventoryUpdateList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int InventoryUpdateListCount { get; set; }
        [JsonIgnore] public virtual bool HasInventoryUpdateListCount => InventoryUpdateListCount > 0;
        public bool ShouldSerializeInventoryUpdateListCount() => HasInventoryUpdateListCount;

        #endregion list service
    }
}


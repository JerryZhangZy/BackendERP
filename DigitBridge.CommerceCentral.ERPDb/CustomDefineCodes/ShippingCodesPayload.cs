    
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
    public class ShippingCodesPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "ShippingCodesUuids", val => ShippingCodesUuids = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple ShippingCodes dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple ShippingCodes dto data.")]
        public IList<string> ShippingCodesUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasShippingCodesUuids => ShippingCodesUuids != null && ShippingCodesUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasShippingCodesUuids;

        /// <summary>
        /// (Response Data) Array of ShippingCodes entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<ShippingCodesDataDto> ShippingCodess { get; set; }
        [JsonIgnore] public virtual bool HasShippingCodess => ShippingCodess != null && ShippingCodess.Count > 0;
        public bool ShouldSerializeShippingCodess() => HasShippingCodess;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single ShippingCodes entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public ShippingCodesDataDto ShippingCodes { get; set; }
        [JsonIgnore] public virtual bool HasShippingCodes => ShippingCodes != null;
        public bool ShouldSerializeShippingCodes() => HasShippingCodes;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ShippingCodesList { get; set; }
        [JsonIgnore] public virtual bool HasShippingCodesList => ShippingCodesList != null && ShippingCodesList.Length > 0;
        public bool ShouldSerializeShippingCodesList() => HasShippingCodesList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int ShippingCodesListCount { get; set; }
        [JsonIgnore] public virtual bool HasShippingCodesListCount => ShippingCodesListCount > 0;
        public bool ShouldSerializeShippingCodesListCount() => HasShippingCodesListCount;

        #endregion list service
    }
}

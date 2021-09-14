    
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
    public class PurchaseOrderPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "poNums", val => PoNums = val.Split(",").Distinct().ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple PurchaseOrder dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple PurchaseOrder dto data.")]
        public IList<string> PoNums { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasPoNums => PoNums != null && PoNums.Count > 0;
        public bool ShouldSerializePoNums() => HasPoNums;

        /// <summary>
        /// (Response Data) Array of PurchaseOrder entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<PurchaseOrderDataDto> PurchaseOrders { get; set; }
        [JsonIgnore] public virtual bool HasPurchaseOrders => PurchaseOrders != null && PurchaseOrders.Count > 0;
        public bool ShouldSerializePurchaseOrders() => HasPurchaseOrders;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single PurchaseOrder entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public PurchaseOrderDataDto PurchaseOrder { get; set; }
        [JsonIgnore] public virtual bool HasPurchaseOrder => PurchaseOrder != null;
        public bool ShouldSerializePurchaseOrder() => HasPurchaseOrder;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder PurchaseOrderList { get; set; }
        [JsonIgnore] public virtual bool HasPurchaseOrderList => PurchaseOrderList != null && PurchaseOrderList.Length > 0;
        public bool ShouldSerializePurchaseOrderList() => HasPurchaseOrderList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int PurchaseOrderListCount { get; set; }
        [JsonIgnore] public virtual bool HasPurchaseOrderListCount => PurchaseOrderListCount > 0;
        public bool ShouldSerializePurchaseOrderListCount() => HasPurchaseOrderListCount;

        #endregion list service
    }
}



    
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
    public class OrderShipmentPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "OrderShipmentUuids", val => OrderShipmentUuids = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple OrderShipment dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple OrderShipment dto data.")]
        public IList<string> OrderShipmentUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasOrderShipmentUuids => OrderShipmentUuids != null && OrderShipmentUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasOrderShipmentUuids;

        /// <summary>
        /// (Response Data) Array of OrderShipment entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<OrderShipmentDataDto> OrderShipments { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipments => OrderShipments != null && OrderShipments.Count > 0;
        public bool ShouldSerializeOrderShipments() => HasOrderShipments;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single OrderShipment entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipment => OrderShipment != null;
        public bool ShouldSerializeOrderShipment() => HasOrderShipment;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder OrderShipmentList { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipmentList => OrderShipmentList != null && OrderShipmentList.Length > 0;
        public bool ShouldSerializeOrderShipmentList() => HasOrderShipmentList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int OrderShipmentListCount { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipmentListCount => OrderShipmentListCount > 0;
        public bool ShouldSerializeOrderShipmentListCount() => HasOrderShipmentListCount;

        #endregion list service
    }
}


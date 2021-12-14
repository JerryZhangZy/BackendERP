

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
                { "OrderShipmentNumbers", val => OrderShipmentNumbers = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple OrderShipment dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple OrderShipment dto data.")]
        public IList<string> OrderShipmentNumbers { get; set; }
        [JsonIgnore]
        public virtual bool HasOrderShipmentNumbers => OrderShipmentNumbers != null && OrderShipmentNumbers.Count > 0;
        //public bool ShouldSerializeShipmentUuids() => HasOrderShipmentNumbers;

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

        #region summary service 

        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ShipmentSummary { get; set; }
        [JsonIgnore] public virtual bool HasShipmentSummary => ShipmentSummary != null;

        public bool ShouldSerializeShipmentSummary() => HasShipmentSummary;



        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List summary result which load filter.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder OrderShipmentListSummary { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipmentListSummary => OrderShipmentListSummary != null && OrderShipmentListSummary.Length > 0;
        public bool ShouldSerializeOrderShipmentListSummary() => HasOrderShipmentListSummary;
        #endregion

        #region Re send wms shipment to erp

        /// <summary>
        /// (Request Parameter) Array of uuid that will be resend to erp.
        /// </summary>
        [OpenApiPropertyDescription(" (Request Parameter) Array of uuid that will be resend to erp.")]
        public IList<string> WMSShipmentIDs { get; set; }
        [JsonIgnore]
        public virtual bool HasWMSShipmentIDs => WMSShipmentIDs != null && WMSShipmentIDs.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasWMSShipmentIDs;

        /// <summary>
        /// (Response Parameter) Array of WMSShipmentID which have been sent to erp.
        /// </summary>
        [OpenApiPropertyDescription(" (Response Parameter) Array of WMSShipmentID which have been sent to erp.")]
        public List<string> SentWMSShipmentIDs { get; set; } = new List<string>();
        [JsonIgnore]
        public virtual bool HasSentWMSShipmentIDs => SentWMSShipmentIDs != null && SentWMSShipmentIDs.Count > 0;
        public bool ShouldSerializeSentWMSShipmentIDs() => HasSentWMSShipmentIDs;
        #endregion

        #region create shipment by salesorderuuid

        /// <summary>
        /// (Request Data) salesorderuuid.
        /// </summary>
        [OpenApiPropertyDescription("(Request Data) salesorderuuid.")]
        public string SalesOrderUuid { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderUuid => !SalesOrderUuid.IsZero();
        public bool ShouldSerializeSalesOrderUuid() => HasSalesOrderUuid;

        /// <summary>
        /// (Response Data) OrderShipmetUuid.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) OrderShipmetUuid.")]
        public string OrderShipmetUuid { get; set; }
        [JsonIgnore] public virtual bool HasOrderShipmetUuid => !OrderShipmetUuid.IsZero();
        public bool ShouldSerializeOrderShipmetUuid() => HasOrderShipmetUuid;
        #endregion
    }
}


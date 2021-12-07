using DigitBridge.Base.Utility;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class WMSOrderShipmentPayload : ResponsePayloadBase
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder WMSShipmentProcessesList { get; set; }
        [JsonIgnore] public virtual bool HasWMSShipmentProcessesList => WMSShipmentProcessesList != null && WMSShipmentProcessesList.Length > 0;
        public bool ShouldSerializeWMSShipmentProcessesList() => HasWMSShipmentProcessesList;

        #endregion 
    }
    public class WSMShipmentResendPayload : ResponsePayloadBase
    {

        #region Re send wms shipment to erp

        /// <summary>
        /// (Request Parameter) Array of uuid that will be resend to erp.
        /// </summary>
        [OpenApiPropertyDescription(" (Request Parameter) Array of uuid that will be resend to erp.")]
        public IList<string> WMSShipmentIDs { get; set; } = new List<string>();
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

    }
}

using DigitBridge.Base.Utility;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class WMSOrderShipmentPayload
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder WMSShipmentList { get; set; }
        [JsonIgnore] public virtual bool HasWMSShipmentList => WMSShipmentList != null && WMSShipmentList.Length > 0;
        public bool ShouldSerializeWMSShipmentList() => HasWMSShipmentList;

        #endregion
    }
}

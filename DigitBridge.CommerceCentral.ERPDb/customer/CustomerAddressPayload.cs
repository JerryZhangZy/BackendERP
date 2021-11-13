    
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
    public class CustomerAddressPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "AddressUuids", val => AddressUuids = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple CustomerAddress dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple CustomerAddress dto data.")]
        public IList<string> AddressUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasAddressUuids => AddressUuids != null && AddressUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasAddressUuids;

        /// <summary>
        /// (Response Data) Array of CustomerAddress entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<CustomerAddressDataDto> CustomerAddresss { get; set; }
        [JsonIgnore] public virtual bool HasCustomerAddresss => CustomerAddresss != null && CustomerAddresss.Count > 0;
        public bool ShouldSerializeCustomerAddresss() => HasCustomerAddresss;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single CustomerAddress entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public CustomerAddressDataDto CustomerAddress { get; set; }
        [JsonIgnore] public virtual bool HasCustomerAddress => CustomerAddress != null;
        public bool ShouldSerializeCustomerAddress() => HasCustomerAddress;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder CustomerAddressList { get; set; }
        [JsonIgnore] public virtual bool HasCustomerAddressList => CustomerAddressList != null && CustomerAddressList.Length > 0;
        public bool ShouldSerializeCustomerAddressList() => HasCustomerAddressList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int CustomerAddressListCount { get; set; }
        [JsonIgnore] public virtual bool HasCustomerAddressListCount => CustomerAddressListCount > 0;
        public bool ShouldSerializeCustomerAddressListCount() => HasCustomerAddressListCount;

        #endregion list service
    }
}

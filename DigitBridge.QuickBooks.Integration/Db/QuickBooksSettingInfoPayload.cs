    
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
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "SettingUuids", val => SettingUuids = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple QuickBooksSettingInfo dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple QuickBooksSettingInfo dto data.")]
        public IList<string> SettingUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasSettingUuids => SettingUuids != null && SettingUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasSettingUuids;

        /// <summary>
        /// (Response Data) Array of QuickBooksSettingInfo entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<QuickBooksSettingInfoDataDto> QuickBooksSettingInfos { get; set; }
        [JsonIgnore] public virtual bool HasQuickBooksSettingInfos => QuickBooksSettingInfos != null && QuickBooksSettingInfos.Count > 0;
        public bool ShouldSerializeQuickBooksSettingInfos() => HasQuickBooksSettingInfos;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single QuickBooksSettingInfo entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public QuickBooksSettingInfoDataDto QuickBooksSettingInfo { get; set; }
        [JsonIgnore] public virtual bool HasQuickBooksSettingInfo => QuickBooksSettingInfo != null;
        public bool ShouldSerializeQuickBooksSettingInfo() => HasQuickBooksSettingInfo;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder QuickBooksSettingInfoList { get; set; }
        [JsonIgnore] public virtual bool HasQuickBooksSettingInfoList => QuickBooksSettingInfoList != null && QuickBooksSettingInfoList.Length > 0;
        public bool ShouldSerializeQuickBooksSettingInfoList() => HasQuickBooksSettingInfoList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int QuickBooksSettingInfoListCount { get; set; }
        [JsonIgnore] public virtual bool HasQuickBooksSettingInfoListCount => QuickBooksSettingInfoListCount > 0;
        public bool ShouldSerializeQuickBooksSettingInfoListCount() => HasQuickBooksSettingInfoListCount;

        #endregion list service
    }
}

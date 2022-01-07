    
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
    public class ApTransactionPayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "TransUuids", val => TransUuids = val.Split(",").ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple ApTransaction dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple ApTransaction dto data.")]
        public IList<string> TransUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasTransUuids => TransUuids != null && TransUuids.Count > 0;
        public bool ShouldSerializeSalesOrderUuids() => HasTransUuids;

        /// <summary>
        /// (Response Data) Array of ApTransaction entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<ApTransactionDataDto> ApTransactions { get; set; }
        [JsonIgnore] public virtual bool HasApTransactions => ApTransactions != null && ApTransactions.Count > 0;
        public bool ShouldSerializeApTransactions() => HasApTransactions;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single ApTransaction entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public ApTransactionDataDto ApTransaction { get; set; }
        [JsonIgnore] public virtual bool HasApTransaction => ApTransaction != null;
        public bool ShouldSerializeApTransaction() => HasApTransaction;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ApTransactionList { get; set; }
        [JsonIgnore] public virtual bool HasApTransactionList => ApTransactionList != null && ApTransactionList.Length > 0;
        public bool ShouldSerializeApTransactionList() => HasApTransactionList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int ApTransactionListCount { get; set; }
        [JsonIgnore] public virtual bool HasApTransactionListCount => ApTransactionListCount > 0;
        public bool ShouldSerializeApTransactionListCount() => HasApTransactionListCount;

        #endregion list service
    }
}


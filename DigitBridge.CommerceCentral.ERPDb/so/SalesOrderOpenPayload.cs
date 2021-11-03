

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
    public class SalesOrderOpenListPayload : PayloadBase
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder SalesOrderOpenList { get; set; } 

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int SalesOrderOpenListCount { get; set; }
        [JsonIgnore] public virtual bool HasSalesOrderOpenListCount => SalesOrderOpenListCount > 0;
        public bool ShouldSerializeSalesOrderOpenListCount() => HasSalesOrderOpenListCount;

        #endregion list service 
    }
}


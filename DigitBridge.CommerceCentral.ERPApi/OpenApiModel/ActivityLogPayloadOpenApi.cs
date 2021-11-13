    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadAdd
    {
        /// <summary>
        /// (Request Data) ActivityLog object to add.
        /// (Response Data) ActivityLog object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) ActivityLog object to add.")]
        public ActivityLogDataDto ActivityLog { get; set; }
        
        public static ActivityLogPayloadAdd GetSampleData()
        {
            var data = new ActivityLogPayloadAdd();
            data.ActivityLog = new ActivityLogDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadUpdate
    {
        /// <summary>
        /// (Request Data) ActivityLog object to update.
        /// (Response Data) ActivityLog object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) ActivityLog object to update.")]
        public ActivityLogDataDto ActivityLog { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) ActivityLog object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) ActivityLog object to get.")]
        public ActivityLogDataDto ActivityLog { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple ActivityLogs.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple ActivityLogs.")]
        public IList<string> LogUuids { get; set; }

        /// <summary>
        /// (Response) Array of ActivityLog which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of ActivityLog which get by uuid array.")]
        public IList<ActivityLogDataDto> ActivityLogs { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class ActivityLogPayloadFind : FilterPayloadBase<ActivityLogFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> ActivityLogList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int ActivityLogListCount { get; set; }
        
        public static ActivityLogPayloadFind GetSampleData()
        {
            var data = new ActivityLogPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = ActivityLogFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class ActivityLogFilter
    {
        //public string City { get; set; }

        public static Faker<ActivityLogFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<ActivityLogFilter>()
                //.RuleFor(u => u.City, f => "")
                ;
            #endregion faker data rules
        }
    }

}

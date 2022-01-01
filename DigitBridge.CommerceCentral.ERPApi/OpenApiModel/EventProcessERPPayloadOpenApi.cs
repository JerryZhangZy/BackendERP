    
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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadAdd
    {
        /// <summary>
        /// (Request Data) EventProcessERP object to add.
        /// (Response Data) EventProcessERP object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) EventProcessERP object to add.")]
        public EventProcessERPDataDto EventProcessERP { get; set; }
        
        public static EventProcessERPPayloadAdd GetSampleData()
        {
            var data = new EventProcessERPPayloadAdd();
            data.EventProcessERP = new EventProcessERPDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadUpdate
    {
        /// <summary>
        /// (Request Data) EventProcessERP object to update.
        /// (Response Data) EventProcessERP object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) EventProcessERP object to update.")]
        public EventProcessERPDataDto EventProcessERP { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) EventProcessERP object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) EventProcessERP object to get.")]
        public EventProcessERPDataDto EventProcessERP { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple EventProcessERPs.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple EventProcessERPs.")]
        public IList<string> EventUuids { get; set; }

        /// <summary>
        /// (Response) Array of EventProcessERP which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of EventProcessERP which get by uuid array.")]
        public IList<EventProcessERPDataDto> EventProcessERPs { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class EventProcessERPPayloadFind : FilterPayloadBase<EventProcessERPFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> EventProcessERPList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int EventProcessERPListCount { get; set; }
        
        public static EventProcessERPPayloadFind GetSampleData()
        {
            var data = new EventProcessERPPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = EventProcessERPFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class EventProcessERPFilter
    {
        public DateTime ActionDateFrom { get; set; }
        public DateTime ActionDateTo { get; set; }
        public int ActionStatus { get; set; }
        public ErpEventType ERPEventProcessType { get; set; }

        public static Faker<EventProcessERPFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<EventProcessERPFilter>()
                //.RuleFor(u => u.City, f => "")
                ;
            #endregion faker data rules
        }
    }

}


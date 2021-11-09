    
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
    public class SystemCodesPayloadAdd
    {
        /// <summary>
        /// (Request Data) SystemCodes object to add.
        /// (Response Data) SystemCodes object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) SystemCodes object to add.")]
        public SystemCodesDataDto SystemCodes { get; set; }
        
        public static SystemCodesPayloadAdd GetSampleData()
        {
            var data = new SystemCodesPayloadAdd();
            data.SystemCodes = new SystemCodesDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class SystemCodesPayloadUpdate
    {
        /// <summary>
        /// (Request Data) SystemCodes object to update.
        /// (Response Data) SystemCodes object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) SystemCodes object to update.")]
        public SystemCodesDataDto SystemCodes { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class SystemCodesPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) SystemCodes object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) SystemCodes object to get.")]
        public SystemCodesDataDto SystemCodes { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class SystemCodesPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple SystemCodess.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple SystemCodess.")]
        public IList<string> SystemCodeUuids { get; set; }

        /// <summary>
        /// (Response) Array of SystemCodes which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of SystemCodes which get by uuid array.")]
        public IList<SystemCodesDataDto> SystemCodess { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class SystemCodesPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class SystemCodesPayloadFind : FilterPayloadBase<SystemCodesFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> SystemCodesList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int SystemCodesListCount { get; set; }
        
        public static SystemCodesPayloadFind GetSampleData()
        {
            var data = new SystemCodesPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = SystemCodesFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class SystemCodesFilter
    {
        //public string City { get; set; }

        public static Faker<SystemCodesFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<SystemCodesFilter>()
                //.RuleFor(u => u.City, f => "")
                ;
            #endregion faker data rules
        }
    }

}

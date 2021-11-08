    
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
    public class InitNumbersPayloadAdd
    {
        /// <summary>
        /// (Request Data) InitNumbers object to add.
        /// (Response Data) InitNumbers object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InitNumbers object to add.")]
        public InitNumbersDataDto InitNumbers { get; set; }
        
        public static InitNumbersPayloadAdd GetSampleData()
        {
            var data = new InitNumbersPayloadAdd();
            data.InitNumbers = new InitNumbersDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InitNumbersPayloadUpdate
    {
        /// <summary>
        /// (Request Data) InitNumbers object to update.
        /// (Response Data) InitNumbers object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InitNumbers object to update.")]
        public InitNumbersDataDto InitNumbers { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class InitNumbersPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) InitNumbers object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InitNumbers object to get.")]
        public InitNumbersDataDto InitNumbers { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class InitNumbersPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple InitNumberss.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple InitNumberss.")]
        public IList<string> InitNumbersUuids { get; set; }

        /// <summary>
        /// (Response) Array of InitNumbers which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of InitNumbers which get by uuid array.")]
        public IList<InitNumbersDataDto> InitNumberss { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class InitNumbersPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InitNumbersPayloadFind : FilterPayloadBase<InitNumbersFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> InitNumbersList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int InitNumbersListCount { get; set; }
        
        public static InitNumbersPayloadFind GetSampleData()
        {
            var data = new InitNumbersPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = InitNumbersFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class InitNumbersFilter
    {
        public string InitNumbersUuid { get; set; }
        public string CustomerUuid { get; set; }
        public string Type { get; set; }

        public static Faker<InitNumbersFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<InitNumbersFilter>()
                .RuleFor(u => u.InitNumbersUuid, f => "")
                 .RuleFor(u => u.CustomerUuid, f => "")
                  .RuleFor(u => u.Type, f => "")
                ;
            #endregion faker data rules
        }
    }

}


    
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
using DigitBridge.QuickBooks.Integration;

namespace DigitBridge.CommerceCentral.ERPFuncApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadAdd
    {
        /// <summary>
        /// (Request Data) QuickBooksSettingInfo object to add.
        /// (Response Data) QuickBooksSettingInfo object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) QuickBooksSettingInfo object to add.")]
        public QuickBooksSettingInfoDataDto QuickBooksSettingInfo { get; set; }
        
        public static QuickBooksSettingInfoPayloadAdd GetSampleData()
        {
            var data = new QuickBooksSettingInfoPayloadAdd();
            data.QuickBooksSettingInfo = new QuickBooksSettingInfoDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadUpdate
    {
        /// <summary>
        /// (Request Data) QuickBooksSettingInfo object to update.
        /// (Response Data) QuickBooksSettingInfo object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) QuickBooksSettingInfo object to update.")]
        public QboIntegrationSetting SettingInfo { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) QuickBooksSettingInfo object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) QuickBooksSettingInfo object to get.")]
        public QboIntegrationSetting SettingInfo { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple QuickBooksSettingInfos.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple QuickBooksSettingInfos.")]
        public IList<string> SettingUuids { get; set; }

        /// <summary>
        /// (Response) Array of QuickBooksSettingInfo which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of QuickBooksSettingInfo which get by uuid array.")]
        public IList<QuickBooksSettingInfoDataDto> QuickBooksSettingInfos { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class QuickBooksSettingInfoPayloadFind : FilterPayloadBase<QuickBooksSettingInfoFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> QuickBooksSettingInfoList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int QuickBooksSettingInfoListCount { get; set; }
        
        public static QuickBooksSettingInfoPayloadFind GetSampleData()
        {
            var data = new QuickBooksSettingInfoPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = QuickBooksSettingInfoFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class QuickBooksSettingInfoFilter
    {
        //public string City { get; set; }

        public static Faker<QuickBooksSettingInfoFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<QuickBooksSettingInfoFilter>()
                //.RuleFor(u => u.City, f => "")
                ;
            #endregion faker data rules
        }
    }

}


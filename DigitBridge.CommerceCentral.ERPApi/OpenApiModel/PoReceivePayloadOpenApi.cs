    
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
    public class PoReceivePayloadAdd
    {
        /// <summary>
        /// (Request Data) PoReceive object to add.
        /// (Response Data) PoReceive object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) PoReceive object to add.")]
        public PoTransactionDataDto PoTransaction { get; set; }
        
        public static PoReceivePayloadAdd GetSampleData()
        {
            var data = new PoReceivePayloadAdd();
            data.PoTransaction = new PoTransactionDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class PoReceivePayloadUpdate
    {
        /// <summary>
        /// (Request Data) PoReceive object to update.
        /// (Response Data) PoReceive object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) PoReceive object to update.")]
        public PoTransactionDataDto PoTransaction { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class PoReceivePayloadGetSingle
    {
        /// <summary>
        /// (Response Data) PoReceive object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) PoReceive object to get.")]
        public PoTransactionDataDto PoTransaction { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class PoReceivePayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple PoReceives.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple PoReceives.")]
        public IList<string> TransUuids { get; set; }

        /// <summary>
        /// (Response) Array of PoReceive which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of PoReceive which get by uuid array.")]
        public IList<PoTransactionDataDto> PoTransactions { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class PoReceivePayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class PoReceivePayloadFind : FilterPayloadBase<PoReceiveFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> PoTransactionList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int PoTransactionListCount { get; set; }
        
        public static PoReceivePayloadFind GetSampleData()
        {
            var data = new PoReceivePayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = PoReceiveFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class PoReceiveFilter
    {
        //public string City { get; set; }

        public static Faker<PoReceiveFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<PoReceiveFilter>()
                //.RuleFor(u => u.City, f => "")
                ;
            #endregion faker data rules
        }
    }

}


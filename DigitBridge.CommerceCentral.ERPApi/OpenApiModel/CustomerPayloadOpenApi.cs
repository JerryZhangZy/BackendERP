
    
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
    public class CustomerPayloadAdd
    {
        /// <summary>
        /// (Request Data) Customer object to add.
        /// (Response Data) Customer object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Customer object to add.")]
        public CustomerDataDto Customer { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class CustomerPayloadUpdate
    {
        /// <summary>
        /// (Request Data) Customer object to update.
        /// (Response Data) Customer object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Customer object to update.")]
        public CustomerDataDto Customer { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class CustomerPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) Customer object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Customer object to get.")]
        public CustomerDataDto Customer { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class CustomerPayloadGetMultiple
    {
        /// <summary>
        /// (Request Parameter) Array of customer code to load multiple Customer dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of customer code to load multiple Customer dto data.")]
        public IList<string> CustomerCodes { get; set; }

        /// <summary>
        /// (Request) Array of uuid to get multiple Customers.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple Customers.")]
        public IList<string> CustomerUuids { get; set; }

        /// <summary>
        /// (Response) Array of Customer which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of Customer which get by uuid array.")]
        public IList<CustomerDataDto> Customers { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class CustomerPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class CustomerPayloadFind : FilterPayloadBase<CustomerFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> CustomerList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int CustomerListCount { get; set; }

    }


    public class CustomerFilter
    {
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string Area { get; set; }

        public string Region { get; set; }

        public string ShippingCarrier { get; set; }
    }
}

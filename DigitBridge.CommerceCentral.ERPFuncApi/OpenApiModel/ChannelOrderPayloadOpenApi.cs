
    
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

namespace DigitBridge.CommerceCentral.ERPFuncApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadAdd
    {
        /// <summary>
        /// (Request Data) ChannelOrder object to add.
        /// (Response Data) ChannelOrder object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) ChannelOrder object to add.")]
        public ChannelOrderDataDto ChannelOrder { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadUpdate
    {
        /// <summary>
        /// (Request Data) ChannelOrder object to update.
        /// (Response Data) ChannelOrder object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) ChannelOrder object to update.")]
        public ChannelOrderDataDto ChannelOrder { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) ChannelOrder object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) ChannelOrder object to get.")]
        public ChannelOrderDataDto ChannelOrder { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple ChannelOrders.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple ChannelOrders.")]
        public IList<string> CentralOrderUuids { get; set; }

        /// <summary>
        /// (Response) Array of ChannelOrder which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of ChannelOrder which get by uuid array.")]
        public IList<ChannelOrderDataDto> ChannelOrders { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class ChannelOrderPayloadFind : PayloadBase
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> ChannelOrderList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int ChannelOrderListCount { get; set; }

    }

}


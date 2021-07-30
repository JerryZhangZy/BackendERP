
    
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
    public class OrderShipmentPayloadAdd
    {
        /// <summary>
        /// (Request Data) OrderShipment object to add.
        /// (Response Data) OrderShipment object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) OrderShipment object to add.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadUpdate
    {
        /// <summary>
        /// (Request Data) OrderShipment object to update.
        /// (Response Data) OrderShipment object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) OrderShipment object to update.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) OrderShipment object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) OrderShipment object to get.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple OrderShipments.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple OrderShipments.")]
        public IList<string> OrderShipmentUuids { get; set; }

        /// <summary>
        /// (Response) Array of OrderShipment which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of OrderShipment which get by uuid array.")]
        public IList<OrderShipmentDataDto> OrderShipments { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadFind : PayloadBase
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> OrderShipmentList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int OrderShipmentListCount { get; set; }

    }

}


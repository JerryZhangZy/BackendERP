
    
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
    public class InventoryLogPayloadAdd
    {
        /// <summary>
        /// (Request Data) InventoryLog object to add.
        /// (Response Data) InventoryLog object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InventoryLog object to add.")]
        public InventoryLogDataDto InventoryLog { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InventoryLogPayloadUpdate
    {
        /// <summary>
        /// (Request Data) InventoryLog object to update.
        /// (Response Data) InventoryLog object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InventoryLog object to update.")]
        public InventoryLogDataDto InventoryLog { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class InventoryLogPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) InventoryLog object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InventoryLog object to get.")]
        public InventoryLogDataDto InventoryLog { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class InventoryLogPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple InventoryLogs.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple InventoryLogs.")]
        public IList<string> InventoryLogUuids { get; set; }

        /// <summary>
        /// (Response) Array of InventoryLog which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of InventoryLog which get by uuid array.")]
        public IList<InventoryLogDataDto> InventoryLogs { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class InventoryLogPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InventoryLogPayloadFind : FilterPayloadBase<InventoryLogFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> InventoryLogList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int? InventoryLogListCount { get; set; }

    }

    [Serializable()]
    public class InventoryLogFilter
    {

        public string ProductUuid { get; set; }

        public string SKU { get; set; }

        public string Brand { get; set; }

        public string Manufacturer { get; set; }

        public string ProductTitle { get; set; }

        public string FNSku { get; set; }

        public string UPC { get; set; }

        public string WarehouseCode { get; set; }

        public string LotNum { get; set; }

        public string LpnNum { get; set; }
    }

}


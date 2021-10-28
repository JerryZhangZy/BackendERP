
    
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
    public class InvoiceTransactionPayloadAdd
    {
        /// <summary>
        /// (Request Data) InvoiceTransaction object to add.
        /// (Response Data) InvoiceTransaction object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InvoiceTransaction object to add.")]
        public InvoiceTransactionDataDto InvoiceTransaction { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InvoiceTransactionPayloadUpdate
    {
        /// <summary>
        /// (Request Data) InvoiceTransaction object to update.
        /// (Response Data) InvoiceTransaction object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InvoiceTransaction object to update.")]
        public InvoiceTransactionDataDto InvoiceTransaction { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class InvoiceTransactionPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) InvoiceTransaction object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) InvoiceTransaction object to get.")]
        public InvoiceTransactionDataDto InvoiceTransaction { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class InvoiceTransactionPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple InvoiceTransactions.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple InvoiceTransactions.")]
        public IList<string> TransUuids { get; set; }

        /// <summary>
        /// (Response) Array of InvoiceTransaction which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of InvoiceTransaction which get by uuid array.")]
        public IList<InvoiceTransactionDataDto> InvoiceTransactions { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class InvoiceTransactionPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InvoiceTransactionPayloadFind : PayloadBase
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> InvoiceTransactionList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int InvoiceTransactionListCount { get; set; }

    }

}



//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Intuit.Ipp.Data;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class QboInvoicePayloadSave
    {

    }
    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class QboInvoicePayloadGetMultiple
    {
        /// <summary>
        /// (Response) Array of QboInvoice which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of QboInvoice which get by erp invoice number.")]
        public IList<Invoice> QboInvoices { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class QboInvoicePayloadDelete
    {
    }
}


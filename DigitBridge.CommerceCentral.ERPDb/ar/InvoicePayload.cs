
    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class InvoicePayload : PayloadBase
    {
        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "invoiceNumbers", val => InvoiceNumbers = val.Split(",").Distinct().ToList() }
            };
        }


        #region multiple Dto list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple Invoice dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of uuid to load multiple Invoice dto data.")]
        public IList<string> InvoiceNumbers { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasInvoiceNumbers => InvoiceNumbers != null && InvoiceNumbers.Count > 0;
        public bool ShouldSerializeInvoiceUuids() => HasInvoiceNumbers;

        /// <summary>
        /// (Response Data) Array of Invoice entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by uuid array.")]
        public IList<InvoiceDataDto> Invoices { get; set; }
        [JsonIgnore] public virtual bool HasInvoices => Invoices != null && Invoices.Count > 0;
        public bool ShouldSerializeInvoices() => HasInvoices;

        #endregion multiple Dto list


        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single Invoice entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by Number.")]
        public InvoiceDataDto Invoice { get; set; }
        [JsonIgnore] public virtual bool HasInvoice => Invoice != null;
        public bool ShouldSerializeInvoice() => HasInvoice;

        #endregion single Dto object


        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder InvoiceList { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceList => InvoiceList != null && InvoiceList.Length > 0;
        public bool ShouldSerializeInvoiceList() => HasInvoiceList;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int InvoiceListCount { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceListCount => InvoiceListCount > 0;
        public bool ShouldSerializeInvoiceListCount() => HasInvoiceListCount;

        #endregion list service


        #region summary service 

        [OpenApiPropertyDescription("(Response Data) summary result which load filter")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder InvoiceSummary { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceSummary => InvoiceSummary != null;
        public bool ShouldSerializeInvoiceSummary() => HasInvoiceSummary;
        #endregion
    }
}




using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class QboInvoicePayload : PayloadBase
    {
        #region multiple Dto list 
        /// <summary>
        /// (Response Data) Array of QboInvoice entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by invoice number.")]
        public IList<Invoice> QboInvoices { get; set; }
        [JsonIgnore] public virtual bool HasQboInvoices => QboInvoices != null && QboInvoices.Count > 0;
        public bool ShouldSerializeQboInvoices() => HasQboInvoices;

        #endregion multiple Dto list

        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single Invoice entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by erp invoice number.")]
        public Invoice QboInvoice { get; set; }
        [JsonIgnore] public virtual bool HasQboInvoice => QboInvoice != null;
        public bool ShouldSerializeQboInvoice() => HasQboInvoice;

        #endregion single Dto object

    }
}


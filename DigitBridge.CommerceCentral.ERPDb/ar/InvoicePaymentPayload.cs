

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
    public class InvoicePaymentPayload : InvoiceTransactionPayload
    {
        #region Set transtype to payment
        /// <summary>
        /// always load transtype=TransTypeEnum.Payment for all query
        /// </summary>
        [JsonIgnore]
        public override int TransType => (int)TransTypeEnum.Payment;

        #endregion 

        public InvoiceHeaderDto InvoiceHeader { get; set; }

        #region summary service 

        [OpenApiPropertyDescription("(Response Data) summary result which load filter")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder InvoicePaymentSummary { get; set; }
        [JsonIgnore] public virtual bool HasInvoicePaymentSummary => InvoicePaymentSummary != null;
        public bool ShouldSerializeInvoicePaymentSummary() => HasInvoicePaymentSummary;
        #endregion

        /// <summary>
        /// Delegate function to load request parameter to payload property.
        /// </summary>
        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "applyInvoices", val => ApplyInvoices = val.JsonToObject<List<ApplyInvoice>>()}
            };
        }

        #region multiple payment list

        /// <summary>
        /// (Request Parameter) Array of uuid to load multiple Invoice dto data.
        /// </summary>
        [OpenApiPropertyDescription("(Request Parameter) Array of applyInvoice  to add multi payments")]
        public IList<ApplyInvoice> ApplyInvoices { get; set; } = new List<ApplyInvoice>();
        [JsonIgnore]
        public virtual bool HasApplyInvoices => ApplyInvoices != null && ApplyInvoices.Count > 0;
        public bool ShouldSerializeApplyInvoices() => HasApplyInvoices;

        #endregion
    }
}


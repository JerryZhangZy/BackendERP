

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
    public class ApNewPaymentPayload : PayloadBase
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder ApInvoiceList { get; set; }
        [JsonIgnore] public virtual bool HasApInvoiceList => ApInvoiceList != null && ApInvoiceList.Length > 0;
        public bool ShouldSerializeApInvoiceList() => HasApInvoiceList;
        #endregion

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int ApInvoiceListCount { get; set; }
        [JsonIgnore] public virtual bool HasApInvoiceListCount => ApInvoiceListCount > 0;
        public bool ShouldSerializeApInvoiceListCount() => HasApInvoiceListCount;

        public IList<ApplyInvoice> ApplyInvoices { get; set; } = new List<ApplyInvoice>();
        [JsonIgnore]
        public virtual bool HasApplyInvoices => ApplyInvoices != null && ApplyInvoices.Count > 0;
        public bool ShouldSerializeApplyInvoices() => HasApplyInvoices;

        public IList<ApInvoiceListForPayment> Payments { get; set; } = new List<ApInvoiceListForPayment>();
        [JsonIgnore]
        public virtual bool HasPayments => Payments != null && Payments.Count > 0;
        public bool ShouldSerializePayments() => HasPayments;

        #region payment

        public ApInvoiceTransactionDto ApTransaction { get; set; }
        public bool HasApInvoiceTransaction => ApTransaction != null;
        #endregion
    }
}


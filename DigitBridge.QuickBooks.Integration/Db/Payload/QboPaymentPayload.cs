

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
    public class QboPaymentPayload : PayloadBase
    {
        #region multiple Dto list 
        /// <summary>
        /// (Response Data) Array of QboPayment entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by Payment number.")]
        public IList<Payment> QboPayments { get; set; }
        [JsonIgnore] public virtual bool HasQboPayments => QboPayments != null && QboPayments.Count > 0;
        public bool ShouldSerializeQboPayments() => HasQboPayments;

        #endregion multiple Dto list

        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single Payment entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by erp Payment number.")]
        public Payment QboPayment { get; set; }
        [JsonIgnore] public virtual bool HasQboPayment => QboPayment != null;
        public bool ShouldSerializeQboPayment() => HasQboPayment;

        #endregion single Dto object

    }
}


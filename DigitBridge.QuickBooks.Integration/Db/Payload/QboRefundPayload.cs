

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
    public class QboRefundPayload : PayloadBase
    {
        #region multiple Dto list 
        /// <summary>
        /// (Response Data) Array of QboRefund entity object which load by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Array of entity object which load by invoice number and tran number.")]
        public IList<RefundReceipt> QboRefunds { get; set; }
        [JsonIgnore] public virtual bool HasQboRefunds => QboRefunds != null && QboRefunds.Count > 0;
        public bool ShouldSerializeQboRefunds() => HasQboRefunds;

        #endregion multiple Dto list

        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single Refund entity object which load by Number.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) Single entity object which load by erp invoice number and tran number.")]
        public RefundReceipt QboRefund { get; set; }
        [JsonIgnore] public virtual bool HasQboRefund => QboRefund != null;
        public bool ShouldSerializeQboRefund() => HasQboRefund;

        #endregion single Dto object

    }
}


using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class InvoicePaymentPayload : PayloadBase
    {
        public InvoiceTransactionDto InvoiceTransaction { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceTransaction => InvoiceTransaction != null;
        public bool ShouldSerializeInvoiceTransaction() => HasInvoiceTransaction;


        public InvoiceHeaderDto InvoiceHeader { get; set; }
        [JsonIgnore] public virtual bool HasInvoiceHeader => InvoiceHeader != null;
        public bool ShouldSerializeInvoiceHeader() => HasInvoiceHeader;
    }
}

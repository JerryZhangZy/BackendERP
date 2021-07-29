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
    public class InvoicePayload : PayloadBase
    {
        public InvoiceDataDto Invoice { get; set; }
        [JsonIgnore] public virtual bool HasInvoice => Invoice != null;
        public bool ShouldSerializeInvoice() => HasInvoice;
         
    }
}

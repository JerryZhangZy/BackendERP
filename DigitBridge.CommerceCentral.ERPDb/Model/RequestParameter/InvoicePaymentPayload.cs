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
        public InvoiceTransactionDto Dto { get; set; }

        public InvoiceHeaderDto InvoiceHeader { get; set; }
    }
}

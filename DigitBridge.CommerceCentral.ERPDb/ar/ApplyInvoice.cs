using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class ApplyInvoice
    {
        public string InvoiceUuid { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// This is used for update trans.
        /// </summary>
        public long? TransRowNum { get; set; }

        [JsonIgnore]
        public bool Success { get; set; } = true;

        [JsonIgnore]
        public string TransUuid { get; set; }
    }
}

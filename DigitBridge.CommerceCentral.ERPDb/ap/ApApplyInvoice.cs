using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class ApApplyInvoice
    {
        public string ApInvoiceUuid { get; set; }
        public string ApInvoiceNum { get; set; }
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

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
        /// <summary>
        /// This is used for update trans.
        /// </summary>
        public long? TransRowNum { get; set; }
        public string TransUuid { get; set; }
        public int TransNum { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string QuickbookDocNum { get; set; }
        public decimal InvoiceTotalAmount { get; set; }
        public decimal InvoicePaidAmount { get; set; }
        public decimal InvoiceBalance { get; set; }
        public decimal PaidAmount { get; set; }
        [JsonIgnore]
        public bool Success { get; set; } = false;

    }
}

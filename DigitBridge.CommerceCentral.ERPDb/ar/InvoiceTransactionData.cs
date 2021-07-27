


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InvoiceTransactionData
    {
        /// <summary>
        /// Get invoiceTransaction by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns> 
        public virtual async Task<InvoiceTransaction> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return (await dbFactory.FindAsync<InvoiceTransaction>($"SELECT TOP 1 * FROM InvoiceTransaction where InvoiceNumber='{invoiceNumber}'")).FirstOrDefault();
        }

    }
}




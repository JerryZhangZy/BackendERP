



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
    public partial class InvoiceHeader
    {
        /// <summary>
        /// get InvoiceHeader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<InvoiceHeader> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            return (await dbFactory.FindAsync<InvoiceHeader>($"SELECT TOP 1 * FROM InvoiceHeader where InvoiceNumber='{invoiceNumber}'")).FirstOrDefault();
        }
    }
}




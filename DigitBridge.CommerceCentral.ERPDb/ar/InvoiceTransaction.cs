
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InvoiceTransaction
    {
        /// <summary>
        /// Get invoiceTransaction by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns> 
        public virtual async Task<InvoiceTransaction> GetByInvoiceNumberAsync(string invoiceNumber, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<InvoiceTransaction>($"SELECT TOP 1 * FROM InvoiceTransaction where InvoiceNumber='{invoiceNumber}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }

        /// <summary>
        /// Get invoiceTransaction by rownum 
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public virtual async Task<InvoiceTransaction> GetByRowNumAsync(long rowNum, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<InvoiceTransaction>($"SELECT TOP 1 * FROM InvoiceTransaction where RowNum={rowNum} and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }
    }
}




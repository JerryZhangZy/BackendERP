


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
    public partial class InvoiceData
    {
        /// <summary>
        /// Get row num by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<long?> GetRowNumAsync(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            return await dbFactory.GetValueAsync<InvoiceHeader, long?>($"SELECT TOP 1 RowNum FROM InvoiceHeader where InvoiceNumber='{invoiceNumber}' and profileNum={profileNum} and masterAccountNum={masterAccountNum}");
        }
        /// <summary>
        /// Get row num by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual long? GetRowNum(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            return dbFactory.GetValue<InvoiceHeader, long?>($"SELECT TOP 1 RowNum FROM InvoiceHeader where InvoiceNumber='{invoiceNumber}' and profileNum={profileNum} and masterAccountNum={masterAccountNum}");
        }
    }
}




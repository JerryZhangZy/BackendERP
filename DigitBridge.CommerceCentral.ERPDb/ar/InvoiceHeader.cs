



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
        public decimal TotalLineCommissionAmount { get; set; }

        public override IList<string> IgnoreUpdateColumns()
        {
            return new List<string> { 
                "PaidAmount",
                "CreditAmount"
            };
        }
        /// <summary>
        /// get InvoiceHeader by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<InvoiceHeader> GetByInvoiceNumberAsync(string invoiceNumber, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<InvoiceHeader>($"SELECT TOP 1 * FROM InvoiceHeader where InvoiceNumber='{invoiceNumber}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }

        /// <summary>
        /// get invoices of customer
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns></returns>
        public async Task<List<InvoiceHeader>> GetByCustomerCodeAsync(string customerCode, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<InvoiceHeader>($"SELECT * FROM InvoiceHeader where InvoiceStatus=1 and CustomerCode='{customerCode}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).ToList();
        }
    }
}




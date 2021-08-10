


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
        public virtual async Task<InvoiceTransaction> GetInvoiceTransaction(string invoiceNumber, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<InvoiceTransaction>($"SELECT TOP 1 * FROM InvoiceTransaction where InvoiceNumber='{invoiceNumber}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }


        public virtual async Task<bool> GetAsync(string invoiceNumber, int masterAccountNum, int profileNum, bool loadOthers = true)
        {
            var obj = await GetInvoiceTransaction(invoiceNumber, masterAccountNum, profileNum);
            if (obj is null) return false;
            InvoiceTransaction = obj;
            if (loadOthers)
                GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}




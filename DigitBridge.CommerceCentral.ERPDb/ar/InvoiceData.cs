


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;

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

        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM InvoiceHeader
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND InvoiceNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj = dbFactory.GetBy<InvoiceHeader>(sql, paras);
            if (obj is null) return false;
            InvoiceHeader = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM InvoiceHeader
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND InvoiceNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj =await dbFactory.GetByAsync<InvoiceHeader>(sql, paras);
            if (obj is null) return false;
            InvoiceHeader = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
         
    }
}






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
    public partial class VendorData
    {
        /// <summary>
        /// Get vendor by vendorCode.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM Vendor
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND VendorCode = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj = dbFactory.GetBy<Vendor>(sql, paras);
            if (obj is null) return false;
            Vendor = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }

        /// <summary>
        /// Get vendor by vendorCode.
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM Vendor
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND VendorCode = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj = await dbFactory.GetByAsync<Vendor>(sql, paras);
            if (obj is null) return false;
            Vendor = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}






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
    public partial class InitNumbersData
    {
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string initNumbersUuid)
        {
            var sql = @"
SELECT TOP 1 * FROM InitNumbers
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND initNumbersUuid = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",initNumbersUuid)
            };

            var obj = await dbFactory.GetByAsync<InitNumbers>(sql, paras);
            if (obj is null) return false;
            // InitNumbers
            InitNumbers = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}




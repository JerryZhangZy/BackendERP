


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
    public partial class OrderShipmentData
    {
        public async Task<long?> GetOrderShipmentNumAsync(long orderShipmentNum, int profileNum, int masterAccountNum)
        {
            return await dbFactory.GetValueAsync<OrderShipmentHeader, long?>($"SELECT TOP 1 orderShipmentNum FROM OrderShipmentHeader where orderShipmentNum={orderShipmentNum} and profileNum={profileNum} and masterAccountNum={masterAccountNum}");
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM OrderShipmentHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderShipmentNum = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number.ToLong())
            };

            var obj = await dbFactory.GetByAsync<OrderShipmentHeader>(sql, paras);
            if (obj is null) return false;
            OrderShipmentHeader = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM OrderShipmentHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderShipmentNum = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number.ToLong())
            };

            var obj = dbFactory.GetBy<OrderShipmentHeader>(sql, paras);
            if (obj is null) return false;
            OrderShipmentHeader = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
    }
}




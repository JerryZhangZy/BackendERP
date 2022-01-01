    

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
    public partial class PurchaseOrderData
    {

        public override void CheckIntegrityOthers()
        {
            foreach (var child in PoItemsRef.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.DatabaseNum != PoHeader.DatabaseNum)
                    child.DatabaseNum = PoHeader.DatabaseNum;
                if (child.MasterAccountNum != PoHeader.MasterAccountNum)
                    child.MasterAccountNum = PoHeader.MasterAccountNum;
                if (child.ProfileNum != PoHeader.ProfileNum)
                    child.ProfileNum = PoHeader.ProfileNum;
            }

        }

       
        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM PoHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND PoNum = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj = dbFactory.GetBy<PoHeader>(sql, paras);
            if (obj is null) return false;
            PoHeader = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM PoHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND PoNum = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };
            var obj = await dbFactory.GetByAsync<PoHeader>(sql, paras);
            if (obj is null) return false;
            PoHeader = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }

        public async Task<bool> GetAllPoNumAsync(int masterAccountNum, int profileNum)
        {

            var sql = @"
                        select  distinct ph.[PoNum] from [dbo].[PoHeader] ph  LEFT JOIN  [dbo].[PoItems] poi on ph.PoUuid=poi.PoUuid where  (poi.PoQty-poi.ReceivedQty-poi.CancelledQty)>0 and ph.MasterAccountNum=@0 and ph.ProfileNum=@1";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum)
            };
            var obj = await dbFactory.GetByAsync<PoHeader>(sql, paras);
            if (obj is null) return false;
         
            return true;
        }
    }
}





    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InventoryData
    {
        public override void CheckIntegrityOthers()
        {
            if (ProductExt != null)
            {
                if (ProductExt.DatabaseNum != ProductBasic.DatabaseNum)
                    ProductExt.DatabaseNum = ProductBasic.DatabaseNum;
                if (ProductExt.MasterAccountNum != ProductBasic.MasterAccountNum)
                    ProductExt.MasterAccountNum = ProductBasic.MasterAccountNum;
                if (ProductExt.ProfileNum != ProductBasic.ProfileNum)
                    ProductExt.ProfileNum = ProductBasic.ProfileNum;
                if (ProductExt.SKU != ProductBasic.SKU)
                    ProductExt.SKU = ProductBasic.SKU;
            }
            foreach (var child in Inventory.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.DatabaseNum != ProductBasic.DatabaseNum)
                    child.DatabaseNum = ProductBasic.DatabaseNum;
                if (child.MasterAccountNum != ProductBasic.MasterAccountNum)
                    child.MasterAccountNum = ProductBasic.MasterAccountNum;
                if (child.ProfileNum != ProductBasic.ProfileNum)
                    child.ProfileNum = ProductBasic.ProfileNum;
                if (child.SKU != ProductBasic.SKU)
                    child.SKU = ProductBasic.SKU;
            }

        }
        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        { 
            var sql = @"
SELECT TOP 1 * FROM ProductBasic
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND SKU = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };
            var obj = dbFactory.GetBy<ProductBasic>(sql, paras);

            if (obj is null) return false;
            ProductBasic = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        } 
    }
}




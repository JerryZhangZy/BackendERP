
    

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

        public virtual Inventory FindInventoryByWarhouse(string warehouseCode)
        {
            if (this.Inventory == null || this.Inventory.Count == 0)
                return null;
            return this.Inventory.FindByWarehouseCode(warehouseCode);
        }

        public virtual async Task<bool> GetBySkuWarhouseAsync(string sku, IList<string> warehouseCode, int masterAccountNum, int profileNum)
        {
            if (string.IsNullOrEmpty(sku) || warehouseCode == null || warehouseCode.Count == 0)
                return false;

            ProductBasic = await GetProductBasicBySkuAsync(sku, masterAccountNum, profileNum);
            if (this.ProductBasic is null || string.IsNullOrEmpty(ProductBasic.ProductUuid)) return false;
            var productUuid = ProductBasic.ProductUuid;

            ProductExt = await GetProductExtByProductUuidAsync(productUuid);
            ProductExtAttributes = await GetProductExtAttributesByProductUuidAsync(productUuid);
            Inventory = await GetInventoryByIdWarehouseAsync(productUuid, warehouseCode);
            InventoryAttributes = await GetInventoryAttributesByProductUuidAsync(productUuid);

            if (_OnAfterLoad != null)
                _OnAfterLoad(this);

            return true;
        }

        public virtual async Task<ProductBasic> GetProductBasicBySkuAsync(string sku, int masterAccountNum, int profileNum)
        {
            if (string.IsNullOrEmpty(sku))
                return null;

            var sql = "WHERE MasterAccountNum = @0 AND ProfileNum = @1 AND SKU = @2";
            return dbFactory.GetBy<ProductBasic>(sql, masterAccountNum, profileNum, sku);
        }

        public virtual async Task<IList<Inventory>> GetInventoryByIdWarehouseAsync(string productUuid, IList<string> warehouseCode)
        {
            if (string.IsNullOrEmpty(productUuid) || warehouseCode == null || warehouseCode.Count == 0)
                return null;

            var sql = @"
SELECT * FROM Inventory
WHERE ProductUuid = @0 AND WarehouseCode IN @1 
";

            var paras = new SqlParameter[]
            {
                (SqlParameter)productUuid.ToSqlParameter("@0"),
                (SqlParameter)warehouseCode.JoinToSqlInStatementString().ToSqlParameter("@1")
            };
            return (await dbFactory.FindAsync<Inventory>(sql, paras)).ToList();
        }

        public virtual async Task<IList<Inventory>> GetInventoryBySkuWarehouseAsync(string sku, IList<string> warehouseCode, int masterAccountNum, int profileNum)
        {
            if (string.IsNullOrEmpty(sku) || warehouseCode == null || warehouseCode.Count == 0)
                return null;

            var sql = @"
SELECT * FROM Inventory
WHERE sku = @0 AND WarehouseCode IN @1 
AND MasterAccountNum = @2 AND profileNum = @3
";

            var paras = new SqlParameter[]
            {
                (SqlParameter)sku.ToSqlParameter("@0"),
                (SqlParameter)warehouseCode.JoinToSqlInStatementString().ToSqlParameter("@1"),
                (SqlParameter)masterAccountNum.ToSqlParameter("@2"),
                (SqlParameter)profileNum.ToSqlParameter("@3")
            };
            return (await dbFactory.FindAsync<Inventory>(sql, paras)).ToList();
        }

    }
}





    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;
using Microsoft.Data.SqlClient;

using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.Base.Utility.Model;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a InventoryHelper SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InventoryServiceHelper
    {
        public static bool ExistNumber(string number, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @number
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
        }

        public static async Task<bool> ExistNumberAsync(string number, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
        }

        public static async Task<bool> ExistNumberInExtAsync(string number, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductExt tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
        }

        public static bool ExistNumberInExt(string number, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductExt tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @number
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
        }

        public static bool ExistId(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND ProductUuid = @uuid
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                uuid.ToSqlParameter("uuid")
            );
            return result > 0;
        }

        public static async Task<bool> ExistIdAsync(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND ProductUuid = @uuid
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                uuid.ToSqlParameter("uuid")
            );
            return result > 0;
        }

        public static bool ExistRowNum(long rowNum, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CentralProductNum= @rowNum
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                rowNum.ToSqlParameter("rowNum")
            );
            return result > 0;
        }

        public static async Task<bool> ExistRowNumAsync(long rowNum, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CentralProductNum= @rowNum
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                rowNum.ToSqlParameter("rowNum")
            );
            return result > 0;
        }

        public static long GetRowNumBySku(string sku, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT Top 1 CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @sku
";
            var result = SqlQuery.ExecuteScalar<long>(sql, CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                sku.ToSqlParameter("sku")
            );
            return result;
        }

        public static async Task<long> GetRowNumBySkuAsync(string sku, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT Top 1 CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SKU = @sku
";
            var result = await SqlQuery.ExecuteScalarAsync<long>(sql, CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                sku.ToSqlParameter("sku")
            );
            return result;
        }

        public static List<long> GetRowNums(int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return SqlQuery.Execute(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }

        public static async Task<List<long>> GetRowNumsAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return await SqlQuery.ExecuteAsync(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }
        public static List<(string, string)> GetProductUuidsByInventoryUuids(IList<string> inventoryUuids, int masterAccountNum, int profileNum)
        {
            if (inventoryUuids == null || inventoryUuids.Count == 0)
                return new List<(string, string)>();
            var sql = $@"
SELECT InventoryUuid,ProductUuid FROM Inventory tbl
WHERE MasterAccountNum=@masterAccountNum
AND ProfileNum=@pofileNum
AND (EXISTS (SELECT * FROM @InventoryUuid _InventoryUuid WHERE _InventoryUuid.item = COALESCE([InventoryUuid],'')))";

            return SqlQuery.Execute(
                sql,
                (string inventoryUuid, string productUuid) => (inventoryUuid, productUuid),
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                inventoryUuids.ToParameter<string>("InventoryUuid"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skus"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns>Tuple List,(RowNum,UniqueId,Code)</returns>
        public static List<(long, string, string)> GetKeyInfoBySkus(IList<string> skus, int masterAccountNum, int profileNum)
        {
            if (skus == null || skus.Count == 0)
                return new List<(long, string, string)>();
            var sql = $@"
SELECT CentralProductNum,ProductUuid,SKU FROM ProductBasic tbl
WHERE MasterAccountNum=@masterAccountNum
AND ProfileNum=@pofileNum
AND (EXISTS (SELECT * FROM @SKU _SKU WHERE _SKU.item = COALESCE([SKU],'')))";

            return SqlQuery.Execute(
                sql,
                (long rowNum, string customerUuid, string customerCode) => (rowNum, customerUuid, customerCode),
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                skus.ToParameter<string>("SKU"));
        }

        public static async Task<List<(long, string, string)>> GetKeyInfoBySkusAsync(IList<string> skus, int masterAccountNum, int profileNum)
        {
            if (skus == null || skus.Count == 0)
                return new List<(long, string, string)>();
            var sql = $@"
SELECT CentralProductNum,ProductUuid,SKU FROM ProductBasic tbl
WHERE MasterAccountNum=@masterAccountNum
AND ProfileNum=@pofileNum
AND (EXISTS (SELECT * FROM @SKU _SKU WHERE _SKU.item = COALESCE([SKU],'')))";

            return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum, string customerUuid, string customerCode) => (rowNum, customerUuid, customerCode),
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                skus.ToParameter<string>("SKU"));
        }

        public static List<long> GetRowNumsBySkus(IList<string> skus, int masterAccountNum, int profileNum)
        {
            if (skus == null || skus.Count == 0)
                return new List<long>();
            var sql = $@"
SELECT CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum=@masterAccountNum
AND ProfileNum=@pofileNum
AND (EXISTS (SELECT * FROM @SKU _SKU WHERE _SKU.item = COALESCE([SKU],'')))";

            return SqlQuery.Execute(
                sql,
                (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                skus.ToParameter<string>("SKU"));
        }

        public static async Task<List<long>> GetRowNumsBySkusAsync(IList<string> skus, int masterAccountNum, int profileNum)
        {
            if (skus == null || skus.Count == 0)
                return new List<long>();
            var sql = $@"
SELECT CentralProductNum FROM ProductBasic tbl
WHERE MasterAccountNum=@masterAccountNum
AND ProfileNum=@pofileNum
AND (EXISTS (SELECT * FROM @SKU _SKU WHERE _SKU.item = COALESCE([SKU],'')))";

            return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                skus.ToParameter<string>("SKU"));
        }

        public static List<StringArray> ExistInventoryBySkuWithWarehouseCodes(List<StringArray> param, int masterAccountNum, int profileNum)
        {
            var rlist = new List<StringArray>(0);
            if (param == null || param.Count == 0)
                return rlist;

            var result = GetInventoryKeyInfoBySkuWithWarehouseCodes(param, masterAccountNum, profileNum);
            if (result.Count == 0)
                return param;
            if (result.Count == param.Count)
                return rlist;
            foreach (var item in param)
            {
                if (!result.Exists(r => r.Item0 == item.Item0 && r.Item1 == item.Item1))
                    rlist.Add(item);
            }
            return rlist;
        }

        public static async Task<List<StringArray>> ExistInventoryBySkuWithWarehouseCodesAsync(List<StringArray> param, int masterAccountNum, int profileNum)
        {
            var rlist = new List<StringArray>(0);
            if (param == null || param.Count == 0)
                return rlist;

            var result = await GetInventoryKeyInfoBySkuWithWarehouseCodesAsync(param, masterAccountNum, profileNum);
            if (result.Count == 0)
                return param;
            if (result.Count == param.Count)
                return rlist;
            foreach (var item in param)
            {
                if (!result.Exists(r => r.Item0 == item.Item0 && r.Item1 == item.Item1))
                    rlist.Add(item);
            }
            return rlist;
        }

        public static List<StringArray> GetInventoryKeyInfoBySkuWithWarehouseCodes(List<StringArray> param, int masterAccountNum, int profileNum)
        {
            if (param == null || param.Count == 0)
                return new List<StringArray>(0);
            var sql = $@" 
SELECT SKU,WarehouseCode,InventoryUuid,ProductUuid FROM Inventory inv WHERE Exists (SELECT item0 AS SKU,item1 AS WarehouseCode FROM @SKUTable st WHERE inv.SKU=st.item0 AND inv.WarehouseCode=st.item1)
";
            var sqlParameters = new IDataParameter[3]
            {
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                param.ToStringArrayListParameters("SKUTable")
            };
            return SqlQuery.Execute(
                sql,
                (string sku, string warehouseCode, string inventoryUuid, string productUuid) => new StringArray() { Item0 = sku, Item1 = warehouseCode, Item2 = inventoryUuid, Item3 = productUuid },
                sqlParameters);
        }

        public static async Task<List<StringArray>> GetInventoryKeyInfoBySkuWithWarehouseCodesAsync(List<StringArray> param, int masterAccountNum, int profileNum)
        {
            if (param == null || param.Count == 0)
                return new List<StringArray>(0);
            var sql = $@" 
SELECT SKU,WarehouseCode,InventoryUuid FROM Inventory inv WHERE Exists (SELECT item0 AS SKU,item1 AS WarehouseCode FROM @SKUTable st WHERE inv.SKU=st.item0 AND inv.WarehouseCode=st.item1)
";
            var sqlParameters = new IDataParameter[3]
            {
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("pofileNum"),
                param.ToStringArrayListParameters("SKUTable")
            };
            return await SqlQuery.ExecuteAsync(
                sql,
                (string sku, string warehouseCode, string inventoryUuid) => new StringArray() { Item0 = sku, Item1 = warehouseCode, Item2 = inventoryUuid },
                sqlParameters);
        }

        /// <summary>
        /// If transaction change inventory, for example P/O receive, warehouse transfer
        /// need use SQL to update new avg.cost and unit cost.
        /// </summary>
        public static async Task<bool> UpdateInventoryCost(ItemCostClass cost)
        {
            if (cost == null || string.IsNullOrWhiteSpace(cost.InventoryUuid)) return false;

            // TODO create and test helper function to update inventory cost
            //string sqlUpdate = string.Format("" +
            //    "UPDATE ind " +
            //    "SET " +
            //    "ind.price_base = {0}, " +
            //    "ind.prod_duty = {1}, " +
            //    "ind.frt_cus = {2}, " +
            //    "ind.handl_fee = {3}, " +
            //    "ind.avg_cost = {4} " +
            //    "{5} " +
            //    "FROM inv_data ind " +
            //    "WHERE " +
            //    "COALESCE(ind.prod_cd,'') = '{6}' " +
            //    "AND COALESCE(ind.whs_num,'') = '{7}' "
            //    , cost.price_base.ToCost()      /*0*/
            //    , cost.prod_duty.ToCost()       /*1*/
            //    , cost.frt_cus.ToAmount()       /*2*/
            //    , cost.handl_fee.ToAmount()     /*3*/
            //    , cost.avg_cost.ToCost()        /*4*/
            //    , cost.prod_cd.ToSqlSafeString()  /*6*/
            //    , cost.whs_num.ToSqlSafeString()  /*7*/
            //    );
            //var result = OmsDatabase.ExecuteUpdate(sqlUpdate);
            //return result > 0;
            return true;
        }

        /// <summary>
        /// If InentoryLog records created or changed
        /// need use SQL to update instock in inventory table.
        /// </summary>
        public static int UpdateInventoryInStock(string LogUuid, int isAdd)
        {
            if (string.IsNullOrEmpty(LogUuid)) return 0;
            //if (InvoiceVoid(invs_num)) return 0;
            //var add_minus = "-";
            //if (isAdd < 0) add_minus = "+";

            //string sql = @"
            //    "UPDATE inv_data SET in_stock = ind.in_stock {0} logm.qty " +
            //    "FROM inv_data ind INNER JOIN " +
            //    "(SELECT lg.prod_cd, lg.whs_num, SUM(COALESCE(lg.prod_qty,0)) as qty " +
            //    "FROM invt_log lg " +
            //    "INNER JOIN invoice ins ON ins.invs_num = lg.invs_num AND ins.invs_cd = lg.invs_cd " +
            //    "WHERE COALESCE(lg.prod_qty,0) != 0 AND COALESCE(lg.prod_comp,'') != 'C' " +
            //    "AND ins.invs_num = {1} " +
            //    "AND ins.invs_cd = {2} " +
            //    "GROUP BY lg.prod_cd, lg.whs_num) as logm " +
            //    "ON ind.prod_cd = logm.prod_cd AND ind.whs_num = logm.whs_num "
            //    , add_minus, invs_num, invs_cd);

            //var result = OmsDatabase.GetValue<int>(sSelect);
            //return result;
            return 0;
        }

        /// <summary>
        /// If Sales Order created or changed
        /// need use SQL to update open S/O qty in inventory table.
        /// </summary>
        public static int UpdateInventoryOpenSoQty(string SalesOrderUuid, int isAdd)
        {
            if (string.IsNullOrEmpty(SalesOrderUuid)) return 0;
            //if (SoCancel(ord_num)) return 0;
            //var add_minus = "+";
            //if (isAdd < 0) add_minus = "-";

            //string sSelect = string.Format("UPDATE inv_data SET order_qty = ind.order_qty {0} olgm.qty " +
            //    " FROM inv_data ind inner join " +
            //    " (select olg.prod_cd, olg.whs_num, " +
            //    " sum(COALESCE(olg.order_qty,0) - COALESCE(olg.invs_qty,0) - COALESCE(olg.can_qty,0)) as qty " +
            //    " from ord_log olg " +
            //    " INNER JOIN orders ord ON ord.ord_num = olg.ord_num " +
            //    " WHERE COALESCE(ord.close_cd,0) != 200 AND ABS(COALESCE(olg.order_qty,0)) > 0 AND COALESCE(olg.prod_comp,'') <> 'C' " +
            //    " AND COALESCE(olg.order_qty,0) - COALESCE(olg.invs_qty,0) - COALESCE(olg.can_qty,0) > 0.00001 " +
            //    " AND ord.ord_num = {1} " +
            //    " group by olg.prod_cd, olg.whs_num) as olgm " +
            //    " on ind.prod_cd = olgm.prod_cd and ind.whs_num = olgm.whs_num ", add_minus, ord_num);

            //var result = OmsDatabase.GetValue<int>(sSelect);
            //return result;
            return 0;
        }

    }
}




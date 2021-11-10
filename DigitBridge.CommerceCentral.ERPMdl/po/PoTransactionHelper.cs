    

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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a PoTransactionHelper SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class PoTransactionHelper
    {
        public static bool ExistNumber(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
*/
            return true;
        }

        public static async Task<bool> ExistNumberAsync(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
*/
            return true;
        }

        public static bool ExistId(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND TransUuid = @uuid
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
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND TransUuid = @uuid
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                uuid.ToSqlParameter("uuid")
            );
            return result > 0;
        }
        public static async Task<int> GetTranSeqNumAsync(string poUuid, int profileNum)
        {
            var sql = $@"
SELECT isnull(max(TransNum),0)+1 FROM PoTransaction tbl
WHERE ProfileNum = @profileNum
AND PoUuid= @PoUuid
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                profileNum.ToSqlParameter("profileNum"),
                poUuid.ToSqlParameter("PoUuid")
            );
            return result;
        }
        public static int GetTranSeqNum(string poUuid, int profileNum)
        {
            var sql = $@"
SELECT isnull(max(TransNum),0)+1 FROM PoTransaction tbl
WHERE ProfileNum = @profileNum
AND PoUuid= @PoUuid
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                profileNum.ToSqlParameter("profileNum"),
                poUuid.ToSqlParameter("PoUuid")
            );
            return result;
        }

        public static bool ExistRowNum(long rowNum, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND RowNum= @rowNum
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
SELECT COUNT(1) FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND RowNum= @rowNum
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                rowNum.ToSqlParameter("rowNum")
            );
            return result > 0;
        }
        
        public static List<long> GetRowNums(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return SqlQuery.Execute(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }

        public static async Task<List<long>> GetRowNumsAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return await SqlQuery.ExecuteAsync(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }
        public static long GetRowNumByNumber(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            return SqlQuery.ExecuteScalar<long>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
*/
            return 0;
        }

        public static async Task<long> GetRowNumByNumberAsync(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            return await SqlQuery.ExecuteScalarAsync<long>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
*/
            return 0;
        }

        public static List<long> GetRowNumsByNums(IList<string> nums, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND (EXISTS (SELECT * FROM @nums _num WHERE _num.item = COALESCE([SKU],'')))";

            return SqlQuery.Execute(
                sql,
                (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                nums.ToParameter<string>("nums"));
*/
            return new List<long>();
        }

        public static async Task<List<long>> GetRowNumsByNumsAsync(IList<string> nums, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT RowNum FROM PoTransaction tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND (EXISTS (SELECT * FROM @nums _num WHERE _num.item = COALESCE([SKU],'')))";

            return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                nums.ToParameter<string>("nums"));
*/
            return new List<long>();
        }
        
        public static List<(long rowNum, string poItemUuid, decimal transQty)> GetReceiveItemsByPoUuid(IDataBaseFactory dbFactory, string poUuid)
        {

            var sql = $@"
SELECT RowNum,PoItemUuid,TransQty 
FROM PoTransactionItems  
where PoUuid = @poUuid
";
            using (var tx = new ScopedTransaction(dbFactory))
            {
                return SqlQuery.Execute(sql, (long rowNum, string poItemUuid, decimal transQty) => (rowNum, poItemUuid, transQty), poUuid.ToSqlParameter("poUuid"));
            }
        }
    }
}




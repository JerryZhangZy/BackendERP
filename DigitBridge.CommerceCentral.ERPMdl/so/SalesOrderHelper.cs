


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
    /// Represents a SalesOrderHelper SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class SalesOrderHelper
    {
        public static bool ExistNumber(string number, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = SqlQuery.ExecuteScalar<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                 profileNum.ToSqlParameter("profileNum"),
                 number.ToSqlParameter("number"));
            return result > 0;
        }

        public static async Task<bool> ExistNumberAsync(string number, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                  profileNum.ToSqlParameter("profileNum"),
                  number.ToSqlParameter("number"));
            return result > 0;
        }
        public static bool ExistNumber(string number, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = SqlQuery.ExecuteScalar<int>(sql,
                 profileNum.ToSqlParameter("profileNum"),
                 number.ToSqlParameter("number"));
            return result > 0;
        }

        public static async Task<bool> ExistNumberAsync(string number, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE  ProfileNum = @profileNum
AND OrderNumber = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                  profileNum.ToSqlParameter("profileNum"),
                  number.ToSqlParameter("number"));
            return result > 0;
        }
        public static bool ExistId(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SalesOrderUuid = @uuid
";
            var result = SqlQuery.ExecuteScalar<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                 profileNum.ToSqlParameter("profileNum"),
                 uuid.ToSqlParameter("uuid"));
            return result > 0;
        }

        public static async Task<bool> ExistIdAsync(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND SalesOrderUuid = @uuid
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                  profileNum.ToSqlParameter("profileNum"),
                  uuid.ToSqlParameter("uuid"));
            return result > 0;
        }

        public static bool ExistRowNum(long rowNum, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND RowNum= @rowNum
";
            var result = SqlQuery.ExecuteScalar<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                 profileNum.ToSqlParameter("profileNum"),
                 rowNum.ToSqlParameter("rowNum"));
            return result > 0;
        }

        public static async Task<bool> ExistRowNumAsync(long rowNum, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND RowNum= @rowNum
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql, masterAccountNum.ToSqlParameter("masterAccountNum"),
                 profileNum.ToSqlParameter("profileNum"),
                 rowNum.ToSqlParameter("rowNum"));
            return result > 0;
        }
        public static List<long> GetRowNums(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT [RowNum] FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return SqlQuery.Execute(sql,
                    (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum")
            );
        }
        public static async Task<List<long>> GetRowNumsAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT [RowNum] FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return await SqlQuery.ExecuteAsync(sql,
                    (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum")
            );
        }

        //public static async Task<bool> ExistOrderDCAssignmentUuidAsync(string orderDCAssignmentUuid)
        //{

        //    var sql = $@"
        //SELECT COUNT(1) FROM SalesOrderHeader tbl
        //WHERE OrderSourceCode = 'OrderDCAssignmentUuid:' + @orderDCAssignmentUuid
        //";
        //    var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
        //          orderDCAssignmentUuid.ToSqlParameter("orderDCAssignmentUuid"));
        //    return result > 0;
        //}
        public static async Task<bool> ExistOrderDCAssignmentNumAsync(long orderDCAssignmentNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM SalesOrderHeader tbl
WHERE OrderSourceCode = 'OrderDCAssignmentNum:' + Cast(@orderDCAssignmentNum as varchar)
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                  orderDCAssignmentNum.ToSqlParameter("orderDCAssignmentNum"));
            return result > 0;
        }

        public static async Task<string> GetSalesOrderUuidAsync(long orderDCAssignmentNum)
        {
            var sql = $@"
SELECT TOP 1 [SalesOrderUuid] FROM SalesOrderHeader tbl
WHERE OrderSourceCode = 'OrderDCAssignmentNum:' + Cast(@orderDCAssignmentNum as varchar)
";
            var result = await SqlQuery.ExecuteScalarAsync<string>(sql,
                 orderDCAssignmentNum.ToSqlParameter("orderDCAssignmentNum"));

            return result;
        }

        public static async Task<string> GetSalesOrderNumberByUuidAsync(string salesOrderUuid)
        {
            var sql = $@"
SELECT TOP 1 [OrderNumber] FROM SalesOrderHeader
WHERE SalesOrderUuid = @salesOrderUuid)
";
            var result = await SqlQuery.ExecuteScalarAsync<string>(sql,
                 salesOrderUuid.ToSqlParameter("salesOrderUuid"));

            return result;
        }

    }
}




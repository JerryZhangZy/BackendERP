    

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
    /// Represents a InitNumbersHelper SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InitNumbersHelper
    {
        public static bool ExistNumber(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT COUNT(1) FROM InitNumbers tbl
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

        public static async Task<string> GetMinNumberAsync(int masterAccountNum, int profileNum, string customerUuid, string type)
        {
            string sql = string.Empty;
            switch (type)
            {
                case "so":
                    sql = $@"SELECT TOP 1 * FROM (
    SELECT t1.OrderNumber+1 AS number
    FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t1
    WHERE NOT EXISTS(
		SELECT * 
		FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t2 
		WHERE t2.OrderNumber = t1.OrderNumber + 1
	) 
) ot
WHERE ot.number > (SELECT   [Number]   FROM [dbo].[InitNumbers] WHERE [CustomerUuid]=@customerUuid AND [Type]=@type)
ORDER BY ot.number";
                    break;

                case "invoice":

                    sql = $@"SELECT TOP 1 * FROM (
    SELECT t1.InvoiceNumber+1 AS number
    FROM (SELECT CAST(InvoiceNumber AS bigint) AS InvoiceNumber FROM InvoiceHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(InvoiceNumber) < 20 AND ISNUMERIC(InvoiceNumber) = 1) t1
    WHERE NOT EXISTS(
		SELECT * 
		FROM (SELECT CAST(InvoiceNumber AS bigint) AS InvoiceNumber FROM InvoiceHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(InvoiceNumber) < 20 AND ISNUMERIC(InvoiceNumber) = 1) t2 
		WHERE t2.InvoiceNumber = t1.InvoiceNumber + 1
	) 
) ot
WHERE ot.number > (SELECT   [Number]   FROM [dbo].[InitNumbers] WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND [CustomerUuid]=@customerUuid AND [Type]=@type)
ORDER BY ot.number";

                    break;

                case "po":
                    sql = $@"SELECT TOP 1 * FROM (
    SELECT t1.PoNum+1 AS number
    FROM (SELECT CAST(PoNum AS bigint) AS PoNum FROM PoHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(PoNum) < 20 AND ISNUMERIC(PoNum) = 1) t1
    WHERE NOT EXISTS(
		SELECT * 
		FROM (SELECT CAST(PoNum AS bigint) AS PoNum FROM PoHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(PoNum) < 20 AND ISNUMERIC(PoNum) = 1) t2 
		WHERE t2.PoNum = t1.PoNum + 1
	) 
) ot
WHERE ot.number > (SELECT   [Number]   FROM [dbo].[InitNumbers] WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND [CustomerUuid]=@customerUuid AND [Type]=@type)
ORDER BY ot.number";

                    break;
                case "apinvoice":
                    sql = $@"SELECT TOP 1 * FROM (
    SELECT t1.ApInvoiceNum+1 AS number
    FROM (SELECT CAST(ApInvoiceNum AS bigint) AS ApInvoiceNum FROM ApInvoiceHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(ApInvoiceNum) < 20 AND ISNUMERIC(ApInvoiceNum) = 1) t1
    WHERE NOT EXISTS(
		SELECT * 
		FROM (SELECT CAST(ApInvoiceNum AS bigint) AS ApInvoiceNum FROM ApInvoiceHeader WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND LEN(ApInvoiceNum) < 20 AND ISNUMERIC(ApInvoiceNum) = 1) t2 
		WHERE t2.ApInvoiceNum = t1.ApInvoiceNum + 1
	) 
) ot
WHERE ot.number > (SELECT   [Number]   FROM [dbo].[InitNumbers] WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND [CustomerUuid]=@customerUuid AND [Type]=@type)
ORDER BY ot.number";
                    break;
            };


            return await SqlQuery.ExecuteScalarAsync<string>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                customerUuid.ToSqlParameter("customerUuid"),
                type.ToSqlParameter("type")
            );
        }




        //public static async Task<InitNumbers> GetInitNumbersAsync(int masterAccountNum, int profileNum, string customerUuid, string type)
        //{

        //    string sql = $@"SELECT * FROM InitNumbers WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND [CustomerUuid]=@customerUuid AND [Type]=@type";
        //    return await SqlQuery.Execute<InitNumbers>(sql,
        //  masterAccountNum.ToSqlParameter("masterAccountNum"),
        //  profileNum.ToSqlParameter("profileNum"),
        //  customerUuid.ToSqlParameter("customerUuid"),
        //  type.ToSqlParameter("type"));

        //}


        public static (string number, int currentNumber, string prefix, string suffix,long rowNum) GetInitNumbersAsync(IDataBaseFactory dbFactory, int masterAccountNum, int profileNum, string customerUuid, string type)
        {
            using (var trx = new ScopedTransaction(dbFactory))
            {

                var sql = $@"SELECT number,currentNumber,prefix,suffix,rowNum FROM InitNumbers WHERE [MasterAccountNum]=@masterAccountNum AND    [ProfileNum]=@profileNum AND [CustomerUuid]=@customerUuid AND [Type]=@type";
                return SqlQuery.Execute(sql, (string number, int currentNumber, string prefix, string suffix, long rowNum) => (number, currentNumber, prefix, suffix, rowNum), masterAccountNum.ToSqlParameter("masterAccountNum"),
         profileNum.ToSqlParameter("profileNum"),
         customerUuid.ToSqlParameter("customerUuid"),
         type.ToSqlParameter("type"))[0];
            }

        }




        public static async Task<long> GetRowNumByInitNumbersUuidAsync(int masterAccountNum, int profileNum, string initNumbersUuid)
        {

            var sql = $@"
            SELECT RowNum FROM InitNumbers tbl
            WHERE MasterAccountNum = @masterAccountNum
            AND ProfileNum = @profileNum
            AND InitNumbersUuid = @initNumbersUuid
            ";
            return await SqlQuery.ExecuteScalarAsync<long>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                initNumbersUuid.ToSqlParameter("initNumbersUuid")
            );
 
        }


        public static async Task<bool> ExistNumberAsync(string number, int masterAccountNum, int profileNum)
        {
/*
            var sql = $@"
SELECT COUNT(1) FROM InitNumbers tbl
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
SELECT COUNT(1) FROM InitNumbers tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND InitNumbersUuid = @uuid
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
SELECT COUNT(1) FROM InitNumbers tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND InitNumbersUuid = @uuid
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
SELECT COUNT(1) FROM InitNumbers tbl
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
SELECT COUNT(1) FROM InitNumbers tbl
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
SELECT RowNum FROM InitNumbers tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return SqlQuery.Execute(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }

        public static async Task<List<long>> GetRowNumsAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT RowNum FROM InitNumbers tbl
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
SELECT RowNum FROM InitNumbers tbl
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
SELECT RowNum FROM InitNumbers tbl
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
SELECT RowNum FROM InitNumbers tbl
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
SELECT RowNum FROM InitNumbers tbl
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

    }
}





    

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
    /// Represents a CustomerHelper SQL Helper Static Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class CustomerServiceHelper
    {
        public static bool ExistNumber(string number, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT COUNT(1) FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @number
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
SELECT COUNT(1) FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @number
";
            var result = await SqlQuery.ExecuteScalarAsync<int>(sql,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                number.ToSqlParameter("number")
            );
            return result > 0;
        }

        public static bool ExistId(string uuid, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT COUNT(1) FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerUuid = @uuid
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
SELECT COUNT(1) FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerUuid = @uuid
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
SELECT COUNT(1) FROM Customer tbl
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
SELECT COUNT(1) FROM Customer tbl
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

        public static long GetRowNumByCustomerCode(string customerCode, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT Top 1 RowNum FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @customerCode
";
            var result = SqlQuery.ExecuteScalar<long>(sql,CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                customerCode.ToSqlParameter("customerCode")
            );
            return result;
        }

        public static async Task<long> GetRowNumByCustomerCodeAsync(string customerCode, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT Top 1 RowNum FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @customerCode
";
            var result = await SqlQuery.ExecuteScalarAsync<long>(sql,CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                customerCode.ToSqlParameter("customerCode")
            );
            return result;
        }

        public static List<long> GetRowNums(int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT RowNum FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return SqlQuery.Execute(sql,(long rowNum)=>rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum")
            );
        }

        public static async Task<List<long>> GetRowNumsAsync(int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT RowNum FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum";
            return await SqlQuery.ExecuteAsync(sql, (long rowNum) => rowNum,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"));
        }

        public static string GetCustomerUuidByCustomerCode(string customerCode, int masterAccountNum, int profileNum)
        {

            var sql = $@"
SELECT Top 1 CustomerUuid FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @customerCode
";
            var result = SqlQuery.ExecuteScalar<string>(sql, CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                customerCode.ToSqlParameter("customerCode")
            );
            return result;
        }

        public static async Task<string> GetCustomerUuidByCustomerCodeAsync(string customerCode, int masterAccountNum, int profileNum)
        {
            var sql = $@"
SELECT Top 1 CustomerUuid FROM Customer tbl
WHERE MasterAccountNum = @masterAccountNum
AND ProfileNum = @profileNum
AND CustomerCode = @customerCode
";
            var result = await SqlQuery.ExecuteScalarAsync<string>(sql, CommandType.Text,
                masterAccountNum.ToSqlParameter("masterAccountNum"),
                profileNum.ToSqlParameter("profileNum"),
                customerCode.ToSqlParameter("customerCode")
            );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skus"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <returns>Tuple List,(RowNum,UniqueId,Code)</returns>
        public static List<(long, string, string)> GetKeyInfoByCustomerCodes(IList<string> customerCodes, int masterAccountNum, int profileNum)
        {
            if (customerCodes == null || customerCodes.Count == 0)
                return new List<(long, string, string)>();
            var filters = new List<IQueryFilter>
            {
                new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", "", FilterBy.eq, 0)
                {
                    FilterValue = masterAccountNum
                },
                new QueryFilter<int>("ProfileNum", "ProfileNum", "", FilterBy.eq, 0)
                {
                    FilterValue = profileNum
                },
                new QueryFilter<string>("CustomerCode", "CustomerCode", "", FilterBy.eq, "")
                {
                    MultipleFilterValueList = customerCodes
                }
            };
            var whereSql = string.Join(" and ", filters.Select(f => f.GetFilterSQLBySqlParameter()));
            var sqlParams = filters.Select(f => f.GetSqlParameter()).ToArray();
            var sql = $@"
SELECT RowNum,CustomerUuid,CustomerCode FROM Customer tbl
WHERE {whereSql}";

            return SqlQuery.Execute(
                sql,
                (long rowNum, string customerUuid, string customerCode) => (rowNum, customerUuid, customerCode),
                sqlParams);
        }

        public static async Task<List<(long, string, string)>> GetKeyInfoBySkusAsync(IList<string> customerCodes, int masterAccountNum, int profileNum)
        {
            if (customerCodes == null || customerCodes.Count == 0)
                return new List<(long, string, string)>();
            var filters = new List<IQueryFilter>
            {
                new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", "", FilterBy.eq, 0)
                {
                    FilterValue = masterAccountNum
                },
                new QueryFilter<int>("ProfileNum", "ProfileNum", "", FilterBy.eq, 0)
                {
                    FilterValue = profileNum
                },
                new QueryFilter<string>("CustomerCode", "CustomerCode", "", FilterBy.eq, "")
                {
                    MultipleFilterValueList = customerCodes
                }
            };
            var whereSql = string.Join(" and ", filters.Select(f => f.GetFilterSQLBySqlParameter()));
            var sqlParams = filters.Select(f => f.GetSqlParameter()).ToArray();
            var sql = $@"
SELECT RowNum,CustomerUuid,CustomerCode FROM Customer tbl
WHERE {whereSql}";

            return await SqlQuery.ExecuteAsync(
                sql,
                (long rowNum, string customerUuid, string customerCode) => (rowNum, customerUuid, customerCode),
                sqlParams);
        }
    }
}




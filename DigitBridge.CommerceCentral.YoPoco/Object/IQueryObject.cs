using DigitBridge.Base.Utility;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IQueryObject
    {
        IList<IQueryFilter> QueryFilterList { get; }
        bool isOr { get; set; }
        public bool LoadJson { get; set; }
        bool LoadAll { get; set; }

        int SkipRecords { get; set; }
        int PageSize { get; set; }
        int TotalRecords { get; set; }
        int RecordsCount { get; set; }

        IList<string> OrderByList { get; }
        bool HasOrderBy { get; }

        void SetOrderBy(IList<string> orderByList);
        void ClearOrderBy();
        void AddOrderBy(params string[] orderBy);
        IList<string> CopyOrderBy(IList<string> FromOrderByList);
        string GetOrderBySql(string prefix = null);

        string GetSQL();
        string GetSQLWithPrefix(params string[] preString);
        string GetSQLWithoutPrefix(params string[] preString);

        string GetSQLBySqlParameter();
        string GetSQLWithPrefixBySqlParameter(params string[] preString);
        string GetSQLWithoutPrefixBySqlParameter(params string[] preString);

        SqlParameter[] GetSqlParameters();
        SqlParameter[] GetSqlParametersWithPrefix(params string[] prefix);
        SqlParameter[] GetSqlParametersWithoutPrefix(params string[] prefix);

        void DisableAllFilter();
        void AddFilter(IQueryFilter filter);
        void RemoveFilter(IQueryFilter filter);
        void ChangePrefix(string fromPrefix, string toPrefix);

        void InitQueryFilter();
        void ClearAll();

        void SetSecurityParameter(int masterAccountNum, int profileNum);
        void LoadRequestParameter(IPayload payload);
        JObject WriteFilterJObject(JObject obj);

        IQueryFilter GetFilter(string name);
        void SetFilterValue(string name, object value);
        string GetFilterValue(string name);

    }
}
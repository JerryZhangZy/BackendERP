using DigitBridge.Base.Utility;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
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
    public interface ISqlQueryBuilder<TQueryObject>
        where TQueryObject : IQueryObject, new()
    {
        string SQL_Select { get; set; }
        string SQL_SelectSummary { get; set; }
        string SQL_From { get; set; }
        string SQL_Where { get; set; }
        string SQL_WithoutOrder { get; set; }
        string SQL_OrderBy { get; set; }
        string DefaultPrefix { get; set; }
        TQueryObject QueryObject { get; set; }

        bool LoadTotalCount { get; set; }

        bool HasSQL { get; }
        void GetSQL_all();

        string GetCommandText();
        string GetCommandTextForCount();
        string GetCommandTextForSummary();

        SqlParameter[] GetSqlParameters();

        void LoadRequestParameter(IPayload payload);

        SqlQueryResultData Excute();
        Task<SqlQueryResultData> ExcuteAsync();
        bool ExcuteJson(StringBuilder sb);
        Task<bool> ExcuteJsonAsync(StringBuilder sb);

        bool ExcuteJson(StringBuilder sb, string sql, params IDataParameter[] param);
        Task<bool> ExcuteJsonAsync(StringBuilder sb, string sql, params IDataParameter[] param);

        int Count();
        Task<int> CountAsync();

    }

}

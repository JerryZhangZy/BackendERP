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
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// </summary>
    public partial class SqlQueryBuilder<TQueryObject> : ISqlQueryBuilder<TQueryObject>, IMessage
        where TQueryObject : IQueryObject, new()
    {
        public string SQL_Select { get; set; }
        public string SQL_SelectSummary { get; set; }
        public string SQL_From { get; set; }
        public string SQL_Where { get; set; }
        public string SQL_WithoutOrder { get; set; }
        public string SQL_OrderBy { get; set; }

        public bool HasSQL =>
            !string.IsNullOrWhiteSpace(this.SQL_Select) ||
            !string.IsNullOrWhiteSpace(this.SQL_From) ||
            !string.IsNullOrWhiteSpace(this.SQL_WithoutOrder);

        public bool LoadTotalCount { get; set; } = true;

        public bool BySqlParameter { get; set; } = true;
        public string DefaultPrefix { get; set; }

        public bool LoadJson => QueryObject is null ? true : QueryObject.LoadJson;
        public bool LoadAll => QueryObject is null ? true : QueryObject.LoadAll;
        public int SkipRecords => QueryObject is null ? 0 : QueryObject.SkipRecords;
        public int PageSize => QueryObject is null ? (int)20 : (int)(QueryObject.PageSize);
        public string SQL_Paging => QueryObject is null
            ? string.Empty
            : $"OFFSET {this.SkipRecords} ROWS FETCH NEXT {this.PageSize} ROWS ONLY";

        public TQueryObject QueryObject { get; set; }


        #region DataBase
        [JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            return;
        }
        #endregion DataBase

        public SqlQueryBuilder(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            SQL_Select = string.Empty;
            SQL_SelectSummary = string.Empty;
            SQL_From = string.Empty;
            SQL_Where = string.Empty;
            SQL_WithoutOrder = string.Empty;
            SQL_OrderBy = string.Empty;
            DefaultPrefix = string.Empty;
            QueryObject = default(TQueryObject);
        }
        public SqlQueryBuilder(IDataBaseFactory dbFactory, TQueryObject QueryObject) : this(dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            this.QueryObject = QueryObject;
        }


        protected virtual string GetSQL_select()
        {
            return this.SQL_Select;
        }
        protected virtual string GetSQL_from()
        {
            return this.SQL_From;
        }
        protected virtual string GetSQL_where()
        {
            return BySqlParameter ? GetSqlParameter_where() : GetPureSql_where();
        }
        protected virtual string GetPureSql_where()
        {
            this.SQL_Where = (QueryObject != null) ? QueryObject.GetSQL() : string.Empty;
            if (!string.IsNullOrWhiteSpace(this.SQL_Where))
                this.SQL_Where = $" WHERE {this.SQL_Where} ";
            return this.SQL_Where;
        }
        protected virtual string GetSqlParameter_where()
        {
            this.SQL_Where = (QueryObject != null) ? QueryObject.GetSQLBySqlParameter() : string.Empty;
            if (!string.IsNullOrWhiteSpace(this.SQL_Where))
                this.SQL_Where = $" WHERE {this.SQL_Where} ";
            return this.SQL_Where;
        }
        protected virtual string GetSQL_orderBy()
        {
            this.SQL_OrderBy = (QueryObject != null) ? QueryObject.GetOrderBySql() : string.Empty;
            return this.SQL_OrderBy;
        }
        protected virtual string GetSQL_select_summary()
        {
            return this.SQL_SelectSummary;
        }
        protected virtual void AddDefaultOrderBy()
        {
            if (!QueryObject.HasOrderBy)
                QueryObject.AddOrderBy("RowNum");
        }


        public virtual void GetSQL_all()
        {
            this.GetSQL_where();
            this.GetSQL_select();
            this.GetSQL_from();
            this.SQL_WithoutOrder = $"{this.SQL_Select} {this.SQL_From} {this.SQL_Where} ";
            // set default order by
            this.AddDefaultOrderBy();
            this.GetSQL_orderBy();
        }

        public virtual string GetCommandText()
        {
            GetSQL_all();
            if (!HasSQL)
                return string.Empty;

            return (LoadAll)
                ? GetCommandTextForAll()
                : GetCommandTextForPage();
        }

        protected string GetCommandTextForAll()
        {
            var sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine(SQL_Select);
            sb.AppendLine(SQL_From);
            sb.AppendLine(SQL_Where);
            sb.AppendLine(SQL_OrderBy);
            if (this.LoadJson)
                sb.AppendLine(" FOR JSON PATH ");
            return sb.ToString();
        }

        protected virtual string GetCommandTextForPage()
        {
            // select page
            var sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine(SQL_Select);
            sb.AppendLine(SQL_From);
            sb.AppendLine(SQL_Where);
            sb.AppendLine(SQL_OrderBy);
            sb.AppendLine(SQL_Paging);
            if (this.LoadJson)
                sb.AppendLine(" FOR JSON PATH ");
            return sb.ToString();
        }

        public virtual string GetCommandTextForCount()
        {
            GetSQL_all();
            if (!this.HasSQL) return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine("SELECT COUNT(1) ");
            sb.AppendLine(this.SQL_From);
            sb.AppendLine(this.SQL_Where);
            return sb.ToString();
        }

        public virtual string GetCommandTextForSummary()
        {
            GetSQL_all();
            GetSQL_select_summary();
            if (string.IsNullOrWhiteSpace(this.SQL_SelectSummary) ||
                !this.HasSQL) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.AppendLine(SQL_SelectSummary);
            sb.AppendLine(SQL_From);
            sb.AppendLine(SQL_Where);
            //sb.AppendFormat("{0} " +
            //    "FROM ( {1} ) r "
            //    , this.SQL_SelectSummary
            //    , this.SQL_WithoutOrder
            //);
            if (this.LoadJson)
                sb.AppendLine(" FOR JSON PATH ");
            return sb.ToString();
        }

        public virtual SqlParameter[] GetSqlParameters()
        {
            return QueryObject == null ? null : QueryObject.GetSqlParameters();
        }

        public virtual void LoadRequestParameter(IPayload payload)
        {
            if (payload == null)
                return;
            if (QueryObject == null)
                QueryObject = new TQueryObject();

            QueryObject.LoadRequestParameter(payload);
            LoadTotalCount = payload.IsQueryTotalCount;
        }

        public virtual SqlQueryResultData Excute()
        {
            var sql = GetCommandText();
            var param = GetSqlParameters();
            SqlQueryResultData result;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (param == null)
                    result = SqlQuery.QuerySqlQueryResultData(sql, System.Data.CommandType.Text);
                else
                    result = SqlQuery.QuerySqlQueryResultData(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }
        public virtual SqlQueryResultData Excute(string sql, params IDataParameter[] param)
        {
            SqlQueryResultData result;
            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (!param.Any())
                    result = SqlQuery.QuerySqlQueryResultData(sql, System.Data.CommandType.Text);
                else
                    result = SqlQuery.QuerySqlQueryResultData(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual async Task<SqlQueryResultData> ExcuteAsync()
        {
            var sql = GetCommandText();
            var param = GetSqlParameters();
            SqlQueryResultData result;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (param == null)
                    result = await SqlQuery.QuerySqlQueryResultDataAsync(sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.QuerySqlQueryResultDataAsync(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual async Task<SqlQueryResultData> ExcuteAsync(string sql, params IDataParameter[] param)
        {
            SqlQueryResultData result;
            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (!param.Any())
                    result = await SqlQuery.QuerySqlQueryResultDataAsync(sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.QuerySqlQueryResultDataAsync(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual bool ExcuteJson(StringBuilder sb)
        {
            var sql = GetCommandText();
            var param = GetSqlParameters();
            var result = false;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (param == null)
                    result = SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text);
                else
                    result = SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual bool ExcuteJson(StringBuilder sb, string sql, params IDataParameter[] param)
        {
            var result = false;
            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (param == null)
                    result = SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text);
                else
                    result = SqlQuery.QueryJson(sb, sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual async Task<bool> ExcuteJsonAsync(StringBuilder sb)
        {
            var sql = GetCommandText();
            var param = GetSqlParameters();
            var result = false;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (!param.Any())
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, param);
            }
            return result;
        }
        public virtual async Task<bool> ExcuteJsonAsync(StringBuilder sb, string sql, params IDataParameter[] param)
        {
            var result = false;
            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (!param.Any())
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, param);
            }
            return result;
        }
        public virtual int Count()
        {
            if (!LoadTotalCount)
                return 0;
            var sql = GetCommandTextForCount();
            var param = GetSqlParameters();
            var result = 0;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (param == null)
                    result = SqlQuery.ExecuteScalar<int>(sql, System.Data.CommandType.Text);
                else
                    result = SqlQuery.ExecuteScalar<int>(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual async Task<int> CountAsync()
        {
            if (!LoadTotalCount)
                return 0;
            var sql = GetCommandTextForCount();
            var param = GetSqlParameters();
            var result = 0;

            using (var trs = new ScopedTransaction(dbFactory))
            {

                if (param == null)
                    result = await SqlQuery.ExecuteScalarAsync<int>(sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.ExecuteScalarAsync<int>(sql, System.Data.CommandType.Text, param);
            }
            return result;
        }

        public virtual async Task<bool> ExcuteSummaryJsonAsync(StringBuilder sb)
        {
            var sql = GetCommandTextForSummary();
            var param = GetSqlParameters();
            var result = false;

            using (var trs = new ScopedTransaction(dbFactory))
            {
                if (!param.Any())
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text);
                else
                    result = await SqlQuery.QueryJsonAsync(sb, sql, System.Data.CommandType.Text, param);
            }
            return result;
        }


        #region Messages

        public bool HasMessages => Messages.Count > 0;

        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial interface ISelectListQueryBase
    {
        string ListFor { get; set; }
        bool AlwaysLoadAll { get; set; }
        string SqlSelectFromStatement { get; set; }
        bool LoadAll { get; set; }
        long LoadRecords { get; set; }
        string StartFromValue { get; set; }
        string Term { get; set; }
        int? Limit { get; set; }
        int? Offset { get; set; }
        string SQL { get; set; }

        ISelectListQueryBase SetListFilter(DemListClass json);
        ISelectListQueryBase SetListFilter(string term, long max_rec = 0);
        ISelectListQueryBase SetSelectFromSQL(string sqlString);
        DataTable GetDataTable(string sql);
        DataTable GetDataTable();
        DemListClass GetDemListClass(DemListClass obj);
        string GetSQL(SqlStatementParameter param = null);
    }
    
    public partial class SelectListQueryBase : ISelectListQueryBase
    {
        public string ListFor { get; set; } = "";
        public bool AlwaysLoadAll { get; set; } = false;

        public SelectListQueryBase()
        {
            SqlSelectFromStatement = string.Empty;
            LoadAll = false;
            LoadRecords = GlobalSetting.MaxRecordsAutoList;
            StartFromValue = string.Empty;
            Term = string.Empty;
            SQL = string.Empty;
            AlwaysLoadAll = false;
        }
        public SelectListQueryBase(string SqlSelectFromStatement, bool loadAll) : this()
        {
            this.SqlSelectFromStatement = SqlSelectFromStatement;
            LoadAll = loadAll;
        }
        public SelectListQueryBase(string SqlSelectFromStatement, bool loadAll, string term) : this()
        {
            this.SqlSelectFromStatement = SqlSelectFromStatement;
            LoadAll = loadAll;
            StartFromValue = term;
            Term = term;
            SetListFilter(term);
        }
        public SelectListQueryBase(string SqlSelectFromStatement, bool loadAll, string term, int? limit, int? offset) : this()
        {
            this.SqlSelectFromStatement = SqlSelectFromStatement;
            LoadAll = loadAll;
            StartFromValue = term;
            Term = term;
            Limit = limit;
            Offset = offset;
            SetListFilter(term);
        }
        public SelectListQueryBase(DemListClass obj) : this()
        {
            SetListFilter(obj.term);
        }
        
        #region property
        public virtual string SqlSelectFromStatement { get; set; }
        public virtual bool WithZero { get; set; } = false;
        public virtual SelectListQueryBase SetWithZero(bool withZero)
        { 
            WithZero = withZero;
            return this;
        }
        
        public virtual bool LoadAll { get; set; }
        public virtual long LoadRecords { get; set; }
        public virtual string StartFromValue { get; set; }
        public virtual string Term { get; set; }
        public virtual int? Limit { get; set; }
        public virtual int? Offset { get; set; }
        public virtual string SQL { get; set; }
        #endregion

        #region General method for get sql string
        public virtual ISelectListQueryBase SetListFilter(DemListClass json)
        {
            SetListFilter(json.term, json.max_rec);
            if (json.filter != null)
            {
                json.filter.CopyTo(this);
            }
            return this;
        }
        public virtual ISelectListQueryBase SetListFilter(string term, long max_rec = 0)
        {
            Term = term;
            if (term.EqualsIgnoreSpace("ALL") || AlwaysLoadAll)
            {
                LoadAll = true;
                StartFromValue = string.Empty;
            }
            else
            {
                LoadAll = false;
                StartFromValue = (term ?? "").Trim() + "%";
            }
            LoadRecords = (max_rec <= 0) ? GlobalSetting.MaxRecordsAutoList : max_rec;
            return this;
        }
        public virtual ISelectListQueryBase SetSelectFromSQL(string sqlString)
        {
            SqlSelectFromStatement = sqlString;
            return this;
        }
        public virtual string GetSQL(SqlStatementParameter param = null)
        {
            return (LoadAll) ? GetSQLForAll() : GetSQLForStartValue();
        }
        public virtual int GetDataTableCount()
        {
            return GetDataTableCount(GetSQLCount());
        }
        public virtual int GetDataTableCount(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return 0;
            var result = OmsDatabase.GetValue<int>(sql);
            return result;
        }
        public virtual DataTable GetDataTable(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql)) return null;
            var result = OmsDatabase.GetDataTable(sql);
            return result;
        }
        public virtual DataTable GetDataTable()
        {
            return GetDataTable(GetSQL());
        }
        public virtual DemListClass GetDemListClass(DemListClass obj)
        {
            if (obj == null) return obj;
            var table = GetDataTable(GetSQL());
            if (table == null || table.Rows.Count <= 0) return obj;

            obj.ClearData();
            if (WithZero)
            {
                var itemZero = new SelectListItemClass();
                itemZero.Value = "";
                itemZero.Desc = "";
                itemZero.Text = "";
                obj.AddData(itemZero);
            }
            foreach (DataRow ln in table.Rows)
            {
                var item = new SelectListItemClass();

                if (ln.Table.Columns.Contains("cd")) item.Value = ln["cd"].ToString().TrimEnd();
                if (ln.Table.Columns.Contains("cnt")) item.CountNumber = ln["cnt"].ToString().To<int>();
                if (ln.Table.Columns.Contains("descrip")) item.Desc = ln["descrip"].ToString().TrimEnd();
                if (ln.Table.Columns.Contains("text")) item.Text = ln["text"].ToString().TrimEnd();
                if (string.IsNullOrWhiteSpace(item.Text) && ln.Table.Columns.Contains("label")) item.Text = ln["label"].ToString().TrimEnd();

                if (string.IsNullOrWhiteSpace(item.Text)) item.Text = item.Desc;
                if (string.IsNullOrWhiteSpace(item.Text)) item.Text = item.Value;
                obj.AddData(item);

                if (ln.Table.Columns.Count > 3)
                {
                    for (int i = 0; i < ln.Table.Columns.Count; i++)
                    {
                        var colName = ln.Table.Columns[i].ColumnName.ToString();
                        if (string.IsNullOrWhiteSpace(colName) ||
                            colName.EqualsIgnoreSpace("cd") ||
                            colName.EqualsIgnoreSpace("cnt") ||
                            colName.EqualsIgnoreSpace("descrip")) continue;
                        var val = ln[colName]?.ToString().TrimEnd();
                        if (val != null)
                            item.AddMore(colName, val);
                    }
                }
            }
            return obj;
        }
        #endregion

        #region method to override by each child class
        public virtual string GetSqlFromView()
        {
            return string.Empty;
        }

        public virtual string GetSQLForAll()
        {
            return string.Empty;
        }
        public virtual string GetSQLForStartValue()
        {
            return string.Empty;
        }
        public virtual string GetSQLCount()
        {
            return string.Empty;
        }
        #endregion
    }
}

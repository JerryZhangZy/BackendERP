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
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// Base class for all query filter
    /// </summary>
    [Serializable()]
    public abstract partial class QueryObject<TQueryObject> : IQueryObject
        where TQueryObject : QueryObject<TQueryObject>
    {
        public QueryObject() 
        {
            InitQueryFilter();
        }

        public virtual void InitQueryFilter() {}
        public virtual void ClearAll()
        {
            foreach (var i in QueryFilterList)
                i.Clear();
            _orderByList = new List<string>();
            LoadJson = true;
            LoadAll = true;
            SkipRecords = 0;
            PageSize = 20;
            TotalRecords = 0;
            RecordsCount = 0;
            return;
        }

        #region Properties
        protected IList<IQueryFilter> _queryFilterList = new List<IQueryFilter>();
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual IList<IQueryFilter> QueryFilterList 
        { 
            get
            {
                if (_queryFilterList is null)
                    _queryFilterList = new List<IQueryFilter>();
                return _queryFilterList;
            }
        }
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual bool isOr { get; set; } = false;

        /// <summary>
        /// Load json string
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public bool LoadJson { get; set; } = true;
        /// <summary>
        /// Load all records or Load by page
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual bool LoadAll { get; set; } = true;

        /// <summary>
        /// Skip records 
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual int SkipRecords { get; set; } = 0;

        /// <summary>
        /// Load records this query if LoadAll = false
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual int PageSize { get; set; } = 20;

        /// <summary>
        /// Total records of Whole query
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual int TotalRecords { get; set; } = 0;

        /// <summary>
        /// Total records of this load
        /// </summary>
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual int RecordsCount { get; set; } = 0;

        #endregion

        #region Methods

        public virtual string GetSQL()
        {
            if (QueryFilterList.Count == 0) 
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                var sql = qry.GetFilterSQL();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }
        public virtual string GetSQLWithPrefix(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0) 
                return GetSQL();
            if (QueryFilterList.Count == 0) 
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                if (!prefix.Contains<string>(qry.prefix)) 
                    continue;
                var sql = qry.GetFilterSQL();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }
        public virtual string GetSQLWithoutPrefix(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0) 
                return GetSQL();
            if (QueryFilterList.Count == 0) 
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                if (prefix.Contains<string>(qry.prefix)) 
                    continue;
                var sql = qry.GetFilterSQL();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }

        public virtual string GetSQLBySqlParameter()
        {
            if (QueryFilterList.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                var sql = qry.GetFilterSQLBySqlParameter();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }
        public virtual string GetSQLWithPrefixBySqlParameter(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0)
                return GetSQLBySqlParameter();
            if (QueryFilterList.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                if (!prefix.Contains<string>(qry.prefix))
                    continue;
                var sql = qry.GetFilterSQLBySqlParameter();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }
        public virtual string GetSQLWithoutPrefixBySqlParameter(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0)
                return GetSQLBySqlParameter();
            if (QueryFilterList.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;

            foreach (var qry in QueryFilterList)
            {
                if (prefix.Contains<string>(qry.prefix))
                    continue;
                var sql = qry.GetFilterSQLBySqlParameter();
                if (!string.IsNullOrEmpty(sql))
                {
                    if (!isFirst)
                    {
                        if (isOr)
                            sb.Append(" OR ");
                        else
                            sb.Append(" AND ");
                    }
                    sb.Append(sql);
                    if (isFirst) isFirst = false;
                }
            }
            return sb.ToString();
        }

        public virtual SqlParameter[] GetSqlParameters()
        {
            if (QueryFilterList.Count == 0)
                return new SqlParameter[0];

            var lst = QueryFilterList.Select(x => x.GetSqlParameter() as SqlParameter)
                .Where(x => x != null);
            return lst.ToArray();
        }
        public virtual SqlParameter[] GetSqlParametersWithPrefix(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0)
                return GetSqlParameters();
            if (QueryFilterList.Count == 0)
                return new SqlParameter[0];

            var lst = QueryFilterList.Where(x => prefix.Contains<string>(x.prefix))
                .Select(x => x.GetSqlParameter() as SqlParameter)
                .Where(x => x != null);
            return lst.ToArray();
        }

        public virtual SqlParameter[] GetSqlParametersWithoutPrefix(params string[] prefix)
        {
            if (prefix == null || prefix.Length <= 0)
                return GetSqlParameters();
            if (QueryFilterList.Count == 0)
                return new SqlParameter[0];

            var lst = QueryFilterList.Where(x => !prefix.Contains<string>(x.prefix))
                .Select(x => x.GetSqlParameter() as SqlParameter)
                .Where(x => x != null);
            return lst.ToArray();
        }


        public virtual void DisableAllFilter()
        {
            if (QueryFilterList.Count == 0) return;
            foreach (var qry in QueryFilterList)
                qry.Enable = false;
            return;
        }

        public virtual void AddFilter(IQueryFilter filter)
        {
            if (filter != null && !QueryFilterList.Contains(filter)) 
                QueryFilterList.Add(filter);
            return;
        }

        public virtual void RemoveFilter(IQueryFilter filter)
        {
            if (QueryFilterList.Count == 0) return;
            if (filter != null) 
                QueryFilterList.Remove(filter);
            return;
        }

        public virtual void ChangePrefix(string fromPrefix, string toPrefix)
        {
            if (QueryFilterList.Count == 0) 
                return;
            if (string.IsNullOrWhiteSpace(fromPrefix) || string.IsNullOrWhiteSpace(toPrefix)) 
                return;
            foreach (IQueryFilter filter in QueryFilterList)
            {
                if (!filter.prefix.EqualsIgnoreSpace(fromPrefix)) 
                    continue;
                filter.prefix = toPrefix;
            }
            return;
        }

        #endregion

        #region order by 
        protected IList<string> _orderByList = new List<string>();
        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public IList<string> OrderByList
        {
            get
            {
                if (_orderByList == null)
                    _orderByList = new List<string>();
                return _orderByList;
            }
        }

        [XmlIgnore, JsonIgnore, IgnoreDataMember]
        public virtual bool HasOrderBy => (OrderByList.Count > 0);
        public virtual void SetOrderBy(IList<string> orderByList) => _orderByList = orderByList;
        public virtual void ClearOrderBy() => OrderByList.Clear();

        public virtual void AddOrderBy(params string[] orderBy)
        {
            if (orderBy == null || orderBy.Length <= 0)
                return;
            foreach (string item in orderBy)
            {
                if (string.IsNullOrWhiteSpace(item) || _orderByList.Contains(item))
                    continue;
                OrderByList.Add(item);
            }
            return;
        }

        public virtual IList<string> CopyOrderBy(IList<string> FromOrderByList)
        {
            _orderByList = new List<string>();
            if (FromOrderByList == null || FromOrderByList.Count <= 0)
                return OrderByList;
            foreach (string item in FromOrderByList)
            {
                if (string.IsNullOrWhiteSpace(item) || _orderByList.Contains(item))
                    continue;
                OrderByList.Add(item);
            }
            return OrderByList;
        }

        public virtual string GetOrderBySql(string prefix)
        {
            if (OrderByList.Count <= 0)
                return string.Empty;

            var sb = new StringBuilder();
            var isFirst = true;
            foreach (string item in _orderByList)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                var sep = (isFirst) ? string.Empty : ", ";
                var pre = string.IsNullOrWhiteSpace(prefix) ? string.Empty : $"{prefix.Trim()}.";
                sb.Append($"{sep}{pre}{item.Trim()}");
                isFirst = false;
            }
            return (sb.Length > 1)
                ? sb.ToString()
                : string.Empty;
        }

        #endregion

        public virtual void LoadRequestParameter(IPayload payload)
        {
            if (payload == null)
                return;
            SkipRecords = payload.Skip < 0 ? 0 : payload.Skip;
            PageSize = payload.Top < 1 ? 20 : payload.Top;
            if (payload.LoadAll)
                LoadAll = true;

            if (payload.HasSortBy)
                SetOrderBy(payload.SortBy.Split(",").ToList());

            // load all filter
            if (payload.Filter != null && payload.Filter.Count > 0)
            {
                foreach (var kv in payload.Filter)
                {
                    var qry = QueryFilterList.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(kv.Key));
                    if (qry == null) continue;
                    qry.ReadJObject(kv.Value);
                }
                return;
            }
        }

        public virtual JObject WriteFilterJObject(JObject obj)
        {
            if (obj == null)
                obj = new JObject();

            foreach (var qry in QueryFilterList.Where(x => x.CheckEnable()))
            {
                qry.WriteJObject(obj);
            }
            return obj;
        }

        public virtual IQueryFilter GetFilter(string name) => QueryFilterList.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(name));
        public virtual void SetFilterValue(string name, object value) => 
            QueryFilterList.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(name))?.SetValue(value);
        public virtual string GetFilterValue(string name) =>
            QueryFilterList.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(name))?.FilterValueString;

    }
}
 
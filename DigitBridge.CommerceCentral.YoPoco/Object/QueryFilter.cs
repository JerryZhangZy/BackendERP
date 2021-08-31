using DigitBridge.Base.Utility;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
    [Serializable]
    public class QueryFilter<T> : IQueryFilter
    {
        protected string _name;
        protected string _prefix;
        protected string _propertyName;
        protected T _filterValue;
        protected T _defaultValue;
        protected FilterBy _filterMode;
        protected bool _enable;
        protected IList<string> _morePropertyName;
        protected bool _isNVarChar;
        protected bool _isDate;

        protected string _sqlString;
        protected IList<T> _multipleFilterValueList;

        public QueryFilter()
        {
            _defaultValue = default(T);
            _filterValue = _defaultValue;
            _propertyName = string.Empty;
            _prefix = string.Empty;
            _filterMode = FilterBy.eq;
            _enable = false;
            _morePropertyName = new List<string>();
            _sqlString = string.Empty;
            _isDate = false;
        }

        public QueryFilter(string Name, string PropertyName, string prefix, FilterBy FilterMode, T DefaultValue, bool isNVarChar = false, bool Enable = false,bool isDate=false)
            : this()
        {
            _name = Name;
            _defaultValue = DefaultValue;
            _propertyName = PropertyName;
            _prefix = prefix;
            _filterMode = FilterMode;
            _filterValue = _defaultValue;
            _enable = Enable;
            _sqlString = string.Empty;
            _isNVarChar = isNVarChar;
            _isDate = isDate;
        }

        public QueryFilter(string Name, string PropertyName, IEnumerable<string> MorePropertyName, string prefix, FilterBy FilterMode, T DefaultValue, bool isNVarChar = false, bool Enable = false)
            : this(Name, PropertyName, prefix, FilterMode, DefaultValue, isNVarChar, Enable)
        {
            if (MorePropertyName.Any())
                _morePropertyName = MorePropertyName.ToList();
        }

        public virtual void Clear()
        {
            //_defaultValue = default(T);
            //_propertyName = string.Empty;
            //_prefix = string.Empty;
            //_filterMode = FilterBy.eq;
            _filterValue = _defaultValue;
            _enable = false;
            if (MultipleFilterValueList != null)
                MultipleFilterValueList.Clear();
        }

        #region properties

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        public virtual IList<string> MorePropertyName
        {
            get { return _morePropertyName; }
            set { _morePropertyName = value; }
        }

        public virtual string prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        public virtual string SqlString
        {
            get { return _sqlString; }
            set { _sqlString = value; }
        }

        public virtual FilterBy FilterMode
        {
            get
            {
                return _filterMode;
            }
            set
            {
                if (_filterValue is string)
                    _filterMode = value;
                else
                {
                    if (value == FilterBy.bw || value == FilterBy.bn || value == FilterBy.ew || value == FilterBy.en || value == FilterBy.cn || value == FilterBy.nc)
                        _filterMode = FilterBy.eq;
                    else
                        _filterMode = value;
                }
            }
        }

        public virtual int FilterModeInt
        {
            get { return FilterMode.To<int>(); }
            set { FilterMode = value.ToEnum<FilterBy>(FilterBy.eq); }
        }

        public virtual T FilterValue
        {
            get { return _filterValue; }
            set { _filterValue = value; Enable = (value != null); }
        }

        public virtual string FilterValueString
        {
            get { return _filterValue == null ? string.Empty : _filterValue.ToString(); }
            set { _filterValue = value.To<T>(); }
        }

        public virtual IList<T> MultipleFilterValueList
        {
            get { return _multipleFilterValueList; }
            set { _multipleFilterValueList = value; }
        }

        public virtual bool HasMultipleFilterValue => (MultipleFilterValueList != null && MultipleFilterValueList.Count > 0);

        public virtual string MultipleFilterValueString
        {
            get
            {
                return (!HasMultipleFilterValue)
                    ? string.Empty
                    : MultipleFilterValueList.JoinToString(",");
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                MultipleFilterValueList = value.SplitTo<T>(',').ToList();
                var cnt = MultipleFilterValueList.Count - 1;
                for (int i = cnt; i >= 0; i--)
                {
                    T v = MultipleFilterValueList[i];
                    if (!IsValidValue(v))
                    {
                        MultipleFilterValueList.RemoveAt(i);
                        continue;
                    }
                }
            }
        }

        public virtual T DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public virtual bool IsNVarchar
        {
            get { return _isNVarChar; }
            set { _isNVarChar = value; }
        }

        public virtual bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        #endregion properties

        public virtual bool CheckEnable()
        {
            // Filter by multiple value
            if (HasMultipleFilterValue)
            {
                Enable = true;
                return Enable;
            }
            // Filter by single value
            if (FilterValue == null || string.IsNullOrEmpty(PropertyName))
                Enable = false;

            if (FilterValue.Equals(DefaultValue))
                Enable = false;
            return Enable;
        }

        public void AddMorePropertyName(params string[] propertyName)
        {
            foreach (var p in propertyName)
            {
                if (!MorePropertyName.Contains(p))
                {
                    MorePropertyName.Add(p);
                }
            }
        }

        protected virtual string GetPropertyNameSQL()
        {
            return GetPropertyNameSQL(PropertyName);
        }

        protected virtual string GetPropertyNameSQL(string propName)
        {
            if (string.IsNullOrEmpty(propName)) return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("COALESCE(");

            if (propName.IndexOf(".") > 0)
            {
                sb.Append(propName.TrimEnd().Replace(".", ".["));
                sb.Append("]");
            }
            else
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    sb.Append(prefix.TrimEnd());
                    sb.Append(".");
                }
                sb.Append("[");
                sb.Append(propName.TrimEnd());
                sb.Append("]");
            }
            sb.Append(",");
            if (typeof(T) == typeof(String))
                sb.Append("''");
            else if (typeof(T) == typeof(DateTime))
                sb.Append($"CAST('{SqlQuery._SqlMinDateTime.ToShortDateString()}' AS datetime)");
            else
                sb.Append("0");

            sb.Append(")");
            return sb.ToString();
        }

        protected virtual string GetFilterValueSQL(string prefix = null, string postString = null)
        {
            if (FilterValue == null) return string.Empty;
            if (typeof(T) == typeof(String))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("'");
                if (!string.IsNullOrEmpty(prefix)) sb.Append(prefix);
                sb.Append(FilterValue.ToString().TrimEnd().Replace("'", "''"));
                if (!string.IsNullOrEmpty(postString)) sb.Append(postString);
                sb.Append("'");
                return sb.ToString();
            }
            else
            {
                return FilterValue.ToString();
            }
        }

        #region Get filter SQL statement with value

        public virtual string GetFilterSQL()
        {
            if (!CheckEnable()) return string.Empty;
            if (!Enable) return string.Empty;
            if (string.IsNullOrEmpty(PropertyName)) return string.Empty;

            if (!HasMultipleFilterValue)
            {
                return GetSingleFilterSQL();
            }
            else
            {
                return GetMultipleFilterSQL();
            }
        }

        protected virtual string GetMultipleFilterSQL()
        {
            if (!HasMultipleFilterValue) return GetSingleFilterSQL();
            var bk_filterValue = _filterValue;
            var isFirstTime = true;
            StringBuilder sb = new StringBuilder();
            //sb.Append("(");
            foreach (var item in MultipleFilterValueList)
            {
                _filterValue = item;
                var sqlSingle = GetSingleFilterSQL();
                if (!string.IsNullOrWhiteSpace(sqlSingle))
                {
                    if (!isFirstTime)
                    {
                        sb.AppendFormat(" {0} ", GetMultipleValueJoinOperator());
                    }
                    sb.Append(sqlSingle);
                    sqlSingle = string.Empty;
                    isFirstTime = false;
                }
            }
            _filterValue = bk_filterValue;
            //sb.Append(")");
            if (sb.Length > 1)
            {
                sb.Insert(0, "(");
                sb.Append(")");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected virtual string GetSingleFilterSQL()
        {
            if (!CheckEnable()) return string.Empty;
            if (!Enable) return string.Empty;
            if (_filterValue == null || string.IsNullOrEmpty(PropertyName)) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetFilterSQL(PropertyName));

            if (MorePropertyName != null && MorePropertyName.Count > 0)
            {
                var sqlMore = string.Empty;
                foreach (var mProp in MorePropertyName)
                {
                    if (!string.IsNullOrEmpty(mProp))
                    {
                        sqlMore = GetFilterSQL(mProp);
                        if (!string.IsNullOrEmpty(sqlMore))
                        {
                            sb.Append(" OR ");
                            sb.Append(sqlMore);
                            sqlMore = string.Empty;
                        }
                    }
                }
            }
            if (sb.Length > 1)
            {
                sb.Insert(0, "(");
                sb.Append(")");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected virtual string GetFilterSQL(string propName)
        {
            if (_filterValue == null || string.IsNullOrEmpty(propName)) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetPropertyNameSQL(propName));
            switch (FilterMode)
            {
                case FilterBy.eq:
                    sb.Append("=");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.ne:
                    sb.Append("!=");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.lt:
                    sb.Append("<");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.le:
                    sb.Append("<=");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.gt:
                    sb.Append(">");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.ge:
                    sb.Append(">=");
                    sb.Append(GetFilterValueSQL());
                    break;

                case FilterBy.bw:       // begin with
                    sb.Append(" LIKE ");
                    sb.Append(GetFilterValueSQL(null, "%"));
                    break;

                case FilterBy.bn:
                    sb.Append(" NOT LIKE ");
                    sb.Append(GetFilterValueSQL(null, "%"));
                    break;

                case FilterBy.ew:
                    sb.Append(" LIKE ");
                    sb.Append(GetFilterValueSQL("%", null));
                    break;

                case FilterBy.en:
                    sb.Append(" NOT LIKE ");
                    sb.Append(GetFilterValueSQL("%", null));
                    break;

                case FilterBy.cn:
                    sb.Append(" LIKE ");
                    sb.Append(GetFilterValueSQL("%", "%"));
                    break;

                case FilterBy.nc:
                    sb.Append(" NOT LIKE ");
                    sb.Append(GetFilterValueSQL("%", "%"));
                    break;
                //case FilterBy.in:
                //    //string[] ain = new [FilterValue.ToString().TrimEnd()];
                //    //cr = Restrictions.In(PropertyName, new [FilterValue.ToString().TrimEnd()]);
                //    break;
                //case FilterBy.ni:
                //    //cr = Restrictions.Not(Restrictions.Like(PropertyName, FilterValue.ToString().TrimEnd(), MatchMode.Start));
                //    break;
                default:
                    sb.Append("=");
                    sb.Append(GetFilterValueSQL());
                    break;
            }
            return sb.ToString();
        }

        protected virtual string GetMultipleValueJoinOperator()
        {
            if (FilterMode == FilterBy.eq) return "OR";
            if (FilterMode == FilterBy.ne) return "AND";
            if (FilterMode == FilterBy.lt) return "OR";
            if (FilterMode == FilterBy.le) return "OR";
            if (FilterMode == FilterBy.gt) return "OR";
            if (FilterMode == FilterBy.ge) return "OR";
            if (FilterMode == FilterBy.bw) return "OR";
            if (FilterMode == FilterBy.bn) return "AND";
            if (FilterMode == FilterBy.ew) return "OR";
            if (FilterMode == FilterBy.cn) return "OR";
            if (FilterMode == FilterBy.nc) return "AND";
            if (FilterMode == FilterBy.en) return "AND";
            if (FilterMode == FilterBy.en) return "AND";
            return "OR";
        }

        #endregion Get filter SQL statement with value

        #region Get filter SQL statement with SqlParameter

        public virtual string GetFilterSQLBySqlParameter()
        {
            if (!CheckEnable()) return string.Empty;
            if (!Enable) return string.Empty;
            if (string.IsNullOrEmpty(PropertyName) || string.IsNullOrEmpty(Name)) return string.Empty;

            if (!HasMultipleFilterValue)
            {
                return GetSingleFilterSQLBySqlParameter();
            }
            else
            {
                return GetMultipleFilterSQLBySqlParameter();
            }
        }

        protected virtual string GetMultipleFilterSQLBySqlParameter()
        {
            if (!HasMultipleFilterValue) return GetSingleFilterSQLBySqlParameter();

            StringBuilder sb = new StringBuilder();
            var paramName = Name.ToParameterName();
            var paramNamePre = $"_{Name}";

            sb.Append(FilterMode == FilterBy.ne ? "NOT EXISTS " : "EXISTS ")
              .Append("(")
              .Append("SELECT * FROM ")
              .Append(paramName)
              .Append(' ').Append(paramNamePre)
              .Append(" WHERE ")
              .Append(paramNamePre)
              .Append(".item = ")
              .Append(GetPropertyNameSQL(PropertyName))
              .Append(')');

            if (sb.Length > 1)
            {
                sb.Insert(0, "(");
                sb.Append(")");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected virtual string GetSingleFilterSQLBySqlParameter()
        {
            if (!CheckEnable()) return string.Empty;
            if (!Enable) return string.Empty;
            if (_filterValue == null || string.IsNullOrEmpty(PropertyName) || string.IsNullOrEmpty(Name)) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetFilterSQLBySqlParameter(PropertyName, Name));

            if (MorePropertyName != null && MorePropertyName.Count > 0)
            {
                var sqlMore = string.Empty;
                foreach (var mProp in MorePropertyName)
                {
                    if (!string.IsNullOrEmpty(mProp))
                    {
                        sqlMore = GetFilterSQLBySqlParameter(mProp, Name);
                        if (!string.IsNullOrEmpty(sqlMore))
                        {
                            sb.Append(" OR ");
                            sb.Append(sqlMore);
                            sqlMore = string.Empty;
                        }
                    }
                }
            }
            if (sb.Length > 1)
            {
                sb.Insert(0, "(");
                sb.Append(")");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected virtual string GetFilterSQLBySqlParameter(string propName, string name)
        {
            if (_filterValue == null || string.IsNullOrEmpty(propName)) return string.Empty;

            StringBuilder sb = new StringBuilder();
            var paramName = name.ToParameterName();
            sb.Append(GetPropertyNameSQL(propName));
            switch (FilterMode)
            {
                case FilterBy.eq:
                    sb.Append("= ").Append(paramName);
                    break;

                case FilterBy.ne:
                    sb.Append("!= ").Append(paramName);
                    break;

                case FilterBy.lt:
                    sb.Append("< ").Append(paramName);
                    break;

                case FilterBy.le:
                    sb.Append("<= ").Append(paramName);
                    break;

                case FilterBy.gt:
                    sb.Append("> ").Append(paramName);
                    break;

                case FilterBy.ge:
                    sb.Append(">= ").Append(paramName);
                    break;

                case FilterBy.bw:       // begin with
                    sb.Append(" LIKE ").Append(paramName).Append(" + '%'");
                    break;

                case FilterBy.bn:
                    sb.Append(" NOT LIKE ").Append(paramName).Append(" + '%'");
                    break;

                case FilterBy.ew:
                    sb.Append(" LIKE '%' + ").Append(paramName);
                    break;

                case FilterBy.en:
                    sb.Append(" NOT LIKE '%' + ").Append(paramName);
                    break;

                case FilterBy.cn:
                    sb.Append(" LIKE '%' + ").Append(paramName).Append(" + '%'");
                    break;

                case FilterBy.nc:
                    sb.Append(" NOT LIKE '%' + ").Append(paramName).Append(" + '%'");
                    break;

                default:
                    sb.Append("= ").Append(paramName);
                    break;
            }
            return sb.ToString();
        }

        #endregion Get filter SQL statement with SqlParameter

        public virtual IDataParameter GetSqlParameter()
        {
            if (!CheckEnable()) return null;
            if (!Enable) return null;
            if (string.IsNullOrEmpty(Name)) return null;

            return (HasMultipleFilterValue)
                ? MultipleFilterValueList.ToParameter<T>(Name)
                :(typeof(T)==typeof(DateTime)&&_isDate)?FilterValue.ToDateParameter<T>(Name,FilterMode):FilterValue.ToParameter<T>(Name, IsNVarchar);
        }

        protected virtual bool IsValidValue(T value)
        {
            if (value == null || value.Equals(DefaultValue))
            {
                return false;
            }
            if (typeof(T) == typeof(String))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get IList<string> for current multiple filter value
        /// </summary>
        public virtual IList<string> GetMultipleFilterValueList()
        {
            return MultipleFilterValueList.ToStringList();
        }
        /// <summary>
        /// Set current multiple filter value
        /// </summary>
        public virtual void SetMultipleFilterValueList(IList<string> lst)
        {
            if (MultipleFilterValueList == null) MultipleFilterValueList = new List<T>();
            MultipleFilterValueList.Clear();
            foreach (var item in lst)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;
                MultipleFilterValueList.Add(item.To<T>());
            }
            return;
        }
        /// <summary>
        /// Add one value to multiple filter value list
        /// </summary>
        public virtual void AddMultipleFilterValueList(string value)
        {
            if (MultipleFilterValueList == null) return;
            var v = value.To<T>();
            if (!IsValidValue(v)) return;
            if (!MultipleFilterValueList.Contains(v))
            {
                MultipleFilterValueList.Add(v);
            }
            return;
        }

        public virtual void ReadJObject(JToken token)
        {
            if (token == null)
                return;

            switch (token)
            {
                case JObject obj:
                    if (obj.HasValues && obj.ContainsKey("value") && obj["value"].HasValues)
                        SetValue(obj["value"]);
                    if (obj.HasValues && obj.ContainsKey("operator") && obj["operator"].HasValues)
                        SetFilterBy(obj["operator"]);
                    break;
                case JValue val:
                    SetValue(val.Value);
                    break;
            }
        }

        public virtual void WriteJObject(JObject obj)
        {
            if (!CheckEnable())
                return;
            obj.Add(Name,
                new JObject()
                {
                    { "value", (HasMultipleFilterValue) ? MultipleFilterValueString : FilterValueString },
                    { "operator", FilterModeInt }
                });
            return;
        }

        public virtual void SetValue(object value)
        {
            if (value == null)
                return;

            if (value.ToString().Contains(","))
                MultipleFilterValueString = value.ToString();
            else
                this.FilterValue = value.To<T>();
        }

        public virtual void SetFilterBy(object filterBy)
        {
            if (filterBy == null)
                return;
            this.FilterModeInt = filterBy.ToInt();
        }

    }
}
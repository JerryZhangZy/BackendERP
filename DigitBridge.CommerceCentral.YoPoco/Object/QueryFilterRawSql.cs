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
    public class QueryFilterRawSql : QueryFilter<string>
    {
        public QueryFilterRawSql() : base() { }
        public QueryFilterRawSql(string SqlString, string prefix, bool Enable = false)
            : base()
        {
            _enable = Enable;
            _sqlString = SqlString;
            _prefix = prefix;
        }
        public QueryFilterRawSql(string Name, string SqlString, string prefix, bool Enable = false)
            : base()
        {
            _enable = Enable;
            _sqlString = SqlString;
            _prefix = prefix;
            _name = Name;
        }

        public override void Clear()
        {
            base.Clear();
            _enable = false;
        }

        public override bool CheckEnable()
        {
            // Filter by multiple value
            if (string.IsNullOrEmpty(SqlString))
            {
                Enable = false;
                return Enable;
            }
            return Enable;
        }

        #region Get filter SQL statement with value

        public override string GetFilterSQL()
        {
            if (!CheckEnable()) return string.Empty;

            return $"({SqlString})";
        }

        #endregion Get filter SQL statement with value

        #region Get filter SQL statement with SqlParameter

        public override string GetFilterSQLBySqlParameter()
        {
            return GetFilterSQL();
        }

        #endregion Get filter SQL statement with SqlParameter

        public override IDataParameter GetSqlParameter()
        {
            return null;
        }

        public override void ReadJObject(JToken token)
        {
            if (token == null)
                return;

            switch (token)
            {
                case JObject obj:
                    if (obj.HasValues && obj.ContainsKey("value") && obj["value"].HasValues)
                        SetValue(obj["value"]);
                    break;
                case JValue val:
                    SetValue(val.Value);
                    break;
            }
        }

        public override void WriteJObject(JObject obj)
        {
            if (!CheckEnable())
                return;
            obj.Add(Name,
                new JObject()
                {
                    { "value", Enable },
                });
            return;
        }

        public override void SetValue(object value)
        {
            if (value == null)
                return;
            Enable = value.ToBool();
        }
    }
}
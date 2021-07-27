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
    public interface IQueryFilter
    {
        string Name { get; set; }
        string PropertyName { get; set; }
        IList<string> MorePropertyName { get; set; }
        string prefix { get; set; }
        string SqlString { get; set; }
        FilterBy FilterMode { get; set; }
        int FilterModeInt { get; set; }
        string FilterValueString { get; set; }
        string MultipleFilterValueString { get; set; }
        bool HasMultipleFilterValue { get; }

        bool Enable { get; set; }

        bool CheckEnable();
        void Clear();
        void AddMorePropertyName(params string[] propertyName);

        string GetFilterSQL();
        string GetFilterSQLBySqlParameter();

        IDataParameter GetSqlParameter();

        IList<string> GetMultipleFilterValueList();
        void SetMultipleFilterValueList(IList<string> lst);
        void AddMultipleFilterValueList(string value);

        void ReadJObject(JToken token);
        void WriteJObject(JObject obj);
        void SetValue(object value);
        void SetFilterBy(object filterBy);

    }
}

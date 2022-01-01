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
    public class EnumQueryFilter<T> : QueryFilter<int> 
        where T : Enum
    {
        public EnumQueryFilter() : base() { }

        public EnumQueryFilter(string Name, string PropertyName, string prefix, FilterBy FilterMode, int DefaultValue, bool isNVarChar = false, bool Enable = false)
            : base(Name, PropertyName, prefix, FilterMode, DefaultValue, isNVarChar, Enable)
        { }

        public EnumQueryFilter(string Name, string PropertyName, IEnumerable<string> MorePropertyName, string prefix, FilterBy FilterMode, int DefaultValue, bool isNVarChar = false, bool Enable = false)
            : base(Name, PropertyName, MorePropertyName, prefix, FilterMode, DefaultValue, isNVarChar, Enable)
        {}
    }
}
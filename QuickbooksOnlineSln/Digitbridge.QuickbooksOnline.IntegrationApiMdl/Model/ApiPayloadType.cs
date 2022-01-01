using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model
{
    public class RequestParameterType
    {
        public string Name { get; set; }
        public List<string> Sources { get; set; } //Header, Query, Path
        public bool IsRequired { get; set; }
        public string Value { get; set; }
        public string MappedSqlParameterName { get; set; }
        public string MappedSqlParamterType { get; set; } //string, int, datetime, decimal
        public string MappedSqlParamterValueType { get; set; } //Equal, Min, Max, Array, ArrayNot
        public RequestParameterSelectType MappedSqlParameterSelect { get; set; }
    }

    public class RequestParameterSelectType
    {
        public string SelectColumn { get; set; }
        public string FromObject { get; set; }
        public string FilterColumn { get; set; }
        public string FilterColumnType { get; set; }
        public string FilterValueType { get; set; }
    }

    public class WebRequestParameterType
    {
        public string RequestName { get; set; }
        public List<RequestParameterType> RequestParameters;
    }
}

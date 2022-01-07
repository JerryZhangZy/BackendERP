using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPDb
{
    //public class RequestCommon
    //{
    //    public int masterAccountNum { get; set; }
    //    public int profileNum { get; set; }
    //    public RequestPaging paging { get; set; }
    //}
    /// <summary>
    /// Request paging information
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        [JsonIgnore]
        public int MasterAccountNum { get; set; }

        /// <summary>
        /// User ProfileNum
        /// Required, from header
        /// </summary>
        [JsonIgnore]
        public int ProfileNum { get; set; }

        /// <summary>
        /// More parameter from both Header and Query string
        /// Optional,
        /// </summary>
        [JsonIgnore]
        public IDictionary<string, object> Paramters { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Page size to load.
        /// Optional,
        /// Default value is 100.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [DataMember(Name = "$top", EmitDefaultValue = false)]
        public int Top { get; set; }

        /// <summary>
        /// Records to skip.
        /// Optional,
        /// Default value is 0.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [DataMember(Name = "$skip", EmitDefaultValue = false)]
        public int Skip { get; set; }

        /// <summary>
        /// Return total count of records or current requested count of data
        /// Optional,
        /// Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.
        /// Default value: true.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [DataMember(Name = "$count", EmitDefaultValue = false)]
        public bool TotalCount { get; set; } = true;

        /// <summary>
        /// Sort by fields (comma delimited).
        /// Optional,
        /// Default order by LastUpdateDate.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [DataMember(Name = "$sortBy", EmitDefaultValue = false)]
        public string SortBy { get; set; }

        /// <summary>
        /// Filter Json object.
        /// Optional,
        /// Default value: {}.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [DataMember(Name = "$filter", EmitDefaultValue = false)]
        public JObject Filter { get; set; }
    }
}

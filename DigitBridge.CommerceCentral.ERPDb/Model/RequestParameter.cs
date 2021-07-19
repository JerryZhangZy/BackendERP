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
    public class RequestPaging
    {
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

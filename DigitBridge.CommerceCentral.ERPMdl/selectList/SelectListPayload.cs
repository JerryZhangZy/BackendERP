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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace DigitBridge.CommerceCentral.ERPMdl
{

    public class SelectListPayload : PayloadBase
    {
        public static string ListAll = "ALL";
        public SelectListPayload()
        {
            Top = 20;
            Skip = 0;
            LoadAll = false;
        }
        public SelectListPayload(string selectListName, string term) : this()
        {
            SelectListName = selectListName;
            Term = term;
            Top = 20;
            Skip = 0;
            LoadAll = false;
        }
        public void ClearAll()
        {
            Data.Clear();
            return;
        }

        #region properties
        public string Term { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_Term { get { return !string.IsNullOrWhiteSpace(Term); } }
        public virtual bool ShouldSerializeterm() { return (With_Term); }

        public string SelectListName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_SelectListName { get { return !string.IsNullOrWhiteSpace(SelectListName); } }

        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public string Name { get { return SelectListName; } set { SelectListName = value; }  }

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder Data { get; set; }
        [JsonIgnore] public virtual bool HasData => Data != null && Data.Length > 0;
        public bool ShouldSerializeData() => HasData;

        #endregion properties

        public SelectListPayload ClearData()
        {
            Data = new StringBuilder();
            return this;
        }

    }

    /// <summary>
    /// for swagger display only
    /// </summary>
    [Serializable()]
    public class SelectListPayloadInput
    {
        /// <summary>
        /// Page size to load.
        /// Optional,
        /// Default value is 100.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$top")]
        [Range(1, 500, ErrorMessage = "Invalid $top")]
        [DataMember(Name = "$top")]
        [JsonProperty("$top")]
        public int Top { get; set; } = 1;

        /// <summary>
        /// Records to skip.
        /// Optional,
        /// Default value is 0.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$skip")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid $skip.")]
        [DataMember(Name = "$skip")]
        [JsonProperty("$skip")]
        public int Skip { get; set; }

        /// <summary>
        /// true:query totalcount and paging data;false:only query paging data;
        /// Optional,
        /// Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.
        /// Default value: true.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        [Display(Name = "$count")]
        [DataMember(Name = "$count")]
        [JsonProperty("$count")]
        public bool IsQueryTotalCount { get; set; } = true;

        /// <summary>
        /// Sort by fields (comma delimited).
        /// Optional,
        /// Default order by LastUpdateDate.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$sortBy")]
        [DataMember(Name = "$sortBy")]
        [JsonProperty("$sortBy")]
        public string SortBy { get; set; }

        /// <summary>
        /// Load all result rows.
        /// Optional,
        /// Default value is false.
        /// </summary>
        [Display(Name = "$loadAll")]
        [DataMember(Name = "$loadAll")]
        [JsonProperty("$loadAll")]
        public bool LoadAll { get; set; }

        public string Term { get; set; }

        public string SelectListName { get; set; }
    }
}

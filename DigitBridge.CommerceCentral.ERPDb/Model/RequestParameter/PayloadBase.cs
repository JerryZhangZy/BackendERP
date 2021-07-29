using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class PayloadBase : IPayload
    {
        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "masterAccountNum is required")]
        [Display(Name = "masterAccountNum")]
        [DataMember(Name = "masterAccountNum")]
        [JsonIgnore]
        public int MasterAccountNum { get; set; }
        [JsonIgnore] public virtual bool HasMasterAccountNum => MasterAccountNum > 0;
        public bool ShouldSerializeMasterAccountNum() => HasMasterAccountNum;

        /// <summary>
        /// User ProfileNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "profileNum is required")]
        [Display(Name = "profileNum")]
        [DataMember(Name = "profileNum")]
        [JsonIgnore]
        public int ProfileNum { get; set; }
        [JsonIgnore] public virtual bool HasProfileNum => ProfileNum > 0;
        public bool ShouldSerializeProfileNum() => HasProfileNum;

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
        public int Top { get; set; } = 1;
        [JsonIgnore] public virtual bool HasTop => Top > 0;
        public bool ShouldSerializeTop() => HasTop;

        /// <summary>
        /// Records to skip.
        /// Optional,
        /// Default value is 0.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$skip")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid $skip.")]
        [DataMember(Name = "$skip")]
        public int Skip { get; set; }
        [JsonIgnore] public virtual bool HasSkip => Skip >= 0;
        public bool ShouldSerializeSkip() => HasSkip;

        /// <summary>
        /// true:query totalcount and paging data;false:only query paging data;
        /// Optional,
        /// Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.
        /// Default value: true.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        [Display(Name = "$count")]
        [DataMember(Name = "$count")]
        public bool IsQueryTotalCount { get; set; } = true;
        [JsonIgnore] public virtual bool HasIsQueryTotalCount => IsQueryTotalCount;

        /// <summary>
        /// Sort by fields (comma delimited).
        /// Optional,
        /// Default order by LastUpdateDate.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$sortBy")]
        [DataMember(Name = "$sortBy")]
        public string SortBy { get; set; }
        [JsonIgnore] public virtual bool HasSortBy => !string.IsNullOrEmpty(SortBy);
        public bool ShouldSerializeSortBy() => HasSortBy;

        /// <summary>
        /// Load all result rows.
        /// Optional,
        /// Default value is false.
        /// </summary>
        [Display(Name = "$loadAll")]
        [DataMember(Name = "$loadAll")]
        public bool LoadAll { get; set; }

        /// <summary>
        /// Filter Json object.
        /// Optional,
        /// Default value: {}.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        [Display(Name = "$filter")]
        [DataMember(Name = "$filter")]
        public JObject Filter { get; set; }
        [JsonIgnore] public virtual bool HasFilter => Filter != null && Filter.Count > 0;
        public bool ShouldSerializeFilter() => HasFilter;


        /// <summary>
        /// StringBuilder for JSON result of SQL query.
        /// Optional,
        /// </summary> 
        [Display(Name = "ListResults")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public IDictionary<string, StringBuilder> ListResults { get; set; }
        [JsonIgnore] public virtual bool HasListResults => ListResults != null && ListResults.Count > 0;
        public bool ShouldSerializeListResults() => HasListResults;
        public void AddListResult(string name, StringBuilder sb)
        {
            if (ListResults == null)
                ListResults = new Dictionary<string, StringBuilder>();
            ListResults.SetValue(name, sb);
        }
        public void RemoveListResult(string name) => ListResults?.RemoveKey(name);
        public StringBuilder GetListResult(string name) => ListResults?.GetValue(name);


        public virtual IDictionary<string, Action<string>> GetOtherParameters() => null; 
    }
}

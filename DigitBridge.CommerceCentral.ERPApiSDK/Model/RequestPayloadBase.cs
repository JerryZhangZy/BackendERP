using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class RequestPayloadBase
    {
        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "masterAccountNum is required")]
        [Display(Name = "masterAccountNum")]
        [DataMember(Name = "masterAccountNum")]
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
        [JsonProperty("$top")]
        public int Top { get; set; } = 20;


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

        /// <summary>
        /// Filter Json object.
        /// Optional,
        /// Default value: {}.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        [Display(Name = "$filter")]
        [DataMember(Name = "$filter")]
        [JsonProperty("$filter")]
        public JObject Filter { get; set; }
    }
}

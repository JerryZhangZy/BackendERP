using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{
    /// <summary>
    /// This data will be written to http request header
    /// </summary>
    public class RequestHeader
    {
        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary> 
        [Display(Name = "masterAccountNum")]
        public int? MasterAccountNum { get; set; }

        /// <summary>
        /// User ProfileNum
        /// Required, from header
        /// </summary> 
        [Display(Name = "profileNum")]
        public int? ProfileNum { get; set; }
    }

    /// <summary>
    /// This data will be written to http request body or query
    /// </summary>
    public class SearchCriteria
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
        public int? Top { get; set; } = 1;

        /// <summary>
        /// Records to skip.
        /// Optional,
        /// Default value is 0.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$skip")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid $skip.")]
        public int? Skip { get; set; }

        /// <summary>
        /// true:query totalcount and paging data;false:only query paging data;
        /// Optional,
        /// Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.
        /// Default value: true.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        [Display(Name = "$Count")]
        public bool? IsQueryTotalCount { get; set; } = true;

        /// <summary>
        /// Sort by fields (comma delimited).
        /// Optional,
        /// Default order by LastUpdateDate.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        [Display(Name = "$sortBy")]
        public string SortBy { get; set; }


        [Display(Name = "$filter")]
        public JObject Filter { get; set; }

        /// <summary>
        /// Load all result rows.
        /// Optional,
        /// Default value is false.
        /// </summary>
        [Display(Name = "$loadAll")]
        public bool? LoadAll { get; set; }
    }

    public class RequestInfo<T>
    {

        public RequestHeader RequestHeader { get; set; }

        public SearchCriteria RequestQuery { get; set; }

        public T RequestBody { get; set; }
    }
}

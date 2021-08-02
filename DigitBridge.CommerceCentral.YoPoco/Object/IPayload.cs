using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// Request paging information
    /// </summary>
    public interface IPayload
    {
        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        int MasterAccountNum { get; set; }
        bool HasMasterAccountNum { get; }

        /// <summary>
        /// User ProfileNum
        /// Required, from header
        /// </summary>
        int ProfileNum { get; set; }
        bool HasProfileNum { get; }

        /// <summary>
        /// User ProfileNum
        /// Required, from header
        /// </summary>
        int DatabaseNum { get; set; }
        bool HasDatabaseNum { get; }

        /// <summary>
        /// Page size to load.
        /// Optional,
        /// Default value is 100.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        int Top { get; set; }
        bool HasTop { get; }

        /// <summary>
        /// Records to skip.
        /// Optional,
        /// Default value is 0.
        /// Maximum value is 500.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        int Skip { get; set; }
        bool HasSkip { get; }

        /// <summary>
        /// true:query totalcount and paging data;false:only query paging data;
        /// Optional,
        /// Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.
        /// Default value: true.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        bool IsQueryTotalCount { get; set; }
        bool HasIsQueryTotalCount { get; }

        /// <summary>
        /// Sort by fields (comma delimited).
        /// Optional,
        /// Default order by LastUpdateDate.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary>
        string SortBy { get; set; }
        bool HasSortBy { get; }

        /// <summary>
        /// Load all result rows.
        /// Optional,
        /// Default value is false.
        /// </summary>
        bool LoadAll { get; set; }

        /// <summary>
        /// Filter Json object.
        /// Optional,
        /// Default value: {}.
        /// <see cref="https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md"/>
        /// </summary> 
        public JObject Filter { get; set; }
        bool HasFilter { get; }

        //IDictionary<string, StringBuilder> ListResults { get; set; }
        //bool HasListResults { get; }
        //void AddListResult(string name, StringBuilder sb);
        //void RemoveListResult(string name);
        //StringBuilder GetListResult(string name);


        IDictionary<string, Action<string>> GetOtherParameters();
    }
}
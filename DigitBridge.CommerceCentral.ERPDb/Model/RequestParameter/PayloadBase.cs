using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System.Security.Claims;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class PayloadBase : IPayload
    {
        /// <summary>
        /// User BackdoorModeEmail
        /// </summary>
        [Required(ErrorMessage = "BackdoorModeEmail")]
        [Display(Name = "backdoorModeEmail")]
        [DataMember(Name = "backdoorModeEmail")]
        [JsonIgnore]
        public string BackdoorModeEmail { get; set; }
        [JsonIgnore] public virtual bool HasBackdoorModeEmail => !string.IsNullOrEmpty(BackdoorModeEmail);
        public bool ShouldSerializeBackdoorModeEmail() => HasBackdoorModeEmail;

        /// <summary>
        /// User BackdoorModePassword
        /// </summary>
        [Required(ErrorMessage = "BackdoorModePassword")]
        [Display(Name = "backdoorModePassword")]
        [DataMember(Name = "backdoorModePassword")]
        [JsonIgnore]
        public string BackdoorModePassword { get; set; }
        [JsonIgnore] public virtual bool HasBackdoorModePassword => !string.IsNullOrEmpty(BackdoorModePassword);
        public bool ShouldSerializeBackdoorModePassword() => HasBackdoorModePassword;

        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "API ClaimsPrincipal")]
        [Display(Name = "claimsPrincipal")]
        [DataMember(Name = "claimsPrincipal")]
        [JsonIgnore]
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        [JsonIgnore] public virtual bool HasClaimsPrincipal => ClaimsPrincipal != null;
        public bool ShouldSerializeClaimsPrincipal() => HasClaimsPrincipal;

        /// <summary>
        /// User MasterAccountNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "API Access Token")]
        [Display(Name = "accessToken")]
        [DataMember(Name = "accessToken")]
        [JsonIgnore]
        public string AccessToken { get; set; }
        [JsonIgnore] public virtual bool HasAccessToken => !string.IsNullOrEmpty(AccessToken);
        public bool ShouldSerializeAccessToken() => HasAccessToken;

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
        /// User ProfileNum
        /// Required, from header
        /// </summary>
        [Required(ErrorMessage = "databaseNum is required")]
        [Display(Name = "databaseNum")]
        [DataMember(Name = "databaseNum")]
        [JsonIgnore]
        public int DatabaseNum { get; set; }
        [JsonIgnore] public virtual bool HasDatabaseNum => DatabaseNum > 0;
        public bool ShouldSerializeDatabaseNum() => HasDatabaseNum;

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
        [JsonIgnore] public virtual bool HasTop => Top > 0;
        public bool ShouldSerializeTop() => HasTop;

        [JsonIgnore]
        public int FixedTop => HasTop ? (Top>500?500:Top) : 1;

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
        [JsonIgnore] public virtual bool HasSkip => Skip >= 0;
        public bool ShouldSerializeSkip() => HasSkip;

        [JsonIgnore]
        public int FixedSkip => HasSkip ? Skip : 0;

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
        [JsonIgnore] public virtual bool HasIsQueryTotalCount => IsQueryTotalCount;

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
        [JsonIgnore] public virtual bool HasSortBy => !string.IsNullOrEmpty(SortBy);
        public bool ShouldSerializeSortBy() => HasSortBy;

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
        [JsonIgnore] public virtual bool HasFilter => Filter != null && Filter.Count > 0;
        public bool ShouldSerializeFilter() => HasFilter;

        /// <summary>
        /// Request success
        /// </summary>
        [Display(Name = "success")]
        [DataMember(Name = "success")]
        public bool Success { get; set; } = true;

        /// <summary>
        /// Message list for this request
        /// </summary>
        [Display(Name = "messages")]
        [DataMember(Name = "messages")]
        public IList<MessageClass> Messages { get; set; } = new List<MessageClass>();
        [JsonIgnore] public virtual bool HasMessages => Messages != null && Messages.Count > 0;
        public bool ShouldSerializeMessages() => HasMessages;

        public virtual IDictionary<string, Action<string>> GetOtherParameters() => null; 


        /// <summary>
        /// Get new payload object and copy data from original payload,
        /// This do not copy filter object
        /// </summary>
        /// <returns></returns>
        public virtual T Clone<T>() where T: IPayload, new()
        {
            return new T()
            {
                MasterAccountNum = this.MasterAccountNum,
                ProfileNum = this.ProfileNum,
                DatabaseNum = this.DatabaseNum,
                Top = this.Top,
                Skip = this.Skip,
                IsQueryTotalCount = this.IsQueryTotalCount,
                SortBy = this.SortBy,
                LoadAll = this.LoadAll
            };
        }

        /// <summary>
        /// Set payload succes = false and error message
        /// and return false
        /// </summary>
        public virtual bool ReturnError(string message)
        {
            Success = false;
            Messages.AddError(message);
            return Success;
        }

        /// <summary>
        /// Set payload succes = false and error message list
        /// and return false
        /// </summary>
        public virtual bool ReturnError(IList<MessageClass> messages)
        {
            Success = false;
            Messages.Add(messages);
            return Success;
        }

        /// <summary>
        /// Set payload succes = true but with warning message
        /// return true
        /// </summary>
        public virtual bool ReturnWarning(string message)
        {
            Success = true;
            Messages.AddWarning(message);
            return Success;
        }

        /// <summary>
        /// Set payload succes = true but with info message
        /// return true
        /// </summary>
        public virtual bool ReturnInfo(string message)
        {
            Success = true;
            Messages.AddInfo(message);
            return Success;
        }
    }
}

              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksChnlAccSetting Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class QuickBooksChnlAccSettingDto
    {
        public long? RowNum { get; set; }
        [JsonIgnore,XmlIgnore]
        public string UniqueId { get; set; }
        [JsonIgnore,XmlIgnore]
        public DateTime? EnterDateUtc { get; set; }
        [JsonIgnore,XmlIgnore]
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// (Readonly) Database Number. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Database Number. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? DatabaseNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDatabaseNum => DatabaseNum != null;

		/// <summary>
		/// (Readonly) Login user account. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user account. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? MasterAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMasterAccountNum => MasterAccountNum != null;

		/// <summary>
		/// (Readonly) Login user profile. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user profile. <br> Display: false, Editable: false.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? ProfileNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProfileNum => ProfileNum != null;

		/// <summary>
		/// Setting uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Setting uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The SettingUuid value cannot exceed 50 characters. ")]
        public string SettingUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSettingUuid => SettingUuid != null;

		/// <summary>
		/// ChnlAccSetting uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("ChnlAccSetting uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The ChnlAccSettingUuid value cannot exceed 50 characters. ")]
        public string ChnlAccSettingUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChnlAccSettingUuid => ChnlAccSettingUuid != null;

		/// <summary>
		/// Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
		/// </summary>
		[OpenApiPropertyDescription("Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )")]
        [StringLength(150, ErrorMessage = "The ChannelAccountName value cannot exceed 150 characters. ")]
        public string ChannelAccountName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelAccountName => ChannelAccountName != null;

		/// <summary>
		/// Central Channel Account Number
		/// </summary>
		[OpenApiPropertyDescription("Central Channel Account Number")]
        public int? ChannelAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelAccountNum => ChannelAccountNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLastUpdate => LastUpdate != null;

		/// <summary>
		/// Last DateTime that the system exported the orders in this ChnlAcc
		/// </summary>
		[OpenApiPropertyDescription("Last DateTime that the system exported the orders in this ChnlAcc")]
        [DataType(DataType.DateTime)]
        public DateTime? DailySummaryLastExport { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDailySummaryLastExport => DailySummaryLastExport != null;


        [IgnoreCompare]
        public JObject Fields { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasFields => Fields != null;


        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



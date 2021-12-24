              
    

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

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a PaidbyMap Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class PaidbyMapDto
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
		/// Each database has its own default value.
		/// </summary>
		[OpenApiPropertyDescription("Each database has its own default value.")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? DatabaseNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDatabaseNum => DatabaseNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? MasterAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMasterAccountNum => MasterAccountNum != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public int? ProfileNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProfileNum => ProfileNum != null;

		/// <summary>
		/// Global Unique Guid for Code
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for Code")]
        [StringLength(50, ErrorMessage = "The PaidbyMapUuid value cannot exceed 50 characters. ")]
        public string PaidbyMapUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPaidbyMapUuid => PaidbyMapUuid != null;

		/// <summary>
		/// (Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false")]
        public int? ChannelNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelNum => ChannelNum != null;

		/// <summary>
		/// (Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false")]
        public int? ChannelAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelAccountNum => ChannelAccountNum != null;

		/// <summary>
		/// Payment method from channel. <br> Title: Channel Paid By, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Payment method from channel. <br> Title: Channel Paid By, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ChannelPaidBy value cannot exceed 50 characters. ")]
        public string ChannelPaidBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChannelPaidBy => ChannelPaidBy != null;

		/// <summary>
		/// ERP Payment method. <br> Title: Paid By, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("ERP Payment method. <br> Title: Paid By, Display: true, Editable: true")]
        public int? PaidBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPaidBy => PaidBy != null;

		/// <summary>
		/// Payment bank account uuid.
		/// </summary>
		[OpenApiPropertyDescription("Payment bank account uuid.")]
        [StringLength(50, ErrorMessage = "The BankAccountUuid value cannot exceed 50 characters. ")]
        public string BankAccountUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBankAccountUuid => BankAccountUuid != null;

		/// <summary>
		/// Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The BankAccountCode value cannot exceed 50 characters. ")]
        public string BankAccountCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBankAccountCode => BankAccountCode != null;

		/// <summary>
		/// Code description,
		/// </summary>
		[OpenApiPropertyDescription("Code description,")]
        [StringLength(100, ErrorMessage = "The Description value cannot exceed 100 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// Channel will auto paid to merchant. <br> Title: Auto Paid, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Channel will auto paid to merchant. <br> Title: Auto Paid, Display: true, Editable: true")]
        public int? AutoPaid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAutoPaid => AutoPaid != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateBy => UpdateBy != null;


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



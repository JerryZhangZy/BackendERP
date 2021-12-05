              
    

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
    /// Represents a InitNumbers Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class InitNumbersDto
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
		/// Global Unique Guid
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid")]
        [StringLength(50, ErrorMessage = "The InitNumbersUuid value cannot exceed 50 characters. ")]
        public string InitNumbersUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInitNumbersUuid => InitNumbersUuid != null;

		/// <summary>
		/// Customer uuid. CustomerUuid = DEFAULT for system init number <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Customer uuid. CustomerUuid = DEFAULT for system init number <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The CustomerUuid value cannot exceed 50 characters. ")]
        public string CustomerUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCustomerUuid => CustomerUuid != null;

		/// <summary>
		/// Disable this record
		/// </summary>
		[OpenApiPropertyDescription("Disable this record")]
        public bool? InActive { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInActive => InActive != null;

		/// <summary>
		/// InitNumber type, like Invoice#, S/O#, P/O#
		/// </summary>
		[OpenApiPropertyDescription("InitNumber type, like Invoice#, S/O#, P/O#")]
        [StringLength(20, ErrorMessage = "The Type value cannot exceed 20 characters. ")]
        public string Type { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasType => Type != null;

		/// <summary>
		/// Init number,  Current maximum
		/// </summary>
		[OpenApiPropertyDescription("Init number,  Current maximum")]
        public int? CurrentNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCurrentNumber => CurrentNumber != null;

		/// <summary>
		/// Init number, real number will be more than init number and not exist number
		/// </summary>
		[OpenApiPropertyDescription("Init number, real number will be more than init number and not exist number")]
        public int? Number { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNumber => Number != null;


        /// <summary>
        /// The last assigned number, real number will be more than init number and not exist number
        /// </summary>
        [OpenApiPropertyDescription("The last assigned number, real number will be more than init number and not exist number")]
        public long? MaxNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMaxNumber => MaxNumber != null;
        /// <summary>
        /// Prefix append to Init number
        /// </summary>
        [OpenApiPropertyDescription("Prefix append to Init number")]
        [StringLength(20, ErrorMessage = "The Prefix value cannot exceed 20 characters. ")]
        public string Prefix { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPrefix => Prefix != null;

		/// <summary>
		/// Suffix follow by Init number
		/// </summary>
		[OpenApiPropertyDescription("Suffix follow by Init number")]
        [StringLength(20, ErrorMessage = "The Suffix value cannot exceed 20 characters. ")]
        public string Suffix { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSuffix => Suffix != null;

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

		/// <summary>
		/// (Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;

		/// <summary>
		/// (Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateBy => UpdateBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}




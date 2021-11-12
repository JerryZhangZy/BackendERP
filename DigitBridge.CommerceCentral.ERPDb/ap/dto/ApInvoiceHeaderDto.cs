              
    

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
    /// Represents a ApInvoiceHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class ApInvoiceHeaderDto
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
		/// (Readonly) ApInvoice uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) ApInvoice uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ApInvoiceUuid value cannot exceed 50 characters. ")]
        public string ApInvoiceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceUuid => ApInvoiceUuid != null;

		/// <summary>
		/// Unique in this database, ProfileNum + ApInvoiceNum is DigitBridgeApInvoiceNum, which is global unique
		/// </summary>
		[OpenApiPropertyDescription("Unique in this database, ProfileNum + ApInvoiceNum is DigitBridgeApInvoiceNum, which is global unique")]
        [StringLength(50, ErrorMessage = "The ApInvoiceNum value cannot exceed 50 characters. ")]
        public string ApInvoiceNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceNum => ApInvoiceNum != null;

		/// <summary>
		/// Link to PoHeader uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Link to PoHeader uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The PoUuid value cannot exceed 50 characters. ")]
        public string PoUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoUuid => PoUuid != null;

		/// <summary>
		/// Link to PoHeader number, unique in same database and profile. <br> Title: PoHeader Number, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Link to PoHeader number, unique in same database and profile. <br> Title: PoHeader Number, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The PoNum value cannot exceed 50 characters. ")]
        public string PoNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoNum => PoNum != null;

		/// <summary>
		/// A/P Invoice type
		/// </summary>
		[OpenApiPropertyDescription("A/P Invoice type")]
        public int? ApInvoiceType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceType => ApInvoiceType != null;

		/// <summary>
		/// A/P Invoice status
		/// </summary>
		[OpenApiPropertyDescription("A/P Invoice status")]
        public int? ApInvoiceStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceStatus => ApInvoiceStatus != null;

		/// <summary>
		/// A/P Invoice date
		/// </summary>
		[OpenApiPropertyDescription("A/P Invoice date")]
        [DataType(DataType.DateTime)]
        public DateTime? ApInvoiceDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceDate => ApInvoiceDate != null;

		/// <summary>
		/// A/P Invoice time
		/// </summary>
		[OpenApiPropertyDescription("A/P Invoice time")]
        [DataType(DataType.DateTime)]
        public DateTime? ApInvoiceTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceTime => ApInvoiceTime != null;

		/// <summary>
		/// reference Vendor Unique Guid
		/// </summary>
		[OpenApiPropertyDescription("reference Vendor Unique Guid")]
        [StringLength(50, ErrorMessage = "The VendorUuid value cannot exceed 50 characters. ")]
        public string VendorUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorUuid => VendorUuid != null;

        /// <summary>
        /// Vendor readable number, DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique
        /// </summary>
        [OpenApiPropertyDescription("Vendor readable number, DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique")]
        [StringLength(50, ErrorMessage = "The VendorCode value cannot exceed 50 characters. ")]
        public string VendorCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorCode => VendorCode != null;

		/// <summary>
		/// Vendor name
		/// </summary>
		[OpenApiPropertyDescription("Vendor name")]
        [StringLength(200, ErrorMessage = "The VendorName value cannot exceed 200 characters. ")]
        public string VendorName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorName => VendorName != null;

		/// <summary>
		/// Vendor Invoice number
		/// </summary>
		[OpenApiPropertyDescription("Vendor Invoice number")]
        [StringLength(50, ErrorMessage = "The VendorInvoiceNum value cannot exceed 50 characters. ")]
        public string VendorInvoiceNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorInvoiceNum => VendorInvoiceNum != null;

		/// <summary>
		/// Vendor Invoice date
		/// </summary>
		[OpenApiPropertyDescription("Vendor Invoice date")]
        [DataType(DataType.DateTime)]
        public DateTime? VendorInvoiceDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorInvoiceDate => VendorInvoiceDate != null;

		/// <summary>
		/// Balance Due date
		/// </summary>
		[OpenApiPropertyDescription("Balance Due date")]
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDueDate => DueDate != null;

		/// <summary>
		/// Next Billing date
		/// </summary>
		[OpenApiPropertyDescription("Next Billing date")]
        [DataType(DataType.DateTime)]
        public DateTime? BillDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBillDate => BillDate != null;

		/// <summary>
		/// (Ignore) ApInvoice price in currency. <br> Title: Currency, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) ApInvoice price in currency. <br> Title: Currency, Display: false, Editable: false")]
        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCurrency => Currency != null;

		/// <summary>
		/// Total A/P invoice amount.
		/// </summary>
		[OpenApiPropertyDescription("Total A/P invoice amount.")]
        public decimal? TotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalAmount => TotalAmount != null;

		/// <summary>
		/// Total Paid amount
		/// </summary>
		[OpenApiPropertyDescription("Total Paid amount")]
        public decimal? PaidAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPaidAmount => PaidAmount != null;

		/// <summary>
		/// Total Credit amount
		/// </summary>
		[OpenApiPropertyDescription("Total Credit amount")]
        public decimal? CreditAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreditAmount => CreditAmount != null;

		/// <summary>
		/// Current balance of invoice
		/// </summary>
		[OpenApiPropertyDescription("Current balance of invoice")]
        public decimal? Balance { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBalance => Balance != null;

		/// <summary>
		/// G/L Credit account, A/P invoice total should specify G/L Credit account
		/// </summary>
		[OpenApiPropertyDescription("G/L Credit account, A/P invoice total should specify G/L Credit account")]
        public long? CreditAccount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreditAccount => CreditAccount != null;

		/// <summary>
		/// G/L Debit account
		/// </summary>
		[OpenApiPropertyDescription("G/L Debit account")]
        public long? DebitAccount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDebitAccount => DebitAccount != null;

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
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
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




              
    

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
    /// Represents a ApInvoiceTransaction Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class ApInvoiceTransactionDto
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
		/// Global Unique Guid for ApInvoice Transaction
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for ApInvoice Transaction")]
        [StringLength(50, ErrorMessage = "The TransUuid value cannot exceed 50 characters. ")]
        public string TransUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransUuid => TransUuid != null;

		/// <summary>
		/// Transaction number
		/// </summary>
		[OpenApiPropertyDescription("Transaction number")]
        public int? TransNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransNum => TransNum != null;

		/// <summary>
		/// Global Unique Guid for ApInvoice
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for ApInvoice")]
        [StringLength(50, ErrorMessage = "The ApInvoiceUuid value cannot exceed 50 characters. ")]
        public string ApInvoiceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasApInvoiceUuid => ApInvoiceUuid != null;

		/// <summary>
		/// Transaction type
		/// </summary>
		[OpenApiPropertyDescription("Transaction type")]
        public int? TransType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransType => TransType != null;

		/// <summary>
		/// Transaction status
		/// </summary>
		[OpenApiPropertyDescription("Transaction status")]
        public int? TransStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransStatus => TransStatus != null;

		/// <summary>
		/// Transaction date
		/// </summary>
		[OpenApiPropertyDescription("Transaction date")]
        [DataType(DataType.DateTime)]
        public DateTime? TransDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransDate => TransDate != null;

		/// <summary>
		/// Transaction time
		/// </summary>
		[OpenApiPropertyDescription("Transaction time")]
        [DataType(DataType.DateTime)]
        public DateTime? TransTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransTime => TransTime != null;

		/// <summary>
		/// Description of ApInvoice Transaction
		/// </summary>
		[OpenApiPropertyDescription("Description of ApInvoice Transaction")]
        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// Notes of ApInvoice Transaction
		/// </summary>
		[OpenApiPropertyDescription("Notes of ApInvoice Transaction")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

		/// <summary>
		/// Payment method number
		/// </summary>
		[OpenApiPropertyDescription("Payment method number")]
        public int? PaidBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPaidBy => PaidBy != null;

		/// <summary>
		/// Global Unique Guid for Bank account
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for Bank account")]
        [StringLength(50, ErrorMessage = "The BankAccountUuid value cannot exceed 50 characters. ")]
        public string BankAccountUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBankAccountUuid => BankAccountUuid != null;

		/// <summary>
		/// Check number
		/// </summary>
		[OpenApiPropertyDescription("Check number")]
        [StringLength(100, ErrorMessage = "The CheckNum value cannot exceed 100 characters. ")]
        public string CheckNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCheckNum => CheckNum != null;

		/// <summary>
		/// Auth code from merchant bank
		/// </summary>
		[OpenApiPropertyDescription("Auth code from merchant bank")]
        [StringLength(100, ErrorMessage = "The AuthCode value cannot exceed 100 characters. ")]
        public string AuthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAuthCode => AuthCode != null;

		/// <summary>
		/// (Ignore)
		/// </summary>
		[OpenApiPropertyDescription("(Ignore)")]
        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCurrency => Currency != null;

		/// <summary>
		/// Realtime Exchange Rate when process transaction
		/// </summary>
		[OpenApiPropertyDescription("Realtime Exchange Rate when process transaction")]
        public decimal? ExchangeRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasExchangeRate => ExchangeRate != null;

		/// <summary>
		/// Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation
		/// </summary>
		[OpenApiPropertyDescription("Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation")]
        public decimal? Amount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAmount => Amount != null;

		/// <summary>
		/// G/L Credit account
		/// </summary>
		[OpenApiPropertyDescription("G/L Credit account")]
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
		/// (Ignore)
		/// </summary>
		[OpenApiPropertyDescription("(Ignore)")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

		/// <summary>
		/// (Ignore)
		/// </summary>
		[OpenApiPropertyDescription("(Ignore)")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;

		/// <summary>
		/// (Ignore)
		/// </summary>
		[OpenApiPropertyDescription("(Ignore)")]
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




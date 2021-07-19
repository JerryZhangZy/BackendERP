
              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransaction Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InvoiceTransactionDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// (Readonly) Database Number. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Database Number. <br> Display: false, Editable: false.")]
        public int? DatabaseNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDatabaseNum => DatabaseNum != null;

		/// <summary>
		/// (Readonly) Login user account. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user account. <br> Display: false, Editable: false.")]
        public int? MasterAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMasterAccountNum => MasterAccountNum != null;

		/// <summary>
		/// (Readonly) Login user profile. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Login user profile. <br> Display: false, Editable: false.")]
        public int? ProfileNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProfileNum => ProfileNum != null;

		/// <summary>
		/// Invoice Transaction uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Invoice Transaction uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The TransUuid value cannot exceed 50 characters. ")]
        public string TransUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransUuid => TransUuid != null;

		/// <summary>
		/// Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true")]
        public int? TransNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransNum => TransNum != null;

		/// <summary>
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Invoice uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The InvoiceUuid value cannot exceed 50 characters. ")]
        public string InvoiceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceUuid => InvoiceUuid != null;

		/// <summary>
		/// Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The InvoiceNumber value cannot exceed 50 characters. ")]
        public string InvoiceNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceNumber => InvoiceNumber != null;

		/// <summary>
		/// Transaction type, payment, return. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Transaction type, payment, return. <br> Title: Type, Display: true, Editable: true")]
        public int? TransType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransType => TransType != null;

		/// <summary>
		/// Transaction status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Transaction status. <br> Title: Status, Display: true, Editable: true")]
        public int? TransStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransStatus => TransStatus != null;

		/// <summary>
		/// Invoice date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? TransDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransDate => TransDate != null;

		/// <summary>
		/// Invoice time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? TransTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransTime => TransTime != null;

		/// <summary>
		/// Description of Invoice Transaction. <br> Title: Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Description of Invoice Transaction. <br> Title: Description, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The Description value cannot exceed 100 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

		/// <summary>
		/// Payment method number. <br> Title: Paid By, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Payment method number. <br> Title: Paid By, Display: true, Editable: true")]
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
		/// Check number. <br> Title: Check No., Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Check number. <br> Title: Check No., Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The CheckNum value cannot exceed 100 characters. ")]
        public string CheckNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCheckNum => CheckNum != null;

		/// <summary>
		/// Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The AuthCode value cannot exceed 100 characters. ")]
        public string AuthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAuthCode => AuthCode != null;

		/// <summary>
		/// Currency code. <br> Title: Currency, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Currency code. <br> Title: Currency, Display: true, Editable: true")]
        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCurrency => Currency != null;

		/// <summary>
		/// Realtime Exchange Rate when process transaction. <br> Title: Exchange Rate, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Realtime Exchange Rate when process transaction. <br> Title: Exchange Rate, Display: true, Editable: true")]
        public decimal? ExchangeRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasExchangeRate => ExchangeRate != null;

		/// <summary>
		/// (Readonly) Sub total amount of items. Sales amount without discount, tax and other charge. <br> Title: Subtotal, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Sub total amount of items. Sales amount without discount, tax and other charge. <br> Title: Subtotal, Display: true, Editable: false")]
        public decimal? SubTotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSubTotalAmount => SubTotalAmount != null;

		/// <summary>
		/// (Readonly) Sub Total amount deduct discount, but not include tax and other charge. <br> Title: Sales Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Sub Total amount deduct discount, but not include tax and other charge. <br> Title: Sales Amount, Display: true, Editable: false")]
        public decimal? SalesAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSalesAmount => SalesAmount != null;

		/// <summary>
		/// (Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false")]
        public decimal? TotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalAmount => TotalAmount != null;

		/// <summary>
		/// (Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false")]
        public decimal? TaxableAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxableAmount => TaxableAmount != null;

		/// <summary>
		/// (Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false")]
        public decimal? NonTaxableAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNonTaxableAmount => NonTaxableAmount != null;

		/// <summary>
		/// Invoice Tax rate. <br> Title: Tax, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice Tax rate. <br> Title: Tax, Display: true, Editable: true")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Invoice tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Invoice tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

		/// <summary>
		/// Invoice discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true")]
        public decimal? DiscountRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountRate => DiscountRate != null;

		/// <summary>
		/// Invoice discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true")]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountAmount => DiscountAmount != null;

		/// <summary>
		/// Invoice shipping fee. <br> Title: Shipping, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice shipping fee. <br> Title: Shipping, Display: true, Editable: true")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// (Readonly) tax amount for shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) tax amount for shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false")]
        public decimal? ShippingTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

		/// <summary>
		/// Invoice handling charge. <br> Title: Handling, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice handling charge. <br> Title: Handling, Display: true, Editable: true")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// (Readonly) tax amount for handling charge. <br> Title: Handling Tax, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) tax amount for handling charge. <br> Title: Handling Tax, Display: true, Editable: false")]
        public decimal? MiscTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

		/// <summary>
		/// Invoice other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// G/L Credit account. <br> Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("G/L Credit account. <br> Display: true, Editable: true")]
        public long? CreditAccount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreditAccount => CreditAccount != null;

		/// <summary>
		/// G/L Debit account. <br> Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("G/L Debit account. <br> Display: true, Editable: true")]
        public long? DebitAccount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDebitAccount => DebitAccount != null;

		/// <summary>
		/// (Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false")]
        [StringLength(100, ErrorMessage = "The TransSourceCode value cannot exceed 100 characters. ")]
        public string TransSourceCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTransSourceCode => TransSourceCode != null;

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
		/// (Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false")]
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




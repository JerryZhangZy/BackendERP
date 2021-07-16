
              
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransaction Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InvoiceTransactionDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        [StringLength(50, ErrorMessage = "The TransUuid value cannot exceed 50 characters. ")]
        public string TransUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransUuid => TransUuid != null;

        public int? TransNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransNum => TransNum != null;

        [StringLength(50, ErrorMessage = "The InvoiceUuid value cannot exceed 50 characters. ")]
        public string InvoiceUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInvoiceUuid => InvoiceUuid != null;

        public int? TransType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransType => TransType != null;

        public int? TransStatus { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransStatus => TransStatus != null;

        [DataType(DataType.DateTime)]
        public DateTime? TransDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransDate => TransDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? TransTime { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTransTime => TransTime != null;

        [StringLength(100, ErrorMessage = "The Description value cannot exceed 100 characters. ")]
        public string Description { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDescription => Description != null;

        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasNotes => Notes != null;

        public int? PaidBy { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPaidBy => PaidBy != null;

        [StringLength(50, ErrorMessage = "The BankAccountUuid value cannot exceed 50 characters. ")]
        public string BankAccountUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasBankAccountUuid => BankAccountUuid != null;

        [StringLength(100, ErrorMessage = "The CheckNum value cannot exceed 100 characters. ")]
        public string CheckNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCheckNum => CheckNum != null;

        [StringLength(100, ErrorMessage = "The AuthCode value cannot exceed 100 characters. ")]
        public string AuthCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAuthCode => AuthCode != null;

        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCurrency => Currency != null;

        public decimal? ExchangeRate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasExchangeRate => ExchangeRate != null;

        public decimal? SubTotalAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSubTotalAmount => SubTotalAmount != null;

        public decimal? SalesAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSalesAmount => SalesAmount != null;

        public decimal? TotalAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTotalAmount => TotalAmount != null;

        public decimal? TaxableAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxableAmount => TaxableAmount != null;

        public decimal? NonTaxableAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasNonTaxableAmount => NonTaxableAmount != null;

        public decimal? TaxRate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxRate => TaxRate != null;

        public decimal? TaxAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxAmount => TaxAmount != null;

        public decimal? DiscountRate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDiscountRate => DiscountRate != null;

        public decimal? DiscountAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDiscountAmount => DiscountAmount != null;

        public decimal? ShippingAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippingAmount => ShippingAmount != null;

        public decimal? ShippingTaxAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

        public decimal? MiscAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMiscAmount => MiscAmount != null;

        public decimal? MiscTaxAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

        public decimal? ChargeAndAllowanceAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

        public long? CreditAccount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCreditAccount => CreditAccount != null;

        public long? DebitAccount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDebitAccount => DebitAccount != null;

        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasEnterBy => EnterBy != null;

        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUpdateBy => UpdateBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}




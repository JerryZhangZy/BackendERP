
              
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderHeaderDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        public int? DatabaseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDatabaseNum => DatabaseNum != null;

        public int? MasterAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMasterAccountNum => MasterAccountNum != null;

        public int? ProfileNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProfileNum => ProfileNum != null;

        [StringLength(50, ErrorMessage = "The OrderUuid value cannot exceed 50 characters. ")]
        public string OrderUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderUuid => OrderUuid != null;

        [StringLength(50, ErrorMessage = "The OrderNumber value cannot exceed 50 characters. ")]
        public string OrderNumber { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderNumber => OrderNumber != null;

        public int? OrderType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderType => OrderType != null;

        public int? OrderStatus { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderStatus => OrderStatus != null;

        [DataType(DataType.DateTime)]
        public DateTime? OrderDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderDate => OrderDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? OrderTime { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderTime => OrderTime != null;

        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDueDate => DueDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? BillDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasBillDate => BillDate != null;

        [StringLength(50, ErrorMessage = "The CustomerUuid value cannot exceed 50 characters. ")]
        public string CustomerUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerUuid => CustomerUuid != null;

        [StringLength(50, ErrorMessage = "The CustomerNum value cannot exceed 50 characters. ")]
        public string CustomerNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerNum => CustomerNum != null;

        [StringLength(200, ErrorMessage = "The CustomerName value cannot exceed 200 characters. ")]
        public string CustomerName { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCustomerName => CustomerName != null;

        [StringLength(50, ErrorMessage = "The Terms value cannot exceed 50 characters. ")]
        public string Terms { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTerms => Terms != null;

        public int? TermsDays { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTermsDays => TermsDays != null;

        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCurrency => Currency != null;

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

        public decimal? PaidAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPaidAmount => PaidAmount != null;

        public decimal? CreditAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCreditAmount => CreditAmount != null;

        public decimal? Balance { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasBalance => Balance != null;

        public decimal? UnitCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUnitCost => UnitCost != null;

        public decimal? AvgCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAvgCost => AvgCost != null;

        public decimal? LotCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLotCost => LotCost != null;

        [StringLength(100, ErrorMessage = "The OrderSourceCode value cannot exceed 100 characters. ")]
        public string OrderSourceCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderSourceCode => OrderSourceCode != null;

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




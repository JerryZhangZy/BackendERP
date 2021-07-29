
              
    

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
    /// Represents a SalesOrderHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class SalesOrderHeaderDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        /// <summary>
        /// (Readonly) Database Number. <br> Display: false, Editable: false.
        /// </summary>
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiPropertyDescription("(Readonly) Database Number. <br> Display: false, Editable: false.")]
        public int? DatabaseNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDatabaseNum => DatabaseNum != null;

        /// <summary>
        /// (Readonly) Login user account. <br> Display: false, Editable: false.
        /// </summary>
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiPropertyDescription("(Readonly) Login user account. <br> Display: false, Editable: false.")]
        public int? MasterAccountNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMasterAccountNum => MasterAccountNum != null;

        /// <summary>
        /// (Readonly) Login user profile. <br> Display: false, Editable: false.
        /// </summary>
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiPropertyDescription("(Readonly) Login user profile. <br> Display: false, Editable: false.")]
        public int? ProfileNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProfileNum => ProfileNum != null;

        /// <summary>
        /// Order uuid. <br> Display: false, Editable: false.
        /// </summary>
        [OpenApiPropertyDescription("Order uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The SalesOrderUuid value cannot exceed 50 characters. ")]
        public string SalesOrderUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSalesOrderUuid => SalesOrderUuid != null;

		/// <summary>
		/// Readable order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The OrderNumber value cannot exceed 50 characters. ")]
        public string OrderNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderNumber => OrderNumber != null;

		/// <summary>
		/// Order type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order type. <br> Title: Type, Display: true, Editable: true")]
        public int? OrderType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderType => OrderType != null;

		/// <summary>
		/// Order status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order status. <br> Title: Status, Display: true, Editable: true")]
        public int? OrderStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderStatus => OrderStatus != null;

		/// <summary>
		/// Order date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderDate => OrderDate != null;

		/// <summary>
		/// Order time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderTime => OrderTime != null;

		/// <summary>
		/// (Ignore) Order due date. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Order due date. <br> Display: false, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDueDate => DueDate != null;

		/// <summary>
		/// (Ignore) Order bill date. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Order bill date. <br> Display: false, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? BillDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBillDate => BillDate != null;

		/// <summary>
		/// Customer uuid, load from customer data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Customer uuid, load from customer data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The CustomerUuid value cannot exceed 50 characters. ")]
        public string CustomerUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCustomerUuid => CustomerUuid != null;

		/// <summary>
		/// Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The CustomerCode value cannot exceed 50 characters. ")]
        public string CustomerCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCustomerCode => CustomerCode != null;

		/// <summary>
		/// (Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false")]
        [StringLength(200, ErrorMessage = "The CustomerName value cannot exceed 200 characters. ")]
        public string CustomerName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCustomerName => CustomerName != null;

		/// <summary>
		/// Payment terms, default from customer data. <br> Title: Terms, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Payment terms, default from customer data. <br> Title: Terms, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The Terms value cannot exceed 50 characters. ")]
        public string Terms { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTerms => Terms != null;

		/// <summary>
		/// Payment terms days, default from customer data. <br> Title: Days, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Payment terms days, default from customer data. <br> Title: Days, Display: true, Editable: true")]
        public int? TermsDays { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTermsDays => TermsDays != null;

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
		/// Order Tax rate. <br> Title: Tax, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order Tax rate. <br> Title: Tax, Display: true, Editable: true")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Order tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Order tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

		/// <summary>
		/// Order discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true")]
        public decimal? DiscountRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountRate => DiscountRate != null;

		/// <summary>
		/// Order discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true")]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountAmount => DiscountAmount != null;

		/// <summary>
		/// Order shipping fee. <br> Title: Shipping, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order shipping fee. <br> Title: Shipping, Display: true, Editable: true")]
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
		/// Order handling charge. <br> Title: Handling, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order handling charge. <br> Title: Handling, Display: true, Editable: true")]
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
		/// Order other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// (Ignore) Total Paid amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Total Paid amount. <br> Display: false, Editable: false")]
        public decimal? PaidAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPaidAmount => PaidAmount != null;

		/// <summary>
		/// (Ignore) Total Credit amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Total Credit amount. <br> Display: false, Editable: false")]
        public decimal? CreditAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreditAmount => CreditAmount != null;

		/// <summary>
		/// (Ignore) Current balance of Order. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Current balance of Order. <br> Display: false, Editable: false")]
        public decimal? Balance { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBalance => Balance != null;

		/// <summary>
		/// (Ignore) Total Unit Cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Total Unit Cost. <br> Display: false, Editable: false")]
        public decimal? UnitCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUnitCost => UnitCost != null;

		/// <summary>
		/// (Ignore) Total Avg.Cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Total Avg.Cost. <br> Display: false, Editable: false")]
        public decimal? AvgCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAvgCost => AvgCost != null;

		/// <summary>
		/// (Ignore) Total Lot Cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Total Lot Cost. <br> Display: false, Editable: false")]
        public decimal? LotCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotCost => LotCost != null;

		/// <summary>
		/// (Readonly) Order created from other entity number, use to prevent import duplicate order. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Order created from other entity number, use to prevent import duplicate order. <br> Title: Source Number, Display: false, Editable: false")]
        [StringLength(100, ErrorMessage = "The OrderSourceCode value cannot exceed 100 characters. ")]
        public string OrderSourceCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderSourceCode => OrderSourceCode != null;

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




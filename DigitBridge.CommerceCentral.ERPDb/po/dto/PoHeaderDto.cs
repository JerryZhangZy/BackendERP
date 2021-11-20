              
    

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
    /// Represents a PoHeader Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class PoHeaderDto
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
		/// Global Unique Guid for P/O. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for P/O. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The PoUuid value cannot exceed 50 characters. ")]
        public string PoUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoUuid => PoUuid != null;

		/// <summary>
		/// Unique in this database. <br> ProfileNum + PoNum is DigitBridgePoNum, which is global unique. <br> Title: PoNum, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Unique in this database. <br> ProfileNum + PoNum is DigitBridgePoNum, which is global unique. <br> Title: PoNum, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PoNum value cannot exceed 50 characters. ")]
        public string PoNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoNum => PoNum != null;

		/// <summary>
		/// P/O type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O type. <br> Title: Type, Display: true, Editable: true")]
        public int? PoType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoType => PoType != null;

		/// <summary>
		/// P/O status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O status. <br> Title: Status, Display: true, Editable: true")]
        public int? PoStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoStatus => PoStatus != null;

		/// <summary>
		/// P/O date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? PoDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoDate => PoDate != null;

		/// <summary>
		/// P/O time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? PoTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoTime => PoTime != null;

		/// <summary>
		/// Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? EtaShipDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEtaShipDate => EtaShipDate != null;

		/// <summary>
		/// Estimated date when item arrival to buyer . <br> Title: Arrival Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Estimated date when item arrival to buyer . <br> Title: Arrival Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? EtaArrivalDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEtaArrivalDate => EtaArrivalDate != null;

		/// <summary>
		/// Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: false, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? CancelDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelDate => CancelDate != null;

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
		/// reference Vendor Unique Guid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("reference Vendor Unique Guid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The VendorUuid value cannot exceed 50 characters. ")]
        public string VendorUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorUuid => VendorUuid != null;

		/// <summary>
		/// Vendor readable number.<br> DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Vendor readable number.<br> DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The VendorCode value cannot exceed 50 characters. ")]
        public string VendorCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorCode => VendorCode != null;

		/// <summary>
		/// Vendor name. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Vendor name. <br> Display: false, Editable: false")]
        [StringLength(200, ErrorMessage = "The VendorName value cannot exceed 200 characters. ")]
        public string VendorName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVendorName => VendorName != null;

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
		/// Sub total amount is sumary items amount. . <br> Title: Subtotal, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Sub total amount is sumary items amount. . <br> Title: Subtotal, Display: true, Editable: false")]
        public decimal? SubTotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSubTotalAmount => SubTotalAmount != null;

		/// <summary>
		/// Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation <br>(Sum of all items OrderItems
		/// </summary>
		[OpenApiPropertyDescription("Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation <br>(Sum of all items OrderItems")]
        public decimal? TotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalAmount => TotalAmount != null;

		/// <summary>
		/// Default Tax rate for P/O items. . <br> Title: Tax, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Default Tax rate for P/O items. . <br> Title: Tax, Display: true, Editable: true")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Total P/O tax amount (include shipping tax and misc tax) . <br> Title: Tax Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Total P/O tax amount (include shipping tax and misc tax) . <br> Title: Tax Amount, Display: true, Editable: false")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

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
		/// P/O level discount rate. <br> Title: Discount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O level discount rate. <br> Title: Discount, Display: true, Editable: true")]
        public decimal? DiscountRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountRate => DiscountRate != null;

		/// <summary>
		/// P/O level discount amount, base on SubTotalAmount. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O level discount amount, base on SubTotalAmount. <br> Title: Discount Amount, Display: true, Editable: true")]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountAmount => DiscountAmount != null;

		/// <summary>
		/// Total shipping fee for all items. <br> Title: Shipping, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Total shipping fee for all items. <br> Title: Shipping, Display: true, Editable: true")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// tax amount of shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("tax amount of shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false")]
        public decimal? ShippingTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

		/// <summary>
		/// P/O handling charge . <br> Title: Handling, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O handling charge . <br> Title: Handling, Display: true, Editable: true")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// tax amount of handling charge. <br> Title: Handling Tax, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("tax amount of handling charge. <br> Title: Handling Tax, Display: true, Editable: false")]
        public decimal? MiscTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

		/// <summary>
		/// P/O total Charg Allowance Amount. <br> Title: Charge&Allowance, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O total Charg Allowance Amount. <br> Title: Charge&Allowance, Display: true, Editable: true")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// P/O import or create from other entity number, use to prevent import duplicate P/O. <br> Title: Source Code, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("P/O import or create from other entity number, use to prevent import duplicate P/O. <br> Title: Source Code, Display: false, Editable: false")]
        [StringLength(100, ErrorMessage = "The PoSourceCode value cannot exceed 100 characters. ")]
        public string PoSourceCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoSourceCode => PoSourceCode != null;

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




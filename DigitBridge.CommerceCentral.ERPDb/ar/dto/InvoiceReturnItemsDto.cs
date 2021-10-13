              
    

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
    /// Represents a InvoiceReturnItems Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class InvoiceReturnItemsDto
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
		/// (Readonly) Invoice Return Item Line uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Invoice Return Item Line uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ReturnItemUuid value cannot exceed 50 characters. ")]
        public string ReturnItemUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnItemUuid => ReturnItemUuid != null;

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
		/// Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item Line sequence number. <br> Title: Line#, Display: true, Editable: false")]
        public int? Seq { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSeq => Seq != null;

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
		/// (Readonly) Invoice Item Line uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Invoice Item Line uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InvoiceItemsUuid value cannot exceed 50 characters. ")]
        public string InvoiceItemsUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceItemsUuid => InvoiceItemsUuid != null;

		/// <summary>
		/// Return item type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Return item type. <br> Title: Type, Display: true, Editable: true")]
        public int? ReturnItemType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnItemType => ReturnItemType != null;

		/// <summary>
		/// Return item status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Return item status. <br> Title: Status, Display: true, Editable: true")]
        public int? ReturnItemStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnItemStatus => ReturnItemStatus != null;

		/// <summary>
		/// Return date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Return date. <br> Title: Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnDate => ReturnDate != null;

		/// <summary>
		/// Return time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Return time. <br> Title: Time, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnTime => ReturnTime != null;

		/// <summary>
		/// Return item received date. <br> Title: Receive Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Return item received date. <br> Title: Receive Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ReceiveDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceiveDate => ReceiveDate != null;

		/// <summary>
		/// Stock Return Item Date. <br> Title: Processed Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Stock Return Item Date. <br> Title: Processed Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? StockDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockDate => StockDate != null;

		/// <summary>
		/// Product SKU. <br> Title: SKU, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product SKU. <br> Title: SKU, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSKU => SKU != null;

		/// <summary>
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductUuid => ProductUuid != null;

		/// <summary>
		/// (Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUuid => InventoryUuid != null;

		/// <summary>
		/// (Readonly) invoice Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) invoice Warehouse uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InvoiceWarehouseUuid value cannot exceed 50 characters. ")]
        public string InvoiceWarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceWarehouseUuid => InvoiceWarehouseUuid != null;

		/// <summary>
		/// Readable invoice warehouse code, load from inventory data. <br> Title: Invoice Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable invoice warehouse code, load from inventory data. <br> Title: Invoice Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The InvoiceWarehouseCode value cannot exceed 50 characters. ")]
        public string InvoiceWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceWarehouseCode => InvoiceWarehouseCode != null;

		/// <summary>
		/// (Readonly) return Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) return Warehouse uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The WarehouseUuid value cannot exceed 50 characters. ")]
        public string WarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseUuid => WarehouseUuid != null;

		/// <summary>
		/// Readable return warehouse code, load from inventory data. <br> Title: Return Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable return warehouse code, load from inventory data. <br> Title: Return Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 50 characters. ")]
        public string WarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseCode => WarehouseCode != null;

		/// <summary>
		/// Lot Number. <br> Title: Lot Number, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Lot Number. <br> Title: Lot Number, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The LotNum value cannot exceed 100 characters. ")]
        public string LotNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotNum => LotNum != null;

		/// <summary>
		/// Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true")]
        [StringLength(200, ErrorMessage = "The Reason value cannot exceed 200 characters. ")]
        public string Reason { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReason => Reason != null;

		/// <summary>
		/// Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true")]
        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// item line notes. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item line notes. <br> Title: Notes, Display: true, Editable: true")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

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
		/// (Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The UOM value cannot exceed 50 characters. ")]
        public string UOM { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUOM => UOM != null;

		/// <summary>
		/// (Ignore) Product SKU Qty pack type, for example: Case, Box, Each
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Product SKU Qty pack type, for example: Case, Box, Each")]
        [StringLength(50, ErrorMessage = "The PackType value cannot exceed 50 characters. ")]
        public string PackType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackType => PackType != null;

		/// <summary>
		/// (Ignore) Item Qty each per pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Qty each per pack.")]
        public decimal? PackQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackQty => PackQty != null;

		/// <summary>
		/// (Ignore) Item Claim return number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Claim return number of pack.")]
        public decimal? ReturnPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnPack => ReturnPack != null;

		/// <summary>
		/// (Ignore) Receive return number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Receive return number of pack.")]
        public decimal? ReceivePack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceivePack => ReceivePack != null;

		/// <summary>
		/// (Ignore) Putback stock number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Putback stock number of pack.")]
        public decimal? StockPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockPack => StockPack != null;

		/// <summary>
		/// (Ignore) Damage or not putback stock number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Damage or not putback stock number of pack.")]
        public decimal? NonStockPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNonStockPack => NonStockPack != null;

		/// <summary>
		/// Claim return Qty. <br> Title: Claim Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Claim return Qty. <br> Title: Claim Qty, Display: true, Editable: true")]
        public decimal? ReturnQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReturnQty => ReturnQty != null;

		/// <summary>
		/// Receive return qty. <br> Title: Receive Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Receive return qty. <br> Title: Receive Qty, Display: true, Editable: true")]
        public decimal? ReceiveQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceiveQty => ReceiveQty != null;

		/// <summary>
		/// Putback stock qty. <br> Title: Putback Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Putback stock qty. <br> Title: Putback Qty, Display: true, Editable: true")]
        public decimal? StockQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockQty => StockQty != null;

		/// <summary>
		/// Damage or not putback stock qty. <br> Title: Damage Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Damage or not putback stock qty. <br> Title: Damage Qty, Display: true, Editable: true")]
        public decimal? NonStockQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNonStockQty => NonStockQty != null;

		/// <summary>
		/// (Readonly) putback Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) putback Warehouse uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The PutBackWarehouseUuid value cannot exceed 50 characters. ")]
        public string PutBackWarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPutBackWarehouseUuid => PutBackWarehouseUuid != null;

		/// <summary>
		/// Readable putback warehouse code, load from inventory data. <br> Title: Putback Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable putback warehouse code, load from inventory data. <br> Title: Putback Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PutBackWarehouseCode value cannot exceed 50 characters. ")]
        public string PutBackWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPutBackWarehouseCode => PutBackWarehouseCode != null;

		/// <summary>
		/// (Readonly) Damage Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Damage Warehouse uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The DamageWarehouseUuid value cannot exceed 50 characters. ")]
        public string DamageWarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDamageWarehouseUuid => DamageWarehouseUuid != null;

		/// <summary>
		/// Readable Damage warehouse code, load from inventory data. <br> Title: Damage Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable Damage warehouse code, load from inventory data. <br> Title: Damage Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DamageWarehouseCode value cannot exceed 50 characters. ")]
        public string DamageWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDamageWarehouseCode => DamageWarehouseCode != null;

		/// <summary>
		/// Item invoice after discount price. <br> Title: Unit Price, Title: Invoice Discount Price, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item invoice after discount price. <br> Title: Unit Price, Title: Invoice Discount Price, Display: true, Editable: false")]
        public decimal? InvoiceDiscountPrice { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceDiscountPrice => InvoiceDiscountPrice != null;

		/// <summary>
		/// Item invoice discount amount. <br> Title: Item total discount amount, Title: Invoice item discount amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item invoice discount amount. <br> Title: Item total discount amount, Title: Invoice item discount amount, Display: true, Editable: false")]
        public decimal? InvoiceDiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceDiscountAmount => InvoiceDiscountAmount != null;

		/// <summary>
		/// Item return price. <br> Title: Return Price, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item return price. <br> Title: Return Price, Display: true, Editable: true")]
        public decimal? Price { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPrice => Price != null;

		/// <summary>
		/// Item total amount. <br> Title: Ext.Amount, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item total amount. <br> Title: Ext.Amount, Display: true, Editable: false")]
        public decimal? ExtAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasExtAmount => ExtAmount != null;

		/// <summary>
		/// Amount should apply tax. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Amount should apply tax. <br> Display: false, Editable: false")]
        public decimal? TaxableAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxableAmount => TaxableAmount != null;

		/// <summary>
		/// Amount should not apply tax. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Amount should not apply tax. <br> Display: false, Editable: false")]
        public decimal? NonTaxableAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNonTaxableAmount => NonTaxableAmount != null;

		/// <summary>
		/// Default Tax rate for item. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Default Tax rate for item. <br> Display: false, Editable: false")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Item level tax amount (include shipping tax and misc tax). <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level tax amount (include shipping tax and misc tax). <br> Display: false, Editable: false")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

		/// <summary>
		/// Item level Shipping fee for this item. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level Shipping fee for this item. <br> Display: false, Editable: false")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// (Readonly) Item level tax amount for shipping fee. <br> Title: Shipping Tax, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Item level tax amount for shipping fee. <br> Title: Shipping Tax, Display: false, Editable: false")]
        public decimal? ShippingTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

		/// <summary>
		/// Item level handling charge. <br> Title: Handling, Display: false, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item level handling charge. <br> Title: Handling, Display: false, Editable: true")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// (Readonly) Item level tax amount for handling charge. <br> Title: Handling Tax, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Item level tax amount for handling charge. <br> Title: Handling Tax, Display: false, Editable: false")]
        public decimal? MiscTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

		/// <summary>
		/// Item level other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: false, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item level other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: false, Editable: true")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true")]
        public bool? Stockable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockable => Stockable != null;

		/// <summary>
		/// item will add to Invoice total amount. <br> Title: A/R, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item will add to Invoice total amount. <br> Title: A/R, Display: true, Editable: true")]
        public bool? IsAr { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsAr => IsAr != null;

		/// <summary>
		/// item will apply tax. <br> Title: Taxable, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item will apply tax. <br> Title: Taxable, Display: true, Editable: true")]
        public bool? Taxable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxable => Taxable != null;

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




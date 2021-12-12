
              
    

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
    /// Represents a InvoiceItems Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class InvoiceItemsDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

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
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Invoice uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The InvoiceUuid value cannot exceed 50 characters. ")]
        public string InvoiceUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceUuid => InvoiceUuid != null;

		/// <summary>
		/// Invoice Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Invoice Item Line sequence number. <br> Title: Line#, Display: true, Editable: false")]
        public int? Seq { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSeq => Seq != null;

		/// <summary>
		/// Invoice item type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice item type. <br> Title: Type, Display: true, Editable: true")]
        public int? InvoiceItemType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceItemType => InvoiceItemType != null;

		/// <summary>
		/// Invoice item status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice item status. <br> Title: Status, Display: true, Editable: true")]
        public int? InvoiceItemStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInvoiceItemStatus => InvoiceItemStatus != null;

		/// <summary>
		/// (Ignore) Invoice date
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Invoice date")]
        [DataType(DataType.DateTime)]
        public DateTime? ItemDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasItemDate => ItemDate != null;

		/// <summary>
		/// (Ignore) Invoice time
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Invoice time")]
        [DataType(DataType.DateTime)]
        public DateTime? ItemTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasItemTime => ItemTime != null;

		/// <summary>
		/// Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? ShipDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipDate => ShipDate != null;

		/// <summary>
		/// Estimated date when item arrival to buyer. <br> Title: Delivery Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Estimated date when item arrival to buyer. <br> Title: Delivery Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? EtaArrivalDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEtaArrivalDate => EtaArrivalDate != null;

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
		/// (Readonly) Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Warehouse uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The WarehouseUuid value cannot exceed 50 characters. ")]
        public string WarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseUuid => WarehouseUuid != null;

		/// <summary>
		/// Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: true")]
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
        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// Invoice item line notes. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Invoice item line notes. <br> Title: Notes, Display: true, Editable: true")]
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
		/// (Ignore) Item Order number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Order number of pack.")]
        public decimal? OrderPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderPack => OrderPack != null;

		/// <summary>
		/// (Ignore) Item Shipped number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Shipped number of pack.")]
        public decimal? ShipPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipPack => ShipPack != null;

		/// <summary>
		/// (Ignore) Item Cancelled number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Cancelled number of pack.")]
        public decimal? CancelledPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelledPack => CancelledPack != null;

		/// <summary>
		/// (Ignore) Item Cancelled number of pack.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Cancelled number of pack.")]
        public decimal? OpenPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenPack => OpenPack != null;

		/// <summary>
		/// Item Order Qty. <br> Title: Order Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Order Qty. <br> Title: Order Qty, Display: true, Editable: true")]
        public decimal? OrderQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderQty => OrderQty != null;

		/// <summary>
		/// Item Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true")]
        public decimal? ShipQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShipQty => ShipQty != null;

		/// <summary>
		/// Item Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true")]
        public decimal? CancelledQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelledQty => CancelledQty != null;

		/// <summary>
		/// Item Back order Qty. <br> Title: Backorder, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item Back order Qty. <br> Title: Backorder, Display: true, Editable: false")]
        public decimal? OpenQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenQty => OpenQty != null;

		/// <summary>
		/// Item price rule. <br> Title: Price Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item price rule. <br> Title: Price Type, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PriceRule value cannot exceed 50 characters. ")]
        public string PriceRule { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPriceRule => PriceRule != null;

		/// <summary>
		/// Item unit price. <br> Title: Unit Price, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item unit price. <br> Title: Unit Price, Display: true, Editable: true")]
        public decimal? Price { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPrice => Price != null;

		/// <summary>
		/// Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item level discount rate. <br> Title: Discount Rate, Display: true, Editable: true")]
        public decimal? DiscountRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountRate => DiscountRate != null;

		/// <summary>
		/// Item level discount amount. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item level discount amount. <br> Title: Discount Amount, Display: true, Editable: true")]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountAmount => DiscountAmount != null;

		/// <summary>
		/// Item after discount price. <br> Title: Discount Price, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item after discount price. <br> Title: Discount Price, Display: true, Editable: false")]
        public decimal? DiscountPrice { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountPrice => DiscountPrice != null;

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
		/// Shipping fee for this item. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Shipping fee for this item. <br> Display: false, Editable: false")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// Item level tax amount of shipping fee. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level tax amount of shipping fee. <br> Display: false, Editable: false")]
        public decimal? ShippingTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

		/// <summary>
		/// Item level handling charge. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level handling charge. <br> Display: false, Editable: false")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// Item level tax amount of handling charge. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level tax amount of handling charge. <br> Display: false, Editable: false")]
        public decimal? MiscTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

		/// <summary>
		/// Item level Charge and Allowance Amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item level Charge and Allowance Amount. <br> Display: false, Editable: false")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// Item total amount include all. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item total amount include all. <br> Display: false, Editable: false")]
        public decimal? ItemTotalAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasItemTotalAmount => ItemTotalAmount != null;

		/// <summary>
		/// Item order amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item order amount. <br> Display: false, Editable: false")]
        public decimal? OrderAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderAmount => OrderAmount != null;

		/// <summary>
		/// Item cancelled amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item cancelled amount. <br> Display: false, Editable: false")]
        public decimal? CancelledAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelledAmount => CancelledAmount != null;

		/// <summary>
		/// Item backorder amount. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item backorder amount. <br> Display: false, Editable: false")]
        public decimal? OpenAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenAmount => OpenAmount != null;

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
		/// item will calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item will calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true")]
        public bool? Costable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCostable => Costable != null;

		/// <summary>
		/// item will calculate profit. <br> Title: Apply Profit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("item will calculate profit. <br> Title: Apply Profit, Display: true, Editable: true")]
        public bool? IsProfit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsProfit => IsProfit != null;

		/// <summary>
		/// (Ignore) Item Unit Cost.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Unit Cost.")]
        public decimal? UnitCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUnitCost => UnitCost != null;

		/// <summary>
		/// (Ignore) Item Avg.Cost.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Avg.Cost.")]
        public decimal? AvgCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAvgCost => AvgCost != null;

		/// <summary>
		/// (Ignore) Item Lot Cost.
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item Lot Cost.")]
        public decimal? LotCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotCost => LotCost != null;

		/// <summary>
		/// (Ignore) Lot receive Date
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Lot receive Date")]
        [DataType(DataType.DateTime)]
        public DateTime? LotInDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotInDate => LotInDate != null;

		/// <summary>
		/// (Ignore) Lot Expiration date
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Lot Expiration date")]
        [DataType(DataType.DateTime)]
        public DateTime? LotExpDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotExpDate => LotExpDate != null;

		/// <summary>
		/// (Readonly) Link to CentralOrderLineUuid in OrderLine. <br> Title: CentralOrderLineUuid, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Link to CentralOrderLineUuid in OrderLine. <br> Title: CentralOrderLineUuid, Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The CentralOrderLineUuid value cannot exceed 50 characters. ")]
        public string CentralOrderLineUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCentralOrderLineUuid => CentralOrderLineUuid != null;

		/// <summary>
		/// (Readonly) DB Channel Order Line RowID. <br> Title: Channel Order Line RowID, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) DB Channel Order Line RowID. <br> Title: Channel Order Line RowID, Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The DBChannelOrderLineRowID value cannot exceed 50 characters. ")]
        public string DBChannelOrderLineRowID { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDBChannelOrderLineRowID => DBChannelOrderLineRowID != null;

		/// <summary>
		/// (Readonly) Link to OrderDCAssignmentLineUuid in OrderDCAssignmentLine. <br> Title: CentralOrderLineUuid, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Link to OrderDCAssignmentLineUuid in OrderDCAssignmentLine. <br> Title: CentralOrderLineUuid, Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The OrderDCAssignmentLineUuid value cannot exceed 50 characters. ")]
        public string OrderDCAssignmentLineUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderDCAssignmentLineUuid => OrderDCAssignmentLineUuid != null;

		/// <summary>
		/// (Readonly) Link to OrderDCAssignmentLineNum in OrderDCAssignmentLine. <br> Title: OrderDCAssignmentLineNum, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Link to OrderDCAssignmentLineNum in OrderDCAssignmentLine. <br> Title: OrderDCAssignmentLineNum, Display: false, Editable: false")]
        public long? OrderDCAssignmentLineNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOrderDCAssignmentLineNum => OrderDCAssignmentLineNum != null;

		/// <summary>
		/// Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Sales Rep Commission Rate, Title: Commission%, Display: true, Editable: true")]
        public decimal? CommissionRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCommissionRate => CommissionRate != null;

		/// <summary>
		/// Sales Rep Commission Amount, Title: Commission, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Sales Rep Commission Amount, Title: Commission, Display: true, Editable: true")]
        public decimal? CommissionAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCommissionAmount => CommissionAmount != null;

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

		public InvoiceItemsAttributesDto InvoiceItemsAttributes { get; set; }
		[JsonIgnore, XmlIgnore, IgnoreCompare]
		[OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
		public bool HasInvoiceItemsAttributes => InvoiceItemsAttributes != null;

        #endregion Children - Generated 

        #region properties
        /// <summary>
        /// Item Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
        /// </summary>
        [OpenApiPropertyDescription("Item Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true")]
        public decimal? TotalReturnQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTotalReturnQty => TotalReturnQty != null;
        #endregion properties
    }
}




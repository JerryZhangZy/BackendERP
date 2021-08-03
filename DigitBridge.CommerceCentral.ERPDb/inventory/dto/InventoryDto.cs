
              
    

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
    /// Represents a Inventory Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class InventoryDto
    {
        [JsonIgnore]
        public long? RowNum { get; set; }

        [JsonIgnore]
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
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductUuid => ProductUuid != null;

		/// <summary>
		/// (Readonly) Inventory uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Inventory uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUuid => InventoryUuid != null;

		/// <summary>
		/// Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The StyleCode value cannot exceed 100 characters. ")]
        public string StyleCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStyleCode => StyleCode != null;

		/// <summary>
		/// Product color and pattern code. <br> Title: Color, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product color and pattern code. <br> Title: Color, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ColorPatternCode value cannot exceed 50 characters. ")]
        public string ColorPatternCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasColorPatternCode => ColorPatternCode != null;

		/// <summary>
		/// Product size type. <br> Title: Size Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product size type. <br> Title: Size Type, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The SizeType value cannot exceed 50 characters. ")]
        public string SizeType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSizeType => SizeType != null;

		/// <summary>
		/// Product size code. <br> Title: Size, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product size code. <br> Title: Size, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The SizeCode value cannot exceed 50 characters. ")]
        public string SizeCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSizeCode => SizeCode != null;

		/// <summary>
		/// Product width code. <br> Title: Width, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product width code. <br> Title: Width, Display: true, Editable: true")]
        [StringLength(30, ErrorMessage = "The WidthCode value cannot exceed 30 characters. ")]
        public string WidthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWidthCode => WidthCode != null;

		/// <summary>
		/// Product length code. <br> Title: Length, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product length code. <br> Title: Length, Display: true, Editable: true")]
        [StringLength(30, ErrorMessage = "The LengthCode value cannot exceed 30 characters. ")]
        public string LengthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLengthCode => LengthCode != null;

		/// <summary>
		/// Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PriceRule value cannot exceed 50 characters. ")]
        public string PriceRule { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPriceRule => PriceRule != null;

		/// <summary>
		/// Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true")]
        public int? LeadTimeDay { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLeadTimeDay => LeadTimeDay != null;

		/// <summary>
		/// Default P/O qty. <br> Title: Deafult P/O Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Default P/O qty. <br> Title: Deafult P/O Qty, Display: true, Editable: true")]
        public decimal? PoSize { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoSize => PoSize != null;

		/// <summary>
		/// Garantee minimal Instock in anytime. <br> Title: Min.Stock, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Garantee minimal Instock in anytime. <br> Title: Min.Stock, Display: true, Editable: true")]
        public decimal? MinStock { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMinStock => MinStock != null;

		/// <summary>
		/// Product SKU. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product SKU. load from ProductBasic data. <br> Display: false, Editable: false")]
        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSKU => SKU != null;

		/// <summary>
		/// (Readonly) Warehouse uuid, load from warehouse data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Warehouse uuid, load from warehouse data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The WarehouseUuid value cannot exceed 50 characters. ")]
        public string WarehouseUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseUuid => WarehouseUuid != null;

		/// <summary>
		/// Readable warehouse code, load from warehouse data. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Readable warehouse code, load from warehouse data. <br> Title: Warehouse Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 50 characters. ")]
        public string WarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseCode => WarehouseCode != null;

		/// <summary>
		/// (Readonly) Warehouse name, load from warehouse data. <br> Title: Warehouse Name, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Warehouse name, load from warehouse data. <br> Title: Warehouse Name, Display: true, Editable: false")]
        [StringLength(200, ErrorMessage = "The WarehouseName value cannot exceed 200 characters. ")]
        public string WarehouseName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseName => WarehouseName != null;

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
		/// Lot receive Date. <br> Title: Receive Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Lot receive Date. <br> Title: Receive Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? LotInDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotInDate => LotInDate != null;

		/// <summary>
		/// Lot Expiration date. <br> Title: Expiration Date, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Lot Expiration date. <br> Title: Expiration Date, Display: true, Editable: true")]
        [DataType(DataType.DateTime)]
        public DateTime? LotExpDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotExpDate => LotExpDate != null;

		/// <summary>
		/// Lot description. <br> Title: Lot Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Lot description. <br> Title: Lot Description, Display: true, Editable: true")]
        [StringLength(200, ErrorMessage = "The LotDescription value cannot exceed 200 characters. ")]
        public string LotDescription { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotDescription => LotDescription != null;

		/// <summary>
		/// LPN Number. <br> Title: LPN, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("LPN Number. <br> Title: LPN, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The LpnNum value cannot exceed 100 characters. ")]
        public string LpnNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLpnNum => LpnNum != null;

		/// <summary>
		/// LPN description. <br> Title: LPN Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("LPN description. <br> Title: LPN Description, Display: true, Editable: true")]
        [StringLength(200, ErrorMessage = "The LpnDescription value cannot exceed 200 characters. ")]
        public string LpnDescription { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLpnDescription => LpnDescription != null;

		/// <summary>
		/// Inventory notes. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Inventory notes. <br> Title: Notes, Display: true, Editable: true")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

		/// <summary>
		/// (Ignore) Inventory price in currency. <br> Title: Currency, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Inventory price in currency. <br> Title: Currency, Display: false, Editable: false")]
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
		/// Item Qty per Pallot. <br> Title: Qty/Pallot, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Qty per Pallot. <br> Title: Qty/Pallot, Display: true, Editable: true")]
        public decimal? QtyPerPallot { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasQtyPerPallot => QtyPerPallot != null;

		/// <summary>
		/// Item Qty per case. <br> Title: Qty/Case, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Qty per case. <br> Title: Qty/Case, Display: true, Editable: true")]
        public decimal? QtyPerCase { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasQtyPerCase => QtyPerCase != null;

		/// <summary>
		/// Item Qty per box. <br> Title: Qty/Box, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Qty per box. <br> Title: Qty/Box, Display: true, Editable: true")]
        public decimal? QtyPerBox { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasQtyPerBox => QtyPerBox != null;

		/// <summary>
		/// Product specified pack type name. <br> Title: Pack Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product specified pack type name. <br> Title: Pack Type, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PackType value cannot exceed 50 characters. ")]
        public string PackType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackType => PackType != null;

		/// <summary>
		/// Qty per each pack. <br> Title: Qty/Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Qty per each pack. <br> Title: Qty/Pack, Display: true, Editable: true")]
        public decimal? PackQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackQty => PackQty != null;

		/// <summary>
		/// Default pack type in S/O or invoice. <br> Title: Default Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Default pack type in S/O or invoice. <br> Title: Default Pack, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DefaultPackType value cannot exceed 50 characters. ")]
        public string DefaultPackType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDefaultPackType => DefaultPackType != null;

		/// <summary>
		/// Item in stock Qty. <br> Title: Instock, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item in stock Qty. <br> Title: Instock, Display: true, Editable: false")]
        public decimal? Instock { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInstock => Instock != null;

		/// <summary>
		/// (Ignore) Item On hand. <br> Title: Onhand, Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Item On hand. <br> Title: Onhand, Display: false, Editable: false")]
        public decimal? OnHand { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOnHand => OnHand != null;

		/// <summary>
		/// Open S/O qty. <br> Title: Open S/O, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Open S/O qty. <br> Title: Open S/O, Display: true, Editable: false")]
        public decimal? OpenSoQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenSoQty => OpenSoQty != null;

		/// <summary>
		/// Open Fulfillment qty. <br> Title: Open Fulfillment, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Open Fulfillment qty. <br> Title: Open Fulfillment, Display: true, Editable: false")]
        public decimal? OpenFulfillmentQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenFulfillmentQty => OpenFulfillmentQty != null;

		/// <summary>
		/// Availiable sales qty. <br> Title: Available, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Availiable sales qty. <br> Title: Available, Display: true, Editable: false")]
        public decimal? AvaQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAvaQty => AvaQty != null;

		/// <summary>
		/// Open P/O qty. <br> Title: Open P/O, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Open P/O qty. <br> Title: Open P/O, Display: true, Editable: false")]
        public decimal? OpenPoQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenPoQty => OpenPoQty != null;

		/// <summary>
		/// Open InTransit qty. <br> Title: In transit, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Open InTransit qty. <br> Title: In transit, Display: true, Editable: false")]
        public decimal? OpenInTransitQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenInTransitQty => OpenInTransitQty != null;

		/// <summary>
		/// Open Work in process qty. <br> Title: WIP, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Open Work in process qty. <br> Title: WIP, Display: true, Editable: false")]
        public decimal? OpenWipQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOpenWipQty => OpenWipQty != null;

		/// <summary>
		/// Forcasting projected qty. <br> Title: Projected, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Forcasting projected qty. <br> Title: Projected, Display: true, Editable: false")]
        public decimal? ProjectedQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProjectedQty => ProjectedQty != null;

		/// <summary>
		/// P/O receive price. <br> Title: Base Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("P/O receive price. <br> Title: Base Cost, Display: true, Editable: true")]
        public decimal? BaseCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBaseCost => BaseCost != null;

		/// <summary>
		/// Duty Tax rate. <br> Title: Duty Rate, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Duty Tax rate. <br> Title: Duty Rate, Display: true, Editable: true")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Duty tax amount. <br> Title: Duty Amount, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Duty tax amount. <br> Title: Duty Amount, Display: true, Editable: true")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

		/// <summary>
		/// Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// Handling charge. <br> Title: Handling Fee, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Handling charge. <br> Title: Handling Fee, Display: true, Editable: true")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// Other Charg or Allowance Amount. <br> Title: Charg&Allowance, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Other Charg or Allowance Amount. <br> Title: Charg&Allowance, Display: true, Editable: true")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// Unit cost include duty,and charge. <br> Title: Unit Cost, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Unit cost include duty,and charge. <br> Title: Unit Cost, Display: true, Editable: false")]
        public decimal? UnitCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUnitCost => UnitCost != null;

		/// <summary>
		/// Moving average cost. <br> Title: Avg.Cost, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Moving average cost. <br> Title: Avg.Cost, Display: true, Editable: false")]
        public decimal? AvgCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAvgCost => AvgCost != null;

		/// <summary>
		/// Item cost display for sales. <br> Title: Sales Cost, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item cost display for sales. <br> Title: Sales Cost, Display: true, Editable: false")]
        public decimal? SalesCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSalesCost => SalesCost != null;

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

		public InventoryAttributesDto InventoryAttributes { get; set; }
		[JsonIgnore, XmlIgnore, IgnoreCompare]
		[OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
		public bool HasInventoryAttributes => InventoryAttributes != null;
		
        #endregion Children - Generated 

    }
}




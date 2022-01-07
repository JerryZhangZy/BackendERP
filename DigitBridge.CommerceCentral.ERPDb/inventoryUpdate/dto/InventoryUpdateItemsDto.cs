              
    

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
    /// Represents a InventoryUpdateItems Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class InventoryUpdateItemsDto
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
		/// (Readonly) Order Item Line uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Order Item Line uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryUpdateItemsUuid value cannot exceed 50 characters. ")]
        public string InventoryUpdateItemsUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUpdateItemsUuid => InventoryUpdateItemsUuid != null;

		/// <summary>
		/// Order uuid. <br> Display: false, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("Order uuid. <br> Display: false, Editable: false.")]
        [StringLength(50, ErrorMessage = "The InventoryUpdateUuid value cannot exceed 50 characters. ")]
        public string InventoryUpdateUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUpdateUuid => InventoryUpdateUuid != null;

		/// <summary>
		/// Order Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Order Item Line sequence number. <br> Title: Line#, Display: true, Editable: false")]
        public int? Seq { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSeq => Seq != null;

		/// <summary>
		/// (Ignore) Update item date
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Update item date")]
        [DataType(DataType.DateTime)]
        public DateTime? ItemDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasItemDate => ItemDate != null;

		/// <summary>
		/// (Ignore) Update item time
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Update item time")]
        [DataType(DataType.DateTime)]
        public DateTime? ItemTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasItemTime => ItemTime != null;

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
		/// Order item line notes. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Order item line notes. <br> Title: Notes, Display: true, Editable: true")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

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
		/// Product SKU Qty pack type, for example: Case, Box, Each. <br> Title: Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product SKU Qty pack type, for example: Case, Box, Each. <br> Title: Pack, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The PackType value cannot exceed 50 characters. ")]
        public string PackType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackType => PackType != null;

		/// <summary>
		/// Item Qty each per pack. <br> Title: Qty/Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item Qty each per pack. <br> Title: Qty/Pack, Display: true, Editable: true")]
        public decimal? PackQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPackQty => PackQty != null;

		/// <summary>
		/// Item updated pack (positive/negative). <br> Title: Update Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item updated pack (positive/negative). <br> Title: Update Pack, Display: true, Editable: true")]
        public decimal? UpdatePack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdatePack => UpdatePack != null;

		/// <summary>
		/// Item count pack (use for Count only). <br> Title: Count Pack, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item count pack (use for Count only). <br> Title: Count Pack, Display: true, Editable: true")]
        public decimal? CountPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCountPack => CountPack != null;

		/// <summary>
		/// (Readonly) Instock pack before update. <br> Title: Instock Pack, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Instock pack before update. <br> Title: Instock Pack, Display: true, Editable: false")]
        public decimal? BeforeInstockPack { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeInstockPack => BeforeInstockPack != null;

		/// <summary>
		/// Item updated qty (positive/negative). <br> Title: Update Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item updated qty (positive/negative). <br> Title: Update Qty, Display: true, Editable: true")]
        public decimal? UpdateQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateQty => UpdateQty != null;

		/// <summary>
		/// Item count qty (use for Count only). <br> Title: Count Qty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Item count qty (use for Count only). <br> Title: Count Qty, Display: true, Editable: true")]
        public decimal? CountQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCountQty => CountQty != null;

		/// <summary>
		/// (Readonly) Instock before update. <br> Title: Instock, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Instock before update. <br> Title: Instock, Display: true, Editable: false")]
        public decimal? BeforeInstockQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeInstockQty => BeforeInstockQty != null;

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




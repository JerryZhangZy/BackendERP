
              
    

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
    /// Represents a InventoryLog Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InventoryLogDto
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
		/// (Readonly) Inventory log Line uuid. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Inventory log Line uuid. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryLogUuid value cannot exceed 50 characters. ")]
        public string InventoryLogUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryLogUuid => InventoryLogUuid != null;

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
		/// (Readonly) Inventory uuid. load from Inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Inventory uuid. load from Inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUuid => InventoryUuid != null;

		/// <summary>
		/// Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Batch number for log update. <br> Title: Batch Number, Display: true, Editable: false")]
        public long? BatchNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBatchNum => BatchNum != null;

		/// <summary>
		/// Log type. Which transaction to update inventory. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log type. Which transaction to update inventory. For Example: Shippment, P/O Receive, Adjust. <br> Title: Type, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The LogType value cannot exceed 50 characters. ")]
        public string LogType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogType => LogType != null;

		/// <summary>
		/// Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Transaction ID (for example: PO receive, Shhipment). <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The LogUuid value cannot exceed 50 characters. ")]
        public string LogUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogUuid => LogUuid != null;

		/// <summary>
		/// Transaction Number (for example: PO receive number, Shhipment number). <br> Title: Number, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Transaction Number (for example: PO receive number, Shhipment number). <br> Title: Number, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The LogNumber value cannot exceed 100 characters. ")]
        public string LogNumber { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogNumber => LogNumber != null;

		/// <summary>
		/// Transaction Item ID (for example: PO receive item Id, Fulfillment item Id). <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Transaction Item ID (for example: PO receive item Id, Fulfillment item Id). <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The LogItemUuid value cannot exceed 50 characters. ")]
        public string LogItemUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogItemUuid => LogItemUuid != null;

		/// <summary>
		/// Log status. <br> Title: Status, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log status. <br> Title: Status, Display: true, Editable: false")]
        public int? LogStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogStatus => LogStatus != null;

		/// <summary>
		/// Log date. <br> Title: Date, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log date. <br> Title: Date, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LogDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogDate => LogDate != null;

		/// <summary>
		/// Log time. <br> Title: Time, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log time. <br> Title: Time, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LogTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogTime => LogTime != null;

		/// <summary>
		/// Log create by. <br> Title: By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log create by. <br> Title: By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The LogBy value cannot exceed 100 characters. ")]
        public string LogBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogBy => LogBy != null;

		/// <summary>
		/// Product SKU. <br> Title: SKU, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product SKU. <br> Title: SKU, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSKU => SKU != null;

		/// <summary>
		/// Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: false")]
        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Readable warehouse code, load from inventory data. <br> Title: Warehouse Code, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The WarehouseCode value cannot exceed 50 characters. ")]
        public string WarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarehouseCode => WarehouseCode != null;

		/// <summary>
		/// Lot Number. <br> Title: Lot Number, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Lot Number. <br> Title: Lot Number, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The LotNum value cannot exceed 100 characters. ")]
        public string LotNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotNum => LotNum != null;

		/// <summary>
		/// Lot receive Date. <br> Title: Lot Receive Date, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Lot receive Date. <br> Title: Lot Receive Date, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LotInDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotInDate => LotInDate != null;

		/// <summary>
		/// Lot Expiration date. <br> Title: Lot Expiration Date, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Lot Expiration date. <br> Title: Lot Expiration Date, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? LotExpDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLotExpDate => LotExpDate != null;

		/// <summary>
		/// LPN Number. <br> Title: LPN, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("LPN Number. <br> Title: LPN, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The LpnNum value cannot exceed 100 characters. ")]
        public string LpnNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLpnNum => LpnNum != null;

		/// <summary>
		/// Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The StyleCode value cannot exceed 100 characters. ")]
        public string StyleCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStyleCode => StyleCode != null;

		/// <summary>
		/// Product color and pattern code. <br> Title: Color, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product color and pattern code. <br> Title: Color, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The ColorPatternCode value cannot exceed 50 characters. ")]
        public string ColorPatternCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasColorPatternCode => ColorPatternCode != null;

		/// <summary>
		/// Product size code. <br> Title: Size, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product size code. <br> Title: Size, Display: true, Editable: false")]
        [StringLength(50, ErrorMessage = "The SizeCode value cannot exceed 50 characters. ")]
        public string SizeCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSizeCode => SizeCode != null;

		/// <summary>
		/// Product width code. <br> Title: Width, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product width code. <br> Title: Width, Display: true, Editable: false")]
        [StringLength(30, ErrorMessage = "The WidthCode value cannot exceed 30 characters. ")]
        public string WidthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWidthCode => WidthCode != null;

		/// <summary>
		/// Product length code. <br> Title: Length, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Product length code. <br> Title: Length, Display: true, Editable: false")]
        [StringLength(30, ErrorMessage = "The LengthCode value cannot exceed 30 characters. ")]
        public string LengthCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLengthCode => LengthCode != null;

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
		/// Log Transaction Qty (>0: in, <0: out ). <br> Title: Qty, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Log Transaction Qty (>0: in, <0: out ). <br> Title: Qty, Display: true, Editable: false")]
        public decimal? LogQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLogQty => LogQty != null;

		/// <summary>
		/// Before in stock Qty. <br> Title: Instock, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Before in stock Qty. <br> Title: Instock, Display: true, Editable: false")]
        public decimal? BeforeInstock { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeInstock => BeforeInstock != null;

		/// <summary>
		/// Before base cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Before base cost. <br> Display: false, Editable: false")]
        public decimal? BeforeBaseCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeBaseCost => BeforeBaseCost != null;

		/// <summary>
		/// Before unit cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Before unit cost. <br> Display: false, Editable: false")]
        public decimal? BeforeUnitCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeUnitCost => BeforeUnitCost != null;

		/// <summary>
		/// Before avg. cost. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("Before avg. cost. <br> Display: false, Editable: false")]
        public decimal? BeforeAvgCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBeforeAvgCost => BeforeAvgCost != null;

		/// <summary>
		/// (Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}





              
    

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
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a OrderItems Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class OrderItemsDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        [StringLength(50, ErrorMessage = "The OrderItemsUuid value cannot exceed 50 characters. ")]
        public string OrderItemsUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderItemsUuid => OrderItemsUuid != null;

        [StringLength(50, ErrorMessage = "The OrderUuid value cannot exceed 50 characters. ")]
        public string OrderUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderUuid => OrderUuid != null;

        public int? Seq { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSeq => Seq != null;

        public int? OrderItemType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderItemType => OrderItemType != null;

        public int? OrderItemStatus { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderItemStatus => OrderItemStatus != null;

        [DataType(DataType.DateTime)]
        public DateTime? ItemDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasItemDate => ItemDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? ItemTime { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasItemTime => ItemTime != null;

        [DataType(DataType.DateTime)]
        public DateTime? ShipDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipDate => ShipDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? EtaArrivalDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasEtaArrivalDate => EtaArrivalDate != null;

        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSKU => SKU != null;

        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProductUuid => ProductUuid != null;

        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasInventoryUuid => InventoryUuid != null;

        [StringLength(50, ErrorMessage = "The WarehouseUuid value cannot exceed 50 characters. ")]
        public string WarehouseUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasWarehouseUuid => WarehouseUuid != null;

        [StringLength(100, ErrorMessage = "The LotNum value cannot exceed 100 characters. ")]
        public string LotNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLotNum => LotNum != null;

        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDescription => Description != null;

        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasNotes => Notes != null;

        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCurrency => Currency != null;

        [StringLength(50, ErrorMessage = "The UOM value cannot exceed 50 characters. ")]
        public string UOM { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUOM => UOM != null;

        [StringLength(50, ErrorMessage = "The PackType value cannot exceed 50 characters. ")]
        public string PackType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackType => PackType != null;

        public decimal? PackQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackQty => PackQty != null;

        public decimal? OrderPack { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderPack => OrderPack != null;

        public decimal? ShipPack { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipPack => ShipPack != null;

        public decimal? CancelledPack { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCancelledPack => CancelledPack != null;

        public decimal? OrderQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOrderQty => OrderQty != null;

        public decimal? ShipQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasShipQty => ShipQty != null;

        public decimal? CancelledQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCancelledQty => CancelledQty != null;

        [StringLength(50, ErrorMessage = "The PriceRule value cannot exceed 50 characters. ")]
        public string PriceRule { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPriceRule => PriceRule != null;

        public decimal? Price { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPrice => Price != null;

        public decimal? DiscountRate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDiscountRate => DiscountRate != null;

        public decimal? DiscountAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDiscountAmount => DiscountAmount != null;

        public decimal? DiscountPrice { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDiscountPrice => DiscountPrice != null;

        public decimal? ExtAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasExtAmount => ExtAmount != null;

        public decimal? TaxRate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxRate => TaxRate != null;

        public decimal? TaxAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxAmount => TaxAmount != null;

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

        public decimal? ItemTotalAmount { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasItemTotalAmount => ItemTotalAmount != null;

        public bool? Stockable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasStockable => Stockable != null;

        public bool? IsAr { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasIsAr => IsAr != null;

        public bool? Taxable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxable => Taxable != null;

        public bool? Costable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCostable => Costable != null;

        public decimal? UnitCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUnitCost => UnitCost != null;

        public decimal? AvgCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAvgCost => AvgCost != null;

        public decimal? LotCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLotCost => LotCost != null;

        [DataType(DataType.DateTime)]
        public DateTime? LotInDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLotInDate => LotInDate != null;

        [DataType(DataType.DateTime)]
        public DateTime? LotExpDate { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLotExpDate => LotExpDate != null;

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

		public OrderItemsAttributesDto OrderItemsAttributes { get; set; }
		[XmlIgnore, JsonIgnore, IgnoreCompare]
		public bool HasOrderItemsAttributes => OrderItemsAttributes != null;
		
        #endregion Children - Generated 

    }
}




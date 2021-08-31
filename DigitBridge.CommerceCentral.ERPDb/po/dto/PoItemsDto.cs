              
    

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
    /// Represents a PoItems Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class PoItemsDto
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
		/// Global Unique Guid for P/O Item Line
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for P/O Item Line")]
        [StringLength(50, ErrorMessage = "The PoItemUuid value cannot exceed 50 characters. ")]
        public string PoItemUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoItemUuid => PoItemUuid != null;

		/// <summary>
		/// Global Unique Guid for P/O
		/// </summary>
		[OpenApiPropertyDescription("Global Unique Guid for P/O")]
        [StringLength(50, ErrorMessage = "The PoUuid value cannot exceed 50 characters. ")]
        public string PoUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoUuid => PoUuid != null;

		/// <summary>
		/// P/O Item Line sort sequence
		/// </summary>
		[OpenApiPropertyDescription("P/O Item Line sort sequence")]
        public int? Seq { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSeq => Seq != null;

		/// <summary>
		/// P/O item type
		/// </summary>
		[OpenApiPropertyDescription("P/O item type")]
        public int? PoItemType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoItemType => PoItemType != null;

		/// <summary>
		/// P/O item status
		/// </summary>
		[OpenApiPropertyDescription("P/O item status")]
        public int? PoItemStatus { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoItemStatus => PoItemStatus != null;

		/// <summary>
		/// P/O date
		/// </summary>
		[OpenApiPropertyDescription("P/O date")]
        [DataType(DataType.DateTime)]
        public DateTime? PoDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoDate => PoDate != null;

		/// <summary>
		/// P/O time
		/// </summary>
		[OpenApiPropertyDescription("P/O time")]
        [DataType(DataType.DateTime)]
        public DateTime? PoTime { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoTime => PoTime != null;

		/// <summary>
		/// Estimated vendor ship date
		/// </summary>
		[OpenApiPropertyDescription("Estimated vendor ship date")]
        [DataType(DataType.DateTime)]
        public DateTime? EtaShipDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEtaShipDate => EtaShipDate != null;

		/// <summary>
		/// Estimated date when item arrival to buyer
		/// </summary>
		[OpenApiPropertyDescription("Estimated date when item arrival to buyer")]
        [DataType(DataType.DateTime)]
        public DateTime? EtaArrivalDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEtaArrivalDate => EtaArrivalDate != null;

		/// <summary>
		/// Usually it is related to shipping instruction
		/// </summary>
		[OpenApiPropertyDescription("Usually it is related to shipping instruction")]
        [DataType(DataType.DateTime)]
        public DateTime? CancelDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelDate => CancelDate != null;

		/// <summary>
		/// Product product uuid
		/// </summary>
		[OpenApiPropertyDescription("Product product uuid")]
        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductUuid => ProductUuid != null;

		/// <summary>
		/// Product Inventory uuid
		/// </summary>
		[OpenApiPropertyDescription("Product Inventory uuid")]
        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUuid => InventoryUuid != null;

		/// <summary>
		/// Product SKU
		/// </summary>
		[OpenApiPropertyDescription("Product SKU")]
        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSKU => SKU != null;

		/// <summary>
		/// P/O item description
		/// </summary>
		[OpenApiPropertyDescription("P/O item description")]
        [StringLength(200, ErrorMessage = "The Description value cannot exceed 200 characters. ")]
        public string Description { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDescription => Description != null;

		/// <summary>
		/// P/O item notes
		/// </summary>
		[OpenApiPropertyDescription("P/O item notes")]
        [StringLength(500, ErrorMessage = "The Notes value cannot exceed 500 characters. ")]
        public string Notes { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNotes => Notes != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(10, ErrorMessage = "The Currency value cannot exceed 10 characters. ")]
        public string Currency { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCurrency => Currency != null;

		/// <summary>
		/// Item P/O Qty.
		/// </summary>
		[OpenApiPropertyDescription("Item P/O Qty.")]
        public decimal? PoQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPoQty => PoQty != null;

		/// <summary>
		/// Item Received Qty.
		/// </summary>
		[OpenApiPropertyDescription("Item Received Qty.")]
        public decimal? ReceivedQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasReceivedQty => ReceivedQty != null;

		/// <summary>
		/// Item Cancelled Qty.
		/// </summary>
		[OpenApiPropertyDescription("Item Cancelled Qty.")]
        public decimal? CancelledQty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCancelledQty => CancelledQty != null;

		/// <summary>
		/// Item P/O price rule.
		/// </summary>
		[OpenApiPropertyDescription("Item P/O price rule.")]
        [StringLength(50, ErrorMessage = "The PriceRule value cannot exceed 50 characters. ")]
        public string PriceRule { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPriceRule => PriceRule != null;

		/// <summary>
		/// Item P/O price.
		/// </summary>
		[OpenApiPropertyDescription("Item P/O price.")]
        public decimal? Price { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPrice => Price != null;

		/// <summary>
		/// Item total amount.
		/// </summary>
		[OpenApiPropertyDescription("Item total amount.")]
        public decimal? ExtAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasExtAmount => ExtAmount != null;

		/// <summary>
		/// Default Tax rate for P/O items.
		/// </summary>
		[OpenApiPropertyDescription("Default Tax rate for P/O items.")]
        public decimal? TaxRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxRate => TaxRate != null;

		/// <summary>
		/// Total P/O tax amount (include shipping tax and misc tax)
		/// </summary>
		[OpenApiPropertyDescription("Total P/O tax amount (include shipping tax and misc tax)")]
        public decimal? TaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxAmount => TaxAmount != null;

		/// <summary>
		/// P/O level discount rate.
		/// </summary>
		[OpenApiPropertyDescription("P/O level discount rate.")]
        public decimal? DiscountRate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountRate => DiscountRate != null;

		/// <summary>
		/// P/O level discount amount, base on SubTotalAmount
		/// </summary>
		[OpenApiPropertyDescription("P/O level discount amount, base on SubTotalAmount")]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDiscountAmount => DiscountAmount != null;

		/// <summary>
		/// Total shipping fee for all items
		/// </summary>
		[OpenApiPropertyDescription("Total shipping fee for all items")]
        public decimal? ShippingAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingAmount => ShippingAmount != null;

		/// <summary>
		/// tax amount of shipping fee
		/// </summary>
		[OpenApiPropertyDescription("tax amount of shipping fee")]
        public decimal? ShippingTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShippingTaxAmount => ShippingTaxAmount != null;

		/// <summary>
		/// P/O handling charge
		/// </summary>
		[OpenApiPropertyDescription("P/O handling charge")]
        public decimal? MiscAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscAmount => MiscAmount != null;

		/// <summary>
		/// tax amount of handling charge
		/// </summary>
		[OpenApiPropertyDescription("tax amount of handling charge")]
        public decimal? MiscTaxAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMiscTaxAmount => MiscTaxAmount != null;

		/// <summary>
		/// P/O total Charg Allowance Amount
		/// </summary>
		[OpenApiPropertyDescription("P/O total Charg Allowance Amount")]
        public decimal? ChargeAndAllowanceAmount { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasChargeAndAllowanceAmount => ChargeAndAllowanceAmount != null;

		/// <summary>
		/// P/O item will update inventory instock qty
		/// </summary>
		[OpenApiPropertyDescription("P/O item will update inventory instock qty")]
        public bool? Stockable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockable => Stockable != null;

		/// <summary>
		/// P/O item will update inventory cost
		/// </summary>
		[OpenApiPropertyDescription("P/O item will update inventory cost")]
        public bool? Costable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCostable => Costable != null;

		/// <summary>
		/// P/O item will apply tax
		/// </summary>
		[OpenApiPropertyDescription("P/O item will apply tax")]
        public bool? Taxable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxable => Taxable != null;

		/// <summary>
		/// P/O item will apply to total amount
		/// </summary>
		[OpenApiPropertyDescription("P/O item will apply to total amount")]
        public bool? IsAp { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsAp => IsAp != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(100, ErrorMessage = "The EnterBy value cannot exceed 100 characters. ")]
        public string EnterBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEnterBy => EnterBy != null;

		/// <summary>
		/// 
		/// </summary>
		[OpenApiPropertyDescription("")]
        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateBy => UpdateBy != null;



        #endregion Properties - Generated 

        #region Children - Generated 

		public PoItemsAttributesDto PoItemsAttributes { get; set; }
		[JsonIgnore, XmlIgnore, IgnoreCompare]
		[OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
		public bool HasPoItemsAttributes => PoItemsAttributes != null;
		
		public PoItemsRefDto PoItemsRef { get; set; }
		[JsonIgnore, XmlIgnore, IgnoreCompare]
		[OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
		public bool HasPoItemsRef => PoItemsRef != null;
		
        #endregion Children - Generated 

    }
}



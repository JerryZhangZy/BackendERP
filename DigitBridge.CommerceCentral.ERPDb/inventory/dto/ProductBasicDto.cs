
              
    

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
    /// Represents a ProductBasic Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class ProductBasicDto
    {
        public long? RowNum { get; set; }
        [JsonIgnore,XmlIgnore]
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// (Readonly) Product Unique Number. Required, <br> Title: Product Number, Display: true, Editable: false.
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product Unique Number. Required, <br> Title: Product Number, Display: true, Editable: false.")]
        public long? CentralProductNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCentralProductNum => CentralProductNum != null;

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
		/// Product SKU. Required. <br> Title: Sku, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product SKU. Required. <br> Title: Sku, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSKU => SKU != null;

		/// <summary>
		/// Product FN SKU. <br> Title: FNSku, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product FN SKU. <br> Title: FNSku, Display: true, Editable: true")]
        [StringLength(10, ErrorMessage = "The FNSku value cannot exceed 10 characters. ")]
        public string FNSku { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasFNSku => FNSku != null;

		/// <summary>
		/// Product FN SKU. <br> Title: Condition, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product FN SKU. <br> Title: Condition, Display: true, Editable: true")]
        public bool? Condition { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCondition => Condition != null;

		/// <summary>
		/// Product Brand. <br> Title: Brand, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Brand. <br> Title: Brand, Display: true, Editable: true")]
        [StringLength(150, ErrorMessage = "The Brand value cannot exceed 150 characters. ")]
        public string Brand { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBrand => Brand != null;

		/// <summary>
		/// Product Manufacturer. <br> Title: Manufacturer, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Manufacturer. <br> Title: Manufacturer, Display: true, Editable: true")]
        [StringLength(255, ErrorMessage = "The Manufacturer value cannot exceed 255 characters. ")]
        public string Manufacturer { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasManufacturer => Manufacturer != null;

		/// <summary>
		/// Product Title. <br> Title: Title, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Title. <br> Title: Title, Display: true, Editable: true")]
        [StringLength(500, ErrorMessage = "The ProductTitle value cannot exceed 500 characters. ")]
        public string ProductTitle { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductTitle => ProductTitle != null;

		/// <summary>
		/// Product Long Description. <br> Title: Long Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Long Description. <br> Title: Long Description, Display: true, Editable: true")]
        [StringLength(2000, ErrorMessage = "The LongDescription value cannot exceed 2000 characters. ")]
        public string LongDescription { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLongDescription => LongDescription != null;

		/// <summary>
		/// Product Short Description. <br> Title: Short Description, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Short Description. <br> Title: Short Description, Display: true, Editable: true")]
        [StringLength(100, ErrorMessage = "The ShortDescription value cannot exceed 100 characters. ")]
        public string ShortDescription { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasShortDescription => ShortDescription != null;

		/// <summary>
		/// Product Subtitle. <br> Title: Subtitle, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Subtitle. <br> Title: Subtitle, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The Subtitle value cannot exceed 50 characters. ")]
        public string Subtitle { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSubtitle => Subtitle != null;

		/// <summary>
		/// Product ASIN. <br> Title: ASIN, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product ASIN. <br> Title: ASIN, Display: true, Editable: true")]
        [StringLength(10, ErrorMessage = "The ASIN value cannot exceed 10 characters. ")]
        public string ASIN { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasASIN => ASIN != null;

		/// <summary>
		/// Product UPC. <br> Title: UPC, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product UPC. <br> Title: UPC, Display: true, Editable: true")]
        [StringLength(20, ErrorMessage = "The UPC value cannot exceed 20 characters. ")]
        public string UPC { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUPC => UPC != null;

		/// <summary>
		/// Product EAN. <br> Title: EAN, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product EAN. <br> Title: EAN, Display: true, Editable: true")]
        [StringLength(20, ErrorMessage = "The EAN value cannot exceed 20 characters. ")]
        public string EAN { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasEAN => EAN != null;

		/// <summary>
		/// Product UPC. <br> Title: ISBN, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product UPC. <br> Title: ISBN, Display: true, Editable: true")]
        [StringLength(20, ErrorMessage = "The ISBN value cannot exceed 20 characters. ")]
        public string ISBN { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasISBN => ISBN != null;

		/// <summary>
		/// Product UPC. <br> Title: MPN, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product UPC. <br> Title: MPN, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The MPN value cannot exceed 50 characters. ")]
        public string MPN { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMPN => MPN != null;

		/// <summary>
		/// Product retail price. <br> Title: Default Price, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product retail price. <br> Title: Default Price, Display: true, Editable: true")]
        public decimal? Price { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasPrice => Price != null;

		/// <summary>
		/// Product display sales cost. <br> Title: Sales Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product display sales cost. <br> Title: Sales Cost, Display: true, Editable: true")]
        public decimal? Cost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCost => Cost != null;

		/// <summary>
		/// (Ignore) Product display avg. cost. <br> Title: Sales Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("(Ignore) Product display avg. cost. <br> Title: Sales Cost, Display: true, Editable: true")]
        public decimal? AvgCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAvgCost => AvgCost != null;

		/// <summary>
		/// Product MAP Price. <br> Title: MAP Price, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product MAP Price. <br> Title: MAP Price, Display: true, Editable: true")]
        public decimal? MAPPrice { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMAPPrice => MAPPrice != null;

		/// <summary>
		/// Product MSRP Price. <br> Title: MSRP, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product MSRP Price. <br> Title: MSRP, Display: true, Editable: true")]
        public decimal? MSRP { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMSRP => MSRP != null;

		/// <summary>
		/// Product is Bundle. None=0 ; BundleItem =1. <br> Title: Bundle, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product is Bundle. None=0 ; BundleItem =1. <br> Title: Bundle, Display: true, Editable: true")]
        public bool? BundleType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBundleType => BundleType != null;

		/// <summary>
		/// Product Type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Type. <br> Title: Type, Display: true, Editable: true")]
        public bool? ProductType { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductType => ProductType != null;

		/// <summary>
		/// Product Variation By Item=0 ; Child =1; Parent =2. <br> Title: VariationBy, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Variation By Item=0 ; Child =1; Parent =2. <br> Title: VariationBy, Display: true, Editable: true")]
        [StringLength(80, ErrorMessage = "The VariationVaryBy value cannot exceed 80 characters. ")]
        public string VariationVaryBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVariationVaryBy => VariationVaryBy != null;

		/// <summary>
		/// Product info need CopyToChildren. <br> Title: CopyToChildren, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product info need CopyToChildren. <br> Title: CopyToChildren, Display: true, Editable: true")]
        public bool? CopyToChildren { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCopyToChildren => CopyToChildren != null;

		/// <summary>
		/// Product include Multiple Quantity. <br> Title: MultipackQuantity, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product include Multiple Quantity. <br> Title: MultipackQuantity, Display: true, Editable: true")]
        public int? MultipackQuantity { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasMultipackQuantity => MultipackQuantity != null;

		/// <summary>
		/// Variation Parent SKU. <br> Title: Parent SKU, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Variation Parent SKU. <br> Title: Parent SKU, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The VariationParentSKU value cannot exceed 50 characters. ")]
        public string VariationParentSKU { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasVariationParentSKU => VariationParentSKU != null;

		/// <summary>
		/// IsInRelationship. <br> Title: In Relationship, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("IsInRelationship. <br> Title: In Relationship, Display: true, Editable: true")]
        public bool? IsInRelationship { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsInRelationship => IsInRelationship != null;

		/// <summary>
		/// Net Weight. <br> Title: Net Weight, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Net Weight. <br> Title: Net Weight, Display: true, Editable: true")]
        public decimal? NetWeight { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasNetWeight => NetWeight != null;

		/// <summary>
		/// Gross Weight. <br> Title: Gross Weight, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Gross Weight. <br> Title: Gross Weight, Display: true, Editable: true")]
        public decimal? GrossWeight { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasGrossWeight => GrossWeight != null;

		/// <summary>
		/// Unit measure of Weight. <br> Title: Weight Unit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Unit measure of Weight. <br> Title: Weight Unit, Display: true, Editable: true")]
        public bool? WeightUnit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWeightUnit => WeightUnit != null;

		/// <summary>
		/// Height. <br> Title: Height, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Height. <br> Title: Height, Display: true, Editable: true")]
        public decimal? ProductHeight { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductHeight => ProductHeight != null;

		/// <summary>
		/// Length. <br> Title: Length, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Length. <br> Title: Length, Display: true, Editable: true")]
        public decimal? ProductLength { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductLength => ProductLength != null;

		/// <summary>
		/// Width. <br> Title: Width, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Width. <br> Title: Width, Display: true, Editable: true")]
        public decimal? ProductWidth { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductWidth => ProductWidth != null;

		/// <summary>
		/// Box Height. <br> Title: Box Height, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Box Height. <br> Title: Box Height, Display: true, Editable: true")]
        public decimal? BoxHeight { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBoxHeight => BoxHeight != null;

		/// <summary>
		/// Box Length. <br> Title: Box Length, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Box Length. <br> Title: Box Length, Display: true, Editable: true")]
        public decimal? BoxLength { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBoxLength => BoxLength != null;

		/// <summary>
		/// Box Width. <br> Title: Box Width, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Box Width. <br> Title: Box Width, Display: true, Editable: true")]
        public decimal? BoxWidth { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasBoxWidth => BoxWidth != null;

		/// <summary>
		/// Dimension measure unit. <br> Title: Dimension Unit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Dimension measure unit. <br> Title: Dimension Unit, Display: true, Editable: true")]
        public bool? Unit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUnit => Unit != null;

		/// <summary>
		/// HarmonizedCode. <br> Title: Harmonized, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("HarmonizedCode. <br> Title: Harmonized, Display: true, Editable: true")]
        [StringLength(20, ErrorMessage = "The HarmonizedCode value cannot exceed 20 characters. ")]
        public string HarmonizedCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasHarmonizedCode => HarmonizedCode != null;

		/// <summary>
		/// TaxProductCode. <br> Title: Tax Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("TaxProductCode. <br> Title: Tax Code, Display: true, Editable: true")]
        [StringLength(25, ErrorMessage = "The TaxProductCode value cannot exceed 25 characters. ")]
        public string TaxProductCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxProductCode => TaxProductCode != null;

		/// <summary>
		/// Product Is Blocked. <br> Title: Blocked, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Is Blocked. <br> Title: Blocked, Display: true, Editable: true")]
        public bool? IsBlocked { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsBlocked => IsBlocked != null;

		/// <summary>
		/// Product Warranty. <br> Title: Warranty, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Warranty. <br> Title: Warranty, Display: true, Editable: true")]
        [StringLength(255, ErrorMessage = "The Warranty value cannot exceed 255 characters. ")]
        public string Warranty { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasWarranty => Warranty != null;

		/// <summary>
		/// (Readonly) User who created this product. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who created this product. <br> Title: Created By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The CreateBy value cannot exceed 100 characters. ")]
        public string CreateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreateBy => CreateBy != null;

		/// <summary>
		/// (Readonly) User who updated this product. <br> Title: Updated By, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) User who updated this product. <br> Title: Updated By, Display: true, Editable: false")]
        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateBy => UpdateBy != null;

		/// <summary>
		/// (Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCreateDate => CreateDate != null;

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdateDate { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasUpdateDate => UpdateDate != null;

		/// <summary>
		/// ClassificationNum. <br> Title: Classification, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("ClassificationNum. <br> Title: Classification, Display: true, Editable: true")]
        public long? ClassificationNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasClassificationNum => ClassificationNum != null;

		/// <summary>
		/// Product Original UPC. <br> Title: Original UPC, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Original UPC. <br> Title: Original UPC, Display: true, Editable: true")]
        [StringLength(20, ErrorMessage = "The OriginalUPC value cannot exceed 20 characters. ")]
        public string OriginalUPC { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOriginalUPC => OriginalUPC != null;

		/// <summary>
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductUuid => ProductUuid != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}




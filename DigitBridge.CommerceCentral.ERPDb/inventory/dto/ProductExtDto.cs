
              
    

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
    /// Represents a ProductExt Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public class ProductExtDto
    {
        public long? RowNum { get; set; }
        [JsonIgnore, XmlIgnore]
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        [JsonIgnore, XmlIgnore]
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
		/// (Readonly) Product Number. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product Number. load from ProductBasic data. <br> Display: false, Editable: false")]
        public long? CentralProductNum { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCentralProductNum => CentralProductNum != null;

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
		/// Product class code. <br> Title: Class, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product class code. <br> Title: Class, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ClassCode value cannot exceed 50 characters. ")]
        public string ClassCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasClassCode => ClassCode != null;

		/// <summary>
		/// Product sub class code. <br> Title: Sub Class, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product sub class code. <br> Title: Sub Class, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The SubClassCode value cannot exceed 50 characters. ")]
        public string SubClassCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSubClassCode => SubClassCode != null;

		/// <summary>
		/// Product department code. <br> Title: Department, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product department code. <br> Title: Department, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DepartmentCode value cannot exceed 50 characters. ")]
        public string DepartmentCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDepartmentCode => DepartmentCode != null;

		/// <summary>
		/// Product division code. <br> Title: Division, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product division code. <br> Title: Division, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DivisionCode value cannot exceed 50 characters. ")]
        public string DivisionCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDivisionCode => DivisionCode != null;

		/// <summary>
		/// Product OEM code. <br> Title: OEM, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product OEM code. <br> Title: OEM, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The OEMCode value cannot exceed 50 characters. ")]
        public string OEMCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasOEMCode => OEMCode != null;

		/// <summary>
		/// Product alternate number. <br> Title: Alt. Code, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product alternate number. <br> Title: Alt. Code, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The AlternateCode value cannot exceed 50 characters. ")]
        public string AlternateCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasAlternateCode => AlternateCode != null;

		/// <summary>
		/// Product remark. <br> Title: Remark, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product remark. <br> Title: Remark, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The Remark value cannot exceed 50 characters. ")]
        public string Remark { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasRemark => Remark != null;

		/// <summary>
		/// Product model. <br> Title: Model, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product model. <br> Title: Model, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The Model value cannot exceed 50 characters. ")]
        public string Model { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasModel => Model != null;

		/// <summary>
		/// Product in page of catalog. <br> Title: Catalog, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product in page of catalog. <br> Title: Catalog, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The CatalogPage value cannot exceed 50 characters. ")]
        public string CatalogPage { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCatalogPage => CatalogPage != null;

		/// <summary>
		/// Product Category. <br> Title: Category, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Category. <br> Title: Category, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The CategoryCode value cannot exceed 50 characters. ")]
        public string CategoryCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCategoryCode => CategoryCode != null;

		/// <summary>
		/// Product Group. <br> Title: Group, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Group. <br> Title: Group, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The GroupCode value cannot exceed 50 characters. ")]
        public string GroupCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasGroupCode => GroupCode != null;

		/// <summary>
		/// Product Sub Group. <br> Title: Sub Group, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product Sub Group. <br> Title: Sub Group, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The SubGroupCode value cannot exceed 50 characters. ")]
        public string SubGroupCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSubGroupCode => SubGroupCode != null;

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
		/// Product need calculate inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need calculate inventory instock qty. <br> Title: Stockable, Display: true, Editable: true")]
        public bool? Stockable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasStockable => Stockable != null;

		/// <summary>
		/// Product need add to Invoice sales amount amount. <br> Title: A/R, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need add to Invoice sales amount amount. <br> Title: A/R, Display: true, Editable: true")]
        public bool? IsAr { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsAr => IsAr != null;

		/// <summary>
		/// Product need add to A/P Invoice payable amount. <br> Title: A/P, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need add to A/P Invoice payable amount. <br> Title: A/P, Display: true, Editable: true")]
        public bool? IsAp { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsAp => IsAp != null;

		/// <summary>
		/// Product need apply tax. <br> Title: Taxable, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need apply tax. <br> Title: Taxable, Display: true, Editable: true")]
        public bool? Taxable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasTaxable => Taxable != null;

		/// <summary>
		/// Product need calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need calculate total cost. <br> Title: Apply Cost, Display: true, Editable: true")]
        public bool? Costable { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasCostable => Costable != null;

		/// <summary>
		/// Product need calculate profit. <br> Title: Apply Profit, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product need calculate profit. <br> Title: Apply Profit, Display: true, Editable: true")]
        public bool? IsProfit { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasIsProfit => IsProfit != null;

		/// <summary>
		/// Product is release to sales
		/// </summary>
		[OpenApiPropertyDescription("Product is release to sales")]
        public bool? Release { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasRelease => Release != null;

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
		/// Product SKU Qty unit of measure. <br> Title: UOM, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product SKU Qty unit of measure. <br> Title: UOM, Display: true, Editable: true")]
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
		/// Default Warehouse. <br> Title: Deafult Warehouse, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Default Warehouse. <br> Title: Deafult Warehouse, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DefaultWarehouseCode value cannot exceed 50 characters. ")]
        public string DefaultWarehouseCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDefaultWarehouseCode => DefaultWarehouseCode != null;

		/// <summary>
		/// Default Vendor code when make P/O. <br> Title: Deafult Warehouse, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Default Vendor code when make P/O. <br> Title: Deafult Warehouse, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The DefaultVendorCode value cannot exceed 50 characters. ")]
        public string DefaultVendorCode { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasDefaultVendorCode => DefaultVendorCode != null;

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
		/// A display cost for sales. <br> Title: Sales Cost, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("A display cost for sales. <br> Title: Sales Cost, Display: true, Editable: true")]
        public decimal? SalesCost { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasSalesCost => SalesCost != null;

		/// <summary>
		/// Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true")]
        public int? LeadTimeDay { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasLeadTimeDay => LeadTimeDay != null;

		/// <summary>
		/// Product year. <br> Title: Year od Product, Display: true, Editable: true
		/// </summary>
		[OpenApiPropertyDescription("Product year. <br> Title: Year od Product, Display: true, Editable: true")]
        [StringLength(50, ErrorMessage = "The ProductYear value cannot exceed 50 characters. ")]
        public string ProductYear { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductYear => ProductYear != null;

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

        #endregion Children - Generated 

    }
}




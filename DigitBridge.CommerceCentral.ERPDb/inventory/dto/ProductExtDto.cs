
              
    

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
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a ProductExt Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class ProductExtDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        public int? DatabaseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDatabaseNum => DatabaseNum != null;

        public int? MasterAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMasterAccountNum => MasterAccountNum != null;

        public int? ProfileNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProfileNum => ProfileNum != null;

        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProductUuid => ProductUuid != null;

        public long? CentralProductNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCentralProductNum => CentralProductNum != null;

        [StringLength(100, ErrorMessage = "The SKU value cannot exceed 100 characters. ")]
        public string SKU { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSKU => SKU != null;

        [StringLength(100, ErrorMessage = "The StyleCode value cannot exceed 100 characters. ")]
        public string StyleCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasStyleCode => StyleCode != null;

        [StringLength(50, ErrorMessage = "The ColorPatternCode value cannot exceed 50 characters. ")]
        public string ColorPatternCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasColorPatternCode => ColorPatternCode != null;

        [StringLength(50, ErrorMessage = "The SizeType value cannot exceed 50 characters. ")]
        public string SizeType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSizeType => SizeType != null;

        [StringLength(50, ErrorMessage = "The SizeCode value cannot exceed 50 characters. ")]
        public string SizeCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSizeCode => SizeCode != null;

        [StringLength(30, ErrorMessage = "The WidthCode value cannot exceed 30 characters. ")]
        public string WidthCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasWidthCode => WidthCode != null;

        [StringLength(30, ErrorMessage = "The LengthCode value cannot exceed 30 characters. ")]
        public string LengthCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLengthCode => LengthCode != null;

        [StringLength(50, ErrorMessage = "The ClassCode value cannot exceed 50 characters. ")]
        public string ClassCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasClassCode => ClassCode != null;

        [StringLength(50, ErrorMessage = "The SubClassCode value cannot exceed 50 characters. ")]
        public string SubClassCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSubClassCode => SubClassCode != null;

        [StringLength(50, ErrorMessage = "The DepartmentCode value cannot exceed 50 characters. ")]
        public string DepartmentCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDepartmentCode => DepartmentCode != null;

        [StringLength(50, ErrorMessage = "The DivisionCode value cannot exceed 50 characters. ")]
        public string DivisionCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDivisionCode => DivisionCode != null;

        [StringLength(50, ErrorMessage = "The OEMCode value cannot exceed 50 characters. ")]
        public string OEMCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOEMCode => OEMCode != null;

        [StringLength(50, ErrorMessage = "The AlternateCode value cannot exceed 50 characters. ")]
        public string AlternateCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAlternateCode => AlternateCode != null;

        [StringLength(50, ErrorMessage = "The Remark value cannot exceed 50 characters. ")]
        public string Remark { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasRemark => Remark != null;

        [StringLength(50, ErrorMessage = "The Model value cannot exceed 50 characters. ")]
        public string Model { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasModel => Model != null;

        [StringLength(50, ErrorMessage = "The CatalogPage value cannot exceed 50 characters. ")]
        public string CatalogPage { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCatalogPage => CatalogPage != null;

        [StringLength(50, ErrorMessage = "The CategoryCode value cannot exceed 50 characters. ")]
        public string CategoryCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCategoryCode => CategoryCode != null;

        [StringLength(50, ErrorMessage = "The GroupCode value cannot exceed 50 characters. ")]
        public string GroupCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasGroupCode => GroupCode != null;

        [StringLength(50, ErrorMessage = "The SubGroupCode value cannot exceed 50 characters. ")]
        public string SubGroupCode { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSubGroupCode => SubGroupCode != null;

        [StringLength(50, ErrorMessage = "The PriceRule value cannot exceed 50 characters. ")]
        public string PriceRule { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPriceRule => PriceRule != null;

        public bool? Stockable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasStockable => Stockable != null;

        public bool? IsAr { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasIsAr => IsAr != null;

        public bool? IsAp { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasIsAp => IsAp != null;

        public bool? Taxable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasTaxable => Taxable != null;

        public bool? Costable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCostable => Costable != null;

        public bool? IsProfit { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasIsProfit => IsProfit != null;

        public bool? Release { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasRelease => Release != null;

        [StringLength(50, ErrorMessage = "The UOM value cannot exceed 50 characters. ")]
        public string UOM { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUOM => UOM != null;

        public decimal? QtyPerPallot { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasQtyPerPallot => QtyPerPallot != null;

        public decimal? QtyPerCase { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasQtyPerCase => QtyPerCase != null;

        public decimal? QtyPerBox { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasQtyPerBox => QtyPerBox != null;

        [StringLength(50, ErrorMessage = "The PackType value cannot exceed 50 characters. ")]
        public string PackType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackType => PackType != null;

        public decimal? PackQty { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPackQty => PackQty != null;

        [StringLength(50, ErrorMessage = "The DefaultPackType value cannot exceed 50 characters. ")]
        public string DefaultPackType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDefaultPackType => DefaultPackType != null;

        [StringLength(50, ErrorMessage = "The DefaultWarehouseNum value cannot exceed 50 characters. ")]
        public string DefaultWarehouseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDefaultWarehouseNum => DefaultWarehouseNum != null;

        [StringLength(50, ErrorMessage = "The DefaultVendorNum value cannot exceed 50 characters. ")]
        public string DefaultVendorNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDefaultVendorNum => DefaultVendorNum != null;

        public decimal? PoSize { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasPoSize => PoSize != null;

        public decimal? MinStock { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMinStock => MinStock != null;

        public decimal? SalesCost { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSalesCost => SalesCost != null;

        public int? LeadTimeDay { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasLeadTimeDay => LeadTimeDay != null;

        [StringLength(50, ErrorMessage = "The ProductYear value cannot exceed 50 characters. ")]
        public string ProductYear { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProductYear => ProductYear != null;



        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}




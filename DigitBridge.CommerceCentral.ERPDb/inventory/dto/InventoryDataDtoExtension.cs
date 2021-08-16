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
using Newtonsoft.Json;
using CsvHelper;
using System.IO;

using Bogus;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InventoryDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InventoryDataDtoExtension
    {
        /// <summary>
        /// Merge InventoryDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">InventoryDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this InventoryDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(dto.ProductBasic.MergeName(dto.ProductExt, dto.ProductExtAttributes));
            result.Add(dto.ProductBasic.Merge(dto.ProductExt, dto.ProductExtAttributes));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this InventoryDataDto dto, bool withHeaderText = false)
        {
            //return null;
            //TODO change to merge Dto children object
            if (!dto.HasInventory)
                return null;

            var result = new List<dynamic>();
            var inventoryItems = new Inventory() { InventoryAttributes = new InventoryAttributes() };

            if (withHeaderText)
                result.Add(inventoryItems.MergeName(inventoryItems.InventoryAttributes));

            foreach (var item in dto.Inventory)
            {
                result.Add(item.Merge(item.InventoryAttributes));
            }
            return result;
        }


        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public static IList<InventoryDataDto> GetFakerData(this InventoryDataDto dto, int count)
        {
            var obj = new InventoryDataDto();
            var datas = new List<InventoryDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static InventoryDataDto GetFakerData(this InventoryDataDto dto)
        {
            var data = new InventoryDataDto();
			data.ProductBasic = new ProductBasicDto().GetFaker().Generate();
			data.ProductExt = new ProductExtDto().GetFaker().Generate();
			data.ProductExtAttributes = new ProductExtAttributesDto().GetFaker().Generate();
			data.Inventory = new InventoryDto().GetFaker().Generate(3);
			foreach (var ln in data.Inventory)
				ln.InventoryAttributes = new InventoryAttributesDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for ProductBasicDto
		/// </summary>
		/// <param name="dto">ProductBasicDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<ProductBasicDto> GetFaker(this ProductBasicDto dto)
		{
			#region faker data rules
			return new Faker<ProductBasicDto>()
				.RuleFor(u => u.CentralProductNum, f => default(long))
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.FNSku, f => f.Commerce.Product())
				.RuleFor(u => u.Condition, f => f.Random.Byte())
				.RuleFor(u => u.Brand, f => f.Company.CompanyName())
				.RuleFor(u => u.Manufacturer, f => f.Company.CompanyName())
				.RuleFor(u => u.ProductTitle, f => f.Commerce.ProductName())
				.RuleFor(u => u.LongDescription, f => f.Commerce.ProductDescription())
				.RuleFor(u => u.ShortDescription, f => f.Commerce.ProductAdjective())
				.RuleFor(u => u.Subtitle, f => f.Commerce.ProductName())
				.RuleFor(u => u.ASIN, f => f.Commerce.Ean13())
				.RuleFor(u => u.UPC, f => f.Commerce.Ean13())
				.RuleFor(u => u.EAN, f => f.Commerce.Ean8())
				.RuleFor(u => u.ISBN, f => f.Commerce.Ean13())
				.RuleFor(u => u.MPN, f => f.Commerce.Department())
				.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.Cost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MAPPrice, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MSRP, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BundleType, f => f.Random.Byte())
				.RuleFor(u => u.ProductType, f => f.Random.Byte())
				.RuleFor(u => u.VariationVaryBy, f => f.Lorem.Sentence().TruncateTo(80))
				.RuleFor(u => u.CopyToChildren, f => f.Random.Byte())
				.RuleFor(u => u.MultipackQuantity, f => f.Random.Int(1, 100))
				.RuleFor(u => u.VariationParentSKU, f => f.Commerce.Product())
				.RuleFor(u => u.IsInRelationship, f => f.Random.Byte())
				.RuleFor(u => u.NetWeight, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.GrossWeight, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.WeightUnit, f => f.Random.Byte())
				.RuleFor(u => u.ProductHeight, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ProductLength, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ProductWidth, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BoxHeight, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BoxLength, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BoxWidth, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DimensionUnit, f => f.Random.Byte())
				.RuleFor(u => u.HarmonizedCode, f => f.Lorem.Word())
				.RuleFor(u => u.TaxProductCode, f => f.Lorem.Word())
				.RuleFor(u => u.IsBlocked, f => f.Random.Byte())
				.RuleFor(u => u.Warranty, f => f.Lorem.Sentence().TruncateTo(255))
				.RuleFor(u => u.CreateBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				.RuleFor(u => u.CreateDate, f => null)
				.RuleFor(u => u.UpdateDate, f => null)
				.RuleFor(u => u.ClassificationNum, f => default(long))
				.RuleFor(u => u.OriginalUPC, f => f.Commerce.Ean13())
				.RuleFor(u => u.ProductUuid, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for ProductExtDto
		/// </summary>
		/// <param name="dto">ProductExtDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<ProductExtDto> GetFaker(this ProductExtDto dto)
		{
			#region faker data rules
			return new Faker<ProductExtDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ProductUuid, f => null)
				.RuleFor(u => u.CentralProductNum,f=>null)
				.RuleFor(u => u.SKU, f =>null)
				.RuleFor(u => u.StyleCode, f => f.Lorem.Word())
				.RuleFor(u => u.ColorPatternCode, f => f.Commerce.Color())
				.RuleFor(u => u.SizeType, f => f.Random.AlphaNumeric(10))
				.RuleFor(u => u.SizeCode, f => f.Lorem.Word())
				.RuleFor(u => u.WidthCode, f => f.Lorem.Word())
				.RuleFor(u => u.LengthCode, f => f.Lorem.Word())
				.RuleFor(u => u.ClassCode, f => f.Lorem.Word())
				.RuleFor(u => u.SubClassCode, f => f.Lorem.Word())
				.RuleFor(u => u.DepartmentCode, f => f.Lorem.Word())
				.RuleFor(u => u.DivisionCode, f => f.Lorem.Word())
				.RuleFor(u => u.OEMCode, f => f.Lorem.Word())
				.RuleFor(u => u.AlternateCode, f => f.Lorem.Word())
				.RuleFor(u => u.Remark, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.Model, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.CatalogPage, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.CategoryCode, f => f.Lorem.Word())
				.RuleFor(u => u.GroupCode, f => f.Lorem.Word())
				.RuleFor(u => u.SubGroupCode, f => f.Lorem.Word())
				.RuleFor(u => u.PriceRule, f => f.PickRandom(FakerExtension.PriceRule))
				.RuleFor(u => u.Stockable, f => f.Random.Bool())
				.RuleFor(u => u.IsAr, f => f.Random.Bool())
				.RuleFor(u => u.IsAp, f => f.Random.Bool())
				.RuleFor(u => u.Taxable, f => f.Random.Bool())
				.RuleFor(u => u.Costable, f => f.Random.Bool())
				.RuleFor(u => u.IsProfit, f => f.Random.Bool())
				.RuleFor(u => u.Release, f => f.Random.Bool())
				.RuleFor(u => u.Currency, f =>"USD")
				.RuleFor(u => u.UOM, f => f.PickRandom(FakerExtension.UOM))
				.RuleFor(u => u.QtyPerPallot, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.QtyPerCase, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.QtyPerBox, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PackType, f => f.PickRandom(FakerExtension.PackType))
				.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DefaultPackType, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.DefaultWarehouseCode, f => f.Lorem.Word())
				.RuleFor(u => u.DefaultVendorCode, f => f.Lorem.Word())
				.RuleFor(u => u.PoSize, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MinStock, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.SalesCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LeadTimeDay, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ProductYear, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for ProductExtAttributesDto
		/// </summary>
		/// <param name="dto">ProductExtAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<ProductExtAttributesDto> GetFaker(this ProductExtAttributesDto dto)
		{
			#region faker data rules
			return new Faker<ProductExtAttributesDto>()
				.RuleFor(u => u.ProductUuid, f => null)
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for InventoryDto
		/// </summary>
		/// <param name="dto">InventoryDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InventoryDto> GetFaker(this InventoryDto dto)
		{
			#region faker data rules
			return new Faker<InventoryDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ProductUuid, f => null)
				.RuleFor(u => u.InventoryUuid, f =>null)
				.RuleFor(u => u.StyleCode, f => f.Lorem.Word())
				.RuleFor(u => u.ColorPatternCode, f => f.Commerce.Color())
				.RuleFor(u => u.SizeType, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.SizeCode, f => f.Lorem.Word())
				.RuleFor(u => u.WidthCode, f => f.Lorem.Word())
				.RuleFor(u => u.LengthCode, f => f.Lorem.Word())
				.RuleFor(u => u.PriceRule, f => f.PickRandom(FakerExtension.PriceRule))
				.RuleFor(u => u.LeadTimeDay, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoSize, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MinStock, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.WarehouseCode, f => f.Lorem.Word())
				.RuleFor(u => u.WarehouseName, f => f.Company.CompanyName())
				.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.LotDescription, f => f.Commerce.ProductName())
				.RuleFor(u => u.LpnNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.LpnDescription, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.UOM, f => f.PickRandom(FakerExtension.UOM))
				.RuleFor(u => u.QtyPerPallot, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.QtyPerCase, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.QtyPerBox, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PackType, f => f.PickRandom(FakerExtension.PackType))
				.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DefaultPackType, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.Instock, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OnHand, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenSoQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenFulfillmentQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AvaQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenPoQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenInTransitQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenWipQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ProjectedQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BaseCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.SalesCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for InventoryAttributesDto
		/// </summary>
		/// <param name="dto">InventoryAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InventoryAttributesDto> GetFaker(this InventoryAttributesDto dto)
		{
			#region faker data rules
			return new Faker<InventoryAttributesDto>()
				.RuleFor(u => u.ProductUuid, f =>null)
				.RuleFor(u => u.InventoryUuid, f => null)
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}

    }
}



    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InventoryDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InventoryDataDtoMapperDefault : IDtoMapper<InventoryData, InventoryDataDto> 
    {
        #region read from dto to data

        public virtual InventoryData ReadDto(InventoryData data, InventoryDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new InventoryData();
                data.New();
            }

			if (dto.ProductBasic != null)
			{
				if (data.ProductBasic is null)
					data.ProductBasic = data.NewProductBasic();
				ReadProductBasic(data.ProductBasic, dto.ProductBasic);
			}
			if (dto.ProductExt != null)
			{
				if (data.ProductExt is null)
					data.ProductExt = data.NewProductExt();
				ReadProductExt(data.ProductExt, dto.ProductExt);
			}
			if (dto.ProductExtAttributes != null)
			{
				if (data.ProductExtAttributes is null)
					data.ProductExtAttributes = data.NewProductExtAttributes();
				ReadProductExtAttributes(data.ProductExtAttributes, dto.ProductExtAttributes);
			}
			if (dto.Inventory != null)
			{
				if (data.Inventory is null)
					data.Inventory = new List<Inventory>();
				var deleted = ReadInventory(data.Inventory, dto.Inventory);
				data.SetInventoryDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadProductBasic(ProductBasic data, ProductBasicDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasFNSku) data.FNSku = dto.FNSku;
			if (dto.HasCondition) data.Condition = dto.Condition.ToBool();
			if (dto.HasBrand) data.Brand = dto.Brand;
			if (dto.HasManufacturer) data.Manufacturer = dto.Manufacturer;
			if (dto.HasProductTitle) data.ProductTitle = dto.ProductTitle;
			if (dto.HasLongDescription) data.LongDescription = dto.LongDescription;
			if (dto.HasShortDescription) data.ShortDescription = dto.ShortDescription;
			if (dto.HasSubtitle) data.Subtitle = dto.Subtitle;
			if (dto.HasASIN) data.ASIN = dto.ASIN;
			if (dto.HasUPC) data.UPC = dto.UPC;
			if (dto.HasEAN) data.EAN = dto.EAN;
			if (dto.HasISBN) data.ISBN = dto.ISBN;
			if (dto.HasMPN) data.MPN = dto.MPN;
			if (dto.HasPrice) data.Price = dto.Price.ToDecimal();
			if (dto.HasCost) data.Cost = dto.Cost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasMAPPrice) data.MAPPrice = dto.MAPPrice.ToDecimal();
			if (dto.HasMSRP) data.MSRP = dto.MSRP.ToDecimal();
			if (dto.HasBundleType) data.BundleType = dto.BundleType.ToBool();
			if (dto.HasProductType) data.ProductType = dto.ProductType.ToBool();
			if (dto.HasVariationVaryBy) data.VariationVaryBy = dto.VariationVaryBy;
			if (dto.HasCopyToChildren) data.CopyToChildren = dto.CopyToChildren.ToBool();
			if (dto.HasMultipackQuantity) data.MultipackQuantity = dto.MultipackQuantity.ToInt();
			if (dto.HasVariationParentSKU) data.VariationParentSKU = dto.VariationParentSKU;
			if (dto.HasIsInRelationship) data.IsInRelationship = dto.IsInRelationship.ToBool();
			if (dto.HasNetWeight) data.NetWeight = dto.NetWeight.ToDecimal();
			if (dto.HasGrossWeight) data.GrossWeight = dto.GrossWeight.ToDecimal();
			if (dto.HasWeightUnit) data.WeightUnit = dto.WeightUnit.ToBool();
			if (dto.HasProductHeight) data.ProductHeight = dto.ProductHeight.ToDecimal();
			if (dto.HasProductLength) data.ProductLength = dto.ProductLength.ToDecimal();
			if (dto.HasProductWidth) data.ProductWidth = dto.ProductWidth.ToDecimal();
			if (dto.HasBoxHeight) data.BoxHeight = dto.BoxHeight.ToDecimal();
			if (dto.HasBoxLength) data.BoxLength = dto.BoxLength.ToDecimal();
			if (dto.HasBoxWidth) data.BoxWidth = dto.BoxWidth.ToDecimal();
			if (dto.HasUnit) data.Unit = dto.Unit.ToBool();
			if (dto.HasHarmonizedCode) data.HarmonizedCode = dto.HarmonizedCode;
			if (dto.HasTaxProductCode) data.TaxProductCode = dto.TaxProductCode;
			if (dto.HasIsBlocked) data.IsBlocked = dto.IsBlocked.ToBool();
			if (dto.HasWarranty) data.Warranty = dto.Warranty;
			if (dto.HasCreateBy) data.CreateBy = dto.CreateBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;
			if (dto.HasCreateDate) data.CreateDate = dto.CreateDate.ToDateTime();
			if (dto.HasUpdateDate) data.UpdateDate = dto.UpdateDate.ToDateTime();
			if (dto.HasClassificationNum) data.ClassificationNum = dto.ClassificationNum.ToLong();
			if (dto.HasOriginalUPC) data.OriginalUPC = dto.OriginalUPC;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadProductExt(ProductExt data, ProductExtDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasCentralProductNum) data.CentralProductNum = dto.CentralProductNum.ToLong();
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasStyleCode) data.StyleCode = dto.StyleCode;
			if (dto.HasColorPatternCode) data.ColorPatternCode = dto.ColorPatternCode;
			if (dto.HasSizeType) data.SizeType = dto.SizeType;
			if (dto.HasSizeCode) data.SizeCode = dto.SizeCode;
			if (dto.HasWidthCode) data.WidthCode = dto.WidthCode;
			if (dto.HasLengthCode) data.LengthCode = dto.LengthCode;
			if (dto.HasClassCode) data.ClassCode = dto.ClassCode;
			if (dto.HasSubClassCode) data.SubClassCode = dto.SubClassCode;
			if (dto.HasDepartmentCode) data.DepartmentCode = dto.DepartmentCode;
			if (dto.HasDivisionCode) data.DivisionCode = dto.DivisionCode;
			if (dto.HasOEMCode) data.OEMCode = dto.OEMCode;
			if (dto.HasAlternateCode) data.AlternateCode = dto.AlternateCode;
			if (dto.HasRemark) data.Remark = dto.Remark;
			if (dto.HasModel) data.Model = dto.Model;
			if (dto.HasCatalogPage) data.CatalogPage = dto.CatalogPage;
			if (dto.HasCategoryCode) data.CategoryCode = dto.CategoryCode;
			if (dto.HasGroupCode) data.GroupCode = dto.GroupCode;
			if (dto.HasSubGroupCode) data.SubGroupCode = dto.SubGroupCode;
			if (dto.HasPriceRule) data.PriceRule = dto.PriceRule;
			if (dto.HasStockable) data.Stockable = dto.Stockable.ToBool();
			if (dto.HasIsAr) data.IsAr = dto.IsAr.ToBool();
			if (dto.HasIsAp) data.IsAp = dto.IsAp.ToBool();
			if (dto.HasTaxable) data.Taxable = dto.Taxable.ToBool();
			if (dto.HasCostable) data.Costable = dto.Costable.ToBool();
			if (dto.HasIsProfit) data.IsProfit = dto.IsProfit.ToBool();
			if (dto.HasRelease) data.Release = dto.Release.ToBool();
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasQtyPerPallot) data.QtyPerPallot = dto.QtyPerPallot.ToDecimal();
			if (dto.HasQtyPerCase) data.QtyPerCase = dto.QtyPerCase.ToDecimal();
			if (dto.HasQtyPerBox) data.QtyPerBox = dto.QtyPerBox.ToDecimal();
			if (dto.HasPackType) data.PackType = dto.PackType;
			if (dto.HasPackQty) data.PackQty = dto.PackQty.ToDecimal();
			if (dto.HasDefaultPackType) data.DefaultPackType = dto.DefaultPackType;
			if (dto.HasDefaultWarehouseCode) data.DefaultWarehouseCode = dto.DefaultWarehouseCode;
			if (dto.HasDefaultVendorCode) data.DefaultVendorCode = dto.DefaultVendorCode;
			if (dto.HasPoSize) data.PoSize = dto.PoSize.ToDecimal();
			if (dto.HasMinStock) data.MinStock = dto.MinStock.ToDecimal();
			if (dto.HasSalesCost) data.SalesCost = dto.SalesCost.ToDecimal();
			if (dto.HasLeadTimeDay) data.LeadTimeDay = dto.LeadTimeDay.ToInt();
			if (dto.HasProductYear) data.ProductYear = dto.ProductYear;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadProductExtAttributes(ProductExtAttributes data, ProductExtAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadInventory(Inventory data, InventoryDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasStyleCode) data.StyleCode = dto.StyleCode;
			if (dto.HasColorPatternCode) data.ColorPatternCode = dto.ColorPatternCode;
			if (dto.HasSizeType) data.SizeType = dto.SizeType;
			if (dto.HasSizeCode) data.SizeCode = dto.SizeCode;
			if (dto.HasWidthCode) data.WidthCode = dto.WidthCode;
			if (dto.HasLengthCode) data.LengthCode = dto.LengthCode;
			if (dto.HasPriceRule) data.PriceRule = dto.PriceRule;
			if (dto.HasLeadTimeDay) data.LeadTimeDay = dto.LeadTimeDay.ToInt();
			if (dto.HasPoSize) data.PoSize = dto.PoSize.ToDecimal();
			if (dto.HasMinStock) data.MinStock = dto.MinStock.ToDecimal();
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasWarehouseCode) data.WarehouseCode = dto.WarehouseCode;
			if (dto.HasWarehouseName) data.WarehouseName = dto.WarehouseName;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasLotInDate) data.LotInDate = dto.LotInDate;
			if (dto.HasLotExpDate) data.LotExpDate = dto.LotExpDate;
			if (dto.HasLotDescription) data.LotDescription = dto.LotDescription;
			if (dto.HasLpnNum) data.LpnNum = dto.LpnNum;
			if (dto.HasLpnDescription) data.LpnDescription = dto.LpnDescription;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasQtyPerPallot) data.QtyPerPallot = dto.QtyPerPallot.ToDecimal();
			if (dto.HasQtyPerCase) data.QtyPerCase = dto.QtyPerCase.ToDecimal();
			if (dto.HasQtyPerBox) data.QtyPerBox = dto.QtyPerBox.ToDecimal();
			if (dto.HasPackType) data.PackType = dto.PackType;
			if (dto.HasPackQty) data.PackQty = dto.PackQty.ToDecimal();
			if (dto.HasDefaultPackType) data.DefaultPackType = dto.DefaultPackType;
			if (dto.HasInstock) data.Instock = dto.Instock.ToDecimal();
			if (dto.HasOnHand) data.OnHand = dto.OnHand.ToDecimal();
			if (dto.HasOpenSoQty) data.OpenSoQty = dto.OpenSoQty.ToDecimal();
			if (dto.HasOpenFulfillmentQty) data.OpenFulfillmentQty = dto.OpenFulfillmentQty.ToDecimal();
			if (dto.HasAvaQty) data.AvaQty = dto.AvaQty.ToDecimal();
			if (dto.HasOpenPoQty) data.OpenPoQty = dto.OpenPoQty.ToDecimal();
			if (dto.HasOpenInTransitQty) data.OpenInTransitQty = dto.OpenInTransitQty.ToDecimal();
			if (dto.HasOpenWipQty) data.OpenWipQty = dto.OpenWipQty.ToDecimal();
			if (dto.HasProjectedQty) data.ProjectedQty = dto.ProjectedQty.ToDecimal();
			if (dto.HasBaseCost) data.BaseCost = dto.BaseCost.ToDecimal();
			if (dto.HasTaxRate) data.TaxRate = dto.TaxRate;
			if (dto.HasTaxAmount) data.TaxAmount = dto.TaxAmount;
			if (dto.HasShippingAmount) data.ShippingAmount = dto.ShippingAmount;
			if (dto.HasMiscAmount) data.MiscAmount = dto.MiscAmount;
			if (dto.HasChargeAndAllowanceAmount) data.ChargeAndAllowanceAmount = dto.ChargeAndAllowanceAmount;
			if (dto.HasUnitCost) data.UnitCost = dto.UnitCost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasSalesCost) data.SalesCost = dto.SalesCost.ToDecimal();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			#region read all grand children object

			if (dto.InventoryAttributes != null)
			{
				if (data.InventoryAttributes is null)
					data.InventoryAttributes = new InventoryAttributes();
				ReadInventoryAttributes(data.InventoryAttributes, dto.InventoryAttributes);
			}

			#endregion read all grand children object

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<Inventory> ReadInventory(IList<Inventory> data, IList<InventoryDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<Inventory>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.InventoryUuid == itemDto.InventoryUuid);
				if (obj is null)
					obj = new Inventory().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadInventory(obj, itemDto);

			}
			return lstOrig;
		}

		protected virtual void ReadInventoryAttributes(InventoryAttributes data, InventoryAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			return;
		}


        #endregion read from dto to data

        #region write to dto from data

        public virtual InventoryDataDto WriteDto(InventoryData data, InventoryDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new InventoryDataDto();

            data.CheckIntegrity();

			if (data.ProductBasic != null)
			{
				dto.ProductBasic = new ProductBasicDto();
				WriteProductBasic(data.ProductBasic, dto.ProductBasic);
			}
			if (data.ProductExt != null)
			{
				dto.ProductExt = new ProductExtDto();
				WriteProductExt(data.ProductExt, dto.ProductExt);
			}
			if (data.ProductExtAttributes != null)
			{
				dto.ProductExtAttributes = new ProductExtAttributesDto();
				WriteProductExtAttributes(data.ProductExtAttributes, dto.ProductExtAttributes);
			}
			if (data.Inventory != null)
			{
				dto.Inventory = new List<InventoryDto>();
				WriteInventory(data.Inventory, dto.Inventory);
			}
            return dto;
        }

		protected virtual void WriteProductBasic(ProductBasic data, ProductBasicDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.CentralProductNum = data.CentralProductNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.SKU = data.SKU;
			dto.FNSku = data.FNSku;
			dto.Condition = data.Condition;
			dto.Brand = data.Brand;
			dto.Manufacturer = data.Manufacturer;
			dto.ProductTitle = data.ProductTitle;
			dto.LongDescription = data.LongDescription;
			dto.ShortDescription = data.ShortDescription;
			dto.Subtitle = data.Subtitle;
			dto.ASIN = data.ASIN;
			dto.UPC = data.UPC;
			dto.EAN = data.EAN;
			dto.ISBN = data.ISBN;
			dto.MPN = data.MPN;
			dto.Price = data.Price;
			dto.Cost = data.Cost;
			dto.AvgCost = data.AvgCost;
			dto.MAPPrice = data.MAPPrice;
			dto.MSRP = data.MSRP;
			dto.BundleType = data.BundleType;
			dto.ProductType = data.ProductType;
			dto.VariationVaryBy = data.VariationVaryBy;
			dto.CopyToChildren = data.CopyToChildren;
			dto.MultipackQuantity = data.MultipackQuantity;
			dto.VariationParentSKU = data.VariationParentSKU;
			dto.IsInRelationship = data.IsInRelationship;
			dto.NetWeight = data.NetWeight;
			dto.GrossWeight = data.GrossWeight;
			dto.WeightUnit = data.WeightUnit;
			dto.ProductHeight = data.ProductHeight;
			dto.ProductLength = data.ProductLength;
			dto.ProductWidth = data.ProductWidth;
			dto.BoxHeight = data.BoxHeight;
			dto.BoxLength = data.BoxLength;
			dto.BoxWidth = data.BoxWidth;
			dto.Unit = data.Unit;
			dto.HarmonizedCode = data.HarmonizedCode;
			dto.TaxProductCode = data.TaxProductCode;
			dto.IsBlocked = data.IsBlocked;
			dto.Warranty = data.Warranty;
			dto.CreateBy = data.CreateBy;
			dto.UpdateBy = data.UpdateBy;
			dto.CreateDate = data.CreateDate;
			dto.UpdateDate = data.UpdateDate;
			dto.ClassificationNum = data.ClassificationNum;
			dto.RowNum = data.RowNum;
			dto.OriginalUPC = data.OriginalUPC;
			dto.ProductUuid = data.ProductUuid;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteProductExt(ProductExt data, ProductExtDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ProductUuid = data.ProductUuid;
			dto.CentralProductNum = data.CentralProductNum;
			dto.SKU = data.SKU;
			dto.StyleCode = data.StyleCode;
			dto.ColorPatternCode = data.ColorPatternCode;
			dto.SizeType = data.SizeType;
			dto.SizeCode = data.SizeCode;
			dto.WidthCode = data.WidthCode;
			dto.LengthCode = data.LengthCode;
			dto.ClassCode = data.ClassCode;
			dto.SubClassCode = data.SubClassCode;
			dto.DepartmentCode = data.DepartmentCode;
			dto.DivisionCode = data.DivisionCode;
			dto.OEMCode = data.OEMCode;
			dto.AlternateCode = data.AlternateCode;
			dto.Remark = data.Remark;
			dto.Model = data.Model;
			dto.CatalogPage = data.CatalogPage;
			dto.CategoryCode = data.CategoryCode;
			dto.GroupCode = data.GroupCode;
			dto.SubGroupCode = data.SubGroupCode;
			dto.PriceRule = data.PriceRule;
			dto.Stockable = data.Stockable;
			dto.IsAr = data.IsAr;
			dto.IsAp = data.IsAp;
			dto.Taxable = data.Taxable;
			dto.Costable = data.Costable;
			dto.IsProfit = data.IsProfit;
			dto.Release = data.Release;
			dto.Currency = data.Currency;
			dto.UOM = data.UOM;
			dto.QtyPerPallot = data.QtyPerPallot;
			dto.QtyPerCase = data.QtyPerCase;
			dto.QtyPerBox = data.QtyPerBox;
			dto.PackType = data.PackType;
			dto.PackQty = data.PackQty;
			dto.DefaultPackType = data.DefaultPackType;
			dto.DefaultWarehouseCode = data.DefaultWarehouseCode;
			dto.DefaultVendorCode = data.DefaultVendorCode;
			dto.PoSize = data.PoSize;
			dto.MinStock = data.MinStock;
			dto.SalesCost = data.SalesCost;
			dto.LeadTimeDay = data.LeadTimeDay;
			dto.ProductYear = data.ProductYear;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteProductExtAttributes(ProductExtAttributes data, ProductExtAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.ProductUuid = data.ProductUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteInventory(Inventory data, InventoryDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.StyleCode = data.StyleCode;
			dto.ColorPatternCode = data.ColorPatternCode;
			dto.SizeType = data.SizeType;
			dto.SizeCode = data.SizeCode;
			dto.WidthCode = data.WidthCode;
			dto.LengthCode = data.LengthCode;
			dto.PriceRule = data.PriceRule;
			dto.LeadTimeDay = data.LeadTimeDay;
			dto.PoSize = data.PoSize;
			dto.MinStock = data.MinStock;
			dto.SKU = data.SKU;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.WarehouseCode = data.WarehouseCode;
			dto.WarehouseName = data.WarehouseName;
			dto.LotNum = data.LotNum;
			dto.LotInDate = data.LotInDate;
			dto.LotExpDate = data.LotExpDate;
			dto.LotDescription = data.LotDescription;
			dto.LpnNum = data.LpnNum;
			dto.LpnDescription = data.LpnDescription;
			dto.Notes = data.Notes;
			dto.Currency = data.Currency;
			dto.UOM = data.UOM;
			dto.QtyPerPallot = data.QtyPerPallot;
			dto.QtyPerCase = data.QtyPerCase;
			dto.QtyPerBox = data.QtyPerBox;
			dto.PackType = data.PackType;
			dto.PackQty = data.PackQty;
			dto.DefaultPackType = data.DefaultPackType;
			dto.Instock = data.Instock;
			dto.OnHand = data.OnHand;
			dto.OpenSoQty = data.OpenSoQty;
			dto.OpenFulfillmentQty = data.OpenFulfillmentQty;
			dto.AvaQty = data.AvaQty;
			dto.OpenPoQty = data.OpenPoQty;
			dto.OpenInTransitQty = data.OpenInTransitQty;
			dto.OpenWipQty = data.OpenWipQty;
			dto.ProjectedQty = data.ProjectedQty;
			dto.BaseCost = data.BaseCost;
			dto.TaxRate = data.TaxRate;
			dto.TaxAmount = data.TaxAmount;
			dto.ShippingAmount = data.ShippingAmount;
			dto.MiscAmount = data.MiscAmount;
			dto.ChargeAndAllowanceAmount = data.ChargeAndAllowanceAmount;
			dto.UnitCost = data.UnitCost;
			dto.AvgCost = data.AvgCost;
			dto.SalesCost = data.SalesCost;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			#region write all grand children object

			if (data.InventoryAttributes != null)
			{
				dto.InventoryAttributes = new InventoryAttributesDto();
				WriteInventoryAttributes(data.InventoryAttributes, dto.InventoryAttributes);
			}

			#endregion write all grand children object

			return;
		}
		protected virtual void WriteInventory(IList<Inventory> data, IList<InventoryDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new InventoryDto();
				dto.Add(obj);
				WriteInventory(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WriteInventoryAttributes(InventoryAttributes data, InventoryAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

        #endregion write to dto from data

    }
}




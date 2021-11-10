    

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
    /// Represents a PoTransactionDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class PoTransactionDataDtoMapperDefault : IDtoMapper<PoTransactionData, PoTransactionDataDto> 
    {
        #region read from dto to data

        public virtual PoTransactionData ReadDto(PoTransactionData data, PoTransactionDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new PoTransactionData();
                data.New();
            }

			if (dto.PoTransaction != null)
			{
				if (data.PoTransaction is null)
					data.PoTransaction = data.NewPoTransaction();
				ReadPoTransaction(data.PoTransaction, dto.PoTransaction);
			}
			if (dto.PoTransactionItems != null)
			{
				if (data.PoTransactionItems is null)
					data.PoTransactionItems = new List<PoTransactionItems>();
				var deleted = ReadPoTransactionItems(data.PoTransactionItems, dto.PoTransactionItems);
				data.SetPoTransactionItemsDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadPoTransaction(PoTransaction data, PoTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasTransNum) data.TransNum = dto.TransNum.ToInt();
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasPoNum) data.PoNum = dto.PoNum;
			if (dto.HasTransType) data.TransType = dto.TransType;
			if (dto.HasTransStatus) data.TransStatus = dto.TransStatus;
			if (dto.HasTransDate) data.TransDate = dto.TransDate.ToDateTime();
			if (dto.HasTransTime) data.TransTime = dto.TransTime.ToTimeSpan();
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasVendorUuid) data.VendorUuid = dto.VendorUuid;
			if (dto.HasVendorCode) data.VendorCode = dto.VendorCode;
			if (dto.HasVendorName) data.VendorName = dto.VendorName;
			if (dto.HasVendorInvoiceNum) data.VendorInvoiceNum = dto.VendorInvoiceNum;
			if (dto.HasVendorInvoiceDate) data.VendorInvoiceDate = dto.VendorInvoiceDate;
			if (dto.HasDueDate) data.DueDate = dto.DueDate;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasSubTotalAmount) data.SubTotalAmount = dto.SubTotalAmount.ToDecimal();
			if (dto.HasTotalAmount) data.TotalAmount = dto.TotalAmount.ToDecimal();
			if (dto.HasTaxRate) data.TaxRate = dto.TaxRate;
			if (dto.HasTaxAmount) data.TaxAmount = dto.TaxAmount;
			if (dto.HasDiscountRate) data.DiscountRate = dto.DiscountRate;
			if (dto.HasDiscountAmount) data.DiscountAmount = dto.DiscountAmount;
			if (dto.HasShippingAmount) data.ShippingAmount = dto.ShippingAmount;
			if (dto.HasShippingTaxAmount) data.ShippingTaxAmount = dto.ShippingTaxAmount;
			if (dto.HasMiscAmount) data.MiscAmount = dto.MiscAmount;
			if (dto.HasMiscTaxAmount) data.MiscTaxAmount = dto.MiscTaxAmount;
			if (dto.HasChargeAndAllowanceAmount) data.ChargeAndAllowanceAmount = dto.ChargeAndAllowanceAmount;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadPoTransactionItems(PoTransactionItems data, PoTransactionItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasTransItemUuid) data.TransItemUuid = dto.TransItemUuid;
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasPoItemUuid) data.PoItemUuid = dto.PoItemUuid;
			if (dto.HasItemType) data.ItemType = dto.ItemType;
			if (dto.HasItemStatus) data.ItemStatus = dto.ItemStatus;
			if (dto.HasItemDate) data.ItemDate = dto.ItemDate.ToDateTime();
			if (dto.HasItemTime) data.ItemTime = dto.ItemTime.ToTimeSpan();
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasLotDescription) data.LotDescription = dto.LotDescription;
			if (dto.HasLotInDate) data.LotInDate = dto.LotInDate;
			if (dto.HasLotExpDate) data.LotExpDate = dto.LotExpDate;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasTransQty) data.TransQty = dto.TransQty.ToDecimal();
			if (dto.HasPrice) data.Price = dto.Price.ToDecimal();
			if (dto.HasExtAmount) data.ExtAmount = dto.ExtAmount.ToDecimal();
			if (dto.HasTaxRate) data.TaxRate = dto.TaxRate;
			if (dto.HasTaxAmount) data.TaxAmount = dto.TaxAmount;
			if (dto.HasDiscountRate) data.DiscountRate = dto.DiscountRate;
			if (dto.HasDiscountAmount) data.DiscountAmount = dto.DiscountAmount;
			if (dto.HasShippingAmount) data.ShippingAmount = dto.ShippingAmount;
			if (dto.HasShippingTaxAmount) data.ShippingTaxAmount = dto.ShippingTaxAmount;
			if (dto.HasMiscAmount) data.MiscAmount = dto.MiscAmount;
			if (dto.HasMiscTaxAmount) data.MiscTaxAmount = dto.MiscTaxAmount;
			if (dto.HasChargeAndAllowanceAmount) data.ChargeAndAllowanceAmount = dto.ChargeAndAllowanceAmount;
			if (dto.HasStockable) data.Stockable = dto.Stockable.ToBool();
			if (dto.HasIsAp) data.IsAp = dto.IsAp.ToBool();
			if (dto.HasTaxable) data.Taxable = dto.Taxable.ToBool();
			if (dto.HasCostable) data.Costable = dto.Costable.ToBool();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<PoTransactionItems> ReadPoTransactionItems(IList<PoTransactionItems> data, IList<PoTransactionItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<PoTransactionItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = (itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.TransItemUuid == itemDto.TransItemUuid)) ?? lstOrig.Find(x => x.PoItemUuid == itemDto.PoItemUuid);
				if (obj is null)
					obj = new PoTransactionItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadPoTransactionItems(obj, itemDto);

			}
			return lstOrig;
		}



        #endregion read from dto to data

        #region write to dto from data

        public virtual PoTransactionDataDto WriteDto(PoTransactionData data, PoTransactionDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new PoTransactionDataDto();

            data.CheckIntegrity();

			if (data.PoTransaction != null)
			{
				dto.PoTransaction = new PoTransactionDto();
				WritePoTransaction(data.PoTransaction, dto.PoTransaction);
			}
			if (data.PoTransactionItems != null)
			{
				dto.PoTransactionItems = new List<PoTransactionItemsDto>();
				WritePoTransactionItems(data.PoTransactionItems, dto.PoTransactionItems);
			}
            return dto;
        }

		protected virtual void WritePoTransaction(PoTransaction data, PoTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.TransUuid = data.TransUuid;
			dto.TransNum = data.TransNum;
			dto.PoUuid = data.PoUuid;
			dto.PoNum = data.PoNum;
			dto.TransType = data.TransType;
			dto.TransStatus = data.TransStatus;
			dto.TransDate = data.TransDate;
			dto.TransTime = data.TransTime.ToDateTime();
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.VendorUuid = data.VendorUuid;
			dto.VendorCode = data.VendorCode;
			dto.VendorName = data.VendorName;
			dto.VendorInvoiceNum = data.VendorInvoiceNum;
			dto.VendorInvoiceDate = data.VendorInvoiceDate;
			dto.DueDate = data.DueDate;
			dto.Currency = data.Currency;
			dto.SubTotalAmount = data.SubTotalAmount;
			dto.TotalAmount = data.TotalAmount;
			dto.TaxRate = data.TaxRate;
			dto.TaxAmount = data.TaxAmount;
			dto.DiscountRate = data.DiscountRate;
			dto.DiscountAmount = data.DiscountAmount;
			dto.ShippingAmount = data.ShippingAmount;
			dto.ShippingTaxAmount = data.ShippingTaxAmount;
			dto.MiscAmount = data.MiscAmount;
			dto.MiscTaxAmount = data.MiscTaxAmount;
			dto.ChargeAndAllowanceAmount = data.ChargeAndAllowanceAmount;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WritePoTransactionItems(PoTransactionItems data, PoTransactionItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.TransItemUuid = data.TransItemUuid;
			dto.TransUuid = data.TransUuid;
			dto.Seq = data.Seq;
			dto.PoUuid = data.PoUuid;
			dto.PoItemUuid = data.PoItemUuid;
			dto.ItemType = data.ItemType;
			dto.ItemStatus = data.ItemStatus;
			dto.ItemDate = data.ItemDate;
			dto.ItemTime = data.ItemTime.ToDateTime();
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.SKU = data.SKU;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.LotNum = data.LotNum;
			dto.LotDescription = data.LotDescription;
			dto.LotInDate = data.LotInDate;
			dto.LotExpDate = data.LotExpDate;
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.Currency = data.Currency;
			dto.UOM = data.UOM;
			dto.TransQty = data.TransQty;
			dto.Price = data.Price;
			dto.ExtAmount = data.ExtAmount;
			dto.TaxRate = data.TaxRate;
			dto.TaxAmount = data.TaxAmount;
			dto.DiscountRate = data.DiscountRate;
			dto.DiscountAmount = data.DiscountAmount;
			dto.ShippingAmount = data.ShippingAmount;
			dto.ShippingTaxAmount = data.ShippingTaxAmount;
			dto.MiscAmount = data.MiscAmount;
			dto.MiscTaxAmount = data.MiscTaxAmount;
			dto.ChargeAndAllowanceAmount = data.ChargeAndAllowanceAmount;
			dto.Stockable = data.Stockable;
			dto.IsAp = data.IsAp;
			dto.Taxable = data.Taxable;
			dto.Costable = data.Costable;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;
			
			dto.PoQty = data.PoQty;
			dto.ReceivedQty = data.ReceivedQty;
			dto.OpenQty = data.OpenQty;

			#endregion read properties

			return;
		}
		protected virtual void WritePoTransactionItems(IList<PoTransactionItems> data, IList<PoTransactionItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new PoTransactionItemsDto();
				dto.Add(obj);
				WritePoTransactionItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}



        #endregion write to dto from data

    }
}




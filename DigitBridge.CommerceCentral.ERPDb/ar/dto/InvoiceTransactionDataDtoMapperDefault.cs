

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
	/// Represents a InvoiceTransactionDataDtoMapperDefault Class.
	/// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
	/// </summary>
	public class InvoiceTransactionDataDtoMapperDefault : IDtoMapper<InvoiceTransactionData, InvoiceTransactionDataDto>
	{
		#region read from dto to data

		public virtual InvoiceTransactionData ReadDto(InvoiceTransactionData data, InvoiceTransactionDataDto dto)
		{
			if (dto is null)
				return data;
			if (data is null)
			{
				data = new InvoiceTransactionData();
				data.New();
			}

			if (dto.InvoiceTransaction != null)
			{
				if (data.InvoiceTransaction is null)
					data.InvoiceTransaction = data.NewInvoiceTransaction();
				ReadInvoiceTransaction(data.InvoiceTransaction, dto.InvoiceTransaction);
			}
			if (dto.InvoiceReturnItems != null)
			{
				if (data.InvoiceReturnItems is null)
					data.InvoiceReturnItems = new List<InvoiceReturnItems>();
				var deleted = ReadInvoiceReturnItems(data.InvoiceReturnItems, dto.InvoiceReturnItems);
				data.SetInvoiceReturnItemsDeleted(deleted);
			}

			data.CheckIntegrity();
			return data;
		}

		public virtual void ReadInvoiceTransaction(InvoiceTransaction data, InvoiceTransactionDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasTransNum) data.TransNum = dto.TransNum.ToInt();
			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasInvoiceNumber) data.InvoiceNumber = dto.InvoiceNumber;
			if (dto.HasTransType) data.TransType = dto.TransType.ToInt();
			if (dto.HasTransStatus) data.TransStatus = dto.TransStatus.ToInt();
			if (dto.HasTransDate) data.TransDate = dto.TransDate.ToDateTime();
			if (dto.HasTransTime) data.TransTime = dto.TransTime.ToTimeSpan();
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasPaidBy) data.PaidBy = dto.PaidBy.ToInt();
			if (dto.HasBankAccountUuid) data.BankAccountUuid = dto.BankAccountUuid;
			if (dto.HasBankAccountCode) data.BankAccountCode = dto.BankAccountCode;
			if (dto.HasCheckNum) data.CheckNum = dto.CheckNum;
			if (dto.HasAuthCode) data.AuthCode = dto.AuthCode;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasExchangeRate) data.ExchangeRate = dto.ExchangeRate.ToDecimal();
			if (dto.HasSubTotalAmount) data.SubTotalAmount = dto.SubTotalAmount.ToDecimal();
			if (dto.HasSalesAmount) data.SalesAmount = dto.SalesAmount.ToDecimal();
			if (dto.HasTotalAmount) data.TotalAmount = dto.TotalAmount.ToDecimal();
			if (dto.HasTaxableAmount) data.TaxableAmount = dto.TaxableAmount.ToDecimal();
			if (dto.HasNonTaxableAmount) data.NonTaxableAmount = dto.NonTaxableAmount.ToDecimal();
			if (dto.HasTaxRate) data.TaxRate = dto.TaxRate.ToDecimal();
			if (dto.HasTaxAmount) data.TaxAmount = dto.TaxAmount.ToDecimal();
			if (dto.HasDiscountRate) data.DiscountRate = dto.DiscountRate.ToDecimal();
			if (dto.HasDiscountAmount) data.DiscountAmount = dto.DiscountAmount.ToDecimal();
			if (dto.HasShippingAmount) data.ShippingAmount = dto.ShippingAmount.ToDecimal();
			if (dto.HasShippingTaxAmount) data.ShippingTaxAmount = dto.ShippingTaxAmount.ToDecimal();
			if (dto.HasMiscAmount) data.MiscAmount = dto.MiscAmount.ToDecimal();
			if (dto.HasMiscTaxAmount) data.MiscTaxAmount = dto.MiscTaxAmount.ToDecimal();
			if (dto.HasChargeAndAllowanceAmount) data.ChargeAndAllowanceAmount = dto.ChargeAndAllowanceAmount.ToDecimal();
			if (dto.HasCreditAccount) data.CreditAccount = dto.CreditAccount.ToLong();
			if (dto.HasDebitAccount) data.DebitAccount = dto.DebitAccount.ToLong();
			if (dto.HasTransSourceCode) data.TransSourceCode = dto.TransSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadInvoiceReturnItems(InvoiceReturnItems data, InvoiceReturnItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasReturnItemUuid) data.ReturnItemUuid = dto.ReturnItemUuid;
			if (dto.HasTransUuid) data.TransUuid = dto.TransUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasInvoiceItemsUuid) data.InvoiceItemsUuid = dto.InvoiceItemsUuid;
			if (dto.HasReturnItemType) data.ReturnItemType = dto.ReturnItemType.ToInt();
			if (dto.HasReturnItemStatus) data.ReturnItemStatus = dto.ReturnItemStatus.ToInt();
			if (dto.HasReturnDate) data.ReturnDate = dto.ReturnDate.ToDateTime();
			if (dto.HasReturnTime) data.ReturnTime = dto.ReturnTime.ToTimeSpan();
			if (dto.HasReceiveDate) data.ReceiveDate = dto.ReceiveDate;
			if (dto.HasStockDate) data.StockDate = dto.StockDate;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasInvoiceWarehouseUuid) data.InvoiceWarehouseUuid = dto.InvoiceWarehouseUuid;
			if (dto.HasInvoiceWarehouseCode) data.InvoiceWarehouseCode = dto.InvoiceWarehouseCode;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasWarehouseCode) data.WarehouseCode = dto.WarehouseCode;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasReason) data.Reason = dto.Reason;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasPackType) data.PackType = dto.PackType;
			if (dto.HasPackQty) data.PackQty = dto.PackQty.ToDecimal();
			if (dto.HasReturnPack) data.ReturnPack = dto.ReturnPack.ToDecimal();
			if (dto.HasReceivePack) data.ReceivePack = dto.ReceivePack.ToDecimal();
			if (dto.HasStockPack) data.StockPack = dto.StockPack.ToDecimal();
			if (dto.HasNonStockPack) data.NonStockPack = dto.NonStockPack.ToDecimal();
			if (dto.HasReturnQty) data.ReturnQty = dto.ReturnQty.ToDecimal();
			if (dto.HasReceiveQty) data.ReceiveQty = dto.ReceiveQty.ToDecimal();
			if (dto.HasStockQty) data.StockQty = dto.StockQty.ToDecimal();
			if (dto.HasNonStockQty) data.NonStockQty = dto.NonStockQty.ToDecimal();
			if (dto.HasPutBackWarehouseUuid) data.PutBackWarehouseUuid = dto.PutBackWarehouseUuid;
			if (dto.HasPutBackWarehouseCode) data.PutBackWarehouseCode = dto.PutBackWarehouseCode;
			if (dto.HasDamageWarehouseUuid) data.DamageWarehouseUuid = dto.DamageWarehouseUuid;
			if (dto.HasDamageWarehouseCode) data.DamageWarehouseCode = dto.DamageWarehouseCode;
			if (dto.HasInvoiceDiscountPrice) data.InvoiceDiscountPrice = dto.InvoiceDiscountPrice.ToDecimal();
			if (dto.HasPrice) data.Price = dto.Price.ToDecimal();
			if (dto.HasExtAmount) data.ExtAmount = dto.ExtAmount.ToDecimal();
			if (dto.HasTaxableAmount) data.TaxableAmount = dto.TaxableAmount.ToDecimal();
			if (dto.HasNonTaxableAmount) data.NonTaxableAmount = dto.NonTaxableAmount.ToDecimal();
			if (dto.HasTaxRate) data.TaxRate = dto.TaxRate.ToDecimal();
			if (dto.HasTaxAmount) data.TaxAmount = dto.TaxAmount.ToDecimal();
			if (dto.HasShippingAmount) data.ShippingAmount = dto.ShippingAmount.ToDecimal();
			if (dto.HasShippingTaxAmount) data.ShippingTaxAmount = dto.ShippingTaxAmount.ToDecimal();
			if (dto.HasMiscAmount) data.MiscAmount = dto.MiscAmount.ToDecimal();
			if (dto.HasMiscTaxAmount) data.MiscTaxAmount = dto.MiscTaxAmount.ToDecimal();
			if (dto.HasChargeAndAllowanceAmount) data.ChargeAndAllowanceAmount = dto.ChargeAndAllowanceAmount.ToDecimal();
			if (dto.HasStockable) data.Stockable = dto.Stockable.ToBool();
			if (dto.HasIsAr) data.IsAr = dto.IsAr.ToBool();
			if (dto.HasTaxable) data.Taxable = dto.Taxable.ToBool();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<InvoiceReturnItems> ReadInvoiceReturnItems(IList<InvoiceReturnItems> data, IList<InvoiceReturnItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<InvoiceReturnItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.ReturnItemUuid == itemDto.ReturnItemUuid);
				if (obj is null)
					obj = new InvoiceReturnItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadInvoiceReturnItems(obj, itemDto);

			}
			return lstOrig;
		}



		#endregion read from dto to data

		#region write to dto from data

		public virtual InvoiceTransactionDataDto WriteDto(InvoiceTransactionData data, InvoiceTransactionDataDto dto)
		{
			if (data is null)
				return null;
			if (dto is null)
				dto = new InvoiceTransactionDataDto();

			data.CheckIntegrity();

			if (data.InvoiceTransaction != null)
			{
				dto.InvoiceTransaction = new InvoiceTransactionDto();
				WriteInvoiceTransaction(data.InvoiceTransaction, dto.InvoiceTransaction);
			}
			if (data.InvoiceReturnItems != null)
			{
				dto.InvoiceReturnItems = new List<InvoiceReturnItemsDto>();
				WriteInvoiceReturnItems(data.InvoiceReturnItems, dto.InvoiceReturnItems);
			}
			return dto;
		}

		public virtual void WriteInvoiceTransaction(InvoiceTransaction data, InvoiceTransactionDto dto)
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
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.InvoiceNumber = data.InvoiceNumber;
			dto.TransType = data.TransType;
			dto.TransStatus = data.TransStatus;
			dto.TransDate = data.TransDate;
			dto.TransTime = data.TransTime.ToDateTime();
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.PaidBy = data.PaidBy;
			dto.BankAccountUuid = data.BankAccountUuid;
			dto.BankAccountCode = data.BankAccountCode;
			dto.CheckNum = data.CheckNum;
			dto.AuthCode = data.AuthCode;
			dto.Currency = data.Currency;
			dto.ExchangeRate = data.ExchangeRate;
			dto.SubTotalAmount = data.SubTotalAmount;
			dto.SalesAmount = data.SalesAmount;
			dto.TotalAmount = data.TotalAmount;
			dto.TaxableAmount = data.TaxableAmount;
			dto.NonTaxableAmount = data.NonTaxableAmount;
			dto.TaxRate = data.TaxRate;
			dto.TaxAmount = data.TaxAmount;
			dto.DiscountRate = data.DiscountRate;
			dto.DiscountAmount = data.DiscountAmount;
			dto.ShippingAmount = data.ShippingAmount;
			dto.ShippingTaxAmount = data.ShippingTaxAmount;
			dto.MiscAmount = data.MiscAmount;
			dto.MiscTaxAmount = data.MiscTaxAmount;
			dto.ChargeAndAllowanceAmount = data.ChargeAndAllowanceAmount;
			dto.CreditAccount = data.CreditAccount;
			dto.DebitAccount = data.DebitAccount;
			dto.TransSourceCode = data.TransSourceCode;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteInvoiceReturnItems(InvoiceReturnItems data, InvoiceReturnItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.ReturnItemUuid = data.ReturnItemUuid;
			dto.TransUuid = data.TransUuid;
			dto.Seq = data.Seq;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.InvoiceItemsUuid = data.InvoiceItemsUuid;
			dto.ReturnItemType = data.ReturnItemType;
			dto.ReturnItemStatus = data.ReturnItemStatus;
			dto.ReturnDate = data.ReturnDate;
			dto.ReturnTime = data.ReturnTime.ToDateTime();
			dto.ReceiveDate = data.ReceiveDate;
			dto.StockDate = data.StockDate;
			dto.SKU = data.SKU;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.InvoiceWarehouseUuid = data.InvoiceWarehouseUuid;
			dto.InvoiceWarehouseCode = data.InvoiceWarehouseCode;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.WarehouseCode = data.WarehouseCode;
			dto.LotNum = data.LotNum;
			dto.Reason = data.Reason;
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.Currency = data.Currency;
			dto.UOM = data.UOM;
			dto.PackType = data.PackType;
			dto.PackQty = data.PackQty;
			dto.ReturnPack = data.ReturnPack;
			dto.ReceivePack = data.ReceivePack;
			dto.StockPack = data.StockPack;
			dto.NonStockPack = data.NonStockPack;
			dto.ReturnQty = data.ReturnQty;
			dto.ReceiveQty = data.ReceiveQty;
			dto.StockQty = data.StockQty;
			dto.NonStockQty = data.NonStockQty;
			dto.PutBackWarehouseUuid = data.PutBackWarehouseUuid;
			dto.PutBackWarehouseCode = data.PutBackWarehouseCode;
			dto.DamageWarehouseUuid = data.DamageWarehouseUuid;
			dto.DamageWarehouseCode = data.DamageWarehouseCode;
			dto.InvoiceDiscountPrice = data.InvoiceDiscountPrice;
			dto.Price = data.Price;
			dto.ExtAmount = data.ExtAmount;
			dto.TaxableAmount = data.TaxableAmount;
			dto.NonTaxableAmount = data.NonTaxableAmount;
			dto.TaxRate = data.TaxRate;
			dto.TaxAmount = data.TaxAmount;
			dto.ShippingAmount = data.ShippingAmount;
			dto.ShippingTaxAmount = data.ShippingTaxAmount;
			dto.MiscAmount = data.MiscAmount;
			dto.MiscTaxAmount = data.MiscTaxAmount;
			dto.ChargeAndAllowanceAmount = data.ChargeAndAllowanceAmount;
			dto.Stockable = data.Stockable;
			dto.IsAr = data.IsAr;
			dto.Taxable = data.Taxable;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WriteInvoiceReturnItems(IList<InvoiceReturnItems> data, IList<InvoiceReturnItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new InvoiceReturnItemsDto();
				dto.Add(obj);
				WriteInvoiceReturnItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}



		#endregion write to dto from data

	}
}




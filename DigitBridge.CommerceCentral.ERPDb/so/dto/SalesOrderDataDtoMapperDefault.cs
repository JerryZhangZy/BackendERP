    

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
    /// Represents a SalesOrderDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class SalesOrderDataDtoMapperDefault : IDtoMapper<SalesOrderData, SalesOrderDataDto> 
    {
        #region read from dto to data

        public virtual SalesOrderData ReadDto(SalesOrderData data, SalesOrderDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new SalesOrderData();
                data.New();
            }

			if (dto.SalesOrderHeader != null)
			{
				if (data.SalesOrderHeader is null)
					data.SalesOrderHeader = data.NewSalesOrderHeader();
				ReadSalesOrderHeader(data.SalesOrderHeader, dto.SalesOrderHeader);
			}
			if (dto.SalesOrderHeaderInfo != null)
			{
				if (data.SalesOrderHeaderInfo is null)
					data.SalesOrderHeaderInfo = data.NewSalesOrderHeaderInfo();
				ReadSalesOrderHeaderInfo(data.SalesOrderHeaderInfo, dto.SalesOrderHeaderInfo);
			}
			if (dto.SalesOrderHeaderAttributes != null)
			{
				if (data.SalesOrderHeaderAttributes is null)
					data.SalesOrderHeaderAttributes = data.NewSalesOrderHeaderAttributes();
				ReadSalesOrderHeaderAttributes(data.SalesOrderHeaderAttributes, dto.SalesOrderHeaderAttributes);
			}
			if (dto.SalesOrderItems != null)
			{
				if (data.SalesOrderItems is null)
					data.SalesOrderItems = new List<SalesOrderItems>();
				var deleted = ReadSalesOrderItems(data.SalesOrderItems, dto.SalesOrderItems);
				data.SetSalesOrderItemsDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadSalesOrderHeader(SalesOrderHeader data, SalesOrderHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasOrderNumber) data.OrderNumber = dto.OrderNumber;
			if (dto.HasOrderType) data.OrderType = dto.OrderType.ToInt();
			if (dto.HasOrderStatus) data.OrderStatus = dto.OrderStatus.ToInt();
			if (dto.HasOrderDate) data.OrderDate = dto.OrderDate.ToDateTime();
			if (dto.HasOrderTime) data.OrderTime = dto.OrderTime.ToTimeSpan();
			if (dto.HasDueDate) data.DueDate = dto.DueDate;
			if (dto.HasBillDate) data.BillDate = dto.BillDate;
			if (dto.HasCustomerUuid) data.CustomerUuid = dto.CustomerUuid;
			if (dto.HasCustomerNum) data.CustomerNum = dto.CustomerNum;
			if (dto.HasCustomerName) data.CustomerName = dto.CustomerName;
			if (dto.HasTerms) data.Terms = dto.Terms;
			if (dto.HasTermsDays) data.TermsDays = dto.TermsDays.ToInt();
			if (dto.HasCurrency) data.Currency = dto.Currency;
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
			if (dto.HasPaidAmount) data.PaidAmount = dto.PaidAmount.ToDecimal();
			if (dto.HasCreditAmount) data.CreditAmount = dto.CreditAmount.ToDecimal();
			if (dto.HasBalance) data.Balance = dto.Balance.ToDecimal();
			if (dto.HasUnitCost) data.UnitCost = dto.UnitCost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasLotCost) data.LotCost = dto.LotCost.ToDecimal();
			if (dto.HasOrderSourceCode) data.OrderSourceCode = dto.OrderSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			return;
		}


		protected virtual void ReadSalesOrderHeaderInfo(SalesOrderHeaderInfo data, SalesOrderHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasCentralFulfillmentNum) data.CentralFulfillmentNum = dto.CentralFulfillmentNum.ToLong();
			if (dto.HasShippingCarrier) data.ShippingCarrier = dto.ShippingCarrier;
			if (dto.HasShippingClass) data.ShippingClass = dto.ShippingClass;
			if (dto.HasDistributionCenterNum) data.DistributionCenterNum = dto.DistributionCenterNum.ToInt();
			if (dto.HasCentralOrderNum) data.CentralOrderNum = dto.CentralOrderNum.ToLong();
			if (dto.HasChannelNum) data.ChannelNum = dto.ChannelNum.ToInt();
			if (dto.HasChannelAccountNum) data.ChannelAccountNum = dto.ChannelAccountNum.ToInt();
			if (dto.HasChannelOrderID) data.ChannelOrderID = dto.ChannelOrderID;
			if (dto.HasSecondaryChannelOrderID) data.SecondaryChannelOrderID = dto.SecondaryChannelOrderID;
			if (dto.HasShippingAccount) data.ShippingAccount = dto.ShippingAccount;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasWarehouseCode) data.WarehouseCode = dto.WarehouseCode;
			if (dto.HasRefNum) data.RefNum = dto.RefNum;
			if (dto.HasCustomerPoNum) data.CustomerPoNum = dto.CustomerPoNum;
			if (dto.HasEndBuyerUserID) data.EndBuyerUserID = dto.EndBuyerUserID;
			if (dto.HasEndBuyerName) data.EndBuyerName = dto.EndBuyerName;
			if (dto.HasEndBuyerEmail) data.EndBuyerEmail = dto.EndBuyerEmail;
			if (dto.HasShipToName) data.ShipToName = dto.ShipToName;
			if (dto.HasShipToFirstName) data.ShipToFirstName = dto.ShipToFirstName;
			if (dto.HasShipToLastName) data.ShipToLastName = dto.ShipToLastName;
			if (dto.HasShipToSuffix) data.ShipToSuffix = dto.ShipToSuffix;
			if (dto.HasShipToCompany) data.ShipToCompany = dto.ShipToCompany;
			if (dto.HasShipToCompanyJobTitle) data.ShipToCompanyJobTitle = dto.ShipToCompanyJobTitle;
			if (dto.HasShipToAttention) data.ShipToAttention = dto.ShipToAttention;
			if (dto.HasShipToAddressLine1) data.ShipToAddressLine1 = dto.ShipToAddressLine1;
			if (dto.HasShipToAddressLine2) data.ShipToAddressLine2 = dto.ShipToAddressLine2;
			if (dto.HasShipToAddressLine3) data.ShipToAddressLine3 = dto.ShipToAddressLine3;
			if (dto.HasShipToCity) data.ShipToCity = dto.ShipToCity;
			if (dto.HasShipToState) data.ShipToState = dto.ShipToState;
			if (dto.HasShipToStateFullName) data.ShipToStateFullName = dto.ShipToStateFullName;
			if (dto.HasShipToPostalCode) data.ShipToPostalCode = dto.ShipToPostalCode;
			if (dto.HasShipToPostalCodeExt) data.ShipToPostalCodeExt = dto.ShipToPostalCodeExt;
			if (dto.HasShipToCounty) data.ShipToCounty = dto.ShipToCounty;
			if (dto.HasShipToCountry) data.ShipToCountry = dto.ShipToCountry;
			if (dto.HasShipToEmail) data.ShipToEmail = dto.ShipToEmail;
			if (dto.HasShipToDaytimePhone) data.ShipToDaytimePhone = dto.ShipToDaytimePhone;
			if (dto.HasShipToNightPhone) data.ShipToNightPhone = dto.ShipToNightPhone;
			if (dto.HasBillToName) data.BillToName = dto.BillToName;
			if (dto.HasBillToFirstName) data.BillToFirstName = dto.BillToFirstName;
			if (dto.HasBillToLastName) data.BillToLastName = dto.BillToLastName;
			if (dto.HasBillToSuffix) data.BillToSuffix = dto.BillToSuffix;
			if (dto.HasBillToCompany) data.BillToCompany = dto.BillToCompany;
			if (dto.HasBillToCompanyJobTitle) data.BillToCompanyJobTitle = dto.BillToCompanyJobTitle;
			if (dto.HasBillToAttention) data.BillToAttention = dto.BillToAttention;
			if (dto.HasBillToAddressLine1) data.BillToAddressLine1 = dto.BillToAddressLine1;
			if (dto.HasBillToAddressLine2) data.BillToAddressLine2 = dto.BillToAddressLine2;
			if (dto.HasBillToAddressLine3) data.BillToAddressLine3 = dto.BillToAddressLine3;
			if (dto.HasBillToCity) data.BillToCity = dto.BillToCity;
			if (dto.HasBillToState) data.BillToState = dto.BillToState;
			if (dto.HasBillToStateFullName) data.BillToStateFullName = dto.BillToStateFullName;
			if (dto.HasBillToPostalCode) data.BillToPostalCode = dto.BillToPostalCode;
			if (dto.HasBillToPostalCodeExt) data.BillToPostalCodeExt = dto.BillToPostalCodeExt;
			if (dto.HasBillToCounty) data.BillToCounty = dto.BillToCounty;
			if (dto.HasBillToCountry) data.BillToCountry = dto.BillToCountry;
			if (dto.HasBillToEmail) data.BillToEmail = dto.BillToEmail;
			if (dto.HasBillToDaytimePhone) data.BillToDaytimePhone = dto.BillToDaytimePhone;
			if (dto.HasBillToNightPhone) data.BillToNightPhone = dto.BillToNightPhone;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			return;
		}


		protected virtual void ReadSalesOrderHeaderAttributes(SalesOrderHeaderAttributes data, SalesOrderHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			return;
		}


		protected virtual void ReadSalesOrderItems(SalesOrderItems data, SalesOrderItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasSalesOrderItemsUuid) data.SalesOrderItemsUuid = dto.SalesOrderItemsUuid;
			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasOrderItemType) data.OrderItemType = dto.OrderItemType.ToInt();
			if (dto.HasSalesOrderItemstatus) data.SalesOrderItemstatus = dto.SalesOrderItemstatus.ToInt();
			if (dto.HasItemDate) data.ItemDate = dto.ItemDate.ToDateTime();
			if (dto.HasItemTime) data.ItemTime = dto.ItemTime.ToTimeSpan();
			if (dto.HasShipDate) data.ShipDate = dto.ShipDate;
			if (dto.HasEtaArrivalDate) data.EtaArrivalDate = dto.EtaArrivalDate;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasWarehouseCode) data.WarehouseCode = dto.WarehouseCode;
			if (dto.HasLotNum) data.LotNum = dto.LotNum;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasUOM) data.UOM = dto.UOM;
			if (dto.HasPackType) data.PackType = dto.PackType;
			if (dto.HasPackQty) data.PackQty = dto.PackQty.ToDecimal();
			if (dto.HasOrderPack) data.OrderPack = dto.OrderPack.ToDecimal();
			if (dto.HasShipPack) data.ShipPack = dto.ShipPack.ToDecimal();
			if (dto.HasCancelledPack) data.CancelledPack = dto.CancelledPack.ToDecimal();
			if (dto.HasOrderQty) data.OrderQty = dto.OrderQty.ToDecimal();
			if (dto.HasShipQty) data.ShipQty = dto.ShipQty.ToDecimal();
			if (dto.HasCancelledQty) data.CancelledQty = dto.CancelledQty.ToDecimal();
			if (dto.HasOpenQty) data.OpenQty = dto.OpenQty.ToDecimal();
			if (dto.HasPriceRule) data.PriceRule = dto.PriceRule;
			if (dto.HasPrice) data.Price = dto.Price.ToDecimal();
			if (dto.HasDiscountRate) data.DiscountRate = dto.DiscountRate.ToDecimal();
			if (dto.HasDiscountAmount) data.DiscountAmount = dto.DiscountAmount.ToDecimal();
			if (dto.HasDiscountPrice) data.DiscountPrice = dto.DiscountPrice.ToDecimal();
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
			if (dto.HasItemTotalAmount) data.ItemTotalAmount = dto.ItemTotalAmount.ToDecimal();
			if (dto.HasShipAmount) data.ShipAmount = dto.ShipAmount.ToDecimal();
			if (dto.HasCancelledAmount) data.CancelledAmount = dto.CancelledAmount.ToDecimal();
			if (dto.HasOpenAmount) data.OpenAmount = dto.OpenAmount.ToDecimal();
			if (dto.HasStockable) data.Stockable = dto.Stockable.ToBool();
			if (dto.HasIsAr) data.IsAr = dto.IsAr.ToBool();
			if (dto.HasTaxable) data.Taxable = dto.Taxable.ToBool();
			if (dto.HasCostable) data.Costable = dto.Costable.ToBool();
			if (dto.HasIsProfit) data.IsProfit = dto.IsProfit.ToBool();
			if (dto.HasUnitCost) data.UnitCost = dto.UnitCost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasLotCost) data.LotCost = dto.LotCost.ToDecimal();
			if (dto.HasLotInDate) data.LotInDate = dto.LotInDate;
			if (dto.HasLotExpDate) data.LotExpDate = dto.LotExpDate;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			#region read all grand children object

			if (dto.SalesOrderItemsAttributes != null)
			{
				if (data.SalesOrderItemsAttributes is null)
					data.SalesOrderItemsAttributes = new SalesOrderItemsAttributes();
				ReadSalesOrderItemsAttributes(data.SalesOrderItemsAttributes, dto.SalesOrderItemsAttributes);
			}

			#endregion read all grand children object

			return;
		}

		protected virtual IList<SalesOrderItems> ReadSalesOrderItems(IList<SalesOrderItems> data, IList<SalesOrderItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<SalesOrderItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.SalesOrderItemsUuid == itemDto.SalesOrderItemsUuid);
				if (obj is null)
					obj = new SalesOrderItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadSalesOrderItems(obj, itemDto);

			}
			return lstOrig;
		}

		protected virtual void ReadSalesOrderItemsAttributes(SalesOrderItemsAttributes data, SalesOrderItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasSalesOrderItemsUuid) data.SalesOrderItemsUuid = dto.SalesOrderItemsUuid;
			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			return;
		}


        #endregion read from dto to data

        #region write to dto from data

        public virtual SalesOrderDataDto WriteDto(SalesOrderData data, SalesOrderDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new SalesOrderDataDto();

            data.CheckIntegrity();

			if (data.SalesOrderHeader != null)
			{
				dto.SalesOrderHeader = new SalesOrderHeaderDto();
				WriteSalesOrderHeader(data.SalesOrderHeader, dto.SalesOrderHeader);
			}
			if (data.SalesOrderHeaderInfo != null)
			{
				dto.SalesOrderHeaderInfo = new SalesOrderHeaderInfoDto();
				WriteSalesOrderHeaderInfo(data.SalesOrderHeaderInfo, dto.SalesOrderHeaderInfo);
			}
			if (data.SalesOrderHeaderAttributes != null)
			{
				dto.SalesOrderHeaderAttributes = new SalesOrderHeaderAttributesDto();
				WriteSalesOrderHeaderAttributes(data.SalesOrderHeaderAttributes, dto.SalesOrderHeaderAttributes);
			}
			if (data.SalesOrderItems != null)
			{
				dto.SalesOrderItems = new List<SalesOrderItemsDto>();
				WriteSalesOrderItems(data.SalesOrderItems, dto.SalesOrderItems);
			}
            return dto;
        }

		protected virtual void WriteSalesOrderHeader(SalesOrderHeader data, SalesOrderHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.OrderNumber = data.OrderNumber;
			dto.OrderType = data.OrderType;
			dto.OrderStatus = data.OrderStatus;
			dto.OrderDate = data.OrderDate;
			dto.OrderTime = data.OrderTime.ToDateTime();
			dto.DueDate = data.DueDate;
			dto.BillDate = data.BillDate;
			dto.CustomerUuid = data.CustomerUuid;
			dto.CustomerNum = data.CustomerNum;
			dto.CustomerName = data.CustomerName;
			dto.Terms = data.Terms;
			dto.TermsDays = data.TermsDays;
			dto.Currency = data.Currency;
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
			dto.PaidAmount = data.PaidAmount;
			dto.CreditAmount = data.CreditAmount;
			dto.Balance = data.Balance;
			dto.UnitCost = data.UnitCost;
			dto.AvgCost = data.AvgCost;
			dto.LotCost = data.LotCost;
			dto.OrderSourceCode = data.OrderSourceCode;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteSalesOrderHeaderInfo(SalesOrderHeaderInfo data, SalesOrderHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.CentralFulfillmentNum = data.CentralFulfillmentNum;
			dto.ShippingCarrier = data.ShippingCarrier;
			dto.ShippingClass = data.ShippingClass;
			dto.DistributionCenterNum = data.DistributionCenterNum;
			dto.CentralOrderNum = data.CentralOrderNum;
			dto.ChannelNum = data.ChannelNum;
			dto.ChannelAccountNum = data.ChannelAccountNum;
			dto.ChannelOrderID = data.ChannelOrderID;
			dto.SecondaryChannelOrderID = data.SecondaryChannelOrderID;
			dto.ShippingAccount = data.ShippingAccount;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.WarehouseCode = data.WarehouseCode;
			dto.RefNum = data.RefNum;
			dto.CustomerPoNum = data.CustomerPoNum;
			dto.EndBuyerUserID = data.EndBuyerUserID;
			dto.EndBuyerName = data.EndBuyerName;
			dto.EndBuyerEmail = data.EndBuyerEmail;
			dto.ShipToName = data.ShipToName;
			dto.ShipToFirstName = data.ShipToFirstName;
			dto.ShipToLastName = data.ShipToLastName;
			dto.ShipToSuffix = data.ShipToSuffix;
			dto.ShipToCompany = data.ShipToCompany;
			dto.ShipToCompanyJobTitle = data.ShipToCompanyJobTitle;
			dto.ShipToAttention = data.ShipToAttention;
			dto.ShipToAddressLine1 = data.ShipToAddressLine1;
			dto.ShipToAddressLine2 = data.ShipToAddressLine2;
			dto.ShipToAddressLine3 = data.ShipToAddressLine3;
			dto.ShipToCity = data.ShipToCity;
			dto.ShipToState = data.ShipToState;
			dto.ShipToStateFullName = data.ShipToStateFullName;
			dto.ShipToPostalCode = data.ShipToPostalCode;
			dto.ShipToPostalCodeExt = data.ShipToPostalCodeExt;
			dto.ShipToCounty = data.ShipToCounty;
			dto.ShipToCountry = data.ShipToCountry;
			dto.ShipToEmail = data.ShipToEmail;
			dto.ShipToDaytimePhone = data.ShipToDaytimePhone;
			dto.ShipToNightPhone = data.ShipToNightPhone;
			dto.BillToName = data.BillToName;
			dto.BillToFirstName = data.BillToFirstName;
			dto.BillToLastName = data.BillToLastName;
			dto.BillToSuffix = data.BillToSuffix;
			dto.BillToCompany = data.BillToCompany;
			dto.BillToCompanyJobTitle = data.BillToCompanyJobTitle;
			dto.BillToAttention = data.BillToAttention;
			dto.BillToAddressLine1 = data.BillToAddressLine1;
			dto.BillToAddressLine2 = data.BillToAddressLine2;
			dto.BillToAddressLine3 = data.BillToAddressLine3;
			dto.BillToCity = data.BillToCity;
			dto.BillToState = data.BillToState;
			dto.BillToStateFullName = data.BillToStateFullName;
			dto.BillToPostalCode = data.BillToPostalCode;
			dto.BillToPostalCodeExt = data.BillToPostalCodeExt;
			dto.BillToCounty = data.BillToCounty;
			dto.BillToCountry = data.BillToCountry;
			dto.BillToEmail = data.BillToEmail;
			dto.BillToDaytimePhone = data.BillToDaytimePhone;
			dto.BillToNightPhone = data.BillToNightPhone;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteSalesOrderHeaderAttributes(SalesOrderHeaderAttributes data, SalesOrderHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteSalesOrderItems(SalesOrderItems data, SalesOrderItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.SalesOrderItemsUuid = data.SalesOrderItemsUuid;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.Seq = data.Seq;
			dto.OrderItemType = data.OrderItemType;
			dto.SalesOrderItemstatus = data.SalesOrderItemstatus;
			dto.ItemDate = data.ItemDate;
			dto.ItemTime = data.ItemTime.ToDateTime();
			dto.ShipDate = data.ShipDate;
			dto.EtaArrivalDate = data.EtaArrivalDate;
			dto.SKU = data.SKU;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.WarehouseCode = data.WarehouseCode;
			dto.LotNum = data.LotNum;
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.Currency = data.Currency;
			dto.UOM = data.UOM;
			dto.PackType = data.PackType;
			dto.PackQty = data.PackQty;
			dto.OrderPack = data.OrderPack;
			dto.ShipPack = data.ShipPack;
			dto.CancelledPack = data.CancelledPack;
			dto.OrderQty = data.OrderQty;
			dto.ShipQty = data.ShipQty;
			dto.CancelledQty = data.CancelledQty;
			dto.OpenQty = data.OpenQty;
			dto.PriceRule = data.PriceRule;
			dto.Price = data.Price;
			dto.DiscountRate = data.DiscountRate;
			dto.DiscountAmount = data.DiscountAmount;
			dto.DiscountPrice = data.DiscountPrice;
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
			dto.ItemTotalAmount = data.ItemTotalAmount;
			dto.ShipAmount = data.ShipAmount;
			dto.CancelledAmount = data.CancelledAmount;
			dto.OpenAmount = data.OpenAmount;
			dto.Stockable = data.Stockable;
			dto.IsAr = data.IsAr;
			dto.Taxable = data.Taxable;
			dto.Costable = data.Costable;
			dto.IsProfit = data.IsProfit;
			dto.UnitCost = data.UnitCost;
			dto.AvgCost = data.AvgCost;
			dto.LotCost = data.LotCost;
			dto.LotInDate = data.LotInDate;
			dto.LotExpDate = data.LotExpDate;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			#region write all grand children object

			if (data.SalesOrderItemsAttributes != null)
			{
				dto.SalesOrderItemsAttributes = new SalesOrderItemsAttributesDto();
				WriteSalesOrderItemsAttributes(data.SalesOrderItemsAttributes, dto.SalesOrderItemsAttributes);
			}

			#endregion write all grand children object

			return;
		}
		protected virtual void WriteSalesOrderItems(IList<SalesOrderItems> data, IList<SalesOrderItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new SalesOrderItemsDto();
				dto.Add(obj);
				WriteSalesOrderItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WriteSalesOrderItemsAttributes(SalesOrderItemsAttributes data, SalesOrderItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.SalesOrderItemsUuid = data.SalesOrderItemsUuid;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

        #endregion write to dto from data

    }
}




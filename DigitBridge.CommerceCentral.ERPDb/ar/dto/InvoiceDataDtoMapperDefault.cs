    

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
    /// Represents a InvoiceDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InvoiceDataDtoMapperDefault : IDtoMapper<InvoiceData, InvoiceDataDto> 
    {
        #region read from dto to data

        public virtual InvoiceData ReadDto(InvoiceData data, InvoiceDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new InvoiceData();
                data.New();
            }

			if (dto.InvoiceHeader != null)
			{
				if (data.InvoiceHeader is null)
					data.InvoiceHeader = data.NewInvoiceHeader();
				ReadInvoiceHeader(data.InvoiceHeader, dto.InvoiceHeader);
			}
			if (dto.InvoiceHeaderInfo != null)
			{
				if (data.InvoiceHeaderInfo is null)
					data.InvoiceHeaderInfo = data.NewInvoiceHeaderInfo();
				ReadInvoiceHeaderInfo(data.InvoiceHeaderInfo, dto.InvoiceHeaderInfo);
			}
			if (dto.InvoiceHeaderAttributes != null)
			{
				if (data.InvoiceHeaderAttributes is null)
					data.InvoiceHeaderAttributes = data.NewInvoiceHeaderAttributes();
				ReadInvoiceHeaderAttributes(data.InvoiceHeaderAttributes, dto.InvoiceHeaderAttributes);
			}
			if (dto.InvoiceItems != null)
			{
				if (data.InvoiceItems is null)
					data.InvoiceItems = new List<InvoiceItems>();
				var deleted = ReadInvoiceItems(data.InvoiceItems, dto.InvoiceItems);
				data.SetInvoiceItemsDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadInvoiceHeader(InvoiceHeader data, InvoiceHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasInvoiceNumber) data.InvoiceNumber = dto.InvoiceNumber;
			if (dto.HasQboDocNumber) data.QboDocNumber = dto.QboDocNumber;
			if (dto.HasSalesOrderUuid) data.SalesOrderUuid = dto.SalesOrderUuid;
			if (dto.HasOrderNumber) data.OrderNumber = dto.OrderNumber;
			if (dto.HasInvoiceType) data.InvoiceType = dto.InvoiceType.ToInt();
			if (dto.HasInvoiceStatus) data.InvoiceStatus = dto.InvoiceStatus.ToInt();
			if (dto.HasInvoiceDate) data.InvoiceDate = dto.InvoiceDate.ToDateTime();
			if (dto.HasInvoiceTime) data.InvoiceTime = dto.InvoiceTime.ToTimeSpan();
			if (dto.HasDueDate) data.DueDate = dto.DueDate;
			if (dto.HasBillDate) data.BillDate = dto.BillDate;
			if (dto.HasShipDate) data.ShipDate = dto.ShipDate;
			if (dto.HasCustomerUuid) data.CustomerUuid = dto.CustomerUuid;
			if (dto.HasCustomerCode) data.CustomerCode = dto.CustomerCode;
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
			if (dto.HasChannelAmount) data.ChannelAmount = dto.ChannelAmount.ToDecimal();
			if (dto.HasPaidAmount) data.PaidAmount = dto.PaidAmount.ToDecimal();
			if (dto.HasCreditAmount) data.CreditAmount = dto.CreditAmount.ToDecimal();
			if (dto.HasBalance) data.Balance = dto.Balance.ToDecimal();
			if (dto.HasUnitCost) data.UnitCost = dto.UnitCost.ToDecimal();
			if (dto.HasAvgCost) data.AvgCost = dto.AvgCost.ToDecimal();
			if (dto.HasLotCost) data.LotCost = dto.LotCost.ToDecimal();
			if (dto.HasInvoiceSourceCode) data.InvoiceSourceCode = dto.InvoiceSourceCode;
			if (dto.HasSalesRep) data.SalesRep = dto.SalesRep;
			if (dto.HasSalesRep2) data.SalesRep2 = dto.SalesRep2;
			if (dto.HasSalesRep3) data.SalesRep3 = dto.SalesRep3;
			if (dto.HasSalesRep4) data.SalesRep4 = dto.SalesRep4;
			if (dto.HasCommissionRate) data.CommissionRate = dto.CommissionRate.ToDecimal();
			if (dto.HasCommissionRate2) data.CommissionRate2 = dto.CommissionRate2.ToDecimal();
			if (dto.HasCommissionRate3) data.CommissionRate3 = dto.CommissionRate3.ToDecimal();
			if (dto.HasCommissionRate4) data.CommissionRate4 = dto.CommissionRate4.ToDecimal();
			if (dto.HasCommissionAmount) data.CommissionAmount = dto.CommissionAmount.ToDecimal();
			if (dto.HasCommissionAmount2) data.CommissionAmount2 = dto.CommissionAmount2.ToDecimal();
			if (dto.HasCommissionAmount3) data.CommissionAmount3 = dto.CommissionAmount3.ToDecimal();
			if (dto.HasCommissionAmount4) data.CommissionAmount4 = dto.CommissionAmount4.ToDecimal();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadInvoiceHeaderInfo(InvoiceHeaderInfo data, InvoiceHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasCentralFulfillmentNum) data.CentralFulfillmentNum = dto.CentralFulfillmentNum.ToLong();
			if (dto.HasOrderShipmentNum) data.OrderShipmentNum = dto.OrderShipmentNum.ToLong();
			if (dto.HasOrderShipmentUuid) data.OrderShipmentUuid = dto.OrderShipmentUuid;
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
			if (dto.HasDBChannelOrderHeaderRowID) data.DBChannelOrderHeaderRowID = dto.DBChannelOrderHeaderRowID;
			if (dto.HasOrderDCAssignmentNum) data.OrderDCAssignmentNum = dto.OrderDCAssignmentNum.ToLong();
			if (dto.HasEndBuyerUserId) data.EndBuyerUserId = dto.EndBuyerUserId;
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
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasOrderDCAssignmentNum) data.OrderDCAssignmentNum = dto.OrderDCAssignmentNum.ToLong();
			if (dto.HasDBChannelOrderHeaderRowID) data.DBChannelOrderHeaderRowID = dto.DBChannelOrderHeaderRowID;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadInvoiceHeaderAttributes(InvoiceHeaderAttributes data, InvoiceHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadInvoiceItems(InvoiceItems data, InvoiceItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasInvoiceItemsUuid) data.InvoiceItemsUuid = dto.InvoiceItemsUuid;
			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasInvoiceItemType) data.InvoiceItemType = dto.InvoiceItemType.ToInt();
			if (dto.HasInvoiceItemStatus) data.InvoiceItemStatus = dto.InvoiceItemStatus.ToInt();
			if (dto.HasItemDate) data.ItemDate = dto.ItemDate.ToDateTime();
			if (dto.HasItemTime) data.ItemTime = dto.ItemTime.ToTimeSpan();
			if (dto.HasShipDate) data.ShipDate = dto.ShipDate;
			if (dto.HasEtaArrivalDate) data.EtaArrivalDate = dto.EtaArrivalDate;
			if (dto.HasCentralOrderLineUuid) data.CentralOrderLineUuid = dto.CentralOrderLineUuid;
			if (dto.HasDBChannelOrderLineRowID) data.DBChannelOrderLineRowID = dto.DBChannelOrderLineRowID;
			if (dto.HasOrderDCAssignmentLineUuid) data.OrderDCAssignmentLineUuid = dto.OrderDCAssignmentLineUuid;
			if (dto.HasOrderDCAssignmentLineNum) data.OrderDCAssignmentLineNum = dto.OrderDCAssignmentLineNum.ToLong();
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
			if (dto.HasOpenPack) data.OpenPack = dto.OpenPack.ToDecimal();
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
			if (dto.HasOrderAmount) data.OrderAmount = dto.OrderAmount.ToDecimal();
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
			if (dto.HasCentralOrderLineUuid) data.CentralOrderLineUuid = dto.CentralOrderLineUuid;
			if (dto.HasDBChannelOrderLineRowID) data.DBChannelOrderLineRowID = dto.DBChannelOrderLineRowID;
			if (dto.HasOrderDCAssignmentLineUuid) data.OrderDCAssignmentLineUuid = dto.OrderDCAssignmentLineUuid;
			if (dto.HasOrderDCAssignmentLineNum) data.OrderDCAssignmentLineNum = dto.OrderDCAssignmentLineNum.ToLong();
			if (dto.HasCommissionRate) data.CommissionRate = dto.CommissionRate.ToDecimal();
			if (dto.HasCommissionAmount) data.CommissionAmount = dto.CommissionAmount.ToDecimal();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;
			if (dto.HasTotalReturnQty) data.TotalReturnQty = dto.TotalReturnQty.ToDecimal();
			#endregion read properties

			#region read all grand children object

			if (dto.InvoiceItemsAttributes != null)
			{
				if (data.InvoiceItemsAttributes is null)
					data.InvoiceItemsAttributes = new InvoiceItemsAttributes();
				ReadInvoiceItemsAttributes(data.InvoiceItemsAttributes, dto.InvoiceItemsAttributes);
			}

			#endregion read all grand children object

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<InvoiceItems> ReadInvoiceItems(IList<InvoiceItems> data, IList<InvoiceItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<InvoiceItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.InvoiceItemsUuid == itemDto.InvoiceItemsUuid);
				if (obj is null)
					obj = new InvoiceItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadInvoiceItems(obj, itemDto);

			}
			return lstOrig;
		}

		protected virtual void ReadInvoiceItemsAttributes(InvoiceItemsAttributes data, InvoiceItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasInvoiceItemsUuid) data.InvoiceItemsUuid = dto.InvoiceItemsUuid;
			if (dto.HasInvoiceUuid) data.InvoiceUuid = dto.InvoiceUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


        #endregion read from dto to data

        #region write to dto from data

        public virtual InvoiceDataDto WriteDto(InvoiceData data, InvoiceDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new InvoiceDataDto();

            data.CheckIntegrity();

			if (data.InvoiceHeader != null)
			{
				dto.InvoiceHeader = new InvoiceHeaderDto();
				WriteInvoiceHeader(data.InvoiceHeader, dto.InvoiceHeader);
			}
			if (data.InvoiceHeaderInfo != null)
			{
				dto.InvoiceHeaderInfo = new InvoiceHeaderInfoDto();
				WriteInvoiceHeaderInfo(data.InvoiceHeaderInfo, dto.InvoiceHeaderInfo);
			}
			if (data.InvoiceHeaderAttributes != null)
			{
				dto.InvoiceHeaderAttributes = new InvoiceHeaderAttributesDto();
				WriteInvoiceHeaderAttributes(data.InvoiceHeaderAttributes, dto.InvoiceHeaderAttributes);
			}
			if (data.InvoiceItems != null)
			{
				dto.InvoiceItems = new List<InvoiceItemsDto>();
				WriteInvoiceItems(data.InvoiceItems, dto.InvoiceItems);
			}
            return dto;
        }

		public virtual void WriteInvoiceHeader(InvoiceHeader data, InvoiceHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.InvoiceNumber = data.InvoiceNumber;
			dto.QboDocNumber = data.QboDocNumber;
			dto.SalesOrderUuid = data.SalesOrderUuid;
			dto.OrderNumber = data.OrderNumber;
			dto.InvoiceType = data.InvoiceType;
			dto.InvoiceStatus = data.InvoiceStatus;
			dto.InvoiceDate = data.InvoiceDate;
			dto.InvoiceTime = data.InvoiceTime.ToDateTime();
			dto.DueDate = data.DueDate;
			if (!data.BillDate.IsZero())
				dto.BillDate = data.BillDate;
			if (!data.ShipDate.IsZero())
				dto.ShipDate = data.ShipDate;
			dto.CustomerUuid = data.CustomerUuid;
			dto.CustomerCode = data.CustomerCode;
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
			dto.ChannelAmount = data.ChannelAmount;
			dto.PaidAmount = data.PaidAmount;
			dto.CreditAmount = data.CreditAmount;
			dto.Balance = data.Balance;
			dto.UnitCost = data.UnitCost;
			dto.AvgCost = data.AvgCost;
			dto.LotCost = data.LotCost;
			dto.InvoiceSourceCode = data.InvoiceSourceCode;
			dto.SalesRep = data.SalesRep;
			dto.SalesRep2 = data.SalesRep2;
			dto.SalesRep3 = data.SalesRep3;
			dto.SalesRep4 = data.SalesRep4;
			dto.CommissionRate = data.CommissionRate;
			dto.CommissionRate2 = data.CommissionRate2;
			dto.CommissionRate3 = data.CommissionRate3;
			dto.CommissionRate4 = data.CommissionRate4;
			dto.CommissionAmount = data.CommissionAmount;
			dto.CommissionAmount2 = data.CommissionAmount2;
			dto.CommissionAmount3 = data.CommissionAmount3;
			dto.CommissionAmount4 = data.CommissionAmount4;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteInvoiceHeaderInfo(InvoiceHeaderInfo data, InvoiceHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.CentralFulfillmentNum = data.CentralFulfillmentNum;
			dto.OrderShipmentNum = data.OrderShipmentNum;
			dto.OrderShipmentUuid = data.OrderShipmentUuid;
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
			dto.DBChannelOrderHeaderRowID = data.DBChannelOrderHeaderRowID;
			dto.OrderDCAssignmentNum = data.OrderDCAssignmentNum;
			dto.EndBuyerUserId = data.EndBuyerUserId;
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
			dto.Notes = data.Notes;
			dto.OrderDCAssignmentNum = data.OrderDCAssignmentNum;
			dto.DBChannelOrderHeaderRowID = data.DBChannelOrderHeaderRowID;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteInvoiceHeaderAttributes(InvoiceHeaderAttributes data, InvoiceHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WriteInvoiceItems(InvoiceItems data, InvoiceItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.InvoiceItemsUuid = data.InvoiceItemsUuid;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.Seq = data.Seq;
			dto.InvoiceItemType = data.InvoiceItemType;
			dto.InvoiceItemStatus = data.InvoiceItemStatus;
			dto.ItemDate = data.ItemDate;
			dto.ItemTime = data.ItemTime.ToDateTime();
			if (!data.ShipDate.IsZero())
				dto.ShipDate = data.ShipDate;
			if (!data.EtaArrivalDate.IsZero())
				dto.EtaArrivalDate = data.EtaArrivalDate;
			dto.CentralOrderLineUuid = data.CentralOrderLineUuid;
			dto.DBChannelOrderLineRowID = data.DBChannelOrderLineRowID;
			dto.OrderDCAssignmentLineUuid = data.OrderDCAssignmentLineUuid;
			dto.OrderDCAssignmentLineNum = data.OrderDCAssignmentLineNum;
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
			dto.OpenPack = data.OpenPack;
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
			dto.OrderAmount = data.OrderAmount;
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
			dto.CentralOrderLineUuid = data.CentralOrderLineUuid;
			dto.DBChannelOrderLineRowID = data.DBChannelOrderLineRowID;
			dto.OrderDCAssignmentLineUuid = data.OrderDCAssignmentLineUuid;
			dto.OrderDCAssignmentLineNum = data.OrderDCAssignmentLineNum;
			dto.CommissionRate = data.CommissionRate;
			dto.CommissionAmount = data.CommissionAmount;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;
			dto.TotalReturnQty = data.TotalReturnQty;
			#endregion read properties

			#region write all grand children object

			if (data.InvoiceItemsAttributes != null)
			{
				dto.InvoiceItemsAttributes = new InvoiceItemsAttributesDto();
				WriteInvoiceItemsAttributes(data.InvoiceItemsAttributes, dto.InvoiceItemsAttributes);
			}

			#endregion write all grand children object

			return;
		}
		protected virtual void WriteInvoiceItems(IList<InvoiceItems> data, IList<InvoiceItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new InvoiceItemsDto();
				dto.Add(obj);
				WriteInvoiceItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WriteInvoiceItemsAttributes(InvoiceItemsAttributes data, InvoiceItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.InvoiceItemsUuid = data.InvoiceItemsUuid;
			dto.InvoiceUuid = data.InvoiceUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

        #endregion write to dto from data

    }
}




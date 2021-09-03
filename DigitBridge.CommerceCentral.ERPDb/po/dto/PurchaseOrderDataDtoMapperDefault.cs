    

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
    /// Represents a PurchaseOrderDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class PurchaseOrderDataDtoMapperDefault : IDtoMapper<PurchaseOrderData, PurchaseOrderDataDto> 
    {
        #region read from dto to data

        public virtual PurchaseOrderData ReadDto(PurchaseOrderData data, PurchaseOrderDataDto dto)
        {
            if (dto is null)
                return data;
            if (data is null)
            {
                data = new PurchaseOrderData();
                data.New();
            }

			if (dto.PoHeader != null)
			{
				if (data.PoHeader is null)
					data.PoHeader = data.NewPoHeader();
				ReadPoHeader(data.PoHeader, dto.PoHeader);
			}
			if (dto.PoHeaderInfo != null)
			{
				if (data.PoHeaderInfo is null)
					data.PoHeaderInfo = data.NewPoHeaderInfo();
				ReadPoHeaderInfo(data.PoHeaderInfo, dto.PoHeaderInfo);
			}
			if (dto.PoHeaderAttributes != null)
			{
				if (data.PoHeaderAttributes is null)
					data.PoHeaderAttributes = data.NewPoHeaderAttributes();
				ReadPoHeaderAttributes(data.PoHeaderAttributes, dto.PoHeaderAttributes);
			}
			if (dto.PoItems != null)
			{
				if (data.PoItems is null)
					data.PoItems = new List<PoItems>();
				var deleted = ReadPoItems(data.PoItems, dto.PoItems);
				data.SetPoItemsDeleted(deleted);
			}

            data.CheckIntegrity();
            return data;
        }

		protected virtual void ReadPoHeader(PoHeader data, PoHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasPoNum) data.PoNum = dto.PoNum;
			if (dto.HasPoType) data.PoType = dto.PoType;
			if (dto.HasPoStatus) data.PoStatus = dto.PoStatus;
			if (dto.HasPoDate) data.PoDate = dto.PoDate.ToDateTime();
			if (dto.HasPoTime) data.PoTime = dto.PoTime.ToTimeSpan();
			if (dto.HasEtaShipDate) data.EtaShipDate = dto.EtaShipDate;
			if (dto.HasEtaArrivalDate) data.EtaArrivalDate = dto.EtaArrivalDate;
			if (dto.HasCancelDate) data.CancelDate = dto.CancelDate;
			if (dto.HasVendorUuid) data.VendorUuid = dto.VendorUuid;
			if (dto.HasVendorNum) data.VendorNum = dto.VendorNum;
			if (dto.HasVendorName) data.VendorName = dto.VendorName;
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
			if (dto.HasPoSourceCode) data.PoSourceCode = dto.PoSourceCode;
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadPoHeaderInfo(PoHeaderInfo data, PoHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
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
			if (dto.HasRefNum) data.RefNum = dto.RefNum;
			if (dto.HasCustomerPoNum) data.CustomerPoNum = dto.CustomerPoNum;
			if (dto.HasWarehouseUuid) data.WarehouseUuid = dto.WarehouseUuid;
			if (dto.HasCustomerUuid) data.CustomerUuid = dto.CustomerUuid;
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

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadPoHeaderAttributes(PoHeaderAttributes data, PoHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			data.CheckIntegrity();
			return;
		}


		protected virtual void ReadPoItems(PoItems data, PoItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasPoItemUuid) data.PoItemUuid = dto.PoItemUuid;
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasSeq) data.Seq = dto.Seq.ToInt();
			if (dto.HasPoItemType) data.PoItemType = dto.PoItemType;
			if (dto.HasPoItemStatus) data.PoItemStatus = dto.PoItemStatus;
			if (dto.HasPoDate) data.PoDate = dto.PoDate.ToDateTime();
			if (dto.HasPoTime) data.PoTime = dto.PoTime.ToTimeSpan();
			if (dto.HasEtaShipDate) data.EtaShipDate = dto.EtaShipDate;
			if (dto.HasEtaArrivalDate) data.EtaArrivalDate = dto.EtaArrivalDate;
			if (dto.HasCancelDate) data.CancelDate = dto.CancelDate;
			if (dto.HasProductUuid) data.ProductUuid = dto.ProductUuid;
			if (dto.HasInventoryUuid) data.InventoryUuid = dto.InventoryUuid;
			if (dto.HasSKU) data.SKU = dto.SKU;
			if (dto.HasDescription) data.Description = dto.Description;
			if (dto.HasNotes) data.Notes = dto.Notes;
			if (dto.HasCurrency) data.Currency = dto.Currency;
			if (dto.HasPoQty) data.PoQty = dto.PoQty.ToDecimal();
			if (dto.HasReceivedQty) data.ReceivedQty = dto.ReceivedQty.ToDecimal();
			if (dto.HasCancelledQty) data.CancelledQty = dto.CancelledQty.ToDecimal();
			if (dto.HasPriceRule) data.PriceRule = dto.PriceRule;
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
			if (dto.HasCostable) data.Costable = dto.Costable.ToBool();
			if (dto.HasTaxable) data.Taxable = dto.Taxable.ToBool();
			if (dto.HasIsAp) data.IsAp = dto.IsAp.ToBool();
			if (dto.HasUpdateDateUtc) data.UpdateDateUtc = dto.UpdateDateUtc;
			if (dto.HasEnterBy) data.EnterBy = dto.EnterBy;
			if (dto.HasUpdateBy) data.UpdateBy = dto.UpdateBy;

			#endregion read properties

			#region read all grand children object

			if (dto.PoItemsAttributes != null)
			{
				if (data.PoItemsAttributes is null)
					data.PoItemsAttributes = new PoItemsAttributes();
				ReadPoItemsAttributes(data.PoItemsAttributes, dto.PoItemsAttributes);
			}
			if (dto.PoItemsRef != null)
			{
				if (data.PoItemsRef is null)
					data.PoItemsRef = new PoItemsRef();
				ReadPoItemsRef(data.PoItemsRef, dto.PoItemsRef);
			}

			#endregion read all grand children object

			data.CheckIntegrity();
			return;
		}

		protected virtual IList<PoItems> ReadPoItems(IList<PoItems> data, IList<PoItemsDto> dto)
		{
			if (data is null || dto is null)
				return null;
			var lstOrig = new List<PoItems>(data.Where(x => x != null).ToList());
			data.Clear();
			foreach (var itemDto in dto)
			{
				if (itemDto == null) continue;

				var obj = itemDto.RowNum > 0
					? lstOrig.Find(x => x.RowNum == itemDto.RowNum)
					: lstOrig.Find(x => x.PoItemUuid == itemDto.PoItemUuid);
				if (obj is null)
					obj = new PoItems().SetAllowNull(false);
				else
					lstOrig.Remove(obj);

				data.Add(obj);

				ReadPoItems(obj, itemDto);

			}
			return lstOrig;
		}

		protected virtual void ReadPoItemsAttributes(PoItemsAttributes data, PoItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasPoItemUuid) data.PoItemUuid = dto.PoItemUuid;
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasFields) data.Fields.LoadJson(dto.Fields);

			#endregion read properties

			data.CheckIntegrity();
			return;
		}
		protected virtual void ReadPoItemsRef(PoItemsRef data, PoItemsRefDto dto)
		{
			if (data is null || dto is null)
				return;

			#region read all not null properties

			if (dto.HasDatabaseNum) data.DatabaseNum = dto.DatabaseNum.ToInt();
			if (dto.HasMasterAccountNum) data.MasterAccountNum = dto.MasterAccountNum.ToInt();
			if (dto.HasProfileNum) data.ProfileNum = dto.ProfileNum.ToInt();
			if (dto.HasPoUuid) data.PoUuid = dto.PoUuid;
			if (dto.HasPoItemUuid) data.PoItemUuid = dto.PoItemUuid;
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
			if (dto.HasCustomerUuid) data.CustomerUuid = dto.CustomerUuid;
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

			data.CheckIntegrity();
			return;
		}


        #endregion read from dto to data

        #region write to dto from data

        public virtual PurchaseOrderDataDto WriteDto(PurchaseOrderData data, PurchaseOrderDataDto dto)
        {
            if (data is null)
                return null;
            if (dto is null)
                dto = new PurchaseOrderDataDto();

            data.CheckIntegrity();

			if (data.PoHeader != null)
			{
				dto.PoHeader = new PoHeaderDto();
				WritePoHeader(data.PoHeader, dto.PoHeader);
			}
			if (data.PoHeaderInfo != null)
			{
				dto.PoHeaderInfo = new PoHeaderInfoDto();
				WritePoHeaderInfo(data.PoHeaderInfo, dto.PoHeaderInfo);
			}
			if (data.PoHeaderAttributes != null)
			{
				dto.PoHeaderAttributes = new PoHeaderAttributesDto();
				WritePoHeaderAttributes(data.PoHeaderAttributes, dto.PoHeaderAttributes);
			}
			if (data.PoItems != null)
			{
				dto.PoItems = new List<PoItemsDto>();
				WritePoItems(data.PoItems, dto.PoItems);
			}
            return dto;
        }

		protected virtual void WritePoHeader(PoHeader data, PoHeaderDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.PoUuid = data.PoUuid;
			dto.PoNum = data.PoNum;
			dto.PoType = data.PoType;
			dto.PoStatus = data.PoStatus;
			dto.PoDate = data.PoDate;
			dto.PoTime = data.PoTime.ToDateTime();
			dto.EtaShipDate = data.EtaShipDate;
			dto.EtaArrivalDate = data.EtaArrivalDate;
			dto.CancelDate = data.CancelDate;
			dto.VendorUuid = data.VendorUuid;
			dto.VendorNum = data.VendorNum;
			dto.VendorName = data.VendorName;
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
			dto.PoSourceCode = data.PoSourceCode;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WritePoHeaderInfo(PoHeaderInfo data, PoHeaderInfoDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.PoUuid = data.PoUuid;
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
			dto.RefNum = data.RefNum;
			dto.CustomerPoNum = data.CustomerPoNum;
			dto.WarehouseUuid = data.WarehouseUuid;
			dto.CustomerUuid = data.CustomerUuid;
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
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WritePoHeaderAttributes(PoHeaderAttributes data, PoHeaderAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.PoUuid = data.PoUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

		protected virtual void WritePoItems(PoItems data, PoItemsDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.PoItemUuid = data.PoItemUuid;
			dto.PoUuid = data.PoUuid;
			dto.Seq = data.Seq;
			dto.PoItemType = data.PoItemType;
			dto.PoItemStatus = data.PoItemStatus;
			dto.PoDate = data.PoDate;
			dto.PoTime = data.PoTime.ToDateTime();
			dto.EtaShipDate = data.EtaShipDate;
			dto.EtaArrivalDate = data.EtaArrivalDate;
			dto.CancelDate = data.CancelDate;
			dto.ProductUuid = data.ProductUuid;
			dto.InventoryUuid = data.InventoryUuid;
			dto.SKU = data.SKU;
			dto.Description = data.Description;
			dto.Notes = data.Notes;
			dto.Currency = data.Currency;
			dto.PoQty = data.PoQty;
			dto.ReceivedQty = data.ReceivedQty;
			dto.CancelledQty = data.CancelledQty;
			dto.PriceRule = data.PriceRule;
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
			dto.Costable = data.Costable;
			dto.Taxable = data.Taxable;
			dto.IsAp = data.IsAp;
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			#region write all grand children object

			if (data.PoItemsAttributes != null)
			{
				dto.PoItemsAttributes = new PoItemsAttributesDto();
				WritePoItemsAttributes(data.PoItemsAttributes, dto.PoItemsAttributes);
			}
			if (data.PoItemsRef != null)
			{
				dto.PoItemsRef = new PoItemsRefDto();
				WritePoItemsRef(data.PoItemsRef, dto.PoItemsRef);
			}

			#endregion write all grand children object

			return;
		}
		protected virtual void WritePoItems(IList<PoItems> data, IList<PoItemsDto> dto)
		{
			if (data is null || dto is null)
				return;

			dto.Clear();

			#region write all list items and properties with null

			foreach (var itemData in data)
			{
				if (itemData is null) continue;
				var obj = new PoItemsDto();
				dto.Add(obj);
				WritePoItems(itemData, obj);
			}

			#endregion write all list items and properties with null
			return;
		}


		protected virtual void WritePoItemsAttributes(PoItemsAttributes data, PoItemsAttributesDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.PoItemUuid = data.PoItemUuid;
			dto.PoUuid = data.PoUuid;
			dto.Fields = data.Fields.ToJson();
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}
		protected virtual void WritePoItemsRef(PoItemsRef data, PoItemsRefDto dto)
		{
			if (data is null || dto is null)
				return;

			#region write all properties with null

			dto.RowNum = data.RowNum;
			dto.DatabaseNum = data.DatabaseNum;
			dto.MasterAccountNum = data.MasterAccountNum;
			dto.ProfileNum = data.ProfileNum;
			dto.PoUuid = data.PoUuid;
			dto.PoItemUuid = data.PoItemUuid;
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
			dto.CustomerUuid = data.CustomerUuid;
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
			dto.EnterDateUtc = data.EnterDateUtc;
			dto.UpdateDateUtc = data.UpdateDateUtc;
			dto.EnterBy = data.EnterBy;
			dto.UpdateBy = data.UpdateBy;
			dto.DigitBridgeGuid = data.DigitBridgeGuid;

			#endregion read properties

			return;
		}

        #endregion write to dto from data

    }
}




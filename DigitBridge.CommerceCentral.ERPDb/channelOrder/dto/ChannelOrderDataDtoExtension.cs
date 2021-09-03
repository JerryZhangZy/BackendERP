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
    /// Represents a ChannelOrderDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ChannelOrderDataDtoExtension
    {
        /// <summary>
        /// Merge ChannelOrderDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">ChannelOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this ChannelOrderDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
			if (!dto.HasOrderHeader)
				return result;
			//TODO change to merge Dto children object
			if (withHeaderText)
                result.Add(dto.OrderHeader.MergeName(dto.OrderHeader));
            result.Add(dto.OrderHeader.Merge(dto.OrderHeader));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this ChannelOrderDataDto dto, bool withHeaderText = false)
        {
			//TODO change to merge Dto children object
			var result = new List<dynamic>();
			if (!dto.HasOrderLine)
                return result;

            var orderLines = new OrderLineDto();

            if (withHeaderText)
                result.Add(orderLines.MergeName(orderLines));

            foreach (var item in dto.OrderLine)
            {
                result.Add(item.Merge(item));
            }
            return result;
        }


        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public static IList<ChannelOrderDataDto> GetFakerData(this ChannelOrderDataDto dto, int count)
        {
            var obj = new ChannelOrderDataDto();
            var datas = new List<ChannelOrderDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static ChannelOrderDataDto GetFakerData(this ChannelOrderDataDto dto)
        {
            var data = new ChannelOrderDataDto();
			data.OrderHeader = new OrderHeaderDto().GetFaker().Generate();
			data.OrderLine = new OrderLineDto().GetFaker().Generate(3);
            return data;
        }


		/// <summary>
		/// Get faker object for OrderHeaderDto
		/// </summary>
		/// <param name="dto">OrderHeaderDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<OrderHeaderDto> GetFaker(this OrderHeaderDto dto)
		{
			#region faker data rules
			return new Faker<OrderHeaderDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.UserDataPresent, f => f.Random.Bool())
				.RuleFor(u => u.UserDataRemoveDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SecondaryChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SellerOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.OriginalOrderDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.SellerPublicNote, f => f.Lorem.Sentence().TruncateTo(4500))
				.RuleFor(u => u.SellerPrivateNote, f => f.Lorem.Sentence().TruncateTo(4500))
				.RuleFor(u => u.EndBuyerInstruction, f => f.Lorem.Sentence().TruncateTo(4500))
				.RuleFor(u => u.TotalOrderAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalShippingDiscount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalShippingDiscountTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalInsuranceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalGiftOptionAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalGiftOptionTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AdditionalCostOrDiscount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PromotionAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.EstimatedShipDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.DeliverByDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.RequestedShippingCarrier, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.RequestedShippingClass, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.ResellerID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.FlagNum, f => default(short))
				.RuleFor(u => u.FlagDesc, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.PaymentStatus, f => f.Random.Bool())
				.RuleFor(u => u.PaymentUpdateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.ShippingUpdateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.EndBuyerUserID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerEmail, f => f.Internet.Email())
				.RuleFor(u => u.PaymentMethod, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.ShipToName, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToFirstName, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToLastName, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToSuffix, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.ShipToCompany, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToAttention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.ShipToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.ShipToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.ShipToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.ShipToAddressLine3, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToCity, f => f.Address.City())
				.RuleFor(u => u.ShipToState, f => f.Address.State())
				.RuleFor(u => u.ShipToStateFullName, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToPostalCodeExt, f => f.Lorem.Word())
				.RuleFor(u => u.ShipToCounty, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.ShipToCountry, f => f.Address.Country())
				.RuleFor(u => u.ShipToEmail, f => f.Internet.Email())
				.RuleFor(u => u.BillToName, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToFirstName, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToLastName, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToSuffix, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.BillToCompany, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToAttention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.BillToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.BillToAddressLine3, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToCity, f => f.Address.City())
				.RuleFor(u => u.BillToState, f => f.Address.State())
				.RuleFor(u => u.BillToStateFullName, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToPostalCodeExt, f => f.Lorem.Word())
				.RuleFor(u => u.BillToCounty, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.BillToCountry, f => f.Address.Country())
				.RuleFor(u => u.BillToEmail, f => f.Internet.Email())
				.RuleFor(u => u.BillToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.SignatureFlag, f => f.Lorem.Sentence().TruncateTo(15))
				.RuleFor(u => u.PickupFlag, f => f.Lorem.Sentence().TruncateTo(15))
				.RuleFor(u => u.MerchantDivision, f => f.Lorem.Sentence().TruncateTo(30))
				.RuleFor(u => u.MerchantBatchNumber, f => f.Lorem.Sentence().TruncateTo(30))
				.RuleFor(u => u.MerchantDepartmentSiteID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.ReservationNumber, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.MerchantShipToAddressType, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.CustomerOrganizationType, f => f.Random.Bool())
				.RuleFor(u => u.OrderMark, f => f.Random.Bool())
				.RuleFor(u => u.OrderMark2, f => f.Random.Bool())
				.RuleFor(u => u.OrderStatus, f => f.Random.Bool())
				.RuleFor(u => u.OrderStatusUpdateDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.DBChannelOrderHeaderRowID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.DCAssignmentStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.DCAssignmentDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.CentralOrderUuid, f => String.Empty)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for OrderLineDto
		/// </summary>
		/// <param name="dto">OrderLineDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<OrderLineDto> GetFaker(this OrderLineDto dto)
		{
			#region faker data rules
			return new Faker<OrderLineDto>()
				.RuleFor(u => u.CentralOrderLineNum, f => default(long))
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralProductNum, f => default(long))
				.RuleFor(u => u.ChannelItemID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.ItemTitle, f => f.Lorem.Sentence().TruncateTo(200))
				.RuleFor(u => u.OrderQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineItemTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineShippingDiscount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineShippingDiscountTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineRecyclingFee, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineGiftMsg, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.LineGiftNotes, f => f.Lorem.Sentence().TruncateTo(400))
				.RuleFor(u => u.LineGiftAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LineGiftTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LinePromotionCodes, f => f.Lorem.Word())
				.RuleFor(u => u.LinePromotionAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LinePromotionTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.BundleStatus, f => f.Random.Bool())
				.RuleFor(u => u.HarmonizedCode, f => f.Lorem.Word())
				.RuleFor(u => u.UPC, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.EAN, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.UnitOfMeasure, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.DBChannelOrderLineRowID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralOrderUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralOrderLineUuid, f => String.Empty)
				;
			#endregion faker data rules
		}

    }
}



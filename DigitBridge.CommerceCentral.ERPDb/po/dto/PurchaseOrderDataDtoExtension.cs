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
    /// Represents a PurchaseOrderDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class PurchaseOrderDataDtoExtension
    {
        /// <summary>
        /// Merge PurchaseOrderDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">PurchaseOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IList<dynamic> MergeHeaderRecord(this PurchaseOrderDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
			if (!dto.HasPoHeader)
				return result;
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(dto.PoHeader.MergeName(dto.PoHeaderInfo));
            result.Add(dto.PoHeader.Merge(dto.PoHeaderInfo));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IList<dynamic> MergeDetailRecord(this PurchaseOrderDataDto dto, bool withHeaderText = false)
        {
			var result = new List<dynamic>();

			if (!dto.HasPoItems)
				return result;
            var poItems = new PoItemsDto() {PoItemsAttributes=new PoItemsAttributesDto(), PoItemsRef = new PoItemsRefDto()};

            if (withHeaderText)
                result.Add(poItems.MergeName(poItems.PoItemsRef,poItems.PoItemsAttributes));

            foreach (var item in dto.PoItems)
            {
                result.Add(item.Merge(item.PoItemsRef,item.PoItemsAttributes));
            }
            return result;
        }


        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public static IList<PurchaseOrderDataDto> GetFakerData(this PurchaseOrderDataDto dto, int count)
        {
            var obj = new PurchaseOrderDataDto();
            var datas = new List<PurchaseOrderDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static PurchaseOrderDataDto GetFakerData(this PurchaseOrderDataDto dto)
        {
            var data = new PurchaseOrderDataDto();
			data.PoHeader = new PoHeaderDto().GetFaker().Generate();
			data.PoHeaderInfo = new PoHeaderInfoDto().GetFaker().Generate();
			data.PoHeaderAttributes = new PoHeaderAttributesDto().GetFaker().Generate();
			data.PoItems = new PoItemsDto().GetFaker().Generate(3);
			foreach (var ln in data.PoItems)
				ln.PoItemsAttributes = new PoItemsAttributesDto().GetFaker().Generate();
			foreach (var ln in data.PoItems)
				ln.PoItemsRef = new PoItemsRefDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for PoHeaderDto
		/// </summary>
		/// <param name="dto">PoHeaderDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoHeaderDto> GetFaker(this PoHeaderDto dto)
		{
			#region faker data rules
			return new Faker<PoHeaderDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.PoUuid, f => String.Empty)
				.RuleFor(u => u.PoNum, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.PoType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.PoTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.EtaShipDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.CancelDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.VendorUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.VendorNum, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.VendorName, f => f.Company.CompanyName())
				.RuleFor(u => u.Currency, f => "USD")
				.RuleFor(u => u.SubTotalAmount, f => f.Random.Decimal(100, 1000, 2))
				.RuleFor(u => u.TotalAmount, f => f.Random.Decimal(100, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.50m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PoSourceCode, f =>null)
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoHeaderInfoDto
		/// </summary>
		/// <param name="dto">PoHeaderInfoDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoHeaderInfoDto> GetFaker(this PoHeaderInfoDto dto)
		{
			#region faker data rules
			return new Faker<PoHeaderInfoDto>()
				.RuleFor(u => u.PoUuid, f => String.Empty)
				.RuleFor(u => u.CentralFulfillmentNum, f => default(long))
				.RuleFor(u => u.ShippingCarrier, f =>"Fedex")
				.RuleFor(u => u.ShippingClass, f => "Fedex")
				.RuleFor(u => u.DistributionCenterNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SecondaryChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.ShippingAccount, f => "Amazon")
				.RuleFor(u => u.RefNum, f => f.Random.Int(100000,9999999).ToString())
				.RuleFor(u => u.CustomerPoNum, f => f.Random.Int(100000, 9999999).ToString())
				.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerUserID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerName, f => f.Company.CompanyName())
				.RuleFor(u => u.EndBuyerEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToName, f => f.Person.FullName.ToString())
				.RuleFor(u => u.ShipToFirstName, f => f.Person.FirstName.ToString())
				.RuleFor(u => u.ShipToLastName, f => f.Person.LastName.ToString())
				.RuleFor(u => u.ShipToSuffix, f => f.Company.CompanySuffix())
				.RuleFor(u => u.ShipToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToCompanyJobTitle, f => f.Company.CompanySuffix())
				.RuleFor(u => u.ShipToAttention, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.ShipToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.ShipToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.ShipToAddressLine3, f => string.Empty)
				.RuleFor(u => u.ShipToCity, f => f.Address.City())
				.RuleFor(u => u.ShipToState, f => f.Address.State())
				.RuleFor(u => u.ShipToStateFullName, f => f.Address.StateAbbr())
				.RuleFor(u => u.ShipToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToPostalCodeExt, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToCounty, f => f.Address.County())
				.RuleFor(u => u.ShipToCountry, f => f.Address.Country())
				.RuleFor(u => u.ShipToEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.ShipToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToName, f => f.Person.FullName.ToString())
				.RuleFor(u => u.BillToFirstName, f => f.Person.FirstName.ToString())
				.RuleFor(u => u.BillToLastName, f => f.Person.LastName.ToString())
				.RuleFor(u => u.BillToSuffix, f => f.Person.Gender.ToString())
				.RuleFor(u => u.BillToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToCompanyJobTitle, f => f.Company.CompanySuffix())
				.RuleFor(u => u.BillToAttention, f =>string.Empty)
				.RuleFor(u => u.BillToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.BillToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.BillToAddressLine3, f => string.Empty)
				.RuleFor(u => u.BillToCity, f => f.Address.City())
				.RuleFor(u => u.BillToState, f => f.Address.State())
				.RuleFor(u => u.BillToStateFullName, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToPostalCodeExt, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToCounty, f => f.Address.County())
				.RuleFor(u => u.BillToCountry, f => f.Address.Country())
				.RuleFor(u => u.BillToEmail, f => f.Internet.Email())
				.RuleFor(u => u.BillToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoHeaderAttributesDto
		/// </summary>
		/// <param name="dto">PoHeaderAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoHeaderAttributesDto> GetFaker(this PoHeaderAttributesDto dto)
		{
			#region faker data rules
			return new Faker<PoHeaderAttributesDto>()
				.RuleFor(u => u.PoUuid, f => String.Empty)
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoItemsDto
		/// </summary>
		/// <param name="dto">PoItemsDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoItemsDto> GetFaker(this PoItemsDto dto)
		{
			#region faker data rules
			return new Faker<PoItemsDto>()
				.RuleFor(u => u.PoItemUuid, f => String.Empty)
				.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoItemType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoItemStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.PoTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.EtaShipDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.CancelDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.Currency, f =>"USD")
				.RuleFor(u => u.PoQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ReceivedQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CancelledQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PriceRule, f => f.PickRandom(FakerExtension.PriceRule))
				.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ExtAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.Stockable, f => f.Random.Bool())
				.RuleFor(u => u.Costable, f => f.Random.Bool())
				.RuleFor(u => u.Taxable, f => f.Random.Bool())
				.RuleFor(u => u.IsAp, f => f.Random.Bool())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoItemsAttributesDto
		/// </summary>
		/// <param name="dto">PoItemsAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoItemsAttributesDto> GetFaker(this PoItemsAttributesDto dto)
		{
			#region faker data rules
			return new Faker<PoItemsAttributesDto>()
				.RuleFor(u => u.PoItemUuid, f => String.Empty)
				.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoItemsRefDto
		/// </summary>
		/// <param name="dto">PoItemsRefDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoItemsRefDto> GetFaker(this PoItemsRefDto dto)
		{
			#region faker data rules
			return new Faker<PoItemsRefDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.PoItemUuid, f => String.Empty)
				.RuleFor(u => u.CentralFulfillmentNum, f => default(long))
				.RuleFor(u => u.ShippingCarrier, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.ShippingClass, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.DistributionCenterNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SecondaryChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.ShippingAccount, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerUserID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerName, f => f.Company.CompanyName())
				.RuleFor(u => u.EndBuyerEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToName, f => f.Person.FullName.ToString())
				.RuleFor(u => u.ShipToFirstName, f => f.Person.FirstName.ToString())
				.RuleFor(u => u.ShipToLastName, f => f.Person.LastName.ToString())
				.RuleFor(u => u.ShipToSuffix, f => f.Person.Gender.ToString())
				.RuleFor(u => u.ShipToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToCompanyJobTitle, f => f.Company.CompanySuffix())
				.RuleFor(u => u.ShipToAttention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.ShipToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.ShipToAddressLine3, f => string.Empty)
				.RuleFor(u => u.ShipToCity, f => f.Address.City())
				.RuleFor(u => u.ShipToState, f => f.Address.State())
				.RuleFor(u => u.ShipToStateFullName, f => f.Address.State())
				.RuleFor(u => u.ShipToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToPostalCodeExt, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToCounty, f => f.Address.County())
				.RuleFor(u => u.ShipToCountry, f => f.Address.Country())
				.RuleFor(u => u.ShipToEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.ShipToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToName, f => f.Person.FullName.ToString())
				.RuleFor(u => u.BillToFirstName, f => f.Person.FirstName.ToString())
				.RuleFor(u => u.BillToLastName, f => f.Person.LastName.ToString())
				.RuleFor(u => u.BillToSuffix, f => f.Person.Gender.ToString())
				.RuleFor(u => u.BillToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToCompanyJobTitle, f => f.Company.CompanySuffix())
				.RuleFor(u => u.BillToAttention, f => string.Empty)
				.RuleFor(u => u.BillToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.BillToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.BillToAddressLine3, f => string.Empty)
				.RuleFor(u => u.BillToCity, f => f.Address.City())
				.RuleFor(u => u.BillToState, f => f.Address.State())
				.RuleFor(u => u.BillToStateFullName, f => f.Address.State())
				.RuleFor(u => u.BillToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToPostalCodeExt, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToCounty, f => f.Address.County())
				.RuleFor(u => u.BillToCountry, f => f.Address.Country())
				.RuleFor(u => u.BillToEmail, f => f.Internet.Email())
				.RuleFor(u => u.BillToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}

    }
}



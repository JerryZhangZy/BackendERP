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
    /// Represents a InvoiceDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InvoiceDataDtoExtension
    {
        /// <summary>
        /// Merge InvoiceDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">InvoiceDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this InvoiceDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(dto.InvoiceHeader.MergeName(dto.InvoiceHeaderInfo, dto.InvoiceHeaderAttributes));
            result.Add(dto.InvoiceHeader.Merge(dto.InvoiceHeaderInfo, dto.InvoiceHeaderAttributes));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this InvoiceDataDto dto, bool withHeaderText = false)
        {
			//TODO change to merge Dto children object
			var result = new List<dynamic>();
			if (!dto.HasInvoiceItems)
                return result;

            var invoiceItems = new InvoiceItems() { InvoiceItemsAttributes = new InvoiceItemsAttributes() };

            if (withHeaderText)
                result.Add(invoiceItems.MergeName(invoiceItems.InvoiceItemsAttributes));

            foreach (var item in dto.InvoiceItems)
            {
                result.Add(item.Merge(item.InvoiceItemsAttributes));
            }
            return result;
        }


        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public static IList<InvoiceDataDto> GetFakerData(this InvoiceDataDto dto, int count)
        {
            var obj = new InvoiceDataDto();
            var datas = new List<InvoiceDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static InvoiceDataDto GetFakerData(this InvoiceDataDto dto)
        {
            var data = new InvoiceDataDto();
			data.InvoiceHeader = new InvoiceHeaderDto().GetFaker().Generate();
			data.InvoiceHeaderInfo = new InvoiceHeaderInfoDto().GetFaker().Generate();
			data.InvoiceHeaderAttributes = new InvoiceHeaderAttributesDto().GetFaker().Generate();
			data.InvoiceItems = new InvoiceItemsDto().GetFaker().Generate(3);
			foreach (var ln in data.InvoiceItems)
				ln.InvoiceItemsAttributes = new InvoiceItemsAttributesDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for InvoiceHeaderDto
		/// </summary>
		/// <param name="dto">InvoiceHeaderDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InvoiceHeaderDto> GetFaker(this InvoiceHeaderDto dto)
		{
			#region faker data rules
			return new Faker<InvoiceHeaderDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.InvoiceUuid, f => null)
				.RuleFor(u=>u.EnterDateUtc,f=>null)
				.RuleFor(u => u.DigitBridgeGuid, f => new Guid())
				.RuleFor(u => u.RowNum, f => null)
				.RuleFor(u => u.InvoiceNumber, f => f.Random.Int(1,100).ToString())
				.RuleFor(u => u.SalesOrderUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.OrderNumber, f => f.Random.Int(1,100).ToString())
				.RuleFor(u => u.InvoiceType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.InvoiceStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.InvoiceDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.InvoiceTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.DueDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.BillDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CustomerCode, f => f.Lorem.Word())
				.RuleFor(u => u.CustomerName, f => f.Name.FullName())
				.RuleFor(u => u.Terms, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.TermsDays, f => f.Random.Int(1, 100))
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.SubTotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.SalesAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxableAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.NonTaxableAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PaidAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CreditAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.Balance, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LotCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.InvoiceSourceCode, f => f.Lorem.Word())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for InvoiceHeaderInfoDto
		/// </summary>
		/// <param name="dto">InvoiceHeaderInfoDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InvoiceHeaderInfoDto> GetFaker(this InvoiceHeaderInfoDto dto)
		{
			#region faker data rules
			return new Faker<InvoiceHeaderInfoDto>()
				.RuleFor(u => u.DigitBridgeGuid, f => new Guid())
				.RuleFor(u => u.InvoiceUuid, f => String.Empty)
				.RuleFor(u => u.CentralFulfillmentNum, f => default(long))
				.RuleFor(u => u.OrderShipmentNum, f => default(long))
				.RuleFor(u => u.OrderShipmentUuid, f => f.Random.Guid().ToString())
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
				.RuleFor(u => u.WarehouseCode, f => f.Lorem.Word())
				.RuleFor(u => u.RefNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.CustomerPoNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.EndBuyerUserId, f => f.Random.Guid().ToString())
				.RuleFor(u => u.EndBuyerName, f => f.Name.FullName())
				.RuleFor(u => u.EndBuyerEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToName, f => f.Name.FullName())
				.RuleFor(u => u.ShipToFirstName, f => f.Name.FirstName())
				.RuleFor(u => u.ShipToLastName, f => f.Name.LastName())
				.RuleFor(u => u.ShipToSuffix, f => f.Name.Suffix())
				.RuleFor(u => u.ShipToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.ShipToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToAttention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.ShipToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.ShipToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.ShipToAddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
				.RuleFor(u => u.ShipToCity, f => f.Address.City())
				.RuleFor(u => u.ShipToState, f => f.Address.StateAbbr())
				.RuleFor(u => u.ShipToStateFullName, f => f.Address.State())
				.RuleFor(u => u.ShipToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.ShipToPostalCodeExt, f => f.Lorem.Word())
				.RuleFor(u => u.ShipToCounty, f => f.Address.County())
				.RuleFor(u => u.ShipToCountry, f => f.Address.Country())
				.RuleFor(u => u.ShipToEmail, f => f.Internet.Email())
				.RuleFor(u => u.ShipToDaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.ShipToNightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.BillToName, f => f.Name.FullName())
				.RuleFor(u => u.BillToFirstName, f => f.Name.FirstName())
				.RuleFor(u => u.BillToLastName, f => f.Name.LastName())
				.RuleFor(u => u.BillToSuffix, f => f.Name.Suffix())
				.RuleFor(u => u.BillToCompany, f => f.Company.CompanyName())
				.RuleFor(u => u.BillToCompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToAttention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.BillToAddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.BillToAddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.BillToAddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
				.RuleFor(u => u.BillToCity, f => f.Address.City())
				.RuleFor(u => u.BillToState, f => f.Address.StateAbbr())
				.RuleFor(u => u.BillToStateFullName, f => f.Address.State())
				.RuleFor(u => u.BillToPostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.BillToPostalCodeExt, f => f.Lorem.Word())
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
		/// Get faker object for InvoiceHeaderAttributesDto
		/// </summary>
		/// <param name="dto">InvoiceHeaderAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InvoiceHeaderAttributesDto> GetFaker(this InvoiceHeaderAttributesDto dto)
		{
			#region faker data rules
			return new Faker<InvoiceHeaderAttributesDto>()
				.RuleFor(u => u.InvoiceUuid, f => String.Empty)
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for InvoiceItemsDto
		/// </summary>
		/// <param name="dto">InvoiceItemsDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InvoiceItemsDto> GetFaker(this InvoiceItemsDto dto)
		{
			#region faker data rules
			return new Faker<InvoiceItemsDto>()
				.RuleFor(u => u.InvoiceItemsUuid, f => null)
				.RuleFor(u => u.InvoiceUuid, f => null)
				.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
				.RuleFor(u => u.InvoiceItemType, f => f.PickRandom(FakerExtension.InvoiceItemType))
				.RuleFor(u => u.InvoiceItemStatus, f => f.PickRandom(FakerExtension.InvoiceItemStatus))
				.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.ItemTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.ShipDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.EtaArrivalDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.WarehouseCode, f => f.Lorem.Word())
				.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.UOM, f => f.PickRandom(FakerExtension.UOM))
				.RuleFor(u => u.PackType, f => f.PickRandom(FakerExtension.PackType))
				.RuleFor(u => u.PackQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OrderPack, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShipPack, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CancelledPack, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenPack, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OrderQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShipQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CancelledQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PriceRule, f => f.PickRandom(FakerExtension.PriceRule))
				.RuleFor(u => u.Price, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountPrice, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ExtAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxableAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.NonTaxableAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ItemTotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OrderAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CancelledAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.OpenAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.Stockable, f => f.Random.Bool())
				.RuleFor(u => u.IsAr, f => f.Random.Bool())
				.RuleFor(u => u.Taxable, f => f.Random.Bool())
				.RuleFor(u => u.Costable, f => f.Random.Bool())
				.RuleFor(u => u.IsProfit, f => f.Random.Bool())
				.RuleFor(u => u.UnitCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.AvgCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LotCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for InvoiceItemsAttributesDto
		/// </summary>
		/// <param name="dto">InvoiceItemsAttributesDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InvoiceItemsAttributesDto> GetFaker(this InvoiceItemsAttributesDto dto)
		{
			#region faker data rules
			return new Faker<InvoiceItemsAttributesDto>()
				.RuleFor(u => u.InvoiceItemsUuid, f => String.Empty)
				.RuleFor(u => u.InvoiceUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.Fields, f => f.Random.JObject())
				;
			#endregion faker data rules
		}

    }
}



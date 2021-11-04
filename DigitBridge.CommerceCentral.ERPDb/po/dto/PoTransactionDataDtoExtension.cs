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
    /// Represents a PoTransactionDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class PoTransactionDataDtoExtension
    {
        /// <summary>
        /// Merge PoTransactionDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">PoTransactionDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this PoTransactionDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasPoTransaction)
                return result;
            //TODO change to merge Dto children object
            //if (withHeaderText)
            //    result.Add(dto.SalesOrderHeader.MergeName(dto.SalesOrderHeaderInfo, dto.SalesOrderHeaderAttributes));
            //result.Add(dto.SalesOrderHeader.Merge(dto.SalesOrderHeaderInfo, dto.SalesOrderHeaderAttributes));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this PoTransactionDataDto dto, bool withHeaderText = false)
        {
            return null;
            //TODO change to merge Dto children object
            //var result = new List<dynamic>();
            //if (!dto.HasSalesOrderItems) 
            //    return result;
            //
            //var salesOrderItems = new SalesOrderItems() { SalesOrderItemsAttributes = new SalesOrderItemsAttributes()};
            //
            //if (withHeaderText)
            //    result.Add(salesOrderItems.MergeName(salesOrderItems.SalesOrderItemsAttributes));
            //
            //foreach (var item in dto.SalesOrderItems)
            //{
            //    result.Add(item.Merge(item.SalesOrderItemsAttributes));
            //}
            //return result;
        }


        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <param name="count">Generate multiple fake data</param>
        /// <returns>list for Fake data</returns>
        public static IList<PoTransactionDataDto> GetFakerData(this PoTransactionDataDto dto, int count)
        {
            var obj = new PoTransactionDataDto();
            var datas = new List<PoTransactionDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static PoTransactionDataDto GetFakerData(this PoTransactionDataDto dto)
        {
            var data = new PoTransactionDataDto();
			data.PoTransaction = new PoTransactionDto().GetFaker().Generate();
			data.PoTransactionItems = new PoTransactionItemsDto().GetFaker().Generate(3);
            return data;
        }


		/// <summary>
		/// Get faker object for PoTransactionDto
		/// </summary>
		/// <param name="dto">PoTransactionDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoTransactionDto> GetFaker(this PoTransactionDto dto)
		{
			#region faker data rules
			return new Faker<PoTransactionDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.TransUuid, f => System.Guid.NewGuid().ToString())
				.RuleFor(u => u.TransNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.PoNum, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.TransType, f => f.Random.Int(1, 2))
				.RuleFor(u => u.TransStatus, f => f.Random.Int(0, 6))
				.RuleFor(u => u.TransDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.TransTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.VendorUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.VendorInvoiceNum, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.VendorInvoiceDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.DueDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.SubTotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.TaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DiscountRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.DiscountAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ShippingTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.MiscTaxAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.ChargeAndAllowanceAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for PoTransactionItemsDto
		/// </summary>
		/// <param name="dto">PoTransactionItemsDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<PoTransactionItemsDto> GetFaker(this PoTransactionItemsDto dto)
		{
			#region faker data rules
			return new Faker<PoTransactionItemsDto>()
				.RuleFor(u => u.TransItemUuid, f => String.Empty)
				.RuleFor(u => u.TransUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.Seq, f => f.Random.Int(1, 100))
				.RuleFor(u => u.PoUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.PoItemUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.ItemType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ItemStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ItemDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.ItemTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.ProductUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.InventoryUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.WarehouseUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.LotNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.LotDescription, f => f.Commerce.ProductName())
				.RuleFor(u => u.LotInDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.LotExpDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.UOM, f => f.PickRandom(FakerExtension.UOM))
				.RuleFor(u => u.TransQty, f => f.Random.Decimal(1, 1000, 2))
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
				.RuleFor(u => u.IsAp, f => f.Random.Bool())
				.RuleFor(u => u.Taxable, f => f.Random.Bool())
				.RuleFor(u => u.Costable, f => f.Random.Bool())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}

    }
}


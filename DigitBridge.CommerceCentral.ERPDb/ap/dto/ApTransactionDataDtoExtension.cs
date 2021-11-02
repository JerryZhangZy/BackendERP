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
    /// Represents a ApTransactionDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class ApTransactionDataDtoExtension
    {
        /// <summary>
        /// Merge ApTransactionDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">ApTransactionDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this ApTransactionDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasApInvoiceTransaction)
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
        public static IEnumerable<dynamic> MergeDetailRecord(this ApTransactionDataDto dto, bool withHeaderText = false)
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
        public static IList<ApTransactionDataDto> GetFakerData(this ApTransactionDataDto dto, int count)
        {
            var obj = new ApTransactionDataDto();
            var datas = new List<ApTransactionDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static ApTransactionDataDto GetFakerData(this ApTransactionDataDto dto)
        {
            var data = new ApTransactionDataDto();
			data.ApInvoiceTransaction = new ApInvoiceTransactionDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for ApInvoiceTransactionDto
		/// </summary>
		/// <param name="dto">ApInvoiceTransactionDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<ApInvoiceTransactionDto> GetFaker(this ApInvoiceTransactionDto dto)
		{
			#region faker data rules
			return new Faker<ApInvoiceTransactionDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.TransUuid, f => String.Empty)
				.RuleFor(u => u.TransNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ApInvoiceUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.TransType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.TransStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.TransDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.TransTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.PaidBy, f => f.Random.Int(1, 100))
				.RuleFor(u => u.BankAccountUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CheckNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.AuthCode, f => f.Lorem.Word())
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.ExchangeRate, f => f.Random.Decimal(0.01m, 0.99m, 2))
				.RuleFor(u => u.Amount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CreditAccount, f => default(long))
				.RuleFor(u => u.DebitAccount, f => default(long))
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}

    }
}



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
using System.Dynamic;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a MiscInvoiceDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class MiscInvoiceDataDtoExtension
    {
        static dynamic ToDynamicName(object obj)
        {
            dynamic expando = new ExpandoObject();

            // get obj Schema
            var objSchema = ObjectSchema.ForType(obj.GetType());
            foreach (var item in objSchema.Properties)
            {
                ObjectSchemaExtension.AddExpandoObjectPropertyName(expando, item.Value);
            }
            return expando;
        }
        static dynamic ToDynamic(object obj)
        {
            dynamic expando = new ExpandoObject();

            // get obj Schema
            var objSchema = ObjectSchema.ForType(obj.GetType());
            foreach (var item in objSchema.Properties)
            {
                ObjectSchemaExtension.AddExpandoObjectProperty(expando, item.Value, obj);
            }

            return expando;
        }
        /// <summary>
        /// Merge MiscInvoiceDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">MiscInvoiceDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this MiscInvoiceDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasMiscInvoiceHeader)
                return result;
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(ToDynamicName(dto.MiscInvoiceHeader));
            result.Add(ToDynamic(dto.MiscInvoiceHeader));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this MiscInvoiceDataDto dto, bool withHeaderText = false)
        {
            return new List<dynamic>();
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
        public static IList<MiscInvoiceDataDto> GetFakerData(this MiscInvoiceDataDto dto, int count)
        {
            var obj = new MiscInvoiceDataDto();
            var datas = new List<MiscInvoiceDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static MiscInvoiceDataDto GetFakerData(this MiscInvoiceDataDto dto)
        {
            var data = new MiscInvoiceDataDto();
			data.MiscInvoiceHeader = new MiscInvoiceHeaderDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for MiscInvoiceHeaderDto
		/// </summary>
		/// <param name="dto">MiscInvoiceHeaderDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<MiscInvoiceHeaderDto> GetFaker(this MiscInvoiceHeaderDto dto)
		{
			#region faker data rules
			return new Faker<MiscInvoiceHeaderDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.MiscInvoiceUuid, f => String.Empty)
				.RuleFor(u => u.MiscInvoiceNumber, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.QboDocNumber, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.MiscInvoiceType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.MiscInvoiceStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.MiscInvoiceDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.MiscInvoiceTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CustomerCode, f => f.Lorem.Word())
				.RuleFor(u => u.CustomerName, f => f.Company.CompanyName())
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence().TruncateTo(500))
				.RuleFor(u => u.PaidBy, f => f.Random.Int(1, 100))
				.RuleFor(u => u.BankAccountUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.BankAccountCode, f => f.Lorem.Word())
				.RuleFor(u => u.CheckNum, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.AuthCode, f => f.Lorem.Word())
				.RuleFor(u => u.Currency, f => f.Lorem.Sentence().TruncateTo(10))
				.RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.PaidAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.CreditAmount, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.Balance, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.InvoiceSourceCode, f => f.Lorem.Word())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}

    }
}


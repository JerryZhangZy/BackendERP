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
    /// Represents a InitNumbersDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class InitNumbersDataDtoExtension
    {
        /// <summary>
        /// Merge InitNumbersDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">InitNumbersDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this InitNumbersDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasInitNumbers)
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
        public static IEnumerable<dynamic> MergeDetailRecord(this InitNumbersDataDto dto, bool withHeaderText = false)
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
        public static IList<InitNumbersDataDto> GetFakerData(this InitNumbersDataDto dto, int count)
        {
            var obj = new InitNumbersDataDto();
            var datas = new List<InitNumbersDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static InitNumbersDataDto GetFakerData(this InitNumbersDataDto dto)
        {
            var data = new InitNumbersDataDto();
			data.InitNumbers = new InitNumbersDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for InitNumbersDto
		/// </summary>
		/// <param name="dto">InitNumbersDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<InitNumbersDto> GetFaker(this InitNumbersDto dto)
		{
			#region faker data rules
			return new Faker<InitNumbersDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.InitNumbersUuid, f => String.Empty)
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.InActive, f => f.Random.Bool())
				.RuleFor(u => u.Type, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.CurrentNumber, f => f.Random.Int(1, 100))
				.RuleFor(u => u.Number, f => f.Random.Int(1, 100))
				.RuleFor(u => u.Prefix, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.Suffix, f => f.Lorem.Sentence().TruncateTo(20))
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
                .RuleFor(u => u.MaxNumber, f => default(long))
                ;
			#endregion faker data rules
		}

    }
}



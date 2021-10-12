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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksExportLogDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class QuickBooksExportLogDataDtoExtension
    {
        /// <summary>
        /// Merge QuickBooksExportLogDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">QuickBooksExportLogDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this QuickBooksExportLogDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasQuickBooksExportLog)
                return result;
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(dto.QuickBooksExportLog.MergeName(dto.QuickBooksExportLog));
            result.Add(dto.QuickBooksExportLog.Merge(dto.QuickBooksExportLog));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this QuickBooksExportLogDataDto dto, bool withHeaderText = false)
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
        public static IList<QuickBooksExportLogDataDto> GetFakerData(this QuickBooksExportLogDataDto dto, int count)
        {
            var obj = new QuickBooksExportLogDataDto();
            var datas = new List<QuickBooksExportLogDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static QuickBooksExportLogDataDto GetFakerData(this QuickBooksExportLogDataDto dto)
        {
            var data = new QuickBooksExportLogDataDto();
			data.QuickBooksExportLog = new QuickBooksExportLogDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for QuickBooksExportLogDto
		/// </summary>
		/// <param name="dto">QuickBooksExportLogDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<QuickBooksExportLogDto> GetFaker(this QuickBooksExportLogDto dto)
		{
			#region faker data rules
			return new Faker<QuickBooksExportLogDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.QuickBooksExportLogUuid, f => String.Empty)
				.RuleFor(u => u.BatchNum, f => default(long))
				.RuleFor(u => u.LogType, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.LogUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.DocNumber, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.DocStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.LogDate, f => f.Date.Past(0).Date)
				.RuleFor(u => u.LogTime, f => f.Date.Timespan().ToDateTime())
				.RuleFor(u => u.LogBy, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.TxnId, f => f.Random.Guid().ToString())
				.RuleFor(u => u.LogStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ErrorMessage, f => f.Lorem.Sentence().TruncateTo(20000))
				.RuleFor(u => u.RequestInfo, f => f.Lorem.Sentence().TruncateTo(2000))
				.RuleFor(u => u.EnterBy, f => null)
				;
			#endregion faker data rules
		}

    }
}



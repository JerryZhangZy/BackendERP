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
    /// Represents a WarehouseDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class WarehouseDataDtoExtension
    {
        /// <summary>
        /// Merge WarehouseDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">WarehouseDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this WarehouseDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            //TODO change to merge Dto children object
            //if (withHeaderText)
            //    result.Add(dto.SalesOrderHeader.MergeName(dto.SalesOrderHeaderInfo, dto.SalesOrderHeaderAttributes));
            //result.Add(dto.SalesOrderHeader.Merge(dto.SalesOrderHeaderInfo, dto.SalesOrderHeaderAttributes));
            if (withHeaderText)
                result.Add(dto.DistributionCenter.MergeName(dto.DistributionCenter));
            result.Add(dto.DistributionCenter.Merge(dto.DistributionCenter));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this WarehouseDataDto dto, bool withHeaderText = false)
        {
            return null;
            //TODO change to merge Dto children object
            //if (!dto.HasSalesOrderItems) 
            //    return null;
            //
            //var result = new List<dynamic>();
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
        public static IList<WarehouseDataDto> GetFakerData(this WarehouseDataDto dto, int count)
        {
            var obj = new WarehouseDataDto();
            var datas = new List<WarehouseDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static WarehouseDataDto GetFakerData(this WarehouseDataDto dto)
        {
            var data = new WarehouseDataDto();
			data.DistributionCenter = new DistributionCenterDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for DistributionCenterDto
		/// </summary>
		/// <param name="dto">DistributionCenterDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<DistributionCenterDto> GetFaker(this DistributionCenterDto dto)
		{
			#region faker data rules
			return new Faker<DistributionCenterDto>()
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.DistributionCenterNum, f =>null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.DistributionCenterName, f => f.Company.CompanyName())
				.RuleFor(u => u.DistributionCenterCode, f => f.Lorem.Word())
				.RuleFor(u => u.DistributionCenterType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.Status, f => f.Random.Int(1, 100))
				.RuleFor(u => u.DefaultLevel, f => f.Random.Bool())
				.RuleFor(u => u.AddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.AddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.City, f => f.Address.City())
				.RuleFor(u => u.State, f => f.Address.State())
				.RuleFor(u => u.ZipCode, f => f.Lorem.Word())
				.RuleFor(u => u.CompanyName, f => f.Company.CompanyName())
				.RuleFor(u => u.ContactName, f => f.Company.CompanyName())
				.RuleFor(u => u.ContactEmail, f => f.Internet.Email())
				.RuleFor(u => u.ContactPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.MainPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.Fax, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.Website, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.Email, f => f.Internet.Email())
				.RuleFor(u => u.BusinessHours, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.Notes, f => f.Lorem.Sentence())
				.RuleFor(u => u.Priority, f => f.Random.Int(1, 100))
				.RuleFor(u => u.DistributionCenterUuid, f => null)
				;
			#endregion faker data rules
		}

    }
}



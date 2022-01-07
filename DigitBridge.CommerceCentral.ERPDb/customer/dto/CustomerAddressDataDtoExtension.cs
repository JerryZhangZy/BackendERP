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
    /// Represents a CustomerAddressDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class CustomerAddressDataDtoExtension
    {
        /// <summary>
        /// Merge CustomerAddressDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">CustomerAddressDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this CustomerAddressDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            if(!dto.HasCustomerAddress)
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
        public static IEnumerable<dynamic> MergeDetailRecord(this CustomerAddressDataDto dto, bool withHeaderText = false)
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
        public static IList<CustomerAddressDataDto> GetFakerData(this CustomerAddressDataDto dto, int count)
        {
            var obj = new CustomerAddressDataDto();
            var datas = new List<CustomerAddressDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static CustomerAddressDataDto GetFakerData(this CustomerAddressDataDto dto)
        {
            var data = new CustomerAddressDataDto();
			data.CustomerAddress = new CustomerAddressDto().GetFaker().Generate();
            return data;
        }


		/// <summary>
		/// Get faker object for CustomerAddressDto
		/// </summary>
		/// <param name="dto">CustomerAddressDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<CustomerAddressDto> GetFaker(this CustomerAddressDto dto)
		{
			#region faker data rules
			return new Faker<CustomerAddressDto>()
				.RuleFor(u => u.AddressUuid, f => String.Empty)
				.RuleFor(u => u.CustomerUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.AddressCode, f => f.Lorem.Word())
				.RuleFor(u => u.AddressType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.Description, f => f.Commerce.ProductName())
				.RuleFor(u => u.Name, f => f.Company.CompanyName())
				.RuleFor(u => u.FirstName, f => f.Company.CompanyName())
				.RuleFor(u => u.LastName, f => f.Company.CompanyName())
				.RuleFor(u => u.Suffix, f => f.Random.AlphaNumeric(50))
				.RuleFor(u => u.Company, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.CompanyJobTitle, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.Attention, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.AddressLine1, f => f.Address.StreetAddress())
				.RuleFor(u => u.AddressLine2, f => f.Address.SecondaryAddress())
				.RuleFor(u => u.AddressLine3, f => f.Lorem.Sentence().TruncateTo(200))
				.RuleFor(u => u.City, f => f.Address.City())
				.RuleFor(u => u.State, f => f.Address.State())
				.RuleFor(u => u.StateFullName, f => f.Company.CompanyName())
				.RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
				.RuleFor(u => u.PostalCodeExt, f => f.Lorem.Word())
				.RuleFor(u => u.County, f => f.Lorem.Sentence().TruncateTo(100))
				.RuleFor(u => u.Country, f => f.Address.Country())
				.RuleFor(u => u.Email, f => f.Internet.Email())
				.RuleFor(u => u.DaytimePhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.NightPhone, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.UpdateDateUtc, f => null)
				.RuleFor(u => u.EnterBy, f => null)
				.RuleFor(u => u.UpdateBy, f => null)
				;
			#endregion faker data rules
		}

    }
}



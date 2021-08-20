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
    /// Represents a DCAssignmentDataDtoMapperDefault Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public static class DCAssignmentDataDtoExtension
    {
        /// <summary>
        /// Merge DCAssignmentDataDto header objects to one dynamic object
        /// </summary>
        /// <param name="dto">DCAssignmentDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>Single dynamic object include all properties of Dto header objects</returns>
        public static IEnumerable<dynamic> MergeHeaderRecord(this DCAssignmentDataDto dto, bool withHeaderText = false)
        {
            var result = new List<dynamic>();
            //TODO change to merge Dto children object
            if (withHeaderText)
                result.Add(dto.OrderDCAssignmentHeader.MergeName(dto.OrderDCAssignmentHeader));
            result.Add(dto.OrderDCAssignmentHeader.Merge(dto.OrderDCAssignmentHeader));
            return result;
        }

        /// <summary>
        /// Merge SalesOrderDataDto detailt list to dynamic object list
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object to merge data</param>
        /// <param name="withHeaderText">Add header text line at first</param>
        /// <returns>list of dynamic object include all properties of detailt objects</returns>
        public static IEnumerable<dynamic> MergeDetailRecord(this DCAssignmentDataDto dto, bool withHeaderText = false)
        {
			//TODO change to merge Dto children object
			var result = new List<dynamic>();
			if (!dto.HasOrderDCAssignmentLine)
                return result;

            var orderDCAssignmentLineDto = new OrderDCAssignmentLineDto() ;

            if (withHeaderText)
                result.Add(orderDCAssignmentLineDto.MergeName(orderDCAssignmentLineDto));

            foreach (var item in dto.OrderDCAssignmentLine)
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
        public static IList<DCAssignmentDataDto> GetFakerData(this DCAssignmentDataDto dto, int count)
        {
            var obj = new DCAssignmentDataDto();
            var datas = new List<DCAssignmentDataDto>();
            for (int i = 0; i < count; i++)
                datas.Add(obj.GetFakerData());
            return datas;
        }

        /// <summary>
        /// Generate fake data for SalesOrderDataDto object
        /// </summary>
        /// <param name="dto">SalesOrderDataDto object</param>
        /// <returns>single Fake data</returns>
        public static DCAssignmentDataDto GetFakerData(this DCAssignmentDataDto dto)
        {
            var data = new DCAssignmentDataDto();
			data.OrderDCAssignmentHeader = new OrderDCAssignmentHeaderDto().GetFaker().Generate();
			data.OrderDCAssignmentLine = new OrderDCAssignmentLineDto().GetFaker().Generate(3);
            return data;
        }


		/// <summary>
		/// Get faker object for OrderDCAssignmentHeaderDto
		/// </summary>
		/// <param name="dto">OrderDCAssignmentHeaderDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<OrderDCAssignmentHeaderDto> GetFaker(this OrderDCAssignmentHeaderDto dto)
		{
			#region faker data rules
			return new Faker<OrderDCAssignmentHeaderDto>()
				.RuleFor(u => u.OrderDCAssignmentNum, f => default(long))
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.ShippingCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.InsuranceCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.TaxCost, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.FulfillmentType, f => f.Random.Int(1, 100))
				.RuleFor(u => u.DistributionCenterNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.SellerWarehouseID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.UseSystemShippingLabel, f => f.Random.Int(1, 100))
				.RuleFor(u => u.UseChannelPackingSlip, f => f.Random.Int(1, 100))
				.RuleFor(u => u.UseSystemReturnLabel, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ShippingLabelFormat, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ReturnLabelFormat, f => f.Random.Int(1, 100))
				.RuleFor(u => u.DBChannelOrderHeaderRowID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.FulfillmentProcessStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.IntegrationStatus, f => f.Random.Int(1, 100))
				.RuleFor(u => u.IntegrationDateUtc, f => f.Date.Past(0).Date)
				.RuleFor(u => u.CentralOrderUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.OrderDCAssignmentUuid, f => String.Empty)
				;
			#endregion faker data rules
		}
		/// <summary>
		/// Get faker object for OrderDCAssignmentLineDto
		/// </summary>
		/// <param name="dto">OrderDCAssignmentLineDto</param>
		/// <returns>Faker object use to generate data</returns>
		public static Faker<OrderDCAssignmentLineDto> GetFaker(this OrderDCAssignmentLineDto dto)
		{
			#region faker data rules
			return new Faker<OrderDCAssignmentLineDto>()
				.RuleFor(u => u.OrderDCAssignmentLineNum, f => default(long))
				.RuleFor(u => u.DatabaseNum, f => null)
				.RuleFor(u => u.MasterAccountNum, f => null)
				.RuleFor(u => u.ProfileNum, f => null)
				.RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
				.RuleFor(u => u.OrderDCAssignmentNum, f => default(long))
				.RuleFor(u => u.CentralOrderNum, f => default(long))
				.RuleFor(u => u.CentralOrderLineNum, f => default(long))
				.RuleFor(u => u.ChannelOrderID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralProductNum, f => default(long))
				.RuleFor(u => u.DistributionProductNum, f => default(long))
				.RuleFor(u => u.SKU, f => f.Commerce.Product())
				.RuleFor(u => u.ChannelItemID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.OrderQty, f => f.Random.Decimal(1, 1000, 2))
				.RuleFor(u => u.DBChannelOrderLineRowID, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralOrderUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.CentralOrderLineUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.OrderDCAssignmentUuid, f => f.Random.Guid().ToString())
				.RuleFor(u => u.OrderDCAssignmentLineUuid, f => String.Empty)
				;
			#endregion faker data rules
		}

    }
}



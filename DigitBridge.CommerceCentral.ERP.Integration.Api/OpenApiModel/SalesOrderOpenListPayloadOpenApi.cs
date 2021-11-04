

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Bogus;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class SalesOrderOpenListPayloadFind : FilterPayloadBase<SalesOrderOpenListFilter>
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        public string SalesOrderOpenList { get; set; }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int? SalesOrderOpenListCount { get; set; }

        #endregion list service 

        public static SalesOrderOpenListPayloadFind GetSampleData()
        {
            var data = new SalesOrderOpenListPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = SalesOrderOpenListFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class SalesOrderOpenListFilter
    {
        public string SalesOrderUuid { get; set; }

        public string OrderNumberFrom { get; set; }

        public string OrderNumberTo { get; set; }

        public DateTime ShipDateFrom { get; set; }

        public DateTime ShipDateTo { get; set; }

        public DateTime OrderDateFrom { get; set; }

        public DateTime OrderDateTo { get; set; }

        public DateTime UpdateDateUtcFrom { get; set; }

        public DateTime UpdateDateUtcTo { get; set; }

        //public SalesOrderStatus OrderStatus { get; set; }

        public int OrderType { get; set; }

        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public static Faker<SalesOrderOpenListFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<SalesOrderOpenListFilter>()
                .RuleFor(u => u.SalesOrderUuid, f => string.Empty)
                .RuleFor(u => u.OrderNumberFrom, f => string.Empty)
                .RuleFor(u => u.OrderNumberTo, f => string.Empty)
                .RuleFor(u => u.ShipDateFrom, f => f.Date.Past(0).Date.AddDays(-30))
                .RuleFor(u => u.ShipDateTo, f => f.Date.Past(0).Date)
                .RuleFor(u => u.OrderDateFrom, f => f.Date.Past(0).Date.AddDays(-30))
                .RuleFor(u => u.OrderDateTo, f => f.Date.Past(0).Date)
                .RuleFor(u => u.UpdateDateUtcFrom, f => f.Date.Past(0).Date.AddDays(-30))
                .RuleFor(u => u.UpdateDateUtcTo, f => f.Date.Past(0).Date)
                .RuleFor(u => u.OrderType, f => (int)SalesOrderType.ChannelOrder)
                .RuleFor(u => u.CustomerCode, f => string.Empty)
                .RuleFor(u => u.CustomerName, f => string.Empty)
                //.RuleFor(u => u.OrderStatus, f => (int)SalesOrderStatus.Open) 
                ;
            #endregion faker data rules
        }
    }
}


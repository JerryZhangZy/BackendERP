

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using DigitBridge.CommerceCentral.ERPDb;
using Newtonsoft.Json.Serialization;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPFuncApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadAdd
    {
        /// <summary>
        /// (Request Data) SalesOrder object to add.
        /// (Response Data) SalesOrder object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) SalesOrder object to add.")]
        public SalesOrderDataDto SalesOrder { get; set; }

        public static SalesOrderPayloadAdd GetSampleData()
        {
            var data = new SalesOrderPayloadAdd();
            data.SalesOrder = new SalesOrderDataDto().GetFakerData();
            return data;
        }
    }

    //[Serializable()]
    //public class SalesOrderPayloadAddExample : OpenApiExample<SalesOrderPayloadAdd>
    //{
    //    public override IOpenApiExample<SalesOrderPayloadAdd> Build(NamingStrategy namingStrategy = null)
    //    {
    //        var data = new SalesOrderPayloadAdd();
    //        data.SalesOrder = new SalesOrderDataDto().GetFakerData();
    //        this.Examples.Add(
    //            OpenApiExampleResolver.Resolve(
    //                "SalesOrderPayloadAddExample",
    //                data,
    //                namingStrategy
    //            ));

    //        return this;
    //    }
    //}

    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadUpdate
    {
        /// <summary>
        /// (Request Data) SalesOrder object to update.
        /// (Response Data) SalesOrder object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) SalesOrder object to update.")]
        public SalesOrderDataDto SalesOrder { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) SalesOrder object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) SalesOrder object to get.")]
        public SalesOrderDataDto SalesOrder { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple SalesOrders.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple SalesOrders.")]
        public IList<string> SalesOrderUuids { get; set; }

        /// <summary>
        /// (Response) Array of SalesOrder which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of SalesOrder which get by uuid array.")]
        public IList<SalesOrderDataDto> SalesOrders { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadFind : FilterPayloadBase<SalesOrderFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> SalesOrderList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int? SalesOrderListCount { get; set; }

        public static SalesOrderPayloadFind GetSampleData()
        {
            var data = new SalesOrderPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = SalesOrderFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class SalesOrderFilter
    {
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public DateTime OrderDateFrom { get; set; }

        public DateTime OrderDateTo { get; set; }

        public string OrderStatus { get; set; }

        public string OrderType { get; set; }

        public static Faker<SalesOrderFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<SalesOrderFilter>()
                .RuleFor(u => u.CustomerCode, f => string.Empty)
                .RuleFor(u => u.CustomerName, f => string.Empty)
                .RuleFor(u => u.OrderDateTo, f => f.Date.Past(0).Date.Date)
                .RuleFor(u => u.OrderDateFrom, f => f.Date.Past(0).Date.Date.AddDays(-30)) 
                .RuleFor(u => u.OrderStatus, f => "")
                .RuleFor(u => u.OrderType, f => "")
                ;
            #endregion faker data rules
        }
    }

    /// <summary>
    /// Response payload object for Create SalesOrder By CentralOrderUuid API
    /// </summary>
    [Serializable()]
    public class SalesOrderPayloadCreateByCentralOrderUuid : SalesOrderPayload
    {
        [OpenApiPropertyDescription("(Response) SalesOrder UUID.")]
        public string CentralOrderUuid { get; set; }
        public List<string> SalesOrderUuids { get; set; }
    }
}


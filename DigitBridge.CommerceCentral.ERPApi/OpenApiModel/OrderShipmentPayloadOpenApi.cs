

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
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadAdd
    {
        /// <summary>
        /// (Request Data) OrderShipment object to add.
        /// (Response Data) OrderShipment object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) OrderShipment object to add.")]
        public OrderShipmentDataDto OrderShipment { get; set; }

        public static OrderShipmentPayloadAdd GetSampleData()
        {
            var data = new OrderShipmentPayloadAdd();
            data.OrderShipment = new OrderShipmentDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadUpdate
    {
        /// <summary>
        /// (Request Data) OrderShipment object to update.
        /// (Response Data) OrderShipment object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) OrderShipment object to update.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadGetSingle
    {
        /// <summary>
        /// (Response Data) OrderShipment object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) OrderShipment object to get.")]
        public OrderShipmentDataDto OrderShipment { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple OrderShipments.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple OrderShipments.")]
        public IList<string> OrderShipmentUuids { get; set; }

        /// <summary>
        /// (Response) Array of OrderShipment which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of OrderShipment which get by uuid array.")]
        public IList<OrderShipmentDataDto> OrderShipments { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class OrderShipmentPayloadFind : FilterPayloadBase<OrderShipmentFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> OrderShipmentList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int? OrderShipmentListCount { get; set; }
        public static OrderShipmentPayloadFind GetSampleData()
        {
            var data = new OrderShipmentPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = OrderShipmentFilter.GetFaker().Generate()
            };
            return data;
        }

    }

    [Serializable()]
    public class OrderShipmentFilter
    {
        public string ChannelNum { get; set; }
        public string ChannelAccountNum { get; set; }
        public string OrderDCAssigmentNum { get; set; }
        public string CentralOrderNum { get; set; }
        public string ChannelOrderID { get; set; }
        public string ShipmentID { get; set; }
        public string WarehouseID { get; set; }
        public string WarehouseCode { get; set; }
        public string InvoiceNumber { get; set; }
        public string OrderNumber { get; set; }
        public string MainTrackingNumber { get; set; }
        public string MainReturnTrackingNumber { get; set; }
        public int ShipmentType { get; set; }
        public int ProcessStatus { get; set; }
        public string ShippingClass { get; set; }
        public string ShippingCarrier { get; set; }
        public DateTime ShipDateFrom { get; set; }
        public DateTime ShipDateTo { get; set; }


        public static Faker<OrderShipmentFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<OrderShipmentFilter>()
                .RuleFor(u => u.ChannelNum, f => string.Empty)
                .RuleFor(u => u.ChannelAccountNum, f => string.Empty)
                .RuleFor(u => u.OrderDCAssigmentNum, f => string.Empty)
                .RuleFor(u => u.CentralOrderNum, f => string.Empty)
                .RuleFor(u => u.ChannelOrderID, f => string.Empty)
                .RuleFor(u => u.ShipmentID, f => string.Empty)
                .RuleFor(u => u.WarehouseID, f => string.Empty)
                .RuleFor(u => u.ShipmentType, f => 0)
                .RuleFor(u => u.MainTrackingNumber, f => string.Empty)
                .RuleFor(u => u.MainReturnTrackingNumber, f => string.Empty)
                .RuleFor(u => u.ProcessStatus, f => f.Random.Number())
                ;
            #endregion faker data rules
        }
    }


    [Serializable()]
    public class OrderShipmentFromSalesOrderReqest
    {
        /// <summary>
        /// (Request Data) salesorderuuid.
        /// </summary>
        [OpenApiPropertyDescription("(Request Data) salesorderuuid.")]
        public string SalesOrderUuid { get; set; }
    }
    [Serializable()]
    public class OrderShipmentFromSalesOrderResponse : ResponsePayloadBase
    {
        /// <summary>
        /// (Response Data) OrderShipmetUuid.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) OrderShipmetUuid.")]
        public string OrderShipmetUuid { get; set; }
    }
}




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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Request and Response payload object for Add API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadAdd
    {
        /// <summary>
        /// (Request Data) Invoice object to add.
        /// (Response Data) Invoice object which has been added.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Invoice object to add.")]
        public InvoiceDataDto Invoice { get; set; }

        internal static InvoicePayloadAdd GetSampleData()
        {
            var data = new InvoicePayloadAdd();
            data.Invoice = new InvoiceDataDto().GetFakerData();
            return data;
        }
    }


    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadUpdate
    {
        /// <summary>
        /// (Request Data) Invoice object to update.
        /// (Response Data) Invoice object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) Invoice object to update.")]
        public InvoiceDataDto Invoice { get; set; }
    }



    /// <summary>
    /// Response payload object for GET single API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadGetSingle
    {
        /// <summary>
        /// (Response Data) Invoice object.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Invoice object to get.")]
        public InvoiceDataDto Invoice { get; set; }
    }


    /// <summary>
    /// Request and Response payload object for GET multiple API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadGetMultiple
    {
        /// <summary>
        /// (Request) Array of uuid to get multiple Invoices.
        /// </summary>
        [OpenApiPropertyDescription("(Request) Array of uuid to get multiple Invoices.")]
        public IList<string> InvoiceUuids { get; set; }

        /// <summary>
        /// (Response) Array of Invoice which get by uuid array.
        /// </summary>
        [OpenApiPropertyDescription("(Response) Array of Invoice which get by uuid array.")]
        public IList<InvoiceDataDto> Invoices { get; set; }
    }


    /// <summary>
    /// Response payload object for DELETE API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadDelete
    {
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadFind : FilterPayloadBase<InvoiceFilter>
    {
        /// <summary>
        /// (Response) List result which load by filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response) List result which load by filter and paging.")]
        public IList<Object> InvoiceList { get; set; }

        /// <summary>
        /// (Response) List result count which load by filter and paging.
        /// </summary>
        public int? InvoiceListCount { get; set; }

        public static InvoicePayloadFind GetSampleData()
        {
            var data = new InvoicePayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = InvoiceFilter.GetFaker().Generate()
            };
            return data;
        }
    }

    public class InvoiceFilter
    {
        public string InvoiceNumber { get; set; }

        public string InvoiceType { get; set; }

        public string InvoiceStatus { get; set; }

        public DateTime InvoiceDateFrom { get; set; }

        public DateTime InvoiceDateTo { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string ShippingCarrier { get; set; }

        public string WarehouseCode { get; set; }

        public static Faker<InvoiceFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<InvoiceFilter>()
                .RuleFor(u => u.InvoiceNumber, f => string.Empty)
                .RuleFor(u => u.InvoiceType, f => string.Empty)
                .RuleFor(u => u.InvoiceStatus, f => string.Empty)
                .RuleFor(u => u.InvoiceDateFrom, f => f.Date.Past(0).Date.Date.AddDays(-30))
                .RuleFor(u => u.InvoiceDateTo, f => f.Date.Past(0).Date.Date)
                .RuleFor(u => u.CustomerCode, f => string.Empty)
                .RuleFor(u => u.CustomerName, f => string.Empty)
                .RuleFor(u => u.ShippingCarrier, f => string.Empty)
                .RuleFor(u => u.WarehouseCode, f => string.Empty)
                ;
            #endregion faker data rules
        }
    }

    /// <summary>
    /// Response payload object for Create Invoice By OrderShipmentUuid API
    /// </summary>
    [Serializable()]
    public class InvoicePayloadCreateByOrderShipmentUuid : InvoicePayload
    {
        [OpenApiPropertyDescription("(Response) Invoice Number.")]

        public string OrderShipmentUuid { get; set; }
        public string InvoiceUuid { get; set; }
    }


    /// <summary>
    /// Request Response payload for FIND API
    /// </summary>
    [Serializable()]
    public class InvoiceUnprocessPayloadFind : FilterPayloadBase<InvoiceUnprocessPayloadFilter>
    {
        #region list service

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result which load filter and paging.")]
        public IList<Object> InvoiceUnprocessListPayload { get; set; }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int? InvoiceUnprocessListPayloadCount { get; set; }

        #endregion list service 

        public static InvoiceUnprocessPayloadFind GetSampleData()
        {
            var data = new InvoiceUnprocessPayloadFind()
            {
                LoadAll = false,
                Skip = 0,
                Top = 20,
                SortBy = "",
                Filter = InvoiceUnprocessPayloadFilter.GetFaker().Generate()
            };
            return data;
        }

    }

    public class InvoiceUnprocessPayloadFilter
    {
        public int ChannelNum { get; set; }
        public int ChannelAccountNum { get; set; }
        public int EventProcessActionStatus { get; set; }

        public static Faker<InvoiceUnprocessPayloadFilter> GetFaker()
        {
            #region faker data rules
            return new Faker<InvoiceUnprocessPayloadFilter>()
                .RuleFor(u => u.ChannelNum, f => f.Random.Int(1, 100))
                .RuleFor(u => u.ChannelAccountNum, f => f.Random.Int(1, 100))
                .RuleFor(u => u.EventProcessActionStatus, f => (int)EventProcessActionStatusEnum.Pending)
                ;
            #endregion faker data rules
        }
    }
}


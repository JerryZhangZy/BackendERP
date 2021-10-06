﻿using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.QuickBooks.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(QboInvoiceApi))]
    public static class QboInvoiceApi
    {
        /// <summary>
        /// Export erp invoice to quick book invoice.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(ExportInvoice))]
        [OpenApiOperation(operationId: "ExportInvoice", tags: new[] { "Quick Books Invoices" }, Summary = "Export erp invoice to quick book invoice by erp invoice number.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadSave))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> ExportInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var invoiceExportService = new QboInvoiceService(payload, dataBaseFactory);
            await invoiceExportService.Export(invoiceNumber);
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Get Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetQboInvoiceList))]
        [OpenApiOperation(operationId: "GetQboInvoiceList", tags: new[] { "Quick Books Invoices" }, Summary = "Get Quick Books invoices by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> GetQboInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var invoiceExportService = new QboInvoiceService(payload, dataBaseFactory);
            await invoiceExportService.GetQboInvoiceList(invoiceNumber);
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Get Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteQboInvoiceList))]
        [OpenApiOperation(operationId: "DeleteQboInvoiceList", tags: new[] { "Quick Books Invoices" }, Summary = "Delete Quick Books invoices by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> DeleteQboInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "Delete", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var invoiceExportService = new QboInvoiceService(payload, dataBaseFactory);
            await invoiceExportService.DeleteQboInvoiceList(invoiceNumber);
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Void Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(VoidQboInvoiceList))]
        [OpenApiOperation(operationId: "VoidQboInvoiceList", tags: new[] { "Quick Books Invoices" }, Summary = "Void Quick Books invoices by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> VoidQboInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "Patch", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var invoiceExportService = new QboInvoiceService(payload, dataBaseFactory);
            await invoiceExportService.VoidQboInvoiceList(invoiceNumber);
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }
    }
}

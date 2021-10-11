using DigitBridge.CommerceCentral.ApiCommon;
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
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadSave))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> ExportInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboInvoiceService(payload, dataBaseFactory);
            payload.Success = await service.ExportAsync(invoiceNumber);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Get Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetQboInvoice))]
        [OpenApiOperation(operationId: "GetQboInvoice", tags: new[] { "Quick Books Invoices" }, Summary = "Get Quick Books invoice by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> GetQboInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboInvoiceService(payload, dataBaseFactory);
            payload.Success = await service.GetQboInvoiceAsync(invoiceNumber);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Get Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteQboInvoice))]
        [OpenApiOperation(operationId: "DeleteQboInvoice", tags: new[] { "Quick Books Invoices" }, Summary = "Delete Quick Books invoice by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> DeleteQboInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "Delete", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboInvoiceService(payload, dataBaseFactory);
            payload.Success = await service.DeleteQboInvoiceAsync(invoiceNumber);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }

        /// <summary>
        /// Void Qbo Invoice list by erp invoice number.
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(VoidQboInvoice))]
        [OpenApiOperation(operationId: "VoidQboInvoice", tags: new[] { "Quick Books Invoices" }, Summary = "Void Quick Books invoice by erp invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboInvoicePayloadGetMultiple))]
        public static async Task<JsonNetResponse<QboInvoicePayload>> VoidQboInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "Patch", Route = "quickBooksInvoices/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboInvoiceService(payload, dataBaseFactory);
            payload.Success = await service.VoidQboInvoiceAsync(invoiceNumber);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboInvoicePayload>(payload);
        }
    }
}

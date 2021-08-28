using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice
    /// </summary> 
    [ApiFilter(typeof(InvoiceApi))]
    public static class InvoiceApi
    {
        /// <summary>
        /// Get one invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoice))]
        [OpenApiOperation(operationId: "GetInvoice", tags: new[] { "Invoices" }, Summary = "Get one invoice by invoiceNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "InvoiceUuids", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "InvoiceUuids", Description = "Invoice Uuids. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/{invoiceNumber}")] HttpRequest req,
            ILogger log,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.GetByInvoiceNumberAsync(invoiceNumber, payload);
            if (success)
            {
                payload.Invoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Get list by search criteria
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [FunctionName(nameof(GetInvoiceList))]
        [OpenApiOperation(operationId: "GetInvoiceList", tags: new[] { "Invoices" }, Summary = "Get one invoice by invoiceNumbers")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "InvoiceUuids", In = ParameterLocation.Query, Required = false, Type = typeof(string[]), Summary = "InvoiceUuids", Description = "Array of InvoiceUuids.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetMultiple), Description = "mulit invoice.")]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload = await srv.GetListByInvoiceUuidAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Delete invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoices))]
        [OpenApiOperation(operationId: "DeleteInvoices", tags: new[] { "Invoices" }, Summary = "Delete one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "rowNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "invoice rowNum", Description = "Sales invoice rowNum. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadDelete))]
        public static async Task<JsonNetResponse<InvoicePayload>> DeleteInvoices(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoices/{rowNum}")] HttpRequest req,
           long rowNum)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.DeleteByRowNumAsync(payload, rowNum);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        ///  Update invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoices))]
        [OpenApiOperation(operationId: "UpdateInvoices", tags: new[] { "Invoices" }, Summary = "Update one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate))]
        public static async Task<JsonNetResponse<InvoicePayload>> UpdateInvoices(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }
        /// <summary>
        /// Add invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoices))]
        [OpenApiOperation(operationId: "AddInvoices", tags: new[] { "Invoices" }, Summary = "Add one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayload>> AddInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(InvoicesList))]
        [OpenApiOperation(operationId: "InvoicesList", tags: new[] { "Invoices" }, Summary = "Load invoices list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayload>> InvoicesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceList(dataBaseFactory, new InvoiceQuery());
            payload = await srv.GetInvoiceListAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Add invoice
        /// </summary>
        [FunctionName(nameof(Sample_Invocies_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvociesSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayloadAdd>> Sample_Invocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePayloadAdd>(InvoicePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_Invoice_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoiceFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice find")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayloadFind>> Sample_Invoice_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices/find")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePayloadFind>(InvoicePayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportInvoices))]
        [OpenApiOperation(operationId: "ExportInvoices", tags: new[] { "Invoices" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoices/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InvoiceManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-invoices.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportInvoices))]
        [OpenApiOperation(operationId: "ImportInvoices", tags: new[] { "Invoices" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "File", In = ParameterLocation.Query, Type = typeof(IFormFile), Summary = "File", Description = "submit by form", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        public static async Task<InvoicePayload> ImportInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoices/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new InvoiceManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }
    }
}


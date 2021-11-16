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
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice
    /// </summary> 
    [ApiFilter(typeof(InvoiceApi))]
    public static class InvoiceApi
    {
        [FunctionName(nameof(CheckInvoiceNumberExist))]
        [OpenApiOperation(operationId: "CheckInvoiceNumberExist", tags: new[] { "Invoices" }, Summary = "exam an invoice number whether been used")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<bool> CheckInvoiceNumberExist(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/existinvoiceNumber/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            int masterAccountNum = req.Headers["masterAccountNum"].ToInt();
            int profileNum = req.Headers["profileNum"].ToInt(); 
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var srv = new InvoiceService(dataBaseFactory);

            return await srv.ExistInvoiceNumber(invoiceNumber, masterAccountNum, profileNum);
        }

        /// <summary>
        /// Get one invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoice))]
        [OpenApiOperation(operationId: "GetInvoice", tags: new[] { "Invoices" }, Summary = "Get one invoice by invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/{invoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
            ILogger log,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, invoiceNumber);
            if (payload.Success)
            {
                payload.Invoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Get list by invoice numbers.
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [FunctionName(nameof(GetListByInvoiceNumbers))]
        [OpenApiOperation(operationId: "GetListByInvoiceNumbers", tags: new[] { "Invoices" }, Summary = "Get invoice list by invoice numbers")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "invoiceNumbers", Description = "Array of invoiceNumber.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetMultiple), Description = "mulit invoice.")]
        public static async Task<JsonNetResponse<InvoicePayload>> GetListByInvoiceNumbers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            await srv.GetListByInvoiceNumbersAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Delete invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteByInvoiceNumber))]
        [OpenApiOperation(operationId: "DeleteByInvoiceNumber", tags: new[] { "Invoices" }, Summary = "Delete one invoice by invoice number.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadDelete))]
        public static async Task<JsonNetResponse<InvoicePayload>> DeleteByInvoiceNumber(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoices/{invoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
           string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.DeleteByInvoiceNumberAsync(payload, invoiceNumber);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        ///  Update invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoice))]
        [OpenApiOperation(operationId: "UpdateInvoice", tags: new[] { "Invoices" }, Summary = "Update one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate))]
        public static async Task<JsonNetResponse<InvoicePayload>> UpdateInvoice(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.AddQboInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<InvoicePayload>(payload);
        }
        /// <summary>
        /// Add invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoice))]
        [OpenApiOperation(operationId: "AddInvoice", tags: new[] { "Invoices" }, Summary = "Add one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayload>> AddInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.Invoice = srv.ToDto();

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.AddQboInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Load invoices list
        /// </summary>
        [FunctionName(nameof(InvoicesList))]
        [OpenApiOperation(operationId: "InvoicesList", tags: new[] { "Invoices" }, Summary = "Load invoices list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayload>> InvoicesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceList(dataBaseFactory, new InvoiceQuery());
            await srv.GetInvoiceListAsync(payload);
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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayloadAdd>> Sample_Invocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<InvoicePayloadAdd>(InvoicePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_Invoice_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoiceFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayloadFind>> Sample_Invoice_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
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
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoices/export")] Microsoft.AspNetCore.Http.HttpRequest req)
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
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        public static async Task<InvoicePayload> ImportInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoices/import")] Microsoft.AspNetCore.Http.HttpRequest req)
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

        /// <summary>
        /// Create Invoice by OrderShipmentUuid
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(CreateInvoiceByOrderShipmentUuid))]
        [OpenApiOperation(operationId: "CreateInvoiceByOrdershipmentUuid", tags: new[] { "Invoices" }, Summary = "Create invoice by OrderShipmentUuid")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadCreateByOrderShipmentUuid), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadCreateByOrderShipmentUuid))]
        public static async Task<InvoicePayload> CreateInvoiceByOrderShipmentUuid(
            [HttpTrigger(AuthorizationLevel.Function, "POST"
            , Route = "invoices/createinvoicebyordershipmentuuid")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InvoiceManager(dbFactory);

            var crtPayLoad = new InvoicePayloadCreateByOrderShipmentUuid()
            {
                OrderShipmentUuid = payload.OrderShipmentUuid
            };
            (bool ret, string invoiceUuid) = await svc.CreateInvoiceByOrderShipmentIdAsync(payload.OrderShipmentUuid);
            if (ret)
            {
                crtPayLoad.Success = true;
                crtPayLoad.InvoiceUuid = invoiceUuid;
            }
            else
            {
                crtPayLoad.Success = false;
                crtPayLoad.InvoiceUuid = "";
            }
            crtPayLoad.Messages = svc.Messages;
            return crtPayLoad;
        }


        /// <summary>
        /// Get sales order summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(InvoiceSummary))]
        [OpenApiOperation(operationId: "InvoiceSummary", tags: new[] { "Invoices" }, Summary = "Get invoice summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Result is List<InvoiceDataDto>")]
        public static async Task<JsonNetResponse<InvoicePayload>> InvoiceSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoices/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceSummaryInquiry(dataBaseFactory, new InvoiceSummaryQuery());
            await srv.InvoiceSummaryAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        #region  Unprocessed Invoices

        /// <summary>
        /// Get invoice unprocess list
        /// </summary>
        [FunctionName(nameof(GetUnprocessedInvoices))]
        [OpenApiOperation(operationId: "GetUnprocessedInvoices", tags: new[] { "Invoices" }, Summary = "Get unprocessed invoices")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetUnprocessedInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices/list/unprocess")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceUnprocessList(dataBaseFactory, new InvoiceUnprocessQuery());
            await srv.GetUnprocessedInvoicesAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        [FunctionName(nameof(Sample_Invoice_Unprocess_Find))]
        [OpenApiOperation(operationId: "Sample_Invoice_Unprocess_Find", tags: new[] { "Sample" }, Summary = "Get new sample of invoice unprocess find")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceUnprocessPayloadFind>> Sample_Invoice_Unprocess_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices/list/unprocess")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<InvoiceUnprocessPayloadFind>(InvoiceUnprocessPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(UpdateFaultInvoices))]
        [OpenApiOperation(operationId: "UpdateFaultInvoices", tags: new[] { "Invoices" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(FaultInvoiceRequestPayload[]), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FaultInvoiceResponsePayload[]))]
        public static async Task<JsonNetResponse<IList<FaultInvoiceResponsePayload>>> UpdateFaultInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "invoices/fault/batchUpdate")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var faultInvoiceList = await req.GetBodyObjectAsync<IList<FaultInvoiceRequestPayload>>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InvoiceManager(dbFactory);
            var result = await svc.UpdateFaultInvoicesAsync(payload, faultInvoiceList);
            return new JsonNetResponse<IList<FaultInvoiceResponsePayload>>(result);
        }

        #endregion
    }
}


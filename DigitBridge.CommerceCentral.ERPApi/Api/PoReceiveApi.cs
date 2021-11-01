using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice payment 
    /// </summary> 
    [ApiFilter(typeof(PoReceiveApi))]
    public static class PoReceiveApi
    {
        /// <summary>
        /// Get po Receives by invoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetPoReceives))]
        [OpenApiOperation(operationId: "GetPoReceives", tags: new[] { "Po Receives" }, Summary = "Get po Receives by invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> GetPoReceives(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "poReceives/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            await srv.GetPaymentWithPoHeaderAsync(payload, invoiceNumber);
            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Get invoice payment by invoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetPoReceive))]
        [OpenApiOperation(operationId: "GetPoReceive", tags: new[] { "Po Receives" }, Summary = "Get invoice payment by invoice number and trannum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> GetPoReceive(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "poReceives/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, invoiceNumber, transNum);
            if (payload.Success)
                payload.PoTransaction = srv.ToDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<PoReceivePayload>(payload);
        }


        /// <summary>
        /// Delete invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeletePoReceives))]
        [OpenApiOperation(operationId: "DeletePoReceives", tags: new[] { "Po Receives" }, Summary = "Delete one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadDelete))]
        public static async Task<JsonNetResponse<PoReceivePayload>> DeletePoReceives(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "poReceives/{invoiceNumber}/{transNum}")]
            HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, invoiceNumber, transNum);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            // if (payload.Success)
            //     srv.DeleteQboRefundEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        ///  Update invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdatePoReceives))]
        [OpenApiOperation(operationId: "UpdatePoReceives", tags: new[] { "Po Receives" }, Summary = "Update one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadUpdate))]
        public static async Task<JsonNetResponse<PoReceivePayload>> UpdatePoReceives(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "poReceives")] HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.PoTransaction = srv.ToDto();
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            //if (payload.Success)
            // srv.AddQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.ApplyInvoices);

            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Add po receive 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddPoReceives))]
        [OpenApiOperation(operationId: "AddPoReceives", tags: new[] { "Po Receives" }, Summary = "Add one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd))]
        public static async Task<JsonNetResponse<PoReceivePayload>> AddPoReceives(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives")] HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.PoTransaction = srv.ToDto();

            //Directly return without waiting this result. 
            //if (payload.Success)
            // srv.AddQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.ApplyInvoices);

            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(PoReceivesList))]
        [OpenApiOperation(operationId: "PoReceivesList", tags: new[] { "Po Receives" }, Summary = "Load poReceives list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind))]
        public static async Task<JsonNetResponse<PoReceivePayload>> PoReceivesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            // var srv = new PoReceiveList(dataBaseFactory, new PoReceiveQuery());
            // await srv.GetPoReceiveListAsync(payload);
            return new JsonNetResponse<PoReceivePayload>(payload);
        }
        
        /// <summary>
        /// Add Invoice payment
        /// </summary>
        [FunctionName(nameof(Sample_PoReceives_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PoReceivesSample", tags: new[] { "Sample" }, Summary = "Get new sample of PoReceive")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd))]
        public static async Task<JsonNetResponse<PoReceivePayloadAdd>> Sample_PoReceives_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/poReceives")] HttpRequest req)
        {
            return new JsonNetResponse<PoReceivePayloadAdd>(PoReceivePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_PoReceive_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PoReceiveFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice payment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadFind))]
        public static async Task<JsonNetResponse<PoReceivePayloadFind>> Sample_PoReceive_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/poReceives/find")] HttpRequest req)
        {
            return new JsonNetResponse<PoReceivePayloadFind>(PoReceivePayloadFind.GetSampleData());
        }


        // [FunctionName(nameof(ExportPayment))]
        // [OpenApiOperation(operationId: "ExportPayment", tags: new[] { "Po Receives" })]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayload), Description = "Request Body in json format")]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        // public static async Task<FileContentResult> ExportPayment(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/export")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var svc = new PoReceiveManager(dbFactory);
        //
        //     var exportData = await svc.ExportAsync(payload);
        //     var downfile = new FileContentResult(exportData, "text/csv");
        //     downfile.FileDownloadName = "export-Payments.csv";
        //     return downfile;
        // }
        //
        // [FunctionName(nameof(ImportPayment))]
        // [OpenApiOperation(operationId: "ImportPayment", tags: new[] { "Po Receives" })]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayload))]
        // public static async Task<PoReceivePayload> ImportPayment(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/import")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var files = req.Form.Files;
        //     var svc = new PoReceiveManager(dbFactory);
        //
        //     await svc.ImportAsync(payload, files);
        //     payload.Success = true;
        //     payload.Messages = svc.Messages;
        //     return payload;
        // }

        // /// <summary>
        // /// Get invoice payment summary by search criteria
        // /// </summary>
        // /// <param name="req"></param> 
        // [FunctionName(nameof(PoReceiveSummary))]
        // [OpenApiOperation(operationId: "PoReceiveSummary", tags: new[] { "Po Receives" }, Summary = "Get invoice payment summary")]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Result is List<PoReceiveDataDto>")]
        // public static async Task<JsonNetResponse<PoReceivePayload>> PoReceiveSummary(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/Summary")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var srv = new PoReceiveSummaryInquiry(dataBaseFactory, new PoReceiveSummaryQuery());
        //     await srv.PoReceiveSummaryAsync(payload);
        //     return new JsonNetResponse<PoReceivePayload>(payload);
        // }
        
    }
}


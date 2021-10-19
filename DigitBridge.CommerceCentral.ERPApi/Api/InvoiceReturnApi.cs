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
    /// Process invoice Return 
    /// </summary> 
    [ApiFilter(typeof(InvoiceReturnApi))]
    public static class InvoiceReturnApi
    {
        /// <summary>
        /// Get invoicereturns by invoiceNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoiceReturns))]
        [OpenApiOperation(operationId: "GetInvoiceReturns", tags: new[] { "Invoice returns" }, Summary = "Get  invoice returns by invoiceNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadGetMultiple))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> GetInvoiceReturns(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoiceReturns/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            await srv.GetReturnsAsync(payload, invoiceNumber);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }

        /// <summary>
        /// Get one invoice return by invoiceNumber and transNum
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoiceReturn))]
        [OpenApiOperation(operationId: "GetInvoiceReturn", tags: new[] { "Invoice returns" }, Summary = "Get one invoice return by invoiceNumber and transNum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> GetInvoiceReturn(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoiceReturns/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, invoiceNumber, transNum);
            if (payload.Success)
                payload.InvoiceTransaction = srv.ToDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }


        /// <summary>
        /// Delete invoice return  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoiceReturns))]
        [OpenApiOperation(operationId: "DeleteInvoiceReturns", tags: new[] { "Invoice returns" }, Summary = "Delete one invoice Return ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadDelete))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> DeleteInvoiceReturns(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoiceReturns/{invoiceNumber}/{transNum}")]
            HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, invoiceNumber, transNum);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.ToQboQueueAsync(payload, ErpEventType.DeleteQboRefund);


            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }

        /// <summary>
        ///  Update invoice return  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoiceReturns))]
        [OpenApiOperation(operationId: "UpdateInvoiceReturns", tags: new[] { "Invoice returns" }, Summary = "Update one invoice return ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadUpdate))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> UpdateInvoiceReturns(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoiceReturns")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.ToQboQueueAsync(payload, ErpEventType.InvoiceRetrunToQboRefund);

            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }

        /// <summary>
        /// Add invoice return 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoiceReturns))]
        [OpenApiOperation(operationId: "AddInvoiceReturns", tags: new[] { "Invoice returns" }, Summary = "Add one invoice return ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadAdd))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> AddInvoiceReturns(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoiceReturns")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.InvoiceTransaction = srv.ToDto();

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.ToQboQueueAsync(payload, ErpEventType.InvoiceRetrunToQboRefund);

            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }

        /// <summary>
        /// Load return list
        /// </summary>
        [FunctionName(nameof(InvoiceReturnsList))]
        [OpenApiOperation(operationId: "InvoiceReturnsList", tags: new[] { "Invoice returns" }, Summary = "Load invoice  return list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> InvoiceReturnsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoiceReturns/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnList(dataBaseFactory, new InvoiceReturnQuery());
            await srv.GetInvoiceReturnListAsync(payload);
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }


        /// <summary>
        /// Add Invoice return
        /// </summary>
        [FunctionName(nameof(Sample_InvoiceReturns_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoiceReturnsSample", tags: new[] { "Sample" }, Summary = "Get new sample of Invoice return")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadAdd))]
        public static async Task<JsonNetResponse<InvoiceReturnPayloadAdd>> Sample_InvoiceReturns_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoiceReturns")] HttpRequest req)
        {
            return new JsonNetResponse<InvoiceReturnPayloadAdd>(InvoiceReturnPayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_InvoiceReturn_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoiceReturnFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice return find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceReturnPayloadFind>> Sample_InvoiceReturn_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoiceReturns/find")] HttpRequest req)
        {
            return new JsonNetResponse<InvoiceReturnPayloadFind>(InvoiceReturnPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportReturn))]
        [OpenApiOperation(operationId: "ExportReturn", tags: new[] { "Invoice returns" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportReturn(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoiceReturns/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InvoiceReturnManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-Returns.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportReturn))]
        [OpenApiOperation(operationId: "ImportReturn", tags: new[] { "Invoice returns" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayload))]
        public static async Task<InvoiceReturnPayload> ImportReturn(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoiceReturns/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new InvoiceReturnManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }


        /// <summary>
        /// Get invoice Return summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(InvoiceReturnSummary))]
        [OpenApiOperation(operationId: "InvoiceReturnSummary", tags: new[] { "Invoice returns" }, Summary = "Get invoice return summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Result is List<InvoiceReturnDataDto>")]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> InvoiceReturnSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoiceReturns/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnSummaryInquiry(dataBaseFactory, new InvoiceReturnSummaryQuery());
            await srv.InvoiceReturnSummaryAsync(payload);
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }

        /// <summary>
        /// Get invoice new return by invoiceNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewReturn))]
        [OpenApiOperation(operationId: "NewReturn", tags: new[] { "Invoice returns" }, Summary = "Get invoice new return by invoiceNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> NewReturn(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoiceReturns/newReturn/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.NewReturnAsync(payload, invoiceNumber);
            payload.Messages = srv.Messages;
            payload.InvoiceTransaction = srv.ToDto();
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }
    }
}


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
    /// Process apinvoice payment 
    /// </summary> 
    [ApiFilter(typeof(ApPaymentApi))]
    public static class ApPaymentApi
    {
        /// <summary>
        /// Get apinvoice payments by apinvoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApPayments))]
        [OpenApiOperation(operationId: "GetApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Get apinvoice payments by apinvoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> GetApPayments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApPayments/{apInvoiceNumber}")] HttpRequest req,
            string apInvoiceNumber)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            await srv.GetPaymentWithApHeaderAsync(payload, apInvoiceNumber);
            return new JsonNetResponse<ApPaymentPayload>(payload);
        }

        /// <summary>
        /// Get apinvoice payment by apinvoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApPayment))]
        [OpenApiOperation(operationId: "GetApPayment", tags: new[] { "ApInvoice payments" }, Summary = "Get invoice payment by invoice number and trannum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> GetApPayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApPayments/{apInvoiceNumber}/{transNum}")] HttpRequest req,
            string apInvoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, apInvoiceNumber, transNum);
            if (payload.Success)
                payload.ApTransaction = srv.ToDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<ApPaymentPayload>(payload);
        }


        /// <summary>
        /// Delete apinvoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteApPayments))]
        [OpenApiOperation(operationId: "DeleteApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Delete one apinvoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadDelete))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> DeleteApPayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "ApPayments/{apInvoiceNumber}/{transNum}")]
            HttpRequest req,
            string apInvoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, apInvoiceNumber, transNum);
            payload.Messages = srv.Messages;

            ////Directly return without waiting this result. 
            //if (payload.Success)
            //    srv.DeleteQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<ApPaymentPayload>(payload);
        }

        /// <summary>
        ///  Update apinvoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateApPayments))]
        [OpenApiOperation(operationId: "UpdateApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Update one apinvoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApPaymentPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadUpdate))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> UpdateApPayments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "ApPayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.ApTransaction = srv.ToDto();
            payload.Messages = srv.Messages;

            ////Directly return without waiting this result. 
            ////if (payload.Success)
            //srv.AddQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.ApplyInvoices);

            return new JsonNetResponse<ApPaymentPayload>(payload);
        }

        /// <summary>
        /// Add apinvoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddApPayments))]
        [OpenApiOperation(operationId: "AddApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Add one apinvoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApPaymentPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadAdd))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> AddApPayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ApPayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.ApTransaction = srv.ToDto();

            ////Directly return without waiting this result. 
            ////if (payload.Success)
            //srv.AddQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.ApplyInvoices);

            return new JsonNetResponse<ApPaymentPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(ApPaymentsList))]
        [OpenApiOperation(operationId: "ApPaymentsList", tags: new[] { "ApInvoice payments" }, Summary = "Load ApPayments list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> ApPaymentsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ApPayments/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentList(dataBaseFactory, new ApPaymentQuery());
            await srv.GetApPaymentListAsync(payload);
            return new JsonNetResponse<ApPaymentPayload>(payload);
        } 

        /// <summary>
        /// Add apInvoice payment
        /// </summary>
        [FunctionName(nameof(Sample_ApPayments_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ApPaymentsSample", tags: new[] { "Sample" }, Summary = "Get new sample of ApPayment")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadAdd))]
        public static async Task<JsonNetResponse<ApPaymentPayloadAdd>> Sample_ApPayments_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/ApPayments")] HttpRequest req)
        {
            return new JsonNetResponse<ApPaymentPayloadAdd>(ApPaymentPayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_ApPayment_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ApPaymentFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of apinvoice payment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadFind))]
        public static async Task<JsonNetResponse<ApPaymentPayloadFind>> Sample_ApPayment_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/ApPayments/find")] HttpRequest req)
        {
            return new JsonNetResponse<ApPaymentPayloadFind>(ApPaymentPayloadFind.GetSampleData());
        }


        [FunctionName(nameof(ExportPayment))]
        [OpenApiOperation(operationId: "ExportPayment", tags: new[] { "ApInvoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ApPayments/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new ApPaymentManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-Payments.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportPayment))]
        [OpenApiOperation(operationId: "ImportPayment", tags: new[] { "ApInvoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayload))]
        public static async Task<ApPaymentPayload> ImportPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ApPayments/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new ApPaymentManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }

        /// <summary>
        /// Get apinvoice payment summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(ApPaymentSummary))]
        [OpenApiOperation(operationId: "ApPaymentSummary", tags: new[] { "ApInvoice payments" }, Summary = "Get invoice payment summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Result is List<ApPaymentDataDto>")]
        public static async Task<JsonNetResponse<ApPaymentPayload>> ApPaymentSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ApPayments/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentSummaryInquiry(dataBaseFactory, new ApPaymentSummaryQuery());
            await srv.ApPaymentSummaryAsync(payload);
            return new JsonNetResponse<ApPaymentPayload>(payload);
        }


        ///// <summary>
        ///// Get apinvoice new Payment by apInvoiceNumber
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="apInvoiceNumber"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(NewPaymentByapInvoiceNumber))]
        //[OpenApiOperation(operationId: "NewPaymentByapInvoiceNumber", tags: new[] { "ApInvoice payments" }, Summary = "Get invoice new Payment by apInvoiceNumber")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceNewPaymentPayload))]
        //public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> NewPaymentByapInvoiceNumber(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ApPayments/newPayment/apInvoiceNumber/{apInvoiceNumber}")] HttpRequest req,
        //    string apInvoiceNumber)
        //{
        //    var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new ApPaymentService(dataBaseFactory);
        //    payload.Success = await srv.NewPaymentByapInvoiceNumberAsync(payload, apInvoiceNumber);
        //    payload.Messages = srv.Messages;
        //    return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        //}

        ///// <summary>
        ///// Get apinvoice new Payment by customerCode
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="customerCode"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(NewPaymentByCustomerCode))]
        //[OpenApiOperation(operationId: "NewPaymentByCustomerCode", tags: new[] { "ApInvoice payments" }, Summary = "Get apinvoice new Payment by CustomerCode")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "customerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "customerCode", Description = "Customer code.", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceNewPaymentPayload))]
        //public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> NewPaymentByCustomerCode(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ApPayments/newPayment/customerCode/{customerCode}")] HttpRequest req,
        //    string customerCode)
        //{
        //    var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new ApPaymentService(dataBaseFactory);
        //    payload.Success = await srv.NewPaymentByCustomerCode(payload, customerCode);
        //    payload.Messages = srv.Messages;
        //    return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        //}
    }
}


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
        /// <param name="apInvoiceNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApPayments))]
        [OpenApiOperation(operationId: "GetApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Get apinvoice payments by apinvoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNum", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> GetApPayments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApPayments/{apInvoiceNum}")] HttpRequest req,
            string apInvoiceNum)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            await srv.GetPaymentWithApHeaderAsync(payload, apInvoiceNum);
            return new JsonNetResponse<ApPaymentPayload>(payload);
        }

        /// <summary>
        /// Get apinvoice payment by apinvoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNum"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApPayment))]
        [OpenApiOperation(operationId: "GetApPayment", tags: new[] { "ApInvoice payments" }, Summary = "Get invoice payment by invoice number and trannum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNum", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> GetApPayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApPayments/{apInvoiceNum}/{transNum}")] HttpRequest req,
            string apInvoiceNum, int transNum)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, apInvoiceNum, transNum);
            if (payload.Success)
                payload.ApTransaction = srv.ToDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<ApPaymentPayload>(payload);
        }


        /// <summary>
        /// Delete apinvoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNum"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteApPayments))]
        [OpenApiOperation(operationId: "DeleteApPayments", tags: new[] { "ApInvoice payments" }, Summary = "Delete one apinvoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNum", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadDelete))]
        public static async Task<JsonNetResponse<ApPaymentPayload>> DeleteApPayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "ApPayments/{apInvoiceNum}/{transNum}")]
            HttpRequest req,
            string apInvoiceNum, int transNum)
        {
            var payload = await req.GetParameters<ApPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, apInvoiceNum, transNum);
            payload.Messages = srv.Messages;

            ////Directly return without waiting this result. 
            //if (payload.Success)
            //    srv.DeleteQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum);

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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayloadFind))]
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


        [FunctionName(nameof(ExportApPayment))]
        [OpenApiOperation(operationId: "ExportApPayment", tags: new[] { "ApInvoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportApPayment(
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

        [FunctionName(nameof(ImportApPayment))]
        [OpenApiOperation(operationId: "ImportApPayment", tags: new[] { "ApInvoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApPaymentPayload))]
        public static async Task<ApPaymentPayload> ImportApPayment(
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


        /// <summary>
        /// Get apinvoice new Payment by apInvoiceNum
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewPaymentByApInvoiceNum))]
        #region open api definition
        [OpenApiOperation(operationId: "NewPaymentByApInvoiceNum", tags: new[] { "ApInvoice payments" }, Summary = "Get invoice new Payment by apInvoiceNum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNum", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApNewPaymentPayload))]
        #endregion
        public static async Task<JsonNetResponse<ApNewPaymentPayload>> NewPaymentByApInvoiceNum(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ApPayments/newPayment/apInvoiceNum/{apInvoiceNum}")] HttpRequest req,
            string apInvoiceNum)
        {
            var payload = await req.GetParameters<ApNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.NewPaymentByApInvoiceNumAsync(payload, apInvoiceNum);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<ApNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Get apinvoice new Payment by VendorCode
        /// </summary>
        /// <param name="req"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewPaymentByVendorNum))]
        #region open api definition
        [OpenApiOperation(operationId: "NewPaymentByVendorNum", tags: new[] { "ApInvoice payments" }, Summary = "Get apinvoice new Payment by vendornum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "vendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "VendorCode", Description = "VendorCode.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApNewPaymentPayload))]
        #endregion
        public static async Task<JsonNetResponse<ApNewPaymentPayload>> NewPaymentByVendorNum(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "ApPayments/newPayment/vendorNum/{vendorCode}")] HttpRequest req,
            string vendorCode)
        {
            var payload = await req.GetParameters<ApNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            payload.Success = await srv.NewPaymentByVendorNum(payload, vendorCode);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<ApNewPaymentPayload>(payload);
        }

        [FunctionName(nameof(AddApInvoicePayments))]
        #region open api definition
        [OpenApiOperation(operationId: "AddApInvoicePayments", tags: new[] { "ApInvoice payments" }, Summary = "Add invoice payments of a customer ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApNewPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApNewPaymentPayload))]
        #endregion
        public static async Task<JsonNetResponse<ApNewPaymentPayload>> AddApInvoicePayments(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "apInvoicePayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApNewPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            await srv.UpdateApInvoicePaymentAsync(payload);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<ApNewPaymentPayload>(payload);
        }

        [FunctionName(nameof(UpdateApInvoicePayments))]
        #region open api definition
        [OpenApiOperation(operationId: "UpdateApInvoicePayments", tags: new[] { "ApInvoice payments" }, Summary = "Update apinvoice payments ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApNewPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApNewPaymentPayload))]
        #endregion
        public static async Task<JsonNetResponse<ApNewPaymentPayload>> UpdateApInvoicePayments(
        [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "apInvoicePayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ApNewPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            await srv.UpdateApInvoicePaymentAsync(payload);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<ApNewPaymentPayload>(payload);
        }

        [FunctionName(nameof(GetApPaymentsByPaymentNum))]
        #region open api definition
        [OpenApiOperation(operationId: "GetApPaymentsByPaymentNum", tags: new[] { "ApInvoice payments" }, Summary = "Get apinvoice payments by payment number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "paymentNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "apInvoiceNum", Description = "payment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApNewPaymentPayload))]
        #endregion
        public static async Task<JsonNetResponse<ApNewPaymentPayload>> GetApPaymentsByPaymentNum(
        [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "apInvoicePayments/paymentNumber/{paymentNum}")] HttpRequest req,
        long paymentNum)
        {
            var payload = await req.GetParameters<ApNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApPaymentService(dataBaseFactory);
            await srv.GetByPaymentNumberAsync(payload, paymentNum);
            return new JsonNetResponse<ApNewPaymentPayload>(payload);
        }
    }
}


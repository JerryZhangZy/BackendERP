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
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice payment 
    /// </summary> 
    [ApiFilter(typeof(InvoicePaymentApi))]
    public static class InvoicePaymentApi
    {
        [FunctionName(nameof(ExistCheckNumber))]
        [OpenApiOperation(operationId: "ExistCheckNumber", tags: new[] { "Invoice payments" }, Summary = "exam an invoice number whether been used")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "checkNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<bool> ExistCheckNumber(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoicePayments/existCheckNum/{checkNumber}")] HttpRequest req,
            string checkNumber)
        {
            int masterAccountNum = req.Headers["masterAccountNum"].ToInt();
            int profileNum = req.Headers["profileNum"].ToInt();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var srv = new InvoicePaymentService(dataBaseFactory);

            return await srv.ExistCheckNumber(checkNumber, masterAccountNum, profileNum);
        }

        /// <summary>
        /// Get invoice new Payment by invoiceNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewPaymentByInvoiceNumber))]
        [OpenApiOperation(operationId: "NewPaymentByInvoiceNumber", tags: new[] { "Invoice payments" }, Summary = "Get invoice new Payment by invoiceNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadNew))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> NewPaymentByInvoiceNumber(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoicePayments/newPayment/invoice/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            payload.Success = await srv.NewPaymentByInvoiceNumberAsync(payload, invoiceNumber);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Get invoice new Payment by customerCode
        /// </summary>
        /// <param name="req"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewPaymentByCustomerCode))]
        [OpenApiOperation(operationId: "NewPaymentByCustomerCode", tags: new[] { "Invoice payments" }, Summary = "Get invoice new Payment by CustomerCode")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "customerCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "customerCode", Description = "Customer code.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadNew))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> NewPaymentByCustomerCode(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoicePayments/newPayment/customer/{customerCode}")] HttpRequest req,
            string customerCode)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            payload.Success = await srv.NewPaymentByCustomerCode(payload, customerCode);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Add invoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoicePayments))]
        [OpenApiOperation(operationId: "AddInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Add invoice payments of a customer ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceNewPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadUpdate))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> AddInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoicePayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            await srv.UpdateInvoicePaymentsAsync(payload);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Add invoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoicePaymentsList))]
        [OpenApiOperation(operationId: "UpdateInvoicePaymentsList", tags: new[] { "Invoice payments" }, Summary = "update a list of invoice payments ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceNewPaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadUpdate))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> UpdateInvoicePaymentsList(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoicePayments")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            await srv.UpdateInvoicePaymentsAsync(payload);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Get invoice payments by invoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoicePaymentsByPaymentNumber))]
        [OpenApiOperation(operationId: "GetInvoicePaymentsByPaymentNumber", tags: new[] { "Invoice payments" }, Summary = "Get invoice payments by payment number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "paymentNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "paymentNumber", Description = "Payment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadNew))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> GetInvoicePaymentsByPaymentNumber(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoicePayments/paymentNumber/{paymentNumber}")] HttpRequest req,
            string paymentNumber)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            await srv.GetByPaymentNumberAsync(payload, paymentNumber.ToLong());
            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Get invoice payments by invoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoicePayments))]
        [OpenApiOperation(operationId: "GetInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Get invoice payments by invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> GetInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoicePayments/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            await srv.GetPaymentsAsync(payload, invoiceNumber);
            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Get invoice payment by invoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoicePayment))]
        [OpenApiOperation(operationId: "GetInvoicePayment", tags: new[] { "Invoice payments" }, Summary = "Get invoice payment by invoice number and trannum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> GetInvoicePayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoicePayments/Trans/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            await srv.GetPaymentsAsync(payload, invoiceNumber, transNum);
            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }


        /// <summary>
        /// Delete invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoicePayments))]
        [OpenApiOperation(operationId: "DeleteInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Delete one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadDelete))]
        public static async Task<JsonNetResponse<InvoiceNewPaymentPayload>> DeleteInvoicePayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoicePayments/{invoiceNumber}/{transNum}")]
            HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<InvoiceNewPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, invoiceNumber, transNum);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            if (payload.Success)
                srv.DeleteQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<InvoiceNewPaymentPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(InvoicePaymentsList))]
        [OpenApiOperation(operationId: "InvoicePaymentsList", tags: new[] { "Invoice payments" }, Summary = "Load invoicepayments list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> InvoicePaymentsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoicePayments/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentList(dataBaseFactory, new InvoicePaymentQuery());
            await srv.GetInvoicePaymentListAsync(payload);
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }

        /// <summary>
        /// Add Invoice payment
        /// </summary>
        [FunctionName(nameof(Sample_InvoicePayments_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoicePaymentsSample", tags: new[] { "Sample" }, Summary = "Get new sample of InvoicePayment")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadUpdate))]
        public static async Task<JsonNetResponse<InvoicePaymentPayloadUpdate>> Sample_InvoicePayments_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoicePayments")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePaymentPayloadUpdate>(InvoicePaymentPayloadUpdate.GetSampleData());
        }

        [FunctionName(nameof(Sample_InvoicePayment_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoicePaymentFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice payment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadFind))]
        public static async Task<JsonNetResponse<InvoicePaymentPayloadFind>> Sample_InvoicePayment_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoicePayments/find")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePaymentPayloadFind>(InvoicePaymentPayloadFind.GetSampleData());
        }


        [FunctionName(nameof(ExportPayment))]
        [OpenApiOperation(operationId: "ExportPayment", tags: new[] { "Invoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePaymentPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoicePayments/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InvoicePaymentManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-Payments.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportPayment))]
        [OpenApiOperation(operationId: "ImportPayment", tags: new[] { "Invoice payments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayload))]
        public static async Task<InvoicePaymentPayload> ImportPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoicePayments/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new InvoicePaymentManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }

        /// <summary>
        /// Get invoice payment summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(InvoicePaymentSummary))]
        [OpenApiOperation(operationId: "InvoicePaymentSummary", tags: new[] { "Invoice payments" }, Summary = "Get invoice payment summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadFind), Description = "Result is List<InvoicePaymentDataDto>")]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> InvoicePaymentSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "invoicePayments/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentSummaryInquiry(dataBaseFactory, new InvoicePaymentSummaryQuery());
            await srv.InvoicePaymentSummaryAsync(payload);
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }

        [FunctionName(nameof(InvoicePaymentsListSummary))]
        [OpenApiOperation(operationId: "InvoicePaymentsListSummary", tags: new[] { "Invoice payments" }, Summary = "Load invoice payments list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadFind))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> InvoicePaymentsListSummary(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoicePayments/find/summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoicePaymentList(dataBaseFactory, new InvoicePaymentQuery());
            await srv.GetInvoicePaymentListSummaryAsync(payload);
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }
    }
}


using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice payment 
    /// </summary> 
    [ApiFilter(typeof(InvoicePaymentApi))]
    public static class InvoicePaymentApi
    {
        /// <summary>
        /// Get one invoice payment by orderNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoicePayment))]
        [OpenApiOperation(operationId: "GetInvoicePayment", tags: new[] { "Invoice payments" }, Summary = "Get one invoice payment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SwaggerInvoicePayment<InvoiceTransactionDto>))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> GetInvoicePayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "InvoicePayment/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            payload.Dto = await srv.GetByInvoiceNumberAsync(invoiceNumber);
            payload.InvoiceHeader = await srv.GetInvoiceHeaderAsync(invoiceNumber);
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }


        /// <summary>
        /// Delete invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoicePayments))]
        [OpenApiOperation(operationId: "DeleteInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Delete one invoice payment ")]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> DeleteInvoicePayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "InvoicePayment/{invoiceNumber}")]
            HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.DeleteByInvoiceNumberAsync(invoiceNumber);
            //payload.ResponseData = $"{success} to delete ";
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }

        /// <summary>
        ///  Update invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoicePayments))]
        [OpenApiOperation(operationId: "UpdateInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Update one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<InvoiceTransactionDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> UpdateInvoicePayments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "InvoicePayment")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>(true); 
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var dataDto = new InvoiceTransactionDataDto() { InvoiceTransaction = payload.Dto };
            var success = await srv.UpdateAsync(dataDto);
            //payload.ResponseData = $"{success} to update data";
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }

        /// <summary>
        /// Add invoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoicePayments))]
        [OpenApiOperation(operationId: "AddInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Add one invoice payment ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<InvoiceTransactionDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        public static async Task<JsonNetResponse<InvoicePaymentPayload>> AddInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "InvoicePayment")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePaymentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var dataDto = new InvoiceTransactionDataDto() { InvoiceTransaction = payload.Dto };
            var success = await srv.AddAsync(dataDto);
            //payload.ResponseData = $"{success} to add data, the uuid is:{srv.Data.UniqueId}";
            return new JsonNetResponse<InvoicePaymentPayload>(payload);
        }
    }
}


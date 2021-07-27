using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [OpenApiOperation(operationId: "GetInvoicePayment", tags: new[] { "Invoice payments" }, Summary = "Get one invoice payment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<InvoicePaymentDataDto>))]
        [FunctionName(nameof(GetInvoicePayment))]
        public static async Task<IActionResult> GetInvoicePayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "InvoicePayment/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.GetByInvoiceNumberAsync(invoiceNumber);

            if (success)
            {
                var dto = srv.ToDto(srv.Data);
                return new Response<InvoicePaymentDataDto>(dto, success);
            }
            return new Response<string>("no record found", success);
        }


        /// <summary>
        /// Delete invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [OpenApiOperation(operationId: "DeleteInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Delete one invoice payment ")]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName(nameof(DeleteInvoicePayments))]
        public static async Task<IActionResult> DeleteInvoicePayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "InvoicePayment/{invoiceNumber}")]
            HttpRequest req,
            string invoiceNumber)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.DeleteByInvoiceNumberAsync(invoiceNumber);
            return new Response<string>("Delete invoice payment  result", success);
        }

        /// <summary>
        ///  Update invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoicePayments))]
        [OpenApiOperation(operationId: "UpdateInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Update one invoice payment ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePaymentDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]

        public static async Task<IActionResult> UpdateInvoicePayments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "InvoicePayment")] HttpRequest req,
[FromBodyBinding] InvoicePaymentDataDto dto)
        {
            var parameters = req.GetRequestParameter<PayloadBase>();
            if (parameters.ProfileNum != dto.InvoiceTransaction.ProfileNum
                || parameters.MasterAccountNum != dto.InvoiceTransaction.MasterAccountNum)
                throw new System.Exception("Invalid request");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.UpdateAsync(dto);
            return new Response<string>("Update invoice payment  result", success);
        }

        /// <summary>
        /// Add invoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoicePayments))]
        [OpenApiOperation(operationId: "AddInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Add one invoice payment ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceTransactionDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<IActionResult> AddInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "InvoicePayment")] HttpRequest req)
        {
            InvoiceTransactionDataDto dto=

            var parameters = req.GetRequestParameter<PayloadBase>();
            dto.InvoiceTransaction.ProfileNum = parameters.ProfileNum;
            dto.InvoiceTransaction.MasterAccountNum = parameters.MasterAccountNum;
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.AddAsync(dto);
            return new Response<string>($"new invoice payment  uuid is:{srv.Data.UniqueId}", success);
        }
    }
}


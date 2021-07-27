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
using System;
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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        [FunctionName(nameof(GetInvoicePayment))]
        public static async Task<JsonNetResponse<PayloadBase>> GetInvoicePayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "InvoicePayment/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<PayloadBase>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            payload.ResponseData = await srv.GetByInvoiceNumberAsync(invoiceNumber);
            return new JsonNetResponse<PayloadBase>(payload);
        }


        /// <summary>
        /// Delete invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        [OpenApiOperation(operationId: "DeleteInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Delete one invoice payment ")]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName(nameof(DeleteInvoicePayments))]
        public static async Task<JsonNetResponse<PayloadBase>> DeleteInvoicePayments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "InvoicePayment/{invoiceNumber}")]
            HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<PayloadBase>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.DeleteByInvoiceNumberAsync(invoiceNumber);
            payload.ResponseData = $"{success} to delete ";
            return new JsonNetResponse<PayloadBase>(payload);
        }

        /// <summary>
        ///  Update invoice payment  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoicePayments))]
        [OpenApiOperation(operationId: "UpdateInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Update one invoice payment ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceTransactionDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]

        public static async Task<JsonNetResponse<PayloadBase>> UpdateInvoicePayments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "InvoicePayment")] HttpRequest req)
        {
            var payload = await req.GetParameters<PayloadBase>();
            var dto = await req.GetBodyObjectAsync<InvoiceTransactionDataDto>();
            dto.InvoiceReturnItems = null;
            if (dto.InvoiceTransaction.MasterAccountNum != payload.MasterAccountNum
                || dto.InvoiceTransaction.ProfileNum != payload.ProfileNum)
                throw new Exception("Invalid request");
            payload.ReqeustData = dto;

            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to update data";
            return new JsonNetResponse<PayloadBase>(payload);
        }

        /// <summary>
        /// Add invoice payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoicePayments))]
        [OpenApiOperation(operationId: "AddInvoicePayments", tags: new[] { "Invoice payments" }, Summary = "Add one invoice payment ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceTransactionDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PayloadBase))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<PayloadBase>> AddInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "InvoicePayment")] HttpRequest req)
        {
            var payload = await req.GetParameters<PayloadBase>();
            var dto = await req.GetBodyObjectAsync<InvoiceTransactionDataDto>();
            dto.InvoiceReturnItems = null;
            dto.InvoiceTransaction.MasterAccountNum = payload.MasterAccountNum;
            dto.InvoiceTransaction.ProfileNum = payload.ProfileNum;
            payload.ReqeustData = dto;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new InvoiceTransactionService(dataBaseFactory);
            var success = await srv.AddAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to add data, the uuid is:{srv.Data.UniqueId}";
            return new JsonNetResponse<PayloadBase>(payload);
        }
    }
}


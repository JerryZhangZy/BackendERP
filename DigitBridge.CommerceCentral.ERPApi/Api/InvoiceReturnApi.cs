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
    /// Process invoice return 
    /// </summary> 
    [ApiFilter(typeof(InvoiceReturnApi))]
    public static class InvoiceReturnApi
    {
        /// <summary>
        /// Get one invoice return by orderNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInvoiceReturn))]
        [OpenApiOperation(operationId: "GetInvoiceReturn", tags: new[] { "Invoice returns" }, Summary = "Get one invoice return")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> GetInvoiceReturn(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "InvoiceReturn/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            await srv.GetReturnWithInvoiceHeaderAsync(invoiceNumber, payload);
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        }


        /// <summary>
        /// Delete invoice Return  
        /// </summary>
        /// <param name="req"></param>
        /// <param name="transUuid"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoiceReturns))]
        [OpenApiOperation(operationId: "DeleteInvoiceReturns", tags: new[] { "Invoice returns" }, Summary = "Delete one invoice return ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Transaction uuid", Description = "Transaction uuid. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePaymentPayloadDelete))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> DeleteInvoiceReturns(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "InvoiceReturn/{transUuid}")]
            HttpRequest req,
            string transUuid)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.DeleteByTransUuidAsync(transUuid, payload);
            payload.Messages = srv.Messages;
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
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadUpdate))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> UpdateInvoiceReturns(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "InvoiceReturn")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true); 
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;
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
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceReturnPayloadAdd))]
        public static async Task<JsonNetResponse<InvoiceReturnPayload>> AddInvoicePayments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "InvoiceReturn")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceReturnPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoiceReturnPayload>(payload);
        } 
        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(InvoiceReturnsList))]
        [OpenApiOperation(operationId: "InvoiceReturnsList", tags: new[] { "Invoice returns" }, Summary = "Load invoice  return list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceTransactionPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceTransactionPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceTransactionPayload>> InvoiceReturnsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "InvoiceReturn/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceTransactionPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceReturnList(dataBaseFactory, new InvoiceReturnQuery());
            payload = await srv.GetInvoiceReturnListAsync(payload);
            return new JsonNetResponse<InvoiceTransactionPayload>(payload);
        }
    }
}


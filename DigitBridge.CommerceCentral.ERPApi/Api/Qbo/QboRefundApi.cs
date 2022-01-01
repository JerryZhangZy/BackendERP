using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.QuickBooks.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(QboRefundApi))]
    public static class QboRefundApi
    {
        /// <summary>
        /// Export erp Refund to quick book Refund.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(ExportQboRefund))]
        [OpenApiOperation(operationId: "ExportQboRefund", tags: new[] { "Quick Books Refunds" }, Summary = "Export erp Refund to quick book Refund by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboRefundPayload))]
        public static async Task<JsonNetResponse<QboRefundPayload>> ExportQboRefund(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "quickBooksRefunds/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboRefundPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboRefundService(payload, dataBaseFactory);
            payload.Success = await service.ExportByNumberAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboRefundPayload>(payload);
        }


        /// <summary>
        /// Delete qbo Refund 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteQboRefund))]
        [OpenApiOperation(operationId: "DeleteQboRefund", tags: new[] { "Quick Books Refunds" }, Summary = "Delete quick book Refund by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboRefundPayload))]
        public static async Task<JsonNetResponse<QboRefundPayload>> DeleteQboRefund(
            [HttpTrigger(AuthorizationLevel.Function, "Delete", Route = "quickBooksRefunds/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboRefundPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboRefundService(payload, dataBaseFactory);
            payload.Success = await service.DeleteQboRefundByNumberAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboRefundPayload>(payload);
        }

        ///// <summary>
        ///// void qbo Refund 
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="invoiceNumber"></param>
        ///// <param name="transNum"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(VoidQboRefund))]
        //[OpenApiOperation(operationId: "VoidQboRefund", tags: new[] { "Quick Books Refunds" }, Summary = "Void quick book Refund by erp invoice number and transNum.")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        //public static async Task<JsonNetResponse<QboRefundPayload>> VoidQboRefund(
        //    [HttpTrigger(AuthorizationLevel.Function, "Patch", Route = "quickBooksRefunds/{invoiceNumber}/{transNum}")] HttpRequest req,
        //    string invoiceNumber, int transNum)
        //{
        //    var payload = await req.GetParameters<QboRefundPayload>();
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new QboRefundService(payload, dataBaseFactory);
        //    payload.Success = await service.VoidQboRefundAsync(invoiceNumber, transNum);
        //    payload.Messages = service.Messages;
        //    return new JsonNetResponse<QboRefundPayload>(payload);
        //}

        /// <summary>
        /// get qbo Refund 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetQboRefund))]
        [OpenApiOperation(operationId: "GetQboRefund", tags: new[] { "Quick Books Refunds" }, Summary = "Get quick book Refund by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QboRefundPayload))]
        public static async Task<JsonNetResponse<QboRefundPayload>> GetQboRefund(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "quickBooksRefunds/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboRefundPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboRefundService(payload, dataBaseFactory);
            payload.Success = await service.GetQboRefundByNumberAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboRefundPayload>(payload);
        }
    }
}

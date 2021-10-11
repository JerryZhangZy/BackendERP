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
    [ApiFilter(typeof(QboPaymentApi))]
    public static class QboPaymentApi
    {
        /// <summary>
        /// Export erp Payment to quick book Payment.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(ExportQboPayment))]
        [OpenApiOperation(operationId: "ExportQboPayment", tags: new[] { "Quick Books Payments" }, Summary = "Export erp Payment to quick book Payment by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QboPaymentPayload>> ExportQboPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "quickBooksPayments/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboPaymentService(payload, dataBaseFactory);
            payload.Success = await service.ExportAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboPaymentPayload>(payload);
        }


        /// <summary>
        /// Delete qbo Payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteQboPayment))]
        [OpenApiOperation(operationId: "DeleteQboPayment", tags: new[] { "Quick Books Payments" }, Summary = "Delete quick book Payment by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QboPaymentPayload>> DeleteQboPayment(
            [HttpTrigger(AuthorizationLevel.Function, "Delete", Route = "quickBooksPayments/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboPaymentService(payload, dataBaseFactory);
            payload.Success = await service.DeleteQboPaymentAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboPaymentPayload>(payload);
        }

        /// <summary>
        /// void qbo Payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(VoidQboPayment))]
        [OpenApiOperation(operationId: "VoidQboPayment", tags: new[] { "Quick Books Payments" }, Summary = "Void quick book Payment by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QboPaymentPayload>> VoidQboPayment(
            [HttpTrigger(AuthorizationLevel.Function, "Patch", Route = "quickBooksPayments/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboPaymentService(payload, dataBaseFactory);
            payload.Success = await service.VoidQboPaymentAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboPaymentPayload>(payload);
        }

        /// <summary>
        /// get qbo Payment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetQboPayment))]
        [OpenApiOperation(operationId: "GetQboPayment", tags: new[] { "Quick Books Payments" }, Summary = "Get quick book Payment by erp invoice number and transNum.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QboPaymentPayload>> GetQboPayment(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "quickBooksPayments/{invoiceNumber}/{transNum}")] HttpRequest req,
            string invoiceNumber, int transNum)
        {
            var payload = await req.GetParameters<QboPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new QboPaymentService(payload, dataBaseFactory);
            payload.Success = await service.GetQboPaymentAsync(invoiceNumber, transNum);
            payload.Messages = service.Messages;
            return new JsonNetResponse<QboPaymentPayload>(payload);
        }
    }
}

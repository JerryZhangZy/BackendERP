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
        /// <returns></returns>
        [FunctionName(nameof(ExportQboPayment))]
        [OpenApiOperation(operationId: "ExportQboPayment", tags: new[] { "Quick Books Payments" }, Summary = "Export erp Payment to quick book Payment by erp invoice number.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QboPaymentPayload>> ExportQboPayment(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "quickBooksPayments/{invoiceNumber}")] HttpRequest req,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<QboPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var PaymentExportService = new QboPaymentService(payload, dataBaseFactory);
            await PaymentExportService.Export(invoiceNumber);
            return new JsonNetResponse<QboPaymentPayload>(payload);
        } 
         
    }
}

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
    [ApiFilter(typeof(MiscPaymentApi))]
    public static class MiscPaymentApi
    {
        /// <summary>
        /// Get apinvoice payments by apinvoice num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="miscInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetMiscPayments))]
        [OpenApiOperation(operationId: "GetMiscPayments", tags: new[] { "MiscInvoice payments" }, Summary = "Get miscinvoice payments by miscinvoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "miscInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "miscInvoiceNumber", Description = "miscInvoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscPaymentPayloadGetMulti))]
        public static async Task<JsonNetResponse<MiscPaymentPayload>> GetMiscPayments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "miscPayments/{miscInvoiceNumber}")] HttpRequest req,
            string miscInvoiceNumber)
        {
            var payload = await req.GetParameters<MiscPaymentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoicePaymentService(dataBaseFactory);
            await srv.GetPaymentWithMiscInvoiceHeaderAsync(payload, miscInvoiceNumber);
            return new JsonNetResponse<MiscPaymentPayload>(payload);
        } 
    }
}


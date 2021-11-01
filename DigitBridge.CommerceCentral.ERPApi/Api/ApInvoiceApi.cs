using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;


namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    /// <summary>
    /// Process invoice
    /// </summary> 
    [ApiFilter(typeof(InvoiceApi))]
   public static class ApInvoiceApi
    {
        /// <summary>
        /// Get one invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="apinvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApInvoice))]
        [OpenApiOperation(operationId: "GetApInvoice", tags: new[] { "apInvoices" }, Summary = "Get one invoice by invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> GetApInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "apInvoices/{apInvoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
            ILogger log,
            string apInvoiceNumber)
        {
            var payload = await req.GetParameters<ApInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, apInvoiceNumber);
            if (payload.Success)
            {
                payload.ApInvoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<ApInvoicePayload>(payload);
        }

        /// <summary>
        /// Add apinvoice
        /// </summary>
        [FunctionName(nameof(Sample_Apinvocies_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ApInvociesSample", tags: new[] { "Sample" }, Summary = "Get new sample of a/p invoice")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadAdd))]
        public static async Task<JsonNetResponse<ApInvoicePayloadAdd>> Sample_Apinvocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/apInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<ApInvoicePayloadAdd>(ApInvoicePayloadAdd.GetSampleData());
        }
    }
}

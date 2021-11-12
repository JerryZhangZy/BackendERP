using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
//using FaultInvoiceResponsePayload= DigitBridge.CommerceCentral.ERP.Integration.Api.

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(CommerceCentralInvoiceApi))]
    public static class CommerceCentralInvoiceApi
    {
        /// <summary>
        /// Get unprocessed invoices
        /// </summary>
        [FunctionName(nameof(GetUnprocessedInvoices))]
        [OpenApiOperation(operationId: "GetUnprocessedInvoices", tags: new[] { "CommerceCentralInvoices" }, Summary = "Get unprocessed invoices")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetUnprocessedInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "commercecentral/invoices/list/unprocess")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceUnprocessList(dataBaseFactory, new InvoiceUnprocessQuery());
            await srv.GetUnprocessedInvoicesAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        [FunctionName(nameof(Sample_Invoice_Unprocess_Find))]
        [OpenApiOperation(operationId: "Sample_Invoice_Unprocess_Find", tags: new[] { "Sample" }, Summary = "Get new sample of invoice unprocess find")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceUnprocessPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceUnprocessPayloadFind>> Sample_Invoice_Unprocess_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices/list/unprocess")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<InvoiceUnprocessPayloadFind>(InvoiceUnprocessPayloadFind.GetSampleData());
        }
    }
}

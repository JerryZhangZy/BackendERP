using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(ChannelInvoiceBroker))]
    public static class ChannelInvoiceBroker
    {
        /// <summary>
        /// Load InvoiceUnprocessList
        /// </summary>
        [FunctionName(nameof(GetInvoiceUnprocessList))]
        [OpenApiOperation(operationId: "GetInvoiceUnprocessList", tags: new[] { "Invoices" }, Summary = "Get invoice unprocess list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceUnprocessListPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoiceUnprocessListPayloadFind))]
        public static async Task<JsonNetResponse<InvoiceUnprocessListPayload>> GetInvoiceUnprocessList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoiceUnprocessListPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceUnprocessList(dataBaseFactory, new InvoiceQuery());
            await srv.GetInvoiceUnprocessListAsync(payload);
            return new JsonNetResponse<InvoiceUnprocessListPayload>(payload);
        }
    }
}

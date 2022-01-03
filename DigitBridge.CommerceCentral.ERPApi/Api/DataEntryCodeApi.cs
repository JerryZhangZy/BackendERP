using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.YoPoco;
using System.Net;

namespace DigitBridge.CommerceCentral.ERPApi
{
    [ApiFilter(typeof(DataEntryCodeApi))]
    public static class DataEntryCodeApi
    {
        [FunctionName(nameof(SelectList))]
        [OpenApiOperation(operationId: "SelectList", tags: new[] { "DataEntryCode" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SelectListPayloadInput), Description = "select list")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SelectListPayload))]
        public static async Task<JsonNetResponse<SelectListPayload>> SelectList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "dataEntryCode/selectList")] HttpRequest req,
            ILogger log)
        {
            var payload = await req.GetParameters<SelectListPayload>(true);
            payload.Skip = 0;
            payload.LoadAll = false;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var slf = new SelectListFactory(dataBaseFactory);
            var result = await slf.GetSelectListAsync(payload);
            return new JsonNetResponse<SelectListPayload>(payload, System.Net.HttpStatusCode.OK);
        }
    }
}

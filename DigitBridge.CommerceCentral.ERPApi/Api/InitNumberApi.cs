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
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Process InitNumberApi
    /// </summary> 
    [ApiFilter(typeof(InitNumberApi))]
    public static class InitNumberApi
    {
        /// <summary>
        /// Load InitNumbers list
        /// </summary>
        [FunctionName(nameof(InitNumbersList))]
        [OpenApiOperation(operationId: "InitNumbersList", tags: new[] { "InitNumbers" }, Summary = "Load InitNumber list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumbersPayload))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> InitNumbersList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "initNumbers")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            var success = await srv.GetAllInitNumbersAsync(payload);
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }

        [FunctionName(nameof(UpdateMulti))]
        [OpenApiOperation(operationId: "UpdateMulti", tags: new[] { "InitNumbers" }, Summary = "update InitNumber list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InitNumbersPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumbersPayload))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> UpdateMulti(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "initNumbers")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload);
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }
    }
}

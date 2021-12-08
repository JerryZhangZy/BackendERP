using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(CustomerApi))]
    public static class CustomIOFormatApi
    {


        [FunctionName(nameof(GetCustomIOFormat))]
        [OpenApiOperation(operationId: "GetCustomIOFormat", tags: new[] { "CustomIOFormats" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "formatType", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "formatType", Description = "formatType", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "formatNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "formatNumber", Description = "formatNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadGetSingle))]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> GetCustomIOFormat(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customIOFormats/{formatType}/{formatNumber}")] HttpRequest req,
            string formatType ,string formatNumber)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomIOFormatService(dbFactory);

           
            //if (await svc.GetCustomerByCustomerCodeAsync(payload, customerCode))
            //    payload.Customer = svc.ToDto();
            //else
            //    payload.Messages = svc.Messages;
            return new JsonNetResponse<CustomIOFormatPayload>(payload);

        }
    }
}

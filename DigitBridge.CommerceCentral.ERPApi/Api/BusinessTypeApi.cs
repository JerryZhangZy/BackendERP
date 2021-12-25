using System.IO;
using System.Net;
using System.Threading.Tasks;
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
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    public static class BusinessTypeApi
    {
        [FunctionName("GetBusinessTypes")]
        #region open api definition
        [OpenApiOperation(operationId: "GetBusinessTypes", tags: new[] { "Business Types" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(BusinessTypePayload))]
        #endregion
        public static async Task<JsonNetResponse<BusinessTypePayload>> GetBusinessTypes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "businessType")] HttpRequest req)
        {
            var payload = await req.GetParameters<BusinessTypePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new BusinessTypeService(dbFactory);
            payload.Success = svc.GetBusinessTypes(payload);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<BusinessTypePayload>(payload);
        }

        [FunctionName(nameof(UpdateBusinessTypes))]
        #region open api definition
        [OpenApiOperation(operationId: "UpdateBusinessTypes", tags: new[] { "Business Types" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BusinessTypePayload), Description = "paid by map data ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(BusinessTypePayload))]
        #endregion
        public static async Task<JsonNetResponse<BusinessTypePayload>> UpdateBusinessTypes(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "businessType")] HttpRequest req)
        {
            var payload = await req.GetParameters<BusinessTypePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new BusinessTypeService(dbFactory);
            svc.UpdateGetBusinessTypes(payload);

            return new JsonNetResponse<BusinessTypePayload>(payload);
        }
    }
}


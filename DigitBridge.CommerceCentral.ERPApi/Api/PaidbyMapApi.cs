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
using System.Linq;

namespace DigitBridge.CommerceCentral.ERPApi
{
    public static class PaidbyMapApi
    {
        [FunctionName("GetPaybyMaps")]
        #region open api definition
        [OpenApiOperation(operationId: "GetPaybyMaps", tags: new[] { "PaybyMaps" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PaidbyMapPayload))]
        #endregion
        public static async Task<JsonNetResponse<PaidbyMapPayload>> GetPaybyMaps(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "paidbyMap")] HttpRequest req)
        {
            var payload = await req.GetParameters<PaidbyMapPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PaidbyMapService(dbFactory);
            payload.Success = svc.GetPaidbyMaps(payload);
            payload.Messages = svc.Messages;
            return new JsonNetResponse<PaidbyMapPayload>(payload);
        }

        [FunctionName(nameof(UpdatePaidbyMaps))]
        #region open api definition
        [OpenApiOperation(operationId: "UpdatePaidbyMaps", tags: new[] { "PaybyMaps" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PaidbyMapPayload), Description = "paid by map data ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PaidbyMapPayload))]
        #endregion
        public static async Task<JsonNetResponse<PaidbyMapPayload>> UpdatePaidbyMaps(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "paidbyMap")] HttpRequest req)
        {
            var payload = await req.GetParameters<PaidbyMapPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PaidbyMapService(dbFactory);
            svc.UpdatePaidbyMaps(payload);

            return new JsonNetResponse<PaidbyMapPayload>(payload);
        }
    }
}


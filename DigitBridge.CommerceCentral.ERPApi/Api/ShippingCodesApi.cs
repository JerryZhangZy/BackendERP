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

namespace DigitBridge.CommerceCentral.ERPApi
{
    public static class ShippingCodesApi
    {
        [FunctionName("GetShippingCodes")]
        #region open api definition
        [OpenApiOperation(operationId: "GetPaybyMaps", tags: new[] { "Shipping Codes" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShippingCodesPayload))]
        #endregion
        public static async Task<JsonNetResponse<ShippingCodesPayload>> GetShippingCodes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "shippingCodes")] HttpRequest req)
        {
            var payload = await req.GetParameters<ShippingCodesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new ShippingCodesService(dbFactory);
            payload.Success = svc.GetShippingCodes(payload);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ShippingCodesPayload>(payload);
        }

        [FunctionName(nameof(UpdateShippingCodes))]
        #region open api definition
        [OpenApiOperation(operationId: "UpdateShippingCodes", tags: new[] { "Shipping Codes" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ShippingCodesPayload), Description = "paid by map data ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShippingCodesPayload))]
        #endregion
        public static async Task<JsonNetResponse<ShippingCodesPayload>> UpdateShippingCodes(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "shippingCodes")] HttpRequest req)
        {
            var payload = await req.GetParameters<ShippingCodesPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new ShippingCodesService(dbFactory);
            svc.UpdateShippingCodes(payload);

            return new JsonNetResponse<ShippingCodesPayload>(payload);
        }
    }
}


using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.ApiCommon;
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

namespace DigitBridge.CommerceCentral.ERPApi
{
    [ApiFilter(typeof(InventoryLogApi))]
    public static class InventoryLogApi
    {
        [FunctionName(nameof(GetInventoryLogs))]
        [OpenApiOperation(operationId: "GetInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "logUuids", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "logUuids", Description = "Transaction ID Arrays", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayload), Description = "Result is InventoryLogs")]
        public static async Task<JsonNetResponse<InventoryLogPayload>> GetInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "inventoryLogs/{logUuid?}")] HttpRequest req,
            string logUuid)
        {
            var payload = await req.GetParameters<InventoryLogPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);

            if (!string.IsNullOrEmpty(logUuid))
            {
                payload.LogUuids.Add(logUuid);
            }
            var svc = new InventoryLogService(dbFactory);
            var result = svc.GetListByUuid(payload);

            return new JsonNetResponse<InventoryLogPayload>(result);
        }

        [FunctionName(nameof(DeleteInventoryLogs))]
        [OpenApiOperation(operationId: "DeleteInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "logUuid", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "logUuid", Description = "Transaction ID ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayload), Description = "return delete count")]
        public static async Task<JsonNetResponse<InventoryLogPayload>> DeleteInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "inventoryLogs/{logUuid}")] HttpRequest req,
            string logUuid)
        {
            var payload = await req.GetParameters<InventoryLogPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            payload.LogUuids.Add(logUuid);
            var svc = new InventoryLogService(dbFactory);
            var result= svc.DeleteByLogUuid(payload);
            return new JsonNetResponse<InventoryLogPayload>(result);
        }
        [FunctionName(nameof(AddInventoryLogs))]
        [OpenApiOperation(operationId: "AddInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<InventoryLogDto>), Description = "InventoryLogList ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayload), Description = "return add count")]
        public static async Task<JsonNetResponse<InventoryLogPayload>> AddInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "inventoryLogs")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryLogPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new InventoryLogService(dbFactory);

            var result= svc.AddList(payload);
            return new JsonNetResponse<InventoryLogPayload>(result);
        }

        [FunctionName(nameof(UpdateInventoryLogs))]
        [OpenApiOperation(operationId: "UpdateInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<InventoryLogDto>), Description = "InventoryLogList ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayload), Description = "return update count")]
        public static async Task<JsonNetResponse<InventoryLogPayload>> UpdateInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PATCH", Route = "inventoryLogs")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryLogPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new InventoryLogService(dbFactory);

            var result= svc.UpdateInventoryLogList(payload);

            return new JsonNetResponse<InventoryLogPayload>(result);
        }
    }
}


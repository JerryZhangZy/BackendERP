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
        [FunctionName(nameof(InventoryLogList))]
        #region open api definition
        [OpenApiOperation(operationId: "InventoryLogList", tags: new[] { "InventoryLogs" }, Summary = "Load inventorylog list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryLogPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayloadFind))]
        #endregion
        public static async Task<JsonNetResponse<InventoryLogPayload>> InventoryLogList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "inventoryLogs/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryLogPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryLogList(dataBaseFactory, new InventoryLogQuery());
            payload = await srv.GetInventoryLogListAsync(payload);
            return new JsonNetResponse<InventoryLogPayload>(payload);
        }

        [FunctionName(nameof(InventoryLogSummary))]
        #region open api defintion
        [OpenApiOperation(operationId: "InventoryLogSummary", tags: new[] { "InventoryLogs" }, Summary = "Load inventory list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryLogPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryLogPayload))]
        #endregion
        public static async Task<JsonNetResponse<InventoryLogPayload>> InventoryLogSummary(
          [HttpTrigger(AuthorizationLevel.Function, "post", Route = "inventoryLogs/find/summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryLogPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryLogList(dataBaseFactory, new InventoryLogQuery());
            await srv.GetInventoryLogSummaryAsync(payload);
            return new JsonNetResponse<InventoryLogPayload>(payload);
        }
    }
}


using System.Collections.Generic;
using System.IO;
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
        [OpenApiParameter(name: "logUuid", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "logUuid", Description = "Transaction ID ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<List<InventoryLogDto>>), Description = "Result is List<InventoryLogDt0>")]
        public static async Task<IActionResult> GetInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "inventoryLogs/{logUuid?}")] HttpRequest req,
            string logUuid,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0 ;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; 
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryLogService(dbFactory);
            var list = svc.GetListByUuid(logUuid);
            return new Response<List<InventoryLogDto>>(list);
        }

        [FunctionName(nameof(DeleteInventoryLogs))]
        [OpenApiOperation(operationId: "DeleteInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "logUuid", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "logUuid", Description = "Transaction ID ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<int>), Description = "return delete count")]
        public static async Task<IActionResult> DeleteInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "inventoryLogs/{logUuid?}")] HttpRequest req,
            string logUuid,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; 
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryLogService(dbFactory);
            var deletecount = svc.DeleteByLogUuid(logUuid);
            return new Response<int>(deletecount);
        }
        [FunctionName(nameof(AddInventoryLogs))]
        [OpenApiOperation(operationId: "AddInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<InventoryLogDto>), Description = "InventoryLogList ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<int>), Description = "return add count")]
        public static async Task<IActionResult> AddInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "inventoryLogs")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryLogService(dbFactory);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<InventoryLogDto> dtolist = JsonConvert.DeserializeObject<List<InventoryLogDto>>(requestBody);

            var addcount = svc.AddList(dtolist);
            return new Response<int>(addcount);
        }

        [FunctionName(nameof(UpdateInventoryLogs))]
        [OpenApiOperation(operationId: "UpdateInventoryLogs", tags: new[] { "InventoryLogs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(List<InventoryLogDto>), Description = "InventoryLogList ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<int>), Description = "return update count")]
        public static async Task<IActionResult> UpdateInventoryLogs(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PATCH", Route = "inventoryLogs")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0; 
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryLogService(dbFactory);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<InventoryLogDto> dtolist = JsonConvert.DeserializeObject<List<InventoryLogDto>>(requestBody);

            int updatecount = svc.UpdateInventoryLogList(dtolist);

            return new Response<int>(updatecount);
        }
    }
}


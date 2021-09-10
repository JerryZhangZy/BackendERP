using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Utility.Enums;
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
    [ApiFilter(typeof(InventoryUpdateApi))]
    public static class InventoryUpdateApi
    {
        [FunctionName(nameof(GetInventoryUpdate))]
        [OpenApiOperation(operationId: "GetInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "BatchNumber", Description = "InventoryUpdate Number,ProfileNum-BatchNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadGetSingle))]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> GetInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "inventoryUpdates/{BatchNumber}")] HttpRequest req,
            string BatchNumber = null)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryUpdateService(dbFactory);

            var spilterIndex = BatchNumber.IndexOf("-");
            var batchNumber = BatchNumber;
            if (spilterIndex > 0 && BatchNumber.StartsWith(payload.ProfileNum.ToString()))
            {
                batchNumber = BatchNumber.Substring(spilterIndex + 1);
            }
            if (await svc.GetInventoryUpdateByBatchNumberAsync(payload, batchNumber))
                payload.InventoryUpdate = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<InventoryUpdatePayload>(payload);

        }

        [FunctionName(nameof(GetMultiInventoryUpdates))]
        [OpenApiOperation(operationId: "GetMultiInventoryUpdates", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "BatchNumbers", Description = "BatchNumber Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadGetMultiple))]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> GetMultiInventoryUpdates(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "inventoryUpdates")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new InventoryUpdateService(dbFactory);
            payload =await svc.GetInventoryUpdatesByCodeArrayAsync(payload);
            return new JsonNetResponse<InventoryUpdatePayload>(payload);

        }

        [FunctionName(nameof(AddInventoryUpdate))]
        [OpenApiOperation(operationId: "AddInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadAdd), Description = "InventoryUpdateDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadAdd))]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> AddInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "inventoryUpdates")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryUpdateService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.InventoryUpdate = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<InventoryUpdatePayload>(payload);
        }

        [FunctionName(nameof(UpdateInventoryUpdate))]
        [OpenApiOperation(operationId: "UpdateInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadUpdate), Description = "InventoryUpdateDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadUpdate))]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> UpdateInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "inventoryUpdates")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryUpdateService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.InventoryUpdate = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<InventoryUpdatePayload>(payload);
        }

        [FunctionName(nameof(DeleteInventoryUpdate))]
        [OpenApiOperation(operationId: "DeleteInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "BatchNumber", Description = "BatchNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> DeleteInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "inventoryUpdates/{BatchNumber}")] HttpRequest req,
            string BatchNumber)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryUpdateService(dbFactory);
            var spilterIndex = BatchNumber.IndexOf("-");
            var batchNumber = BatchNumber;
            if (spilterIndex > 0 && batchNumber.StartsWith(payload.ProfileNum.ToString()))
            {
                batchNumber = BatchNumber.Substring(spilterIndex + 1);
            }
            payload.BatchNumbers.Add(batchNumber);
            if (await svc.DeleteByBatchNumberAsync(payload, batchNumber))
                payload.InventoryUpdate = null;
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<InventoryUpdatePayload>(payload);
        }

        /// <summary>
        /// Load inventoryUpdate list
        /// </summary>
        [FunctionName(nameof(InventoryUpdatesList))]
        [OpenApiOperation(operationId: "InventoryUpdatesList", tags: new[] { "InventoryUpdates" }, Summary = "Load inventoryUpdate list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadFind))]
        public static async Task<JsonNetResponse<InventoryUpdatePayload>> InventoryUpdatesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "inventoryUpdates/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryUpdateList(dataBaseFactory, new InventoryUpdateQuery());
            payload = await srv.GetInventoryUpdateListAsync(payload);
            return new JsonNetResponse<InventoryUpdatePayload>(payload);
        }

        /// <summary>
        /// Add inventoryUpdate
        /// </summary>
        [FunctionName(nameof(Sample_InventoryUpdate_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InventoryUpdateAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of inventoryUpdate")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadAdd))]
        public static async Task<JsonNetResponse<InventoryUpdatePayloadAdd>> Sample_InventoryUpdate_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/inventoryUpdates")] HttpRequest req)
        {
            return new JsonNetResponse<InventoryUpdatePayloadAdd>(InventoryUpdatePayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find inventoryUpdate
        /// </summary>
        [FunctionName(nameof(Sample_InventoryUpdate_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InventoryUpdateFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of inventoryUpdate find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadFind))]
        public static async Task<JsonNetResponse<InventoryUpdatePayloadFind>> Sample_InventoryUpdate_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/inventoryUpdates/find")] HttpRequest req)
        {
            return new JsonNetResponse<InventoryUpdatePayloadFind>(InventoryUpdatePayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportInventoryUpdate))]
        [OpenApiOperation(operationId: "ExportInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryUpdatePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "inventoryUpdates/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryUpdatePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryUpdateManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-inventoryUpdate.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportInventoryUpdate))]
        [OpenApiOperation(operationId: "ImportInventoryUpdate", tags: new[] { "InventoryUpdates" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "InventoryUpdateType", In = ParameterLocation.Path, Required = true, Type = typeof(InventoryUpdateType), Summary = "InventoryUpdateType", Description = "InventoryUpdateType", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryUpdatePayload))]
        public static async Task<InventoryUpdatePayload> ImportInventoryUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "inventoryUpdates/import/{InventoryUpdateType}")] HttpRequest req, int InventoryUpdateType)
        { 
            var payload = await req.GetParameters<InventoryUpdatePayload>();
            payload.InventoryUpdateType = (InventoryUpdateType)InventoryUpdateType;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new InventoryUpdateManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }
    }
}


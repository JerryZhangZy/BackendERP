using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
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
    [ApiFilter(typeof(SystemCodesApi))]
    public static class SystemCodesApi
    {
        [FunctionName(nameof(GetSystemCodes))]
        [OpenApiOperation(operationId: "GetSystemCodes", tags: new[] { "SystemCodess" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SystemCodesUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "SystemCodesUuid", Description = "SystemCodesUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayloadGetSingle))]
        public static async Task<JsonNetResponse<SystemCodesPayload>> GetSystemCodes(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "systemCodess/{SystemCodesUuid}")] HttpRequest req,
            string SystemCodesUuid = null)
        {
            var payload = await req.GetParameters<SystemCodesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemCodesService(dbFactory);

            var spilterIndex = SystemCodesUuid.IndexOf("-");
            var systemCodesCode = SystemCodesUuid;
            if (spilterIndex > 0 && systemCodesCode.StartsWith(payload.ProfileNum.ToString()))
            {
                systemCodesCode = SystemCodesUuid.Substring(spilterIndex + 1);
            }
            if (await svc.GetSystemCodesBySystemCodesUuidAsync(payload, systemCodesCode))
                payload.SystemCodes = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<SystemCodesPayload>(payload);

        }
        [FunctionName(nameof(GetMultiSystemCodess))]
        [OpenApiOperation(operationId: "GetMultiSystemCodess", tags: new[] { "SystemCodess" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SystemCodesUuids", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "SystemCodesUuids", Description = "SystemCodesUuid Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayloadGetMultiple))]
        public static async Task<JsonNetResponse<SystemCodesPayload>> GetMultiSystemCodess(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "systemCodess")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemCodesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new SystemCodesService(dbFactory);
            payload = await svc.GetSystemCodessByUuidArrayAsync(payload);
            return new JsonNetResponse<SystemCodesPayload>(payload);

        }

        [FunctionName(nameof(UpdateSystemCodes))]
        [OpenApiOperation(operationId: "UpdateSystemCodes", tags: new[] { "SystemCodess" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemCodesPayloadUpdate), Description = "SystemCodesDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayloadUpdate))]
        public static async Task<JsonNetResponse<SystemCodesPayload>> UpdateSystemCodes(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "systemCodess")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemCodesPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemCodesService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.SystemCodes = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<SystemCodesPayload>(payload);
        }

        /// <summary>
        /// Load systemCodes list
        /// </summary>
        [FunctionName(nameof(SystemCodessList))]
        [OpenApiOperation(operationId: "SystemCodessList", tags: new[] { "SystemCodess" }, Summary = "Load systemCodes list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemCodesPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayloadFind))]
        public static async Task<JsonNetResponse<SystemCodesPayload>> SystemCodessList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "systemCodess/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemCodesPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SystemCodesList(dataBaseFactory, new SystemCodesQuery());
            await srv.GetSystemCodesListAsync(payload);
            return new JsonNetResponse<SystemCodesPayload>(payload);
        }

        /// <summary>
        /// find systemCodes
        /// </summary>
        [FunctionName(nameof(Sample_SystemCodes_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "SystemCodesFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of systemCodes find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayloadFind))]
        public static async Task<JsonNetResponse<SystemCodesPayloadFind>> Sample_SystemCodes_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/systemCodess/find")] HttpRequest req)
        {
            return new JsonNetResponse<SystemCodesPayloadFind>(SystemCodesPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportSystemCodes))]
        [OpenApiOperation(operationId: "ExportSystemCodes", tags: new[] { "SystemCodess" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemCodesPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportSystemCodes(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "systemCodess/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemCodesPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemCodesManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-systemCodes.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportSystemCodes))]
        [OpenApiOperation(operationId: "ImportSystemCodes", tags: new[] { "SystemCodess" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemCodesPayload))]
        public static async Task<SystemCodesPayload> ImportSystemCodes(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "systemCodess/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemCodesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new SystemCodesManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }
    }
}


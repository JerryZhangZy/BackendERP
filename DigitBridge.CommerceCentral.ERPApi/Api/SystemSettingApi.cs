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
    [ApiFilter(typeof(SystemSettingApi))]
    public static class SystemSettingApi
    {
        [FunctionName(nameof(GetSystemSetting))]
        [OpenApiOperation(operationId: "GetSystemSetting", tags: new[] { "SystemSetting" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemSettingPayloadGetSingle))]
        public static async Task<JsonNetResponse<SystemSettingPayload>> GetSystemSetting(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "systemSettings")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemSettingPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemSettingService(dbFactory);
            payload.Success = await svc.GetAsync(payload);
            payload.Messages = svc.Messages;
            return new JsonNetResponse<SystemSettingPayload>(payload);

        }

        [FunctionName(nameof(UpdateSystemSetting))]
        [OpenApiOperation(operationId: "UpdateSystemSetting", tags: new[] { "SystemSetting" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemSettingPayloadUpdate), Description = "SystemSettingDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemSettingPayloadUpdate))]
        public static async Task<JsonNetResponse<SystemSettingPayload>> UpdateSystemSetting(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "systemSettings")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemSettingPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemSettingService(dbFactory);
            if (await svc.AddOrUpdateAsync(payload))
                payload.SystemSetting = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<SystemSettingPayload>(payload);
        }

        ///// <summary>
        ///// Load systemSetting list
        ///// </summary>
        //[FunctionName(nameof(SystemSettingList))]
        //[OpenApiOperation(operationId: "SystemSettingList", tags: new[] { "SystemSetting" }, Summary = "Load systemSetting list data")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemSettingPayloadFind), Description = "Request Body in json format")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemSettingPayloadFind))]
        //public static async Task<JsonNetResponse<SystemSettingPayload>> SystemSettingList(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "systemSettings/find")] HttpRequest req)
        //{
        //    var payload = await req.GetParameters<SystemSettingPayload>(true);
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new SystemSettingList(dataBaseFactory, new SystemSettingQuery());
        //    await srv.GetSystemSettingListAsync(payload);
        //    return new JsonNetResponse<SystemSettingPayload>(payload);
        //}

        /// <summary>
        /// find systemSetting
        /// </summary>
        [FunctionName(nameof(Sample_SystemSetting_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "SystemSettingFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of systemSetting find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemSettingPayloadFind))]
        public static async Task<JsonNetResponse<SystemSettingPayloadFind>> Sample_SystemSetting_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/systemSettings/find")] HttpRequest req)
        {
            return new JsonNetResponse<SystemSettingPayloadFind>(SystemSettingPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportSystemSetting))]
        [OpenApiOperation(operationId: "ExportSystemSetting", tags: new[] { "SystemSetting" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SystemSettingPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportSystemSetting(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "systemSettings/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemSettingPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SystemSettingManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-systemSetting.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportSystemSetting))]
        [OpenApiOperation(operationId: "ImportSystemSetting", tags: new[] { "SystemSetting" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemSettingPayload))]
        public static async Task<SystemSettingPayload> ImportSystemSetting(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "systemSettings/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<SystemSettingPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new SystemSettingManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }
    }
}


using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(ExportFilesApi))]
    public static class ExportFilesApi
    {

        [FunctionName(nameof(ExportSalesOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportSalesOrderFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ImportExportFilesPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportSalesOrderFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ExportFiles/salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportSalesOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(GetExportSalesOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "GetExportSalesOrderFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "processUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "processUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> GetExportSalesOrderFiles(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ExportFiles/salesorder/result/{processUuid}")] HttpRequest req, string processUuid)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();

            var svc = new ExportBlobService();
            payload.Success = await svc.LoadFileNamesFromBlobAsync(processUuid);

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }
    }
}


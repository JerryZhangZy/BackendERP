using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.QuickBooks.Integration;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.ERPFuncApi;

namespace DigitBridge.CommerceCentral.ERPApi.ERPFuncApi
{
    [ApiFilter(typeof(QboExportLogApi))]
    public static class QboExportLogApi
    {
        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(QboExportLogList))]
        [OpenApiOperation(operationId: "QboExportLogList", tags: new[] { "Quick Books ExportLogs" }, Summary = "Load quickbook exportlog list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(QuickBooksExportLogPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksExportLogPayloadFind))]
        public static async Task<JsonNetResponse<QuickBooksExportLogPayload>> QboExportLogList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "quickBooksExportLogs/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<QuickBooksExportLogPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new QuickBooksExportLogList(dataBaseFactory, new QuickBooksExportLogQuery());
            payload = await srv.GetQuickBooksExportLogListAsync(payload);
            return new JsonNetResponse<QuickBooksExportLogPayload>(payload);
        }

        /// <summary>
        /// find productext
        /// </summary>
        [FunctionName(nameof(Sample_QuickBooksExportLogs_Find))]
        [OpenApiOperation(operationId: "QuickBooksExportLogsFindSample", tags: new[] { "Sample" }, Summary = "Load quickbook exportlog list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadFind))]
        public static async Task<JsonNetResponse<QuickBooksExportLogPayloadFind>> Sample_QuickBooksExportLogs_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/QuickBooksExportLogs/find")] HttpRequest req)
        {
            return new JsonNetResponse<QuickBooksExportLogPayloadFind>(QuickBooksExportLogPayloadFind.GetSampleData());
        }
    }
}

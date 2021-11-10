using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(WMSSalesOrderApi))]
    public static class WMSSalesOrderApi
    {
        /// <summary>
        /// Load sales order list
        /// </summary>
        [FunctionName(nameof(GetSalesOrdersOpenList))]
        [OpenApiOperation(operationId: "GetSalesOrdersOpenList", tags: new[] { "WMSSalesOrder" }, Summary = "Load open sales order list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayloadFind))]
        public static async Task<JsonNetResponse<SalesOrderOpenListPayload>> GetSalesOrdersOpenList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/salesOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderOpenListPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderOpenList(dataBaseFactory, new SalesOrderOpenQuery());
            await srv.GetSalesOrdersOpenListAsync(payload);
            return new JsonNetResponse<SalesOrderOpenListPayload>(payload);
        }


        [FunctionName(nameof(Sample_SalesOrder_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "SalesOrderFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of salesorder find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderOpenListPayloadFind))]
        public static async Task<JsonNetResponse<SalesOrderOpenListPayloadFind>> Sample_SalesOrder_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/salesOrders/find")] HttpRequest req)
        {
            return new JsonNetResponse<SalesOrderOpenListPayloadFind>(SalesOrderOpenListPayloadFind.GetSampleData());
        }
    }
}

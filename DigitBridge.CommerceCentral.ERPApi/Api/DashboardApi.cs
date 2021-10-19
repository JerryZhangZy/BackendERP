using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
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
    [ApiFilter(typeof(DashboardApi))]
    public static class DashboardApi
    {
        /// <summary>
        /// Load warehouseTransfer list
        /// </summary>
        [FunctionName(nameof(GetCompanySummary))]
        [OpenApiOperation(operationId: "GetCompanySummary", tags: new[] { "DashBoards" }, Summary = "Load Company YTD Summary Inquiry")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateFrom", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date From", Description = "Date From", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateTo", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date To", Description = "Date To", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Type = typeof(string), Summary = "Query Name", Description = "Query Name", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "Customer Code", Description = "Customer Code", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SalesCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "SalesCode", Description = "SalesCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CompanySummaryPayload))]
        public static async Task<JsonNetResponse<CompanySummaryPayload>> GetCompanySummary(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "dashboards/companySummary")] HttpRequest req)
        {
            var payload = await req.GetParameters<CompanySummaryPayload>();
            var filter = new SummaryInquiryFilter(payload)
            {
                Name = string.Empty,
                CustomerCode = string.Empty,
                SalesCode = string.Empty,
                DateFrom = req.GetData("dateFrom", ParameterLocation.Query).ToDateTime(),
                DateTo = req.GetData("dateTo", ParameterLocation.Query).ToDateTime()
            };
            payload.Filters = filter;
            var srv = new CompanySummaryService();
            await srv.GetCompanySummaryAsync(payload);
            return new JsonNetResponse<CompanySummaryPayload>(payload);
        }

        /// <summary>
        /// Load warehouseTransfer list
        /// </summary>
        [FunctionName(nameof(GetCustomerSummary))]
        [OpenApiOperation(operationId: "GetCustomerSummary", tags: new[] { "DashBoards" }, Summary = "Load Customer Summary Inquiry")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateFrom", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date From", Description = "Date From", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateTo", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date To", Description = "Date To", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Type = typeof(string), Summary = "Query Name", Description = "Query Name", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "Customer Code", Description = "Customer Code", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SalesCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "SalesCode", Description = "SalesCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CompanySummaryPayload))]
        public static async Task<JsonNetResponse<CompanySummaryPayload>> GetCustomerSummary(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "dashboards/customerSummary")] HttpRequest req)
        {
            var payload = await req.GetParameters<CompanySummaryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var filter = new SummaryInquiryFilter(payload)
            {
                Name = string.Empty,
                CustomerCode = string.Empty,
                SalesCode = string.Empty,
                DateFrom = req.GetData("dateFrom", ParameterLocation.Query).ToDateTime(),
                DateTo = req.GetData("dateTo", ParameterLocation.Query).ToDateTime()
            };
            payload.Filters = filter;
            var srv = new CustomerSummaryInquiry(dbFactory,new CustomerSummaryQuery());
            await srv.GetCustomerSummaryAsync(payload);
            return new JsonNetResponse<CompanySummaryPayload>(payload);
        }

        /// <summary>
        /// Load warehouseTransfer list
        /// </summary>
        [FunctionName(nameof(GetProductSummary))]
        [OpenApiOperation(operationId: "GetProductSummary", tags: new[] { "DashBoards" }, Summary = "Load Product Summary Inquiry")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateFrom", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date From", Description = "Date From", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "dateTo", In = ParameterLocation.Query, Type = typeof(DateTime), Summary = "Date To", Description = "Date To", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Type = typeof(string), Summary = "Query Name", Description = "Query Name", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "CustomerCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "Customer Code", Description = "Customer Code", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SalesCode", In = ParameterLocation.Query, Type = typeof(string), Summary = "SalesCode", Description = "SalesCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CompanySummaryPayload))]
        public static async Task<JsonNetResponse<CompanySummaryPayload>> GetProductSummary(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "dashboards/prodductSummary")] HttpRequest req)
        {
            var payload = await req.GetParameters<CompanySummaryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var filter = new SummaryInquiryFilter(payload)
            {
                Name = string.Empty,
                CustomerCode = string.Empty,
                SalesCode = string.Empty,
                DateFrom = req.GetData("dateFrom", ParameterLocation.Query).ToDateTime(),
                DateTo = req.GetData("dateTo", ParameterLocation.Query).ToDateTime()
            };
            payload.Filters = filter;
            var srv = new ProductSummaryInquiry(dbFactory,new ProductSummaryQuery());
            await srv.GetProductSummaryAsync(payload);
            return new JsonNetResponse<CompanySummaryPayload>(payload);
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(EventProcessApi))]
    public static class EventProcessApi
    {
        /// <summary>
        /// sales order list receive acknowledge API
        /// this will set EventProcessERP ActionStatus = 1
        /// </summary>
        [FunctionName(nameof(AckReceiveSalesOrders))]
        [OpenApiOperation(operationId: "AckReceiveSalesOrders", tags: new[] { "WMSSalesOrder" }, Summary = "Send received EventUuid list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AcknowledgePayload), Description = "Array of Received ProcessUuid")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<string>))]
        public static async Task<JsonNetResponse<AcknowledgePayload>> AckReceiveSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/salesOrders/AckReceive")] HttpRequest req)
        {
            var payload = await req.GetParameters<AcknowledgePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            payload.EventProcessType = EventProcessTypeEnum.SalesOrderToWMS;

            var srv = new EventProcessERPService(dataBaseFactory);
            await srv.UpdateActionStatusAsync(payload);
            return new JsonNetResponse<AcknowledgePayload>(payload);
        }


        [FunctionName(nameof(AckProcessSalesOrders))]
        [OpenApiOperation(operationId: "AckProcessSalesOrders", tags: new[] { "Eventprocesss" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AcknowledgeProcessPayload), Description = "Sales Order Process Result")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AcknowledgeProcessPayload))]
        public static async Task<JsonNetResponse<AcknowledgeProcessPayload>> AckProcessSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/salesOrders/AckProcess")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<AcknowledgeProcessPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            payload.EventProcessType = EventProcessTypeEnum.SalesOrderToWMS;

            var srv = new EventProcessERPService(dataBaseFactory);
            await srv.UpdateProcessStatusAsync(payload);
            return new JsonNetResponse<AcknowledgeProcessPayload>(payload);
        }
    }
}
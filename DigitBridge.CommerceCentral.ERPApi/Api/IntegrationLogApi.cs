using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;
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
    [ApiFilter(typeof(IntegrationLogApi))]
    public static class IntegrationLogApi
    {
        /// <summary>
        /// Load erpevent list
        /// </summary>
        [FunctionName(nameof(CentralOrderTransferLog))]
        [OpenApiOperation(operationId: "CentralOrderTransferLog", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadFind))]
        public static async Task<JsonNetResponse<EventERPPayload>> CentralOrderTransferLog(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/CentralOrderTransfer/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventERPList(dataBaseFactory, new EventERPQuery());
            await srv.GetEventERPList_CentralOrderTransferAsync(payload);
            return new JsonNetResponse<EventERPPayload>(payload);
        }

        /// <summary>
        /// Load activityLog list
        /// </summary>
        [FunctionName(nameof(ActivityLogList))]
        [OpenApiOperation(operationId: "ActivityLogList", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(ActivityLogPayloadFind))]
        public static async Task<JsonNetResponse<ActivityLogPayload>> ActivityLogList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/ActivityLog/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<ActivityLogPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ActivityLogList(dataBaseFactory);
            await srv.GetActivityLogListAsync(payload);
            return new JsonNetResponse<ActivityLogPayload>(payload);
        }

        /// <summary>
        /// Load EventProcessERP list
        /// </summary>
        [FunctionName(nameof(EventProcessERPList))]
        [OpenApiOperation(operationId: "EventProcessERPList", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventProcessERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventProcessERPPayloadFind))]
        public static async Task<JsonNetResponse<EventProcessERPPayload>> EventProcessERPList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/EventProcessERP/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventProcessERPPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventProcessERPList(dataBaseFactory);
            await srv.GetEventProcessERPListAsync(payload);
            return new JsonNetResponse<EventProcessERPPayload>(payload);
        }
        /// <summary>
        /// Load InvoiceToCommerceCentral EventProcessERP list
        /// </summary>
        [FunctionName(nameof(InvoiceToCommerceCentralList))]
        [OpenApiOperation(operationId: "InvoiceToCommerceCentralList", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventProcessERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventProcessERPPayloadFind))]
        public static async Task<JsonNetResponse<EventProcessERPPayload>> InvoiceToCommerceCentralList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/InvoiceToCommerceCentral/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventProcessERPPayload>(true);
            payload.Filter["ERPEventProcessType"] = (int)EventProcessTypeEnum.InvoiceToCommerceCentral;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventProcessERPList(dataBaseFactory);
            await srv.GetEventProcessERPListAsync(payload);
            return new JsonNetResponse<EventProcessERPPayload>(payload);
        }
        /// <summary>
        /// Load SalesOrderToWMS EventProcessERP list
        /// </summary>
        [FunctionName(nameof(SalesOrderToWMSList))]
        [OpenApiOperation(operationId: "SalesOrderToWMSList", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventProcessERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventProcessERPPayloadFind))]
        public static async Task<JsonNetResponse<EventProcessERPPayload>> SalesOrderToWMSList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/SalesOrderToWMS/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventProcessERPPayload>(true);
            payload.Filter["ERPEventProcessType"] = (int)EventProcessTypeEnum.SalesOrderToWMS;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventProcessERPList(dataBaseFactory);
            await srv.GetEventProcessERPListAsync(payload);
            return new JsonNetResponse<EventProcessERPPayload>(payload);
        }
        /// <summary>
        /// Load PoToWMS EventProcessERP list
        /// </summary>
        [FunctionName(nameof(PoToWMSList))]
        [OpenApiOperation(operationId: "PoToWMSList", tags: new[] { "IntegrationLog" },
            Summary = "Load Central Order Transfer to Sales Order Log")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "MasterAccountNum", Description = "From login profile",
            Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int),
            Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventProcessERPPayloadFind),
            Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventProcessERPPayloadFind))]
        public static async Task<JsonNetResponse<EventProcessERPPayload>> PoToWMSList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "IntegrationLog/PoToWMS/find")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<EventProcessERPPayload>(true);
            payload.Filter["ERPEventProcessType"] = (int)EventProcessTypeEnum.PoToWMS;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventProcessERPList(dataBaseFactory);
            await srv.GetEventProcessERPListAsync(payload);
            return new JsonNetResponse<EventProcessERPPayload>(payload);
        }


        [FunctionName(nameof(ReSendEvent))]
        [OpenApiOperation(operationId: "ReSendEvent", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "eventUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "eventUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(EventERPPayloadUpdate))]
        public static async Task<JsonNetResponse<EventERPPayload>> ReSendEvent(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents/resend/{eventUuid}")]
            HttpRequest req, string eventUuid)
        {
            var payload = await req.GetParameters<EventERPPayload>();
            var svc = new EventApi();
            payload.Success = await svc.ResendEventAsync(payload.MasterAccountNum, payload.ProfileNum, eventUuid);
            payload.Messages = svc.Messages;
            return new JsonNetResponse<EventERPPayload>(payload);
        }
    }
}


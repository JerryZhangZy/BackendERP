using System.IO;
using System.Net;
using System.Threading.Tasks;
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

namespace DigitBridge.CommerceCentral.EventERPApi
{
    public static class EventApi
    {
        [FunctionName(nameof(AddEventERP))]
        [OpenApiOperation(operationId: "AddEventERP", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadAdd), Description = "EventERPDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayload>> AddEventERP(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "erpevents")] HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory, MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.AddAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<EventERPPayload>(payload);
        }

        [FunctionName(nameof(UpdateEventERP))]
        [OpenApiOperation(operationId: "UpdateEventERP", tags: new[] { "EventERPs" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadUpdate), Description = "EventERPDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadUpdate))]
        public static async Task<JsonNetResponse<EventERPPayload>> UpdateEventERP(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "erpevents")] HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventERPService(dbFactory,MySingletonAppSetting.AzureWebJobsStorage);
            if (await svc.UpdateAsync(payload))
                payload.EventERP = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<EventERPPayload>(payload);
        }

        /// <summary>
        /// Load erpevent list
        /// </summary>
        [FunctionName(nameof(EventERPsList))]
        [OpenApiOperation(operationId: "EventERPsList", tags: new[] { "EventERPs" }, Summary = "Load erpevent list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventERPPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadFind))]
        public static async Task<JsonNetResponse<EventERPPayload>> EventERPsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "erpevents/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<EventERPPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new EventERPList(dataBaseFactory, new EventERPQuery());
            await srv.GetEventERPListAsync(payload);
            return new JsonNetResponse<EventERPPayload>(payload);
        }

        /// <summary>
        /// Add erpevent
        /// </summary>
        [FunctionName(nameof(Sample_EventERP_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "EventERPAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of erpevent")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadAdd))]
        public static async Task<JsonNetResponse<EventERPPayloadAdd>> Sample_EventERP_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/erpevents")] HttpRequest req)
        {
            return new JsonNetResponse<EventERPPayloadAdd>(EventERPPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find erpevent
        /// </summary>
        [FunctionName(nameof(Sample_EventERP_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "EventERPFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of erpevent find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventERPPayloadFind))]
        public static async Task<JsonNetResponse<EventERPPayloadFind>> Sample_EventERP_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/erpevents/find")] HttpRequest req)
        {
            return new JsonNetResponse<EventERPPayloadFind>(EventERPPayloadFind.GetSampleData());
        }
    }
}


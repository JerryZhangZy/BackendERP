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
    [ApiFilter(typeof(EventProcessBroker))]
    public static class EventProcessBroker
    {
        [FunctionName(nameof(BatchUpdateFaultProcess))]
        [OpenApiOperation(operationId: "BatchUpdateFaultProcess", tags: new[] { "Eventprocesss" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "EventProcessType", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "bussiness type", Description = "Bussiness type. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(EventProcessRequestPayload[]), Description = "UpdateEventDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EventProcessResponsePayload))]
        public static async Task<JsonNetResponse<EventProcessResponsePayload>> BatchUpdateFaultProcess(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "eventProcesss/{EventProcessType}")]
            HttpRequest req, int eventProcessType)
        {
            var rquestDatas = await req.GetBodyObjectAsync<IList<EventProcessRequestPayload>>();
            var payload = await req.GetParameters<EventProcessERPPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new EventProcessERPService(dbFactory);
            var success = await svc.BatchUpdateFaultProcessAsync(rquestDatas, eventProcessType);

            var result = new EventProcessResponsePayload()
            {
                Success = success,
                Messages = svc.Messages
            };

            return new JsonNetResponse<EventProcessResponsePayload>(result);
        }
    }
}
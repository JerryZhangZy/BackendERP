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

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(CentralOrderApi))]
    public static class CentralOrderApi
    {


        /// <summary>
        /// Get one sales order by orderNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="centralOrderNumber"></param>
        [FunctionName(nameof(CentralOrderReference))]
        [OpenApiOperation(operationId: "CentralOrderReference", tags: new[] { "CentralOrders" }, Summary = "Get  CentralOrders info")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "centralOrderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "centralOrderNumber", Description = "centralOrderNumber. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CentralOrderReferencePayload))]
        public static async Task<JsonNetResponse<CentralOrderReferencePayload>> CentralOrderReference(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "centralOrders/Trace/{centralOrderNumber}")] HttpRequest req,
            long centralOrderNumber)
        {
            var payload = await req.GetParameters<CentralOrderReferencePayload>();
            payload.CentralOrderNum = centralOrderNumber;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ChannelOrderManager(dataBaseFactory);
            await srv.CentralOrderReferenceAsync(payload);

            return new JsonNetResponse<CentralOrderReferencePayload>(payload);
        }

        [FunctionName(nameof(CentralOrdersList))]
        [OpenApiOperation(operationId: "CentralOrdersList", tags: new[] { "CentralOrders" }, Summary = "Load central order list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ChannelOrderPayload))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind))]
        public static async Task<JsonNetResponse<ChannelOrderPayload>> CentralOrdersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "centralOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<ChannelOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CentralOrderList(dataBaseFactory, new CentralOrderQuery());
            await srv.GetChannelOrderListAsync(payload);
            return new JsonNetResponse<ChannelOrderPayload>(payload);
        }


        [FunctionName(nameof(ResendChannelOrderToErp))]
        [OpenApiOperation(operationId: "ResendChannelOrderToErp", tags: new[] { "CentralOrders" }, Summary = "Resend one channel order to erp")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
        Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "centralOrderUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "centralOrderUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
        bodyType: typeof(ChannelOrderPayload))]
        public static async Task<JsonNetResponse<ChannelOrderPayload>> ResendChannelOrderToErp(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "centralOrders/resendChannelOrderToErp/{centralOrderUuid}")]
            HttpRequest req, string centralOrderUuid)
        {
            var payload = await req.GetParameters<ChannelOrderPayload>();
            var svc = new IntegrationChannelOrderApi();
            payload.Success = await svc.ReSendChannelOrderToErpAsync(payload, centralOrderUuid);
            payload.Messages = svc.Messages;
            return new JsonNetResponse<ChannelOrderPayload>(payload);
        } 

        [FunctionName(nameof(ResendAllChannelOrderToErp))]
        [OpenApiOperation(operationId: "ResendAllChannelOrderToErp", tags: new[] { "CentralOrders" }, Summary = "Load channel order list by criteria then send them to erp")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
        Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
        bodyType: typeof(ChannelOrderPayload))]
        public static async Task<JsonNetResponse<ChannelOrderPayload>> ResendAllChannelOrderToErp(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "centralOrders/resendAllChannelOrderToErp")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<ChannelOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new IntegrationChannelOrderApi(dataBaseFactory); 
            payload.Success = await svc.ReSendAllChannelOrderToErp(payload);
            payload.Messages = svc.Messages;
            return new JsonNetResponse<ChannelOrderPayload>(payload);
        }
    }
}


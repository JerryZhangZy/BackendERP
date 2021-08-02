using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process order shipment
    /// </summary> 
    [ApiFilter(typeof(ShipmentApi))]
    public static class ShipmentApi
    {
        /// <summary>
        /// Get order shipment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetShipments))]
        [OpenApiOperation(operationId: "GetShipments", tags: new[] { "Shipments" }, Summary = "Get one/multiple order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SwaggerOne<OrderShipmentDataDto>))]
        public static async Task<JsonNetResponse<ShipmentPayload>> GetShipments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments/{orderShipmentNum}")] HttpRequest req,
            ILogger log, string orderShipmentNum)
        {
            var payload = await req.GetParameters<ShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.GetByOrderShipmentNumAsync(orderShipmentNum);
            if (success)
            {
                payload.Dto = srv.ToDto(srv.Data);
            }
            return new JsonNetResponse<ShipmentPayload>(payload);
        }

        /// <summary>
        /// Delete order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderShipmentUuid"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteShipments))]
        [OpenApiOperation(operationId: "DeleteShipments", tags: new[] { "Shipments" }, Summary = "Delete one order shipment")]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderShipmentUuid", Description = "Order shipment uuid. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> DeleteShipments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "shipments/{orderShipmentUuid}")]
            HttpRequest req,
            string orderShipmentUuid)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory); 
            var success = await srv.DeleteByOrderShipmentUuidAsync(orderShipmentUuid, payload);
            //return new Response<string>("Delete order shipment result", success);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        ///  Update order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateShipments))]
        [OpenApiOperation(operationId: "UpdateShipments", tags: new[] { "Shipments" }, Summary = "Update one order shipment")]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<OrderShipmentDataDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> UpdateShipments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }
        /// <summary>
        /// Add order shipment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddShipments))]
        [OpenApiOperation(operationId: "AddShipments", tags: new[] { "Shipments" }, Summary = "Add one order shipment")]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<OrderShipmentDataDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> AddShipments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.AddAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }
    }
}


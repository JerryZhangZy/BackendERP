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
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
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
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteShipments))]
        [OpenApiOperation(operationId: "DeleteShipments", tags: new[] { "Shipments" }, Summary = "Delete one order shipment")]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<ShipmentPayload>> DeleteShipments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "shipments/{orderShipmentNum}")]
            HttpRequest req,
            string orderShipmentNum)
        {
            var payload = await req.GetParameters<ShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new OrderShipmentService(dataBaseFactory); 
            var success = await srv.DeleteByOrderShipmentNumAsync(orderShipmentNum);
            //return new Response<string>("Delete order shipment result", success);
            return new JsonNetResponse<ShipmentPayload>(payload);
        }

        /// <summary>
        ///  Update order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateShipments))]
        [OpenApiOperation(operationId: "UpdateShipments", tags: new[] { "Shipments" }, Summary = "Update one order shipment")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<OrderShipmentDataDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<ShipmentPayload>> UpdateShipments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload.Dto);
            return new JsonNetResponse<ShipmentPayload>(payload);
        }
        /// <summary>
        /// Add order shipment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddShipments))]
        [OpenApiOperation(operationId: "AddShipments", tags: new[] { "Shipments" }, Summary = "Add one order shipment")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SwaggerOne<OrderShipmentDataDto>), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ShipmentPayload))]
        public static async Task<JsonNetResponse<ShipmentPayload>> AddShipments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<ShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.AddAsync(payload.Dto);
            return new JsonNetResponse<ShipmentPayload>(payload);
        }
    }
}


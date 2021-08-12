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
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = false, Type = typeof(long), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadGetSingle))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> GetShipments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments/{orderShipmentNum}")] HttpRequest req,
            ILogger log, long orderShipmentNum)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.GetDataAsync(orderShipmentNum,payload);
            if (success)
            {
                payload.OrderShipment = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Delete order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderShipmentUuid"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteShipments))]
        [OpenApiOperation(operationId: "DeleteShipments", tags: new[] { "Shipments" }, Summary = "Delete one order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "rowNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "rowNum", Description = "rowNum. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadDelete))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> DeleteShipments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "shipments/{rowNum}")]
            HttpRequest req,
            long rowNum)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory); 
            payload.Success = await srv.DeleteByOrderShipmentUuidAsync(payload, rowNum);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        ///  Update order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateShipments))]
        [OpenApiOperation(operationId: "UpdateShipments", tags: new[] { "Shipments" }, Summary = "Update one order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadUpdate))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> UpdateShipments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }
        /// <summary>
        /// Add order shipment
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddShipments))]
        [OpenApiOperation(operationId: "AddShipments", tags: new[] { "Shipments" }, Summary = "Add one order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> AddShipments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Load shipment list
        /// </summary>
        [FunctionName(nameof(ShipmentsList))]
        [OpenApiOperation(operationId: "ShipmentsList", tags: new[] { "Shipments" }, Summary = "Load shipment list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> ShipmentsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentList(dataBaseFactory, new OrderShipmentQuery());
            payload = await srv.GetOrderShipmentListAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Add shipment
        /// </summary>
        [FunctionName(nameof(ShipmentAddSample))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ShipmentAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd))]
        public static async Task<JsonNetResponse<OrderShipmentPayloadAdd>> ShipmentAddSample(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Sample/post:shipments")] HttpRequest req)
        {
            return new JsonNetResponse<OrderShipmentPayloadAdd>(OrderShipmentPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find shipment
        /// </summary>
        [FunctionName(nameof(ShipmentFindSample))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ShipmentFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind))]
        public static async Task<JsonNetResponse<OrderShipmentPayloadFind>> ShipmentFindSample(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Sample/find:shipments")] HttpRequest req)
        {
            return new JsonNetResponse<OrderShipmentPayloadFind>(OrderShipmentPayloadFind.GetSampleData());
        }
    }
}


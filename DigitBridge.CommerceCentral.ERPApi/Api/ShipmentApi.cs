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
        [OpenApiOperation(operationId: "GetShipments", tags: new[] { "Shipments" }, Summary = "Get one/multiple order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "paging", Type = typeof(RequestPaging),Required =false,Explode =true,In =ParameterLocation.Query)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<OrderShipmentDataDto>))]
        [FunctionName(nameof(GetShipments))]
        public static async Task<IActionResult> GetShipments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments/{orderShipmentNum}")] HttpRequest req,
            ILogger log, string orderShipmentNum)
        {
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum").Value;
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.GetByOrderShipmentNumAsync(orderShipmentNum);
            if (success)
            {
                var dto = srv.ToDto(srv.Data);
                return new Response<OrderShipmentDataDto>(dto, success);
            }
            return new Response<string>("no record found", success);
        }

        /// <summary>
        /// Delete order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [OpenApiOperation(operationId: "DeleteShipments", tags: new[] { "Shipments" }, Summary = "Delete one order shipment")]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName(nameof(DeleteShipments))]
        public static async Task<IActionResult> DeleteShipments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "shipments/{orderShipmentNum}")]
            HttpRequest req,
            string orderShipmentNum)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.DeleteByOrderShipmentNumAsync(orderShipmentNum);
            return new Response<string>("Delete order shipment result", success);
        }

        /// <summary>
        ///  Update order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateShipments))]
        [OpenApiOperation(operationId: "UpdateShipments", tags: new[] { "Shipments" }, Summary = "Update one order shipment")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> UpdateShipments(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "shipments")] HttpRequest req,
[FromBodyBinding] OrderShipmentDataDto dto)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.UpdateAsync(dto);
            return new Response<string>("Update order shipment result", success);
        }
        /// <summary>
        /// Add order shipment
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddShipments))]
        [OpenApiOperation(operationId: "AddShipments", tags: new[] { "Shipments" }, Summary = "Add one order shipment")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> AddShipments(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req,
            [FromBodyBinding] OrderShipmentDataDto dto)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.AddAsync(dto);
            return new Response<string>($"New order shipment uuid is:{srv.Data.UniqueId}", success);
        }
    }
}


using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
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

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Process salesorder
    /// </summary> 
    public static class SalesOrderApi
    {
        /// <summary>
        /// Get salesorder
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetSalesOrders", tags: new[] { "SalesOrders" })]
        [FunctionName("GetSalesOrders")]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Example = typeof(SalesOrderDataDto), Description = "The OK response")]
        public static async Task<IActionResult> GetSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesorder/{orderNumber}")] HttpRequest req,
            ILogger log)
        {
            var orderNumber = req.GetRouteObject<string>("orderNumber");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.GetByOrderNumberAsync(orderNumber);
            if (success)
            {
                var dto = srv.ToDto(srv.Data);
                return new OkObjectResult(dto);
            }
            else
                return new NoContentResult();
        }

        /// <summary>
        /// Delete salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "DeleteSalesOrders", tags: new[] { "SalesOrders" })]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName("DeleteSalesOrders")]
        public static async Task<IActionResult> DeleteSalesOrders(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "salesorder/{orderNumber}")]
            HttpRequest req)
        {
            var orderNumber = req.GetRouteObject<string>("orderNumber");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.DeleteByOrderNumberAsync(orderNumber);
            return new OkObjectResult(success);
        }

        /// <summary>
        ///  Update salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateSalesOrders))]
        [OpenApiOperation(operationId: "UpdateSalesOrders", tags: new[] { "SalesOrders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        public static async Task<IActionResult> UpdateSalesOrders(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesorder")] HttpRequest req)
        {
            var dto = await req.GetBodyObjectAsync<SalesOrderDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var result = srv.Update(dto);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Add sales order
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddSalesOrders))]
        [OpenApiOperation(operationId: "AddSalesOrders", tags: new[] { "SalesOrders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        public static async Task<IActionResult> AddSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesorder")] HttpRequest req)
        {
            var dto = await req.GetBodyObjectAsync<SalesOrderDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var result = srv.Add(dto);
            return new OkObjectResult(result);
        }
    }
}


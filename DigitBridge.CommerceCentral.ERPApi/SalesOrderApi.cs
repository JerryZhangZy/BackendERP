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
using DigitBridge.Base.Utility;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "salesOrders/{orderNumber}")] HttpRequest req,
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
    }
}


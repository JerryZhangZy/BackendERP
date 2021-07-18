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
using Newtonsoft.Json;

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
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderHeaderDto), Example = typeof(SalesOrderHeaderDto), Description = "The OK response")]
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

        //[FunctionName(nameof(AddSalesOrders))]
        //[OpenApiOperation(operationId: "AddSalesOrders", tags: new[] { "SalesOrders" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Example = typeof(SalesOrderDataDto), Description = "The OK response")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Example = typeof(SalesOrderDataDto), Description = "The OK response")]
        //public static async Task<IActionResult> AddSalesOrders(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "salesOrders")] HttpRequest req,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    string responseMessage = string.IsNullOrEmpty(name)
        //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"Hello, {name}. This HTTP triggered function executed successfully.";

        //    return new OkObjectResult(responseMessage);
        //}

    }
}


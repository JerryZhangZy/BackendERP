using System.ComponentModel.DataAnnotations;
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
    [CommonAttribute]
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
        //[OpenApiParameter(name: "paging", Type = typeof(RequestPaging),Required =false,Explode =true,In =ParameterLocation.Query)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<SalesOrderDataDto>))]
        [FunctionName("GetSalesOrders")]
        public static async Task<IActionResult> GetSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesorder/{orderNumber}")] HttpRequest req,
            ILogger log)
        {
            var orderNumber = req.GetRouteObject<string>("orderNumber");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.GetByOrderNumberAsync(orderNumber);
            SalesOrderDataDto dto = null;
            if (success)
            {
                dto = srv.ToDto(srv.Data);
            }
            return new Response<SalesOrderDataDto>(dto, success);
        }
         
        /// <summary>
        /// Delete salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [OpenApiOperation(operationId: "DeleteSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Delete one sales order")]
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
            return new Response<string>("Delete sales order result", success);
        }

        /// <summary>
        ///  Update salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateSalesOrders))]
        [OpenApiOperation(operationId: "UpdateSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Update one sales order")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> UpdateSalesOrders(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesorder")] HttpRequest req)
        {
            var dto = await req.GetBodyObjectAsync<SalesOrderDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.UpdateAsync(dto);
            return new Response<string>("Update sales order result", success);
        }
        /// <summary>
        /// Add sales order
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddSalesOrders))]
        [OpenApiOperation(operationId: "AddSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Add one sales order")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> AddSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesorder")] HttpRequest req)
        {
            var dto = await req.GetBodyObjectAsync<SalesOrderDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.AddAsync(dto);
            return new Response<string>($"new sales order uuid is:{srv.Data.UniqueId}", success);
        }
    }
}


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
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(SalesOrderApi))]
    public static class SalesOrderApi
    {
        /// <summary>
        /// Get one sales order by orderNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetSalesOrder", tags: new[] { "SalesOrders" }, Summary = "Get one sales order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        [FunctionName(nameof(GetSalesOrder))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesorder/{orderNumber}")] HttpRequest req,
            string orderNumber)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.GetByOrderNumberAsync(orderNumber);
            if (success)
            {
                payload.ResponseData = srv.ToDto(srv.Data);
            }
            else
                payload.ResponseData = "no record found";
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Get sales order list by search criteria
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [OpenApiOperation(operationId: "GetSalesOrderList", tags: new[] { "SalesOrders" }, Summary = "Get sales order list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>), Description = "Result is List<SalesOrderDataDto>")]
        [FunctionName(nameof(GetSalesOrderList))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetSalesOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);

            payload = await srv.GetListBySalesOrderUuidsNumberAsync(payload);
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Delete salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        [OpenApiOperation(operationId: "DeleteSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Delete one sales order")]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName("DeleteSalesOrders")]
        public static async Task<JsonNetResponse<SalesOrderPayload>> DeleteSalesOrders(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "salesorder/{orderNumber}")]
            HttpRequest req,
            string orderNumber)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.DeleteByOrderNumberAsync(orderNumber);
            payload.ResponseData = $"{success} to delete sales order";
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        ///  Update salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateSalesOrders))]
        [OpenApiOperation(operationId: "UpdateSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Update one sales order")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> UpdateSalesOrders(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to delete sales order";
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }
        /// <summary>
        /// Add sales order
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddSalesOrders))]
        [OpenApiOperation(operationId: "AddSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Add one sales order")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> AddSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.AddAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to add sales order, the uuid is:{srv.Data.UniqueId}";
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }
    }
}


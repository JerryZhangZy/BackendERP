using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
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
    /// demo
    /// </summary> 
    [ApiFilter(typeof(ApiDemo))]
    public static class ApiDemo
    {
        /// <summary>
        /// Get one entity
        /// </summary>
        /// <param name="req"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetOne", tags: new[] { "ApiDemo" }, Summary = "Get one ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "parameterName", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "your parameter", Description = " your parameter", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        [FunctionName(nameof(GetOne))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetOne(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApiDemo/{parameterName}")] HttpRequest req,
            string parameterName)
        {
            // replace SalesOrderPayload to your object
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var payload = await req.GetParameters<SalesOrderPayload>();
            //payload = await srv.GetByOrderNumberAsync(orderNumber);
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Get  list by search criteria
        /// </summary>
        /// <param name="req"></param> 
        /// <param name="salesOrderParameter"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetList", tags: new[] { "ApiDemo" }, Summary = "Get  list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>), Description = "Result is List<SalesOrderDataDto>")]
        [FunctionName(nameof(GetList))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "ApiDemo")] HttpRequest req)
        {
            // replace SalesOrderPayload to your object
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var payload = await req.GetParameters<SalesOrderPayload>();
            //payload = await srv.GetListBySalesOrderUuidsNumberAsync(payload);  
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Get list by search criteria
        /// </summary>
        /// <param name="req"></param> 
        /// <returns></returns>
        [OpenApiOperation(operationId: "FindList", tags: new[] { "ApiDemo" }, Summary = "Get  list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayload), Description = "Result is List<SalesOrderDataDto>")]
        [FunctionName(nameof(FindList))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> FindList(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ApiDemo/parameterfrombody")] HttpRequest req)
        {
            // replace SalesOrderPayload to your object
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var payload = await req.GetParameters<SalesOrderPayload>(true);
            //payload = await srv.GetListBySalesOrderUuidsNumberAsync(payload);
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }


        /// <summary>
        /// Delete one 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        [OpenApiOperation(operationId: "DeleteOne", tags: new[] { "ApiDemo" }, Summary = "Delete one ")]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = " Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [FunctionName(nameof(DeleteOne))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> DeleteOne(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "salesorder/{orderNumber}")]
            HttpRequest req,
            string orderNumber)
        {
            // replace SalesOrderPayload to your object
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(0);
            var payload = await req.GetParameters<SalesOrderPayload>();
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        ///  Update one 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateSalesOrders))]
        [OpenApiOperation(operationId: "UpdateSalesOrders", tags: new[] { "ApiDemo" }, Summary = "Update one ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> UpdateSalesOrders(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesorder")] HttpRequest req)
        {
            // replace SalesOrderPayload to your object
            var payload = await req.GetParameters<SalesOrderPayload, SalesOrderDataDto>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);

            //save payload.ReqeustObject
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }
        /// <summary>
        /// Add one
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddOne))]
        [OpenApiOperation(operationId: "AddOne", tags: new[] { "ApiDemo" }, Summary = "Add one ")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(JsonNetResponse<SalesOrderPayload>))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> AddOne(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesorder")] HttpRequest req)
        {
            // replace SalesOrderPayload to your object
            var payload = await req.GetParameters<SalesOrderPayload, SalesOrderDataDto>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            //save payload.ReqeustObject
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }
    }
}


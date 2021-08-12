using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
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
        [FunctionName(nameof(GetSalesOrder))]
        [OpenApiOperation(operationId: "GetSalesOrder", tags: new[] { "SalesOrders" }, Summary = "Get one sales order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadGetSingle))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesOrders/{orderNumber}")] HttpRequest req,
            string orderNumber)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);
            payload.Success = await srv.GetByOrderNumberAsync(payload, orderNumber);
            if (payload.Success)
                payload.SalesOrder = srv.ToDto(srv.Data);
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Get sales order list by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(GetSalesOrderList))]
        [OpenApiOperation(operationId: "GetSalesOrderList", tags: new[] { "SalesOrders" }, Summary = "Get sales order list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SalesOrderUuids", In = ParameterLocation.Query, Required = false, Type = typeof(IList<string>), Summary = "SalesOrderUuids", Description = "Array of SalesOrderUuid.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadGetMultiple), Description = "Result is List<SalesOrderDataDto>")]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetSalesOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
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
        [FunctionName("DeleteSalesOrders")]
        [OpenApiOperation(operationId: "DeleteSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Delete one sales order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "rowNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadDelete))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> DeleteSalesOrders(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "salesOrders/{rowNum}")]
            HttpRequest req,
            long rowNum)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);
            payload.Success = await srv.DeleteByRowNumAsync(payload, rowNum);
            if (!payload.Success)
                payload.Messages = srv.Messages;
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        ///  Update salesorder 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateSalesOrders))]
        [OpenApiOperation(operationId: "UpdateSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Update one sales order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadUpdate))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> UpdateSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            if (!payload.Success)
                payload.Messages = srv.Messages;
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }
        /// <summary>
        /// Add sales order
        /// </summary>
        [FunctionName(nameof(AddSalesOrders))]
        [OpenApiOperation(operationId: "AddSalesOrders", tags: new[] { "SalesOrders" }, Summary = "Add one sales order")]
        [OpenApiParameter(name: Consts.MasterAccountNum, In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: Consts.ProfileNum, In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadAdd))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> AddSalesOrders(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            if (!payload.Success)
                payload.Messages = srv.Messages;
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Load sales order list
        /// </summary>
        [FunctionName(nameof(SalesOrdersList))]
        [OpenApiOperation(operationId: "SalesOrdersList", tags: new[] { "SalesOrders" }, Summary = "Load sales order list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> SalesOrdersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderList(dataBaseFactory, new SalesOrderQuery());
            payload = await srv.GetSalesOrderListAsync(payload);
            return new JsonNetResponse<SalesOrderPayload>(payload);
        }

        /// <summary>
        /// Add sales order
        /// </summary>
        [FunctionName(nameof(Sample_SalesOrders_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "SalesOrdersSample", tags: new[] { "Sample" }, Summary = "Get new sample of sales order")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadAdd))]
        public static async Task<JsonNetResponse<SalesOrderPayloadAdd>> Sample_SalesOrders_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/salesOrders")] HttpRequest req)
        {
            return new JsonNetResponse<SalesOrderPayloadAdd>(SalesOrderPayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_SalesOrder_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "SalesOrderFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of salesorder find")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind))]
        public static async Task<JsonNetResponse<SalesOrderPayloadFind>> Sample_SalesOrder_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/salesOrders/find")] HttpRequest req)
        {
            return new JsonNetResponse<SalesOrderPayloadFind>(SalesOrderPayloadFind.GetSampleData());
        }
    }
}


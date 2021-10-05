using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
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
            payload.Success = await srv.GetDataAsync(payload, orderNumber);
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
        [OpenApiParameter(name: "orderNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "orderNumbers", Description = "Array of order numbers.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadGetMultiple), Description = "Result is List<SalesOrderDataDto>")]
        public static async Task<JsonNetResponse<SalesOrderPayload>> GetSalesOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "salesOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);

            await srv.GetListByOrderNumbersAsync(payload);
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
        [OpenApiParameter(name: "orderNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "orderNumber", Description = "Sales Order Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadDelete))]
        public static async Task<JsonNetResponse<SalesOrderPayload>> DeleteSalesOrders(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "salesOrders/{orderNumber}")]
            HttpRequest req,
            string orderNumber)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new SalesOrderService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, orderNumber);
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
            payload.SalesOrder = srv.ToDto();
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
            payload.SalesOrder = srv.ToDto();
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
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadAdd))]
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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind))]
        public static async Task<JsonNetResponse<SalesOrderPayloadFind>> Sample_SalesOrder_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/salesOrders/find")] HttpRequest req)
        {
            return new JsonNetResponse<SalesOrderPayloadFind>(SalesOrderPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportSalesOrder))]
        [OpenApiOperation(operationId: "ExportSalesOrder", tags: new[] { "SalesOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "salesOrders/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SalesOrderManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-salesorders.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportSalesOrder))]
        [OpenApiOperation(operationId: "ImportSalesOrder", tags: new[] { "SalesOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayload))]
        public static async Task<SalesOrderPayload> ImportSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "salesOrders/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new SalesOrderManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }

        /// <summary>
        /// Create SalesOrder by CentralOrderUuid
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(CreateSalesOrderByCentralOrderUuid))]
        [OpenApiOperation(operationId: "CreateSalesOrderByCentralOrderUuid", tags: new[] { "SalesOrders" }, Summary = "Create SalesOrder by CentralOrderUuid")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesOrderPayloadCreateByCentralOrderUuid), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesOrderPayloadCreateByCentralOrderUuid))]
        public static async Task<InvoicePayload> CreateSalesOrderByCentralOrderUuid(
            [HttpTrigger(AuthorizationLevel.Function, "POST"
            , Route = "salesOrders/createsalesorderbycentralorderuuid")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new SalesOrderManager(dbFactory);

            var crtPayLoad = req.Body.ToString().StringToObject<SalesOrderPayloadCreateByCentralOrderUuid>();
            (bool ret, List<string> salesOrderNumbers) = await svc.CreateSalesOrderByChannelOrderIdAsync(crtPayLoad.CentralOrderUuid);
            if (ret)
            {
                payload.Success = true;
            }
            else
            {
                payload.Success = false;
            }
            payload.Messages = svc.Messages;
            return payload;
        }

    }
}


using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadGetSingle))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> GetShipments(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments/{orderShipmentNum}")] HttpRequest req,
            ILogger log, long orderShipmentNum)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            var success = await srv.GetDataAsync(payload, orderShipmentNum.ToString());
            if (success)
            {
                payload.OrderShipment = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        [FunctionName(nameof(GetListByOrderShipmentNumbers))]
        [OpenApiOperation(operationId: "GetListByInvoiceNumbers", tags: new[] { "Invoices" }, Summary = "Get invoice list by invoice numbers")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "orderShipmentNumbers", Description = "Array of orderShipmentNumber.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetMultiple), Description = "mulit invoice.")]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> GetListByOrderShipmentNumbers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            await srv.GetListByOrderShipmentNumbersAsync(payload, payload.OrderShipmentNumbers);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Delete order shipment 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteShipments))]
        [OpenApiOperation(operationId: "DeleteShipments", tags: new[] { "Shipments" }, Summary = "Delete one order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "OrderShipmentNum", Description = "orderShipmentNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadDelete))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> DeleteShipments(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "shipments/{orderShipmentNum}")]
            HttpRequest req,
            long orderShipmentNum)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, orderShipmentNum);
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
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
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
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
            payload.OrderShipment = srv.ToDto();
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Load shipment list
        /// </summary>
        [FunctionName(nameof(ShipmentsList))]
        [OpenApiOperation(operationId: "ShipmentsList", tags: new[] { "Shipments" }, Summary = "Load shipment list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> ShipmentsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentList(dataBaseFactory, new OrderShipmentQuery());
            await srv.GetOrderShipmentListAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Add shipment Sample
        /// </summary>
        [FunctionName(nameof(Sample_Shipment_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ShipmentAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd))]
        public static async Task<JsonNetResponse<OrderShipmentPayloadAdd>> Sample_Shipment_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/shipments")] HttpRequest req)
        {
            return new JsonNetResponse<OrderShipmentPayloadAdd>(OrderShipmentPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find shipment
        /// </summary>
        [FunctionName(nameof(Sample_Shipment_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ShipmentFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind))]
        public static async Task<JsonNetResponse<OrderShipmentPayloadFind>> Sample_Shipment_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/shipments/find")] HttpRequest req)
        {
            return new JsonNetResponse<OrderShipmentPayloadFind>(OrderShipmentPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportShipments))]
        [OpenApiOperation(operationId: "ExportShipments", tags: new[] { "Shipments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportShipments(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "shipments/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new OrderShipmentManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-shipments.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportShipments))]
        [OpenApiOperation(operationId: "ImportShipments", tags: new[] { "Shipments" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayload))]
        public static async Task<OrderShipmentPayload> ImportShipments(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "shipments/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new OrderShipmentManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }

        /// <summary>
        /// Get shippment summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(ShipmentSummary))]
        [OpenApiOperation(operationId: "ShipmentSummary", tags: new[] { "Shipments" }, Summary = "Get shippment summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind), Description = "Result is List<ShipmentDataDto>")]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> ShipmentSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "shipments/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ShipmentSummaryInquiry(dataBaseFactory, new ShipmentSummaryQuery());
            await srv.ShipmentSummaryAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

        /// <summary>
        /// Check orderShipmentNum exist
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="orderShipmentNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(CheckShipmentNumExist))]
        [OpenApiOperation(operationId: "CheckShipmentNumExist", tags: new[] { "Shipments" }, Summary = "Check orderShipmentNum exist")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "orderShipmentNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "orderShipmentNum", Description = "Order shipment number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadGetSingle))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> CheckShipmentNumExist(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "shipments/existorderShipmentNum/{orderShipmentNum}")] HttpRequest req,
            ILogger log, long orderShipmentNum)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentService(dataBaseFactory);
            payload.Success = await srv.GetDataAsync(payload, orderShipmentNum.ToString());
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }



        [FunctionName(nameof(ShipmentListSummary))]
        [OpenApiOperation(operationId: "ShipmentListSummary", tags: new[] { "Shipments" }, Summary = "Load Shipments list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadFind))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> ShipmentListSummary(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments/find/summary")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new OrderShipmentList(dataBaseFactory, new OrderShipmentQuery());
            await srv.GetOrderShipmentListSummaryAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }

    }
}


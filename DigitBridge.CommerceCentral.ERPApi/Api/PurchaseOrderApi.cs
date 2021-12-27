using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
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
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPApi
{
    /// <summary>
    /// Purchase Order Api
    /// </summary>
    [ApiFilter(typeof(PurchaseOrderApi))]
    public static class PurchaseOrderApi
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="poNum">poNum</param>
        /// <returns></returns>
        [FunctionName(nameof(ExistPoNum))]
        [OpenApiOperation(operationId: "ExistPoNum", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "PoNum", Description = "PoNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExistPoNumPayload))]
        public static async Task<JsonNetResponse<ExistPoNumPayload>> ExistPoNum(
[HttpTrigger(AuthorizationLevel.Function, "GET", Route = "purchaseOrders/existPoNum/{poNum}")] HttpRequest req,
string poNum = null)
        {
            var payload = await req.GetParameters<ExistPoNumPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderService(dataBaseFactory);

           var result= await srv.GetDataAsync(new PurchaseOrderPayload() {  MasterAccountNum=payload.MasterAccountNum,ProfileNum=payload.ProfileNum}, poNum);
            payload.IsExistPoNum = result;
            payload.Success = true;
            return new JsonNetResponse<ExistPoNumPayload>(payload);

        }





        /// <summary>
        /// Get one purchase order
        /// </summary>
        /// <param name="req">HttpRequest</param>
        /// <param name="poNum">Purchase order number.</param>
        /// <returns></returns>
        [FunctionName(nameof(GetPurchaseOrder))]
        [OpenApiOperation(operationId: "GetPurchaseOrder", tags: new[] { "PurchaseOrders" }, Summary = "Get one purchase order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Purchase order number.", Description = "Purchase order number.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadGetSingle), Description = "The OK response")]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> GetPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "purchaseOrders/{poNum}")] HttpRequest req,
            string poNum)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderService(dataBaseFactory);
            payload.Success = await srv.GetDataAsync(payload, poNum);
            if (payload.Success)
                payload.PurchaseOrder = srv.ToDto(srv.Data);
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }
        /// <summary>
        /// Get purchase order list by poNumbers
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(GetPurchaseOrderList))]
        [OpenApiOperation(operationId: "GetPurchaseOrderList", tags: new[] { "PurchaseOrders" }, Summary = "Get purchase order list by purchase order numbers.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNums", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "Multiple purchase order number.", Description = "Array of purchase order numbers.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadGetMultiple), Description = "Result is List<PurchaseOrderDataDto>")]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> GetPurchaseOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "purchaseOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderService(dataBaseFactory);
            await srv.GetListByOrderNumbersAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }


        [FunctionName(nameof(DeletePurchaseOrder))]
        [OpenApiOperation(operationId: "DeletePurchaseOrder", tags: new[] { "PurchaseOrders" }, Summary = "Delete one purchase order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Purchase order number.", Description = "Purchase order number.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadDelete))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> DeletePurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "purchaseOrders/{poNum}")] HttpRequest req,
            string poNum)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, poNum);
            if (!payload.Success)
                payload.Messages = srv.Messages;
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }
        [FunctionName(nameof(AddPurchaseOrder))]
        [OpenApiOperation(operationId: "AddPurchaseOrder", tags: new[] { "PurchaseOrders" }, Summary = "Add one purchase order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadAdd), Description = "PurchaseOrderDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadAdd))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> AddPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "purchaseOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PurchaseOrderService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.PurchaseOrder = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }

        [FunctionName(nameof(UpdatePurchaseOrder))]
        [OpenApiOperation(operationId: "UpdatePurchaseOrder", tags: new[] { "PurchaseOrders" }, Summary = "Update one purchase order")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadUpdate), Description = "PurchaseOrderDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadUpdate))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> UpdatePurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "purchaseOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PurchaseOrderService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.PurchaseOrder = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }

        /// <summary>
        /// Get purchase order list by criteria.
        /// </summary>
        [FunctionName(nameof(PurchaseOrderList))]
        [OpenApiOperation(operationId: "PurchaseOrderList", tags: new[] { "PurchaseOrders" }, Summary = "Get purchase order list by criteria.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> PurchaseOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "purchaseOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderList(dataBaseFactory, new PurchaseOrderQuery());
            await srv.GetPurchaseOrderListAsync(payload);
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }

        /// <summary>
        /// Add productext
        /// </summary>
        [FunctionName(nameof(Sample_PurchaseOrder_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PurchaseOrderAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of purchaseorder")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadAdd))]
        public static async Task<JsonNetResponse<PurchaseOrderPayloadAdd>> Sample_PurchaseOrder_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/purchaseOrders")] HttpRequest req)
        {
            return new JsonNetResponse<PurchaseOrderPayloadAdd>(PurchaseOrderPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find productext
        /// </summary>
        [FunctionName(nameof(Sample_PurchaseOrder_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PurchaseOrderFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of purchaseorder find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind))]
        public static async Task<JsonNetResponse<PurchaseOrderPayloadFind>> Sample_PurchaseOrder_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/purchaseOrders/find")] HttpRequest req)
        {
            return new JsonNetResponse<PurchaseOrderPayloadFind>(PurchaseOrderPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportPurchaseOrder))]
        [OpenApiOperation(operationId: "ExportPurchaseOrder", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "purchaseOrders/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PurchaseOrderManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-purchaseorder.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportPurchaseOrder))]
        [OpenApiOperation(operationId: "ImportPurchaseOrder", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayload))]
        public static async Task<PurchaseOrderPayload> ImportPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "purchaseOrders/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new PurchaseOrderManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }



        [FunctionName(nameof(PurchaseOrderListSummary))]
        [OpenApiOperation(operationId: "PurchaseOrderListSummary", tags: new[] { "PurchaseOrders" }, Summary = "Load PurchaseOrders list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> PurchaseOrderListSummary(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = "purchaseOrders/find/summary")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PurchaseOrderList(dataBaseFactory, new PurchaseOrderQuery());
            await srv.GetPurchaseOrderListSummaryAsync(payload);
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }
    }
}


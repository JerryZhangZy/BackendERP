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
    [ApiFilter(typeof(PurchaseOrderApi))]
    public static class PurchaseOrderApi
    {
        [FunctionName(nameof(GetPurchaseOrder))]
        [OpenApiOperation(operationId: "GetPurchaseOrder", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "poNum", Description = "PoNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadGetSingle), Description = "The OK response")]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> GetPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "purchaseOrders/{poNum}")] HttpRequest req,
            string poNum)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PurchaseOrderService(dbFactory);
            var spilterIndex = poNum.IndexOf("-");
            var ponum = poNum;
            if (spilterIndex > 0 && ponum.StartsWith(payload.ProfileNum.ToString()))
            {
                ponum = ponum.Substring(spilterIndex + 1);
            }
            payload.PoNums.Add(ponum);
            if (await svc.GetPurchaseOrderByPoNumAsync(payload, ponum))
                payload.PurchaseOrder = svc.ToDto();
            else
                payload.Messages = svc.Messages;

            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }
        [FunctionName(nameof(GetMultiPurchaseOrder))]
        [OpenApiOperation(operationId: "GetMultiPurchaseOrder", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNums", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "PoNums", Description = "PoNum Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadGetMultiple), Description = "The OK response")]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> GetMultiPurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "purchaseOrders")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new PurchaseOrderService(dbFactory);
            payload =await svc.GetPurchaseOrderByPoNumArrayAsync(payload);

            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }

        [FunctionName(nameof(DeletePurchaseOrder))]
        [OpenApiOperation(operationId: "DeletePurchaseOrder", tags: new[] { "PurchaseOrders" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "PoNum", Description = "PoNum = ProfileNumber-PoNum ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadDelete))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> DeletePurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "purchaseOrders/{poNum}")] HttpRequest req,
            string poNum)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var spilterIndex = poNum.IndexOf("-");
            var ponum = poNum;
            if (spilterIndex > 0 && ponum.StartsWith(payload.ProfileNum.ToString()))
            {
                ponum = ponum.Substring(spilterIndex + 1);
            }
            var svc = new PurchaseOrderService(dbFactory);
            if (await svc.DeleteByPoNumAsync(payload, ponum))
                payload.PurchaseOrder = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }
        [FunctionName(nameof(AddPurchaseOrder))]
        [OpenApiOperation(operationId: "AddPurchaseOrder", tags: new[] { "PurchaseOrders" })]
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
        [OpenApiOperation(operationId: "UpdatePurchaseOrder", tags: new[] { "PurchaseOrders" })]
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
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(PurchaseOrderList))]
        [OpenApiOperation(operationId: "PurchaseOrderList", tags: new[] { "PurchaseOrders" }, Summary = "Load productex list data")]
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
            payload = await srv.GetPurchaseOrderListAsync(payload);
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
    }
}


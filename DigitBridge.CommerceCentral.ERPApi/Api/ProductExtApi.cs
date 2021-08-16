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
    [ApiFilter(typeof(ProductExtApi))]
    public static class ProductExtApi
    {
        [FunctionName(nameof(GetProductExt))]
        [OpenApiOperation(operationId: "GetProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "sku", Description = "SKU", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadGetSingle), Description = "The OK response")]
        public static async Task<JsonNetResponse<InventoryPayload>> GetProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "productExts/{SKU}")] HttpRequest req,
            string SKU = null)
        {
            var payload = await req.GetParameters<InventoryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            var spilterIndex = SKU.IndexOf("-");
            var sku = SKU;
            if (spilterIndex > 0)
            {
                sku = SKU.Substring(spilterIndex + 1);
            }
            payload.Skus.Add(sku);
            if (await svc.GetInventoryBySkuAsync(payload, sku))
                payload.Inventory = svc.ToDto();
            else
                payload.Messages = svc.Messages;

            return new JsonNetResponse<InventoryPayload>(payload);
        }
        [FunctionName(nameof(GetMultiProductExt))]
        [OpenApiOperation(operationId: "GetMultiProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "skus", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "skus", Description = "SKU Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadGetMultiple), Description = "The OK response")]
        public static async Task<JsonNetResponse<InventoryPayload>> GetMultiProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "productExts")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            payload =await svc.GetInventoryBySkuArrayAsync(payload);

            return new JsonNetResponse<InventoryPayload>(payload);
        }

        [FunctionName(nameof(DeleteProductExt))]
        [OpenApiOperation(operationId: "DeleteProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "SKU", Description = "SKU = ProfileNumber-SKU ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadDelete))]
        public static async Task<JsonNetResponse<InventoryPayload>> DeleteProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "productExts/{SKU}")] HttpRequest req,
            string SKU)
        {
            var payload = await req.GetParameters<InventoryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var spilterIndex = SKU.IndexOf("-");
            var sku = SKU;
            if (spilterIndex > 0)
            {
                sku = SKU.Substring(spilterIndex + 1);
            }
            payload.Skus.Add(sku);
            var svc = new InventoryService(dbFactory);
            if (await svc.DeleteBySkuAsync(payload,sku))
                payload.Inventory = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<InventoryPayload>(payload);
        }
        [FunctionName(nameof(AddProductExt))]
        [OpenApiOperation(operationId: "AddProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryPayloadAdd), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadAdd))]
        public static async Task<JsonNetResponse<InventoryPayload>> AddProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "productExts")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.Inventory = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<InventoryPayload>(payload);
        }

        [FunctionName(nameof(UpdateProductExt))]
        [OpenApiOperation(operationId: "UpdateProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryPayloadUpdate), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadUpdate))]
        public static async Task<JsonNetResponse<InventoryPayload>> UpdateProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "productExts")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.Inventory = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<InventoryPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(ProductExList))]
        [OpenApiOperation(operationId: "ProductExList", tags: new[] { "ProductExts" }, Summary = "Load productex list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadFind))]
        public static async Task<JsonNetResponse<InventoryPayload>> ProductExList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "productExts/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryList(dataBaseFactory, new InventoryQuery());
            payload = await srv.GetIProductListAsync(payload);
            return new JsonNetResponse<InventoryPayload>(payload);
        }

        /// <summary>
        /// Add productext
        /// </summary>
        [FunctionName(nameof(Sample_ProductExt_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ProductExtAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of product ext")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadAdd))]
        public static async Task<JsonNetResponse<InventoryPayloadAdd>> Sample_ProductExt_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/productExts")] HttpRequest req)
        {
            return new JsonNetResponse<InventoryPayloadAdd>(InventoryPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find productext
        /// </summary>
        [FunctionName(nameof(Sample_ProductExt_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ProductExtFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of product find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventoryPayloadFind))]
        public static async Task<JsonNetResponse<InventoryPayloadFind>> Sample_ProductExt_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/productExts/find")] HttpRequest req)
        {
            return new JsonNetResponse<InventoryPayloadFind>(InventoryPayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportProductExt))]
        [OpenApiOperation(operationId: "ExportWarehouse", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "productExts/export")] HttpRequest req)
        {
            var payload = await req.GetParameters<InventoryPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-warehouse.csv";
            return downfile;
        }
    }
}


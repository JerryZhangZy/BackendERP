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
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "sku", Description = "SKU", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadGetSingle), Description = "The OK response")]
        public static async Task<JsonNetResponse<ProductExPayload>> GetProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "productExt/{SKU}")] HttpRequest req,
            string SKU = null)
        {
            var payload = await req.GetParameters<ProductExPayload>();
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
                payload.InventoryData = svc.ToDto();
            else
                payload.Messages = svc.Messages;

            return new JsonNetResponse<ProductExPayload>(payload);
        }
        [FunctionName(nameof(GetMultiProductExt))]
        [OpenApiOperation(operationId: "GetMultiProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "skus", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "skus", Description = "SKU Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadGetMultiple), Example = typeof(ProductExPayloadGetMultiple), Description = "The OK response")]
        public static async Task<JsonNetResponse<ProductExPayload>> GetMultiProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "productExt")] HttpRequest req)
        {
            var payload = await req.GetParameters<ProductExPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            payload =await svc.GetInventoryBySkuArrayAsync(payload);

            return new JsonNetResponse<ProductExPayload>(payload);
        }

        [FunctionName(nameof(DeleteProductExt))]
        [OpenApiOperation(operationId: "DeleteProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "SKU", Description = "SKU = ProfileNumber-SKU ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadDelete))]
        public static async Task<JsonNetResponse<ProductExPayload>> DeleteProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "productExt/{SKU}")] HttpRequest req,
            string SKU)
        {
            var payload = await req.GetParameters<ProductExPayload>();
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
                payload.InventoryData = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<ProductExPayload>(payload);
        }
        [FunctionName(nameof(AddProductExt))]
        [OpenApiOperation(operationId: "AddProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductExPayloadAdd), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadAdd))]
        public static async Task<JsonNetResponse<ProductExPayload>> AddProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "productExt")] HttpRequest req)
        {
            var payload = await req.GetParameters<ProductExPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.InventoryData = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<ProductExPayload>(payload);
        }

        [FunctionName(nameof(UpdateProductExt))]
        [OpenApiOperation(operationId: "UpdateProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductExPayloadUpdate), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadUpdate))]
        public static async Task<JsonNetResponse<ProductExPayload>> UpdateProductExt(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "productExt")] HttpRequest req)
        {
            var payload = await req.GetParameters<ProductExPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new InventoryService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.InventoryData = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<ProductExPayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(ProductExList))]
        [OpenApiOperation(operationId: "ProductExList", tags: new[] { "ProductExts" }, Summary = "Load productex list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ProductExPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ProductExPayloadFind))]
        public static async Task<JsonNetResponse<ProductExPayload>> ProductExList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "productExt/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<ProductExPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryList(dataBaseFactory, new InventoryQuery());
            payload = await srv.GetIProductListAsync(payload);
            return new JsonNetResponse<ProductExPayload>(payload);
        }
    }
}


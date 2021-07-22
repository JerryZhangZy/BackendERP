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
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "SKU", Description = "SKU = ProfileNumber-SKU ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<InventoryDataDto>), Example = typeof(InventoryDataDto), Description = "The OK response")]
        public static async Task<IActionResult> GetProductExt(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "productExt/{SKU?}")] HttpRequest req,
            string SKU,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var spilterIndex = SKU.IndexOf("-");
            var sku = SKU;
            if (spilterIndex > 0)
            {
                profileNum = SKU.Substring(0, spilterIndex).ToInt();
                sku = SKU.Substring(spilterIndex + 1);
            }
            var svc = new InventoryService(dbFactory);
            var data = svc.GetInventoryBySku(profileNum, sku);
            return new Response<InventoryDataDto>(data);
        }

        [FunctionName(nameof(DeleteProductExt))]
        [OpenApiOperation(operationId: "DeleteProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "SKU", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "SKU", Description = "SKU = ProfileNumber-SKU ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> DeleteProductExt(
            [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "productExt/{SKU?}")] HttpRequest req,
            string SKU,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var spilterIndex = SKU.IndexOf("-");
            var sku = SKU;
            if (spilterIndex > 0)
            {
                profileNum = SKU.Substring(0, spilterIndex).ToInt();
                sku = SKU.Substring(spilterIndex + 1);
            }
            var svc = new InventoryService(dbFactory);
            var result = svc.DeleteBySku(profileNum, sku);
            return new Response<string>("Delete productext result", result);
        }
        [FunctionName(nameof(AddProductExt))]
        [OpenApiOperation(operationId: "AddProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryDataDto), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> AddProductExt(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "productExt")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryService(dbFactory);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var dto = JsonConvert.DeserializeObject<InventoryDataDto>(requestBody);

            var addresult = svc.Add(dto);
            return new Response<string>("Delete productext result", addresult);
        }

        [FunctionName(nameof(UpdateProductExt))]
        [OpenApiOperation(operationId: "UpdateProductExt", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventoryDataDto), Description = "InventoryDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> UpdateProductExt(
            [HttpTrigger(AuthorizationLevel.Anonymous, "PATCH", Route = "productExt")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var masterAccountNum = req.GetHeaderData<int>("masterAccountNum") ?? 0;
            var profileNum = req.GetHeaderData<int>("profileNum") ?? 0;
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var svc = new InventoryService(dbFactory);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var dto = JsonConvert.DeserializeObject<InventoryDataDto>(requestBody);

            var updateresult = svc.Update(dto);
            return new Response<string>("Update productext result", updateresult);
        }
    }
}


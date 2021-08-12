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
    [ApiFilter(typeof(WarehouseApi))]
    public static class WarehouseApi
    {
        [FunctionName(nameof(GetWarehouse))]
        [OpenApiOperation(operationId: "GetWarehouse", tags: new[] { "Warehouses" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "WarehouseCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "WarehouseCode", Description = "WarehouseCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadGetSingle))]
        public static async Task<JsonNetResponse<WarehousePayload>> GetWarehouse(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "warehouses/{WarehouseCode}")] HttpRequest req,
            string WarehouseCode = null)
        {
            var payload = await req.GetParameters<WarehousePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseService(dbFactory);

            var warehouseCode = WarehouseCode;
            if (!string.IsNullOrEmpty(WarehouseCode))
            {
                var spilterIndex = WarehouseCode.IndexOf("-");
                if (spilterIndex > 0)
                {
                    warehouseCode = WarehouseCode.Substring(spilterIndex + 1);
                }
                payload.WarehouseCodes.Add(warehouseCode);
            }
            if (await svc.GetWarehouseByWarehouseCodeAsync(payload, warehouseCode))
                payload.Warehouse = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<WarehousePayload>(payload);

        }
        [FunctionName(nameof(GetMultiWarehouses))]
        [OpenApiOperation(operationId: "GetMultiWarehouses", tags: new[] { "Warehouses" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "WarehouseCodes", In = ParameterLocation.Query, Required = false, Type = typeof(List<string>), Summary = "WarehouseCodes", Description = "WarehouseCode Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadGetMultiple))]
        public static async Task<JsonNetResponse<WarehousePayload>> GetMultiWarehouses(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "warehouses")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehousePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new WarehouseService(dbFactory);
            payload =await svc.GetWarehouseByWarehouseCodeArrayAsync(payload);
            return new JsonNetResponse<WarehousePayload>(payload);

        }

        [FunctionName(nameof(DeleteWarehouse))]
        [OpenApiOperation(operationId: "DeleteWarehouse", tags: new[] { "Warehouses" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "WarehouseCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "WarehouseCode", Description = "WarehouseCode", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadDelete),Description = "The OK response")]
        public static async Task<JsonNetResponse<WarehousePayload>> DeleteWarehouse(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "warehouses/{WarehouseCode}")] HttpRequest req,
            string WarehouseCode)
        {
            var payload = await req.GetParameters<WarehousePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseService(dbFactory);
            var spilterIndex = WarehouseCode.IndexOf("-");
            var warehouseCode = WarehouseCode;
            if (spilterIndex > 0)
            {
                warehouseCode = WarehouseCode.Substring(spilterIndex + 1);
            }
            payload.WarehouseCodes.Add(warehouseCode);
            if (await svc.DeleteByWarehouseCodeAsync(payload,warehouseCode))
                payload.Warehouse = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehousePayload>(payload);
        }

        [FunctionName(nameof(AddWarehouse))]
        [OpenApiOperation(operationId: "AddWarehouse", tags: new[] { "Warehouses" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehousePayloadAdd), Description = "WarehouseDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadAdd))]
        public static async Task<JsonNetResponse<WarehousePayload>> AddWarehouse(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "warehouses")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehousePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseService(dbFactory);
            if (await svc.AddAsync(payload))
                payload.Warehouse = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehousePayload>(payload);
        }

        [FunctionName(nameof(UpdateWarehouse))]
        [OpenApiOperation(operationId: "UpdateWarehouse", tags: new[] { "Warehouses" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehousePayloadUpdate), Description = "WarehouseDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadUpdate))]
        public static async Task<JsonNetResponse<WarehousePayload>> UpdateWarehouse(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "warehouses")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehousePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.Warehouse = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehousePayload>(payload);
        }

        ///// <summary>
        ///// Load warehouse list
        ///// </summary>
        //[FunctionName(nameof(WarehousesList))]
        //[OpenApiOperation(operationId: "WarehousesList", tags: new[] { "Warehouses" }, Summary = "Load warehouse list data")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehousePayloadFind), Description = "Request Body in json format")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadFind))]
        //public static async Task<JsonNetResponse<WarehousePayload>> WarehousesList(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "warehouses/find")] HttpRequest req)
        //{
        //    var payload = await req.GetParameters<WarehousePayload>(true);
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new WarehouseList(dataBaseFactory, new WarehouseQuery());
        //    payload = await srv.GetWarehouseListAsync(payload);
        //    return new JsonNetResponse<WarehousePayload>(payload);
        //}

        /// <summary>
        /// Add shipment
        /// </summary>
        [FunctionName(nameof(Sample_Warehouse_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "WarehouseAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadAdd))]
        public static async Task<JsonNetResponse<WarehousePayloadAdd>> Sample_Warehouse_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/warehouses")] HttpRequest req)
        {
            return new JsonNetResponse<WarehousePayloadAdd>(WarehousePayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find shipment
        /// </summary>
        [FunctionName(nameof(Sample_Warehouse_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "WarehouseFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of shipment find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehousePayloadFind))]
        public static async Task<JsonNetResponse<WarehousePayloadFind>> Sample_Warehouse_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/warehouses/find")] HttpRequest req)
        {
            return new JsonNetResponse<WarehousePayloadFind>(WarehousePayloadFind.GetSampleData());
        }
    }
}


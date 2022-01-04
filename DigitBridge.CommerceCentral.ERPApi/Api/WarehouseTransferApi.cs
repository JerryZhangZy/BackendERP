using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Utility.Enums;
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
    [ApiFilter(typeof(WarehouseTransferApi))]
    public static class WarehouseTransferApi
    {

        [FunctionName(nameof(ExportWarehouseTransfer))]
        [OpenApiOperation(operationId: "ExportWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "WarehouseTransferUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "WarehouseTransferUuid", Description = "WarehouseTransferUuid", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadGetSingle), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportWarehouseTransfer(
   [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "warehouseTransfers/export/{WarehouseTransferUuid}")] HttpRequest req, string WarehouseTransferUuid = null)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var iOManager = new WarehouseTransferIOManager(dbFactory);
            var service = new WarehouseTransferService(dbFactory);

            if (!await service.GetWarehouseTransferByUuidAsync(payload, WarehouseTransferUuid))
            {
                service.AddError("Get WarehouseTransfer datas error");
                return null;
            }
            var dtos = new List<WarehouseTransferDataDto>();
            dtos.Add(service.ToDto());
            var fileBytes = await iOManager.ExportAllColumnsAsync(dtos);
            var downfile = new FileContentResult(fileBytes, "text/csv");
            downfile.FileDownloadName = "export-warehouseTransfers.csv";
            return downfile;
        }



        [FunctionName(nameof(ImportWarehouseTransfer))]
        [OpenApiOperation(operationId: "ImportWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayload))]
        public static async Task<WarehouseTransferPayload> ImportWarehouseTransfer(
     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "warehouseTransfers/import")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            
            var iOManager = new WarehouseTransferIOManager(dbFactory);
            var dtos = await iOManager.ImportWarehouseTransferAsync(payload.MasterAccountNum,payload.ProfileNum,files[0].OpenReadStream());
            payload.WarehouseTransfer = dtos;
            payload.Success = dtos!=null;
            payload.Messages = iOManager.Messages;
            return payload;
        }




        [FunctionName(nameof(GetWarehouseTransfer))]
        [OpenApiOperation(operationId: "GetWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "BatchNumber", Description = "WarehouseTransfer Number,ProfileNum-BatchNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadGetSingle))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> GetWarehouseTransfer(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "warehouseTransfers/{BatchNumber}")] HttpRequest req,
            string BatchNumber = null)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);

            var spilterIndex = BatchNumber.IndexOf("-");
            var batchNumber = BatchNumber;
            if (spilterIndex > 0 && BatchNumber.StartsWith(payload.ProfileNum.ToString()))
            {
                batchNumber = BatchNumber.Substring(spilterIndex + 1);
            }
            if (await svc.GetWarehouseTransferByBatchNumberAsync(payload, batchNumber))
                payload.WarehouseTransfer = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<WarehouseTransferPayload>(payload);

        }

        [FunctionName(nameof(GetMultiWarehouseTransfers))]
        [OpenApiOperation(operationId: "GetMultiWarehouseTransfers", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(List<string>), Summary = "BatchNumbers", Description = "BatchNumber Array", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadGetMultiple))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> GetMultiWarehouseTransfers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "warehouseTransfers")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var svc = new WarehouseTransferService(dbFactory);
            payload =await svc.GetWarehouseTransfersByCodeArrayAsync(payload);
            return new JsonNetResponse<WarehouseTransferPayload>(payload);

        }

        [FunctionName(nameof(AddWarehouseTransfer))]
        [OpenApiOperation(operationId: "AddWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadAdd), Description = "WarehouseTransferDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadAdd))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> AddWarehouseTransfer(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "warehouseTransfers")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);
            payload.WarehouseTransfer.WarehouseTransferHeader.WarehouseTransferStatus = (int)TransferStatus.InTransit;
            if (await svc.AddAsync(payload))
                payload.WarehouseTransfer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }


        [FunctionName(nameof(AddAndCloseWarehouseTransfer))]
        [OpenApiOperation(operationId: "AddAndCloseWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadAdd), Description = "WarehouseTransferDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadAdd))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> AddAndCloseWarehouseTransfer(
    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "addAndCloseWarehouseTransfer")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);
            payload.WarehouseTransfer.WarehouseTransferHeader.WarehouseTransferStatus = (int)TransferStatus.ArriveImmediately;
            if (await svc.AddAsync(payload))
                payload.WarehouseTransfer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }


        [FunctionName(nameof(UpdateWarehouseTransfer))]
        [OpenApiOperation(operationId: "UpdateWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadUpdate), Description = "WarehouseTransferDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadUpdate))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> UpdateWarehouseTransfer(
            [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "warehouseTransfers")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);
            if (await svc.UpdateAsync(payload))
                payload.WarehouseTransfer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }

        [FunctionName(nameof(DeleteWarehouseTransfer))]
        [OpenApiOperation(operationId: "DeleteWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "BatchNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "BatchNumber", Description = "BatchNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> DeleteWarehouseTransfer(
            [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "warehouseTransfers/{BatchNumber}")] HttpRequest req,
            string BatchNumber)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);
            var spilterIndex = BatchNumber.IndexOf("-");
            var batchNumber = BatchNumber;
            if (spilterIndex > 0 && batchNumber.StartsWith(payload.ProfileNum.ToString()))
            {
                batchNumber = BatchNumber.Substring(spilterIndex + 1);
            }
            payload.BatchNumbers.Add(batchNumber);
            if (await svc.DeleteByBatchNumberAsync(payload, batchNumber))
                payload.WarehouseTransfer = null;
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }


        [FunctionName(nameof(CloseWarehouseTransfer))]
        [OpenApiOperation(operationId: "CloseWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "warehouseTransferUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "warehouseTransferUuid", Description = "warehouseTransferUuid", Visibility = OpenApiVisibilityType.Advanced)]
       
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadUpdate))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> CloseWarehouseTransfer(
    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "closeWarehouseTransfer/{warehouseTransferUuid}")] HttpRequest req,
             string warehouseTransferUuid)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new WarehouseTransferService(dbFactory);
            if (await svc.CloseAsync(payload.MasterAccountNum,payload.ProfileNum,warehouseTransferUuid))
                payload.WarehouseTransfer = svc.ToDto();
            else
            {
                payload.Messages = svc.Messages;
                payload.Success = false;
            }
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }





        /// <summary>
        /// Load warehouseTransfer list
        /// </summary>
        [FunctionName(nameof(WarehouseTransfersList))]
        [OpenApiOperation(operationId: "WarehouseTransfersList", tags: new[] { "WarehouseTransfers" }, Summary = "Load warehouseTransfer list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> WarehouseTransfersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "warehouseTransfers/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new WarehouseTransferList(dataBaseFactory, new WarehouseTransferQuery());
            payload = await srv.GetWarehouseTransferListAsync(payload);
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }

        /// <summary>
        /// Load warehouseTransfer list
        /// </summary>
        [FunctionName(nameof(WarehouseTransferItemsList))]
        [OpenApiOperation(operationId: "WarehouseTransferItemsList", tags: new[] { "WarehouseTransfers" }, Summary = "Load warehouseTransfer list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferItemPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferItemPayloadFind))]
        public static async Task<JsonNetResponse<WarehouseTransferItemPayload>> WarehouseTransferItemsList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "warehouseTransfers/items/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferItemPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new WarehouseTransferItemList(dataBaseFactory, new WarehouseTransferItemQuery());
            payload = await srv.GetWarehouseTransferItemListAsync(payload);
            return new JsonNetResponse<WarehouseTransferItemPayload>(payload);
        }

        /// <summary>
        /// Add warehouseTransfer
        /// </summary>
        [FunctionName(nameof(Sample_WarehouseTransfer_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "WarehouseTransferAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of warehouseTransfer")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadAdd))]
        public static async Task<JsonNetResponse<WarehouseTransferPayloadAdd>> Sample_WarehouseTransfer_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/warehouseTransfers")] HttpRequest req)
        {
            return new JsonNetResponse<WarehouseTransferPayloadAdd>(WarehouseTransferPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find warehouseTransfer
        /// </summary>
        [FunctionName(nameof(Sample_WarehouseTransfer_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "WarehouseTransferFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of warehouseTransfer find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind))]
        public static async Task<JsonNetResponse<WarehouseTransferPayloadFind>> Sample_WarehouseTransfer_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/warehouseTransfers/find")] HttpRequest req)
        {
            return new JsonNetResponse<WarehouseTransferPayloadFind>(WarehouseTransferPayloadFind.GetSampleData());
        }




        [FunctionName(nameof(WarehouseTransferListSummary))]
        [OpenApiOperation(operationId: "WarehouseTransferListSummary", tags: new[] { "WarehouseTransfers" }, Summary = "Load WarehouseTransfers list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind))]
        public static async Task<JsonNetResponse<WarehouseTransferPayload>> WarehouseTransferListSummary(
     [HttpTrigger(AuthorizationLevel.Function, "post", Route = "warehouseTransfers/find/summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<WarehouseTransferPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new WarehouseTransferList(dataBaseFactory, new WarehouseTransferQuery());
            await srv.GetWarehouseTransferListSummaryAsync(payload);
            return new JsonNetResponse<WarehouseTransferPayload>(payload);
        }


        //[FunctionName(nameof(ExportWarehouseTransfer))]
        //[OpenApiOperation(operationId: "ExportWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WarehouseTransferPayloadFind), Description = "Request Body in json format")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        //public static async Task<FileContentResult> ExportWarehouseTransfer(
        //    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "warehouseTransfers/export")] HttpRequest req)
        //{
        //    var payload = await req.GetParameters<WarehouseTransferPayload>(true);
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var svc = new WarehouseTransferManager(dbFactory);

        //    var exportData = await svc.ExportAsync(payload);
        //    var downfile = new FileContentResult(exportData, "text/csv");
        //    downfile.FileDownloadName = "export-warehouseTransfer.csv";
        //    return downfile;
        //}

        //[FunctionName(nameof(ImportWarehouseTransfer))]
        //[OpenApiOperation(operationId: "ImportWarehouseTransfer", tags: new[] { "WarehouseTransfers" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "InventoryUpdateType", In = ParameterLocation.Path, Required = true, Type = typeof(InventoryUpdateType), Summary = "InventoryUpdateType", Description = "InventoryUpdateType", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WarehouseTransferPayload))]
        //public static async Task<WarehouseTransferPayload> ImportWarehouseTransfer(
        //    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "warehouseTransfers/import/{InventoryUpdateType}")] HttpRequest req, int InventoryUpdateType)
        //{ 
        //    var payload = await req.GetParameters<WarehouseTransferPayload>();
        //    payload.InventoryUpdateType = (InventoryUpdateType)InventoryUpdateType;
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var files = req.Form.Files;
        //    var svc = new WarehouseTransferManager(dbFactory);

        //    await svc.ImportAsync(payload, files);
        //    payload.Success = true;
        //    payload.Messages = svc.Messages;
        //    return payload;
        //}
    }
}


using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.ERPMdl.po;
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

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process po receive 
    /// </summary> 
    [ApiFilter(typeof(PoReceiveApi))]
    public static class PoReceiveApi
    {
        ///// <summary>
        ///// Get po Receives by po num
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="poNum"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(GetPoReceives))]
        //[OpenApiOperation(operationId: "GetPoReceives", tags: new[] { "Po Receives" }, Summary = "Get po Receives by po number")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "poNum", Description = "Po number. ", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        //public static async Task<JsonNetResponse<PoReceivePayload>> GetPoReceives(
        //    [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "poReceives/{poNum}")] HttpRequest req,
        //    string poNum)
        //{
        //    var payload = await req.GetParameters<PoReceivePayload>();
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new PoReceiveService(dataBaseFactory);
        //    await srv.GetPaymentWithPoHeaderAsync(payload, poNum);
        //    return new JsonNetResponse<PoReceivePayload>(payload);
        //}

        /// <summary>
        /// Get po receive by po num
        /// </summary>
        /// <param name="req"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetPoReceive))]
        [OpenApiOperation(operationId: "GetPoReceive", tags: new[] { "Po Receives" }, Summary = "Get po receive by po number and trannum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
      
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "Transaction Num. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> GetPoReceive(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "poReceives/{transNum}")] HttpRequest req,
             int transNum)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, transNum);
            if (payload.Success)
                payload.PoTransaction = srv.ToDto();
            payload.Messages = srv.Messages;
            return new JsonNetResponse<PoReceivePayload>(payload);
        }


        /// <summary>
        /// Delete po receive
        /// </summary>
        /// <param name="req"></param>
      
        /// <param name="transNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeletePoReceives))]
        [OpenApiOperation(operationId: "DeletePoReceives", tags: new[] { "Po Receives" }, Summary = "Delete one po receive ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "transUuid", Description = "Transaction Uuid. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadDelete))]
        public static async Task<JsonNetResponse<PoReceivePayload>> DeletePoReceives(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "poReceives/{transUuid}")]
            HttpRequest req,
             string transUuid)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, transUuid);
            payload.Messages = srv.Messages;
 

            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        ///  Update po receive  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdatePoReceives))]
        [OpenApiOperation(operationId: "UpdatePoReceives", tags: new[] { "Po Receives" }, Summary = "Update one po receive ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadUpdate))]
        public static async Task<JsonNetResponse<PoReceivePayload>> UpdatePoReceives(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "poReceives")] HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Add po receive 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddPoReceives))]
        [OpenApiOperation(operationId: "AddPoReceives", tags: new[] { "Po Receives" }, Summary = "Add new PO Stock Receive ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd))]
        public static async Task<JsonNetResponse<PoReceivePayload>> AddPoReceives(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives")] HttpRequest req)
        {

            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.AddListAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<PoReceivePayload>(payload);

            //var payload = await req.GetParameters<PoReceivePayload>(true);
            //var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            //var srv = new PoReceiveService(dataBaseFactory);
            ////await srv.AddAsync(payload);
            //if (await srv.AddAsync(payload))
            //{
            //    payload.Success = true;
            //    payload.PoTransaction = srv.ToDto();
            //}
            //payload.Messages = srv.Messages;

            //return new JsonNetResponse<PoReceivePayload>(payload);
        }




        /// <summary>
        /// Load PoReceives list
        /// </summary>
        [FunctionName(nameof(PoReceivesList))]
        [OpenApiOperation(operationId: "PoReceivesList", tags: new[] { "Po Receives" }, Summary = "Load poReceives list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadFind))]
        public static async Task<JsonNetResponse<PoReceivePayload>> PoReceivesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveList(dataBaseFactory, new PoReceiveQuery());
            await srv.GetPoReceiveListAsync(payload);
            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Sample_PoReceives_Post
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(Sample_PoReceives_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PoReceivesSample", tags: new[] { "Sample" }, Summary = "Get new sample of PoReceive")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd))]
        public static async Task<JsonNetResponse<PoReceivePayloadAdd>> Sample_PoReceives_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/poReceives")] HttpRequest req)
        {
            return new JsonNetResponse<PoReceivePayloadAdd>(PoReceivePayloadAdd.GetSampleData());
        }
        /// <summary>
        /// Sample_PoReceive_Find
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(Sample_PoReceive_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "PoReceiveFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of po receive find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadFind))]
        public static async Task<JsonNetResponse<PoReceivePayloadFind>> Sample_PoReceive_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/poReceives/find")] HttpRequest req)
        {
            return new JsonNetResponse<PoReceivePayloadFind>(PoReceivePayloadFind.GetSampleData());
        }





        /// <summary>
        /// Get NewPoReceive return by poNum
        /// </summary>
        /// <param name="req"></param>
        /// <param name="poNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(NewPoReceive))]
        [OpenApiOperation(operationId: "NewPoReceive", tags: new[] { "Po Receives" }, Summary = "Get po new receive by poNum")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "poNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "PoNum", Description = "PoNum ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> NewPoReceive(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/newReceive/po/{poNum}")] HttpRequest req,
            string poNum)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.NewReceiveAsync(payload, poNum);
            payload.Messages = srv.Messages;
            payload.PoTransactions = new List<PoTransactionDataDto>();
            if (payload.Success)
                payload.PoTransactions.Add(srv.ToDto());
 
            return new JsonNetResponse<PoReceivePayload>(payload);
        }

        /// <summary>
        /// Get NewPoReceive return by vendorCode
        /// </summary>
        /// <param name="req"></param>
        /// <param name="vendorCode">vendorCode</param>
        /// <returns></returns>
        [FunctionName(nameof(NewReceiveForVendor))]
        [OpenApiOperation(operationId: "NewReceiveForVendor", tags: new[] { "Po Receives" }, Summary = "Get po new receive For Vendor")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "vendorCode", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "vendorCode", Description = "P/O receive for Vendor", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> NewReceiveForVendor(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/newReceive/vendor/{vendorCode}")] HttpRequest req,
           string vendorCode)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.NewReceiveForVendorAsync(payload,vendorCode);
            payload.Messages = srv.Messages;
  
            return new JsonNetResponse<PoReceivePayload>(payload);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="transNum">transNum</param>
        /// <returns></returns>
        [FunctionName(nameof(ClosePoReceive))]
        [OpenApiOperation(operationId: "ClosePoReceive", tags: new[] { "Po Receives" }, Summary = "close a  receive ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "transNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "transNum", Description = "P/O receive transNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadGetSingle))]
        public static async Task<JsonNetResponse<PoReceivePayload>> ClosePoReceive(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/close/{transNum}")] HttpRequest req,
           int transNum)
        {
            var payload = await req.GetParameters<PoReceivePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveService(dataBaseFactory);
            payload.Success = await srv.ClosePoReceiveAsync(payload, transNum);
            payload.Messages = srv.Messages;

            return new JsonNetResponse<PoReceivePayload>(payload);
        }


 

        [FunctionName(nameof(PoReceiveListSummary))]
        [OpenApiOperation(operationId: "PoReceiveListSummary", tags: new[] { "Po Receives" }, Summary = "Load Po Receives list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadFind))]
        public static async Task<JsonNetResponse<PoReceivePayload>> PoReceiveListSummary(
     [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives/find/summary")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<PoReceivePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new PoReceiveList(dataBaseFactory, new PoReceiveQuery());
            await srv.GetPoReceiveListSummaryAsync(payload);
            return new JsonNetResponse<PoReceivePayload>(payload);
        }





        // [FunctionName(nameof(ExportPayment))]
        // [OpenApiOperation(operationId: "ExportPayment", tags: new[] { "Po Receives" })]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayload), Description = "Request Body in json format")]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        // public static async Task<FileContentResult> ExportPayment(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/export")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var svc = new PoReceiveManager(dbFactory);
        //
        //     var exportData = await svc.ExportAsync(payload);
        //     var downfile = new FileContentResult(exportData, "text/csv");
        //     downfile.FileDownloadName = "export-Payments.csv";
        //     return downfile;
        // }
        //
        // [FunctionName(nameof(ImportPayment))]
        // [OpenApiOperation(operationId: "ImportPayment", tags: new[] { "Po Receives" })]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayload))]
        // public static async Task<PoReceivePayload> ImportPayment(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/import")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var files = req.Form.Files;
        //     var svc = new PoReceiveManager(dbFactory);
        //
        //     await svc.ImportAsync(payload, files); 
        //     payload.Success = true;
        //     payload.Messages = svc.Messages;
        //     return payload;
        // }

        // /// <summary>
        // /// Get po receive summary by search criteria
        // /// </summary>
        // /// <param name="req"></param> 
        // [FunctionName(nameof(PoReceiveSummary))]
        // [OpenApiOperation(operationId: "PoReceiveSummary", tags: new[] { "Po Receives" }, Summary = "Get po receive summary")]
        // [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadFind), Description = "Result is List<PoReceiveDataDto>")]
        // public static async Task<JsonNetResponse<PoReceivePayload>> PoReceiveSummary(
        //     [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "poReceives/Summary")] HttpRequest req)
        // {
        //     var payload = await req.GetParameters<PoReceivePayload>();
        //     var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //     var srv = new PoReceiveSummaryInquiry(dataBaseFactory, new PoReceiveSummaryQuery());
        //     await srv.PoReceiveSummaryAsync(payload);
        //     return new JsonNetResponse<PoReceivePayload>(payload);
        // }


        ///// <summary>
        ///// Add po receive 
        ///// </summary>
        ///// <param name="req"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(AddApPoReceives))]
        //[OpenApiOperation(operationId: "AddApPoReceives", tags: new[] { "Po Receives" }, Summary = "Add PO A/P Receive")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd), Description = "Request Body in json format")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PoReceivePayloadAdd))]
        //public static async Task<JsonNetResponse<PoReceivePayload>> AddApPoReceives(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "poReceives/ap")] HttpRequest req)
        //{
        //    var payload = await req.GetParameters<PoReceivePayload>(true);
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new PoReceiveService(dataBaseFactory);
        //    srv.SetFirstAPReceiveStatus(true);
        //    await srv.AddAsync(payload);
        //    // payload.Success = await srv.AddAsync(payload);
        //    // payload.Messages = srv.Messages;
        //    // payload.PoTransaction = srv.ToDto();

        //    //Directly return without waiting this result. 
        //    //if (payload.Success)
        //    // srv.AddQboPaymentEventAsync(payload.MasterAccountNum, payload.ProfileNum, payload.ApplyInvoices);

        //    return new JsonNetResponse<PoReceivePayload>(payload);
        //}
    }
}


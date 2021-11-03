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
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process MiscInvoice
    /// </summary> 
    [ApiFilter(typeof(MiscInvoiceApi))]
    public static class MiscInvoiceApi
    {
        /// <summary>
        /// Get one MiscInvoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="MiscInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetMiscInvoice))]
        [OpenApiOperation(operationId: "GetMiscInvoice", tags: new[] { "MiscInvoices" }, Summary = "Get one MiscInvoice by MiscInvoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "MiscInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "MiscInvoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> GetMiscInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "MiscInvoices/{MiscInvoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
            ILogger log,
            string MiscInvoiceNumber)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, MiscInvoiceNumber);
            if (payload.Success)
            {
                payload.MiscInvoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

        /// <summary>
        /// Get list by MiscInvoice numbers.
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [FunctionName(nameof(GetListByMiscInvoiceNumbers))]
        [OpenApiOperation(operationId: "GetListByMiscInvoiceNumbers", tags: new[] { "MiscInvoices" }, Summary = "Get MiscInvoice list by MiscInvoice numbers")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "MiscInvoiceNumbers", In = ParameterLocation.Query, Required = true, Type = typeof(IList<string>), Summary = "MiscInvoiceNumbers", Description = "Array of MiscInvoiceNumber.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadGetMultiple), Description = "mulit MiscInvoice.")]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> GetListByMiscInvoiceNumbers(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "MiscInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceService(dataBaseFactory);
            await srv.GetListByMiscInvoiceNumbersAsync(payload);
            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

        /// <summary>
        /// Delete MiscInvoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="MiscInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteByMiscInvoiceNumber))]
        [OpenApiOperation(operationId: "DeleteByMiscInvoiceNumber", tags: new[] { "MiscInvoices" }, Summary = "Delete one MiscInvoice by MiscInvoice number.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "MiscInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "MiscInvoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadDelete))]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> DeleteByMiscInvoiceNumber(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "MiscInvoices/{MiscInvoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
           string MiscInvoiceNumber)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceService(dataBaseFactory);
            payload.Success = await srv.DeleteByNumberAsync(payload, MiscInvoiceNumber);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

        /// <summary>
        ///  Update MiscInvoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateMiscInvoice))]
        [OpenApiOperation(operationId: "UpdateMiscInvoice", tags: new[] { "MiscInvoices" }, Summary = "Update one MiscInvoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MiscInvoicePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadUpdate))]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> UpdateMiscInvoice(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "MiscInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            //if (payload.Success)
            //    srv.AddQboMiscInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }
        /// <summary>
        /// Add MiscInvoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddMiscInvoice))]
        [OpenApiOperation(operationId: "AddMiscInvoice", tags: new[] { "MiscInvoices" }, Summary = "Add one MiscInvoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MiscInvoicePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadAdd))]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> AddMiscInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MiscInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.MiscInvoice = srv.ToDto();

            //Directly return without waiting this result. 
            //if (payload.Success)
            //    srv.AddQboMiscInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

        /// <summary>
        /// Load MiscInvoices list
        /// </summary>
        [FunctionName(nameof(MiscInvoicesList))]
        [OpenApiOperation(operationId: "MiscInvoicesList", tags: new[] { "MiscInvoices" }, Summary = "Load MiscInvoices list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MiscInvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadFind))]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> MiscInvoicesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MiscInvoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new MiscInvoiceList(dataBaseFactory, new MiscInvoiceQuery());
            await srv.GetMiscInvoiceListAsync(payload);
            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

        /// <summary>
        /// Add MiscInvoice
        /// </summary>
        [FunctionName(nameof(Sample_MiscInvocies_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvociesSample", tags: new[] { "Sample" }, Summary = "Get new sample of MiscInvoice")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadAdd))]
        public static async Task<JsonNetResponse<MiscInvoicePayloadAdd>> Sample_MiscInvocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/MiscInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<MiscInvoicePayloadAdd>(MiscInvoicePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_MiscInvoice_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "MiscInvoiceFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of MiscInvoice find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadFind))]
        public static async Task<JsonNetResponse<MiscInvoicePayloadFind>> Sample_MiscInvoice_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/MiscInvoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<MiscInvoicePayloadFind>(MiscInvoicePayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ExportMiscInvoices))]
        [OpenApiOperation(operationId: "ExportMiscInvoices", tags: new[] { "MiscInvoices" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(MiscInvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/csv", bodyType: typeof(File))]
        public static async Task<FileContentResult> ExportMiscInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "MiscInvoices/export")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new MiscInvoiceManager(dbFactory);

            var exportData = await svc.ExportAsync(payload);
            var downfile = new FileContentResult(exportData, "text/csv");
            downfile.FileDownloadName = "export-MiscInvoices.csv";
            return downfile;
        }

        [FunctionName(nameof(ImportMiscInvoices))]
        [OpenApiOperation(operationId: "ImportMiscInvoices", tags: new[] { "MiscInvoices" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(IFormFile), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayload))]
        public static async Task<MiscInvoicePayload> ImportMiscInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "MiscInvoices/import")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var files = req.Form.Files;
            var svc = new MiscInvoiceManager(dbFactory);

            await svc.ImportAsync(payload, files);
            payload.Success = true;
            payload.Messages = svc.Messages;
            return payload;
        }

        /// <summary>
        /// Get sales order summary by search criteria
        /// </summary>
        /// <param name="req"></param> 
        [FunctionName(nameof(MiscInvoiceSummary))]
        [OpenApiOperation(operationId: "MiscInvoiceSummary", tags: new[] { "MiscInvoices" }, Summary = "Get MiscInvoice summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MiscInvoicePayloadFind), Description = "Result is List<MiscInvoiceDataDto>")]
        public static async Task<JsonNetResponse<MiscInvoicePayload>> MiscInvoiceSummary(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "MiscInvoices/Summary")] HttpRequest req)
        {
            var payload = await req.GetParameters<MiscInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            //var srv = new MiscInvoiceSummaryInquiry(dataBaseFactory, new MiscInvoiceSummaryQuery());
            //await srv.MiscInvoiceSummaryAsync(payload);
            return new JsonNetResponse<MiscInvoicePayload>(payload);
        }

    }
}


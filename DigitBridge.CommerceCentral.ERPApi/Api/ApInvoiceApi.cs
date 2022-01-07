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
    /// Process apInvoice
    /// </summary> 
    [ApiFilter(typeof(ApInvoiceApi))]
   public static class ApInvoiceApi
    {
        [FunctionName(nameof(ExistInvoiceNumber))]
        [OpenApiOperation(operationId: "ExistInvoiceNumber", tags: new[] { "ApInvoices" }, Summary = "exam an invoice number whether been used")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNum", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool))]
        public static async Task<bool> ExistInvoiceNumber(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "apInvoices/existInvoiceNum/{invoiceNum}")] HttpRequest req,
            string invoiceNum)
        {
            int masterAccountNum = req.Headers["masterAccountNum"].ToInt();
            int profileNum = req.Headers["profileNum"].ToInt();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var srv = new ApInvoiceService(dataBaseFactory);

            return await srv.ExistApInvoiceNumber(invoiceNum, masterAccountNum, profileNum);
        }

        /// <summary>
        /// Get one invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetApInvoice))]
        [OpenApiOperation(operationId: "GetApInvoice", tags: new[] { "ApInvoices" }, Summary = "Get one invoice by invoice number")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> GetApInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "apInvoices/{apInvoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
            ILogger log,
            string apInvoiceNumber)
        {
            var payload = await req.GetParameters<ApInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            payload.Success = await srv.GetByNumberAsync(payload, apInvoiceNumber);
            if (payload.Success)
            {
                payload.ApInvoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<ApInvoicePayload>(payload);
        }

        ///  Update Apinvoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateApInvoice))]
        [OpenApiOperation(operationId: "UpdateApInvoice", tags: new[] { "ApInvoices" }, Summary = "Update one apinvoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApInvoicePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadUpdate))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> UpdateApInvoice(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "apInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<ApInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            if (payload.Success)
            {
                srv.GetByNumber(payload.MasterAccountNum, payload.ProfileNum, payload.ApInvoice.ApInvoiceHeader.ApInvoiceNum);
                payload.ApInvoice = srv.ToDto();
            }
            payload.Messages = srv.Messages;

            //Directly return without waiting this result. 
            //if (payload.Success)
            //    srv.AddQboApInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<ApInvoicePayload>(payload);
        }



        /// <summary>
        /// Add ApInvoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddApInvoice))]
        #region open api definition
        [OpenApiOperation(operationId: "AddApInvoice", tags: new[] { "ApInvoices" }, Summary = "Add one apInvoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApInvoicePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadAdd))]
        #endregion
        public static async Task<JsonNetResponse<ApInvoicePayload>> AddApInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "apInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<ApInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            if (payload.Success)
            {
                srv.GetByNumber(payload.MasterAccountNum, payload.ProfileNum, srv.Data.ApInvoiceHeader.ApInvoiceNum);
                payload.ApInvoice = srv.ToDto();
            }
            payload.Messages = srv.Messages;
 

            return new JsonNetResponse<ApInvoicePayload>(payload);
        }



        /// <summary>
        /// Delete Apinvoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="apInvoiceNumber"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteByApInvoiceNumber))]
        [OpenApiOperation(operationId: "DeleteByApInvoiceNumber", tags: new[] { "ApInvoices" }, Summary = "Delete one apInvoice by apInvoice number.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "apInvoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "apInvoiceNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadDelete))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> DeleteByApInvoiceNumber(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "apInvoices/{apInvoiceNumber}")] Microsoft.AspNetCore.Http.HttpRequest req,
           string apInvoiceNumber)
        {
            var payload = await req.GetParameters<ApInvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            payload.Success = await srv.DeleteByApInvoiceNumberAsync(payload, apInvoiceNumber);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<ApInvoicePayload>(payload);
        }

        /// <summary>
        /// Load apinvoices list
        /// </summary>
        [FunctionName(nameof(ApInvoicesList))]
        [OpenApiOperation(operationId: "ApInvoicesList", tags: new[] { "ApInvoices" }, Summary = "Load apInvoices list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApInvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadFind))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> ApInvoicesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "apInvoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<ApInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceList(dataBaseFactory, new ApInvoiceQuery());
            await srv.GetApInvoiceListAsync(payload);
            return new JsonNetResponse<ApInvoicePayload>(payload);
        }


        /// <summary>
        /// Add invoice
        /// </summary>
        [FunctionName(nameof(Sample_ApInvocies_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ApInvociesSample", tags: new[] { "Sample" }, Summary = "Get new sample of Apinvoice")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadAdd))]
        public static async Task<JsonNetResponse<ApInvoicePayloadAdd>> Sample_ApInvocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/apInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<ApInvoicePayloadAdd>(ApInvoicePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_ApInvocies_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "ApInvoiceFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of ApInvoice find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadFind))]
        public static async Task<JsonNetResponse<ApInvoicePayloadFind>> Sample_ApInvocies_Find(
         [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/apInvoices/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<ApInvoicePayloadFind>(ApInvoicePayloadFind.GetSampleData());
        }

        [FunctionName(nameof(ApInvoicesListSummary))]
        [OpenApiOperation(operationId: "ApInvoicesListSummary", tags: new[] { "ApInvoices" }, Summary = "Load apInvoices list summary")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ApInvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApInvoicePayloadFind))]
        public static async Task<JsonNetResponse<ApInvoicePayload>> ApInvoicesListSummary(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "apInvoices/find/summary")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<ApInvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceList(dataBaseFactory, new ApInvoiceQuery());
            await srv.GetApInvoiceListSummaryAsync(payload);
            return new JsonNetResponse<ApInvoicePayload>(payload);
        }
    }
}

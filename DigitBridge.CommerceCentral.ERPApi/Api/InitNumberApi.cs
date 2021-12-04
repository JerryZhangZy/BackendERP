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
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{




    /// <summary>
    /// Process InitNumberApi
    /// </summary> 
    [ApiFilter(typeof(InitNumberApi))]
    public static class InitNumberApi
    {



        /// <summary>
        /// Get one  InitNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="initNumbersUuid"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetInitNumber))]
        [OpenApiOperation(operationId: "GetInitNumber", tags: new[] { "InitNumbers" }, Summary = "Get one InitNumber by initNumbersUuid")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "initNumbersUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "initNumbersUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadGetSingle))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> GetInitNumber(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "initNumbers/{initNumbersUuid}")] Microsoft.AspNetCore.Http.HttpRequest req,
            ILogger log,
            string initNumbersUuid)
        {
            var payload = await req.GetParameters<InitNumbersPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            payload.Success = await srv.GetByInitNumbersUuidAsync(payload.MasterAccountNum,payload.ProfileNum, initNumbersUuid);
            if (payload.Success)
            {
                payload.InitNumbers = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }




        ///// <summary>
        ///// Get one  InitNumber
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="log"></param>
        ///// <param name="customerUuid"></param>
        ///// <param name="type">values[so,invoice,po,apinvoice]</param>
        ///// <returns></returns>
        //[FunctionName(nameof(GetNextNumber))]
        //[OpenApiOperation(operationId: "GetNextNumber", tags: new[] { "InitNumbers" }, Summary = "Get one NextNumber by customerUuid and type")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "customerUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "customerUuid", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "type", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "type", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadGetSingle))]
        //public static async Task<JsonNetResponse<InitNumbersSinglePayload>> GetNextNumber(
        //    [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "initNumbers/{customerUuid}/{type}")] Microsoft.AspNetCore.Http.HttpRequest req,
        //    ILogger log,
        //    string customerUuid,string type)
        //{
        //    var payload = await req.GetParameters<InitNumbersSinglePayload>();
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var srv = new InitNumbersService(dataBaseFactory);
        //    payload.CurrentNumber = await srv.GetNextNumberAsync(payload.MasterAccountNum, payload.ProfileNum, customerUuid, type);
        //    payload.Messages = srv.Messages;
        //    return new JsonNetResponse<InitNumbersSinglePayload>(payload);
        //}



        ///  Update InitNumber 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInitNumber))]
        [OpenApiOperation(operationId: "UpdateInitNumber", tags: new[] { "InitNumbers" }, Summary = "Update one InitNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InitNumberPayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadUpdate))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> UpdateInitNumber(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "initNumbers")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;
 
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }



        /// <summary>
        /// Add InitNumber
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInitNumber))]
        [OpenApiOperation(operationId: "AddInitNumber", tags: new[] { "InitNumbers" }, Summary = "Add one InitNumber")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InitNumberPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadAdd))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> AddInitNumber(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "initNumbers")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true); 
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            payload.InitNumbers = srv.ToDto();

 
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }



        /// <summary>
        /// Delete InitNumber 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="initNumbersUuid"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteByInitNumbersUuid))]
        [OpenApiOperation(operationId: "DeleteByInitNumbersUuid", tags: new[] { "InitNumbers" }, Summary = "Delete one InitNumbers by InitNumbersUuid.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "initNumbersUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "initNumbersUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadDelete))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> DeleteByInitNumbersUuid(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "initNumbers/{initNumbersUuid}")] Microsoft.AspNetCore.Http.HttpRequest req,
           string initNumbersUuid)
        {
            var payload = await req.GetParameters<InitNumbersPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            payload.Success = await srv.DeleteByInitNumbersUuidAsync(payload, initNumbersUuid);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }

        /// <summary>
        /// Load InitNumbers list
        /// </summary>
        [FunctionName(nameof(InitNumbersList))]
        [OpenApiOperation(operationId: "InitNumbersList", tags: new[] { "InitNumbers" }, Summary = "Load InitNumber list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InitNumberPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadFind))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> InitNumbersList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "initNumbers/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersList(dataBaseFactory, new InitNumbersQuery());
            await srv.GetInitNumbersListAsync(payload);
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }


        /// <summary>
        /// Sample_InitNumbers_Post
        /// </summary>
        [FunctionName(nameof(Sample_InitNumbers_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InitNumbersSample", tags: new[] { "Sample" }, Summary = "Get new sample of InitNumbers")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadAdd))]
        public static async Task<JsonNetResponse<InitNumbersPayloadAdd>> Sample_InitNumbers_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/initNumbers")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<InitNumbersPayloadAdd>(InitNumbersPayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_InitNumbers_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InitNumberFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of InitNumber find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InitNumberPayloadFind))]
        public static async Task<JsonNetResponse<InitNumbersPayloadFind>> Sample_InitNumbers_Find(
         [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/initNumbers/find")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            return new JsonNetResponse<InitNumbersPayloadFind>(InitNumbersPayloadFind.GetSampleData());
        }

    }
}

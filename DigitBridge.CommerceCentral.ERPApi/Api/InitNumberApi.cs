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

        [FunctionName(nameof(UpdateMulti))]
        public static async Task<JsonNetResponse<InitNumbersPayload>> UpdateMulti(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "initNumbers/updateMulti")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InitNumbersPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InitNumbersService(dataBaseFactory);
            var success = true;
            foreach(var initNumber in payload.InitNumberss)
            {
                payload.InitNumbers = initNumber;
                success = success & await srv.UpdateAsync(payload);
            }
            payload.Success = success;
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InitNumbersPayload>(payload);
        }
    }
}

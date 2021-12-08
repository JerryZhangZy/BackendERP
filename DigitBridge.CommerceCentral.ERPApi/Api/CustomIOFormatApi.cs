using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;
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

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(CustomerApi))]
    public static class CustomIOFormatApi
    {



        //[FunctionName(nameof(GetCustomIOFormatData))]
        //[OpenApiOperation(operationId: "GetCustomIOFormatData", tags: new[] { "CustomIOFormats" })]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "formatType", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "formatType", Description = "formatType", Visibility = OpenApiVisibilityType.Advanced)]
       
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadGetSingle))]
        //public static async Task<JsonNetResponse<CustomIOFormatPayload>> GetCustomIOFormatData(
        //  [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customIOFormats/{formatType}")] HttpRequest req,
        //  string formatType, int formatNumber)
        //{
        //    var payload = await req.GetParameters<CustomIOFormatPayload>();
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var svc = new CustomIOFormatService(dbFactory);


        //    payload.Success = await svc.GetAsync(payload, formatType, formatNumber);
        //    if (payload.Success)
        //        payload.CustomIOFormat = svc.ToDto();
        //    else
        //        payload.Messages = svc.Messages;
        //    return new JsonNetResponse<CustomIOFormatPayload>(payload);

        //}




        [FunctionName(nameof(GetCustomIOFormat))]
        [OpenApiOperation(operationId: "GetCustomIOFormat", tags: new[] { "CustomIOFormats" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "formatType", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "formatType", Description = "formatType", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "formatNumber", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "formatNumber", Description = "formatNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadGetSingle))]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> GetCustomIOFormat(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "customIOFormats/{formatType}/{formatNumber}")] HttpRequest req,
            string formatType ,int formatNumber)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomIOFormatService(dbFactory);


            payload.Success = await svc.GetAsync(payload, formatType, formatNumber);
            if(payload.Success)
                payload.CustomIOFormat = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<CustomIOFormatPayload>(payload);

        }



        [FunctionName(nameof(AddCustomIOFormat))]
        [OpenApiOperation(operationId: "AddCustomIOFormat", tags: new[] { "CustomIOFormats" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadAdd), Description = "CustomIOFormatDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadAdd))]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> AddCustomIOFormat(
    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "customIOFormats")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomIOFormatService(dbFactory);
            payload.Success = await svc.AddAsync(payload);
            if(payload.Success)
                payload.CustomIOFormat = svc.ToDto();
            else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<CustomIOFormatPayload>(payload);
        }




        [FunctionName(nameof(UpdateCustomIOFormat))]
        [OpenApiOperation(operationId: "UpdateCustomIOFormat", tags: new[] { "CustomIOFormats" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadUpdate), Description = "CustomIOFormatDataDto ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadUpdate))]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> UpdateCustomIOFormat(
          [HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "customIOFormats")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>(true);
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomIOFormatService(dbFactory);
            payload.Success = await svc.UpdateAsync(payload);
            if(payload.Success)
                payload.CustomIOFormat = svc.ToDto();
            else
                payload.Messages = svc.Messages;
 
            return new JsonNetResponse<CustomIOFormatPayload>(payload);
        }





        [FunctionName(nameof(DeleteCustomIOFormat))]
        [OpenApiOperation(operationId: "DeleteCustomIOFormat", tags: new[] { "CustomIOFormats" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "formatNumber", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "formatNumber", Description = "formatNumber", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadDelete), Description = "The OK response")]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> DeleteCustomIOFormat(
          [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "customIOFormats/{formatNumber}")] HttpRequest req,
          int formatNumber)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var svc = new CustomIOFormatService(dbFactory);

            payload.Success = await svc.DeleteByNumberAsync(payload, formatNumber.ToString());
            if (payload.Success)
                payload.CustomIOFormat = svc.ToDto();
           else
                payload.Messages = svc.Messages;
            return new JsonNetResponse<CustomIOFormatPayload>(payload);
        }


        /// <summary>
        /// Load CustomIOFormat list
        /// </summary>
        [FunctionName(nameof(CustomIOFormatList))]
        [OpenApiOperation(operationId: "CustomIOFormatList", tags: new[] { "CustomIOFormats" }, Summary = "Load CustomIOFormat list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadFind))]
        public static async Task<JsonNetResponse<CustomIOFormatPayload>> CustomIOFormatList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customIOFormats/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<CustomIOFormatPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new CustomIOFormatList(dataBaseFactory, new CustomIOFormatQuery());
            await srv.GetCustomIOFormatListAsync(payload);
            return new JsonNetResponse<CustomIOFormatPayload>(payload);
        }




        /// <summary>
        /// Add CustomIOFormat
        /// </summary>
        [FunctionName(nameof(Sample_CustomIOFormat_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "CustomIOFormatAddSample", tags: new[] { "Sample" }, Summary = "Get new sample of CustomIOFormat")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadAdd))]
        public static async Task<JsonNetResponse<CustomIOFormatPayloadAdd>> Sample_CustomIOFormat_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/customIOFormat")] HttpRequest req)
        {
            return new JsonNetResponse<CustomIOFormatPayloadAdd>(CustomIOFormatPayloadAdd.GetSampleData());
        }

        /// <summary>
        /// find CustomIOFormat
        /// </summary>
        [FunctionName(nameof(Sample_CustomIOFormat_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "Sample_CustomIOFormat_Find", tags: new[] { "Sample" }, Summary = "Get new sample of CustomIOFormat find")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CustomIOFormatPayloadFind))]
        public static async Task<JsonNetResponse<CustomIOFormatPayloadFind>> Sample_CustomIOFormat_Find(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/customIOFormats/find")] HttpRequest req)
        {
            return new JsonNetResponse<CustomIOFormatPayloadFind>(CustomIOFormatPayloadFind.GetSampleData());
        }
    }
}

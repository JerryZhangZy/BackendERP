using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.QuickBooks.Integration;
using DigitBridge.QuickBooks.Integration.Mdl;
using DigitBridge.QuickBooks.Integration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(QuickBooksUserCredentialApi))]
    public static class QuickBooksUserCredentialApi
    {
        [FunctionName(nameof(GetOAuthUrl))]
        [OpenApiOperation(operationId: "GetOAuthUrl", tags: new[] { "QuickBooksUserCredential" }, Summary = "Get OAuth Url")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> GetOAuthUrl(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "QboUserCredential/oauthUrl")] HttpRequest req)
        {
            var payload = await req.GetParameters<QuickBooksConnectionInfoPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = await UserCredentialService.CreateAsync(dataBaseFactory);
            (bool isSuccess, string respond) = await srv.OAuthUrlAsync(payload);
            if (isSuccess)
            {
                payload.OAuthUrl = respond;
            }
            else
            {
                payload.Success = false;
                payload.Messages.AddError(respond);
            }
            return new JsonNetResponse<QuickBooksConnectionInfoPayload>(payload);
        }

        [FunctionName(nameof(TokenReceiver))]
        [OpenApiParameter(name: "MasterAccountNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "ProfileNum", In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> TokenReceiver(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "QboUserCredential/TokenReceiver/{MasterAccountNum}/{ProfileNum}")] HttpRequest req,int MasterAccountNum,int ProfileNum)
        {
                var payload = new QuickBooksConnectionInfoPayload()
                {
                    ProfileNum = ProfileNum,
                    MasterAccountNum = MasterAccountNum
                };
                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var srv = await UserCredentialService.CreateAsync(dataBaseFactory);

                string authCode = req.Query["code"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "code").ForceToTrimString();
                string requestState = req.Query["state"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "state").ForceToTrimString();
                string realmId = req.Query["realmId"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "realmId").ForceToTrimString();

                (bool isSuccess, string respond) = await srv.HandleTokensAsync(realmId, authCode,requestState);

                if (!isSuccess)
                {
                    payload.Success = false;
                    payload.Messages.AddError("Error Getting Token From Quickbooks");
                }
                else
                {
                    payload.TokenReceiverReturnUrl = respond;
                }

                return new JsonNetResponse<QuickBooksConnectionInfoPayload>(payload);
        }
        /// <summary>
        /// Return the QboOAuthTokenStatus for User in <int> 0 : Uninitialized, 1 : Success, 2: Error
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetTokenStatus))]
        [OpenApiOperation(operationId: "GetTokenStatus", tags: new[] { "QuickBooksUserCredential" }, Summary = "Get Token Status")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> GetTokenStatus(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "QboUserCredential/tokenStatus")] HttpRequest req)
        {
            var payload = await req.GetParameters<QuickBooksConnectionInfoPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = await UserCredentialService.CreateAsync(dataBaseFactory);
            (bool isSuccess, string respond) = await srv.GetTokenStatusAsync(payload);
            if (isSuccess)
            {
                payload.TokenStatus = respond;
            }
            else
            {
                payload.Success = false;
                payload.Messages.AddError(respond);
            }
            return new JsonNetResponse<QuickBooksConnectionInfoPayload>(payload);
        }

        [FunctionName(nameof(DisconnectUser))]
        [OpenApiOperation(operationId: "DisconnectUser", tags: new[] { "QuickBooksUserCredential" }, Summary = "DisconnectUser")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> DisconnectUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "disconnectUser")] HttpRequest req)
        {
            var payload = await req.GetParameters<QuickBooksConnectionInfoPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = await UserCredentialService.CreateAsync(dataBaseFactory);
            (bool isSuccess, string respond) = await srv.DisconnectUserAsync(payload);
            if (!isSuccess)
            {
                payload.Success = false;
                payload.Messages.AddError(respond);
            }
            return new JsonNetResponse<QuickBooksConnectionInfoPayload>(payload);
        }
    }
}

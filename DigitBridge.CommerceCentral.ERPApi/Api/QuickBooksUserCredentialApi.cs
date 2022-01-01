using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.QuickBooks.Integration;
using DigitBridge.QuickBooks.Integration.Mdl;
using DigitBridge.QuickBooks.Integration.Mdl.Qbo;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(QuickBooksUserCredentialApi))]
    public static class QuickBooksUserCredentialApi
    {
        [FunctionName(nameof(GetQuickBooksOAuthUrl))]
        [OpenApiOperation(operationId: "GetQuickBooksOAuthUrl", tags: new[] { "QuickBooksUserCredential" }, Summary = "Get OAuth Url")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> GetQuickBooksOAuthUrl(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "qboUserCredential/oauthUrl")] HttpRequest req)
        {
            var payload = await req.GetParameters<QuickBooksConnectionInfoPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = await UserCredentialService.CreateAsync(dataBaseFactory);
            (bool isSuccess, string respond, string state) = await srv.OAuthUrlAsync(payload);
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

        [FunctionName(nameof(QuickBooksTokenReceiver))] 
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> QuickBooksTokenReceiver(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "qboUserCredential/TokenReceiver")] HttpRequest req)
        {

            string requestState = req.Query["state"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "state").ForceToTrimString();
            var tableUniversal = await QboCloudTableUniversal.AuthTableUniversal();
            var authMapInfo =tableUniversal.GetEntityByRowKey(requestState);
            if (authMapInfo != null)
            {
                string authCode = req.Query["code"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "code").ForceToTrimString();
                string realmId = req.Query["realmId"].ForceToTrimString();//MyAppHelper.GetHeaderQueryValue(req, "realmId").ForceToTrimString();

                var payload = new QuickBooksConnectionInfoPayload()
                {
                    ProfileNum = authMapInfo.ProfileNum,
                    MasterAccountNum = authMapInfo.MasterAccountNum
                };
                var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var srv = await UserCredentialService.CreateAsync(dataBaseFactory);
                (bool isSuccess, string respond) = await srv.HandleTokensAsync(realmId, authCode, requestState,authMapInfo);

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
            var pl = new QuickBooksConnectionInfoPayload()
            {
                Success = false
            };
            pl.Messages.AddError("Error Getting Token From Quickbooks");
            return new JsonNetResponse<QuickBooksConnectionInfoPayload>(pl);
        }

        /// <summary>
        /// Return the QboOAuthTokenStatus for User in <int> 0 : Uninitialized, 1 : Success, 2: Error
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="context"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetQuickBooksTokenStatus))]
        [OpenApiOperation(operationId: "GetQuickBooksTokenStatus", tags: new[] { "QuickBooksUserCredential" }, Summary = "Get Token Status")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> GetQuickBooksTokenStatus(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "qboUserCredential/tokenStatus")] HttpRequest req)
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

        [FunctionName(nameof(DisconnectQuickBooksUser))]
        [OpenApiOperation(operationId: "DisconnectQuickBooksUser", tags: new[] { "QuickBooksUserCredential" }, Summary = "DisconnectUser")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(QuickBooksConnectionInfoPayload), Description = "The OK response")]
        public static async Task<JsonNetResponse<QuickBooksConnectionInfoPayload>> DisconnectQuickBooksUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "qboUserCredential/disconnectUser")] HttpRequest req)
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

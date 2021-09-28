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
        ///// <summary>
        ///// Return the QboOAuthTokenStatus for User in <int> 0 : Uninitialized, 1 : Success, 2: Error
        ///// </summary>
        ///// <param name="req"></param>
        ///// <param name="log"></param>
        ///// <param name="context"></param>
        ///// <param name="claimsPrincipal"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(GetTokenStatus))]
        //public static async Task<IActionResult> GetTokenStatus(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "tokenStatus")] HttpRequest req,
        //     ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger Get TokenStatus function processed a request.");
        //    try
        //    {
        //        MyAppSetting appSetting = new MyAppSetting(context);

        //        string masterAccountNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.MasterAccountNum);
        //        string profileNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.ProfileNum);

        //        if (string.IsNullOrEmpty(masterAccountNum) || string.IsNullOrEmpty(profileNum))
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "Missing Parameters in Header",
        //                Error = new ErrorInfo() { Title = "MasterAccountNum and ProfileNum are required but not provided." }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }

        //        QboDbConfig dbConfig = new QboDbConfig(
        //            appSetting.DBConnectionValue,
        //            appSetting.AzureUseManagedIdentity,
        //            appSetting.AzureTokenProviderConnectionString,
        //            appSetting.AzureTenantId,
        //            appSetting.CentralOrderTableName,
        //            appSetting.CentralItemLineTableName,
        //            appSetting.QuickBooksConnectionInfoTableName,
        //            appSetting.QuickBooksIntegrationSettingTableName,
        //            appSetting.QuickBooksChannelAccSettingTableName,
        //            appSetting.CryptKey);

        //        QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
        //            appSetting.RedirectUrl,
        //            appSetting.Environment,
        //            appSetting.BaseUrl,
        //            appSetting.MinorVersion,
        //            appSetting.AppClientId,
        //            appSetting.AppClientSecret);

        //        var userCredential = await UserCredentialService.CreateAsync(
        //            dbConfig, qboConnectionConfig, masterAccountNum, profileNum);

        //        (bool isSuccess, string respond) = await userCredential.GetTokenStatus();

        //        if (!isSuccess)
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "Uninitialized User",
        //                Error = new ErrorInfo() { Title = respond }
        //            };
        //        }

        //        return new OkObjectResult(
        //            new TokenStatusApiResponseType
        //            {
        //                QboOAuthTokenStatus = respond.ForceToInt()
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError("Exception: " + ex.Message);
        //        string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
        //        ObjectResult or = new ObjectResult(new { exception = errMsg })
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };
        //        return or;
        //    }
        //}

        //[FunctionName(nameof(DisconnectUser))]
        //public static async Task<IActionResult> DisconnectUser(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "disconnectUser")] HttpRequest req,
        //     ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger Get DisconnectUser function processed a request.");
        //    try
        //    {
        //        MyAppSetting appSetting = new MyAppSetting(context);

        //        string masterAccountNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.MasterAccountNum);
        //        string profileNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.ProfileNum);

        //        if (string.IsNullOrEmpty(masterAccountNum) || string.IsNullOrEmpty(profileNum))
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "Missing Parameters in Header",
        //                Error = new ErrorInfo() { Title = "MasterAccountNum and ProfileNum are required but not provided." }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }

        //        QboDbConfig dbConfig = new QboDbConfig(
        //            appSetting.DBConnectionValue,
        //            appSetting.AzureUseManagedIdentity,
        //            appSetting.AzureTokenProviderConnectionString,
        //            appSetting.AzureTenantId,
        //            appSetting.CentralOrderTableName,
        //            appSetting.CentralItemLineTableName,
        //            appSetting.QuickBooksConnectionInfoTableName,
        //            appSetting.QuickBooksIntegrationSettingTableName,
        //            appSetting.QuickBooksChannelAccSettingTableName,
        //            appSetting.CryptKey);

        //        QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
        //            appSetting.RedirectUrl,
        //            appSetting.Environment,
        //            appSetting.BaseUrl,
        //            appSetting.MinorVersion,
        //            appSetting.AppClientId,
        //            appSetting.AppClientSecret);

        //        var userCredential = await UserCredentialService.CreateAsync(
        //            dbConfig, qboConnectionConfig, masterAccountNum, profileNum);

        //        (bool isSuccess, string respond) = await userCredential.DisconnectUser();

        //        if (!isSuccess)
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "Uninitialized User",
        //                Error = new ErrorInfo() { Title = respond }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }

        //        return new OkObjectResult("Disconnected User from Quickbooks Online Successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.LogError("Exception: " + ex.Message);
        //        string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
        //        ObjectResult or = new ObjectResult(new { exception = errMsg })
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };
        //        return or;
        //    }
        //}
    }
}

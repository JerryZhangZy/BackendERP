using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using Digitbridge.QuickbooksOnline.IntegrationApiFunc;
using System.Net;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl.Infrastructure;
using UneedgoHelper.DotNet.Common;
using System.Reflection;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl;
using Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model;
using System.Collections.Generic;
using System.Linq;

namespace Digitbridge.QuickbooksOnline.IntegrationApiFunc
{
    public static class QuickBooksOnlineUserCredentialApi
    {

        [FunctionName(nameof(GetOAuthUrl))]
        public static async Task<IActionResult> GetOAuthUrl(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "oauthUrl")] HttpRequest req,
             ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            log.LogInformation("C# HTTP trigger GET GetOAuthUrl function processed a request.");
            try
            {
                string masterAccountNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.MasterAccountNum);
                string profileNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.ProfileNum);

                if (string.IsNullOrEmpty(masterAccountNum) || string.IsNullOrEmpty(profileNum))
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Missing Parameters in Header",
                        Error = new ErrorInfo() { Title = "MasterAccountNum and ProfileNum are required but not provided." }
                    };
                    return new ObjectResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                MyAppSetting appSetting = new MyAppSetting(context);

                QboDbConfig dbConfig = new QboDbConfig(
                    appSetting.DBConnectionValue,
                    appSetting.AzureUseManagedIdentity,
                    appSetting.AzureTokenProviderConnectionString,
                    appSetting.AzureTenantId,
                    appSetting.CentralOrderTableName,
                    appSetting.CentralItemLineTableName,
                    appSetting.QuickBooksConnectionInfoTableName,
                    appSetting.QuickBooksIntegrationSettingTableName,
                    appSetting.QuickBooksChannelAccSettingTableName,
                    appSetting.CryptKey);

                QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                    appSetting.RedirectUrl,
                    appSetting.Environment,
                    appSetting.BaseUrl,
                    appSetting.MinorVersion,
                    appSetting.AppClientId,
                    appSetting.AppClientSecret);

                QuickBooksOnlineUserCredential userCredential = await QuickBooksOnlineUserCredential.CreateAsync(
                    dbConfig, qboConnectionConfig, masterAccountNum, profileNum);

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                (bool isSuccess, string respond) = await userCredential.OAuthUrl();

                if (!isSuccess)
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Failed to Get Quickbooks Online Login Redirect Url",
                        Error = new ErrorInfo() { Title = respond }
                    };
                }

                return new OkObjectResult(
                    new UserCredentilApiResponseType
                    {
                        RedirectUrl = respond
                    });
            }
            catch (Exception ex)
            {
                log.LogError("Exception: " + ex.Message);
                string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                ObjectResult or = new ObjectResult(new { exception = errMsg })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return or;
            }
        }

        [FunctionName(nameof(TokenReceiver))]
        public static async Task<IActionResult> TokenReceiver(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tokenReceiver")] HttpRequest req,
             ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            log.LogInformation("C# HTTP trigger Post TokenReceiver function processed a request.");
            try
            {
                
                // Check if the request is Valid
                MyAppSetting appSetting = new MyAppSetting(context);

                List<RequestParameterType> requestParamters = appSetting.WebRequestParameters
                   .FirstOrDefault(p => p.RequestName == context.FunctionName).RequestParameters;

                bool ret;
                string errMsg;

                (ret, errMsg) = MyAppHelper.ValidateRequestFromQuery(req, requestParamters);

                if (ret == false)
                {
                    return new ObjectResult(new { error = errMsg })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                string authCode = MyAppHelper.GetHeaderQueryValue(req, "code").ForceToTrimString();
                string requestState = MyAppHelper.GetHeaderQueryValue(req, "state").ForceToTrimString();
                string realmId = MyAppHelper.GetHeaderQueryValue(req, "realmId").ForceToTrimString();

                QboDbConfig dbConfig = new QboDbConfig(
                    appSetting.DBConnectionValue,
                    appSetting.AzureUseManagedIdentity,
                    appSetting.AzureTokenProviderConnectionString,
                    appSetting.AzureTenantId,
                    appSetting.CentralOrderTableName,
                    appSetting.CentralItemLineTableName,
                    appSetting.QuickBooksConnectionInfoTableName,
                    appSetting.QuickBooksIntegrationSettingTableName,
                    appSetting.QuickBooksChannelAccSettingTableName,
                    appSetting.CryptKey);

                QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                    appSetting.RedirectUrl,
                    appSetting.Environment,
                    appSetting.BaseUrl,
                    appSetting.MinorVersion,
                    appSetting.AppClientId,
                    appSetting.AppClientSecret);

                QuickBooksOnlineUserCredential userCredential = await QuickBooksOnlineUserCredential.CreateAsync(
                    dbConfig, qboConnectionConfig, requestState);

                (bool isSuccess, string respond) = await userCredential.HandleTokens(realmId, authCode);

                if (!isSuccess)
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Error Getting Token From Quickbooks",
                        Error = new ErrorInfo() { Title = respond }
                    };
                }

                return new RedirectResult(appSetting.TokenReceiverReturnUrl)  { Permanent = true};
            }
            catch (Exception ex)
            {
                log.LogError("Exception: " + ex.Message);
                string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                ObjectResult or = new ObjectResult(new { exception = errMsg })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return or;
            }
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
        public static async Task<IActionResult> GetTokenStatus(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "tokenStatus")] HttpRequest req,
             ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            log.LogInformation("C# HTTP trigger Get TokenStatus function processed a request.");
            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);

                string masterAccountNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.MasterAccountNum);
                string profileNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.ProfileNum);

                if (string.IsNullOrEmpty(masterAccountNum) || string.IsNullOrEmpty(profileNum))
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Missing Parameters in Header",
                        Error = new ErrorInfo() { Title = "MasterAccountNum and ProfileNum are required but not provided." }
                    };
                    return new ObjectResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                QboDbConfig dbConfig = new QboDbConfig(
                    appSetting.DBConnectionValue,
                    appSetting.AzureUseManagedIdentity,
                    appSetting.AzureTokenProviderConnectionString,
                    appSetting.AzureTenantId,
                    appSetting.CentralOrderTableName,
                    appSetting.CentralItemLineTableName,
                    appSetting.QuickBooksConnectionInfoTableName,
                    appSetting.QuickBooksIntegrationSettingTableName,
                    appSetting.QuickBooksChannelAccSettingTableName,
                    appSetting.CryptKey);

                QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                    appSetting.RedirectUrl,
                    appSetting.Environment,
                    appSetting.BaseUrl,
                    appSetting.MinorVersion,
                    appSetting.AppClientId,
                    appSetting.AppClientSecret);

                QuickBooksOnlineUserCredential userCredential = await QuickBooksOnlineUserCredential.CreateAsync(
                    dbConfig, qboConnectionConfig, masterAccountNum, profileNum);

                (bool isSuccess, string respond) = await userCredential.GetTokenStatus();

                if (!isSuccess)
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Uninitialized User",
                        Error = new ErrorInfo() { Title = respond }
                    };
                }

                return new OkObjectResult(
                    new TokenStatusApiResponseType
                    {
                        QboOAuthTokenStatus = respond.ForceToInt()
                    });
            }
            catch (Exception ex)
            {
                log.LogError("Exception: " + ex.Message);
                string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                ObjectResult or = new ObjectResult(new { exception = errMsg })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return or;
            }
        }

        [FunctionName(nameof(DisconnectUser))]
        public static async Task<IActionResult> DisconnectUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "disconnectUser")] HttpRequest req,
             ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            log.LogInformation("C# HTTP trigger Get DisconnectUser function processed a request.");
            try
            {
                MyAppSetting appSetting = new MyAppSetting(context);

                string masterAccountNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.MasterAccountNum);
                string profileNum = MyAppHelper.GetHeaderQueryValue(req, MyHttpHeaderName.ProfileNum);

                if (string.IsNullOrEmpty(masterAccountNum) || string.IsNullOrEmpty(profileNum))
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Missing Parameters in Header",
                        Error = new ErrorInfo() { Title = "MasterAccountNum and ProfileNum are required but not provided." }
                    };
                    return new ObjectResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                QboDbConfig dbConfig = new QboDbConfig(
                    appSetting.DBConnectionValue,
                    appSetting.AzureUseManagedIdentity,
                    appSetting.AzureTokenProviderConnectionString,
                    appSetting.AzureTenantId,
                    appSetting.CentralOrderTableName,
                    appSetting.CentralItemLineTableName,
                    appSetting.QuickBooksConnectionInfoTableName,
                    appSetting.QuickBooksIntegrationSettingTableName,
                    appSetting.QuickBooksChannelAccSettingTableName,
                    appSetting.CryptKey);

                QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
                    appSetting.RedirectUrl,
                    appSetting.Environment,
                    appSetting.BaseUrl,
                    appSetting.MinorVersion,
                    appSetting.AppClientId,
                    appSetting.AppClientSecret);

                QuickBooksOnlineUserCredential userCredential = await QuickBooksOnlineUserCredential.CreateAsync(
                    dbConfig, qboConnectionConfig, masterAccountNum, profileNum);

                (bool isSuccess, string respond) = await userCredential.DisconnectUser();

                if (!isSuccess)
                {
                    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
                    {
                        Id = HttpResponseCodeID.BAD_REQUEST,
                        Status = "400",
                        Title = "Uninitialized User",
                        Error = new ErrorInfo() { Title = respond }
                    };
                    return new ObjectResult(errorResponse)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                return new OkObjectResult("Disconnected User from Quickbooks Online Successfully.");
            }
            catch (Exception ex)
            {
                log.LogError("Exception: " + ex.Message);
                string errMsg = ExceptionUtility.FormatMessage(MethodBase.GetCurrentMethod(), ex);
                ObjectResult or = new ObjectResult(new { exception = errMsg })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return or;
            }
        }
    }

}

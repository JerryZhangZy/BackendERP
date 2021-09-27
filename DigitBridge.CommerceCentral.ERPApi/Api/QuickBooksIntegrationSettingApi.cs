using DigitBridge.CommerceCentral.ApiCommon;
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
    [ApiFilter(typeof(QuickBooksIntegrationSettingApi))]
    public static class QuickBooksIntegrationSettingApi
    {
        //[FunctionName(nameof(GetIntegrationSetting))]
        //[OpenApiOperation(operationId: "IntegraionSetting", tags: new[] { "IntegraionSetting" })]
        //[OpenApiParameter(name: "MasterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "MasterAccountNum", Description = "Enter User's MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "ProfileNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "ProfileNum", Description = "Enter User's ProfileNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IntegrationSettingApiRespondType), Summary = "IntegrationSetting and List of ChannelAccountSettings", Description = "Retrun Json result if success.")]
        //public static async Task<IActionResult> GetIntegrationSetting(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "integraionSetting")] HttpRequest req,
        //    ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger GetIntegrationSetting function processed a request.");
        //    string ignoreMessage = "";

        //    try
        //    {
        //        //if (MyAppHelper.VerifyProfileLevelPermission(req, claimsPrincipal, context,
        //        //       "", "", out ignoreMessage) == false)
        //        //{
        //        //    DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //        //    {
        //        //        Id = HttpResponseCodeID.FOBIDDEN,
        //        //        Status = "401",
        //        //        Title = "Unauthorized",
        //        //        Error = new ErrorInfo() { Title = "No permission to access this resource." }
        //        //    };
        //        //    return new ObjectResult( errorResponse )
        //        //    {
        //        //        StatusCode = (int)HttpStatusCode.Unauthorized
        //        //    };
        //        //}

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

        //        MyAppSetting appSetting = new MyAppSetting(context);

        //        QboDbConfig dbConfig = new QboDbConfig(
        //              appSetting.DBConnectionValue,
        //              appSetting.AzureUseManagedIdentity,
        //              appSetting.AzureTokenProviderConnectionString,
        //              appSetting.AzureTenantId,
        //              appSetting.CentralOrderTableName,
        //              appSetting.CentralItemLineTableName,
        //              appSetting.QuickBooksConnectionInfoTableName,
        //              appSetting.QuickBooksIntegrationSettingTableName,
        //              appSetting.QuickBooksChannelAccSettingTableName,
        //              appSetting.CryptKey);

        //        QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
        //             appSetting.RedirectUrl,
        //             appSetting.Environment,
        //             appSetting.BaseUrl,
        //             appSetting.MinorVersion,
        //             appSetting.AppClientId,
        //             appSetting.AppClientSecret);

        //        var integrationSetting =
        //            await IntegrationSettingService.CreateAsync(dbConfig, qboConnectionConfig,
        //            masterAccountNum.ForceToInt(), profileNum.ForceToInt());

        //        IntegrationSettingApiRespondType response =
        //            await integrationSetting.GetIntegrationSetting();

        //        return new OkObjectResult(response);

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

        //[FunctionName(nameof(AddIntegrationSetting))]
        //[OpenApiOperation(operationId: "IntegraionSetting", tags: new[] { "IntegraionSetting" })]
        //[OpenApiParameter(name: "MasterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "MasterAccountNum", Description = "Enter User's MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "ProfileNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "ProfileNum", Description = "Enter User's ProfileNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(IntegrationSettingApiReqType), Required = true, Description = "Request Body in json format. Except for ChannelQboFeeAcountName and ChannelQboFeeAcountId are conditional required, all fields are required.")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text", bodyType: typeof(string), Description = "Add Integration Setting Response.")]
        //public static async Task<IActionResult> AddIntegrationSetting(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "integraionSetting")] HttpRequest req,
        //    ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger PostIntegrationSetting function processed a request.");
        //    try
        //    {
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

        //        MyAppSetting appSetting = new MyAppSetting(context);

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

        //        var integrationSetting =
        //            await IntegrationSettingService.CreateAsync(dbConfig, qboConnectionConfig,
        //            masterAccountNum.ForceToInt(), profileNum.ForceToInt());

        //        bool isSettingExist = await integrationSetting.IsQboIntegrationSettingExist();

        //        // If settings existed, return https status code 200 and message to state settings exists.
        //        if (isSettingExist)
        //        {
        //            return new OkObjectResult("Quickbooks Online Integration Setting for this user existed, no setting was added.");
        //        }

        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //        (bool isSuccess, string curErrMsg) = await integrationSetting.PostIntegrationSetting(requestBody);

        //        if (!isSuccess)
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "User Quickbooks Online IntegraionSetting Insert Error",
        //                Error = new ErrorInfo() { Title = curErrMsg }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }
        //        // If settings are created successfully, return http status code 201.
        //        return new ObjectResult("Quickbooks Online Integration Setting was added successfully.") { StatusCode = (int)HttpStatusCode.Created };
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

        //[FunctionName(nameof(PatchIntegrationSetting))]
        //[OpenApiOperation(operationId: "IntegraionSetting", tags: new[] { "IntegraionSetting" })]
        //[OpenApiParameter(name: "MasterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "MasterAccountNum", Description = "Enter User's MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "ProfileNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "ProfileNum", Description = "Enter User's ProfileNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(IntegrationSettingApiReqType), Required = true, Description = "Request Body in json format. Except for ChannelQboFeeAcountName and ChannelQboFeeAcountId are conditional required, all fields are required.")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text", bodyType: typeof(string), Description = "Update Integration Setting Response.")]
        //public static async Task<IActionResult> PatchIntegrationSetting(
        //    [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "integraionSetting")] HttpRequest req,
        //    ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger PatchIntegrationSetting function processed a request.");
        //    try
        //    {
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

        //        MyAppSetting appSetting = new MyAppSetting(context);

        //        QboDbConfig dbConfig = new QboDbConfig(
        //              appSetting.DBConnectionValue,
        //              appSetting.AzureUseManagedIdentity,
        //              appSetting.AzureTokenProviderConnectionString,
        //              appSetting.AzureTenantId,
        //              appSetting.CentralOrderTableName,
        //              appSetting.CentralItemLineTableName,
        //              appSetting.QuickBooksConnectionInfoTableName,
        //              appSetting.QuickBooksIntegrationSettingTableName,
        //              appSetting.QuickBooksChannelAccSettingTableName,
        //              appSetting.CryptKey);

        //        QboConnectionConfig qboConnectionConfig = new QboConnectionConfig(
        //            appSetting.RedirectUrl,
        //            appSetting.Environment,
        //            appSetting.BaseUrl,
        //            appSetting.MinorVersion,
        //            appSetting.AppClientId,
        //            appSetting.AppClientSecret);

        //        var integrationSetting =
        //            await IntegrationSettingService.CreateAsync(dbConfig, qboConnectionConfig,
        //            masterAccountNum.ForceToInt(), profileNum.ForceToInt());

        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //        (bool isSuccess, string curErrMsg) = await integrationSetting.PatchIntegrationSetting(requestBody);

        //        if (!isSuccess)
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "User Quickbooks Online IntegraionSetting Update Error",
        //                Error = new ErrorInfo() { Title = curErrMsg }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }
        //        return new OkObjectResult("Quickbooks Online Integration Setting Updated.");
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

        //[FunctionName(nameof(GetUserInitialData))]
        //[OpenApiOperation(operationId: "InitialData", tags: new[] { "IntegraionSetting" })]
        //[OpenApiParameter(name: "MasterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "MasterAccountNum", Description = "Enter User's MasterAccountNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "ProfileNum", In = ParameterLocation.Header, Required = true, Type = typeof(string), Summary = "ProfileNum", Description = "Enter User's ProfileNum", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UserInitialDataApiResponseType), Summary = "Initial Data for User Setting.", Description = "Retrun Json result if success.")]
        //public static async Task<IActionResult> GetUserInitialData(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "userInitialData")] HttpRequest req,
        //    ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        //{
        //    log.LogInformation("C# HTTP trigger GetUserInitialData function processed a request.");
        //    try
        //    {
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

        //        MyAppSetting appSetting = new MyAppSetting(context);

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
        //             appSetting.RedirectUrl,
        //             appSetting.Environment,
        //             appSetting.BaseUrl,
        //             appSetting.MinorVersion,
        //             appSetting.AppClientId,
        //             appSetting.AppClientSecret);

        //        var integrationSetting =
        //            await IntegrationSettingService.CreateAsync(
        //                dbConfig, qboConnectionConfig, masterAccountNum.ForceToInt(), profileNum.ForceToInt());

        //        UserInitialDataApiResponseType response =
        //            await integrationSetting.GetUserInitialData();

        //        if (response == null)
        //        {
        //            DigitBridgeHttpResponse errorResponse = new DigitBridgeHttpResponse()
        //            {
        //                Id = HttpResponseCodeID.BAD_REQUEST,
        //                Status = "400",
        //                Title = "Failed to get User Initial Data from Quickbooks Online",
        //                Error = new ErrorInfo()
        //                {
        //                    Title = "User's Quickbooks Online Credential Information is uninitialized or incompleted."
        //                }
        //            };
        //            return new ObjectResult(errorResponse)
        //            {
        //                StatusCode = (int)HttpStatusCode.BadRequest
        //            };
        //        }

        //        return new OkObjectResult(response);

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

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
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

namespace DigitBridge.CommerceCentral.ERPApi
{
    [ApiFilter(typeof(AccountApi))]
    public static class AccountApi
    {
        [FunctionName(nameof(GetProfiles))]
        [OpenApiOperation(operationId: "Profiles", tags: new[] { "User Account" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AccountPayload), Description = "User Login Account ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AccountPayload))]
        public static async Task<JsonNetResponse<AccountPayload>> GetProfiles(
  [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "account/Profiles")] HttpRequest req,
  ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            var payload = await req.GetParameters<AccountPayload>(true);
            payload.ClaimsPrincipal = claimsPrincipal;
            payload.ClaimsPrincipal = AccountApiService.EngineerClaimsPrincipal(payload);

            if (!(await AccountApiService.LoadUserProfiles(payload)))
                return new JsonNetResponse<AccountPayload>(HttpStatusCode.Unauthorized);

            return new JsonNetResponse<AccountPayload>(payload);
        }

        [FunctionName(nameof(Logout))]
        [OpenApiOperation(operationId: "Logout", tags: new[] { "User Account" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AccountPayload), Description = "User Login Account ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AccountPayload))]
        public static async Task<JsonNetResponse<AccountPayload>> Logout(
  [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "account/Logout")] HttpRequest req,
  ILogger log, ExecutionContext context, ClaimsPrincipal claimsPrincipal)
        {
            var payload = await req.GetParameters<AccountPayload>(true);
            payload.ClaimsPrincipal = claimsPrincipal;
            payload.ClaimsPrincipal = AccountApiService.EngineerClaimsPrincipal(payload);

            await AccountApiService.Logout(payload);
            return new JsonNetResponse<AccountPayload>(payload);
        }

    }
}


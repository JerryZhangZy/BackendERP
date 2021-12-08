using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.inventorySync;
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
namespace DigitBridge.CommerceCentral.ERP.Integration.Api.Api
{
    /// <summary>
    /// 
    /// </summary>
    [ApiFilter(typeof(CommerceCentralSyncProductApi))]
    public static class CommerceCentralSyncProductApi
    {
        [FunctionName(nameof(SyncFromProductBasic))]
        [OpenApiOperation(operationId: "SyncFromProductBasic", tags: new[] { "ProductExts" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExistSKUPayload))]
        public static async Task SyncFromProductBasic([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "syncProducts")] HttpRequest req)
        {
            var masterAccountNum = req.GetHeaderValue("masterAccountNum").ToInt();
            var profileNum = req.GetHeaderValue("profileNum").ToInt();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var uuids = dbFactory.Db.Query<string>(
@$"SELECT pb.ProductUuid FROM ProductBasic pb LEFT JOIN ProductExt pe ON pb.ProductUuid=pe.ProductUuid
WHERE pb.MasterAccountNum={masterAccountNum} AND pb.ProfileNum={profileNum} AND pe.ProductUuid IS NULL");
            foreach (var uuid in uuids)
                await QueueUniversal<ERPQueueMessage>.SendMessageAsync(ERPQueueSetting.ERPSyncProductQueue, MySingletonAppSetting.AzureWebJobsStorage, new ERPQueueMessage()
                {
                    //DatabaseNum = payload.DatabaseNum,
                    MasterAccountNum = masterAccountNum,
                    ProfileNum = profileNum,
                    ProcessUuid = uuid,
                });
        }
    }
}

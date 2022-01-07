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
    [ApiFilter(typeof(WMSInventorySyncApi))]
    public static class WMSInventorySyncApi
    {
        /// <summary>
        ///Sync Inventory
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(InventorySync))]
        [OpenApiOperation(operationId: "InventorySync", tags: new[] { "WMSInventorySyncs" }, Summary = "Sync Inventory")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventorySyncItem[]), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventorySyncPayloadAdd))]
        public static async Task<JsonNetResponse<InventorySyncUpdatePayload>> InventorySync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/inventory")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InventorySyncUpdatePayload>();
            payload.InventorySyncItems = await req.GetBodyObjectAsync<IList<InventorySyncItem>>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InventoryUpdateManager(dataBaseFactory);
            var items = srv.GetUpdateStockByList(payload);
            if (items != null && items.Count > 0)
            {
                var items20 = new List<InventoryUpdateItems>();
                foreach (var item in items)
                {
                    items20.Add(item);
                    if (items20.Count >= 20)
                    {
                        await QueueUniversal<ERPQueueMessage>.SendMessageAsync(ERPQueueSetting.ERPSyncInventoryByWmsQueue, MySingletonAppSetting.AzureWebJobsStorage, new ERPQueueMessage
                        {
                            //ERPEventType = (ErpEventType)erpdata.ERPEventType,
                            DatabaseNum = payload.DatabaseNum,
                            MasterAccountNum = payload.MasterAccountNum,
                            ProfileNum = payload.ProfileNum,
                            //ProcessUuid = erpdata.ProcessUuid,
                            ProcessData = JsonConvert.SerializeObject(items20),
                            //ProcessSource = erpdata.ProcessSource,
                            //EventUuid = erpdata.EventUuid,
                        });
                        items20.Clear();
                    }
                }
                if (items20.Count > 0)
                    await QueueUniversal<ERPQueueMessage>.SendMessageAsync(ERPQueueSetting.ERPSyncInventoryByWmsQueue, MySingletonAppSetting.AzureWebJobsStorage, new ERPQueueMessage
                    {
                        //ERPEventType = (ErpEventType)erpdata.ERPEventType,
                        DatabaseNum = payload.DatabaseNum,
                        MasterAccountNum = payload.MasterAccountNum,
                        ProfileNum = payload.ProfileNum,
                        //ProcessUuid = erpdata.ProcessUuid,
                        ProcessData = JsonConvert.SerializeObject(items20),
                        //ProcessSource = erpdata.ProcessSource,
                        //EventUuid = erpdata.EventUuid,
                    });
                payload.Success = true;
                payload.Messages = srv.Messages;
            }
            else
            {
                payload.Success = false;
                payload.Messages = srv.Messages;
            }
            return new JsonNetResponse<InventorySyncUpdatePayload>(payload);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPApiSDK;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.Log;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(InventoryBroker))]
    public static class InventoryBroker
    {
        [FunctionName("SyncProductFromProductBasic")]
        public static async Task SyncProductFromProductBasic([QueueTrigger("erp-sync-product-from-productbasic-queue")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new InventoryPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new InventoryService(dbFactory);
                var productUuid = message.ProcessUuid;
                var rowNum = await svc.GetRowNumByProductFindAsync(new ProductFindClass()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum,
                    ProductUuid = productUuid
                });
                await svc.EditAsync(rowNum);
                var data = svc.Data;
                svc.DetachData(null);
                var result = await svc.AddInventoryForExistProductAsync(data);
            }
            catch (Exception e)
            {
                var reqInfo = new Dictionary<string, object>
                {
                    { "QueueFunctionName", "SyncProductFromProductBasic" },
                    { "QueueMessage", myQueueItem }
                };
                LogCenter.CaptureException(e, reqInfo);
            }
        }

        [FunctionName("SyncInventoryByWms")]
        public static async Task SyncInventoryByWms([QueueTrigger("erp-sync-inventory-by-wms")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            try
            {
                ERPQueueMessage message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
                var payload = new InventoryPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum
                };
                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var svc = new InventoryUpdateService(dbFactory);
                var items = JsonConvert.DeserializeObject<List<InventoryUpdateItems>>(message.ProcessData);
                await svc.AddCountAsync(items, message.MasterAccountNum, message.ProfileNum);
            }
            catch (Exception e)
            {
                var reqInfo = new Dictionary<string, object>
                {
                    { "QueueFunctionName", "SyncInventoryByWms" },
                    { "QueueMessage", myQueueItem }
                };
                LogCenter.CaptureException(e, reqInfo);
            }
        }
    }
}

//using DigitBridge.Base.Utility;
//using DigitBridge.CommerceCentral.ApiCommon;
//using DigitBridge.CommerceCentral.ERPDb;
//using DigitBridge.CommerceCentral.ERPMdl;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
//using System;
//using System.IO;
//using System.Net;
//using System.Threading.Tasks;

//namespace DigitBridge.CommerceCentral.ERPBroker
//{
//    [ApiFilter(typeof(PoReceiveBroker))]
//    public static class PoReceiveBroker
//    {

//        [FunctionName("CreatePoReceiveByWMS")]
//        public static async Task CreatePoReceiveByWMS([QueueTrigger(QueueName.Erp_Create_PoReceive_By_WMS)] string myQueueItem, ILogger log)
//        {
//            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
//            var payload = new PoReceivePayload()
//            {
//                MasterAccountNum = message.MasterAccountNum,
//                ProfileNum = message.ProfileNum,
//            };

//            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

//            var service = new PoReceiveManager(dbFactory);
//            await service.CreatePoTransByQueueTriggerAsync(payload, message.ProcessUuid);
//        }

//    }
//}

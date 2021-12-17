using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(ShipmentBroker))]
    public static class ImportBroker
    {
        [FunctionName("ImportCustomer")]
        public static async Task ImportCustomer([QueueTrigger(QueueName.Erp_Import_Customer)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new CustomerIOManager(dbFactory);
            await service.ImportAsync(payload);
        }
        //[FunctionName("ImportVendor")]
        //public static async Task ImportVendor([QueueTrigger(QueueName.Erp_Import_Vendor)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ImportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new VendorIOManager(dbFactory);
        //    await service.ImportAsync(payload);
        //}


        /// <summary>
        /// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to salesorder data.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ImportSalesOrderFiles")]
        public static async Task ImportSalesOrderFiles([QueueTrigger(QueueName.Erp_Import_SalesOrder)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new SalesOrderIOManager(dbFactory);
            await service.ImportAsync(payload);
        }
    }
}

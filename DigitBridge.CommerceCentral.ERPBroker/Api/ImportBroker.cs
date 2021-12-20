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
        [FunctionName("ImportVendor")]
        public static async Task ImportVendor([QueueTrigger(QueueName.Erp_Import_Vendor)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new VendorIOManager(dbFactory);
            await service.ImportAsync(payload);
        }

        [FunctionName("ImportPurchaseOrder")]
        public static async Task ImportPurchaseOrder([QueueTrigger(QueueName.Erp_Import_PurchaseOrder)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new PurchaseOrderIOManager(dbFactory);
            await service.ImportAsync(payload);
        }


        [FunctionName("ImportPoReceive")]
        public static async Task ImportPoReceive([QueueTrigger(QueueName.Erp_Import_PoReceive)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new PoReceiveIOManager(dbFactory);
            await service.ImportAsync(payload);
        }


        [FunctionName("ImportWarehouseTransfer")]
        public static async Task ImportWarehouseTransfer([QueueTrigger(QueueName.Erp_Import_WarehouseTransfer)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new WarehouseTransferIOManager(dbFactory);
            await service.ImportAsync(payload);
        }



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
            var success = await service.ImportAsync(payload);
            // TODO if false write error to log.
        }

        /// <summary>
        /// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to inventory data.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ImportInventoryFiles")]
        public static async Task ImportInventoryFiles([QueueTrigger(QueueName.Erp_Import_Inventory)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new InventoryIOManager(dbFactory);
            var success = await service.ImportAsync(payload);
            // TODO if false write error to log.
        }

        /// <summary>
        /// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to inventoryupdate data.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ImportInventoryUpdateFiles")]
        public static async Task ImportInventoryUpdateFiles([QueueTrigger(QueueName.Erp_Import_InventoryUpdate)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new InventoryUpdateIOManager(dbFactory);
            var success = await service.ImportAsync(payload);
            // TODO if false write error to log.
        }

        /// <summary>
        /// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to invoicepayment data.
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ImportInvoicePaymentFiles")]
        public static async Task ImportInvoicePaymentFiles([QueueTrigger(QueueName.Erp_Import_InvoicePayment)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ImportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new InvoicePaymentIOManager(dbFactory);
            var success = await service.ImportAsync(payload);
            // TODO if false write error to log.
        }
    }
}

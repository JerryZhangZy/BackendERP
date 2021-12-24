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
    [ApiFilter(typeof(ExportBroker))]
    public static class ExportBroker
    {
        //[FunctionName("ExportCustomer")]
        //public static async Task ExportCustomer([QueueTrigger(QueueName.Erp_Export_Customer)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new CustomerIOManager(dbFactory);
        //    await service.ExportAsync(payload);
        //}

        //[FunctionName("ExportVendor")]
        //public static async Task ExportVendor([QueueTrigger(QueueName.Erp_Export_Vendor)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new VendorIOManager(dbFactory);
        //    await service.ExportAsync(payload);
        //}

        [FunctionName("ExportWarehouseTransfer")]
        public static async Task ExportWarehouseTransfer([QueueTrigger(QueueName.Erp_Export_WarehouseTransfer)] string myQueueItem, ILogger log)
        {
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            var payload = new ImportExportFilesPayload()
            {
                MasterAccountNum = message.MasterAccountNum,
                ProfileNum = message.ProfileNum,
                ExportUuid = message.ProcessUuid,
            };
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var service = new WarehouseTransferIOManager(dbFactory);
            await service.ExportAsync(payload);
        }


        //[FunctionName("ExportPurchaseOrder")]
        //public static async Task ExportPurchaseOrder([QueueTrigger(QueueName.Erp_Export_PurchaseOrder)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new PurchaseOrderIOManager(dbFactory);
        //    await service.ExportAsync(payload);
        //}


        //[FunctionName("ExportPoReceive")]
        //public static async Task ExportPoReceive([QueueTrigger(QueueName.Erp_Export_PoReceive)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new PoReceiveIOManager(dbFactory);
        //    await service.ExportAsync(payload);
        //}


        //[FunctionName("ExportWarehouseTransfer")]
        //public static async Task ExportWarehouseTransfer([QueueTrigger(QueueName.Erp_Export_WarehouseTransfer)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new WarehouseTransferIOManager(dbFactory);
        //    await service.ExportAsync(payload);
        //}



        ///// <summary>
        ///// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to salesorder data.
        ///// </summary>
        ///// <param name="myQueueItem"></param>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //[FunctionName("ExportSalesOrderFiles")]
        //public static async Task ExportSalesOrderFiles([QueueTrigger(QueueName.Erp_Export_SalesOrder)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new SalesOrderIOManager(dbFactory);
        //    var success = await service.ExportAsync(payload);
        //    // TODO if false write error to log.
        //}

        ///// <summary>
        ///// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to inventory data.
        ///// </summary>
        ///// <param name="myQueueItem"></param>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //[FunctionName("ExportInventoryFiles")]
        //public static async Task ExportInventoryFiles([QueueTrigger(QueueName.Erp_Export_Inventory)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new InventoryIOManager(dbFactory);
        //    var success = await service.ExportAsync(payload);
        //    // TODO if false write error to log.
        //}

        ///// <summary>
        ///// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to inventoryupdate data.
        ///// </summary>
        ///// <param name="myQueueItem"></param>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //[FunctionName("ExportInventoryUpdateFiles")]
        //public static async Task ExportInventoryUpdateFiles([QueueTrigger(QueueName.Erp_Export_InventoryUpdate)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new InventoryUpdateIOManager(dbFactory);
        //    var success = await service.ExportAsync(payload);
        //    // TODO if false write error to log.
        //}

        ///// <summary>
        ///// Receive message from queue, then download files from blob by processuuid of message, finally transfer the files to invoicepayment data.
        ///// </summary>
        ///// <param name="myQueueItem"></param>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //[FunctionName("ExportInvoicePaymentFiles")]
        //public static async Task ExportInvoicePaymentFiles([QueueTrigger(QueueName.Erp_Export_InvoicePayment)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new ImportExportFilesPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //        ExportUuid = message.ProcessUuid,
        //    };
        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //    var service = new InvoicePaymentIOManager(dbFactory);
        //    var success = await service.ExportAsync(payload);
        //    // TODO if false write error to log.
        //}
    }
}

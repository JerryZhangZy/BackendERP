using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(ImportFilesApi))]
    public static class ImportFilesApi
    {

        [FunctionName(nameof(ImportCustomerFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportCustomer", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportCustomerFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/customer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportCustomer);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }




        [FunctionName(nameof(ImportVendorFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportVendor", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportVendorFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/vendor")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportVendor);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }
        
        [FunctionName(nameof(ImportPurchaseOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportPurchaseOrder", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportPurchaseOrderFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/purchaseOrder")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportPurchaseOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }






        [FunctionName(nameof(ImportPoReceiveFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportPoReceive", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportPoReceiveFiles(
    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/poReceive")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportPoReceive);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }



        [FunctionName(nameof(ImportWarehouseTransferFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportWarehouseTransfer", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportWarehouseTransferFiles(
         [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/warehouseTransfer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportWarehouseTransfer);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }





        [FunctionName(nameof(ImportSalesOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportSalesOrderFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportSalesOrderFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportSalesOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ImportInvoiceFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportInvoiceFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportInvoiceFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/invoice")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportInvoice);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ImportShipmentFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportShipmentFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportShipmentFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/shipment")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportShipment);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ImportInventoryFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportInventoryFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportInventoryFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/inventory")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportInventory);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ImportInventoryUpdateFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportInventoryUpdateFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportInventoryUpdateFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/inventoryUpdate")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportInventoryUpdate);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ImportInvoicePaymentFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ImportInvoicePaymentFiles", tags: new[] { "ImportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ImportInvoicePaymentFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "importFiles/invoicePayment")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            payload.LoadRequest(req);

            var svc = new ImportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpImportInvoicePayment);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }
    }
}


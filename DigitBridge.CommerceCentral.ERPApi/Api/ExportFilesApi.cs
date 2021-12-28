using DigitBridge.Base.Common;
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
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Export files
    /// </summary> 
    [ApiFilter(typeof(ExportFilesApi))]
    public static class ExportFilesApi
    {

        [FunctionName(nameof(ExportSalesOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportSalesOrderFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ExportSalesOrderPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportSalesOrderFiles(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/salesorder")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);

            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportSalesOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }

        [FunctionName(nameof(ExportCustomerFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportCustomerFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportCustomerFiles(
      [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/customer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportCustomer);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }

        [FunctionName(nameof(ExportVendorFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportVendorFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportVendorFiles(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/vendor")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportVendor);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }


        [FunctionName(nameof(ExportWarehouseTransferFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportWarehouseTransferFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportWarehouseTransferFiles(
[HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/warehouseTransfer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportWarehouseTransfer);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }



        [FunctionName(nameof(ExportPurchaseOrderFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportPurchaseOrderFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportPurchaseOrderFiles(
[HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/purchaseOrder")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportPurchaseOrder);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }






        [FunctionName(nameof(ExportPoReceiveFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportPoReceiveFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportPoReceiveFiles(
    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/poReceive")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportPoReceive);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }


        [FunctionName(nameof(ExportInvoiceFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportInvoiceFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ExportSalesOrderPayload), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportInvoiceFiles(
          [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/invoice")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);

            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportInvoice);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);
        }



        [FunctionName(nameof(ExportShipmentFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "ExportShipmentFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/file", bodyType: typeof(ImportExportFilesPayload), Description = "type form data,key=File,value=Files")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<JsonNetResponse<ImportExportFilesPayload>> ExportShipmentFiles(
[HttpTrigger(AuthorizationLevel.Function, "POST", Route = "exportFiles/customer")] HttpRequest req)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>(true);
            //payload.LoadRequest(req);
            var svc = new ExportManger();
            payload.Success = await svc.SendToBlobAndQueue(payload, ErpEventType.ErpExportShipment);
            payload.Messages = svc.Messages;

            return new JsonNetResponse<ImportExportFilesPayload>(payload);


        }



        [FunctionName(nameof(GetExportFiles))]
        #region swagger Doc
        [OpenApiOperation(operationId: "GetExportFiles", tags: new[] { "ExportFiles" })]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "processUuid", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "processUuid", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ImportExportFilesPayload))]
        #endregion swagger Doc
        public static async Task<FileContentResult> GetExportFiles(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "exportFiles/result/{processUuid}")] HttpRequest req, string processUuid)
        {
            var payload = await req.GetParameters<ImportExportFilesPayload>();
            payload.ExportUuid = processUuid;

            var svc = new ExportBlobService();
            return await svc.DownloadFileFromBlobAsync(payload);
        }
    }
}


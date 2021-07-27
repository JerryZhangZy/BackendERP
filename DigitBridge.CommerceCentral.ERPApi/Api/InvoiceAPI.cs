using DigitBridge.CommerceCentral.ApiCommon;
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
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process invoice
    /// </summary> 
    [ApiFilter(typeof(InvoiceApi))]
    public static class InvoiceApi
    {
        /// <summary>
        /// Get one invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetInvoice", tags: new[] { "Invoices" }, Summary = "Get one/multiple invoices")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        [FunctionName(nameof(GetInvoice))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/{invoiceNumber}")] HttpRequest req,
            ILogger log,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.GetByInvoiceNumberAsync(invoiceNumber);
            if (success)
            {
                payload.ResponseData = srv.ToDto(srv.Data);
            }
            else
                payload.ResponseData = "no record found";
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Get list by search criteria
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [OpenApiOperation(operationId: "GetInvoiceList", tags: new[] { "Invoices" }, Summary = "Get list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload), Description = "Result is List<SalesOrderDataDto>")]
        [FunctionName(nameof(GetInvoiceList))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            //todo
            payload.ReqeustData = "This api isn't implemented";
            //payload = await srv.GetListBySalesOrderUuidsNumberAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Delete invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "DeleteInvoices", tags: new[] { "Invoices" }, Summary = "Delete one invoice")]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Sales invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        [FunctionName(nameof(DeleteInvoices))]
        public static async Task<JsonNetResponse<InvoicePayload>> DeleteInvoices(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoices/{invoiceNumber}")] HttpRequest req,
           string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum); 
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.DeleteByInvoiceNumberAsync(invoiceNumber);
            payload.ResponseData = $"{success} to delete ";
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        ///  Update invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoices))]
        [OpenApiOperation(operationId: "UpdateInvoices", tags: new[] { "Invoices" }, Summary = "Update one invoice")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        public static async Task<JsonNetResponse<InvoicePayload>> UpdateInvoices(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderDataDto, InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.UpdateAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to update data";
            return new JsonNetResponse<InvoicePayload>(payload);
        }
        /// <summary>
        /// Add invoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddInvoices))]
        [OpenApiOperation(operationId: "AddInvoices", tags: new[] { "Invoices" }, Summary = "Add one invoice")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayload))]
        public static async Task<JsonNetResponse<InvoicePayload>> AddInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<SalesOrderDataDto, InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload.MasterAccountNum);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.AddAsync(payload.ReqeustData);
            payload.ResponseData = $"{success} to add data, the uuid is:{srv.Data.UniqueId}";
            return new JsonNetResponse<InvoicePayload>(payload);
        }
    }
}

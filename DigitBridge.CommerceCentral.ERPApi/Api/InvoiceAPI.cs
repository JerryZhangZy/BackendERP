using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
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
        [FunctionName(nameof(GetInvoice))]
        [OpenApiOperation(operationId: "GetInvoice", tags: new[] { "Invoices" }, Summary = "Get one/multiple invoices")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetSingle))]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoice(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/{invoiceNumber}")] HttpRequest req,
            ILogger log,
            string invoiceNumber)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.GetByInvoiceNumberAsync(invoiceNumber, payload);
            if (success)
            {
                payload.Invoice = srv.ToDto(srv.Data);
            }
            else
                payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Get list by search criteria
        /// </summary>
        /// <param name="req"></param> 

        /// <returns></returns>
        [FunctionName(nameof(GetInvoiceList))]
        [OpenApiOperation(operationId: "GetInvoiceList", tags: new[] { "Invoices" }, Summary = "Get list")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadGetMultiple), Description = "Result is List<InvoiceDataDto>")]
        public static async Task<JsonNetResponse<InvoicePayload>> GetInvoiceList(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            //todo
            //payload.ReqeustData = "This api isn't implemented";
            //payload = await srv.GetListByInvoiceUuidsNumberAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Delete invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        [FunctionName(nameof(DeleteInvoices))]
        [OpenApiOperation(operationId: "DeleteInvoices", tags: new[] { "Invoices" }, Summary = "Delete one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "rowNum", In = ParameterLocation.Path, Required = true, Type = typeof(long), Summary = "invoice rowNum", Description = "Sales invoice rowNum. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadDelete))]
        public static async Task<JsonNetResponse<InvoicePayload>> DeleteInvoices(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoices/{rowNum}")] HttpRequest req,
           long rowNum)
        {
            var payload = await req.GetParameters<InvoicePayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.DeleteByInvoiceUuidAsync(payload, rowNum);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        ///  Update invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoices))]
        [OpenApiOperation(operationId: "UpdateInvoices", tags: new[] { "Invoices" }, Summary = "Update one invoice")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadUpdate))]
        public static async Task<JsonNetResponse<InvoicePayload>> UpdateInvoices(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.UpdateAsync(payload);
            payload.Messages = srv.Messages;
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
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayload>> AddInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceService(dataBaseFactory);
            payload.Success = await srv.AddAsync(payload);
            payload.Messages = srv.Messages;
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Load customer list
        /// </summary>
        [FunctionName(nameof(InvoicesList))]
        [OpenApiOperation(operationId: "InvoicesList", tags: new[] { "Invoices" }, Summary = "Load invoices list data")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoicePayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayload>> InvoicesList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<InvoicePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new InvoiceList(dataBaseFactory, new InvoiceQuery());
            payload = await srv.GetInvoiceListAsync(payload);
            return new JsonNetResponse<InvoicePayload>(payload);
        }

        /// <summary>
        /// Add invoice
        /// </summary>
        [FunctionName(nameof(Sample_Invocies_Post))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvociesSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadAdd))]
        public static async Task<JsonNetResponse<InvoicePayloadAdd>> Sample_Invocies_Post(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePayloadAdd>(InvoicePayloadAdd.GetSampleData());
        }

        [FunctionName(nameof(Sample_Invoice_Find))]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiOperation(operationId: "InvoiceFindSample", tags: new[] { "Sample" }, Summary = "Get new sample of invoice find")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InvoicePayloadFind))]
        public static async Task<JsonNetResponse<InvoicePayloadFind>> Sample_Invoice_Find(
           [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "sample/POST/invoices/find")] HttpRequest req)
        {
            return new JsonNetResponse<InvoicePayloadFind>(InvoicePayloadFind.GetSampleData());
        }
    }
}


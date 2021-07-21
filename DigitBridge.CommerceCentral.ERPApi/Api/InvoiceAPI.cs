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
    [ApiFilter(typeof(InvoiceAPI))]
    public static class InvoiceAPI
    {
        /// <summary>
        /// Get invoices
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "GetInvoices", tags: new[] { "Invoices" }, Summary = "Get one/multiple invoices")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$top", In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = "$top", Description = "Page size. Default value is 100. Maximum value is 100.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$skip", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$skip", Description = "Records to skip. https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$count", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Summary = "$count", Description = "Valid value: true, false. When $count is true, return total count of records, otherwise return requested number of data.", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "$sortBy", In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = "$sortBy", Description = "sort by. Default order by LastUpdateDate. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = false, Type = typeof(string), Summary = "invoiceNumber", Description = "Invoice Number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<InvoiceDataDto>))]
        [FunctionName(nameof(GetInvoices))]
        public static async Task<IActionResult> GetInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "invoices/{invoiceNumber}")] HttpRequest req,
            ILogger log,
            string invoiceNumber)
        {
            //var invoiceNumber = req.GetRouteObject<string>("invoiceNumber");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.GetByInvoiceNumberAsync(invoiceNumber);
            InvoiceDataDto dto = null;
            if (success)
            {
                dto = srv.ToDto(srv.Data);
            }
            return new Response<InvoiceDataDto>(dto, success);
        }

        /// <summary>
        /// Delete invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        [OpenApiOperation(operationId: "DeleteInvoices", tags: new[] { "Invoices" }, Summary = "Delete one invoice")]
        [OpenApiParameter(name: "invoiceNumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "invoiceNumber", Description = "Sales invoice number. ", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        [FunctionName(nameof(DeleteInvoices))]
        public static async Task<IActionResult> DeleteInvoices(
           [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "invoices/{invoiceNumber}")]
           string invoiceNumber)
        {
            //var invoiceNumber = req.GetRouteObject<string>("invoiceNumber");
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.DeleteByInvoiceNumberAsync(invoiceNumber);
            return new Response<string>("Delete invoice result", success);
        }

        /// <summary>
        ///  Update invoice 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(UpdateInvoices))]
        [OpenApiOperation(operationId: "UpdateInvoices", tags: new[] { "Invoices" }, Summary = "Update one invoice")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InvoiceDataDto), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> UpdateInvoices(
[HttpTrigger(AuthorizationLevel.Function, "patch", Route = "invoices")] HttpRequest req,
[FromBodyBinding] InvoiceDataDto dto)
        {
            //var dto = await req.GetBodyObjectAsync<InvoiceDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.UpdateAsync(dto);
            return new Response<string>("Update invoice result", success);
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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<string>))]
        public static async Task<IActionResult> AddInvoices(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "invoices")] HttpRequest req,
            [FromBodyBinding] InvoiceDataDto dto)
        {
            //var dto = await req.GetBodyObjectAsync<InvoiceDataDto>();
            var dataBaseFactory = new DataBaseFactory(ConfigHelper.Dsn);
            var srv = new InvoiceService(dataBaseFactory);
            var success = await srv.AddAsync(dto);
            return new Response<string>($"new invoice uuid is:{srv.Data.UniqueId}", success);
        }
    }
}


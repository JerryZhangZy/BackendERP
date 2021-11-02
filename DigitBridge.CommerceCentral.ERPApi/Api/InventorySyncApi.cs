using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi.OpenApiModel;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.inventorySync;
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
using Newtonsoft.Json;
namespace DigitBridge.CommerceCentral.ERPApi.Api
{
    [ApiFilter(typeof(InventoryUpdateApi))]
    public static class InventoryUpdateApi
    {
        /// <summary>
        /// Add ApInvoice
        /// </summary>
        /// <param name="req"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [FunctionName(nameof(InvoiceSync))]
        [OpenApiOperation(operationId: "InvoiceSync", tags: new[] { "InventoryUpdates" }, Summary = "Sync Inventory")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InventorySyncPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(InventorySyncPayloadAdd))]
        public static async Task<JsonNetResponse<InventorySyncUpdatePayload>> InvoiceSync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "apInvoices")] Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var payload = await req.GetParameters<InventorySyncUpdatePayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new ApInvoiceService(dataBaseFactory);
            //payload.Success = await srv.AddAsync(payload);
            //payload.Messages = srv.Messages;
            //payload.ApInvoice = srv.ToDto();

            // if (payload.Success)
            //srv.AddQboApInvoiceEventAsync(payload.MasterAccountNum, payload.ProfileNum);

            return new JsonNetResponse<InventorySyncUpdatePayload>(null);
        }
    }
}

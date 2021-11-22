using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPApi;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api.Api
{
    public class PurchaseOrderIntegrationApi
    {
        
        /// <summary>
        /// Get purchase order list by criteria.
        /// </summary>
        [FunctionName(nameof(PurchaseOrderList))]
        [OpenApiOperation(operationId: "PurchaseOrderList", tags: new[] { "WMSPurchaseOrders" }, Summary = "Get purchase order list by criteria.")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PurchaseOrderPayloadFind))]
        public static async Task<JsonNetResponse<PurchaseOrderPayload>> PurchaseOrderList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/purchaseOrders/find")] HttpRequest req)
        {
            var payload = await req.GetParameters<PurchaseOrderPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
            var srv = new WmsPurchaseOrderList(dataBaseFactory, new WmsPurchaseOrderQuery());
            await srv.GetPurchaseOrderListAsync(payload);
            return new JsonNetResponse<PurchaseOrderPayload>(payload);
        }

        //// <summary>
        /// Add Shipment list
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(PoReceive))]
        [OpenApiOperation(operationId: "PoReceive", tags: new[] { "WMSPurchaseOrders" }, Summary = "Add WMS po received items to ERP")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WMSPoReceiveItem[]), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WmsPoReceivePayload[]))]
        public static async Task<JsonNetResponse<IList<WmsPoReceivePayload>>> PoReceive(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/purchaseOrders/receive")] HttpRequest req)
        {
            var receiveItems = await req.GetBodyObjectAsync<IList<WMSPoReceiveItem>>();
            var payload = await req.GetParameters<PoReceivePayload>();

            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            var service = new PoReceiveService(dataBaseFactory);
            var result = await service.AddListAsync(payload, receiveItems);

            return new JsonNetResponse<IList<WmsPoReceivePayload>>(result);
        }
        
    }
}
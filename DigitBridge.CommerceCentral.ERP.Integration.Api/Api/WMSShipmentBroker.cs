using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERP.Integration.Api
{
    [ApiFilter(typeof(WMSShipmentBroker))]
    public static class WMSShipmentBroker
    {
        /// <summary>
        /// Add single WMS shipment to ERP
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(AddSingleShipment))]
        [OpenApiOperation(operationId: "AddSingleShipment", tags: new[] { "Shipments" }, Summary = "Add single WMS shipment to ERP")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InputOrderShipmentType), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WmsOrderShipmentPayload))]
        public static async Task<JsonNetResponse<WmsOrderShipmentPayload>> AddSingleShipment(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments/single")] HttpRequest req)
        {
            var wmsShipment = await req.GetBodyObjectAsync<InputOrderShipmentType>();
            var payload = await req.GetParameters<OrderShipmentPayload>();
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            var shipmentManager = new OrderShipmentManager(dataBaseFactory);
            var result = await shipmentManager.CreateShipmentAsync(payload, wmsShipment);
            result.Messages = shipmentManager.Messages;

            return new JsonNetResponse<WmsOrderShipmentPayload>(result);
        } 
    }
}

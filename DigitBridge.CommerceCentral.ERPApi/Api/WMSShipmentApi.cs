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

namespace DigitBridge.CommerceCentral.ERPApi
{

    /// <summary>
    /// Process salesorder
    /// </summary> 
    [ApiFilter(typeof(WMSShipmentApi))]
    public static class WMSShipmentApi
    {

        [FunctionName(nameof(ResendWMSShipmentToERP))]
        [OpenApiOperation(operationId: "ResendWMSShipmentToERP", tags: new[] { "WMSShipments" }, Summary = "Resend wms shipment list to erp by wms shipment id")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string),
        Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(WMSShipmentPayloadResendRequest), Required = true, Description = "Array of wms shipment id.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
        bodyType: typeof(WMSShipmentPayloadResendResponse))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> ResendWMSShipmentToERP(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "wmsShipments/resendWMSShipmentToERP")]
            HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var svc = new IntegrationWMSShipmentApi();
            await svc.ReSendWMSShipmentToErpAsync(payload);
            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }
    }
}


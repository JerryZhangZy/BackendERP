using DigitBridge.Base.Utility;
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
    [ApiFilter(typeof(WMSShipmentApi))]
    public static class WMSShipmentApi
    {
        //// <summary>
        /// Add Shipment list
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(CreateShipmentList))]
        [OpenApiOperation(operationId: "CreateShipmentList", tags: new[] { "WMSShipments" }, Summary = "Add WMS shipment list to ERP")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InputOrderShipmentType[]), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentCreateResultPayload[]))]
        public static async Task<JsonNetResponse<IList<OrderShipmentCreateResultPayload>>> CreateShipmentList(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/shipments")] HttpRequest req)
        {
            var inputShipments = await req.GetBodyObjectAsync<IList<InputOrderShipmentType>>();
            var payload = await req.GetParameters<OrderShipmentPayload>();

            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            var shipmentManager = new OrderShipmentManager(dataBaseFactory, MySingletonAppSetting.IntegrationStorage);
            var result = await shipmentManager.CreateShipmentListAsync(payload, inputShipments);

            return new JsonNetResponse<IList<OrderShipmentCreateResultPayload>>(result);
        }
    }
}

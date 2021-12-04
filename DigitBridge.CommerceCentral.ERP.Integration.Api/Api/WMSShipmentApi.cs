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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
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

            var shipmentManager = new OrderShipmentManager(dataBaseFactory, MySingletonAppSetting.AzureWebJobsStorage);
            var result = await shipmentManager.CreateShipmentListAsync(payload, inputShipments);

            return new JsonNetResponse<IList<OrderShipmentCreateResultPayload>>(result);
        }

        //// <summary>
        /// Add Shipment list
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(GetWMSShipmentListAsync))]
        [OpenApiOperation(operationId: "GetWMSShipmentListAsync", tags: new[] { "WMSShipments" }, Summary = "Get WMS shipment list  ")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(string[]), Required = true, Description = "Array of WMS ShipmentID")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(StringBuilder))]
        public static async Task<JsonNetResponse<StringBuilder>> GetWMSShipmentListAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "wms/shipments/find")] HttpRequest req)
        {
            var shipmentIDs = await req.GetBodyObjectAsync<IList<string>>();
            var masterAccountNum = req.MasterAccountNum();
            var profileNum = req.ProfileNum();

            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(masterAccountNum);
            var wmsListService = new WMSOrderShipmentList(dataBaseFactory);

            var result = await wmsListService.GetWMSShipmentListAsync(masterAccountNum, profileNum, shipmentIDs);

            return new JsonNetResponse<StringBuilder>(result);
        }

    }
}

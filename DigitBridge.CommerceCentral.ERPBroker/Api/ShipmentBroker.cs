using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(ShipmentBroker))]
    public static class ShipmentBroker
    {
        ///// <summary>
        ///// CreateShipment
        ///// </summary>
        ///// <param name="req"></param>
        ///// <returns></returns>
        //[FunctionName(nameof(CreateShipment))]
        //[OpenApiOperation(operationId: "CreateShipment", tags: new[] { "Shipments" }, Summary = "Add one input order shipment to erp")]
        //[OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        //[OpenApiRequestBody(contentType: "application/json", bodyType: typeof(InputOrderShipmentType), Description = "Request Body in json format")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayload))]
        //public static async Task<JsonNetResponse<OrderShipmentPayload>> CreateShipment(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req)
        //{
        //    var inputShipment = await req.GetBodyObjectAsync<InputOrderShipmentType>();
        //    var payload = new OrderShipmentPayload()
        //    {
        //        OrderShipment = InputOrderShipmentMapper.MapperToErpShipment(inputShipment),
        //        MasterAccountNum = req.MasterAccountNum(),
        //        ProfileNum = req.ProfileNum(),
        //    };
        //    var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

        //    var shipmentManager = new OrderShipmentManager(dataBaseFactory);
        //    payload.Success = await shipmentManager.CreateShipmentAsync(payload);
        //    payload.Messages = shipmentManager.Messages;

        //    return new JsonNetResponse<OrderShipmentPayload>(payload);
        //}

        //[FunctionName("CreateShipmentByWMS")]
        //public static async Task CreateShipmentByWMS([QueueTrigger(QueueName.Erp_Create_Shipment_By_WMS)] string myQueueItem, ILogger log)
        //{
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    var payload = new OrderShipmentPayload()
        //    {
        //        MasterAccountNum = message.MasterAccountNum,
        //        ProfileNum = message.ProfileNum,
        //    };

        //    var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

        //    var service = new OrderShipmentManager(dbFactory);
        //    await service.CreateShipmentAsync(payload, message.ProcessUuid);
        //}

    }
}

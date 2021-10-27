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
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(ShipmentBroker))]
    public static class ShipmentBroker
    {
        //[FunctionName("CreateShipment")]
        //public static async Task CreateShipment([QueueTrigger(QueueName.Erp_Shipment_Queue)] string myQueueItem, ILogger log)
        //{
        //    var client = new ShipmentClient();
        //    var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
        //    try
        //    {
        //        var payload = new OrderShipmentPayload()
        //        {
        //            MasterAccountNum = message.MasterAccountNum,
        //            ProfileNum = message.ProfileNum,
        //            OrderShipmentUuids = new List<string>() { message.ProcessUuid }
        //        };

        //        var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
        //        var service = new OrderShipmentManager(dbFactory);
        //        (bool success, List<string> salesOrderUuids) = await service.CreateShipmentFromPayloadAsync(payload);

        //        client.SendActionResultAsync(message, service.Messages.ObjectToString(), success);
        //    }
        //    catch (Exception e)
        //    {
        //        client.SendActionResultAsync(message, e.ObjectToString());
        //    }
        //}

        /// <summary>
        /// CreateShipment
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName(nameof(CreateShipment))]
        [OpenApiOperation(operationId: "CreateShipment", tags: new[] { "Shipments" }, Summary = "Add one order shipment")]
        [OpenApiParameter(name: "masterAccountNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "MasterAccountNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "profileNum", In = ParameterLocation.Header, Required = true, Type = typeof(int), Summary = "ProfileNum", Description = "From login profile", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "API Keys", Description = "Azure Function App key", Visibility = OpenApiVisibilityType.Advanced)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd), Description = "Request Body in json format")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OrderShipmentPayloadAdd))]
        public static async Task<JsonNetResponse<OrderShipmentPayload>> CreateShipment(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shipments")] HttpRequest req)
        {
            var payload = await req.GetParameters<OrderShipmentPayload>(true);
            var dataBaseFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);

            var shipmentManager = new OrderShipmentManager(dataBaseFactory);
            payload.Success = await shipmentManager.CreateShipmentAsync(payload);
            payload.Messages = shipmentManager.Messages; 

            return new JsonNetResponse<OrderShipmentPayload>(payload);
        }
    }
}

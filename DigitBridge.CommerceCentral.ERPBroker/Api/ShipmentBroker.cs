using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPEventSDK;
using DigitBridge.CommerceCentral.ERPEventSDK.ApiClient;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.Log;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPBroker
{
    [ApiFilter(typeof(ShipmentBroker))]
    public static class ShipmentBroker
    {
        [FunctionName("CreateShipment")]
        public static async Task CreateShipment([QueueTrigger(QueueName.Erp_Shipment_Queue)] string myQueueItem, ILogger log)
        {
            var client = new ShipmentClient();
            var message = JsonConvert.DeserializeObject<ERPQueueMessage>(myQueueItem);
            try
            {
                var payload = new OrderShipmentPayload()
                {
                    MasterAccountNum = message.MasterAccountNum,
                    ProfileNum = message.ProfileNum,
                    OrderShipmentUuids = new List<string>() { message.ProcessUuid }
                };

                var dbFactory = await MyAppHelper.CreateDefaultDatabaseAsync(payload);
                var service = new OrderShipmentManager(dbFactory);
                (bool success, List<string> salesOrderUuids) = await service.CreateShipmentFromPayloadAsync(payload);

                client.SendActionResultAsync(message, service.Messages.ObjectToString(), success);
            }
            catch (Exception e)
            {
                client.SendActionResultAsync(message, e.ObjectToString());
            }
        }
    }
}

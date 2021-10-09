using Azure.Storage.Blobs;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public static class AzureUtil
    {
        public static T QueueMessageToEntity<T>(this QueueMessage message) where T : IQueueEntity
        {
            var entity= JsonConvert.DeserializeObject<T>(message.MessageText);
            entity.MessageId = message.MessageId;
            entity.PopReceipt = message.PopReceipt;
            return entity;
        }

        public static T PeekedMessageToEntity<T>(this PeekedMessage message) where T : IQueueEntity
        {
            var entity= JsonConvert.DeserializeObject<T>(message.MessageText);
            entity.MessageId = message.MessageId;
            return entity;
        }

        public static IList<string> GetAllBlobContainers(string connectionStr)
        {
            var client = new BlobServiceClient(connectionStr);
            var containerItems= client.GetBlobContainers();
            var nameList = new List<string>();
            foreach(var item in containerItems)
            {
                nameList.Add(item.Name);
            }
            return nameList;
        }
    }
}

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
    }
}

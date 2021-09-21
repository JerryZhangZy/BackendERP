using Digitbridge.QuickbooksOnline.AzureQueue.Infrastructure;
using Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages;
using DigitBridge.QuickBooksOnline.AzureQueue.QueueConnection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;

namespace Digitbridge.QuickbooksOnline.AzureQueue.QueueConnection
{
    public class QueueCommunicator
    {
        public interface IQueueCommunicator
        {
            Task AddQueueAsync<T>(T obj) where T : BaseQueueMessage;
            Task AddQueueAsync<T>(string route, T obj);
        }
        public class MsAzureQueueCommunicator : IQueueCommunicator
        {
            private readonly IMessageSerializer _messageSerializer;
            public readonly IMsAzureQueueClientFactory _msAzureQueueClient;

            public MsAzureQueueCommunicator(IMessageSerializer messageSerializer
                , QueueConfig queueConfig)
            {
                _messageSerializer = messageSerializer;
                _msAzureQueueClient = new MsAzureQueueClientFactory(queueConfig);
            }

            public async Task AddQueueAsync<T>(T obj) where T : BaseQueueMessage
            {
                try
                {
                    var queueReference = await _msAzureQueueClient.CreateQueueClient(obj.Route);
                    var serializedMessage = _messageSerializer.Serialize(obj);
                    await queueReference.AddMessageAsync(serializedMessage);
                }
                catch (Exception ex)
                {
                    throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
                }
            }
            // public async Task AddQueueAsync(string route, string serializedMessage) 
            public async Task AddQueueAsync<T>(string route, T obj)
            {
                try
                {
                    var queueReference = await _msAzureQueueClient.CreateQueueClient(route);
                    var serializedMessage = _messageSerializer.Serialize(obj);
                    await queueReference.AddMessageAsync(serializedMessage);
                }
                catch (Exception ex)
                {
                    throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
                }
            }
        }
    }
}

using Digitbridge.QuickbooksOnline.AzureQueue.Infrastructure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Azure.Storage;

namespace DigitBridge.QuickBooksOnline.AzureQueue.QueueConnection
{

    public interface IMsAzureQueueClientFactory
    {
        Task<MsAzureQueue> CreateQueueClient(string queueName);
    }

    public class MsAzureQueueClientFactory : IMsAzureQueueClientFactory
    {
        private readonly QueueConfig _queueConfig;
        private MsAzureQueue _msAzureQueue;

        public MsAzureQueueClientFactory(QueueConfig queueConfig)
        {
            _queueConfig = queueConfig;
        }

        public async Task<MsAzureQueue> CreateQueueClient(string queueName)
        {

            _msAzureQueue = await MsAzureQueue.CreateAsync(queueName
                , true, _queueConfig.AccountName
                , _queueConfig.EndpointSuffix
                , _queueConfig.AccountKey
                , _queueConfig.UseHttps
                , _queueConfig.UseManagedIdentity
                , _queueConfig.TokenProviderConnectionString
                , _queueConfig.TenantId
                );

            return _msAzureQueue;
        }
    }
}

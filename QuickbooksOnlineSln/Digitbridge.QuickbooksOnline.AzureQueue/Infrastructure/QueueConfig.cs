using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.AzureQueue.Infrastructure
{
    public class QueueConfig
    {
        public string QueueConnectionString { get; set; }
        public string AccountName { get; set; }
        public string EndpointSuffix { get; set; }
        public string AccountKey { get; set; }
        public bool UseHttps { get; set; }
        public bool UseManagedIdentity { get; set; }
        public string TokenProviderConnectionString { get; set; }
        public string TenantId { get; set; }
        public QueueConfig()
        {

        }

        public QueueConfig(string queueConnectionString, string accountName
            , string endpointSuffix, string accountKey
            , bool useHttps, bool useManagedIdentity
            , string tokenProviderConnectionString, string tenantId)
        {
            QueueConnectionString = queueConnectionString;
            AccountName = accountName;
            EndpointSuffix = endpointSuffix;
            AccountKey = accountKey;
            UseHttps = useHttps;
            UseManagedIdentity = useManagedIdentity;
            TokenProviderConnectionString = tokenProviderConnectionString;
            TenantId = tenantId;
        }
    }
}

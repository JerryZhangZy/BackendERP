using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.IntegrationApiMdl.Infrastructure
{
    public class QboDbConfig
    {
        public string QuickBooksDbConnectionString { get; set; }
        public bool UseAzureManagedIdentity { get; set; }
        public string TokenProviderConnectionString { get; set; }
        public string AzureTenantId { get; set; }
        public string QuickBooksDbOrderCentralTableName { get; set; }
        public string QuickBooksDbItemLineCentralTableName { get; set; }
        public string QuickBooksDbConnectionInfoTableName { get; set; }
        public string QuickBooksDbIntegrationSettingTableName { get; set; }
        public string QuickBooksChannelAccSettingTableName { get; set; }
        public string CryptKey { get; set; }
        
        public QboDbConfig(string dbconnString
            , bool useAzureManagedIdentity
            , string tokenProviderConnectionString
            , string azureTenantId 
            , string quickBooksDbOrderTableName
            , string quickBooksDbItemLineTableName
            , string quickBooksDbConnectionInfoTableName
            , string quickBooksDbIntegrationSettingTableName
            , string quickBooksChannelAccSettingTableName
            , string cryptKey)
        {
            QuickBooksDbConnectionString = dbconnString;
            UseAzureManagedIdentity = useAzureManagedIdentity;
            TokenProviderConnectionString = tokenProviderConnectionString;
            AzureTenantId = azureTenantId;
            QuickBooksDbOrderCentralTableName = quickBooksDbOrderTableName;
            QuickBooksDbItemLineCentralTableName = quickBooksDbItemLineTableName;
            QuickBooksDbConnectionInfoTableName = quickBooksDbConnectionInfoTableName;
            QuickBooksDbIntegrationSettingTableName = quickBooksDbIntegrationSettingTableName;
            QuickBooksChannelAccSettingTableName = quickBooksChannelAccSettingTableName;
            CryptKey = cryptKey;
        }
    }
}

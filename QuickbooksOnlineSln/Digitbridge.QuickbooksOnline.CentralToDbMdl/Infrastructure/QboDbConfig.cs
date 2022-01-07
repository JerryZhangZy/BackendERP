using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl
{
    public class QboDbConfig
    {
        public string QuickBooksDbConnectionString { get; set; }
        public bool UseAzureManagedIdentity { get; set; }
        public string AzureTenantId { get; set; }
        public string TokenProviderConnectionString { get; set; }
        public string QuickBooksDbOrderTableName { get; set; }
        public string QuickBooksDbItemLineTableName { get; set; }
        public string QuickBooksIntegrationSettingTableName { get; set; }


        public QboDbConfig(string dbconnString, 
            bool useAzureManagedIdentity, 
            string azureTenentId, 
            string tokenProviderConnectionString,
            string quickBooksDbOrderTableName,
            string quickBooksDbItemLineTableName,
            string quickBooksIntegrationSettingTableName)
        {
            QuickBooksDbConnectionString = dbconnString;
            UseAzureManagedIdentity = useAzureManagedIdentity;
            AzureTenantId = azureTenentId;
            TokenProviderConnectionString = tokenProviderConnectionString;
            QuickBooksDbOrderTableName = quickBooksDbOrderTableName;
            QuickBooksDbItemLineTableName = quickBooksDbItemLineTableName;
            QuickBooksIntegrationSettingTableName = quickBooksIntegrationSettingTableName;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class MsSqlUniversalDBConfig
    {
        public string DbConnectionString { get; set; }
        public bool UseAzureManagedIdentity { get; set; }
        public string AccessToken { get; set; }
        public string TokenProviderConnectionString { get; set; }
        public string TenantId { get; set; }

        public MsSqlUniversalDBConfig(string dbConnString
            , bool useAzureManagedIdentity
            , string tokenProviderConnectionString
            , string tenantId = "")
        {
            DbConnectionString = dbConnString;
            UseAzureManagedIdentity = useAzureManagedIdentity;
            TokenProviderConnectionString = tokenProviderConnectionString;
            TenantId = tenantId;
        }
        public MsSqlUniversalDBConfig(string dbConnString
            , bool useAzureManagedIdentity
            , string accessToken)
        {
            DbConnectionString = dbConnString;
            UseAzureManagedIdentity = useAzureManagedIdentity;
            AccessToken = accessToken;
        }
        public MsSqlUniversalDBConfig(string dbConnString)
        {
            DbConnectionString = dbConnString;
        }
    }
}

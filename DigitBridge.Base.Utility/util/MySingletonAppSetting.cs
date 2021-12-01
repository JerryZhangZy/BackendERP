using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.Base.Utility
{
    public static class MySingletonAppSetting
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder().
                    SetBasePath(Environment.CurrentDirectory).
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

        public static bool DebugMode
        {
            get
            {
                string value = GetValueByName("DebugMode");
                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public static bool UseOrchestration
        {
            get
            {
                string value = GetValueByName("UseOrchestration");
                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }
#if DEBUG
        public static string AzureWebJobsStorage => "DefaultEndpointsProtocol=https;AccountName=dbgerptablestoragedev;AccountKey=0ZLZN6MUD6JdeQeFcjuy/DYsf4m/tCtBi0VNJbPU6Puz7jXlrQrJJMf+E8IIlu/9y9iQMJHw5H/vgyJf3rgtXw==;EndpointSuffix=core.windows.net";
#else
        public static string AzureWebJobsStorage => GetValueByName("AzureWebJobsStorage");
#endif
        public static string OrchestrationDbConnString => GetValueByName("OrchestrationDbConnString");

        public static string DBConnectionString => GetValueByName("DBConnectionString");
        public static string DbTenantId => GetValueByName("DbTenantId");

        public static string OrchestrationDbTenantId => GetValueByName("OrchestrationDbTenantId");

        public static string CryptKey => GetValueByName("CryptKey");

        

        public static string EventApi_BaseUrl => GetValueByName("EventApi_BaseUrl");
        public static string EventApi_AuthCode => GetValueByName("EventApi_AuthCode");
        public static string ERPSummaryTableName => GetValueByName("ERPSummaryTableName");
        public static string ERPSummaryTableConnectionString => GetValueByName("ERPSummaryTableConnectionString");

        public static bool UseAzureManagedIdentity
        {
            get
            {
                string value = GetValueByName("UseAzureManagedIdentity");
                return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public static string AzureTokenProviderConnectionString => GetValueByName("AzureTokenProviderConnectionString");

        public static int DefaultCacheExpireSlidingMins
        {
            get
            {
                int value = GetValueByName("DefaultCacheExpireSlidingMins").ForceToInt();
                return value <= 0 ? 1 : value;
            }
        }

        public static int BulkProcessTrackingTop => MySingletonAppSetting.GetValueByName("BulkProcessTrackingTop").ForceToInt();

        public static string[] ProcessTrackingEventActionStatus => GetValueByName("ProcessTrackingEventActionStatus").Split(';');

        public static List<string> ResetEventTables => GetValueByName("ResetEventTables").Split(';').ToList();

        public static List<string> ResetEventActionStatus => GetValueByName("ResetEventActionStatus").Split(';').ToList();

        public static int ResetEventRunMinutes => _config["ResetEventRunMinutes"].ForceToInt();

        public static string GetValueByName(string name)
        {
            try
            {
                string value = _config[name];
                if (value == null)
                {
                    //local file read from values
                    value = _config[$"Values:{name}"];
                }
                if (value != null)
                {
                    return value;
                }
                else
                {
                    throw new Exception("Setting [" + name + "] is not configured");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, "setting name: " + name);
            }
        }
    }
}
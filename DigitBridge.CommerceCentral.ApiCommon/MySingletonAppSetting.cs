using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static class MySingletonAppSetting
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder().
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

        public static string OrchestrationDbConnString => GetValueByName("OrchestrationDbConnString");

        public static string OrchestrationDbTenantId => GetValueByName("OrchestrationDbTenantId");

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
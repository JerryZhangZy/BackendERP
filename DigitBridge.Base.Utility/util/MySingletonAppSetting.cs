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
                    AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true).
                    AddJsonFile($"appsettings.test.json", optional: true, reloadOnChange: true).
                    AddJsonFile($"appsettings.test.{Environment.MachineName}.json", optional: true, reloadOnChange: true).
                    AddJsonFile($"local.settings.{Environment.MachineName}.json", optional: true, reloadOnChange: true).
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
        public static string AzureWebJobsStorage => "DefaultEndpointsProtocol=https;AccountName=dbgerpintegrationapidev;AccountKey=AVy804YTnk+hlZvEX+D/6v7PB0Xbd/GxpobBX4A/7hRwR8vyqpXYuhf9gWG1uALEq0vcScUdDroImBgzxsbESA==;EndpointSuffix=core.windows.net";
#else
        public static string AzureWebJobsStorage => GetValueByName("AzureWebJobsStorage");
#endif
        public static string OrchestrationDbConnString => GetValueByName("OrchestrationDbConnString");
        public static string Dsn => GetValueByName("dsn");
        public static string DBConnectionString => GetValueByName("DBConnectionString");
        public static string DbTenantId => GetValueByName("DbTenantId");

        public static string OrchestrationDbTenantId => GetValueByName("OrchestrationDbTenantId");

        public static string CryptKey => GetValueByName("CryptKey");



        public static string EventApi_BaseUrl => GetValueByName("EventApi_BaseUrl");
        public static string EventApi_AuthCode => GetValueByName("EventApi_AuthCode");

        public static string ERP_Integration_Api_BaseUrl => GetValueByName("ERP_Integration_Api_BaseUrl");
        public static string ERP_Integration_Api_AuthCode => GetValueByName("ERP_Integration_Api_AuthCode");


        public static string ERPSummaryTableName => GetValueByName("ERPSummaryTableName");
        public static string ERPSummaryTableConnectionString => GetValueByName("ERPSummaryTableConnectionString");

        public static string ERPImportContainerName => GetValueByName("ERPImportContainerName");
        public static string ERPExportContainerName => GetValueByName("ERPExportContainerName");
        public static string ERPBlobConnectionString => GetValueByName("ERPBlobConnectionString");

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


        #region user account
        public static bool BackdoorMode => GetValueByName("BackdoorMode").ToBoolByString();
        public static string BackdoorModePassword => GetValueByName("BackdoorModePassword");
        public static string BackdoorModeEmail => GetValueByName("BackdoorModeEmail");


        /// <summary>
        /// centrel account api endpoint url
        /// </summary>
        public static string AccountApiEndPoint => GetValueByName("AccountApiEndPoint");
        /// <summary>
        /// central account api function code
        /// </summary>
        public static string AccountApiCode => GetValueByName("AccountApiCode");

        /// <summary>
        /// orchestration api endpoint url
        /// </summary>
        public static string OrchestrationApiEndpoint => GetValueByName("OrchestrationApiEndpoint");
        /// <summary>
        /// orchestration api function code
        /// </summary>
        public static string OrchestrationApiCode => GetValueByName("OrchestrationApiCode");
        /// <summary>
        /// orchestration db tagart name(Fulfillment)
        /// </summary>
        public static string OrchestrationTargetName => GetValueByName("OrchestrationTargetName");
        /// <summary>
        /// orchestration db azure token
        /// </summary>
        public static string OrchestrationDbAzureToken => GetValueByName("OrchestrationDbAzureToken");

        #endregion user account
    }
}
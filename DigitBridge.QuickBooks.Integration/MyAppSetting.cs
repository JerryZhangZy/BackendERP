using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration
{
    public static class MyAppSetting
    {
        private static IConfigurationRoot _config= new ConfigurationBuilder().
                    SetBasePath(System.Environment.CurrentDirectory).
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

        public static string Environment
        {
            get
            {
                return GetValueByName("Environment");
            }
        }
        public static string RedirectUrl
        {
            get
            {
                return GetValueByName("RedirectUrl");
            }
        }
        public static string TokenReceiverReturnUrl
        {
            get
            {
                return GetValueByName("TokenReceiverReturnUrl");
            }
        }
        public static string BaseUrl
        {
            get
            {
                return GetValueByName("BaseUrl");
            }
        }
        public static string MinorVersion
        {
            get
            {
                return GetValueByName("MinorVersion");
            }
        }

        public static string AppClientId
        {
            get
            {
                return GetValueByName("AppClientId");
            }
        }

        public static string AppClientSecret
        {
            get
            {
                return GetValueByName("AppClientSecret");
            }
        }
        public static string CryptKey
        {
            get
            {
                return GetValueByName("CryptKey");
            }
        }

        public static string AzureWebJobsStorage
        {
            get
            {
                return GetValueByName("AzureWebJobsStorage");
            }
        }

        public static string OAuthTableName
        {
            get
            {
                return GetValueByName("OAuthTableName");
            }
        }
        public static string DebugTableName
        {
            get
            {
                return GetValueByName("DebugTableName");
            }
        }
        public static bool IsDebug
        {
            get
            {
                return Convert.ToBoolean(GetValueByName("IsDebug"));
            }
        }
        public static string GetValueByName(string name)
        {
            try
            {
                name = $"QuickBooks_{name}";
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
                    throw new Exception("Setting (" + name + ") is not configured");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, "setting name: " + name);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;

namespace DigitBridge.QuickBooks.Integration
{
    public static class MyAppSetting
    {
        private static IConfigurationRoot _config= new ConfigurationBuilder().
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

        public static string Environment
        {
            get
            {
                return _config.GetSection("QuickBooks")["Environment"];
            }
        }
        public static string RedirectUrl
        {
            get
            {
                return _config.GetSection("QuickBooks")["RedirectUrl"];
            }
        }
        public static string TokenReceiverReturnUrl
        {
            get
            {
                return _config.GetSection("QuickBooks")["TokenReceiverReturnUrl"];
            }
        }
        public static string BaseUrl
        {
            get
            {
                return _config.GetSection("QuickBooks")["BaseUrl"];
            }
        }
        public static string MinorVersion
        {
            get
            {
                return _config.GetSection("QuickBooks")["MinorVersion"];
            }
        }

        public static string AppClientId
        {
            get
            {
                return _config.GetSection("QuickBooks")["AppClientId"];
            }
        }

        public static string AppClientSecret
        {
            get
            {
                return _config.GetSection("QuickBooks")["AppClientSecret"];
            }
        }
        public static string CryptKey
        {
            get
            {
                return _config.GetSection("QuickBooks")["CryptKey"];
            }
        }
    }
}

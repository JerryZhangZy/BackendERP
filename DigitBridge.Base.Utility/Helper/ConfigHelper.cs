using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace DigitBridge.Base.Utility
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder().
                   AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                   AddEnvironmentVariables().
                   Build();

        public static string Dsn
        {
            get
            {
                return GetValueByName("dsn");
            }
        }
        public static string GetValueByName(string name, bool throwException = true)
        {
            string value = _config[name];
            if (value == null)
            {
                value = _config[$"Values:{name}"];
            }
            if (value == null && throwException)
                throw new Exception($"Setting [{ name }] is not configured");
            return value;
        }
    }
}

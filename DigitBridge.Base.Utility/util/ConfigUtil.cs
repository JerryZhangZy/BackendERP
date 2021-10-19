﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DigitBridge.Base.Utility
{
    public static class ConfigUtil
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder().
                    SetBasePath(Environment.CurrentDirectory).
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

        public static string EventApi_BaseUrl => GetValueByName("EventApi_BaseUrl");
        public static string EventApi_AuthCode => GetValueByName("EventApi_AuthCode");
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
                throw ex;
            }
        }
    }
}
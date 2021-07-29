using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DigitBridge.Log
{
    internal class ConfigHelper
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "local.settings.json"), optional: true, reloadOnChange: true)
                    //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

        public static IConfigurationRoot GetConfiguration() => _config;
        public static string GetValueByName(string name, bool throwException = false)
        {
            try
            {
                var value = _config[name];
                if (value == null)
                    value = _config[$"Values:{name}"]; 
                if (value == null && throwException)
                    throw new Exception($"Setting [{name}] is not configured");
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Setting [{name}] is not configured", ex);
            }
        }

        internal static string SentryDsn => GetValueByName("SentryDsn", true);

        internal static IConfigurationRoot GetConsoleConfiguration()
        {
            try
            {
                var str = "{\"Serilog\": {\"Using\": [ \"Serilog.Sinks.Async\" ],\"MinimumLevel\": \"Verbose\",\"Enrich\": [ \"FromLogContext\", \"WithDemystifiedStackTraces\" ], \"WriteTo\": [{\"Name\": \"Console\",\"Args\": {\"outputTemplate\": \"{NewLine}Date:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel:{Level}{NewLine}Message:{Message}{NewLine}{Exception}\"} }  ]  }}";
                return new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(str))).Build();
            }
            catch (Exception ex)
            {
                Log_Internal.Instance.Write(Guid.NewGuid().ToString(), Serilog.Events.LogEventLevel.Warning, "GetConsoleConfiguration error ", ex);
            }
            return null;
        }
        internal static IConfigurationRoot GetHeaderConfiguration()
        {
            try
            {
                var str = "{\"Serilog\": {\"Using\": [ \"Serilog.Sinks.Async\"],\"MinimumLevel\": \"Verbose\",\"Enrich\": [ \"FromLogContext\", \"WithDemystifiedStackTraces\" ]  }}";
                return new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(str))).Build();
            }
            catch (Exception ex)
            {
                Log_Internal.Instance.Write(Guid.NewGuid().ToString(), Serilog.Events.LogEventLevel.Warning, "GetHeaderConfiguration error ", ex);
            }
            return null;
        }
        internal const string OutputTemplate = "{NewLine}Date:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel:{Level}{NewLine}Message:{Message}{NewLine}{Exception}";
        internal const string FilePath_Internal = @"Logs\\Internal\\log-.json";
        internal const string FilePath_Seri = @"Logs\\Seri\\log-.json";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace DigitBridge.Log
{
    internal class SeriLogInstance
    {
        internal static Logger GetInternalLogger()
        {
            return GetLoggerByPath(ConfigHelper.FilePath_Internal);

        }

        internal static Logger GetSeriLogger()
        {
            return GetLoggerByPath(ConfigHelper.FilePath_Seri);

        }
        private static Logger GetLoggerByPath(string logFilePath)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigHelper.GetHeaderConfiguration())
                .WriteTo.Async(a => a.File(
                formatter: new JsonFormatter(renderMessage: true),
                logFilePath,     // log file name
                                 //outputTemplate: ConfigHelper.OutputTemplate,// show exception detail
                  rollingInterval: RollingInterval.Day,   //  
                  rollOnFileSizeLimit: true,              // limit file max size
                  encoding: Encoding.UTF8,                 //  
                                                           //retainedFileCountLimit: 10,              // max file count
                  shared: true,
                  flushToDiskInterval: TimeSpan.FromSeconds(1),
                  fileSizeLimitBytes: 5 * 1024 * 1024)                // sigle file max size set to 5MB
                  )
                .CreateLogger();

        }

        internal static Logger GetConsoleLogger()
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(ConfigHelper.GetHeaderConfiguration())

                 // send event to console and display detail
                 .WriteTo.Console(
                //formatter: new JsonFormatter(renderMessage: true)
                outputTemplate: ConfigHelper.OutputTemplate
                //formatProvider: new CompactJsonFormatter()
                )
                 .Enrich.FromLogContext()

                 .CreateLogger();
        }


        //internal static Logger GetSeriSentryLogger(Dictionary<string, string> defaulTags = null)
        //{
        //    //        var log = new LoggerConfiguration()
        //    //.WriteTo.Sentry("Sentry DSN", dataScrubber: new MyDataScrubber())
        //    //.Enrich.FromLogContext()
        //    //.CreateLogger();

        //    return new LoggerConfiguration()
        //        .ReadFrom.Configuration(ConfigHelper.GetHeaderConfiguration())
        //        .WriteTo.Sentry(
        //        dsn: ConfigHelper.GetSentryDsn(),
        //        //tags: tagNames,
        //        minimumEventLevel: LogEventLevel.Verbose,
        //        minimumBreadcrumbLevel: LogEventLevel.Verbose,
        //        //textFormatter: new JsonFormatter(renderMessage: true),
        //        sendDefaultPii: true,
        //        defaultTags: defaulTags,
        //        attachStackTrace: true,
        //        debug: true,
        //environment: Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Debug"
        //        )
        //        .Enrich.FromLogContext()
        //        .CreateLogger();
        //}

        internal static Logger GetLogger(IConfigurationRoot config)
        {
            return new LoggerConfiguration()
                 .ReadFrom.Configuration(config)
                 .CreateLogger();
        }

    }
}

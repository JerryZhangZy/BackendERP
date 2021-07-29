using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Sentry;
using Sentry.Protocol;

namespace DigitBridge.Log
{
    internal class Log_Sentry
    {
        private Log_Sentry() { }
        static Log_Sentry()
        {
            SentrySdk.Init(o =>
            {
                o.Dsn = ConfigHelper.SentryDsn;
                //o.Dsn = ConfigHelper.GetSentryDsn();
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                //o.TracesSampleRate = 1.0;
                o.BeforeSend = (sentryEvent) =>
                {
                    // ignore TaskCanceledException/OperationCanceledException
                    return sentryEvent.Exception is TaskCanceledException || sentryEvent.Exception is OperationCanceledException
                       ? null
                       : sentryEvent;
                };
                o.AttachStacktrace = true;
                //o.DiagnosticLevel = SentryLevel.Error;
                //o.BeforeBreadcrumb = (breadcrumb) => {
                //   //o.
                //   // return breadcrumb.Category === 'ui.click' ? null : breadcrumb;
                //}; 
                o.TracesSampleRate = 1.0;
            });
        }

        #region Instance

        private static object _lockObj = new object();
        private static Log_Sentry _instance;

        public static Log_Sentry Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Log_Sentry();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public string Write(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            if (tags != null)
            {
                foreach (var item in tags)
                {
                    exception.AddSentryTag(item.Key, item.Value);
                }
            }
            if (propertyValues != null)
            {
                exception.AddSentryContext("ADDITIONAL DATA", propertyValues);
            }
            // SentrySdk.AddBreadcrumb("ADDITIONAL DATA", "data", "type", tags);

            return SentrySdk.CaptureException(exception).ToString();
        }


        public string Write<T>(Exception exception, T propertyValue, Dictionary<string, string> tags = null)
        {
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    exception.AddSentryTag(tag.Key, tag.Value);
                }
            }
            if (propertyValue != null)
            {
                exception.AddSentryContext("ADDITIONAL DATA", new Dictionary<string, object>() { { "PropertyValue", propertyValue } });
            }

            return SentrySdk.CaptureException(exception).ToString();
        }


        public string Write(string message, SentryLevel level)
        {
            return SentrySdk.CaptureMessage(message, level).ToString();
        }

        public string CaptureEvent(string type, string typevalue, string module, Exception exception = null, Dictionary<string, object> extra = null, Dictionary<string, string> tags = null)
        {
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(exception);
            var jDoc = JsonDocument.Parse(bytes);


            var sentryException = new SentryException()
            {
                Module = module,
                Type = type,
                Value = typevalue,
                Stacktrace = SentryStackTrace.FromJson(jDoc.RootElement)
            };

            var sentryEvent = new SentryEvent
            {
                //Contexts = contexts,
                //Request = request,
                SentryExceptions = new List<SentryException>() { sentryException },
                Level = SentryLevel.Error,
                Message = new SentryMessage() { Message = exception.Message }
            };


            if (extra != null)
                sentryEvent.SetExtras(extra);
            if (tags != null)
                sentryEvent.SetTags(tags);
            //else
            //    sentryEvent.SetTag("test", "test");

            //sentryEvent.ServerName = "dgs";
            //sentryEvent.Contexts = new Contexts();
            return SentrySdk.CaptureEvent(sentryEvent).ToString();
        }
    }
}

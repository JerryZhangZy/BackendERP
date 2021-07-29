using System;
using System.Collections.Generic;
using Sentry;
using Serilog.Events;

namespace DigitBridge.Log
{
    public class LogCenter
    {
        // defualt disable sentry  
        private static bool _sentryEnable = true;
        private static bool _serilogEnable = true;
        private static bool _writeToConsole = true;
        //private static bool _useSeriSentry = false;

        private LogCenter()
        { }
        static LogCenter()
        {
            ConfigSetting();
        }
        /// <summary>
        /// Capture Exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="propertyValues"></param>
        /// <param name="tags"></param>
        /// <param name="eventLevel"></param>
        /// <returns></returns>
        public static string CaptureException(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null, EventLevel eventLevel = EventLevel.Error)
        {
            ValidationHelper.Validation(exception);

            var key = Guid.NewGuid().ToString("N");
            if (_serilogEnable)
            {
                Log_Seri.Instance.Write(key, (LogEventLevel)(int)eventLevel, exception, propertyValues, tags);
            }
            if (_writeToConsole)
            {
                Log_Console.Instance.Write(key, (LogEventLevel)(int)eventLevel, exception, propertyValues, tags);
            }
            // due to the sentry will override the data of exception,then write it at the end
            if (_sentryEnable)
            {
                key = Log_Sentry.Instance.Write(exception, propertyValues, tags);
            }
            return key;
        }

        /// <summary>
        /// Capture Exception and message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="eventLevel"></param>
        /// <returns></returns>
        public static string CaptureException(Exception exception, string message, Dictionary<string, string> tags = null, EventLevel eventLevel = EventLevel.Error)
        {
            ValidationHelper.Validation(exception, message);

            var key = Guid.NewGuid().ToString("N");
            if (_serilogEnable)
            {
                Log_Seri.Instance.Write(key, (LogEventLevel)(int)eventLevel, exception, message, tags);
            }
            if (_writeToConsole)
            {
                Log_Console.Instance.Write(key, (LogEventLevel)(int)eventLevel, exception, message, tags);
            }
            // due to the sentry will override the data of exception,then write it at the end
            if (_sentryEnable)
            {
                key = Log_Sentry.Instance.Write(exception, message, tags);
            }
            return key;
        }

        /// <summary>
        /// Capture Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventLevel"></param>
        /// <returns></returns>
        public static string CaptureMessage(string message, EventLevel eventLevel = EventLevel.Error)
        {
            ValidationHelper.Validation(message);

            var key = Guid.NewGuid().ToString("N");

            if (_serilogEnable)
            {
                Log_Seri.Instance.Write(key, (LogEventLevel)(int)eventLevel, message);
            }
            if (_writeToConsole)
            {
                Log_Console.Instance.Write(key, (LogEventLevel)(int)eventLevel, message);
            }
            if (_sentryEnable)
            {
                key = Log_Sentry.Instance.Write(message, (SentryLevel)(((int)eventLevel) - 1));
            }
            return key;
        }

        [Obsolete("only for sentry,may changed later")]
        public static string CaptureEvent(string type, string typevalue, string module, Exception exception = null, Dictionary<string, object> extra = null, Dictionary<string, string> tags = null)
        {

            return Log_Sentry.Instance.CaptureEvent(type, typevalue, module, exception, extra, tags);
        }

        #region Warning

        public static string Warning(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, propertyValues, tags, EventLevel.Warning);
        }

        public static string Warning(Exception exception, string message, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, message, tags, EventLevel.Warning);
        }

        public static string Warning(string message)
        {
            return CaptureMessage(message, EventLevel.Warning);
        }
        #endregion

        #region Info
        public static string Info(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, propertyValues, tags, EventLevel.Info);
        }

        public static string Info(Exception exception, string message, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, message, tags, EventLevel.Info);
        }

        public static string Info(string message)
        {
            return CaptureMessage(message, EventLevel.Info);
        }
        #endregion

        #region Debug

        public static string Debug(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, propertyValues, tags, EventLevel.Debug);
        }

        public static string Debug(Exception exception, string message, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, message, tags, EventLevel.Debug);
        }

        public static string Debug(string message)
        {
            return CaptureMessage(message, EventLevel.Debug);
        }
        #endregion

        #region Fatal

        public static string Fatal(Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, propertyValues, tags, EventLevel.Fatal);
        }

        public static string Fatal(Exception exception, string message, Dictionary<string, string> tags = null)
        {
            return CaptureException(exception, message, tags, EventLevel.Fatal);
        }

        public static string Fatal(string message)
        {
            return CaptureMessage(message, EventLevel.Fatal);
        }
        #endregion

        /// <summary>
        /// use ppsettings.json setting for sentry
        /// </summary>
        private static void ConfigSetting()
        {
            try
            {
                var sentryEnable = ConfigHelper.GetValueByName("SentryEnable");
                if (!string.IsNullOrEmpty(sentryEnable))
                    _sentryEnable = bool.Parse(sentryEnable);

                var serilogEnable = ConfigHelper.GetValueByName("SeriEnable");
                if (!string.IsNullOrEmpty(serilogEnable))
                    _serilogEnable = bool.Parse(serilogEnable);

                var writeToConsole = ConfigHelper.GetValueByName("Console:WriteTo");
                if (!string.IsNullOrEmpty(writeToConsole))
                    _writeToConsole = bool.Parse(writeToConsole);
            }
            catch (Exception ex)
            {
                // use default setting if not config
                Log_Internal.Instance.Write(Guid.NewGuid().ToString("N"), LogEventLevel.Warning, ex);
            }
        }
    }
}

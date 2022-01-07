using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DigitBridge.Log
{
    abstract class LogBase
    {
        private Logger _logger;
        public LogBase(Logger logger)
        {
            this._logger = logger;
        }
        public LogBase()
        { }
        public virtual void Write(string key, LogEventLevel level, Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            _logger.AddContext(key, exception, propertyValues, tags).Write(level, "");
            //.ForContext("Tags", tags, true)


        }

        public virtual void Write<T>(string key, LogEventLevel level, Exception exception, T messageInfo, Dictionary<string, string> tagNameValues = null)
        {
            _logger.AddContext(key, exception, tags: tagNameValues)
                .ForContext("Message Info", messageInfo, true)
                .Write(level, "");
        }


        public virtual void Write(string key, LogEventLevel level, string message, Exception exception = null)
        {
            _logger.AddContext(key, exception).ForContext("Message", message, false)
                        .Write(level, "");
        }
    }
    static class LogBaseEntension
    {
        internal static ILogger AddContext(this ILogger logger, string key, Exception exception, Dictionary<string, object> propertyValues = null, Dictionary<string, string> tags = null)
        {
            logger = logger.ForContext("Key", key, false);
            logger = logger.ForContext("Exception", exception, true);
            if (tags != null)
            {
                logger = logger.ForContext("Tags", tags, true);
            }

            if (propertyValues != null)
            {
                foreach (var item in propertyValues)
                {
                    logger = logger.ForContext(item.Key, item.Value, true);
                }
            }

            return logger;
        }
    }
}

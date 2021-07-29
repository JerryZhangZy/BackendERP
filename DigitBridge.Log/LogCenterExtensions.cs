using System;
using DigitBridge.Log; 

namespace Microsoft.Extensions.Logging
{
    public static class LogCenterExtensions
    {

        private static string CaptureException(Exception exception, string message, EventLevel level, params object[] args)
        {
            return args == null || args.Length == 0
                ? LogCenter.CaptureException(exception, message, eventLevel: level)
                : LogCenter.CaptureException(exception, CommonHelper.GetPropertyValues(message, args), eventLevel: level);

        }

        #region Error  

        public static string Error(this ILogger logger, string message)
        {
            ValidationHelper.Validation(message);
            logger.LogError(message);
            //todo
            return LogCenter.CaptureMessage(message, EventLevel.Error);
        }

        public static string Error(this ILogger logger, Exception exception, string message, params object[] args)
        {
            ValidationHelper.Validation(exception, message);

            logger.LogError(exception, message, args);

            return CaptureException(exception, message, EventLevel.Error, args);

        }
        #endregion

        #region Info 
        public static string Info(this ILogger logger, string message)
        {
            ValidationHelper.Validation(message);
            logger.LogInformation(message);
            //todo
            return LogCenter.CaptureMessage(message, EventLevel.Info);
        }

        public static string Info(this ILogger logger, Exception exception, string message, params object[] args)
        {
            ValidationHelper.Validation(exception, message);

            logger.LogInformation(exception, message, args);

            return CaptureException(exception, message, EventLevel.Info, args);
        }
        #endregion

        #region Debug

        public static string Debug(this ILogger logger, string message)
        {
            ValidationHelper.Validation(message);
            logger.LogDebug(message);
            //todo
            return LogCenter.CaptureMessage(message, EventLevel.Debug);
        }

        public static string Debug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            ValidationHelper.Validation(exception, message);

            logger.LogDebug(exception, message, args);

            return CaptureException(exception, message, EventLevel.Debug, args);
        }

        #endregion

        #region Warning

        public static string Warning(this ILogger logger, string message)
        {
            ValidationHelper.Validation(message);
            logger.LogWarning(message);
            //todo
            return LogCenter.CaptureMessage(message, EventLevel.Warning);
        }

        public static string Warning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            ValidationHelper.Validation(exception, message);

            logger.LogWarning(exception, message, args);

            return CaptureException(exception, message, EventLevel.Warning, args);
        }

        #endregion 
    }
}

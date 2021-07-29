using System;
using System.Collections.Generic;

namespace DigitBridge.Log
{
    public enum EventLevel
    {   //
        // Summary:
        //     Debug.
        Debug = 1,
        //
        // Summary:
        //     Informational.
        Info = 2,
        //
        // Summary:
        //     Warning.
        Warning = 3,
        //
        // Summary:
        //     Error.
        Error = 4,
        //
        // Summary:
        //     Fatal.
        Fatal = 5

    } 
     
    internal class ValidationHelper
    {
        public static void Validation(Exception exception, string message)
        {
            if (exception == null)
                throw new Exception("exception could not be null");
            if (string.IsNullOrEmpty(message))
                throw new Exception("message could not be null or empty");
        }
        public static void Validation(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new Exception("message could not be null or empty");
        }

        public static void Validation(Exception exception)
        {
            if (exception == null)
                throw new Exception("exception could not be null");
        }
    }
    internal class CommonHelper
    {
        public static Dictionary<string, object> GetPropertyValues(string message, params object[] args)
        {
            var propertyValues = new Dictionary<string, object>() { { "Message", message } };

            var typename = args[0].GetType().Name;
            for (int i = 0; i < args.Length; i++)
            {
                propertyValues.Add(typename + i, args[i]);
            }
            return propertyValues;
        }
    }
}

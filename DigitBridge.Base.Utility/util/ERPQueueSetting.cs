using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.Base.Common
{
    public static class ERPQueueSetting
    {
        private static IConfigurationRoot _config= new ConfigurationBuilder().
                    SetBasePath(Environment.CurrentDirectory).
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();


        public static string AzureWebJobsStorage => GetValueByName("AzureWebJobsStorage");
        
        public static string ERPQuickBooksInvoiceQueue => GetValueByName("ERPQuickBooksInvoiceQueueName");
        public static string ERPQuickBooksInvoiceVoidQueue => GetValueByName("ERPQuickBooksInvoiceVoidQueueName");
        public static string ERPQuickBooksReturnQueue => GetValueByName("ERPQuickBooksReturnQueueName");
        public static string ERPQuickBooksReturnDeleteQueue => GetValueByName("ERPQuickBooksReturnDeleteQueueName");
        public static string ERPQuickBooksPaymentQueue => GetValueByName("ERPQuickBooksPaymentQueueName");
        public static string ERPQuickBooksPaymentDeleteQueue => GetValueByName("ERPQuickBooksPaymentDeleteQueueName");
        public static string ERPSalesOrderQueue => GetValueByName("ERPSalesOrderQueueName");
        public static string ERPInvoiceQueue => GetValueByName("ERPInvoiceQueueName");
        public static string ERPCreateInvoiceByOrdershipmentQueue => GetValueByName("ERPCreateInvoiceByOrdershipmentQueueName");
        public static string ERPCreateSalesOrderByCentralorderQueue => GetValueByName("ERPCreateSalesOrderByCentralorderQueueName");

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
                    throw new Exception("Setting (" + name + ") is not configured");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex, "setting name: " + name);
            }
        }
    }
}

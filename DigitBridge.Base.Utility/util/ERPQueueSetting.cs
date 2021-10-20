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

        public static string ERPQuickBooksInvoiceQueue => GetValueByName("ERP-QuickBooks-Invoice-Queue");
        public static string ERPQuickBooksInvoiceVoidQueue => GetValueByName("ERP-QuickBooks-Invoice-Void-Queue");
        public static string ERPQuickBooksReturnQueue => GetValueByName("ERP-QuickBooks-return-Queue");
        public static string ERPQuickBooksReturnDeleteQueue => GetValueByName("ERP-QuickBooks-return-delete-Queue");
        public static string ERPQuickBooksPaymentQueue => GetValueByName("ERP-QuickBooks-payment-Queue");
        public static string ERPQuickBooksPaymentDeleteQueue => GetValueByName("ERP-QuickBooks-payment-delete-Queue");
        public static string ERPSalesOrderQueue => GetValueByName("ERP-SalesOrder-Queue");
        public static string ERPInvoiceQueue => GetValueByName("ERP-Invoice-Queue");
        public static string ERPCreateInvoiceByOrdershipmentQueue => GetValueByName("ERP-Create-Invoice-By-Ordershipment-Queue");
        public static string ERPCreateSalesOrderByCentralorderQueue => GetValueByName("ERP-Create-SalesOrder-By-Centralorder-Queue");

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

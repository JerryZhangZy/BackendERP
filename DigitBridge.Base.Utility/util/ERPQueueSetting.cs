using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.Base.Common
{
    public static class ERPQueueSetting
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder().
                    SetBasePath(Environment.CurrentDirectory).
                    AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).
                    AddEnvironmentVariables().
                    Build();

#if DEBUG
        public static string AzureWebJobsStorage => "DefaultEndpointsProtocol=https;AccountName=dbgerptablestoragedev;AccountKey=0ZLZN6MUD6JdeQeFcjuy/DYsf4m/tCtBi0VNJbPU6Puz7jXlrQrJJMf+E8IIlu/9y9iQMJHw5H/vgyJf3rgtXw==;EndpointSuffix=core.windows.net";
        public static string IntegrationStorage => "DefaultEndpointsProtocol=https;AccountName=dbgerpintegrationapidev;AccountKey=AVy804YTnk+hlZvEX+D/6v7PB0Xbd/GxpobBX4A/7hRwR8vyqpXYuhf9gWG1uALEq0vcScUdDroImBgzxsbESA==;EndpointSuffix=core.windows.net";
        public static string ERPQuickBooksInvoiceQueue => "erp-quickbooks-invoice-queue";
        public static string ERPQuickBooksInvoiceVoidQueue => "erp-quickbooks-invoice-void-queue";
        public static string ERPQuickBooksReturnQueue => "erp-quickbooks-return-queue";
        public static string ERPQuickBooksReturnDeleteQueue => "erp-quickbooks-return-delete-queue";
        public static string ERPQuickBooksPaymentQueue => "erp-quickbooks-payment-queue";
        public static string ERPQuickBooksPaymentDeleteQueue => "erp-quickbooks-payment-delete-queue";
        public static string ERPSalesOrderQueue => GetValueByName("ERPSalesOrderQueueName");
        public static string ERPInvoiceQueue => GetValueByName("ERPInvoiceQueueName");
        public static string ERPCreateInvoiceByOrdershipmentQueue => "erp-create-invoice-by-ordershipment";
        public static string ERPCreateSalesOrderByCentralorderQueue => "erp-create-salesorder-by-centralorder";
        public static string ERPCreateShipmentByWMSQueue => "erp-create-shipment-by-wms";
#else
        public static string AzureWebJobsStorage => GetValueByName("AzureWebJobsStorage");
        public static string IntegrationStorage => GetValueByName("IntegrationStorage"); 

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
        public static string ERPCreateShipmentByWMSQueue => GetValueByName("ERPCreateShipmentByWMSQueueName");
#endif

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

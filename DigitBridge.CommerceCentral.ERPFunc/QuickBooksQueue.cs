using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitBridge.CommerceCentral.ERPFunc
{
    public static class QuickBooksQueue
    {
        [FunctionName("QboInvoice")]
        public static void QboInvoice([QueueTrigger("QboInvoiceQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

        [FunctionName("QboPayment")]
        public static void QboPayment([QueueTrigger("QboPaymentQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

        [FunctionName("QboReturn")]
        public static void QboReturn([QueueTrigger("QboReturnQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

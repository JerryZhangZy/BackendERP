using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitBridge.CommerceCentral.ERPFunc
{
    public static class QuickBooksQueue
    {
        [FunctionName("QboInvoice")]
        public static void QboInvoice([QueueTrigger("qboinvoicequeue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

        [FunctionName("QboPayment")]
        public static void QboPayment([QueueTrigger("qbopaymentqueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

        [FunctionName("QboReturn")]
        public static void QboReturn([QueueTrigger("qboreturnqueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

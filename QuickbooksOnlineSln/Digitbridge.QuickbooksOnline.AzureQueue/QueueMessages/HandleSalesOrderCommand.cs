using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages
{
    public class QuickBooksOrderTransferQueueMessage
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }
    }
    public class HandleSalesOrderCommand : BaseQueueMessage
    {
        public List<QuickBooksOrderTransferQueueMessage> QuickBooksOrderTransferQueueMessages;
        public HandleSalesOrderCommand() : base(RouteNames.QuickBooksOrderExportQueue)
        {

        }
    }
}

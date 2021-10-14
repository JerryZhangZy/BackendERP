using DigitBridge.Base.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public class ERPQueueMessage: IQueueEntity
    {
        public int  DatabaseNum{get;set;}

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string EventUuid { get; set; }

        public ErpEventType ERPEventType { get; set; }

        public string ProcessSource { get; set; }

        public string ProcessUuid { get; set; }

        public string ProcessData { get; set; }

        public string MessageId { get; set; }

        public string PopReceipt { get; set; }
    }
}

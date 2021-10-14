using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility.Model
{
    public class QueueMessage
    {
        public int  DatabaseNum{get;set;}

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string EventUuid { get; set; }

        public int ERPEventType { get; set; }

        public string ProcessSource { get; set; }

        public string ProcessUuid { get; set; }

        public string ProcessData { get; set; }
    }
}

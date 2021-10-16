using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class QuickBooksDebugInfo :TableEntity
    {

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        //TO(To QuickBooks),Return(call after return)
        public string Direct { get; set; }

        public string DebugInfo { get; set; }

        public string Type { get; set; }
    }
}

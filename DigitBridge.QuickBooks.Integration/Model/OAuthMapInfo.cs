using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class OAuthMapInfo : TableEntity
    {
        public string RequestState { get; set; }

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }
    }
}

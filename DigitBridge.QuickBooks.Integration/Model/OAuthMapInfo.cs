using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class OAuthMapInfo
    {
        [JsonIgnore]
        public string PartitionKey => MasterAccountNum.ToString();

        [JsonIgnore]
        public string RowKey => RequestState;

        public string RequestState { get; set; }

        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }
    }
}

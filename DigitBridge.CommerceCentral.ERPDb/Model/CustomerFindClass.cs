using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// CustomerFindClass use to find customer data.
    /// 1. find by customerUuid
    /// 2. find by customerCode + MasterAccountNum + ProfileNum
    /// 3. find by ChannelNum + ChannelAccountNum + MasterAccountNum + ProfileNum
    /// 4. find by Phone1 + CustomerName + MasterAccountNum + ProfileNum
    /// 5. find by Email + CustomerName + MasterAccountNum + ProfileNum
    /// </summary>
    [Serializable]
    public class CustomerFindClass
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }

        public string CustomerUuid { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public int ChannelNum { get; set; }
        public int ChannelAccountNum { get; set; }
    }
}

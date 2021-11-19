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
    /// ProductFindClass use to find product data.
    /// 1. find by ProductUuid
    /// 2. find by CentralProductNum
    /// 3. find by SKU + MasterAccountNum + ProfileNum
    /// 4. find by UPC + MasterAccountNum + ProfileNum
    /// 5. find by EAN + MasterAccountNum + ProfileNum
    /// 6. find by ASIN + MasterAccountNum + ProfileNum
    /// 7. find by FNSku + MasterAccountNum + ProfileNum
    /// </summary>
    [Serializable]
    public class ProductFindClass
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }

        public string ProductUuid { get; set; }
        public int CentralProductNum { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string EAN { get; set; }
        public string ASIN { get; set; }
        public string FNSku { get; set; }
    }
}

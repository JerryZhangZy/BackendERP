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
   
    [Serializable]
    public class InitNumbersFindClass
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }

        public string Type { get; set; }
     
 
    }
}

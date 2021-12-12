              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InitNumbers
    {
        public string TypeName 
        { 
            get
            {
                return Type.ToInt().ToEnum<ActivityLogType>(ActivityLogType.Unknow).ToName();
            }
        }
    }
}




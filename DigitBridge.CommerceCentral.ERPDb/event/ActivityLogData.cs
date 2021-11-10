    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class ActivityLogData
    {
        public ActivityLogData(IDataBaseFactory dbFactory, ActivityLog log) : base(dbFactory) 
        {
            this.ActivityLog = log;
        }
    }
}




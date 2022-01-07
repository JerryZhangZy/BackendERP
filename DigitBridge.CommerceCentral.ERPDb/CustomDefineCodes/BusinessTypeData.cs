    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class BusinessTypeData
    {
        public List<BusinessType> GetBusinessTypes(int masterAccountNum, int profileNum) =>
            dbFactory.Find<BusinessType>("WHERE MasterAccountNum = @0 AND ProfileNum = @1 ORDER BY RowNum ", masterAccountNum, profileNum).ToList();

    }
}




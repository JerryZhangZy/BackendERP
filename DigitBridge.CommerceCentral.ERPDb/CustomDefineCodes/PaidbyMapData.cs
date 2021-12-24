    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class PaidbyMapData
    {
        public List<PaidbyMap> GetPaidbyMaps(int masterAccountNum, int profileNum) =>
            dbFactory.Find<PaidbyMap>("WHERE MasterAccountNum = @0 AND ProfileNum = @1 ORDER BY RowNum ", masterAccountNum, profileNum).ToList();
        
    }
}




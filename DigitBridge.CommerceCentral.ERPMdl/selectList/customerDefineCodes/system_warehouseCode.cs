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
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class system_warehouseCode : SelectListBase
    {
        public override string Name => "system_warehouseCode";

        public system_warehouseCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = true;
            this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT 
DistributionCenterCode AS [value], 
DistributionCenterCode AS [text], 
1 AS [count],
DistributionCenterUuid AS [warehouseUuid],
DistributionCenterName AS [warehouseName]
FROM DistributionCenter tbl
WHERE {this.QueryObject.GetSQL()}
AND DistributionCenterCode != ''
ORDER BY DistributionCenterCode
";
            return this.SQL_Select;
        }
    }
}

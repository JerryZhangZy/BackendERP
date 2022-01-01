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
    public partial class system_businessType : SelectListBase
    {
        public override string Name => "system_businessType";

        public system_businessType(IDataBaseFactory dbFactory) : base(dbFactory) { }

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
BusinessType AS [value], 
BusinessType AS [text], 
1 AS [count],
BusinessType AS [businessType],
priceRule  AS [priceRule]
FROM BusinessType tbl
WHERE {this.QueryObject.GetSQL()}
AND BusinessType != ''
ORDER BY BusinessType
";
            return this.SQL_Select;
        }
    }
}

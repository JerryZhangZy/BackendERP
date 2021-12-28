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
    public partial class system_shippingClass : SelectListBase
    {
        public override string Name => "system_shippingClass";

        public system_shippingClass(IDataBaseFactory dbFactory) : base(dbFactory) { }

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
ShippingClass AS [value], 
ShippingCarrier + ' - ' + ShippingClass AS [text], 
1 AS [count],
Description AS [description],
ShippingCarrier AS [shippingCarrier],
ShippingClass AS [shippingClass],
Scac AS [scac]
FROM ShippingCodes tbl
WHERE {this.QueryObject.GetSQL()}
AND ShippingCode != ''
ORDER BY ShippingCarrier, ShippingClass
";
            return this.SQL_Select;
        }
    }
}

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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.vender
{
 
    public partial class vendor_vendorCode : SelectListBase
    {
        public override string Name => "vendor_vendorCode";

        public vendor_vendorCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.VendorCode, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.VendorCode AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Vendor] tbl
   
    WHERE COALESCE(tbl.VendorCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.VendorCode 
ORDER BY tbl.VendorCode 
";
            return this.SQL_Select;
        }
    }
}

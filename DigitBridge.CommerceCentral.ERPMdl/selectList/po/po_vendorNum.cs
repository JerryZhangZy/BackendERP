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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.po
{
 
    public partial class po_vendorNum : SelectListBase
    {
        public override string Name => "po_vendorNum";

        public po_vendorNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.vendorNum, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      COALESCE(tbl.vendorNum,'') AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeader] tbl
   
    WHERE COALESCE(tbl.vendorNum,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.vendorNum
ORDER BY tbl.vendorNum 
";
            return this.SQL_Select;
        }
    }
}

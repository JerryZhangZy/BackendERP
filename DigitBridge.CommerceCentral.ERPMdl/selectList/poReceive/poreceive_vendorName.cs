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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.poReceive
{
 
    public partial class poreceive_vendorName : SelectListBase
    {
        public override string Name => "poreceive_vendorName";

        public poreceive_vendorName(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.vendorName, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.vendorName AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoTransaction] tbl
   
    WHERE COALESCE(tbl.vendorName,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.vendorName 
ORDER BY tbl.vendorName 
";
            return this.SQL_Select;
        }
    }
}

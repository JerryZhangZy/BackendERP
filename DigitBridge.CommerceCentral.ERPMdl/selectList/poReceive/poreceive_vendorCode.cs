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
 
    public partial class poreceive_vendorCode : SelectListBase
    {
        public override string Name => "poreceive_vendorCode";

        public poreceive_vendorCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.vendorCode, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.vendorCode AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoTransaction] tbl
   
    WHERE COALESCE(tbl.vendorCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.vendorCode 
ORDER BY tbl.vendorCode 
";
            return this.SQL_Select;
        }
    }
}

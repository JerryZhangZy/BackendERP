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
 
    public partial class poreceive_transNum : SelectListBase
    {
        public override string Name => "poreceive_transNum";

        public poreceive_transNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.transNum, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.transNum AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoTransaction] tbl
   
    WHERE COALESCE(tbl.transNum,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.transNum 
ORDER BY tbl.transNum 
";
            return this.SQL_Select;
        }
    }
}

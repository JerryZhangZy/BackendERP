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
namespace DigitBridge.CommerceCentral.ERPMdl.selectList.poHeaderInfo
{


    public partial class poHeaderInfo_refNum : SelectListBase
    {
        public override string Name => "poHeaderInfo_refNum";

        public poHeaderInfo_refNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.RefNum, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      COALESCE(tbl.RefNum,'') AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeader] tbl
   
    WHERE COALESCE(tbl.RefNum,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.RefNum
ORDER BY tbl.RefNum 
";
            return this.SQL_Select;
        }
    }
}

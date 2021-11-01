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
namespace DigitBridge.CommerceCentral.ERPMdl.selectList.poHeader
{
 
    

    public partial class poHeader_poNum : SelectListBase
    {
        public override string Name => "poHeader_poNum";

        public poHeader_poNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $" tbl.PoNum LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.PoNum  AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeader] tbl
   
    WHERE  
          {this.QueryObject.GetSQL()}
    GROUP BY tbl.PoNum
ORDER BY tbl.PoNum 
";
            return this.SQL_Select;
        }
    }
}

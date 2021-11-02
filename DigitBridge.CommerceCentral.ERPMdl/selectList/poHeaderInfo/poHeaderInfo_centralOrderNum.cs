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
 
    public partial class poHeaderInfo_centralOrderNum : SelectListBase
    {
        public override string Name => "poHeaderInfo_centralOrderNum";

        public poHeaderInfo_centralOrderNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $" tbl.CentralOrderNum LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.CentralOrderNum AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeaderInfo] tbl
   
    WHERE  
          {this.QueryObject.GetSQL()}
    GROUP BY tbl.CentralOrderNum
ORDER BY tbl.CentralOrderNum 
";
            return this.SQL_Select;
        }
    }
}

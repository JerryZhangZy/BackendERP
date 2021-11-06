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
 
    public partial class po_centralOrderNum : SelectListBase
    {
        public override string Name => "po_centralOrderNum";

        public po_centralOrderNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                     $"COALESCE(poh.CentralOrderNum, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      COALESCE(poh.CentralOrderNum,'') AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeaderInfo] poh
   INNER JOIN PoHeader tbl ON tbl.PoUuid=poh.PoUuid
    WHERE COALESCE(poh.CentralOrderNum,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY poh.CentralOrderNum
ORDER BY poh.CentralOrderNum ";
            return this.SQL_Select;
        }
    }
}

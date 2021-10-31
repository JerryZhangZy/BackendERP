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
namespace DigitBridge.CommerceCentral.ERPMdl.selectList.customer
{
 
    public partial class customer_districtn : SelectListBase
    {
        public override string Name => "customer_districtn";

        public customer_districtn(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.Districtn, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.Districtn AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Customer] tbl
   
    WHERE COALESCE(tbl.Districtn,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.Districtn
";
            return this.SQL_Select;
        }
    }
}

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
 
    public partial class customer_customerCode : SelectListBase
    {
        public override string Name => "customer_customerCode";

        public customer_customerCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.customerCode, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.CustomerCode AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Customer] tbl
   
    WHERE COALESCE(tbl.CustomerCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.CustomerCode
ORDER BY tbl.CustomerCode 
";
            return this.SQL_Select;
        }
    }
}

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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class so_customerCode : SelectListBase
    {
        public override string Name => "so_customerCode";

        public so_customerCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"tbl.customerCode LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT i.*, COALESCE(d.CustomerName, '') AS [text] 
FROM (
    SELECT 
        tbl.CustomerUuid AS id, 
        tbl.CustomerCode AS [value], 
        COUNT(1) AS [count]
    FROM SalesOrderHeader tbl
    WHERE tbl.CustomerCode != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.CustomerCode, tbl.CustomerUuid
) i 
LEFT JOIN Customer d ON (d.CustomerUuid = i.id) 
ORDER BY i.[value]
";
            return this.SQL_Select;
        }
    }
}

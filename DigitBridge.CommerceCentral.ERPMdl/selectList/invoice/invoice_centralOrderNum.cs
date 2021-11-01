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
    public partial class invoice_centralOrderNum : SelectListBase
    {
        public override string Name => "invoice_centralOrderNum";

        public invoice_centralOrderNum(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"CAST(CentralOrderNum as varchar(max)) LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT CentralOrderNum AS [value], '' AS [text], COUNT(1) AS [count]
FROM InvoiceHeaderInfo INNER JOIN InvoiceHeader tbl ON InvoiceHeaderInfo.InvoiceUuid=tbl.InvoiceUUid
WHERE {this.QueryObject.GetSQL()}
GROUP BY CentralOrderNum
ORDER BY [value]
";
            return this.SQL_Select;
        }
    }
}

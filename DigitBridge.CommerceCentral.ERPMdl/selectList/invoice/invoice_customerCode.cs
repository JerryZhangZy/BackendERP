﻿using System;
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
    public partial class invoice_customerCode : SelectListBase
    {
        public override string Name => "invoice_customerCode";

        public invoice_customerCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.customerCode, '') LIKE '{this.QueryObject.Term.FilterValue}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT i.*, COALESCE(d.CustomerName, '') AS description 
FROM (
    SELECT 
        tbl.CustomerUuid AS id, 
        tbl.CustomerCode AS cd, 
        COUNT(1) AS cnt
    FROM InvoiceHeader tbl
    WHERE COALESCE(tbl.CustomerCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.CustomerCode, tbl.CustomerUuid
) i 
LEFT JOIN Customer d ON (d.CustomerUuid = i.id) 
ORDER BY i.cd
";
            return this.SQL_Select;
        }
    }
}
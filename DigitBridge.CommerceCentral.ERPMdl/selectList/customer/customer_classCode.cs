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
namespace DigitBridge.CommerceCentral.ERPMdl.selectList.customer
{
 
    public partial class customer_classCode : SelectListBase
    {
        public override string Name => "customer_classCode";

        public customer_classCode(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.ClassCode, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.ClassCode AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Customer] tbl
   
    WHERE COALESCE(tbl.ClassCode,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.ClassCode 
ORDER BY tbl.ClassCode 
";
            return this.SQL_Select;
        }
    }
}

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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.vender
{
 
    public partial class vender_area : SelectListBase
    {
        public override string Name => "vender_area";

        public vender_area(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.area, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.area AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Vendor] tbl
   
    WHERE COALESCE(tbl.area,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.area 
ORDER BY tbl.area 
";
            return this.SQL_Select;
        }
    }
}
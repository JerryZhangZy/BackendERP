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
 
    public partial class vender_vendorName : SelectListBase
    {
        public override string Name => "vender_vendorName";

        public vender_vendorName(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.VendorName, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.VendorName AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[Vendor] tbl
   
    WHERE COALESCE(tbl.VendorName,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.VendorName 
ORDER BY tbl.VendorName 
";
            return this.SQL_Select;
        }
    }
}

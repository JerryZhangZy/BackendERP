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
    public partial class so_shippingCarrier : SelectListBase
    {
        public override string Name => "so_shippingCarrier";

        public so_shippingCarrier(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"ShippingCarrier LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT ShippingCarrier AS [value], '' AS [text], COUNT(1) AS [count]
FROM SalesOrderHeaderInfo INNER JOIN SalesOrderHeader tbl ON SalesOrderHeaderInfo.SalesOrderUuid=tbl.SalesOrderUuid
WHERE {this.QueryObject.GetSQL()}
GROUP BY ShippingCarrier
ORDER BY [value]
";
            return this.SQL_Select;
        }
    }
}
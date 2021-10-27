using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SelectListQuery : QueryObject<SelectListQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = "tbl";

        protected QueryFilterRawSql _Term = new QueryFilterRawSql("COALESCE(tbl.customerCode, '') LIKE 'term%' ", PREFIX, true);
        public QueryFilterRawSql Term => _Term;

        public SelectListQuery() : base(PREFIX)
        {
            AddFilter(_Term);
        }

        public void SetTermSqlString(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                Term.SqlString = string.Empty;
                Term.Enable = false;
            }

        }
    }
}

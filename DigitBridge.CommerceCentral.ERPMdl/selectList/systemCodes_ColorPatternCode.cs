using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.customer
{
    public class systemCodes_ColorPatternCode : SelectListBase
    {
        public override string Name => "system_ColorPatternCode";

        public systemCodes_ColorPatternCode(IDataBaseFactory dbFactory)
            : base(dbFactory) 
        { }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      tbl.SystemCodeName AS [value],
      tbl.JsonFields AS [text]
    FROM
      [dbo].[SystemCodes] tbl
   
    WHERE COALESCE(tbl.SystemCodeName,'') = '{Name}' 
        AND {this.QueryObject.GetSQL()}
    ORDER BY tbl.SystemCodeName 
";
            return this.SQL_Select;
        }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.SystemCodeName, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);
        }
    }
}

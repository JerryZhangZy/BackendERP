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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.po
{
 
    public partial class po_channelOrderID : SelectListBase
    {
        public override string Name => "po_channelOrderID";

        public po_channelOrderID(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(poh.ChannelOrderID, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      COALESCE(poh.ChannelOrderID,'') AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeaderInfo] poh
   INNER JOIN PoHeader tbl ON tbl.PoUuid=poh.PoUuid
    WHERE poh.ChannelOrderID != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY poh.ChannelOrderID
ORDER BY poh.ChannelOrderID ";
            return this.SQL_Select;
        }
    }
}

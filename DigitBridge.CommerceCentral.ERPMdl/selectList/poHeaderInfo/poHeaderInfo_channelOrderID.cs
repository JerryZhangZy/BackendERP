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

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.poHeaderInfo
{
 
    public partial class poHeaderInfo_channelOrderID : SelectListBase
    {
        public override string Name => "poHeaderInfo_channelOrderID";

        public poHeaderInfo_channelOrderID(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = false;
            if (!string.IsNullOrEmpty(this.QueryObject.Term.FilterValue))
                this.QueryObject.SetTermSqlString(
                    $"COALESCE(tbl.ChannelOrderID, '') LIKE '{this.QueryObject.Term.FilterValue.ToSqlSafeString()}%' "
                );
            else
                this.QueryObject.SetTermSqlString(null);

        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
 
    SELECT 
      COALESCE(tbl.ChannelOrderID,'') AS [value],
      '' AS[text],
        COUNT(1) AS [count] FROM
      [dbo].[PoHeader] tbl
   
    WHERE COALESCE(tbl.ChannelOrderID,'') != '' 
        AND {this.QueryObject.GetSQL()}
    GROUP BY tbl.ChannelOrderID
ORDER BY tbl.ChannelOrderID 
";
            return this.SQL_Select;
        }
    }
}

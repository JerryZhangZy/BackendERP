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
    public partial class system_channel : SelectListBase
    {
        public override string Name => "system_channel";

        public system_channel(IDataBaseFactory dbFactory) : base(dbFactory) { }

        protected override void SetFilterSqlString()
        {
            this.QueryObject.LoadAll = true;
            this.QueryObject.SetTermSqlString(null);
        }

        protected override string GetSQL_select()
        {
            this.SetFilterSqlString();
            this.SQL_Select = $@"
SELECT 
ChannelNum AS [value], 
ChannelName AS [text], 
1 AS [count],
Category AS [category], 
PlatformNum AS [platformNum], 
ChannelCurrency AS [channelCurrency]
FROM setting_channel tbl
WHERE {this.QueryObject.GetSQL()}
ORDER BY ChannelNum
";
            return this.SQL_Select;
        }
    }
}

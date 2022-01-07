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
    public partial class system_channelAccount : SelectListBase
    {
        public override string Name => "system_channelAccount";

        public system_channelAccount(IDataBaseFactory dbFactory) : base(dbFactory) { }

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
tbl.ChannelAccountNum AS [value], 
RTRIM(tbl.ChannelAccountName) + ' ( ' + chl.ChannelName + ' )' AS [text], 
1 AS [count],
tbl.ChannelAccountName AS [channelAccountName],
chl.ChannelNum AS [channelNum],
chl.ChannelName AS [channelName]
FROM setting_channelAccount tbl
INNER JOIN setting_channel chl ON (chl.channelNum = tbl.channelNum AND tbl.MasterAccountNum = chl.MasterAccountNum AND tbl.ProfileNum = chl.ProfileNum)
WHERE {this.QueryObject.GetSQL()}
ORDER BY tbl.ChannelNum, tbl.ChannelAccountNum
";
            return this.SQL_Select;
        }
    }
}

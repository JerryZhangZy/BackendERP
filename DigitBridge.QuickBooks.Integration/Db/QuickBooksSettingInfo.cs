              
    

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
using DigitBridge.QuickBooks.Integration.Model;

namespace DigitBridge.QuickBooks.Integration
{
    public partial class QuickBooksSettingInfo
	{
        public IList<QboChnlAccSetting> QboChnlAccSettings { get; set; }
        public QboIntegrationSetting QboIntegrationSetting { get; set; }

		public override QuickBooksSettingInfo ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			if (string.IsNullOrEmpty(IntegrationSettingJsonFields))
			{
				QboIntegrationSetting = JsonConvert.DeserializeObject<QboIntegrationSetting>(IntegrationSettingJsonFields);
			}
			if (string.IsNullOrEmpty(ChannelAccountSettingJsonFields))
			{
				QboChnlAccSettings = JsonConvert.DeserializeObject<List<QboChnlAccSetting>>(ChannelAccountSettingJsonFields);
			}
			return this;
		}
	}
}




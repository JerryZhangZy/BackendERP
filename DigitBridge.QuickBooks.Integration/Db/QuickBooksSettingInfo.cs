              
    

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
        public QboIntegrationSetting SettingInfo { get; set; }

		public override QuickBooksSettingInfo ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			if (string.IsNullOrEmpty(JsonFields))
			{
				SettingInfo = JsonConvert.DeserializeObject<QboIntegrationSetting>(JsonFields);
			}
			Fields.LoadFromValueString(JsonFields);
			return this;
		}

		public override QuickBooksSettingInfo ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}
	}
}




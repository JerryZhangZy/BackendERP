              
    

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
    public partial class QuickBooksIntegrationSetting
    {
		public QboIntegrationSetting QboIntegrationSetting { get; set; }

		public override QuickBooksIntegrationSetting ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			if (string.IsNullOrEmpty(JsonFields))
			{
				QboIntegrationSetting = JsonConvert.DeserializeObject<QboIntegrationSetting>(JsonFields);
			}
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override QuickBooksIntegrationSetting ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			//if (QboIntegrationSetting != null)
			//{
			//	JsonFields = JsonConvert.SerializeObject(QboIntegrationSetting);
			//}
			JsonFields = Fields.ToValueString();
			return this;
		}
	}
}




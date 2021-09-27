              
    

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
    public partial class QuickBooksChnlAccSetting
	{
		public QboChnlAccSetting QboChnlAccSetting { get; set; }

		public override QuickBooksChnlAccSetting ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
            if (string.IsNullOrEmpty(JsonFields))
            {
				QboChnlAccSetting = JsonConvert.DeserializeObject<QboChnlAccSetting>(JsonFields);
			}
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override QuickBooksChnlAccSetting ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
   //         if (QboChnlAccSetting != null)
   //         {
			//	JsonFields = JsonConvert.SerializeObject(QboChnlAccSetting);
			//}
			JsonFields = Fields.ToValueString();
			return this;
		}
	}
}




              
    

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

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class SystemSetting
    {
		public override SystemSetting ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override SystemSetting ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			UpdateDateUtc = DateTime.UtcNow;
			return this;
		} 
	}
}




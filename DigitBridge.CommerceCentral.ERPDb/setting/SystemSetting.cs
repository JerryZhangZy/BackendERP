              
    

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
		 
		private ErpSettingClass _erpSetting;
		/// <summary>
		/// Code description,
		/// </summary>
		public virtual ErpSettingClass ErpSetting
		{
			get
			{
				return _erpSetting;
			}
			set
			{
				_erpSetting = value;
				OnPropertyChanged("ErpSetting", value);
			}
		}

		public override SystemSetting ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			ErpSetting = this.JsonFields.JsonToObject<ErpSettingClass>();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override SystemSetting ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			
			JsonFields = ErpSetting.ObjectToString(); 
			UpdateDateUtc = DateTime.UtcNow;
			return this;
		} 
	}
}




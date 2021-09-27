              
    

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
using UneedgoHelper.DotNet.Common;

namespace DigitBridge.QuickBooks.Integration
{
    public partial class QuickBooksConnectionInfo
    {
		public override QuickBooksConnectionInfo ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			ClientId = CryptoUtility.DecrypTextTripleDES(ClientId, MyAppSetting.CryptKey);
			ClientSecret = CryptoUtility.DecrypTextTripleDES(ClientSecret, MyAppSetting.CryptKey);
			AuthCode = CryptoUtility.DecrypTextTripleDES(AuthCode, MyAppSetting.CryptKey);
			RealmId = CryptoUtility.DecrypTextTripleDES(RealmId, MyAppSetting.CryptKey);
			return this;
		}
		public override QuickBooksConnectionInfo ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			ClientId = CryptoUtility.EncrypTextTripleDES(ClientId, MyAppSetting.CryptKey);
			ClientSecret = CryptoUtility.EncrypTextTripleDES(ClientSecret, MyAppSetting.CryptKey);
			AuthCode = CryptoUtility.EncrypTextTripleDES(AuthCode, MyAppSetting.CryptKey);
			RealmId = CryptoUtility.EncrypTextTripleDES(RealmId, MyAppSetting.CryptKey);
			return this;
		}
	}
}



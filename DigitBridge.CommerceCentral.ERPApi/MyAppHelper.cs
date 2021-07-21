using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    public class MyAppHelper
    {
        public static DataBaseFactory GetDatabase(int masterAccountNum)
        {
            var connectStr = GetConnectionString(masterAccountNum);
            ConfigurationManager.AppSettings["dsn"] = connectStr;
            return new DataBaseFactory(connectStr);
        }

        public static string GetConnectionString(int masterAccountNum)
        {
            return ConfigHelper.Dsn;
        }
    }
}

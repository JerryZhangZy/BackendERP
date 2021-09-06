using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ApiCommon
{
    public static class MyAppHelper
    {
        public async static Task<IDataBaseFactory> CreateDefaultDatabaseAsync(IPayload payload)
        {
            var config = await MyCache.GetCommerceCentralDbConnSetting(payload.MasterAccountNum);
            payload.DatabaseNum = config.DatabaseNum;
            return DataBaseFactory.CreateDefault(config);
        }

        public async static Task<IDataBaseFactory> CreateDefaultDatabaseAsync(int masterAccountNum)
        {
            var config = await MyCache.GetCommerceCentralDbConnSetting(masterAccountNum);
            return DataBaseFactory.CreateDefault(config);
        }

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

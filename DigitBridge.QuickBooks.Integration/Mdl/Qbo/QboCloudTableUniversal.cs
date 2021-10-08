using DigitBridge.Blob;
using DigitBridge.CommerceCentral.AzureStorage;
using DigitBridge.QuickBooks.Integration.Model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public static class QboCloudTableUniversal
    {
        private static TableUniversal<OAuthMapInfo> authTableUniversal;

        public static async Task<TableUniversal<OAuthMapInfo>> AuthTableUniversal()
        {
            if (authTableUniversal == null)
                authTableUniversal = await TableUniversal<OAuthMapInfo>.CreateAsync(MyAppSetting.OAuthTableName, MyAppSetting.AzureWebJobsStorage);
            return authTableUniversal;
        }
        //private static async Task<MsAzureTableOperator> GetOAuthTableOperator()
        //{
        //    return await MsAzureTableOperator.CreateTableAsync(MyAppSetting.OAuthTableName, MyAppSetting.AzureWebJobsStorage, true); ;
        //}

        //private static async Task<MsAzureTableOperator> GetDebugTableOperator()
        //{
        //    return await MsAzureTableOperator.CreateTableAsync(MyAppSetting.DebugTableName, MyAppSetting.AzureWebJobsStorage, true); ;
        //}
    }
}

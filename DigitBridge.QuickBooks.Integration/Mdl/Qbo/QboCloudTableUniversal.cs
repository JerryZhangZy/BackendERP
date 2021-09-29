using DigitBridge.Blob;
using DigitBridge.QuickBooks.Integration.Model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboCloudTableUniversal
    {
        public static async Task<OAuthMapInfo> CreateOAuthMapInfo(string requestState, int masterAccountNum,int profileNum)
        {
            var table = await MsAzureTableOperator.CreateTableAsync(MyAppSetting.OAuthTableName, MyAppSetting.AzureWebJobsStorage, true);
            var request = new OAuthMapInfo()
            {
                MasterAccountNum = masterAccountNum,
                ProfileNum = profileNum,
                RequestState = requestState,
                RowKey = requestState,
                PartitionKey = masterAccountNum.ToString(),
                Timestamp = DateTimeOffset.UtcNow
            };
            return (OAuthMapInfo)await table.InsertOrMergeEntityAsync<OAuthMapInfo>(request);
        }

        public static async Task<OAuthMapInfo> QueryOAuthMapInfo(string requestState)
        {
            var table = await MsAzureTableOperator.CreateTableAsync(MyAppSetting.OAuthTableName, MyAppSetting.AzureWebJobsStorage, true);
            var query = new TableQuery<OAuthMapInfo>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, requestState));
            return table._cloudTable.ExecuteQuery(query).FirstOrDefault();
        }

        public static async Task DeleteOAuthMapInfo(OAuthMapInfo info)
        {
            var table = await MsAzureTableOperator.CreateTableAsync(MyAppSetting.OAuthTableName, MyAppSetting.AzureWebJobsStorage, true);
            var delete = TableOperation.Delete(info);
            await table._cloudTable.ExecuteAsync(delete);
        }
    }
}

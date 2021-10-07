using DigitBridge.Blob;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public class AzureTableUniversal<T> where T: TableEntity
    {
        protected MsAzureTableOperator tableOperator;

        private AzureTableUniversal(MsAzureTableOperator azureTableOperator)
        {
            tableOperator = azureTableOperator;
        }

        public static async Task<AzureTableUniversal<T>> CreateAsync(string tableName, string storageAccountKey, bool createIfNotExists = true)
        {
            var tableOperator = await MsAzureTableOperator.CreateTableAsync(tableName, storageAccountKey, createIfNotExists);
            return new AzureTableUniversal<T>(tableOperator);
        }

        public async Task<T> AddOrUpdateAsync(T entity) => (T)await tableOperator.InsertOrMergeEntityAsync<T>(entity);

        public async Task DeleteAsync(TableEntity entity)
        {
            var delete = TableOperation.Delete(entity);
            await tableOperator._cloudTable.ExecuteAsync(delete);
        }

        public async Task<TableEntity> GetByRowKey(string rowKey)
        {
            var query = new TableQuery<TableEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));
            return tableOperator._cloudTable.ExecuteQuery(query).FirstOrDefault();
        }

        //public async Task<IList<TableEntity>> QueryAsync(string key,string value)
        //{
        //    var query = new TableQuery<TableEntity>().Where($"{key} eq '{value}'");
        //    return tableOperator._cloudTable.ExecuteQuery(query).ToList();
        //}
    }
}

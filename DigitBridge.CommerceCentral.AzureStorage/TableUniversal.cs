using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public class TableUniversal
    {
        protected TableClient tableClient;

        private TableUniversal(TableClient client)
        {
            tableClient = client;
        }

        public static async Task<TableUniversal> CreateAsync(string tableName, string connectionString, bool createIfNotExists = true)
        {
            var client = new TableClient(connectionString, tableName);
            if (createIfNotExists)
                await client.CreateIfNotExistsAsync();
            return new TableUniversal(client);
        }

        public async Task<Response> AddEntityAsync(TableEntity entity)
        {
            return await tableClient.AddEntityAsync(entity);
        }

        public async Task<Response> UpSertEntityAsync(TableEntity entity)
        {
            var source =await GetEntityAsync(entity.RowKey, entity.PartitionKey);
            return await tableClient.UpsertEntityAsync(entity);
        }

        public async Task<Response> UpdateEntityAsync(TableEntity entity)
        {
            var source =await GetEntityAsync(entity.RowKey, entity.PartitionKey);
            return await tableClient.UpdateEntityAsync(entity,source.ETag);
        }

        public async Task<TableEntity> GetEntityAsync(string rowKey , string partitionKey )
        {
            try
            {
                return await tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<Response> DeleteEntityAsync(string rowKey,string partitionKey)
        {
            return await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public Pageable<TableEntity> QueryEntityAsync(string key,string value)
        {
            return tableClient.Query<TableEntity>($"{key} eq '{value}'");
        }
    }
}

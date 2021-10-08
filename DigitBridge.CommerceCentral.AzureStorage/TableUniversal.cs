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
    public class TableUniversal<T>
    {
        protected TableClient tableClient;

        private const string ExtFilelds = "JsonFileds";

        private TableUniversal(TableClient client)
        {
            tableClient = client;
        }

        public static async Task<TableUniversal<T>> CreateAsync(string tableName, string connectionString, bool createIfNotExists = true)
        {
            var client = new TableClient(connectionString, tableName);
            if (createIfNotExists)
                await client.CreateIfNotExistsAsync();
            return new TableUniversal<T>(client);
        }

        public async Task AddEntityAsync(T item,string partitionKey,string rowKey)
        {
            var entity = new TableEntity(partitionKey, rowKey);
            entity.Add(ExtFilelds, JsonConvert.SerializeObject(item));
            await tableClient.AddEntityAsync(entity);
        }

        public async Task<Response> UpSertEntityAsync(T item, string partitionKey, string rowKey)
        {
            var entity = await GetTableEntityAsync(rowKey, partitionKey);
            var fileds = JsonConvert.SerializeObject(item);
            if (entity == null)
            {
                entity = new TableEntity(partitionKey, rowKey);
                entity.Add(ExtFilelds, fileds);
            }
            else
            {
                entity[ExtFilelds] = fileds;
            }
            return await tableClient.UpsertEntityAsync(entity);
        }

        public async Task<bool> UpdateEntityAsync(T item, string partitionKey, string rowKey)
        {
            var entity =await GetTableEntityAsync(rowKey, partitionKey);
            if (entity != null)
            {
                entity[ExtFilelds] = JsonConvert.SerializeObject(item);
                await tableClient.UpdateEntityAsync(entity,entity.ETag);
                return true;
            }
            return false;
        }

        private async Task<TableEntity> GetTableEntityAsync(string rowKey , string partitionKey )
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

        public async Task<T> GetEntityAsync(string rowKey , string partitionKey )
        {
            var entity = await GetTableEntityAsync(rowKey, partitionKey);
            if (entity != null)
                return JsonConvert.DeserializeObject<T>(entity.GetString(ExtFilelds));
            return default;
        }

        public T GetEntityByRowKey(string rowKey)
        {
            return QueryEntityByRowKey(rowKey).FirstOrDefault();
        }

        public async Task DeleteEntityAsync(string rowKey,string partitionKey)
        {
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public IList<T> QueryEntityByPartitionKey(string partitionKey)
        {
             var queryResult= tableClient.Query<TableEntity>(e=>e.PartitionKey==partitionKey);
            return ConvertToEntities(queryResult);
        }

        public IList<T> QueryEntityByRowKey(string rowKey)
        {
             var queryResult= tableClient.Query<TableEntity>(e=>e.RowKey== rowKey);
            return ConvertToEntities(queryResult);
        }

        private IList<T> ConvertToEntities(Pageable<TableEntity> tableList)
        {
            var list = new List<T>();
            foreach (var item in tableList)
            {
                list.Add(JsonConvert.DeserializeObject<T>(item.GetString(ExtFilelds)));
            }
            return list;
        }
    }
}

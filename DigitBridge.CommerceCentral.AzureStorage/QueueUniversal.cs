using Azure.Storage.Queues;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public class QueueUniversal<T> where T : class, IQueueEntity, new()
    {
        protected QueueClient queueClient;

        private QueueUniversal(QueueClient client)
        {
            queueClient = client;
        }

        public static async Task<QueueUniversal<T>> CreateAsync(string queueName, string connectionString)
        {
            var client = new QueueClient(connectionString, queueName.ToLowerInvariant());
            await client.CreateIfNotExistsAsync();
            return new QueueUniversal<T>(client);
        }

        public static async Task SendMessageAsync(string queueName, string connectionString, T entity)
        {
            var client = new QueueClient(connectionString, queueName, new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64 });
            await client.CreateAsync();
            var message = JsonConvert.SerializeObject(entity);
            var result = await client.SendMessageAsync(message);
        }
        public static void SendMessage(string queueName, string connectionString, T entity)
        {
            var client = new QueueClient(connectionString, queueName.ToLowerInvariant());
            client.CreateIfNotExists();
            var message = JsonConvert.SerializeObject(entity);
            client.SendMessage(message);
        }

        public async Task SendMessageAsync(string message)
        {
            await queueClient.SendMessageAsync(message);
        }

        public async Task SendMessageAsync(T entity)
        {
            var message = JsonConvert.SerializeObject(entity);
            await queueClient.SendMessageAsync(message);
        }

        //public async Task<T> PeekMessage()
        //{
        //    var message =await queueClient.PeekMessageAsync();
        //    if (message.Value != null)
        //        return JsonConvert.DeserializeObject<T>(message.Value.MessageText);
        //    return default;
        //}

        //public async Task<IList<T>> PeekMessages(int maxMessages=10)
        //{
        //    var messages =await queueClient.PeekMessagesAsync(maxMessages);
        //    var list = new List<T>();
        //    foreach(var message in messages.Value)
        //    {
        //        list.Add(JsonConvert.DeserializeObject<T>(message.MessageText));
        //    }
        //    return list;
        //}

        public async Task<T> ReceiveMessageAsync()
        {
            var message = await queueClient.ReceiveMessageAsync();
            if (message.Value != null)
                return message.Value.QueueMessageToEntity<T>();
            return default;
        }

        /// <summary>
        /// 获取QueueMessages
        /// </summary>
        /// <param name="maxMessages">min=1,max=32</param>
        /// <returns></returns>
        public async Task<IList<T>> ReceiveMessagesAsync(int maxMessages = 10)
        {
            var messages = await queueClient.ReceiveMessagesAsync(maxMessages);
            var list = new List<T>();
            foreach (var message in messages.Value)
            {
                list.Add(message.QueueMessageToEntity<T>());
            }
            return list;
        }

        public async Task DeleteMessageAsync(T message)
        {
            await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.AzureQueue.QueueMessages
{
    public interface IMessageSerializer
    {
        T Desrialize<T>(string message);
        string Serialize(object obj);
    }

    public class JsonMessageSerializer : IMessageSerializer
    {
        public T Desrialize<T>(string message)
        {
            var obj = JsonConvert.DeserializeObject<T>(message);
            return obj;
        }

        public string Serialize(object obj)
        {
            var message = JsonConvert.SerializeObject(obj);
            return message;
        }
    }
}

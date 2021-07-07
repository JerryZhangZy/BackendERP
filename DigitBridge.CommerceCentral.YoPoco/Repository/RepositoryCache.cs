using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class RepositoryCache
    {
        private readonly Dictionary<string, object> _Cache = new Dictionary<string, object>();

        public RepositoryCache()
        {
            _Cache.Clear();
        }

        public void ClearAll()
        {
            _Cache.Clear();
        }
        public string GenerateKey(string typeName, string key)
            => $"{typeName}:{key}";

        public void RemoveKey<T>(string key)
        {
            if (string.IsNullOrEmpty(key) || (_Cache is null))
                return;
            var k = GenerateKey(typeof(T).Name, key);
            if (_Cache.ContainsKey(k))
                _Cache.Remove(k);
        }

        public T SetData<T>(string key, T objValue)
        {
            if (string.IsNullOrEmpty(key) || (_Cache is null))
                return default;
            var k = GenerateKey(typeof(T).Name, key);
            _Cache[k] = objValue;
            return objValue;
        }

        public T GetData<T>(string key)
        {
            if (_Cache is null)
                return default;
            var k = GenerateKey(typeof(T).Name, key);

            if (!_Cache.TryGetValue(k, out var retValue))
                return default;

            return (T)retValue;
        }

        public T FromCache<T>(string key, Func<T> create)
        {
            var k = GenerateKey(typeof(T).Name, key);

            if (_Cache != null)
            {
                if (_Cache.TryGetValue(k, out var value))
                    return (T)value;
            }

            if (create != null)
            {
                var objValue = create();
                if (objValue != null)
                    SetData(key, objValue);
                return objValue;
            }
            return default;
        }

        public bool HasData<T>(string key)
        {
            if (_Cache == null)
                return false;
            var k = GenerateKey(typeof(T).Name, key);
            return _Cache.ContainsKey(k);
        }
    }
}

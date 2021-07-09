using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class TransactionalCache
    {
        private readonly Dictionary<string, object> _Cache = new Dictionary<string, object>();

        public TransactionalCache()
        {
            _Cache.Clear();
        }

        public void ClearAll()
        {
            _Cache.Clear();
        }

        public void RemoveKey(string key)
        {
            if (string.IsNullOrEmpty(key) || (_Cache == null))
                return;

            if (_Cache.ContainsKey(key))
                _Cache.Remove(key);
        }

        public T SetData<T>(string key, T objValue)
        {
            if (string.IsNullOrEmpty(key) || (_Cache == null))
                return default;

            _Cache[key] = objValue;
            return objValue;
        }

        public T GetData<T>(string key)
        {
            if (_Cache == null)
                return default;

            if (!_Cache.TryGetValue(key, out var retValue))
                return default;

            return (T)retValue;
        }

        public T FromCache<T>(string key, Func<T> create, bool reNew = false, bool useDefault = false)
        {
            if (_Cache != null && !reNew)
            {
                if (_Cache.TryGetValue(key, out var value))
                    return (T)value;
            }

            if (useDefault)
                SetData(key, default(T));
            var objValue = create();
            if (objValue != null)
                SetData(key, objValue);
            return objValue;
        }

        public T FromCacheValueOnly<T>(string key, Func<T> create)
        {
            return FromCache(key, create, false);
        }

        public bool HasData(string key)
        {
            if (_Cache == null)
                return false;

            return _Cache.ContainsKey(key);
        }
    }
}

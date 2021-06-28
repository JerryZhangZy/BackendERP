using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DigitBridge.Base.Utility
{
    public static class DictionaryExtensions
    {
        public static string DictionaryToString(this IDictionary dictionary)
        {
            return dictionary == null || dictionary.Count <= 0
                ? string.Empty
                : string.Join(",", dictionary.Keys.Cast<string>().Select(k => $"{k}: {dictionary[k].ToString()}"));
        }

        public static T GetValue<K, T>(this IDictionary<K, T> dictionary, K key)
        {
            var retVal = default(T);
            dictionary?.TryGetValue(key, out retVal);
            return retVal;
        }

        public static T GetOrAddValue<K, T>(this IDictionary<K, T> dictionary, K key, Func<T> creator)
        {
            var retVal = default(T);
            if (dictionary == null)
                return retVal;

            if (dictionary.TryGetValue(key, out retVal)) return retVal;
            retVal = creator();
            dictionary[key] = retVal;

            return retVal;
        }

        public static void SetValue<K, T>(this IDictionary<K, T> dictionary, K key, T value)
        {
            if (dictionary == null)
                return;

            dictionary[key] = value;
        }

        public static void SetValue<K, T>(this IDictionary<K, T> dictionary, K key, Func<T> value)
        {
            if ((dictionary == null) || (value == null))
                return;

            dictionary[key] = value();
        }

        public static void TryGetSetting<K, T>(this IDictionary<K, T> dictionary, K key, Action<T> onValue, Action onFail)
        {
            if (dictionary.TryGetValue(key, out var value))
                onValue((T)value);
            else
                onFail?.Invoke();
        }

        public static Dictionary<K, T> Clone<K, T>(this IDictionary<K, T> dictionary)
        {
            if (dictionary == null)
                return null;

            var clone = new Dictionary<K, T>(dictionary.Count);
            foreach (var keyValuePair in dictionary)
            {
                var value = keyValuePair.Value;
                if (value is ICloneable)
                    value = (T)((ICloneable)value).Clone();

                clone[keyValuePair.Key] = value;
            }

            return clone;
        }

        public static T GetValue<T>(this string value, T defaultValue = default(T))
        {
            var type = typeof(T);
            var isNullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            try
            {
                if (value == null)
                    return defaultValue;

                if (isNullable)
                    type = Nullable.GetUnderlyingType(type);

                if (type == typeof(Guid))
                {
                    return (T)(object)Guid.Parse(value);
                }

                if (type.IsEnum)
                {
                    return (T)Enum.Parse(type, value, true);
                }

                return (T)System.Convert.ChangeType(value, type);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private static readonly Dictionary<Type, Func<object, object, object>> _converters = new Dictionary<Type, Func<object, object, object>>
        {
            { typeof(Guid), (o,c) => { Guid v; Guid.TryParse(o.ToString(), out v); return v; } }
        };

        public static Dictionary<Type, Func<object, object, object>> Converters => _converters;

    }

}
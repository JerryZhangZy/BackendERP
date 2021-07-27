using Newtonsoft.Json.Linq;
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
        public static bool IsEqualTo<K, T>(this IDictionary<K, T> src, IDictionary<K, T> dst)
        {
            if (ReferenceEquals(src, dst))
                return true;

            if ((src == null) || (dst == null) || (src.Count != dst.Count))
                return false;

            foreach (KeyValuePair<K, T> srcKeyValuePair in src)
            {
                var dstValue = dst.GetValue(srcKeyValuePair.Key);
                if (dstValue == null) return false;
                if (dstValue is IList)
                {
                    var dstArr = JArray.FromObject(dstValue);
                    var srcArr = JArray.FromObject(srcKeyValuePair.Value);
                    if (dstArr.Count != srcArr.Count) return false;
                    for (int i = 0; i < srcArr.Count; i++)
                    {
                        if (!JToken.DeepEquals(srcArr[i], dstArr[i]))
                            return false;
                    }
                }
                else
                {
                    if (!Equals(srcKeyValuePair.Value, dstValue))
                        return false;
                }
            }
            return true;
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


        public static R Maybe<T, R>(this T item, Func<T, R> selector)
        {
            return Maybe(item, selector, null);
        }

        public static R Maybe<T, R>(this T item, Func<T, R> selector, Func<R> defaultFunc)
        {
            if (item == null)
                return defaultFunc != null ? defaultFunc() : default(R);

            return selector(item);
        }


        public static void RemoveKey(this Dictionary<string, object> dict, string key)
        {
            if (!string.IsNullOrEmpty(key) && dict != null && dict.ContainsKey(key))
                dict.Remove(key);
        }

        public static T SetData<T>(this Dictionary<string, object> dict, string key, T objValue)
        {
            if (string.IsNullOrEmpty(key) || (dict == null))
                return default;

            dict[key] = objValue;
            return objValue;
        }

        public static T GetData<T>(this Dictionary<string, object> dict, string key)
        {
            if (dict == null)
                return default;

            if (!dict.TryGetValue(key, out var retValue))
                return default;

            return (T)retValue;
        }

        public static T GetFirstData<T>(this Dictionary<string, object> dict)
        {
            return (dict == null || dict.Count <= 0)
                ? default
                : (T)dict.ElementAt(0).Value;
        }

        public static bool HasData(this Dictionary<string, object> dict, string key)
        {
            if (dict == null)
                return false;

            return dict.ContainsKey(key);
        }

    }

}
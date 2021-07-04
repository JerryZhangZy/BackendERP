using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DigitBridge.Base.Utility
{
    /// <summary>
    /// This class here is for the convenience to copy code.
    /// Don't use it in brand new code. Use BaseTypeExtension or other extension methods
    /// </summary>
    public class ObjectConvert
    {
        public static DateTime MinDatatime = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        public static DateTime MaxDatatime = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

        public static TimeSpan MinTime = new TimeSpan(0,0,0);
        public static TimeSpan MaxTime = new TimeSpan(11,59,59);

        /// <summary>
        /// [True, T, Yes, Y, 1] are true, all the others are false.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ToBoolean(object input)
        {
            var upperInput = ToTrimString(input).ToUpper();
            if (upperInput == "TRUE" || upperInput == "T" || upperInput == "YES"
                || upperInput == "Y" || upperInput == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DateTime ToDateTime(object input)
        {
            var returnValue = DateTime.MinValue;
            try
            {
                if (input != null)
                {
                    returnValue = Convert.ToDateTime(input);
                }
            }
            catch
            {
            }

            return returnValue;
        }

        public static DateTime ToDateTime(string input, string format)
        {
            var returnValue = DateTime.MinValue;
            try
            {
                if (input != null)
                {
                    returnValue = DateTime.ParseExact(input, format, null);
                }
            }
            catch
            {
            }

            return returnValue;
        }

        public static string ToTrimString(object input)
        {
            return (input is null)
                ? string.Empty
                : input.ToString().Trim();
        }

        /// <summary>
        /// Covert the expression to decimal, then convert to int32
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt(object input)
        {
            return ToInt(input, 0);
        }

        /// <summary>
        /// Covert the expression to decimal, then convert to int32
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt(object input, int defaultVal)
        {
            try
            {
                return decimal.ToInt32(ToDecimal(input, defaultVal));
            }
            catch
            {
                return defaultVal;
            }
        }

        public static int ToInt32(object input, int defaultVal)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static byte ToByte(object input, byte defaultVal)
        {
            try
            {
                return Convert.ToByte(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static short ToInt16(object input, short defaultVal)
        {
            try
            {
                return Convert.ToInt16(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static long ToInt64(object input, long defaultVal)
        {
            try
            {
                return Convert.ToInt64(input);
            }
            catch
            {
                return defaultVal;
            }
        }

        public static decimal ToDecimal(object input, decimal defaultVal)
        {
            try
            {
                if (input == null)
                {
                    //null will be converted to zero, this is not the expected behavior.
                    return defaultVal;
                }

                if (IsUsaCurrency(input)) //Added by Yunman 2015/4/27 for currency to decimal
                {
                    return decimal.Parse(input.ToString(), System.Globalization.NumberStyles.Currency);
                }
                else
                {
                    return Convert.ToDecimal(input);
                }
            }
            catch
            {
                return defaultVal;
            }
        }

        public static short ToInt16(object input)
        {
            return Convert.ToInt16(input);
        }

        public static long ToInt64(object input)
        {
            return Convert.ToInt64(input);
        }

        public static decimal ToDecimal(object input)
        {
            return ToDecimal(input, 0);
        }

        /// <summary>
        /// If the input is not null, convert it to a string and trim it.
        /// Otherwise, return an empty string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Trim(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return input.ToString().Trim();
        }


        private static readonly Dictionary<Type, Func<object, object, object>> _converters =
            new Dictionary<Type, Func<object, object, object>>
            {
                {
                    typeof(Guid), (o, c) =>
                    {
                        Guid v;
                        Guid.TryParse(o.ToString(), out v);
                        return v;
                    }
                }
            };

        public static object ConvertObject(object value, Type type, object context = null)
        {
            try
            {
                if (value == DBNull.Value)
                    value = null;

                if (type is { })
                    return value;

                if ((value == null) || ((value is string s) && (type != typeof(string)) && string.IsNullOrEmpty(s)))
                    return (type.IsClass ||
                            (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        ? null
                        : Activator.CreateInstance(type);

                type = Nullable.GetUnderlyingType(type) ?? type;
                if (type.IsInstanceOfType(value))
                    return value;

                if (type.IsEnum)
                {
                    return value.GetType().IsValueType
                        ? Enum.ToObject(type, value)
                        : Enum.Parse(type, value.ToString());
                }

                var converter = _converters.GetValue(type);
                return converter != null ? converter(value, context) : System.Convert.ChangeType(value, type);
            }
            catch (Exception)
            {
                return (type.IsClass || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    ? null
                    : Activator.CreateInstance(type);
            }
        }

        public static bool IsUsaCurrency(object source, StringComparison culture = StringComparison.CurrentCulture)
        {
            if (source.ToString().StartsWith("$", culture))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public static class ObjectExtensions
    {
        public static bool EqualsIgnoreSpace(this string source, string target, bool ignoreLeadingSpace = true, bool ignoreCase = true)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                return (string.IsNullOrWhiteSpace(target) && string.IsNullOrWhiteSpace(source)) ? true : false;

            return string.Equals(
                ignoreLeadingSpace ? source.Trim() : source.TrimEnd(),
                ignoreLeadingSpace ? target.Trim() : target.TrimEnd(),
                ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
        }

        public static object ConvertObject(this object value, Type type, object context = null) =>
            ObjectConvert.ConvertObject(value, type, context);

        public static T ConvertObject<T>(this object value, object context = null)
        {
            var obj = ObjectConvert.ConvertObject(value, typeof(T), context);
            return (obj is null) ? default(T) : (T) obj;
        }

        /// <summary>
        /// Convert IList to one string with separator
        /// </summary>
        public static string JoinToString<T>(this IEnumerable<T> lst, string separator)
        {
            if (lst == null || !lst.Any()) return string.Empty;
            var l = lst.ToList();
            if (l.Count == 1) return l[0].ToString();
            return string.Join<T>(separator, lst);
        }
        public static string JoinToString<T>(this IEnumerable<T> lst)
        {
            return lst.JoinToString<T>(",");
        }

        /// <summary>
        /// Convert IList to one string with separator
        /// </summary>
        public static string JoinToSqlInStatementString<T>(this IList<T> lst)
        {
            var separator = ",";
            if (lst == null || lst.Count <= 0) return string.Empty;
            var isfirstTime = true;
            var sb = new StringBuilder();
            foreach (var item in lst)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.ToString())) continue;
                if (!isfirstTime) sb.Append(separator);
                sb.AppendFormat("'{0}'", item.ToString().Trim().ToSqlSafeString());
                isfirstTime = false;
            }
            if (sb.Length > 1)
            {
                sb.Insert(0, "(");
                sb.Append(")");
            }
            return sb.ToString();
        }

        public static string ToSqlSafeString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str) || !str.Contains("'")) return str.TrimEnd();
            return str.TrimEnd().Replace("'", "''");
        }

        public static string ToFileNameSafeString(this string fileName)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c.ToString(), "");
            }
            return fileName;
        }

        /// <summary>
        /// Covert string to fixed length
        /// Will auto extend or cut string lenth to fixed length;
        /// </summary>
        /// <returns></returns>
        public static string TruncateTo(this string str, int length)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            str = str.TrimEnd();
            return str.Length <= length ? str : str.Substring(0, length);
        }

        /// <summary>
        /// Covert string to fixed length
        /// Will auto extend or cut string lenth to fixed length;
        /// </summary>
        /// <returns></returns>
        public static string ToFixedLength(this string str, int length)
        {
            var len = length;
            var _len = str?.Length ?? 0;
            var _str = str ?? string.Empty;
            return (_len < len)
                ? _str.PadRight(len, ' ')
                : _str.Substring(0, len);
        }

        /// <summary>
        /// Get Datetime for sql server date range
        /// </summary>
        /// <returns></returns>
        public static DateTime? ToSqlSafeValue(this DateTime? netDateTime)
        {
            if (netDateTime == null) return ObjectConvert.MinDatatime;
            if (netDateTime <= ObjectConvert.MinDatatime) return ObjectConvert.MinDatatime;
            if (netDateTime >= ObjectConvert.MaxDatatime) return ObjectConvert.MaxDatatime;
            return netDateTime;
        }
        /// <summary>
        /// Get Datetime for sql server date range
        /// </summary>
        /// <returns></returns>
        public static DateTime ToSqlSafeValue(this DateTime netDateTime)
        {
            if (netDateTime == null) return ObjectConvert.MinDatatime;
            if (netDateTime <= ObjectConvert.MinDatatime) return ObjectConvert.MinDatatime;
            if (netDateTime >= ObjectConvert.MaxDatatime) return ObjectConvert.MaxDatatime;
            return netDateTime;
        }

        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static DateTime? MinValueSql(this DateTime? netDateTime) => (DateTime?)ObjectConvert.MinDatatime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static DateTime? MaxValueSql(this DateTime? netDateTime) => (DateTime?)ObjectConvert.MaxDatatime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static DateTime MinValueSql(this DateTime netDateTime) => ObjectConvert.MinDatatime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static DateTime MaxValueSql(this DateTime netDateTime) => ObjectConvert.MaxDatatime;

        /// <summary>
        /// Get Datetime for sql server date range
        /// </summary>
        /// <returns></returns>
        public static TimeSpan? ToSqlSafeValue(this TimeSpan? netDateTime)
        {
            if (netDateTime == null) return ObjectConvert.MinTime;
            if (netDateTime <= ObjectConvert.MinTime) return ObjectConvert.MinTime;
            if (netDateTime >= ObjectConvert.MaxTime) return ObjectConvert.MaxTime;
            return netDateTime;
        }

        /// <summary>
        /// Get Datetime for sql server date range
        /// </summary>
        /// <returns></returns>
        public static TimeSpan ToSqlSafeValue(this TimeSpan netDateTime)
        {
            if (netDateTime <= ObjectConvert.MinTime) return ObjectConvert.MinTime;
            if (netDateTime >= ObjectConvert.MaxTime) return ObjectConvert.MaxTime;
            return netDateTime;
        }
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static TimeSpan? MinValueSql(this TimeSpan? netDateTime) => ObjectConvert.MinTime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static TimeSpan? MaxValueSql(this TimeSpan? netDateTime) => ObjectConvert.MaxTime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static TimeSpan MinValueSql(this TimeSpan netDateTime) => ObjectConvert.MinTime;
        /// <summary>
        /// Get min Datetime for sql server
        /// </summary>
        /// <returns></returns>
        public static TimeSpan MaxValueSql(this TimeSpan netDateTime) => ObjectConvert.MaxTime;


        public static DateTime ToDateTime(this DateTime? input) => (input is null) ? ObjectConvert.MinDatatime : (DateTime)input;
        public static TimeSpan ToTimeSpan(this DateTime? input) => (input is null) ? ObjectConvert.MinTime : ((DateTime)input).TimeOfDay;
        public static DateTime ToDateTime(this TimeSpan? input) => (input is null) ? DateTime.Today : DateTime.Today + (TimeSpan)input;
        public static DateTime ToDateTime(this TimeSpan input) => DateTime.Today + input;

        public static int ToInt(this int? input) => (input is null) ? default(int) : (int)input;
        public static long ToLong(this long? input) => (input is null) ? default(long) : (long)input;
        public static decimal ToDecimal(this decimal? input) => (input is null) ? default(decimal) : (decimal)input;
        public static byte ToByte(this byte? input) => (input is null) ? default(byte) : (byte)input;
        public static bool ToBool(this bool? input) => (input is null) ? false : (bool)input;

    }
}
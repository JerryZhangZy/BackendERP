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
        public static DateTime SystemMinDatatime = new DateTime(1800, 12, 28);

        public static DateTime MinDatatime = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        public static DateTime MaxDatatime = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

        public static TimeSpan MinTime = new TimeSpan(0, 0, 0);
        public static TimeSpan MaxTime = new TimeSpan(24, 0, 0).Subtract(new TimeSpan(1));

        public static int QtyDecimalDigits = 2;
        public static int PriceDecimalDigits = 2;
        public static int CostDecimalDigits = 3;
        public static int AmountDecimalDigits = 2;
        public static int RateDecimalDigits = 3;

        /// <summary>
        /// Covert input to string,then read it to stream;
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Stream ToStream(object input)
        {
            var inputString = input.ObjectToString();
            var buffer = Encoding.Default.GetBytes(inputString);
            return new MemoryStream(buffer);
        }

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

                //if (type is {})
                //    return value;

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
        public static Stream ToStream(this object value) => ObjectConvert.ToStream(value);

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
            return (obj is null) ? default(T) : (T)obj;
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
        /// Convert string to Camel case
        /// </summary>
        /// <returns></returns>
        public static string ToCamelCase(this string str, bool toCamelCase = true) =>
            string.IsNullOrWhiteSpace(str) || !toCamelCase
                ? str
                : $"{Char.ToLowerInvariant(str[0])}{str.Substring(1)}";


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

        public static T To<T>(this object input)
        {
            if (input is null)
                return default(T);
            var obj = ObjectConvert.ConvertObject(input, typeof(T));
            return (obj is null) ? default(T) : (T)obj;
        }

        /// <summary>
        /// Convert string to Enum by name
        /// </summary>
        public static T ToEnum<T>(this string value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            else
            {
                foreach (string nm in Enum.GetNames(typeof(T)))
                {
                    if (nm.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)Enum.Parse(typeof(T), nm);
                    }
                }
            }
            return default(T);
        }
        /// <summary>
        /// Convert string to Enum by name with default
        /// </summary>
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            else
            {
                foreach (string nm in Enum.GetNames(typeof(T)))
                {
                    if (nm.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)Enum.Parse(typeof(T), nm);
                    }
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// Convert int to Enum by enum value
        /// </summary>
        public static T ToEnum<T>(this int value, T defaultValue) where T : struct
        {
            T result;
            if (Enum.TryParse<T>(value.ToString(), out result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    return (T)result;
                }
            }
            return defaultValue;
        }


        public static string DbToString(this object obj) => (obj == null || obj == DBNull.Value) ? string.Empty : obj.ToString().TrimEnd();
        public static decimal ToDecimal(this object obj) => obj.DbToString().ToDecimal();

        public static int ToInt(this object obj) => obj.DbToString().ToInt();
        public static long ToLong(this object obj) => obj.DbToString().ToLong();

        public static short ToShort(this object obj) => obj.DbToString().ToShort();
        public static short ToShort(this short? s) => (s == null) ? (short)0 : (short)s;
        public static short ToShort(this string s) =>
            string.IsNullOrWhiteSpace(s)
                ? (short)0
                : short.TryParse(s, out short r)
                    ? r
                    : (short)0;

        public static byte ToByte(this object obj) => obj.DbToString().ToByte();

        public static bool ToBool(this object obj) => obj.DbToString().ToBool();
        public static bool ToBool(this string s) =>
            string.IsNullOrWhiteSpace(s)
                ? false
                : (s.ToInt() > 0)
                    ? true
                    : bool.TryParse(s, out bool r)
                        ? r
                        : false;

        public static bool ToBoolByString(this string s) =>
            string.IsNullOrWhiteSpace(s)
                ? false
                : s.EqualsIgnoreSpace("true") || s.EqualsIgnoreSpace("yes")
                    ? true
                    : false;

        /// <summary>
        /// Convert nullable value to its default, non-nullable value when its nullable value is null. 
        /// For example: default(int?) is null, this function will return default(int) is 0;
        /// </summary>
        /// <returns></returns>
        public static T NullableToValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        public static DateTime ToDateTime(this object obj) => obj.DbToString().ToDateTime();

        public static DateTime ToDateTime(this string s) =>
            string.IsNullOrWhiteSpace(s)
                ? ObjectConvert.MinDatatime
                : DateTime.TryParse(s, out DateTime r)
                    ? r
                    : ObjectConvert.MinDatatime;


        public static DateTime ToDateTime(this DateTime? input) => (input is null) ? ObjectConvert.MinDatatime : (DateTime)input;
        public static TimeSpan ToTimeSpan(this object input) => (input is null) ? ObjectConvert.MinTime : ((DateTime)input).TimeOfDay;
        public static TimeSpan ToTimeSpan(this DateTime? input) => (input is null) ? ObjectConvert.MinTime : ((DateTime)input).TimeOfDay;
        public static TimeSpan ToTimeSpan(this TimeSpan? input) => (input is null) ? ObjectConvert.MinTime : (TimeSpan)input;
        public static DateTime ToDateTime(this TimeSpan? input) => (input is null) ? DateTime.UtcNow.Date : DateTime.UtcNow.Date + (TimeSpan)input;
        public static DateTime ToDateTime(this TimeSpan input) => DateTime.UtcNow.Date + input;

        public static string ToDateString(this object input) => input.ToDateTime().ToShortDateString();
        public static string ToTimeString(this object input) => input.ToDateTime().ToShortTimeString();
        public static string ToISO8601_o(this object input) => 
            input.ToDateTime().ToString("o", System.Globalization.CultureInfo.InvariantCulture);
        public static string ToISO8601_s(this object input) => 
            input.ToDateTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        public static string ToISO8601(this object input) => 
            input.ToDateTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture);


        public static int ToInt(this int? input) => (input is null) ? default(int) : (int)input;
        public static int ToInt(this string input) =>
            string.IsNullOrWhiteSpace(input)
                ? (int)0
                : int.TryParse(input, out int r)
                    ? r
                    : (int)0;

        public static long ToLong(this long? input) => (input is null) ? default(long) : (long)input;
        public static long ToLong(this string input) =>
            string.IsNullOrWhiteSpace(input)
                ? (long)0
                : long.TryParse(input, out long r)
                    ? r
                    : (long)0;

        public static decimal ToDecimal(this decimal? input) => (input is null) ? default(decimal) : (decimal)input;
        public static decimal ToDecimal(this string input) =>
            string.IsNullOrWhiteSpace(input)
                ? (decimal)0
                : decimal.TryParse(input, out decimal r)
                    ? r
                    : (decimal)0;


        public static double ToDouble(this double? input) => (input is null) ? default(double) : (double)input;
        public static double ToDouble(this string input) =>
            string.IsNullOrWhiteSpace(input)
                ? (double)0
                : double.TryParse(input, out double r)
                    ? r
                    : (double)0;

        public static byte ToByte(this byte? input) => (input is null) ? default(byte) : (byte)input;
        public static byte ToByte(this string input) =>
            string.IsNullOrWhiteSpace(input)
                ? (byte)0
                : byte.TryParse(input, out byte r)
                    ? r
                    : (byte)0;

        public static bool ToBool(this bool? input) => (input is null) ? false : (bool)input;

        //public static bool IsZero(this decimal input) => Math.Abs(input) < (decimal)0.000001;//TODO input value is 0
        public static bool IsZero(this decimal input) => input <= 0;
        public static bool IsZero(this decimal? input) => (input is null) ? true : input.ToDecimal().IsZero();

        public static bool IsZero(this double input) => Math.Abs(input) < (double)0.000001;//TODO input value is 0
        public static bool IsZero(this double? input) => (input is null) ? true : input.ToDouble().IsZero();

        /// <summary>
        /// Positive int number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsZero(this int input) => input <= 0;
        public static bool IsZero(this int? input) => (input is null) ? true : input.ToInt().IsZero();

        //public static bool IsZero(this long input) => Math.Abs(input) < (long)0;
        /// <summary>
        /// Positive long number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsZero(this long input) => input <= 0;
        public static bool IsZero(this long? input) => (input is null) ? true : input.ToLong().IsZero();

        public static bool IsZero(this string input) => string.IsNullOrEmpty(input);

        public static bool IsZero(this DateTime input) => input <= ObjectConvert.SystemMinDatatime;
        public static bool IsZero(this DateTime? input) => (input is null) ? true : input.ToDateTime().IsZero();

        public static bool IsZero(this TimeSpan input) => input < ObjectConvert.MinTime || input > ObjectConvert.MaxTime;
        public static bool IsZero(this TimeSpan? input) => (input is null) ? true : input.ToTimeSpan().IsZero();


        public static decimal ToRateValue(this decimal? input) => (input is null) ? 0 : ToRateValue(input.Value);
        public static decimal ToRateValue(this decimal input) => input < 0 ? 0 : (input > 100 ? 1 : (input / 100));

        public static decimal ToRateDisplay(this decimal? input) => (input is null) ? 0 : input.ToDecimal() * 100;
        public static decimal ToRateDisplay(this decimal input) => input * 100;


        public static decimal ToPrice(this string input) =>
            string.IsNullOrWhiteSpace(input) ? 0 : input.ToDecimal().ToPrice();

        public static decimal ToPrice(this decimal? input) =>
            (input is null) ? (decimal)0 : input.ToDecimal().ToPrice();

        public static decimal ToPrice(this decimal input) =>
           input <= 0 ? 0 : Math.Round(input, ObjectConvert.PriceDecimalDigits, MidpointRounding.AwayFromZero);

        public static double ToPrice(this double? input) =>
            (input is null) ? (double)0 : input.ToDouble().ToPrice();

        public static double ToPrice(this double input) =>
            Math.Round(input, ObjectConvert.PriceDecimalDigits, MidpointRounding.AwayFromZero);


        public static decimal ToQty(this string input) =>
            string.IsNullOrWhiteSpace(input) ? 0 : input.ToDecimal().ToQty();

        public static decimal ToQty(this decimal? input) =>
            (input is null) ? (decimal)0 : input.ToDecimal().ToQty();

        public static decimal ToQty(this decimal input) =>
            Math.Round(input, ObjectConvert.QtyDecimalDigits, MidpointRounding.AwayFromZero);

        public static double ToQty(this double? input) =>
            (input is null) ? (double)0 : input.ToDouble().ToQty();

        public static double ToQty(this double input) =>
            Math.Round(input, ObjectConvert.QtyDecimalDigits, MidpointRounding.AwayFromZero);


        public static decimal ToAmount(this string input) =>
            string.IsNullOrWhiteSpace(input) ? 0 : input.ToDecimal().ToAmount();

        public static decimal ToAmount(this decimal? input) =>
            (input is null) ? (decimal)0 : input.ToDecimal().ToAmount();

        public static decimal ToAmount(this decimal input) =>
          Math.Round(input, ObjectConvert.AmountDecimalDigits, MidpointRounding.AwayFromZero);

        public static double ToAmount(this double? input) =>
            (input is null) ? (double)0 : input.ToDouble().ToAmount();

        public static double ToAmount(this double input) =>
            Math.Round(input, ObjectConvert.PriceDecimalDigits, MidpointRounding.AwayFromZero);


        public static decimal ToCost(this string input) =>
            string.IsNullOrWhiteSpace(input) ? 0 : input.ToDecimal().ToCost();

        public static decimal ToCost(this decimal? input) =>
            (input is null) ? (decimal)0 : input.ToDecimal().ToCost();

        public static decimal ToCost(this decimal input) =>
            Math.Round(input, ObjectConvert.CostDecimalDigits, MidpointRounding.AwayFromZero);

        public static double ToCost(this double? input) =>
            (input is null) ? (double)0 : input.ToDouble().ToCost();

        public static double ToCost(this double input) =>
            Math.Round(input, ObjectConvert.CostDecimalDigits, MidpointRounding.AwayFromZero);


        public static decimal ToRate(this string input) =>
            string.IsNullOrWhiteSpace(input) ? 0 : input.ToDecimal().ToRate();

        public static decimal ToRate(this decimal? input) =>
            (input is null) ? (decimal)0 : input.ToDecimal().ToRate();

        public static decimal ToRate(this decimal input) =>
          input <= 0 ? 0 : (input > 1 ? 1 : Math.Round(input, ObjectConvert.RateDecimalDigits + 2, MidpointRounding.AwayFromZero));

        public static double ToRate(this double? input) =>
            (input is null) ? (double)0 : input.ToDouble().ToRate();

        public static double ToRate(this double input) =>
            input < 0 ? 0 : (input > 100 ? 100 : Math.Round(input, ObjectConvert.RateDecimalDigits + 2, MidpointRounding.AwayFromZero));

        public static string ToFormatString(this object input, string format)
        {
            if (format.EqualsIgnoreSpace(FormatType.Qty))
                return input.ToDecimal().ToQty().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Amount))
                return input.ToDecimal().ToAmount().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Price))
                return input.ToDecimal().ToPrice().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Cost))
                return input.ToDecimal().ToCost().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Rate))
                return input.ToDecimal().ToRate().ToString();
            if (format.EqualsIgnoreSpace(FormatType.TaxRate))
                return input.ToDecimal().ToRate().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Weight))
                return input.ToDecimal().ToAmount().ToString();
            if (format.EqualsIgnoreSpace(FormatType.Date))
                return input.ToDateString();
            if (format.EqualsIgnoreSpace(FormatType.Time))
                return input.ToTimeString();
            if (format.EqualsIgnoreSpace(FormatType.IsoDate))
                return input.ToISO8601();

            return input.ToString();
        }

        public static decimal RoundTo(this decimal? input, int decimalDigit = 2) =>
            (input is null) ? (decimal)0 : input.ToDecimal().RoundTo(decimalDigit);
        public static decimal RoundTo(this decimal input, int decimalDigit = 2) =>
            Math.Round(input, decimalDigit, MidpointRounding.AwayFromZero);

        public static double RoundTo(this double? input, int decimalDigit = 2) =>
            (input is null) ? (double)0 : input.ToDouble().RoundTo(decimalDigit);
        public static double RoundTo(this double input, int decimalDigit = 2) =>
            Math.Round(input, decimalDigit, MidpointRounding.AwayFromZero);


        public static bool IsString(this Type type) => type == typeof(String);
        public static bool IsInt(this Type type) => type == typeof(int);
        public static bool IsLong(this Type type) => type == typeof(long);
        public static bool IsDecimal(this Type type) => type == typeof(Decimal);

        /// <summary>
        /// Split string to IEnumerable by separator
        /// </summary>
        public static IEnumerable<T> SplitTo<T>(this string input, params char[] separator)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<T>();
            if (separator is null || separator.Length == 0)
                separator = new char[] { ',' };
            return Array.ConvertAll<string, T>(input.Split(separator), item => item.ConvertObject<T>());
        }

        /// <summary>
        /// Convert IList<T> to IList<string>
        /// </summary>
        public static IList<string> ToStringList<T>(this IEnumerable<T> lst) =>
            (lst?.Any() != true)
                ? new List<string>()
            : lst.Select(x => x.ToString()).ToList();

        /// <summary>
        /// Convert IList<T> to IList<string>
        /// </summary>
        public static IList<string> ToStringList<T>(this IEnumerable<T> lst, Func<T, string> selector) =>
            (lst?.Any() != true)
                ? new List<string>()
                : lst.Select(selector).Distinct().ToList();

        /// <summary>
        /// Convert IList<T> to IList<string>
        /// </summary>
        public static IList<T> ToTypeList<T>(this IEnumerable<string> lst) =>
            (lst?.Any() != true)
                ? new List<T>()
                : lst.Select(x => x.ConvertObject<T>()).ToList();
    }
}
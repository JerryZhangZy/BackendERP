using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Dynamic;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class ObjectSchema
    {
        private static Cache<Type, ObjectSchema> _objectSchemas = new Cache<Type, ObjectSchema>();

        private static List<Func<object, object>> _converters = new List<Func<object, object>>();
        private static object _converterLock = new object();
        private static MethodInfo fnGetValue = typeof(IDataRecord).GetMethod("GetValue", new Type[] { typeof(int) });
        private static MethodInfo fnIsDBNull = typeof(IDataRecord).GetMethod("IsDBNull");
        private static FieldInfo fldConverters = typeof(PocoData).GetField("_converters", BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic);
        private static MethodInfo fnListGetItem = typeof(List<Func<object, object>>).GetProperty("Item").GetGetMethod();
        private static MethodInfo fnInvoke = typeof(Func<object, object>).GetMethod("Invoke");

        private Cache<string, Func<IDataReader, object>> ObjectSchemaLoadDataReaderFactories = new Cache<string, Func<IDataReader, object>>();

        public Type Type;
        public Dictionary<string, PocoColumn> Properties { get; private set; }

        public ObjectSchema()
        {
        }

        public ObjectSchema(Type type)
        {
            Type = type;

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var properties = type.GetProperties(bindingFlags);

            #region Add all properties reflection info
            Properties = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
            foreach (var pi in properties)
            {
                var pc = PocoColumn.CreateForProperty(type, pi);
                Properties.Add(pc.Name, pc);
            }
            #endregion Add properties reflection info

        }

        public static ObjectSchema ForType(Type type)
        {
            if (type == typeof(System.Dynamic.ExpandoObject))
                throw new InvalidOperationException("Can't use dynamic types with this method");

            return _objectSchemas.Get(type, () => new ObjectSchema(type));
        }

        // Create factory function that can convert a IDataReader record into a POCO
        public Func<IDataReader, object> GetFactory()
        {
            // Check cache
            var key = $"ObjectSchema_PocoFactories_{Type.Name}";

            return ObjectSchemaLoadDataReaderFactories.Get(key, () =>
            {
                return (IDataReader reader) =>
                {
                    if (reader is null) return null;
                    try
                    {
                        var obj = Activator.CreateInstance(Type);
                        var firstColumn = 0;
                        var countColumns = reader.FieldCount;

                        // Enumerate all fields generating a set assignment for the column
                        for (int i = firstColumn; i < firstColumn + countColumns; i++)
                        {
                            var nm = reader.GetName(i);
                            if (string.IsNullOrEmpty(nm))
                                continue;

                            var val = reader.GetValue(i);
                            if (val is null || val == DBNull.Value)
                                continue;

                            // Get the PocoColumn for this db column, ignore if not known
                            PocoColumn pc;
                            if (!Properties.TryGetValue(nm, out pc))
                                continue;

                            // Get the source type for this column
                            var srcType = reader.GetFieldType(i);
                            var dstType = pc.MemberInfo.GetPropertyType();

                            pc.SetValue(obj, ChangeType(val, srcType, dstType));
                        }
                        return obj;

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                };
            });
        }

        public static object ChangeType(object srcValue, Type srcType, Type dstType)
        {
            if (srcType == dstType) return srcValue;

            if (dstType == typeof(string) && srcType == typeof(string))
                return srcValue.DbToString();
            if (dstType == typeof(long) || dstType == typeof(long?))
                return srcValue.ToLong();
            if (dstType == typeof(int) || dstType == typeof(int?))
                return srcValue.ToInt();
            if (dstType == typeof(decimal) || dstType == typeof(decimal?))
                return srcValue.ToDecimal();
            if (dstType == typeof(byte) || dstType == typeof(byte?))
                return srcValue.ToByte();
            if (dstType == typeof(short) || dstType == typeof(short?))
                return srcValue.ToShort();
            if (dstType == typeof(bool) || dstType == typeof(bool?))
                return srcValue.ToBool();
            if (dstType == typeof(TimeSpan) || dstType == typeof(TimeSpan?))
                return srcValue.ToTimeSpan();
            if (srcType == typeof(DateTime) && (dstType == typeof(DateTime) || dstType == typeof(DateTime?)))
                return (DateTime)srcValue;

            // unwrap nullable types
            Type underlyingDstType = Nullable.GetUnderlyingType(dstType);
            if (underlyingDstType != null)
                dstType = underlyingDstType;
            return Convert.ChangeType(srcValue, dstType, null);
        }

        private static void AddConverterToStack(ILGenerator il, Func<object, object> converter)
        {
            if (converter != null)
            {
                // Add the converter
                int converterIndex;

                lock (_converterLock)
                {
                    converterIndex = _converters.Count;
                    _converters.Add(converter);
                }

                // Generate IL to push the converter onto the stack
                il.Emit(OpCodes.Ldsfld, fldConverters);
                il.Emit(OpCodes.Ldc_I4, converterIndex);
                il.Emit(OpCodes.Callvirt, fnListGetItem); // Converter
            }
        }

        private static Func<object, object> GetConverter(IMapper mapper, PocoColumn pc, Type srcType, Type dstType)
        {
            Func<object, object> converter = null;

            // Get converter from the mapper
            if (pc != null)
            {
                converter = mapper.GetFromDbConverter(pc.MemberInfo, srcType);
                if (converter != null)
                    return converter;
            }

            // Standard DateTime->Utc mapper
            if (pc != null && pc.ForceToUtc && srcType == typeof(DateTime) && (dstType == typeof(DateTime) || dstType == typeof(DateTime?)))
            {
                return delegate (object src) { return new DateTime(((DateTime)src).Ticks, DateTimeKind.Utc); };
            }

            // unwrap nullable types
            Type underlyingDstType = Nullable.GetUnderlyingType(dstType);
            if (underlyingDstType != null)
            {
                dstType = underlyingDstType;
            }

            // Forced type conversion including integral types -> enum
            if (dstType.IsEnum && IsIntegralType(srcType))
            {
                var backingDstType = Enum.GetUnderlyingType(dstType);
                if (underlyingDstType != null)
                {
                    // if dstType is Nullable<Enum>, convert to enum value
                    return delegate (object src) { return Enum.ToObject(dstType, src); };
                }
                else if (srcType != backingDstType)
                {
                    return delegate (object src) { return Convert.ChangeType(src, backingDstType, null); };
                }
            }
            else if (!dstType.IsAssignableFrom(srcType))
            {
                if (dstType.IsEnum && srcType == typeof(string))
                {
                    return delegate (object src) { return EnumMapper.EnumFromString(dstType, (string)src); };
                }

                if (dstType == typeof(Guid) && srcType == typeof(string))
                {
                    return delegate (object src) { return Guid.Parse((string)src); };
                }

                return delegate (object src) { return Convert.ChangeType(GetDefault(dstType, src), dstType, null); };
            }

            return null;
        }

        public static object GetDefault(Type type, object val)
        {
            if (val == null || val == DBNull.Value || string.IsNullOrWhiteSpace(val.ToString()))
            {
                if (type.GetTypeInfo().IsValueType)
                {
                    return Activator.CreateInstance(type);
                }
                return null;
            }
            return val;
        }

        private static bool IsIntegralType(Type type)
        {
            var tc = Type.GetTypeCode(type);
            return tc >= TypeCode.SByte && tc <= TypeCode.UInt64;
        }

        public string GetColumnName(string propertyName)
        {
            return Properties.Values.First(c => c.MemberInfo.Name.Equals(propertyName)).ColumnName;
        }

        public static IEnumerable<string> GetChangedProperties<T>(object A, object B)
        {
            if (A is null || B is null) return null;
            return ObjectSchema.ForType(typeof(T)).GetChangedProperties(A, B);
        }

        public IEnumerable<string> GetChangedProperties(object A, object B)
        {
            if (A is null || B is null) return null;
            if (!Properties.Any()) return null;

            var allProperties = Properties.Select(x => x.Value);
            var allValueProperties = allProperties.Where(col => col.CanCompare);
            var unequalProperties =
                from col in allValueProperties
                let AValue = col.GetValue(A)
                let BValue = col.GetValue(B)
                where AValue != BValue && (AValue == null || !AValue.Equals(BValue))
                select col.Name;
            return unequalProperties.ToList();
        }

        public static void CopyProperties<T>(object A, object B, bool ignoreNull, IEnumerable<string> ignoreNames)
        {
            if (A is null || B is null) return;
            ObjectSchema.ForType(typeof(T)).CopyProperties(A, B, ignoreNull, ignoreNames);
        }

        public void CopyProperties(object A, object B, bool ignoreNull, IEnumerable<string> ignoreNames)
        {
            if (A is null || B is null) return;
            if (!Properties.Any()) return;

            var allProperties = Properties.Select(x => x.Value);
            var allValueProperties = allProperties.Where(col => col.CanCopy);
            ignoreNames = ignoreNames?.ToList() ?? null;
            foreach (var col in allValueProperties)
            {
                if (ignoreNames != null && ignoreNames.Contains(col.Name)) continue;
                var AValue = col.GetValue(A);
                if (ignoreNull && AValue is null) continue;

                col.SetValue(B, AValue);
            }
            return;
        }

        public static void CopyProperties<TSource, TTarget>(object A, object B, bool ignoreNull = true, IEnumerable<string> ignoreNames = null)
        {
            if (A is null || B is null) return;
            try
            {
                var sourceSchema = ObjectSchema.ForType(typeof(TSource));
                var targetSchema = ObjectSchema.ForType(typeof(TTarget));
                if (!sourceSchema.Properties.Any() || !targetSchema.Properties.Any()) return;

                var allPropertiesSource = sourceSchema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCopy);
                var allPropertiesTarget = targetSchema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCopy);

                ignoreNames = ignoreNames?.ToList() ?? null;
                foreach (var colA in allPropertiesSource)
                {
                    if (ignoreNames != null && ignoreNames.Contains(colA.Name)) continue;
                    var AValue = colA.GetValue(A);
                    if (ignoreNull && AValue is null) continue;

                    var colB = allPropertiesTarget.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(colA.Name));
                    if (!colB.CanSet) continue;
                    if (!colB.MemberInfo.GetPropertyType().Equals(colA.MemberInfo.GetPropertyType())) continue;

                    colB.SetValue(B, AValue);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public static bool CompareProperties<TSource, TTarget>(object A, object B, IEnumerable<string> ignoreNames = null)
        {
            if (A == null && B == null || A.Equals(B)) return true;
            if (A is null || B is null) return false;
            try
            {
                var sourceSchema = ObjectSchema.ForType(typeof(TSource));
                var targetSchema = ObjectSchema.ForType(typeof(TTarget));
                if (!sourceSchema.Properties.Any() || !targetSchema.Properties.Any()) return false;

                var allPropertiesSource = sourceSchema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCopy);
                var allPropertiesTarget = targetSchema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCopy);

                ignoreNames = ignoreNames?.ToList() ?? null;
                foreach (var colA in allPropertiesSource)
                {
                    if (ignoreNames != null && ignoreNames.Contains(colA.Name)) continue;
                    var AValue = colA.GetValue(A);

                    var colB = allPropertiesTarget.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(colA.Name));
                    var BValue = colB.GetValue(B);

                    if (AValue != BValue && (AValue == null || !AValue.Equals(BValue)))
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool CompareProperties<T>(object A, IDictionary<string, string> keyValues)
        {
            if (A == null || keyValues == null || keyValues.Count == 0) return false;
            try
            {
                var schema = ObjectSchema.ForType(typeof(T));
                if (!schema.Properties.Any()) return true;

                var allProperties = schema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCompare);

                foreach (var item in keyValues)
                {
                    if (string.IsNullOrEmpty(item.Key)) continue;
                    var name = item.Key;
                    var BValue = item.Value;

                    var col = allProperties.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(name));
                    if (col == null || !col.CanCompare)
                        return false;
                    var AValue = col.GetValue(A)?.ToString();

                    if (!BValue.EqualsIgnoreSpace(AValue) && (AValue == null || !AValue.Equals(BValue)))
                        return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CopyProperties<T>(object A, IDictionary<string, string> keyValues)
        {
            if (A == null || keyValues == null || keyValues.Count == 0) return;
            try
            {
                var schema = ObjectSchema.ForType(typeof(T));
                if (!schema.Properties.Any()) return;

                var allProperties = schema.Properties
                    .Select(x => x.Value)
                    .Where(col => col.CanCopy && col.CanSet);

                foreach (var item in keyValues)
                {
                    if (string.IsNullOrEmpty(item.Key)) continue;
                    var name = item.Key;
                    var BValue = item.Value;

                    var col = allProperties.FirstOrDefault(x => x.Name.EqualsIgnoreSpace(name));
                    if (col == null) continue;

                    col.SetValue(A, BValue);
                }
                return;
            }
            catch (Exception)
            {
                return;
            }
        }


        public static Dictionary<string, PocoColumn> GetProperties<T>() =>
            ObjectSchema.ForType(typeof(T))?.Properties;

        public void GetCsvMapper(object A, object B, bool ignoreNull, IEnumerable<string> ignoreNames)
        {
            if (A is null || B is null) return;
            if (!Properties.Any()) return;

            var allProperties = Properties.Select(x => x.Value);
            var allValueProperties = allProperties.Where(col => col.CanCopy);
            ignoreNames = ignoreNames?.ToList() ?? null;
            foreach (var col in allValueProperties)
            {
                if (ignoreNames != null && ignoreNames.Contains(col.Name)) continue;
                var AValue = col.GetValue(A);
                if (ignoreNull && AValue is null) continue;

                col.SetValue(B, AValue);
            }
            return;
        }

    }

    public static class ObjectSchemaExtension
    {
        public static dynamic Merge(this object obj, params object[] others)
        {
            if (obj == null || others == null || others.Length == 0)
                return null;

            dynamic expando = new ExpandoObject();

            // get obj Schema
            var objSchema = ObjectSchema.ForType(obj.GetType());
            foreach (var item in objSchema.Properties)
            {
                AddExpandoObjectProperty(expando, item.Value, obj);
            }

            foreach (var otherObj in others)
            {
                if (otherObj == null)
                    continue;
                var otherSchema = ObjectSchema.ForType(otherObj.GetType());
                foreach (var item in otherSchema.Properties)
                {
                    AddExpandoObjectProperty(expando, item.Value, otherObj);
                }
            }
            return expando;
        }

        public static void AddExpandoObjectProperty(ExpandoObject expando, PocoColumn col, object obj)
        {
            if (expando == null || col == null || col.PropertyInfo is null) return;

            var result = expando as IDictionary<string, object>;
            if (result.ContainsKey(col.Name)) return;

            var pi = col.PropertyInfo;
            var jsonIgnore = pi.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any();
            var isIgnore = pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Any();

            // get name from attributes 
            var dataMember = pi.GetCustomAttribute<System.Runtime.Serialization.DataMemberAttribute>();
            var jsonProperty = pi.GetCustomAttribute<JsonPropertyAttribute>();
            var display = pi.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
            var nameAttr = dataMember != null
                ? dataMember.Name
                : jsonProperty != null
                    ? jsonProperty.PropertyName
                    : display != null
                        ? display.Name
                        : null;

            var isValueType = (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[]));
            if (jsonIgnore || isIgnore || !isValueType)
                return;

            result[col.Name] = col.GetValue(obj);
        }

        public static dynamic MergeName(this object obj, params object[] others)
        {
            if (obj == null || others == null || others.Length == 0)
                return null;

            dynamic expando = new ExpandoObject();

            // get obj Schema
            var objSchema = ObjectSchema.ForType(obj.GetType());
            foreach (var item in objSchema.Properties)
            {
                AddExpandoObjectPropertyName(expando, item.Value);
            }

            foreach (var otherObj in others)
            {
                if (otherObj == null)
                    continue;
                var otherSchema = ObjectSchema.ForType(otherObj.GetType());
                foreach (var item in otherSchema.Properties)
                {
                    AddExpandoObjectPropertyName(expando, item.Value);
                }
            }
            return expando;
        }

        public static void AddExpandoObjectPropertyName(ExpandoObject expando, PocoColumn col)
        {
            if (expando == null || col == null || col.PropertyInfo is null) return;

            var result = expando as IDictionary<string, object>;
            if (result.ContainsKey(col.Name)) return;

            var pi = col.PropertyInfo;
            var jsonIgnore = pi.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any();
            var isIgnore = pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Any();

            // get name from attributes 
            var dataMember = pi.GetCustomAttribute<System.Runtime.Serialization.DataMemberAttribute>();
            var jsonProperty = pi.GetCustomAttribute<JsonPropertyAttribute>();
            var display = pi.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
            var nameAttr = dataMember != null
                ? dataMember.Name
                : jsonProperty != null
                    ? jsonProperty.PropertyName
                    : display != null
                        ? display.Name
                        : null;


            var isValueType = (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[]));
            if (jsonIgnore || isIgnore || !isValueType)
                return;

            result[col.Name] = string.IsNullOrEmpty(nameAttr) ? col.Name : nameAttr.Split(",")[0];
        }

        public static IEnumerable<dynamic> FilterAndSortProperty(this IEnumerable<ExpandoObject> expando, IList<KeyValuePair<string, object>> props)
        {
            if (expando == null || props == null || props.Count == 0) return expando;
            var source = expando as IDictionary<string, object>;
            var result = new List<dynamic>();
            foreach (var obj in expando)
                result.Add(obj.FilterAndSortProperty(props));
            return result;
        }
        public static dynamic FilterAndSortProperty(this ExpandoObject expando, IList<KeyValuePair<string, object>> props)
        {
            if (expando == null || props == null || props.Count == 0) return expando;

            var source = expando as IDictionary<string, object>;
            var resultExpando = new ExpandoObject();
            var result = resultExpando as IDictionary<string, object>;
            foreach (var item in props)
            {
                if (source.TryGetValue(item.Key, out var value))
                {
                    result[item.Key] = value;
                }
                else if (!result.ContainsKey(item.Key))
                {
                    result[item.Key] = item.Value;
                }
            }
            return result;
        }
        public static IList<KeyValuePair<string, object>> GetPropertyNames(this ExpandoObject expando)
        {
            var source = expando as IDictionary<string, object>;
            var result = new List<KeyValuePair<string, object>>();
            foreach (var item in source)
                result.Add(new KeyValuePair<string, object>(item.Key, null));
            return result;
        }

    }
}
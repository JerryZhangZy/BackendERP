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
                            if (val is null)
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

    }
}
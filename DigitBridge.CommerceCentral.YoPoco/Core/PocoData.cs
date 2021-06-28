using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class PocoData
    {
        private static Cache<Type, PocoData> _pocoDatas = new Cache<Type, PocoData>();
        private static List<Func<object, object>> _converters = new List<Func<object, object>>();
        private static object _converterLock = new object();
        private static MethodInfo fnGetValue = typeof(IDataRecord).GetMethod("GetValue", new Type[] { typeof(int) });
        private static MethodInfo fnIsDBNull = typeof(IDataRecord).GetMethod("IsDBNull");
        private static FieldInfo fldConverters = typeof(PocoData).GetField("_converters", BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic);
        private static MethodInfo fnListGetItem = typeof(List<Func<object, object>>).GetProperty("Item").GetGetMethod();
        private static MethodInfo fnInvoke = typeof(Func<object, object>).GetMethod("Invoke");
        private Cache<Tuple<string, string, int, int>, Delegate> PocoFactories = new Cache<Tuple<string, string, int, int>, Delegate>();
        public bool IsMappingFields { get; private set; }

        public Type Type;
        public string[] QueryColumns { get; private set; }
        public string[] UpdateColumns
        {
            // No need to cache as it's not used by PetaPoco internally
            get { return (from c in Columns where !c.Value.ResultColumn && c.Value.ColumnName != TableInfo.PrimaryKey select c.Key).ToArray(); }
        }

        public TableInfo TableInfo { get; private set; }
        public Dictionary<string, PocoColumn> Columns { get; private set; }
        public Dictionary<string, PocoColumn> Properties { get; private set; }

        public PocoData()
        {
        }

        public PocoData(Type type, IMapper defaultMapper)
        {
            Type = type;

            // Get the mapper for this type
            var mapper = Mappers.GetMapper(type, defaultMapper);
            IsMappingFields = mapper.IsMappingFields;

            // Get the table info
            TableInfo = mapper.GetTableInfo(type);

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = type.GetFields(bindingFlags);
            var properties = type.GetProperties(bindingFlags);

            #region Add data table mapping coulumns to fields or properties
            Columns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
            if (IsMappingFields)
            {
                foreach (var pi in fields)
                {
                    ColumnInfo ci = mapper.GetColumnInfo(pi);
                    if (ci == null)
                        continue;
                    // Store it
                    Columns.Add(ci.ColumnName, PocoColumn.CreateForMapping(type, ci, pi));
                }
            }
            else
            {
                foreach (var pi in properties)
                {
                    ColumnInfo ci = mapper.GetColumnInfo(pi);
                    if (ci == null)
                        continue;
                    // Store it
                    Columns.Add(ci.ColumnName, PocoColumn.CreateForMapping(type, ci, pi));
                }
            }
            // Build column list for automatic select
            QueryColumns = (from c in Columns where !c.Value.ResultColumn || c.Value.AutoSelectedResultColumn select c.Key).ToArray();
            #endregion Add data table mapping coulumns to fields or properties

            #region Add all properties reflection info
            Properties = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
            foreach (var pi in properties)
            {
                var pc = PocoColumn.CreateForProperty(type, pi);
                Properties.Add(pc.Name, pc);
            }
            #endregion Add properties reflection info

        }

        public static PocoData ForObject(object obj, string primaryKeyName, IMapper defaultMapper)
        {
            var t = obj.GetType();
            if (t == typeof(System.Dynamic.ExpandoObject))
            {
                var pd = new PocoData();
                pd.TableInfo = new TableInfo();
                pd.Columns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
                pd.Columns.Add(primaryKeyName, new ExpandoColumn() { ColumnName = primaryKeyName });
                pd.TableInfo.PrimaryKey = primaryKeyName;
                pd.TableInfo.AutoIncrement = true;
                foreach (var col in (obj as IDictionary<string, object>).Keys)
                {
                    if (col != primaryKeyName)
                        pd.Columns.Add(col, new ExpandoColumn() { ColumnName = col });
                }

                return pd;
            }

            return ForType(t, defaultMapper);
        }

        public static PocoData ForType(Type type, IMapper defaultMapper)
        {
            if (type == typeof(System.Dynamic.ExpandoObject))
                throw new InvalidOperationException("Can't use dynamic types with this method");

            return _pocoDatas.Get(type, () => new PocoData(type, defaultMapper));
        }

        private static bool IsIntegralType(Type type)
        {
            var tc = Type.GetTypeCode(type);
            return tc >= TypeCode.SByte && tc <= TypeCode.UInt64;
        }

        // Create factory function that can convert a IDataReader record into a POCO
        public Delegate GetFactory(string sql, string connectionString, int firstColumn, int countColumns, IDataReader reader, IMapper defaultMapper)
        {
            // Check cache
            var key = Tuple.Create<string, string, int, int>(sql, connectionString, firstColumn, countColumns);

            return PocoFactories.Get(key, () =>
            {
                // Create the method
                var m = new DynamicMethod("poco_factory_" + PocoFactories.Count.ToString(), Type, new Type[] { typeof(IDataReader) }, true);
                var il = m.GetILGenerator();
                var mapper = Mappers.GetMapper(Type, defaultMapper);

                if (Type == typeof(object))
                {
                    // var poco=new T()
                    il.Emit(OpCodes.Newobj, typeof(System.Dynamic.ExpandoObject).GetConstructor(Type.EmptyTypes)); // obj

                    MethodInfo fnAdd = typeof(IDictionary<string, object>).GetMethod("Add");

                    // Enumerate all fields generating a set assignment for the column
                    for (int i = firstColumn; i < firstColumn + countColumns; i++)
                    {
                        var srcType = reader.GetFieldType(i);

                        il.Emit(OpCodes.Dup); // obj, obj
                        il.Emit(OpCodes.Ldstr, reader.GetName(i)); // obj, obj, fieldname

                        // Get the converter
                        Func<object, object> converter = mapper.GetFromDbConverter((MemberInfo) null, srcType);

                        /*
						if (ForceDateTimesToUtc && converter == null && srcType == typeof(DateTime))
							converter = delegate(object src) { return new DateTime(((DateTime)src).Ticks, DateTimeKind.Utc); };
						 */

                        // Setup stack for call to converter
                        AddConverterToStack(il, converter);

                        // r[i]
                        il.Emit(OpCodes.Ldarg_0); // obj, obj, fieldname, converter?,    rdr
                        il.Emit(OpCodes.Ldc_I4, i); // obj, obj, fieldname, converter?,  rdr,i
                        il.Emit(OpCodes.Callvirt, fnGetValue); // obj, obj, fieldname, converter?,  value

                        // Convert DBNull to null
                        il.Emit(OpCodes.Dup); // obj, obj, fieldname, converter?,  value, value
                        il.Emit(OpCodes.Isinst, typeof(DBNull)); // obj, obj, fieldname, converter?,  value, (value or null)
                        var lblNotNull = il.DefineLabel();
                        il.Emit(OpCodes.Brfalse_S, lblNotNull); // obj, obj, fieldname, converter?,  value
                        il.Emit(OpCodes.Pop); // obj, obj, fieldname, converter?
                        if (converter != null)
                            il.Emit(OpCodes.Pop); // obj, obj, fieldname, 
                        il.Emit(OpCodes.Ldnull); // obj, obj, fieldname, null
                        if (converter != null)
                        {
                            var lblReady = il.DefineLabel();
                            il.Emit(OpCodes.Br_S, lblReady);
                            il.MarkLabel(lblNotNull);
                            il.Emit(OpCodes.Callvirt, fnInvoke);
                            il.MarkLabel(lblReady);
                        }
                        else
                        {
                            il.MarkLabel(lblNotNull);
                        }

                        il.Emit(OpCodes.Callvirt, fnAdd);
                    }
                }
                else if (Type.IsValueType || Type == typeof(string) || Type == typeof(byte[]))
                {
                    // Do we need to install a converter?
                    var srcType = reader.GetFieldType(0);
                    var converter = GetConverter(mapper, null, srcType, Type);

                    // "if (!rdr.IsDBNull(i))"
                    il.Emit(OpCodes.Ldarg_0); // rdr
                    il.Emit(OpCodes.Ldc_I4_0); // rdr,0
                    il.Emit(OpCodes.Callvirt, fnIsDBNull); // bool
                    var lblCont = il.DefineLabel();
                    il.Emit(OpCodes.Brfalse_S, lblCont);
                    il.Emit(OpCodes.Ldnull); // null
                    var lblFin = il.DefineLabel();
                    il.Emit(OpCodes.Br_S, lblFin);

                    il.MarkLabel(lblCont);

                    // Setup stack for call to converter
                    AddConverterToStack(il, converter);

                    il.Emit(OpCodes.Ldarg_0); // rdr
                    il.Emit(OpCodes.Ldc_I4_0); // rdr,0
                    il.Emit(OpCodes.Callvirt, fnGetValue); // value

                    // Call the converter
                    if (converter != null)
                        il.Emit(OpCodes.Callvirt, fnInvoke);

                    il.MarkLabel(lblFin);
                    il.Emit(OpCodes.Unbox_Any, Type); // value converted
                }
                else
                {
                    // var poco=new T()
                    var ctor = Type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
                    if (ctor == null)
                        throw new InvalidOperationException("Type [" + Type.FullName + "] should have default public or non-public constructor");

                    il.Emit(OpCodes.Newobj, ctor);

                    // Enumerate all fields generating a set assignment for the column
                    for (int i = firstColumn; i < firstColumn + countColumns; i++)
                    {
                        // Get the PocoColumn for this db column, ignore if not known
                        PocoColumn pc;
                        if (!Columns.TryGetValue(reader.GetName(i), out pc))
                            continue;

                        // Get the source type for this column
                        var srcType = reader.GetFieldType(i);
                        var dstType = pc.MemberInfo.GetPropertyType();

                        // "if (!rdr.IsDBNull(i))"
                        il.Emit(OpCodes.Ldarg_0); // poco,rdr
                        il.Emit(OpCodes.Ldc_I4, i); // poco,rdr,i
                        il.Emit(OpCodes.Callvirt, fnIsDBNull); // poco,bool
                        var lblNext = il.DefineLabel();
                        il.Emit(OpCodes.Brtrue_S, lblNext); // poco

                        il.Emit(OpCodes.Dup); // poco,poco

                        // Do we need to install a converter?
                        var converter = GetConverter(mapper, pc, srcType, dstType);

                        // Fast
                        bool Handled = false;
                        if (converter == null)
                        {
                            var valuegetter = typeof(IDataRecord).GetMethod("Get" + srcType.Name, new Type[] { typeof(int) });
                            if (valuegetter != null && valuegetter.ReturnType == srcType &&
                                (valuegetter.ReturnType == dstType || valuegetter.ReturnType == Nullable.GetUnderlyingType(dstType)))
                            {
                                il.Emit(OpCodes.Ldarg_0); // *,rdr
                                il.Emit(OpCodes.Ldc_I4, i); // *,rdr,i
                                il.Emit(OpCodes.Callvirt, valuegetter); // *,value

                                // Convert to Nullable
                                if (Nullable.GetUnderlyingType(dstType) != null)
                                {
                                    il.Emit(OpCodes.Newobj, dstType.GetConstructor(new Type[] { Nullable.GetUnderlyingType(dstType) }));
                                }

                                if (pc.IsField)
                                    il.Emit(OpCodes.Stfld, pc.FieldInfo); // poco
                                else
                                    il.Emit(OpCodes.Callvirt, pc.PropertyInfo.GetSetMethod(true)); // poco
                                Handled = true;
                            }
                        }

                        // Not so fast
                        if (!Handled)
                        {
                            // Setup stack for call to converter
                            AddConverterToStack(il, converter);

                            // "value = rdr.GetValue(i)"
                            il.Emit(OpCodes.Ldarg_0); // *,rdr
                            il.Emit(OpCodes.Ldc_I4, i); // *,rdr,i
                            il.Emit(OpCodes.Callvirt, fnGetValue); // *,value

                            // Call the converter
                            if (converter != null)
                                il.Emit(OpCodes.Callvirt, fnInvoke);

                            // Assign it
                            il.Emit(OpCodes.Unbox_Any, pc.MemberInfo.GetPropertyType()); // poco,poco,value
                            if (pc.IsField)
                                il.Emit(OpCodes.Stfld, pc.FieldInfo); // poco
                            else
                                il.Emit(OpCodes.Callvirt, pc.PropertyInfo.GetSetMethod(true)); // poco
                        }

                        il.MarkLabel(lblNext);
                    }

                    var fnOnLoaded = RecurseInheritedTypes<MethodInfo>(Type,
                        (x) => x.GetMethod("OnLoaded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null));
                    if (fnOnLoaded != null)
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, fnOnLoaded);
                    }
                }

                il.Emit(OpCodes.Ret);

                // Cache it, return it
                return m.CreateDelegate(Expression.GetFuncType(typeof(IDataReader), Type));
            });
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
                return delegate(object src) { return new DateTime(((DateTime) src).Ticks, DateTimeKind.Utc); };
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
                    return delegate(object src) { return Enum.ToObject(dstType, src); };
                }
                else if (srcType != backingDstType)
                {
                    return delegate(object src) { return Convert.ChangeType(src, backingDstType, null); };
                }
            }
            else if (!dstType.IsAssignableFrom(srcType))
            {
                if (dstType.IsEnum && srcType == typeof(string))
                {
                    return delegate(object src) { return EnumMapper.EnumFromString(dstType, (string) src); };
                }

                if (dstType == typeof(Guid) && srcType == typeof(string))
                {
                    return delegate(object src) { return Guid.Parse((string) src); };
                }

                return delegate(object src) { return Convert.ChangeType(GetDefault(dstType, src), dstType, null); };
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

        private static T RecurseInheritedTypes<T>(Type t, Func<Type, T> cb)
        {
            while (t != null)
            {
                T info = cb(t);
                if (info != null)
                    return info;
                t = t.BaseType;
            }

            return default(T);
        }

        internal static void FlushCaches()
        {
            _pocoDatas.Flush();
        }

        public string GetColumnName(string propertyName)
        {
            return Columns.Values.First(c => c.MemberInfo.Name.Equals(propertyName)).ColumnName;
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

        public static IEnumerable<string> GetChangedProperties<T>(object A, object B)
        {
            if (A is null || B is null) return null;
            return PocoData.ForType(typeof(T), new ConventionMapper()).GetChangedProperties(A, B);
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

    }
}
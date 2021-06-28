using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class PocoColumn
    {
        public bool AutoSelectedResultColumn;
        public string ColumnName;
        public bool ForceToUtc;
        public MemberInfo MemberInfo;
        public FieldInfo FieldInfo;
        public PropertyInfo PropertyInfo;
        private PropertyHandler handler;

        public bool IsMappingColumn;
        public string Name;
        public string DtoName;

        public bool ResultColumn;
        public string InsertTemplate { get; set; }
        public string UpdateTemplate { get; set; }
        public bool CanCopy { get; set; }
        public bool CanCompare { get; set; }
        public bool CanSet { get; set; }
        public bool CanGet { get; set; }
        public bool IsField => MemberInfo.MemberType == MemberTypes.Field;

        public static PocoColumn CreateForMapping(Type type, ColumnInfo ci, MemberInfo pi)
        {
            var pc = new PocoColumn()
            {
                MemberInfo = pi,
                ColumnName = ci.ColumnName,
                ResultColumn = ci.ResultColumn,
                AutoSelectedResultColumn = ci.AutoSelectedResultColumn,
                ForceToUtc = ci.ForceToUtc,
                InsertTemplate = ci.InsertTemplate,
                UpdateTemplate = ci.UpdateTemplate
            };
            if (pc.IsField)
                pc.FieldInfo = (FieldInfo)pc.MemberInfo;
            else
                pc.PropertyInfo = (PropertyInfo)pc.MemberInfo;
            return pc;
        }

        public static PocoColumn CreateForProperty(Type type, PropertyInfo pi)
        {
            // Check for [Column]/[Ignore] Attributes
            var columnAttr = pi.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
            var isIgnore = pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Any();
            var jsonIgnore = pi.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any();
            var ignoreCompare = pi.GetCustomAttributes(typeof(IgnoreCompareAttribute), true).Any();
            var dtoAttr = pi.GetCustomAttributes(typeof(DtoNameAttribute), true).FirstOrDefault() as ColumnAttribute;
            var canCopy = !isIgnore && pi.Name != "RowNum";
            var canCompare = !isIgnore && !ignoreCompare;

            var columnName = columnAttr?.Name ?? pi.Name;
            var forceToUtc = columnAttr?.ForceToUtc == true;
            var insertTemplate = columnAttr?.InsertTemplate;
            var updateTemplate = columnAttr?.UpdateTemplate;

            var resultColumn = false;
            var autoSelectedResultColumn = false;
            if (columnAttr is ResultColumnAttribute resAttr)
            {
                resultColumn = true;
                autoSelectedResultColumn = resAttr.IncludeInAutoSelect == IncludeInAutoSelect.Yes;
            }

            var pc = new PocoColumn()
            {
                Name = pi.Name,
                MemberInfo = pi,
                PropertyInfo = pi,
                IsMappingColumn = (!isIgnore && (columnAttr != null)),
                ColumnName = columnName,
                DtoName = dtoAttr?.Name ?? pi.Name,
                ResultColumn = resultColumn,
                AutoSelectedResultColumn = autoSelectedResultColumn,
                ForceToUtc = forceToUtc,
                InsertTemplate = insertTemplate,
                UpdateTemplate = updateTemplate,
                CanCopy = canCopy,
                CanCompare = canCompare,
                CanSet = pi.CanWrite,
                CanGet = pi.CanRead,
                handler = new PropertyHandler(pi)
            };
            return pc;
        }



        public virtual void SetValue(object target, object val)
        {
            if (!IsField && !CanSet) return;
            if (handler?.Set != null)
                this.handler.Set(target, ChangeType(val));
            else
                MemberInfo.SetValue(target, val);
        }

        public virtual object GetValue(object target)
        {
            return (handler?.Get != null)
                ? this.handler.Get(target)
                : MemberInfo.GetValue(target);
        }

        public virtual object ChangeType(object val)
        {
            var t = MemberInfo.GetPropertyType();
            if (val.GetType().IsValueType && t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                t = t.GetGenericArguments()[0];

            return Convert.ChangeType(val, t);
        }
    }

    public static class MemberInfoExtensions
    {
        public static Type GetPropertyType(this MemberInfo member)
        {
            switch (member)
            {
                case FieldInfo mfi:
                    return mfi.FieldType;
                case PropertyInfo mpi:
                    return mpi.PropertyType;
                case EventInfo mei:
                    return mei.EventHandlerType;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo",
                        nameof(member));
            }
        }

        public static bool GetCanWrite(this MemberInfo member)
        {
            switch (member)
            {
                case FieldInfo mfi:
                    return true;
                case PropertyInfo mpi:
                    return mpi.CanWrite;
                default:
                    throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo", nameof(member));
            }
        }

        public static object GetValue(this MemberInfo member, object srcObject)
        {
            switch (member)
            {
                case FieldInfo mfi:
                    return mfi.GetValue(srcObject);
                case PropertyInfo mpi:
                    return mpi.GetValue(srcObject);
                case MethodInfo mi:
                    return mi.Invoke(srcObject, null);
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or MethodInfo",
                        nameof(member));
            }
        }

        public static T GetValue<T>(this MemberInfo member, object srcObject) => (T) member.GetValue(srcObject);

        public static void SetValue(this MemberInfo member, object destObject, object value)
        {
            switch (member)
            {
                case FieldInfo mfi:
                    mfi.SetValue(destObject, value);
                    break;
                case PropertyInfo mpi:
                    mpi.SetValue(destObject, value);
                    break;
                case MethodInfo mi:
                    mi.Invoke(destObject, new object[] {value});
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or MethodInfo",
                        nameof(member));
            }
        }

        public static void SetValue<T>(this MemberInfo member, object destObject, T value) =>
            member.SetValue(destObject, (object) value);
    }
}
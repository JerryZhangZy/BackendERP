using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using CsvHelper.Configuration;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using CsvHelper;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class CsvFormatColumn
    {
        /// <summary>
        /// name of property in data object
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// name of title in csv file, multiple name use , delimited
        /// </summary>
        public virtual string HeaderName { get; set; }

        /// <summary>
        /// column index in csv file
        /// </summary>
        public virtual int Index { get; set; }

        /// <summary>
        /// ignore this column
        /// </summary>
        public virtual bool Ignore { get; set; } = false;

        /// <summary>
        /// default value if column is empty
        /// </summary>
        public virtual string DefaultValue { get; set; }

        /// <summary>
        /// constent value for this column
        /// </summary>
        public virtual string ConstantValue { get; set; }

        /// <summary>
        /// disable this column mapper
        /// </summary>
        public virtual bool Disable { get; set; }

        /// <summary>
        /// Enable this column mapper
        /// </summary>
        [JsonIgnore]
        public virtual bool IsEnable 
        {
            get => !string.IsNullOrEmpty(Name) && !Ignore && !Disable;
        }

        public CsvFormatColumn() { }

        public CsvFormatColumn(string name, string headerName, int index, bool ignore = false, string defaultValue = null, string constantValue = null)
        {
            Name = name;
            HeaderName = headerName;
            Index = index;
            Ignore = ignore;
            DefaultValue = defaultValue;
            ConstantValue = constantValue;
        }

        public virtual CsvFormatColumn Clone(CsvFormatColumn source)
        {
            if (!string.IsNullOrEmpty(source.Name)) Name = source.Name;
            if (!string.IsNullOrEmpty(source.HeaderName)) HeaderName = source.HeaderName;
            if (source.Index >= 0) Index = source.Index;
            if (source.Ignore == true) Ignore = source.Ignore;
            if (!string.IsNullOrEmpty(source.DefaultValue)) DefaultValue = source.DefaultValue;
            if (!string.IsNullOrEmpty(source.ConstantValue)) ConstantValue = source.ConstantValue;
            Disable = source.Disable;
            return this;
        }
    }

    public class CsvFormatParentObject
    {
        [JsonIgnore] public virtual Type Type { get; set; }
        public virtual string Name { get; set; }

        /// <summary>
        /// disable this table mapper
        /// </summary>
        public virtual bool Disable { get; set; }

        /// <summary>
        /// Enable this table mapper
        /// </summary>
        [JsonIgnore]
        public virtual bool IsEnable
        {
            get => !string.IsNullOrEmpty(Name) && !Disable && Columns != null && Columns.Count > 0;
        }

        public virtual IList<CsvFormatColumn> Columns { get; set; } = new List<CsvFormatColumn>();
        public virtual bool HasColumns() => Columns != null && Columns.Count > 0;
            

        public CsvFormatParentObject(Type type, string name = null)
        {
            Type = type;
            Name = !string.IsNullOrEmpty(name) ? name : Type.Name;
        }

        public virtual CsvFormatParentObject Clone(CsvFormatParentObject source)
        {
            if (source.Type != null) Type = source.Type;
            if (!string.IsNullOrEmpty(source.Name)) Name = source.Name;
            Disable = source.Disable;
            foreach (var col in source.Columns)
            {
                if (string.IsNullOrEmpty(col.Name) || string.IsNullOrEmpty(col.HeaderName)) continue;
                var obj = this.Columns.FindByName(col.Name);
                if (obj == null)
                {
                    obj = new CsvFormatColumn();
                    this.Columns.Add(obj);
                }
                obj.Clone(col);
            }
            return this;
        }
    }

    public class CsvFormat
    {
        public virtual int FormatNum { get; set; }
        public virtual string FormatName { get; set; }

        public virtual int SkipLines { get; set; } = 0;
        public virtual string KeyName { get; set; } = null;

        public virtual bool HasHeaderRecord { get; set; } = true;
        public virtual string Delimiter { get; set; } = ",";
        [JsonIgnore] public virtual Encoding Encoding { get; set; } = Encoding.UTF8;
        public virtual IList<CsvFormatParentObject> ParentObject { get; set; } = new List<CsvFormatParentObject>();
        public virtual bool HasParentObject() => ParentObject != null && ParentObject.Count > 0;

        protected virtual CsvFormatParentObject InitParentObject<T>() where T : class, new()
        {
            var t = typeof(T);
            var obj = ParentObject.FindByType(t);
            if (obj == null)
            {
                obj = new CsvFormatParentObject(t);
                ParentObject.Add(obj);
            }
            return obj;
        }

        public virtual void LoadFormat(CsvFormat fmt)
        {
            if (fmt == null) return;
            LoadFormatHeader(fmt);
            LoadParentObject(fmt.ParentObject);
            LoadFormatHeader(fmt);
            LoadFormatHeader(fmt);
        }

        public virtual void LoadFormatHeader(CsvFormat fmt)
        {
            if (fmt.HasHeaderRecord == false) HasHeaderRecord = fmt.HasHeaderRecord;
            if (!string.IsNullOrEmpty(fmt.Delimiter)) Delimiter = fmt.Delimiter;
            FormatNum = fmt.FormatNum;
            FormatName = fmt.FormatName;
            SkipLines = fmt.SkipLines;
            KeyName = fmt.KeyName;
        }

        public virtual void LoadParentObject(IList<CsvFormatParentObject> parents)
        {
            if (parents == null || parents.Count == 0) return;
            foreach (var parent in parents)
            {
                if (string.IsNullOrEmpty(parent.Name)) continue;
                var obj = this.ParentObject.FindByName(parent.Name);
                if (obj == null) continue;
                obj.Clone(parent);
            }
        }

        public virtual bool IsheaderLine(CsvReader csv)
        {
            var headers = GetHeaderNames();
            var found = 0;
            found += headers.Contains(csv.GetField(0)) ? 1 : 0;
            found += headers.Contains(csv.GetField(1)) ? 1 : 0;
            found += headers.Contains(csv.GetField(2)) ? 1 : 0;
            return found > 1;
        }

        public virtual IList<string> GetHeaderNames() 
            => !HasParentObject()
                ? null
                : ParentObject.GetHeaderNames();

        public virtual IList<string> GetNames()
            => !HasParentObject()
                ? null
                : ParentObject.GetNames();

    }

    public static class CsvFormatParentObjectExtension
    {
        public static CsvFormatParentObject FindByType(this IList<CsvFormatParentObject> list, Type ty)
            => (list == null || list.Count == 0) ? null : list.FirstOrDefault(x => x.Type != null && x.Type.Equals(ty));
        public static CsvFormatParentObject FindByName(this IList<CsvFormatParentObject> list, string name)
            => (list == null || list.Count == 0) ? null : list.FirstOrDefault(x => !string.IsNullOrEmpty(x.Name) && x.Name.EqualsIgnoreSpace(name));

        public static IList<string> GetHeaderNames(this IList<CsvFormatParentObject> list)
        {
            if (list == null || list.Count == 0)
                return new List<string>();
            var result = new List<string>();
            foreach (var item in list)
            {
                if (item == null || !item.HasColumns())
                    continue;
                result.AddRange(item.Columns.GetHeaderNames());
            }
            return result;
        }
        public static IList<string> GetNames(this IList<CsvFormatParentObject> list)
        {
            if (list == null || list.Count == 0)
                return new List<string>();
            var result = new List<string>();
            foreach (var item in list)
            {
                if (item == null || !item.HasColumns())
                    continue;
                result.AddRange(item.Columns.GetNames());
            }
            return result;
        }

    }

    public static class CsvFormatColumnExtension
    {
        public static CsvFormatColumn FindByName(this IList<CsvFormatColumn> list, string name)
            => (list == null || list.Count == 0) ? null : list.FirstOrDefault(x => !string.IsNullOrEmpty(x.Name) && x.Name.EqualsIgnoreSpace(name));

        public static CsvFormatColumn FindByHeaderName(this IList<CsvFormatColumn> list, string headerName)
            => (list == null || list.Count == 0) ? null : list.FirstOrDefault(x => !string.IsNullOrEmpty(x.HeaderName) && x.HeaderName.EqualsIgnoreSpace(headerName));

        public static CsvFormatColumn FindByIndex(this IList<CsvFormatColumn> list, int index)
            => (list == null || list.Count == 0) ? null : list.FirstOrDefault(x => x.Index == index);

        public static IList<string> GetHeaderNames(this IList<CsvFormatColumn> list)
        {
            if (list == null || list.Count == 0)
                return new List<string>();
            var result = new List<string>();
            foreach (var item in list)
            {
                if (item == null || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.HeaderName))
                    continue;
                result.AddRange(item.HeaderName.Split(","));
            }
            return result;
        }
        public static IList<string> GetNames(this IList<CsvFormatColumn> list)
        {
            if (list == null || list.Count == 0)
                return new List<string>();
            var result = new List<string>();
            foreach (var item in list)
            {
                if (item == null || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.HeaderName))
                    continue;
                result.AddRange(item.Name.Split(","));
            }
            return result;
        }
    }

}

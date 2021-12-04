using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class CsvAutoMapper<T> : ClassMap<T>
        where T: new()
    {
        protected int NameIndex = 0;
        public CsvAutoMapper(int nameIndex=0)
        {
            NameIndex = nameIndex;
            AutoMap(CultureInfo.InvariantCulture);
            var props = ObjectSchema.GetProperties<T>();
            if (props is null)
                return;
            MapProperties(props);
            //Map(m => m.Name).Name("The Name");
        }

        public CsvAutoMapper<T> MapProperties(IDictionary<string, PocoColumn> properties)
        {
            foreach (var map in this.MemberMaps.ToList())
            {
                if (map is null)
                    continue;
                if (!properties.TryGetValue(map.Data.Member.Name, out var col))
                    map.Ignore(true);
                MapProperty(map, col);
            }
            return this;
        }

        public MemberMap MapProperty(PocoColumn col) =>
            MapProperty(Map(typeof(T), col?.PropertyInfo), col);

        public MemberMap MapProperty(MemberMap map, PocoColumn col)
        {
            if (map is null || col is null || col.PropertyInfo is null) return null;

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

            // ignore column
            if (jsonIgnore || isIgnore || !isValueType)
            {
                map.Ignore(true);
                return map;
            }

            // replace name
            if (!string.IsNullOrEmpty(nameAttr))
            {
                var names = nameAttr.Split(",");
                if (!names.Contains(pi.Name))
                {
                    var lst = names.ToList();
                    lst.Add(pi.Name);
                    names = lst.ToArray();
                }
                map.Name(names);
            }

            // set all column is Optional
            map.Optional();

            map.NameIndex(NameIndex);

            return map;
        }
    }
}

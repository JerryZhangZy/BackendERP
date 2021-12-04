using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class CsvFormatMapper<T> : ClassMap<T>
        where T : new()
    {
        public CsvFormat Format { get; set; }

        protected int NameIndex = 0;
        public CsvFormatMapper(CsvFormat fmt)
        {
            Format = fmt;
            //AutoMap(CultureInfo.InvariantCulture);
            var props = ObjectSchema.GetProperties<T>();
            if (props is null)
                return;
            MapProperties(props);
            //Map(m => m.Name).Name("The Name");
        }

        public CsvFormatMapper<T> MapProperties(IDictionary<string, PocoColumn> properties)
        {
            // get object format for current class type
            var parentObject = Format.ParentObject.FindByType(typeof(T));
            if (parentObject == null || !parentObject.IsEnable) 
                return this;

            foreach (var item in properties)
            {
                if (string.IsNullOrEmpty(item.Key) || item.Value == null)
                    continue;
                var name = item.Key;
                var col = item.Value;

                // get column map
                var formatColoumn = parentObject.Columns.FindByName(name);
                if (formatColoumn == null || !formatColoumn.IsEnable)
                    continue;

                var map = this.MemberMaps.FirstOrDefault(x =>
                        !string.IsNullOrEmpty(x.Data.Names.Names.Find(x => 
                                !string.IsNullOrEmpty(x) && x.EqualsIgnoreSpace(name))));
                if (map == null)
                    MapProperty(col, formatColoumn);
                MapProperty(map, col, formatColoumn);
            }
            return this;
        }

        public MemberMap MapProperty(PocoColumn col, CsvFormatColumn fmt) =>
            MapProperty(Map(typeof(T), col?.PropertyInfo), col, fmt);

        public MemberMap MapProperty(MemberMap map, PocoColumn col, CsvFormatColumn fmt)
        {
            if (map is null || col is null || col.PropertyInfo is null || fmt is null) return null;

            var pi = col.PropertyInfo;

            var jsonIgnore = pi.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any();
            var isIgnore = pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Any();

            // get name from format 
            var names = fmt.HeaderName?.Split(",");

            // add property name to end of name array
            if (!names.Contains(pi.Name))
            {
                var lst = names.ToList();
                lst.Add(pi.Name);
                names = lst.ToArray();
            }

            // add mapper csv name
            map.Name(names);

            // set all column is Optional
            map.Optional();

            map.NameIndex(fmt.Index);

            return map;
        }
    }
}

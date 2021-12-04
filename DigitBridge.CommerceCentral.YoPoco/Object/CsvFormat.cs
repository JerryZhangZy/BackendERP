using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class CsvFormatColumn
    {
        public virtual string Name { get; set; }
        public virtual string HeaderName { get; set; }
        public virtual int Index { get; set; }
        public virtual bool Ignore { get; set; }
        public virtual string DefaultValue { get; set; }
        public virtual string ConstantValue { get; set; }
    }

    public class CsvFormatParentObject
    {
        public virtual string Name { get; set; }
        public virtual IList<CsvFormatColumn> Columns { get; set; } = new List<CsvFormatColumn>();
    }

    public class CsvFormat
    {
        public virtual bool HasHeaderRecord { get; set; } = true;
        public virtual string Delimiter { get; set; } = ",";
        public virtual Encoding Encoding { get; set; } = Encoding.UTF8;
        public virtual IList<CsvFormatParentObject> ParentObject { get; set; } = new List<CsvFormatParentObject>();
    }
}

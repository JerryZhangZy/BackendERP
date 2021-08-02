using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface ICsvHelper<T> where T: new()
    {
        public CsvAutoMapper<T> Mapper { get; }
        public CsvFormat Format { get; set; }

        CsvConfiguration GetConfiguration();
        void GetFormat();
        void RegisterMapper(CsvContext context);

        string Export(IEnumerable<T> data, string fileName);
        IEnumerable<T> Import(string fileName);
    }
}

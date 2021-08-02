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
    public class CsvHelper<T> : ICsvHelper<T> where T: class, new()
    {
        protected CsvAutoMapper<T> _mapper;
        public CsvAutoMapper<T> Mapper => _mapper;
        public CsvFormat Format { get; set; }

        public CsvHelper()
        {
            _mapper = new CsvAutoMapper<T>();
        }

        public virtual void GetFormat() { }

        public virtual CsvConfiguration GetConfiguration()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            if (Format != null)
            {
                config.Delimiter = Format.Delimiter;
                config.Encoding = Format.Encoding;
                config.HasHeaderRecord = Format.HasHeaderRecord;
                config.MissingFieldFound = null;
            }
            return config;
        }

        public virtual void RegisterMapper(CsvContext context) => context.RegisterClassMap(_mapper);

        public virtual string Export(IEnumerable<T> data, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, GetConfiguration()))
            {
                RegisterMapper(csv.Context);
                csv.WriteRecords<T>(data);
            }
            return fileName;
        }

        public virtual IEnumerable<T> Import(string fileName)
        {
            IEnumerable<T> data;
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, GetConfiguration()))
            {
                RegisterMapper(csv.Context);
                data = csv.GetRecords<T>();
            }
            return data;
        }

    }
}

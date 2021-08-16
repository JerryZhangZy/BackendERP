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
            var exportData = Export(data);
            using (var fileStream = new FileStream(fileName,FileMode.OpenOrCreate))
                fileStream.Write(exportData, 0, exportData.Length);
            return fileName;
        }

        public virtual byte[] Export(IEnumerable<T> data)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                using (var csv = new CsvWriter(writer, GetConfiguration()))
                {
                    RegisterMapper(csv.Context);
                    csv.WriteRecords<T>(data);
                }
                return ms.ToArray();
            }
        }

       protected virtual void WriteCsv(T data, CsvWriter csv)
        {

        }

        public virtual IEnumerable<T> Import(string fileName)
        {
            using (var reader = new FileStream(fileName,FileMode.Open))
                return Import(reader);
        }

        public virtual IEnumerable<T> Import(byte[] fileBuffer)
        {
            using (var ms = new MemoryStream(fileBuffer))
                return Import(ms);
        }

        public virtual IEnumerable<T> Import(Stream stream)
        {
            IEnumerable<T> data;
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, GetConfiguration()))
            {
                RegisterMapper(csv.Context);
                data = csv.GetRecords<T>();
            }
            return data;
        }

    }
}

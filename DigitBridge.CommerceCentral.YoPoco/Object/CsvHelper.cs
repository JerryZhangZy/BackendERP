using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public virtual byte[] Export(IEnumerable<T> datas)
        {
            var config = GetConfiguration();
            config.HasHeaderRecord = false;
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.Context.Configuration.HasHeaderRecord = false;
                    foreach (var data in datas)
                    {
                        WriteCsv(data, csv);
                    }
                    csv.Flush();
                }
                return ms.ToArray();
            }
        }

       protected virtual void WriteCsv(T data, CsvWriter csv)
        {
            throw new Exception("must override WriteCsv  Method");
        }

        protected virtual void WriteEntities(CsvWriter csv, IList<dynamic> records,string recordType)
        {
            // get property orders in object 
            var props = new List<KeyValuePair<string, object>>();
            if (Format?.Columns == null || Format?.Columns?.Count == 0)
                props = ((ExpandoObject)records[0]).GetPropertyNames().ToList();
            // add RecordType column at first
            props.Insert(0, new KeyValuePair<string, object>("RecordType", "RecordType"));

            // Sort property of object by orders
            records[0] = ((ExpandoObject)records[0]).FilterAndSortProperty(props);

            // sort data object property orders and set type = "H"
            props[0] = new KeyValuePair<string, object>("RecordType", recordType);
            for (int i = 1; i < records.Count; i++)
                records[i] = ((ExpandoObject)records[i]).FilterAndSortProperty(props);
            csv.WriteRecords(records);
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
            IList<T> data= new List<T>();
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, GetConfiguration()))
            {
                RegisterMapper(csv.Context);
                ReadEntities(csv, data);
            }
            return data;
        }

        public virtual void ReadEntities(CsvReader reader,IList<T> data)
        {
            throw new Exception("must override ReadEntities  Method");
        }

    }
}

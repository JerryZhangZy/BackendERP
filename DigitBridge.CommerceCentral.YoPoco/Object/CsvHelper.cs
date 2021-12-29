using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using DigitBridge.Base.Utility;
using DigitBridge.DataSource;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class CsvHelper<T> : IMessage, ICsvHelper<T> where T : class, new()
    {
        protected IList<ClassMap> _mappers;
        public IList<ClassMap> CsvMappers => _mappers;
        public CsvFormat Format { get; set; }
        public int SkipLines { get; set; }

        public CsvHelper()
        {
            GetMapper();
        }
        public CsvHelper(CsvFormat format)
        {
            Format = format;
            GetMapper();
        }
        public CsvHelper(CsvFormat format, IMessage serviceMessage)
        {
            this.ServiceMessage = serviceMessage;
            Format = format;
            GetMapper();
        }

        public virtual void GetMapper() => _mappers = new List<ClassMap>() { new CsvAutoMapper<T>() };

        public virtual void GetFormat() { }

        public virtual CsvConfiguration GetConfiguration()
        {
            var cultureInfo = (Format?.CultureName).IsZero() ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(Format.CultureName);
            var config = new CsvConfiguration(cultureInfo);
            if (Format != null)
            {
                config.Delimiter = Format.Delimiter;
                config.Encoding = Format.Encoding;
                config.HasHeaderRecord = Format.HasHeaderRecord;
                SkipLines = Format.SkipLines;
            }
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            config.IgnoreBlankLines = true;
            config.ExceptionMessagesContainRawData = false;
            config.TrimOptions = TrimOptions.Trim;

            return config;
        }

        public virtual void RegisterMapper(CsvContext context)
        {
            if (CsvMappers == null || CsvMappers.Count == 0) return;
            foreach (var mapper in CsvMappers.Where(x => x != null && x.MemberMaps != null && x.MemberMaps.Count > 0))
                context.RegisterClassMap(mapper);
        }

        #region export
        public virtual string Export(IEnumerable<T> data, string fileName)
        {
            var exportData = Export(data);
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
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

        protected virtual void WriteEntities(CsvWriter csv, IList<dynamic> records, string recordType)
        {
            if (records == null || records.Count < 1 || records[0] == null)
                return;
            // get property orders in object 
            var props = new List<KeyValuePair<string, object>>();
            if (Format?.ParentObject == null || Format?.ParentObject?.Count == 0)
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

        public virtual async Task<byte[]> ExportAsync(IEnumerable<T> datas)
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
                        await WriteCsvAsync(data, csv);
                    }
                    await csv.FlushAsync();
                }
                return ms.ToArray();
            }
        }

        public virtual async Task<byte[]> ExportAsync(IEnumerable<IEnumerable<string>> lines, IEnumerable<string> headers = null)
        {
            var config = GetConfiguration();
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                using (var csv = new CsvWriter(writer, config))
                {
                    // write header
                    if (config.HasHeaderRecord && headers != null && headers.Any())
                    {
                        foreach (var item in headers)
                        {
                            csv.WriteField(item);
                        }
                        csv.NextRecord();
                    }

                    // write lines
                    if (lines != null && lines.Any())
                    {
                        foreach (var ln in lines)
                        {
                            if (ln == null || !ln.Any()) continue;
                            foreach (var item in ln)
                            {
                                csv.WriteField(item);
                            }
                            csv.NextRecord();
                        }
                    }
                    await csv.FlushAsync();
                }
                return ms.ToArray();
            }
        }

        protected virtual async Task WriteCsvAsync(T data, CsvWriter csv)
        {
            throw new Exception("must override WriteCsv  Method");
        }

        protected virtual async Task WriteEntitiesAsync(CsvWriter csv, IList<dynamic> records, string recordType)
        {
            if (records == null || records.Count < 1 || records[0] == null)
                return;
            // get property orders in object 
            var props = new List<KeyValuePair<string, object>>();
            if (Format?.ParentObject == null || Format?.ParentObject?.Count == 0)
                props = ((ExpandoObject)records[0]).GetPropertyNames().ToList();

            // add RecordType column at first
            props.Insert(0, new KeyValuePair<string, object>("RecordType", "RecordType"));

            // Sort property of object by orders
            records[0] = ((ExpandoObject)records[0]).FilterAndSortProperty(props);

            // sort data object property orders and set type = "H"
            props[0] = new KeyValuePair<string, object>("RecordType", recordType);
            for (int i = 1; i < records.Count; i++)
                records[i] = ((ExpandoObject)records[i]).FilterAndSortProperty(props);
            await csv.WriteRecordsAsync(records);
        }

        public async Task<byte[]> ExportAllColumnsAsync(IList<T> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            Format.EnableAllColumns();
            return await ExportAsync(dtos);
        }

        #endregion export

        #region import 
        public virtual IEnumerable<T> Import(string fileName)
        {
            using (var reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                return Import(reader);
        }

        public virtual IEnumerable<T> Import(byte[] fileBuffer)
        {
            using (var ms = new MemoryStream(fileBuffer))
                return Import(ms);
        }

        public virtual IEnumerable<T> Import(Stream stream)
        {
            IList<T> data = new List<T>();
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, GetConfiguration()))
            {
                RegisterMapper(csv.Context);
                ReadEntities(csv, data);
            }
            return data;
        }

        public virtual void ReadEntities(CsvReader reader, IList<T> data)
        {
            throw new Exception("must override ReadEntities  Method");
        }


        public virtual async Task<IEnumerable<T>> ImportAsync(string fileName)
        {
            using (var reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                return await ImportAsync(reader);
        }
        public virtual async Task<IEnumerable<T>> ImportAsync(Stream stream)
        {
            IList<T> data = new List<T>();
            using (var reader = new StreamReader(stream))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                using (var csv = new CsvReader(reader, GetConfiguration()))
                {
                    // skip lines
                    var lines = 0;
                    while (lines < SkipLines)
                    {
                        await reader.ReadLineAsync();
                        lines++;
                    }

                    RegisterMapper(csv.Context);
                    await ReadEntitiesAsync(csv, data);
                }
            }
            return data;
        }

        public virtual async Task ReadEntitiesAsync(CsvReader reader, IList<T> data)
        {
            throw new Exception("must override ReadEntities  Method");
        }

        protected virtual IDictionary<string, string> GetKeyField(CsvReader csv)
        {
            if (string.IsNullOrEmpty(Format.KeyName))
                return null;
            var result = new Dictionary<string, string>();
            try
            {
                var keyList = Format.KeyName.Split(",");
                foreach (var key in keyList)
                {
                    if (string.IsNullOrEmpty(key)) continue;
                    foreach (var parent in Format.ParentObject)
                    {
                        if (parent == null) continue;
                        if (csv.TryGetField(typeof(string), key, out var value))
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                result.Add(key, value.ToString());
                                break;
                            }
                        }

                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
                //throw;
            }
        }

        public virtual async Task<IEnumerable<T>> ImportAllColumnsAsync(Stream stream)
        {
            Format.EnableAllColumns();
            return await ImportAsync(stream);
        }
        public virtual async Task<IEnumerable<T>> ImportAllColumnsAsync(string fileName)
        {
            Format.EnableAllColumns();
            return await ImportAsync(fileName);
        }

        #endregion import 

        #region excel import 
        public virtual DataSet ImportExcel(string fileName)
        {
            using (var reader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                return ImportExcel(reader);
        }

        public virtual DataSet ImportExcel(Stream stream)
        {
            var ds = DataTableOperator.TranslateExcelToDataSet(stream);
            return ds;
        }
        #endregion excel import 

        #region message
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (ServiceMessage != null)
                    return ServiceMessage.Messages;

                if (_Messages == null)
                    _Messages = new List<MessageClass>();
                return _Messages;
            }
            set
            {
                if (ServiceMessage != null)
                    ServiceMessage.Messages = value;
                else
                    _Messages = value;
            }
        }
        protected IList<MessageClass> _Messages;
        public IMessage ServiceMessage { get; set; }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddInfo(message, code) : Messages.AddInfo(message, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddWarning(message, code) : Messages.AddWarning(message, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddError(message, code) : Messages.AddError(message, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddFatal(message, code) : Messages.AddFatal(message, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddDebug(message, code) : Messages.AddDebug(message, code);

        #endregion message

    }
}

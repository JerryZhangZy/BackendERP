using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface ICsvHelper<T> where T: new()
    {
        public IList<ClassMap> CsvMappers { get; }
        public CsvFormat Format { get; set; }

        CsvConfiguration GetConfiguration();
        void GetFormat();
        void GetMapper();
        void RegisterMapper(CsvContext context);

        Task<byte[]> ExportAsync(IEnumerable<IEnumerable<string>> lines, IEnumerable<string> headers = null);
        string Export(IEnumerable<T> data, string fileName);
        IEnumerable<T> Import(string fileName);

        Task<byte[]> ExportAsync(IEnumerable<T> datas);

        Task<IEnumerable<T>> ImportAsync(string fileName);
        Task<IEnumerable<T>> ImportAsync(Stream stream);
        Task ReadEntitiesAsync(CsvReader reader, IList<T> data);


        IList<T> ImportExcel(string fileName);
        IList<T> ImportExcel(Stream stream);
        void ReadDataSet(DataSet ds, IList<T> data);
        TData ReadDataRow<TData>(DataRow dataRow, TData data = null) where TData : class, new();
    }
}

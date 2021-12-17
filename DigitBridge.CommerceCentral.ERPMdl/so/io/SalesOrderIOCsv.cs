
//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using DigitBridge.CommerceCentral.YoPoco;
using CsvHelper;
using System.IO;
using DigitBridge.Base.Utility;
using System.Dynamic;
using System.Linq;
using DigitBridge.CommerceCentral.ERPDb;
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a SalesOrderIOCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class SalesOrderIOCsv : CsvHelper<SalesOrderDataDto>
    {
        public SalesOrderIOCsv(SalesOrderIOFormat format) : base(format)
        {
        }

        /// <summary>
        /// Create and Init CsvFormatMapper for each Dto Object
        /// </summary>
        public override void GetMapper()
        {
            _mappers = new List<ClassMap>()
            {
                new CsvFormatMapper<SalesOrderHeaderDto>(Format),
                new CsvFormatMapper<SalesOrderHeaderInfoDto>(Format),
                new CsvFormatMapper<SalesOrderHeaderAttributesDto>(Format),
                new CsvFormatMapper<SalesOrderItemsDto>(Format),
            };
        }

        #region import
        /// <summary>
        /// Read whole CSV content to Dto IList, use Format setting
        /// </summary>
        public override async Task ReadEntitiesAsync(CsvReader csv, IList<SalesOrderDataDto> data)
        {
            var hasReadSummary = false;
            var headerFound = false;
            SalesOrderDataDto dto = null;
            // store the last key values, if key values changed, need create new Dto object
            IDictionary<string, string> currentKeyValues = null;

            while (await csv.ReadAsync())
            {
                if (dto == null)
                    dto = new SalesOrderDataDto().NewData();
                // Header line only need read once
                if (!headerFound)
                {
                    // check at least two columns text equal header text define
                    if ((Format?.IsheaderLine(csv)).ToBool())
                    {
                        csv.ReadHeader();
                        hasReadSummary = false;
                        headerFound = true;
                        continue;
                    }
                }

                // if not define KeyName, read all line to same Dto object
                if (string.IsNullOrEmpty(Format.KeyName))
                {
                    if (!hasReadSummary)
                    {
                        await ReadSummaryRecordAsync(csv, dto);
                        hasReadSummary = true;
                    }
                    await ReadDetailRecordAsync(csv, dto);
                }
                // if defined KeyName, create new Dto object depend on key values, and read lines to same key 
                else
                {
                    // get this line key values 
                    var keyValues = GetKeyField(csv);
                    if (keyValues != null)
                    {
                        // if this line key values is not same as saved key values
                        if (!keyValues.IsEqualTo(currentKeyValues))
                        {
                            // save current key values
                            currentKeyValues = keyValues.Clone();
                            // if already read summary data, need add current data to list and create new data object
                            if (hasReadSummary)
                            {
                                data.Add(dto);
                                dto = new SalesOrderDataDto().NewData();
                                hasReadSummary = false;
                            }
                        }
                        // Dto summary object will repeat in multiple csv lines, it only need read once
                        if (!hasReadSummary)
                        {
                            await ReadSummaryRecordAsync(csv, dto);
                            hasReadSummary = true;
                        }
                        // read each detail lines
                        await ReadDetailRecordAsync(csv, dto);
                    }

                }
            }
            if (dto != null)
                data.Add(dto);
        }

        /// <summary>
        /// Read CSV line and set value to Dto summary object depend on property name
        /// </summary>
        protected virtual async Task<bool> ReadSummaryRecordAsync(CsvReader csv, SalesOrderDataDto dto)
        {
            try
            {
                dto.SalesOrderHeader = csv.GetRecord<SalesOrderHeaderDto>();
                dto.SalesOrderHeaderInfo = csv.GetRecord<SalesOrderHeaderInfoDto>();
                dto.SalesOrderHeaderAttributes = csv.GetRecord<SalesOrderHeaderAttributesDto>();
                return true;
            }
            catch (Exception e)
            {
                return false;
                //throw;
            }
        }

        /// <summary>
        /// Read CSV line and set value to Dto item object depend on property name
        /// And add item to Dto items list
        /// </summary>
        protected virtual async Task<bool> ReadDetailRecordAsync(CsvReader csv, SalesOrderDataDto dto)
        {
            try
            {
                var ln = csv.GetRecord<SalesOrderItemsDto>();
                if (ln == null || !ln.HasSKU)
                    return false;
                dto.SalesOrderItems.Add(ln);
                return true;
            }
            catch (Exception e)
            {
                return false;
                //throw;
            }
        }
        #endregion import

        #region export
        /// <summary>
        /// Export List of Dto to CSV file byte[]
        /// </summary>
        public override async Task<byte[]> ExportAsync(IEnumerable<SalesOrderDataDto> datas)
        {
            if (Format == null || datas == null || !datas.Any()) return null;

            // Export columns by index
            Format.SortByIndex();
            var dataList = datas.ToList();
            var lines = new List<IList<string>>();

            // build header line
            var headers = GetHeader(datas);

            // build each Dto object to value lines
            foreach (var data in dataList)
            {
                if (data == null) continue;
                var lns = GetDataLines(data);
                if (lns == null || lns.Count == 0) continue;
                lines = lines.Concat(lns).ToList();
            }
            // export header and value lines
            return await ExportAsync(lines, headers);
        }

        /// <summary>
        /// Build header text list by Format define, this will combine multiple object to one line
        /// </summary>
        protected virtual IList<string> GetHeader(IEnumerable<SalesOrderDataDto> datas)
        {
            if (Format == null || datas == null || !datas.Any()) return null;

            var dataList = datas.ToList();
            var headers = new List<string>();

            // build SalesOrderHeader header
            var (header1, values1) = Format.GetHeaderAndData<SalesOrderHeaderDto>(dataList[0].SalesOrderHeader);
            if (header1 != null) headers.AddRange(header1);

            // build SalesOrderHeaderInfo header
            var (header2, values2) = Format.GetHeaderAndData<SalesOrderHeaderInfoDto>(dataList[0].SalesOrderHeaderInfo);
            if (header2 != null) headers.AddRange(header2);

            // build SalesOrderHeaderAttributes header
            var (header3, values3) = Format.GetHeaderAndData<SalesOrderHeaderAttributesDto>(dataList[0].SalesOrderHeaderAttributes);
            if (header3 != null) headers.AddRange(header3);

            // build SalesOrderItems header
            var (header4, values4) = Format.GetHeaderAndData<SalesOrderItemsDto>(dataList[0].SalesOrderItems[0]);
            if (header4 != null) headers.AddRange(header4);

            return headers;
        }

        /// <summary>
        /// Build value list by Format define, this will combine multiple object to one line
        /// </summary>
        protected virtual IList<IList<string>> GetDataLines(SalesOrderDataDto data)
        {
            if (Format == null || data == null) return null;

            var lines = new List<IList<string>>();
            var lnSummary = new List<string>();

            // build SalesOrderHeader data
            var (header1, values1) = Format.GetHeaderAndData<SalesOrderHeaderDto>(data.SalesOrderHeader);
            if (values1 != null) lnSummary.AddRange(values1);

            // build SalesOrderHeaderInfo data
            var (header2, values2) = Format.GetHeaderAndData<SalesOrderHeaderInfoDto>(data.SalesOrderHeaderInfo);
            if (values2 != null) lnSummary.AddRange(values2);

            // build SalesOrderHeaderAttributes data
            var (header3, values3) = Format.GetHeaderAndData<SalesOrderHeaderAttributesDto>(data.SalesOrderHeaderAttributes);
            if (values3 != null) lnSummary.AddRange(values3);

            // build SalesOrderItems data
            foreach (var item in data.SalesOrderItems)
            {
                if (item == null) continue;
                var (headerLine, valuesLine) = Format.GetHeaderAndData<SalesOrderItemsDto>(item);
                var ln = new List<string>(lnSummary);
                if (valuesLine != null) ln.AddRange(valuesLine);
                lines.Add(ln);
            }
            return lines;
        }

        #endregion export
    }
}





              
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

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InventoryLogDataDtoCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class InventoryLogDataDtoCsv : CsvHelper<InventoryLogDataDto>
    {
        public override void GetFormat() 
        { 

        }

        public override void RegisterMapper(CsvContext context)
        {
			context.RegisterClassMap(new CsvAutoMapper<InventoryLogDto>());
        }

        public override byte[] Export(IEnumerable<InventoryLogDataDto> datas)
        {
            var config = GetConfiguration();
            config.HasHeaderRecord = false;
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                using (var csv = new CsvWriter(writer, config))
                {
                    var detailRecords = datas.MergeDetailRecord(true).ToList();
                    var props = new List<KeyValuePair<string, object>>();
                    if (Format?.Columns == null || Format?.Columns?.Count == 0)
                        props = ((ExpandoObject)detailRecords[0]).GetPropertyNames().ToList();
                    // add RecordType column at first
                    props.Insert(0, new KeyValuePair<string, object>("RecordType", "RecordType"));

                    // Sort property of object by orders
                    detailRecords[0] = ((ExpandoObject)detailRecords[0]).FilterAndSortProperty(props);

                    // sort data object property orders and set type = "H"
                    props[0] = new KeyValuePair<string, object>("RecordType", "L");
                    for (int i = 1; i < detailRecords.Count; i++)
                        detailRecords[i] = ((ExpandoObject)detailRecords[i]).FilterAndSortProperty(props);

                    csv.WriteRecords(detailRecords);
                    csv.Flush();
                }
                return ms.ToArray();
            }
        }

        public override void ReadEntities(CsvReader csv, IList<InventoryLogDataDto> data)
        {
            var isFirst = true;
            InventoryLogDataDto dto = new InventoryLogDataDto();
            while (csv.Read())
            {
                // it is header line
                if (csv.GetField(0).EqualsIgnoreSpace("RecordType"))
                {
                    csv.ReadHeader();
                    isFirst = false;
                    continue;
                }

                switch (csv.GetField(0))
                {
                    case "L":
                        if (!isFirst)
                        {
                            if (dto != null && dto.HasInventoryLog)
                                data.Add(dto);
                            dto = new InventoryLogDataDto();
                            isFirst = false;
                        }
                        dto. InventoryLog = csv.GetRecord<InventoryLogDto>();
                        break;
                    //case "L":
                    //    if (dto.Inventory == null)
                    //        dto.Inventory = new List<InventoryDto>();
                    //    var item = csv.GetRecord<InventoryDto>();
                    //    item.InventoryAttributes = csv.GetRecord<InventoryAttributesDto>();
                    //    dto.Inventory.Add(item);
                    //    break;
                }
            }
            if (dto != null)
                data.Add(dto);
        }
    }
}



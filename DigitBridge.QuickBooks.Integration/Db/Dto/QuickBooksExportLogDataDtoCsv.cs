              
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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksSettingInfoDataDtoCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class QuickBooksExportLogDataDtoCsv : CsvHelper<QuickBooksExportLogDataDto>
    {
        public override void GetFormat() 
        { 

        }

        public override void RegisterMapper(CsvContext context)
        {
			context.RegisterClassMap(new CsvAutoMapper<QuickBooksExportLogDataDto>());
        }
        
        protected override void WriteCsv(QuickBooksExportLogDataDto data, CsvWriter csv)
        {
            // combine multiple Dto to one dynamic object
            var headerRecords = data.MergeHeaderRecord(true).ToList();            
            WriteEntities(csv, headerRecords, "H");

            // Sort property of object by orders
            //var detailRecords = data.MergeDetailRecord(true).ToList();
            //WriteEntities(csv, detailRecords, "L");
        }

        public override void ReadEntities(CsvReader csv, IList<QuickBooksExportLogDataDto> data)
        {
            var isFirst = true;
            QuickBooksExportLogDataDto dto = new QuickBooksExportLogDataDto();
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
                    case "H":
                        if (!isFirst)
                        {
                            if (dto != null && dto.HasQuickBooksExportLog)
                                data.Add(dto);
                            dto = new QuickBooksExportLogDataDto();
                            isFirst = false;
                        }
                        dto. QuickBooksExportLog = csv.GetRecord<QuickBooksExportLogDto>();
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



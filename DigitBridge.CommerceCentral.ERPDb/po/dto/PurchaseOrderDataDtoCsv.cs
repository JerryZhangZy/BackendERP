              
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
    /// Represents a PurchaseOrderDataDtoCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class PurchaseOrderDataDtoCsv : CsvHelper<PurchaseOrderDataDto>
    {
        public override void GetFormat() 
        { 

        }

        public override void RegisterMapper(CsvContext context)
        {
            context.RegisterClassMap(new CsvAutoMapper<PoHeaderDto>());
            context.RegisterClassMap(new CsvAutoMapper<PoHeaderInfoDto>());
            context.RegisterClassMap(new CsvAutoMapper<PoHeaderAttributesDto>());
            context.RegisterClassMap(new CsvAutoMapper<PoItemsDto>(1));
            context.RegisterClassMap(new CsvAutoMapper<PoItemsAttributesDto>(1));
            context.RegisterClassMap(new CsvAutoMapper<PoItemsRefDto>(1));
        }
        
        protected override void WriteCsv(PurchaseOrderDataDto data, CsvWriter csv)
        {
            // combine multiple Dto to one dynamic object
            var headerRecords = data.MergeHeaderRecord(true);            
            WriteEntities(csv, headerRecords, "H");

            // Sort property of object by orders
            var detailRecords = data.MergeDetailRecord(true).ToList();
            WriteEntities(csv, detailRecords, "L");
        }

        public override void ReadEntities(CsvReader csv, IList<PurchaseOrderDataDto> data)
        {
            var isFirst = true;
            PurchaseOrderDataDto dto = new PurchaseOrderDataDto();
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
                            if (dto != null && dto.HasPoHeader)
                                data.Add(dto);
                            dto = new PurchaseOrderDataDto();
                            isFirst = false;
                        }
                        dto.PoHeader = csv.GetRecord<PoHeaderDto>();
                        dto.PoHeaderInfo = csv.GetRecord<PoHeaderInfoDto>();
                        dto.PoHeaderAttributes = csv.GetRecord<PoHeaderAttributesDto>();
                        break;
                    case "L":
                        if (dto.PoItems == null)
                            dto.PoItems = new List<PoItemsDto>();
                        var item = csv.GetRecord<PoItemsDto>();
                        item.PoItemsAttributes = csv.GetRecord<PoItemsAttributesDto>();
                        item.PoItemsRef = csv.GetRecord<PoItemsRefDto>();
                        dto.PoItems.Add(item);
                        break;
                }
            }
            if (dto != null)
                data.Add(dto);
        }
    }
}



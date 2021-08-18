


              
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
    /// Represents a OrderShipmentDataDtoCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class OrderShipmentDataDtoCsv : CsvHelper<OrderShipmentDataDto>
    {
        public override void GetFormat() 
        { 

        }

        public override void RegisterMapper(CsvContext context)
        {
			context.RegisterClassMap(new CsvAutoMapper<OrderShipmentHeaderDto>());
			context.RegisterClassMap(new CsvAutoMapper<OrderShipmentCanceledItemDto>());
			context.RegisterClassMap(new CsvAutoMapper<OrderShipmentPackageDto>());
			context.RegisterClassMap(new CsvAutoMapper<OrderShipmentShippedItemDto>());
        }
        
        protected override void WriteCsv(OrderShipmentDataDto data, CsvWriter csv)
        {
            //data.ExportFixed();
            // combine multiple Dto to one dynamic object
            var headerRecords = data.MergeHeaderRecord(true).ToList();
            
            // get property orders in object 
            var props = new List<KeyValuePair<string, object>>();
            if (Format?.Columns == null || Format?.Columns?.Count == 0)
                props = ((ExpandoObject)headerRecords[0]).GetPropertyNames().ToList();
            // add RecordType column at first
            props.Insert(0, new KeyValuePair<string, object>("RecordType", "RecordType"));

            // Sort property of object by orders
            headerRecords[0] = ((ExpandoObject)headerRecords[0]).FilterAndSortProperty(props);

            // sort data object property orders and set type = "H"
            props[0] = new KeyValuePair<string, object>("RecordType", "H");
            for (int i = 1; i < headerRecords.Count; i++)
                headerRecords[i] = ((ExpandoObject)headerRecords[i]).FilterAndSortProperty(props);            
            csv.WriteRecords(headerRecords);

            //if no details ,please comment undercode
            var detailRecords = data.MergeDetailRecord(true).ToList();
            props = new List<KeyValuePair<string, object>>();
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
        }

        public override void ReadEntities(CsvReader csv, IList<OrderShipmentDataDto> data)
        {
            var isFirst = true;
            OrderShipmentDataDto dto = new OrderShipmentDataDto();
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
                            if (dto != null && dto.HasOrderShipmentHeader)
                                data.Add(dto);
                            dto = new OrderShipmentDataDto();
                            isFirst = false;
                        }
                        dto. OrderShipmentHeader = csv.GetRecord<OrderShipmentHeaderDto>();
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



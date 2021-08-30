

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
using System.Text;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransactionDataDtoCsv Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class InvoicePaymentDataDtoCsv : CsvHelper<InvoiceTransactionDataDto>
    {
        public override void GetFormat()
        {

        }

        public override void RegisterMapper(CsvContext context)
        {
            context.RegisterClassMap(new CsvAutoMapper<InvoiceTransactionDto>());
        }

        protected override void WriteCsv(InvoiceTransactionDataDto data, CsvWriter csv)
        {
            // combine multiple Dto to one dynamic object
            var headerRecords = data.MergeHeaderRecord(true).ToList();
            WriteEntities(csv, headerRecords, "H");
        }

        public override void ReadEntities(CsvReader csv, IList<InvoiceTransactionDataDto> data)
        {
            var isFirst = true;
            InvoiceTransactionDataDto dto = new InvoiceTransactionDataDto();
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
                            if (dto != null && dto.HasInvoiceTransaction)
                                data.Add(dto);
                            dto = new InvoiceTransactionDataDto();
                            isFirst = false;
                        }
                        dto.InvoiceTransaction = csv.GetRecord<InvoiceTransactionDto>();
                        break;
                    
                }
            }
            if (dto != null)
                data.Add(dto);
        }
    }
}



              
    

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
using CsvHelper.Configuration;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a BusinessTypeIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class BusinessTypeIOFormat : CsvFormat
    {
        public BusinessTypeIOFormat() : base()
        {
            InitConfig();
			InitBusinessType();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "BusinessType Deafult Format";
            KeyName = "";
			DefaultKeyName = "BusinessTypeUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitBusinessType()
		{
			var obj = this.InitParentObject<BusinessTypeDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("BusinessTypeUuid", "", idx++, null, false),
				new CsvFormatColumn("BusinessType", "", idx++, null, false),
				new CsvFormatColumn("PriceRule", "", idx++, null, false),
				new CsvFormatColumn("Description", "", idx++, null, false),
				new CsvFormatColumn("JsonFields", "", idx++, null, false),
				new CsvFormatColumn("UpdateDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EnterBy", "", idx++, null, false),
				new CsvFormatColumn("UpdateBy", "", idx++, null, false),
			};
		}



    }
}



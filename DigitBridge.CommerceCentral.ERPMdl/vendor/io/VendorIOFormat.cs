              
    

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
    /// Represents a VendorIOFormat Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class VendorIOFormat : CsvFormat
    {
        public VendorIOFormat() : base()
        {
            InitConfig();
			InitVendor();
			InitVendorAddress();
			InitVendorAttributes();
        }

        protected virtual void InitConfig()
        {
            FormatNum = 1;
            FormatName = "Vendor Deafult Format";
            KeyName = "";
			DefaultKeyName = "VendorUuid";
            HasHeaderRecord = true;
        }

    
		protected virtual void InitVendor()
		{
			var obj = this.InitParentObject<VendorDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("RowNum", "", idx++, null, false),
				new CsvFormatColumn("DatabaseNum", "", idx++, null, false),
				new CsvFormatColumn("MasterAccountNum", "", idx++, null, false),
				new CsvFormatColumn("ProfileNum", "", idx++, null, false),
				new CsvFormatColumn("Digit_supplier_id", "", idx++, null, false),
				new CsvFormatColumn("VendorUuid", "", idx++, null, false),
				new CsvFormatColumn("VendorCode", "", idx++, null, false),
				new CsvFormatColumn("VendorName", "", idx++, null, false),
				new CsvFormatColumn("Contact", "", idx++, null, false),
				new CsvFormatColumn("Phone1", "", idx++, null, false),
				new CsvFormatColumn("Phone2", "", idx++, null, false),
				new CsvFormatColumn("Phone3", "", idx++, null, false),
				new CsvFormatColumn("Phone4", "", idx++, null, false),
				new CsvFormatColumn("Email", "", idx++, null, false),
				new CsvFormatColumn("VendorType", "", idx++, null, false),
				new CsvFormatColumn("VendorStatus", "", idx++, null, false),
				new CsvFormatColumn("BusinessType", "", idx++, null, false),
				new CsvFormatColumn("PriceRule", "", idx++, null, false),
				new CsvFormatColumn("FirstDate", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("Currency", "", idx++, null, false),
				new CsvFormatColumn("TaxRate", "", idx++, FormatType.TaxRate, false),
				new CsvFormatColumn("DiscountRate", "", idx++, FormatType.Rate, false),
				new CsvFormatColumn("ShippingCarrier", "", idx++, null, false),
				new CsvFormatColumn("ShippingClass", "", idx++, null, false),
				new CsvFormatColumn("ShippingAccount", "", idx++, null, false),
				new CsvFormatColumn("Priority", "", idx++, null, false),
				new CsvFormatColumn("Area", "", idx++, null, false),
				new CsvFormatColumn("TaxId", "", idx++, null, false),
				new CsvFormatColumn("ResaleLicense", "", idx++, null, false),
				new CsvFormatColumn("ClassCode", "", idx++, null, false),
				new CsvFormatColumn("DepartmentCode", "", idx++, null, false),
				new CsvFormatColumn("CreditAccount", "", idx++, null, false),
				new CsvFormatColumn("DebitAccount", "", idx++, null, false),
				new CsvFormatColumn("UpdateDateUtc", "", idx++, FormatType.Date, false),
				new CsvFormatColumn("EnterBy", "", idx++, null, false),
				new CsvFormatColumn("UpdateBy", "", idx++, null, false),
			};
		}



    
		protected virtual void InitVendorAddress()
		{
			var obj = this.InitParentObject<VendorAddressDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("AddressUuid", "", idx++, null, false),
				new CsvFormatColumn("AddressCode", "", idx++, null, false),
				new CsvFormatColumn("AddressType", "", idx++, null, false),
				new CsvFormatColumn("Description", "", idx++, null, false),
				new CsvFormatColumn("Name", "", idx++, null, false),
				new CsvFormatColumn("FirstName", "", idx++, null, false),
				new CsvFormatColumn("LastName", "", idx++, null, false),
				new CsvFormatColumn("Suffix", "", idx++, null, false),
				new CsvFormatColumn("Company", "", idx++, null, false),
				new CsvFormatColumn("CompanyJobTitle", "", idx++, null, false),
				new CsvFormatColumn("Attention", "", idx++, null, false),
				new CsvFormatColumn("AddressLine1", "", idx++, null, false),
				new CsvFormatColumn("AddressLine2", "", idx++, null, false),
				new CsvFormatColumn("AddressLine3", "", idx++, null, false),
				new CsvFormatColumn("City", "", idx++, null, false),
				new CsvFormatColumn("State", "", idx++, null, false),
				new CsvFormatColumn("StateFullName", "", idx++, null, false),
				new CsvFormatColumn("PostalCode", "", idx++, null, false),
				new CsvFormatColumn("PostalCodeExt", "", idx++, null, false),
				new CsvFormatColumn("County", "", idx++, null, false),
				new CsvFormatColumn("Country", "", idx++, null, false),
				new CsvFormatColumn("DaytimePhone", "", idx++, null, false),
				new CsvFormatColumn("NightPhone", "", idx++, null, false),
			};
		}



    
		protected virtual void InitVendorAttributes()
		{
			var obj = this.InitParentObject<VendorAttributesDto>();
			var idx = 0;
			obj.Columns = new List<CsvFormatColumn>()
			{
				new CsvFormatColumn("JsonFields", "", idx++, null, false),
			};
		}



    }
}



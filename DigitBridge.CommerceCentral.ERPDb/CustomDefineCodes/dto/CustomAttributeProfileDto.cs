
              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a CustomAttributeProfile Dto Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    public class CustomAttributeProfileDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

        public int? DatabaseNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDatabaseNum => DatabaseNum != null;

        public long? AttributeNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAttributeNum => AttributeNum != null;

        public int? MasterAccountNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMasterAccountNum => MasterAccountNum != null;

        public int? ProfileNum { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasProfileNum => ProfileNum != null;

        [StringLength(50, ErrorMessage = "The AttributeUuid value cannot exceed 50 characters. ")]
        public string AttributeUuid { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAttributeUuid => AttributeUuid != null;

        [StringLength(50, ErrorMessage = "The Type value cannot exceed 50 characters. ")]
        public string Type { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasType => Type != null;

        [StringLength(200, ErrorMessage = "The AttributeName value cannot exceed 200 characters. ")]
        public string AttributeName { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAttributeName => AttributeName != null;

        public int? AttributeType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAttributeType => AttributeType != null;

        public int? AttributeDataType { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasAttributeDataType => AttributeDataType != null;

        [StringLength(200, ErrorMessage = "The DefaultValue value cannot exceed 200 characters. ")]
        public string DefaultValue { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasDefaultValue => DefaultValue != null;

        public string OptionList { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasOptionList => OptionList != null;

        [StringLength(200, ErrorMessage = "The Group1 value cannot exceed 200 characters. ")]
        public string Group1 { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasGroup1 => Group1 != null;

        [StringLength(200, ErrorMessage = "The Group2 value cannot exceed 200 characters. ")]
        public string Group2 { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasGroup2 => Group2 != null;

        [StringLength(200, ErrorMessage = "The Group3 value cannot exceed 200 characters. ")]
        public string Group3 { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasGroup3 => Group3 != null;

        public int? MaxLength { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasMaxLength => MaxLength != null;

        public bool? Searchable { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasSearchable => Searchable != null;

        [DataType(DataType.DateTime)]
        public DateTime? UpdateDateUtc { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUpdateDateUtc => UpdateDateUtc != null;

        [StringLength(100, ErrorMessage = "The CreateBy value cannot exceed 100 characters. ")]
        public string CreateBy { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasCreateBy => CreateBy != null;

        [StringLength(100, ErrorMessage = "The UpdateBy value cannot exceed 100 characters. ")]
        public string UpdateBy { get; set; }
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public bool HasUpdateBy => UpdateBy != null;


        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}



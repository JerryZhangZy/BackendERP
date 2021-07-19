
              
    

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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InventoryAttributes Dto Class.
    /// NOTE: This class is generated from a T4 template Once - if you want re-generate it, you need delete cs file and generate again
    /// </summary>
    public class InventoryAttributesDto
    {
        public long? RowNum { get; set; }
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

        #region Properties - Generated 

		/// <summary>
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The ProductUuid value cannot exceed 50 characters. ")]
        public string ProductUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasProductUuid => ProductUuid != null;

		/// <summary>
		/// (Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
		[OpenApiPropertyDescription("(Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false")]
        [StringLength(50, ErrorMessage = "The InventoryUuid value cannot exceed 50 characters. ")]
        public string InventoryUuid { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasInventoryUuid => InventoryUuid != null;


        [IgnoreCompare]
        public JObject Fields { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        [OpenApiSchemaVisibility(OpenApiVisibilityType.Internal)]
        public bool HasFields => Fields != null;


        #endregion Properties - Generated 

        #region Children - Generated 

        #endregion Children - Generated 

    }
}




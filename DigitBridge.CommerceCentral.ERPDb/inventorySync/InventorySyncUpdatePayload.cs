﻿using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb.inventorySync.dto;

namespace DigitBridge.CommerceCentral.ERPDb.inventorySync
{
    /// <summary>
    /// Request and Response payload object for Patch API
    /// </summary>
    [Serializable()]
    public class InventorySyncUpdatePayload : PayloadBase
    {

        /// <summary>
        /// (Request Data) ApInvoice object to update.
        /// (Response Data) ApInvoice object which has been updated.
        /// </summary>
        [OpenApiPropertyDescription("(Request and Response) InventorySync object to update.")]
        public InventorySyncUpdateDataDto InventorySyncUpdateData { get; set; }

        [JsonIgnore] public virtual bool HasInventorySyncUpdateData => InventorySyncUpdateData != null;
        public bool ShouldSerializeInventorySyncUpdateData() => HasInventorySyncUpdateData;


    }
}

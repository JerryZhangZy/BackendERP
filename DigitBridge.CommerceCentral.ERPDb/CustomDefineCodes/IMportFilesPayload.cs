    
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class ImportFilesPayload : PayloadBase
    {
        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [JsonIgnore] public IFormFileCollection ImportFiles { get; set; }
        [JsonIgnore] public virtual bool HasCustomIOFormatList => ImportFiles != null && ImportFiles.Count > 0;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public ImportOptions Options { get; set; }
        [JsonIgnore] public virtual bool HasImportOptions => Options != null;
        public bool ShouldSerializeImportOptions() => HasImportOptions;

    }

    [Serializable()]
    public class ImportOptions
    {

        /// <summary>
        /// (Request Data) Uuid for this Import batch, this will be Azure Blob name.
        /// </summary>
        public string ImportUuid { get; set; }
        public virtual bool HasImportUuid => !string.IsNullOrEmpty(ImportUuid);

        /// <summary>
        /// (Request Data) Import data type of this Import batch.
        /// </summary>
        public string FormatType { get; set; }
        public virtual bool HasFormatType => !string.IsNullOrEmpty(FormatType);

        /// <summary>
        /// (Request Data) Import format number of this Import batch.
        /// </summary>
        public int FormatNumber { get; set; }
        public virtual bool HasFormatNumber => FormatNumber > 0;
    }

}


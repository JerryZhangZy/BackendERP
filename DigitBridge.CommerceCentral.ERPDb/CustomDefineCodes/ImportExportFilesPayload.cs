
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
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class ImportExportFilesPayload : PayloadBase
    {

        private string exportUuid;
        [JsonIgnore]
        /// <summary>
        /// (Response Data) Uuid for this Export, this will be Azure Blob name.
        /// </summary>
        public string ExportUuid
        {
            get
            {
                if (exportUuid.IsZero())
                    return Options?.ExportUuid;
                return exportUuid;
            }
            set
            { exportUuid = value; }
        }
        public virtual bool HasExportUuid => !string.IsNullOrEmpty(ExportUuid);

        /// <summary>
        /// (Request Data) Uuid for this Import batch, this will be Azure Blob name.
        /// </summary>
        public string ImportUuid { get; set; }
        public virtual bool HasImportUuid => !string.IsNullOrEmpty(ImportUuid);

        /// <summary>
        /// (Response Data) List result which load filter.
        /// </summary>
        [JsonIgnore] public IDictionary<string, byte[]> ExportFiles { get; set; }
        [JsonIgnore] public virtual bool HasExportFiles => ExportFiles != null && ExportFiles.Count > 0;

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        [JsonIgnore] public IDictionary<string, byte[]> ImportFiles { get; set; }
        [JsonIgnore] public virtual bool HasImportFiles => ImportFiles != null && ImportFiles.Count > 0;
        public virtual ImportExportFilesPayload AddFiles(IFormFileCollection files)
        {
            if (files == null) return this;
            ImportFiles = new Dictionary<string, byte[]>();
            foreach (var file in files)
            {
                if (file == null) continue;
                if (!file.FileName.ToLower().EndsWith("csv") && !file.FileName.ToLower().EndsWith("txt")) continue;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        file.OpenReadStream().CopyTo(ms);
                        ImportFiles.TryAdd(file.FileName.ToLower(), ms.ToArray());
                    }
                }
                catch (Exception e)
                {
                    ReturnError($"Import file {file.FileName} has error. {e.Message}");
                    continue;
                }
            }
            return this;
        }

        private ImportExportOptions options;
        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public ImportExportOptions Options
        {
            get
            {
                if (options != null)
                {
                    options.DatabaseNum = this.DatabaseNum;
                    options.MasterAccountNum = this.MasterAccountNum;
                    options.ProfileNum = this.ProfileNum;
                }
                return options;
            }
            set { options = value; }
        }
        [JsonIgnore] public virtual bool HasOptions => Options != null;
        public bool ShouldSerializeOptions() => HasOptions;

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        public IList<string> FileNames { get; set; }
        public virtual bool HasFileNames => FileNames != null && FileNames.Count > 0;

        public virtual ImportExportFilesPayload AddFileName(string fileNames)
        {
            if (!HasFileNames)
                FileNames = new List<string>();
            if (!FileNames.Contains(fileNames))
                FileNames.Add(fileNames);
            return this;
        }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public ImportExportResult Result { get; set; }
        [JsonIgnore] public virtual bool HasResult => Result != null;
        public bool ShouldSerializeResult() => HasResult;

        public bool LoadRequest(HttpRequest req)
        {
            Options = req.Form["options"].ToString().JsonToObject<ImportExportOptions>();
            if (Options == null)
                return false;

            ImportUuid = Options.ImportUuid;
            AddFiles(req.Form.Files);
            return true;
        }
    }

    [Serializable()]
    public class ImportExportOptions
    {
        public bool IsImport { get; set; }

        public virtual int DatabaseNum { get; set; }
        public virtual bool HasDatabaseNum => DatabaseNum > 0;


        public virtual int MasterAccountNum { get; set; }
        public virtual bool HasMasterAccountNum => MasterAccountNum > 0;

        public virtual int ProfileNum { get; set; }
        public virtual bool HasProfileNum => ProfileNum > 0;

        /// <summary>
        /// (Request Data) Uuid for this Import batch, this will be Azure Blob name.
        /// </summary>
        public string ImportUuid { get; set; }
        public virtual bool HasImportUuid => !string.IsNullOrEmpty(ImportUuid);

        /// <summary>
        /// (Response Data) Uuid for this Export, this will be Azure Blob name.
        /// </summary>
        public string ExportUuid { get; set; }
        public virtual bool HasExportUuid => !string.IsNullOrEmpty(ExportUuid);

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

        /// <summary>
        /// Export filter
        /// </summary>
        public JObject Filter { get; set; }
        public virtual bool HasFilter => Filter != null;
    }

    [Serializable()]
    public class ImportExportResult
    {
        public bool IsImport { get; set; }

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

        /// <summary>
        /// (Response Data) finish files count.
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// (Response Data) finish data entity count.
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        public IList<string> FileNames { get; set; }
        public virtual bool HasFileNames => FileNames != null && FileNames.Count > 0;

        public virtual ImportExportResult AddFileName(string fileNames)
        {
            if (!HasFileNames)
                FileNames = new List<string>();
            if (!FileNames.Contains(fileNames))
                FileNames.Add(fileNames);
            return this;
        }

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        public IList<ImportExportResultMessage> Results { get; set; }
        public virtual bool HasResults => Results != null && Results.Count > 0;

        public virtual ImportExportResult AddResult(ImportExportResultMessage result)
        {
            if (!HasResults)
                Results = new List<ImportExportResultMessage>();
            Results.Add(result);
            AddFileName(result.FileName);
            return this;
        }
    }

    [Serializable()]
    public class ImportExportResultMessage
    {
        /// <summary>
        /// (Response Data) Impor/Export file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// (Response Data) Impor/Export file status.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// (Request Data) process date time.
        /// </summary>
        public DateTime ProcessDate { get; set; }

        /// <summary>
        /// (Request Data) process data uuid.
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// (Request Data) process data number or code.
        /// </summary>
        public string ProcessNumber { get; set; }

        /// <summary>
        /// (Request Data) message.
        /// </summary>
        public string Message { get; set; }

    }

}


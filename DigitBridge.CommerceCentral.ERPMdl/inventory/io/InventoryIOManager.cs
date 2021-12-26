
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
using System.Threading.Tasks;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using DigitBridge.Base.Common;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a InventoryIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class InventoryIOManager : IInventoryIOManager, IMessage
    {
        public InventoryIOManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            Format = new InventoryIOFormat();
        }

        #region Service Property
        [XmlIgnore, JsonIgnore]
        protected ExportBlobService _ExportBlobService;
        [XmlIgnore, JsonIgnore]
        public ExportBlobService ExportBlobService
        {
            get
            {
                if (_ExportBlobService is null)
                    _ExportBlobService = new ExportBlobService();
                return _ExportBlobService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected ImportBlobService _ImportBlobService;
        [XmlIgnore, JsonIgnore]
        public ImportBlobService ImportBlobService
        {
            get
            {
                if (_ImportBlobService is null)
                    _ImportBlobService = new ImportBlobService();
                return _ImportBlobService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryService _InventoryService;
        [XmlIgnore, JsonIgnore]
        public InventoryService InventoryService
        {
            get
            {
                if (_InventoryService is null)
                    _InventoryService = new InventoryService(dbFactory);
                return _InventoryService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected CustomIOFormatService _CustomIOFormatService;
        [XmlIgnore, JsonIgnore]
        public CustomIOFormatService CustomIOFormatService
        {
            get
            {
                if (_CustomIOFormatService is null)
                    _CustomIOFormatService = new CustomIOFormatService(dbFactory);
                return _CustomIOFormatService;
            }
        }


        [XmlIgnore, JsonIgnore]
        protected InventoryList _InventoryList;
        [XmlIgnore, JsonIgnore]
        public InventoryList InventoryList
        {
            get
            {
                if (_InventoryList is null)
                    _InventoryList = new InventoryList(dbFactory);
                return _InventoryList;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryManager _InventoryManager;
        [XmlIgnore, JsonIgnore]
        public InventoryManager InventoryManager
        {
            get
            {
                if (_InventoryManager is null)
                    _InventoryManager = new InventoryManager(dbFactory);
                return _InventoryManager;
            }
        }
        #endregion

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages

        [XmlIgnore, JsonIgnore]
        public InventoryIOFormat Format { get; set; }

        [XmlIgnore, JsonIgnore]
        protected InventoryIOCsv _InventoryIOCsv;
        [XmlIgnore, JsonIgnore]
        public InventoryIOCsv InventoryIOCsv
        {
            get
            {
                if (_InventoryIOCsv is null)
                    _InventoryIOCsv = new InventoryIOCsv(Format);
                return _InventoryIOCsv;
            }
        }

        #region import
        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<bool> ImportAsync(ImportExportFilesPayload payload)
        {
            if (payload == null || payload.MasterAccountNum <= 0 || payload.ProfileNum <= 0 || string.IsNullOrWhiteSpace(payload.ImportUuid))
                return false;
            var blobSvc = ImportBlobService;
            // load import files and import options from Blob
            if (!(await blobSvc.LoadFromBlobAsync(payload)))
            {
                payload.ReturnError($"Import files or options not found.");
                return false;
            }
            // load format object
            if (!(await LoadFormatAsync(payload)))
                return false;

            var dtoList = new List<InventoryDataDto>();
            // load each file to import
            foreach (var fileName in payload.FileNames)
            {
                // load one file stram from Blob
                using (var ms = new MemoryStream())
                {
                    if (!(await blobSvc.LoadFileFromBlobAsync(fileName, payload, ms)))
                        continue;
                    ms.Position = 0;

                    var dto = await ImportAsync(ms);
                    if (dto == null || dto.Count == 0) continue;
                    dtoList.AddRange(dto);
                }
            }

            // Verify Dto and save dto to database
            var manager = InventoryManager;
            if (!await manager.SaveImportDataAsync(dtoList))
            {
                this.Messages.Add(manager.Messages);
                return false;
            }
            return true;
        }

        public async Task<bool> LoadFormatAsync(ImportExportFilesPayload payload)
        {
            if (!(await CustomIOFormatService.GetByNumberAsync(
                payload.MasterAccountNum,
                payload.ProfileNum,
                payload.Options.FormatType,
                payload.Options.FormatNumber
                )))
            {
                payload.ReturnError($"Import format not found.");
                return false;
            }
            Format = new InventoryIOFormat();
            Format.LoadFormat(CustomIOFormatService.Data.CustomIOFormat.GetFormatObject());
            return true;
        }
        #endregion


        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<IList<InventoryDataDto>> ImportAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await InventoryIOCsv.ImportAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, import all columns
        /// </summary>
        public async Task<IList<InventoryDataDto>> ImportAllColumnsAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await InventoryIOCsv.ImportAllColumnsAsync(stream);
            return result.ToList();
        }

        public async Task<bool> ExportAsync(ImportExportFilesPayload payload, IList<InventoryDataDto> datas)
        {
            payload.ExportFiles = new Dictionary<string, byte[]>();
            var files = await InventoryIOCsv.ExportAsync(datas);
            //payload.ExportFiles.Add(payload.ExportUuid + ".csv", files);
            payload.ExportFiles.Add(GetFielName(payload.Options), files);
            return true;
        }

        /// <summary>
        /// Export Dto list to csv file stream, depend on format setting
        /// </summary>
        public async Task<byte[]> ExportAsync(IList<InventoryDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await InventoryIOCsv.ExportAsync(dtos);
        }

        /// <summary>
        /// Export Dto list to csv file stream, expot all columns
        /// </summary>
        public async Task<byte[]> ExportAllColumnsAsync(IList<InventoryDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await InventoryIOCsv.ExportAllColumnsAsync(dtos);
        }

        /// <summary>
        /// Export Dto list to csv file stream, depend on format setting
        /// </summary>
        public async Task<bool> ExportAsync(ImportExportFilesPayload payload)
        {
            if (payload == null || payload.MasterAccountNum <= 0 || payload.ProfileNum <= 0 || string.IsNullOrWhiteSpace(payload.ExportUuid))
                return false;

            // load export options from Blob
            if (!(await ExportBlobService.LoadOptionsFromBlobAsync(payload)))
            {
                this.Messages.Add(ExportBlobService.Messages);
                return false;
            }

            // query salesorder list
            var soDatas = await GetInventoryDatasAsync(payload);
            if (soDatas == null)
            {
                AddError("Get inventory datas error");
                return false;
            }

            // load format object
            if (!(await LoadFormatAsync(payload)))
            {
                this.Messages.Add(payload.Messages);
                return false;
            }

            // export file as format
            await ExportAsync(payload, soDatas);

            //save export file to blob
            return await ExportBlobService.SaveFilesToBlobAsync(payload);
        }
        protected async Task<IList<InventoryDataDto>> GetInventoryDatasAsync(ImportExportFilesPayload payload)
        {
            var inventoryPayload = new InventoryPayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                DatabaseNum = payload.DatabaseNum,
                Filter = payload.Options.Filter,
                LoadAll = true,
                IsQueryTotalCount = false,
            };

            await InventoryList.GetProductListAsync(inventoryPayload);
            if (!inventoryPayload.Success)
            {
                this.Messages.Add(InventoryList.Messages);
                return null;
            }

            var json = inventoryPayload.InventoryList.ToString();
            if (json.IsZero()) return new List<InventoryDataDto>();

            var queryResult = JArray.Parse(inventoryPayload.InventoryList.ToString());
            var rownums = queryResult.Select(i => i.Value<long>("rowNum")).ToList();

            var list = new List<InventoryDataDto>();
            foreach (var rownum in rownums)
            {
                if (await InventoryService.GetDataAsync(rownum))
                    list.Add(InventoryService.ToDto());
            }
            return list;
        }

        private string GetFielName(ImportExportOptions importExportOptions)
        {
            if (int.TryParse(importExportOptions.FormatType, out int formatType))
            {
                ActivityLogType activityLogType = (ActivityLogType)formatType;
                return activityLogType.ToString() + DateTime.UtcNow.ToString("yyyyMMdd") + ".csv";
            }
            return importExportOptions.ExportUuid + ".csv";
        }
    }
}


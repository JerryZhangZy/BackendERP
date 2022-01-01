    
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
using DigitBridge.CommerceCentral.ERPMdl.po;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a PoTransactionIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class PoReceiveIOManager : IPoReceiveIOManager, IMessage
    {
        public PoReceiveIOManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            Format = new PoReceiveIOFormat();
        }

        #region Service Property

        [XmlIgnore, JsonIgnore]
        protected PoReceiveList _PoReceiveList;
        [XmlIgnore, JsonIgnore]
        public PoReceiveList PoReceiveList
        {
            get
            {
                if (_PoReceiveList is null)
                    _PoReceiveList = new PoReceiveList(dbFactory);
                return _PoReceiveList;
            }
        }
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
        protected PoReceiveManager _PoReceiveManager;
        [XmlIgnore, JsonIgnore]
        public PoReceiveManager PoReceiveManager
        {
            get
            {
                if (_PoReceiveManager is null)
                    _PoReceiveManager = new PoReceiveManager(dbFactory);
                return _PoReceiveManager;
            }
        }

        //

        [XmlIgnore, JsonIgnore]
        protected PoReceiveService _PoReceiveService;
        [XmlIgnore, JsonIgnore]
        public PoReceiveService PoReceiveService
        {
            get
            {
                if (_PoReceiveService is null)
                    _PoReceiveService = new PoReceiveService(dbFactory);
                return _PoReceiveService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected PoTransactionService _PoTransactionService;
        [XmlIgnore, JsonIgnore]
        public PoTransactionService PoTransactionService
        {
            get
            {
                if (_PoTransactionService is null)
                    _PoTransactionService = new PoTransactionService(dbFactory);
                return _PoTransactionService;
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
        public PoReceiveIOFormat Format { get; set; }

        [XmlIgnore, JsonIgnore]
        protected PoReceiveIOCsv _PoTransactionIOCsv;
        [XmlIgnore, JsonIgnore]
        public PoReceiveIOCsv PoTransactionIOCsv
        {
            get
            {
                if (_PoTransactionIOCsv is null)
                    _PoTransactionIOCsv = new PoReceiveIOCsv(Format);
                return _PoTransactionIOCsv;
            }
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
            Format = new PoReceiveIOFormat();
            Format.LoadFormat(CustomIOFormatService.Data.CustomIOFormat.GetFormatObject());
            return true;
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

            var dtoList = new List<PoTransactionDataDto>();
            // load each file to import
            foreach (var fileName in payload.FileNames)
            {
                // load one file stram from Blob
                using (var ms = new MemoryStream())
                {
                    if (!(await blobSvc.LoadFileFromBlobAsync(fileName, payload, ms)))
                        continue;


                    var dto = await ImportAsync(ms);
                    if (dto == null || dto.Count == 0) continue;
                    dtoList.AddRange(dto);
                }
            }

            // Verify Dto and save dto to database
            var manager = PoReceiveManager;
            await manager.SaveImportDataAsync(dtoList, payload);
            return true;
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<IList<PoTransactionDataDto>> ImportAsync(byte[] buffer)
        {
            
            if (buffer == null) return null;
            using (var ms = new MemoryStream(buffer))
                return await ImportAsync(ms);
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<IList<PoTransactionDataDto>> ImportAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await PoTransactionIOCsv.ImportAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, import all columns
        /// </summary>
        public async Task<IList<PoTransactionDataDto>> ImportAllColumnsAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await PoTransactionIOCsv.ImportAllColumnsAsync(stream);
            return result.ToList();
        }
        #endregion import 

        #region export
        /// <summary>
        /// Export Dto list to csv file stream, depend on format setting
        /// </summary>
        public async Task<byte[]> ExportAsync(IList<PoTransactionDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await PoTransactionIOCsv.ExportAsync(dtos);
        }
        public async Task<bool> ExportAsync(ImportExportFilesPayload payload, IList<PoTransactionDataDto> datas)
        {
            payload.ExportFiles = new Dictionary<string, byte[]>();
            var files = await PoTransactionIOCsv.ExportAsync(datas);
            //payload.ExportFiles.Add(payload.ExportUuid + ".csv", files);
            payload.ExportFiles.Add(GetFielName(payload.Options), files);
            return true;
        }
        /// <summary>
        /// Export Dto list to csv file stream, expot all columns
        /// </summary>
        public async Task<byte[]> ExportAllColumnsAsync(IList<PoTransactionDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await PoTransactionIOCsv.ExportAllColumnsAsync(dtos);
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

            // query   list
            var datas = await GetPoReceiveDatasAsync(payload);
            if (datas == null)
            {
                AddError("Get PoReceive datas error");
                return false;
            }

            // load format object
            if (!(await LoadFormatAsync(payload)))
            {
                this.Messages.Add(payload.Messages);
                return false;
            }

            // export file as format
            await ExportAsync(payload, datas);

            //save export file to blob
            return await ExportBlobService.SaveFilesToBlobAsync(payload);
        }
        protected async Task<IList<PoTransactionDataDto>> GetPoReceiveDatasAsync(ImportExportFilesPayload payload)
        {
            var poReceivePayload = new PoReceivePayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                DatabaseNum = payload.DatabaseNum,
                Filter = payload.Options.Filter,
                LoadAll = true,
                IsQueryTotalCount = false,
            };

            await PoReceiveList.GetPoReceiveListAsync(poReceivePayload);
            if (!poReceivePayload.Success)
            {
                this.Messages.Add(PoReceiveList.Messages);
                return null;
            }

            var json = poReceivePayload.PoTransactionList.ToString();
            if (json.IsZero()) return new List<PoTransactionDataDto>();

            var queryResult = JArray.Parse(poReceivePayload.PoTransactionList.ToString());
            var rownums = queryResult.Select(i => i.Value<long>("rowNum")).ToList();

            return await PoReceiveService.GetPoReceiveDtosAsync(rownums);
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

        #endregion export

    }
}


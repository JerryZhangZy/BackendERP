
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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a SalesOrderIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class SalesOrderIOManager : ISalesOrderIOManager, IMessage
    {
        public SalesOrderIOManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            Format = new SalesOrderIOFormat();
        }

        #region Service Property

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
        protected SalesOrderService _SalesOrderService;
        [XmlIgnore, JsonIgnore]
        public SalesOrderService SalesOrderService
        {
            get
            {
                if (_SalesOrderService is null)
                    _SalesOrderService = new SalesOrderService(dbFactory);
                return _SalesOrderService;
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
        protected SalesOrderManager _SalesOrderManager;
        [XmlIgnore, JsonIgnore]
        public SalesOrderManager SalesOrderManager
        {
            get
            {
                if (_SalesOrderManager is null)
                    _SalesOrderManager = new SalesOrderManager(dbFactory);
                return _SalesOrderManager;
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
        public SalesOrderIOFormat Format { get; set; }

        [XmlIgnore, JsonIgnore]
        protected SalesOrderIOCsv _SalesOrderIOCsv;
        [XmlIgnore, JsonIgnore]
        public SalesOrderIOCsv SalesOrderIOCsv
        {
            get
            {
                if (_SalesOrderIOCsv is null)
                    _SalesOrderIOCsv = new SalesOrderIOCsv(Format);
                return _SalesOrderIOCsv;
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
                this.Messages.Add(payload.Messages);
                return false;
            }
            // load format object
            if (!(await LoadFormatAsync(payload)))
            {
                this.Messages.Add(payload.Messages);
                return false;
            }

            var dtoList = new List<SalesOrderDataDto>();
            // load each file to import
            foreach (var fileName in payload.FileNames)
            {
                // load one file stram from Blob
                using (var ms = new MemoryStream())
                {
                    if (!(await blobSvc.LoadFileFromBlobAsync(fileName, payload, ms)))
                    {
                        this.Messages.Add(payload.Messages);
                        continue;
                    }

                    var dto = await ImportAsync(ms);
                    if (dto == null || dto.Count == 0) continue;
                    dtoList.AddRange(dto);
                }
            }

            // Verify Dto and save dto to database
            var manager = SalesOrderManager;
            if (!await manager.SaveImportDataAsync(dtoList))
            {
                this.Messages.Add(manager.Messages);
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
            Format = new SalesOrderIOFormat();
            Format.LoadFormat(CustomIOFormatService.Data.CustomIOFormat.GetFormatObject());
            return true;
        }

        #endregion
        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<IList<SalesOrderDataDto>> ImportAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await SalesOrderIOCsv.ImportAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, import all columns
        /// </summary>
        public async Task<IList<SalesOrderDataDto>> ImportAllColumnsAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await SalesOrderIOCsv.ImportAllColumnsAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Export Dto list to csv file stream, depend on format setting
        /// </summary>
        public async Task<byte[]> ExportAsync(IList<SalesOrderDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await SalesOrderIOCsv.ExportAsync(dtos);
        }

        /// <summary>
        /// Export Dto list to csv file stream, expot all columns
        /// </summary>
        public async Task<byte[]> ExportAllColumnsAsync(IList<SalesOrderDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await SalesOrderIOCsv.ExportAllColumnsAsync(dtos);
        }

    }
}


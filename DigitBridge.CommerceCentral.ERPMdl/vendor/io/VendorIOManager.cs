    
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
    /// Represents a VendorIOManager Class.
    /// NOTE: This class is generated from a T4 template Once - you you wanr re-generate it, you need delete cs file and generate again
    /// </summary>
    [Serializable()]
    public partial class VendorIOManager : IVendorIOManager, IMessage
    {
        public VendorIOManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            Format = new VendorIOFormat();
        }

        #region Service Property
        [XmlIgnore, JsonIgnore]
        protected VendorService _VendorService;
        [XmlIgnore, JsonIgnore]
        public VendorService VendorService
        {
            get
            {
                if (_VendorService is null)
                    _VendorService = new VendorService(dbFactory);
                return _VendorService;
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
        public VendorIOFormat Format { get; set; }

        [XmlIgnore, JsonIgnore]
        protected VendorIOCsv _VendorIOCsv;
        [XmlIgnore, JsonIgnore]
        public VendorIOCsv VendorIOCsv
        {
            get
            {
                if (_VendorIOCsv is null)
                    _VendorIOCsv = new VendorIOCsv(Format);
                return _VendorIOCsv;
            }
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, depend on format setting
        /// </summary>
        public async Task<IList<VendorDataDto>> ImportAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await VendorIOCsv.ImportAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Import csv file stream to IList of Dto, import all columns
        /// </summary>
        public async Task<IList<VendorDataDto>> ImportAllColumnsAsync(Stream stream)
        {
            if (stream == null) return null;
            var result = await VendorIOCsv.ImportAllColumnsAsync(stream);
            return result.ToList();
        }

        /// <summary>
        /// Export Dto list to csv file stream, depend on format setting
        /// </summary>
        public async Task<byte[]> ExportAsync(IList<VendorDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await VendorIOCsv.ExportAsync(dtos);
        }

        /// <summary>
        /// Export Dto list to csv file stream, expot all columns
        /// </summary>
        public async Task<byte[]> ExportAllColumnsAsync(IList<VendorDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
                return null;
            return await VendorIOCsv.ExportAllColumnsAsync(dtos);
        }

    }
}

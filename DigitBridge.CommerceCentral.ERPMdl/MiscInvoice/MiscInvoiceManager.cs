//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a MiscInvoiceService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class MiscInvoiceManager :  IMiscInvoiceManager , IMessage
    {

        public MiscInvoiceManager() : base() {}

        public MiscInvoiceManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected MiscInvoiceService _miscInvoiceService;
        [XmlIgnore, JsonIgnore]
        public MiscInvoiceService miscInvoiceService
        {
            get
            {
                if (_miscInvoiceService is null)
                    _miscInvoiceService = new MiscInvoiceService(dbFactory);
                return _miscInvoiceService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected MiscInvoiceDataDtoCsv _miscInvoiceDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public MiscInvoiceDataDtoCsv miscInvoiceDataDtoCsv
        {
            get
            {
                if (_miscInvoiceDataDtoCsv is null)
                    _miscInvoiceDataDtoCsv = new MiscInvoiceDataDtoCsv();
                return _miscInvoiceDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected MiscInvoiceList _miscInvoiceList;
        [XmlIgnore, JsonIgnore]
        public MiscInvoiceList miscInvoiceList
        {
            get
            {
                if (_miscInvoiceList is null)
                    _miscInvoiceList = new MiscInvoiceList(dbFactory);
                return _miscInvoiceList;
            }
        }

        public async Task<byte[]> ExportAsync(MiscInvoicePayload payload)
        {
            var rowNumList =await miscInvoiceList.GetRowNumListAsync(payload);
            var dtoList = new List<MiscInvoiceDataDto>();
           foreach(var x in rowNumList)
            {
                if (miscInvoiceService.GetData(x))
                    dtoList.Add(miscInvoiceService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new MiscInvoiceDataDto());
            return miscInvoiceDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(MiscInvoicePayload payload)
        {
            var rowNumList =miscInvoiceList.GetRowNumList(payload);
            var dtoList = new List<MiscInvoiceDataDto>();
            foreach (var x in rowNumList)
            {
                if (miscInvoiceService.GetData(x))
                    dtoList.Add(miscInvoiceService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new MiscInvoiceDataDto());
            return miscInvoiceDataDtoCsv.Export(dtoList);
        }

        public void Import(MiscInvoicePayload payload, IFormFileCollection files)
        {
            if(files==null||files.Count==0)
            {
                AddError("no files upload");
                return;
            }
            foreach(var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = miscInvoiceDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.MiscInvoice = item;
                    if (miscInvoiceService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in miscInvoiceService.Messages)
                            Messages.Add(msg);
                        miscInvoiceService.Messages.Clear();
                    }
                }
                if (payload.HasMiscInvoice)
                    payload.MiscInvoice = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(MiscInvoicePayload payload, IFormFileCollection files)
        {
            if(files==null||files.Count==0)
            {
                AddError("no files upload");
                return;
            }
            foreach(var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list =miscInvoiceDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.MiscInvoice = item;
                    if (await miscInvoiceService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in miscInvoiceService.Messages)
                            Messages.Add(msg);
                        miscInvoiceService.Messages.Clear();
                    }
                }
                if (payload.HasMiscInvoice)
                    payload.MiscInvoice = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

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
    }
}
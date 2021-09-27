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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksConnectionInfoService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class QuickBooksConnectionInfoManager :  IQuickBooksConnectionInfoManager , IMessage
    {

        public QuickBooksConnectionInfoManager() : base() {}

        public QuickBooksConnectionInfoManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected QuickBooksConnectionInfoService _quickBooksConnectionInfoService;
        [XmlIgnore, JsonIgnore]
        public QuickBooksConnectionInfoService quickBooksConnectionInfoService
        {
            get
            {
                if (_quickBooksConnectionInfoService is null)
                    _quickBooksConnectionInfoService = new QuickBooksConnectionInfoService(dbFactory);
                return _quickBooksConnectionInfoService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected QuickBooksConnectionInfoDataDtoCsv _quickBooksConnectionInfoDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public QuickBooksConnectionInfoDataDtoCsv quickBooksConnectionInfoDataDtoCsv
        {
            get
            {
                if (_quickBooksConnectionInfoDataDtoCsv is null)
                    _quickBooksConnectionInfoDataDtoCsv = new QuickBooksConnectionInfoDataDtoCsv();
                return _quickBooksConnectionInfoDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected QuickBooksConnectionInfoList _quickBooksConnectionInfoList;
        [XmlIgnore, JsonIgnore]
        public QuickBooksConnectionInfoList quickBooksConnectionInfoList
        {
            get
            {
                if (_quickBooksConnectionInfoList is null)
                    _quickBooksConnectionInfoList = new QuickBooksConnectionInfoList(dbFactory);
                return _quickBooksConnectionInfoList;
            }
        }

        public async Task<byte[]> ExportAsync(QuickBooksConnectionInfoPayload payload)
        {
            var rowNumList =await quickBooksConnectionInfoList.GetRowNumListAsync(payload);
            var dtoList = new List<QuickBooksConnectionInfoDataDto>();
           foreach(var x in rowNumList)
            {
                if (quickBooksConnectionInfoService.GetData(x))
                    dtoList.Add(quickBooksConnectionInfoService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new QuickBooksConnectionInfoDataDto());
            return quickBooksConnectionInfoDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(QuickBooksConnectionInfoPayload payload)
        {
            var rowNumList =quickBooksConnectionInfoList.GetRowNumList(payload);
            var dtoList = new List<QuickBooksConnectionInfoDataDto>();
            foreach (var x in rowNumList)
            {
                if (quickBooksConnectionInfoService.GetData(x))
                    dtoList.Add(quickBooksConnectionInfoService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new QuickBooksConnectionInfoDataDto());
            return quickBooksConnectionInfoDataDtoCsv.Export(dtoList);
        }

        public void Import(QuickBooksConnectionInfoPayload payload, IFormFileCollection files)
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
                var list = quickBooksConnectionInfoDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.QuickBooksConnectionInfo = item;
                    if (quickBooksConnectionInfoService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in quickBooksConnectionInfoService.Messages)
                            Messages.Add(msg);
                        quickBooksConnectionInfoService.Messages.Clear();
                    }
                }
                if (payload.HasQuickBooksConnectionInfo)
                    payload.QuickBooksConnectionInfo = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(QuickBooksConnectionInfoPayload payload, IFormFileCollection files)
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
                var list =quickBooksConnectionInfoDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.QuickBooksConnectionInfo = item;
                    if (await quickBooksConnectionInfoService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in quickBooksConnectionInfoService.Messages)
                            Messages.Add(msg);
                        quickBooksConnectionInfoService.Messages.Clear();
                    }
                }
                if (payload.HasQuickBooksConnectionInfo)
                    payload.QuickBooksConnectionInfo = null;
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
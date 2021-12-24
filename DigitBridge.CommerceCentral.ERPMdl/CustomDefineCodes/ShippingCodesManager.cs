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
    /// Represents a ShippingCodesService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ShippingCodesManager :  IShippingCodesManager , IMessage
    {

        public ShippingCodesManager() : base() {}

        public ShippingCodesManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected ShippingCodesService _shippingCodesService;
        [XmlIgnore, JsonIgnore]
        public ShippingCodesService shippingCodesService
        {
            get
            {
                if (_shippingCodesService is null)
                    _shippingCodesService = new ShippingCodesService(dbFactory);
                return _shippingCodesService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected ShippingCodesDataDtoCsv _shippingCodesDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public ShippingCodesDataDtoCsv shippingCodesDataDtoCsv
        {
            get
            {
                if (_shippingCodesDataDtoCsv is null)
                    _shippingCodesDataDtoCsv = new ShippingCodesDataDtoCsv();
                return _shippingCodesDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected ShippingCodesList _shippingCodesList;
        [XmlIgnore, JsonIgnore]
        public ShippingCodesList shippingCodesList
        {
            get
            {
                if (_shippingCodesList is null)
                    _shippingCodesList = new ShippingCodesList(dbFactory);
                return _shippingCodesList;
            }
        }

        public async Task<byte[]> ExportAsync(ShippingCodesPayload payload)
        {
            var rowNumList =await shippingCodesList.GetRowNumListAsync(payload);
            var dtoList = new List<ShippingCodesDataDto>();
           foreach(var x in rowNumList)
            {
                if (shippingCodesService.GetData(x))
                    dtoList.Add(shippingCodesService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ShippingCodesDataDto());
            return shippingCodesDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(ShippingCodesPayload payload)
        {
            var rowNumList =shippingCodesList.GetRowNumList(payload);
            var dtoList = new List<ShippingCodesDataDto>();
            foreach (var x in rowNumList)
            {
                if (shippingCodesService.GetData(x))
                    dtoList.Add(shippingCodesService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ShippingCodesDataDto());
            return shippingCodesDataDtoCsv.Export(dtoList);
        }

        public void Import(ShippingCodesPayload payload, IFormFileCollection files)
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
                var list = shippingCodesDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ShippingCodes = item;
                    if (shippingCodesService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in shippingCodesService.Messages)
                            Messages.Add(msg);
                        shippingCodesService.Messages.Clear();
                    }
                }
                if (payload.HasShippingCodes)
                    payload.ShippingCodes = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(ShippingCodesPayload payload, IFormFileCollection files)
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
                var list =shippingCodesDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ShippingCodes = item;
                    if (await shippingCodesService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in shippingCodesService.Messages)
                            Messages.Add(msg);
                        shippingCodesService.Messages.Clear();
                    }
                }
                if (payload.HasShippingCodes)
                    payload.ShippingCodes = null;
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
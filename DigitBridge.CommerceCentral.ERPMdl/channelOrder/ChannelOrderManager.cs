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
    /// Represents a ChannelOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ChannelOrderManager : IMessage
    {
        protected ChannelOrderService channelOrderService;
        protected ChannelOrderDataDtoCsv channelOrderDataDtoCsv;
        protected ChannelOrderList channelOrderList;

        public ChannelOrderManager() : base() {}
        public ChannelOrderManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            channelOrderService = new ChannelOrderService(dbFactory);
            channelOrderDataDtoCsv = new ChannelOrderDataDtoCsv();
            channelOrderList = new ChannelOrderList(dbFactory);
        }

        public async Task<byte[]> ExportAsync(ChannelOrderPayload payload)
        {
            var rowNumList =await channelOrderList.GetRowNumListAsync(payload);
            var dtoList = new List<ChannelOrderDataDto>();
           foreach(var x in rowNumList)
            {
                if (channelOrderService.GetData(x))
                    dtoList.Add(channelOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ChannelOrderDataDto());
            return channelOrderDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(ChannelOrderPayload payload)
        {
            var rowNumList =channelOrderList.GetRowNumList(payload);
            var dtoList = new List<ChannelOrderDataDto>();
            foreach (var x in rowNumList)
            {
                if (channelOrderService.GetData(x))
                    dtoList.Add(channelOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ChannelOrderDataDto());
            return channelOrderDataDtoCsv.Export(dtoList);
        }

        public void Import(ChannelOrderPayload payload, IFormFileCollection files)
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
                var list = channelOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ChannelOrder = item;
                    if (channelOrderService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in channelOrderService.Messages)
                            Messages.Add(msg);
                        channelOrderService.Messages.Clear();
                    }
                }
                if (payload.HasChannelOrder)
                    payload.ChannelOrder = null;
                AddInfo($"FIle:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(ChannelOrderPayload payload, IFormFileCollection files)
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
                var list =channelOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ChannelOrder = item;
                    if (await channelOrderService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in channelOrderService.Messages)
                            Messages.Add(msg);
                        channelOrderService.Messages.Clear();
                    }
                }
                if (payload.HasChannelOrder)
                    payload.ChannelOrder = null;
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

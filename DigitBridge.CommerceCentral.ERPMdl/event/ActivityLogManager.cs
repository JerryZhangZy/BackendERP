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
    /// Represents a ActivityLogService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ActivityLogManager :  IActivityLogManager , IMessage
    {

        public ActivityLogManager() : base() {}

        public ActivityLogManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected ActivityLogService _activityLogService;
        [XmlIgnore, JsonIgnore]
        public ActivityLogService activityLogService
        {
            get
            {
                if (_activityLogService is null)
                    _activityLogService = new ActivityLogService(dbFactory);
                return _activityLogService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected ActivityLogDataDtoCsv _activityLogDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public ActivityLogDataDtoCsv activityLogDataDtoCsv
        {
            get
            {
                if (_activityLogDataDtoCsv is null)
                    _activityLogDataDtoCsv = new ActivityLogDataDtoCsv();
                return _activityLogDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected ActivityLogList _activityLogList;
        [XmlIgnore, JsonIgnore]
        public ActivityLogList activityLogList
        {
            get
            {
                if (_activityLogList is null)
                    _activityLogList = new ActivityLogList(dbFactory);
                return _activityLogList;
            }
        }

        public async Task<byte[]> ExportAsync(ActivityLogPayload payload)
        {
            var rowNumList =await activityLogList.GetRowNumListAsync(payload);
            var dtoList = new List<ActivityLogDataDto>();
           foreach(var x in rowNumList)
            {
                if (activityLogService.GetData(x))
                    dtoList.Add(activityLogService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ActivityLogDataDto());
            return activityLogDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(ActivityLogPayload payload)
        {
            var rowNumList =activityLogList.GetRowNumList(payload);
            var dtoList = new List<ActivityLogDataDto>();
            foreach (var x in rowNumList)
            {
                if (activityLogService.GetData(x))
                    dtoList.Add(activityLogService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new ActivityLogDataDto());
            return activityLogDataDtoCsv.Export(dtoList);
        }

        public void Import(ActivityLogPayload payload, IFormFileCollection files)
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
                var list = activityLogDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ActivityLog = item;
                    if (activityLogService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in activityLogService.Messages)
                            Messages.Add(msg);
                        activityLogService.Messages.Clear();
                    }
                }
                if (payload.HasActivityLog)
                    payload.ActivityLog = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(ActivityLogPayload payload, IFormFileCollection files)
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
                var list =activityLogDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.ActivityLog = item;
                    if (await activityLogService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in activityLogService.Messages)
                            Messages.Add(msg);
                        activityLogService.Messages.Clear();
                    }
                }
                if (payload.HasActivityLog)
                    payload.ActivityLog = null;
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

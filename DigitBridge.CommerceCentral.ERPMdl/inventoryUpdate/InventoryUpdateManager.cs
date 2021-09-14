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
    /// Represents a InventoryUpdateService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InventoryUpdateManager :  IInventoryUpdateManager , IMessage
    {

        public InventoryUpdateManager() : base() {}

        public InventoryUpdateManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateService _inventoryUpdateService;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateService inventoryUpdateService
        {
            get
            {
                if (_inventoryUpdateService is null)
                    _inventoryUpdateService = new InventoryUpdateService(dbFactory);
                return _inventoryUpdateService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateDataDtoCsv _inventoryUpdateDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateDataDtoCsv inventoryUpdateDataDtoCsv
        {
            get
            {
                if (_inventoryUpdateDataDtoCsv is null)
                    _inventoryUpdateDataDtoCsv = new InventoryUpdateDataDtoCsv();
                return _inventoryUpdateDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InventoryUpdateList _inventoryUpdateList;
        [XmlIgnore, JsonIgnore]
        public InventoryUpdateList inventoryUpdateList
        {
            get
            {
                if (_inventoryUpdateList is null)
                    _inventoryUpdateList = new InventoryUpdateList(dbFactory);
                return _inventoryUpdateList;
            }
        }

        public async Task<byte[]> ExportAsync(InventoryUpdatePayload payload)
        {
            var rowNumList =await inventoryUpdateList.GetRowNumListAsync(payload);
            var dtoList = new List<InventoryUpdateDataDto>();
           foreach(var x in rowNumList)
            {
                if (inventoryUpdateService.GetData(x))
                    dtoList.Add(inventoryUpdateService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryUpdateDataDto());
            return inventoryUpdateDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InventoryUpdatePayload payload)
        {
            var rowNumList =inventoryUpdateList.GetRowNumList(payload);
            var dtoList = new List<InventoryUpdateDataDto>();
            foreach (var x in rowNumList)
            {
                if (inventoryUpdateService.GetData(x))
                    dtoList.Add(inventoryUpdateService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InventoryUpdateDataDto());
            return inventoryUpdateDataDtoCsv.Export(dtoList);
        }

        public void Import(InventoryUpdatePayload payload, IFormFileCollection files)
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
                var tlist = inventoryUpdateDataDtoCsv.Import(file.OpenReadStream());
                var list = new List<InventoryUpdateDataDto>();
                foreach (var item in tlist)
                {
                    if (!item.HasInventoryUpdateHeader)
                    {
                        if (item.HasInventoryUpdateItems)
                        {
                            foreach (var uitem in item.InventoryUpdateItems)
                            {
                                var data = new InventoryUpdateDataDto()
                                {
                                    InventoryUpdateHeader = new InventoryUpdateHeaderDto
                                    {
                                        WarehouseCode = uitem.WarehouseCode,
                                        WarehouseUuid = uitem.WarehouseUuid,
                                        DatabaseNum = payload.DatabaseNum,
                                        ProfileNum = payload.ProfileNum,
                                        MasterAccountNum = payload.MasterAccountNum,
                                        InventoryUpdateType = (int)payload.InventoryUpdateType,
                                    },
                                    InventoryUpdateItems = new List<InventoryUpdateItemsDto>() { uitem }
                                };
                                list.Add(data);
                            }
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.InventoryUpdate = item;
                    if (inventoryUpdateService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryUpdateService.Messages)
                            Messages.Add(msg);
                        inventoryUpdateService.Messages.Clear();
                    }
                }
                if (payload.HasInventoryUpdate)
                    payload.InventoryUpdate = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(InventoryUpdatePayload payload, IFormFileCollection files)
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
                var tlist =inventoryUpdateDataDtoCsv.Import(file.OpenReadStream());
                var list = new List<InventoryUpdateDataDto>();
                foreach(var item in tlist)
                {
                    if (!item.HasInventoryUpdateHeader)
                    {
                        if (item.HasInventoryUpdateItems)
                        {
                            foreach(var uitem in item.InventoryUpdateItems)
                            {
                                var data = new InventoryUpdateDataDto()
                                {
                                    InventoryUpdateHeader=new InventoryUpdateHeaderDto
                                    {
                                        WarehouseCode=uitem.WarehouseCode,
                                        WarehouseUuid=uitem.WarehouseUuid,
                                        DatabaseNum=payload.DatabaseNum,
                                        ProfileNum=payload.ProfileNum,
                                        MasterAccountNum=payload.MasterAccountNum,
                                        InventoryUpdateType= (int)payload.InventoryUpdateType,
                                    },
                                    InventoryUpdateItems = new List<InventoryUpdateItemsDto>() { uitem }
                                };
                                list.Add(data);
                            }
                        }
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.InventoryUpdate = item;
                    if (await inventoryUpdateService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in inventoryUpdateService.Messages)
                            Messages.Add(msg);
                        inventoryUpdateService.Messages.Clear();
                    }
                }
                if (payload.HasInventoryUpdate)
                    payload.InventoryUpdate = null;
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
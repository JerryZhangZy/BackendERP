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
    /// Represents a PurchaseOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class PurchaseOrderManager :  IPurchaseOrderManager , IMessage
    {

        public PurchaseOrderManager() : base() {}

        public PurchaseOrderManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected PurchaseOrderService _purchaseOrderService;
        [XmlIgnore, JsonIgnore]
        public PurchaseOrderService purchaseOrderService
        {
            get
            {
                if (_purchaseOrderService is null)
                    _purchaseOrderService = new PurchaseOrderService(dbFactory);
                return _purchaseOrderService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected PurchaseOrderDataDtoCsv _purchaseOrderDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public PurchaseOrderDataDtoCsv purchaseOrderDataDtoCsv
        {
            get
            {
                if (_purchaseOrderDataDtoCsv is null)
                    _purchaseOrderDataDtoCsv = new PurchaseOrderDataDtoCsv();
                return _purchaseOrderDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected PurchaseOrderList _purchaseOrderList;
        [XmlIgnore, JsonIgnore]
        public PurchaseOrderList purchaseOrderList
        {
            get
            {
                if (_purchaseOrderList is null)
                    _purchaseOrderList = new PurchaseOrderList(dbFactory);
                return _purchaseOrderList;
            }
        }


        [XmlIgnore, JsonIgnore]
        protected InitNumbersService _initNumbersService;
        [XmlIgnore, JsonIgnore]
        public InitNumbersService initNumbersService
        {
            get
            {
                if (_initNumbersService is null)
                    _initNumbersService = new InitNumbersService(dbFactory);
                return _initNumbersService;
            }
        }

        public async Task<byte[]> ExportAsync(PurchaseOrderPayload payload)
        {
            var rowNumList =await purchaseOrderList.GetRowNumListAsync(payload);
            var dtoList = new List<PurchaseOrderDataDto>();
           foreach(var x in rowNumList)
            {
                if (purchaseOrderService.GetData(x))
                    dtoList.Add(purchaseOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new PurchaseOrderDataDto());
            return purchaseOrderDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(PurchaseOrderPayload payload)
        {
            var rowNumList =purchaseOrderList.GetRowNumList(payload);
            var dtoList = new List<PurchaseOrderDataDto>();
            foreach (var x in rowNumList)
            {
                if (purchaseOrderService.GetData(x))
                    dtoList.Add(purchaseOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new PurchaseOrderDataDto());
            return purchaseOrderDataDtoCsv.Export(dtoList);
        }

        public void Import(PurchaseOrderPayload payload, IFormFileCollection files)
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
                var list = purchaseOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.PurchaseOrder = item;
                    if (purchaseOrderService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in purchaseOrderService.Messages)
                            Messages.Add(msg);
                        purchaseOrderService.Messages.Clear();
                    }
                }
                if (payload.HasPurchaseOrder)
                    payload.PurchaseOrder = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(PurchaseOrderPayload payload, IFormFileCollection files)
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
                var list =purchaseOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
                {
                    payload.PurchaseOrder = item;
                    if (await purchaseOrderService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in purchaseOrderService.Messages)
                            Messages.Add(msg);
                        purchaseOrderService.Messages.Clear();
                    }
                }
                if (payload.HasPurchaseOrder)
                    payload.PurchaseOrder = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }



        public async Task<string> GetInitNumberForCustomerAsync(int masterAccountNum, int profileNum, string customerUuid)
        {
            return await initNumbersService.GetInitNumberForCustomerAsync(masterAccountNum, profileNum, customerUuid, "po");
        }

        public async Task<bool> UpdateInitNumberForCustomerAsync(int masterAccountNum, int profileNum, string customerUuid,int currentNumber)
        {
            return await initNumbersService.UpdateInitNumberForCustomerAsync(masterAccountNum, profileNum, customerUuid, "po", currentNumber);
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

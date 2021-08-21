




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
    /// Represents a SalesOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class SalesOrderManager : IMessage
    {
        protected SalesOrderService salesOrderService;
        protected SalesOrderDataDtoCsv salesOrderDataDtoCsv;
        protected SalesOrderList salesOrderList;

        private ChannelOrderService _channelOrderSrv;
        private DCAssignmentService _dcAssignmentSrv;

        private SalesOrderTransfer _soTransfer;

        public SalesOrderManager() : base() { }
        public SalesOrderManager(IDataBaseFactory dbFactory, string userId)
        {
            SetDataBaseFactory(dbFactory);
            salesOrderService = new SalesOrderService(dbFactory);
            salesOrderDataDtoCsv = new SalesOrderDataDtoCsv();
            salesOrderList = new SalesOrderList(dbFactory);

            _channelOrderSrv = new ChannelOrderService(dbFactory);
            _dcAssignmentSrv = new DCAssignmentService(dbFactory);

            _soTransfer = new SalesOrderTransfer(this, userId);
        }
        public async Task<byte[]> ExportAsync(SalesOrderPayload payload)
        {
            var rowNumList = await salesOrderList.GetRowNumListAsync(payload);
            var dtoList = new List<SalesOrderDataDto>();
            foreach (var x in rowNumList)
            {
                if (salesOrderService.GetData(x))
                    dtoList.Add(salesOrderService.ToDto());
            }
            if (dtoList.Count == 0)
                dtoList.Add(new SalesOrderDataDto());
            return salesOrderDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(SalesOrderPayload payload)
        {
            var rowNumList = salesOrderList.GetRowNumList(payload);
            var dtoList = new List<SalesOrderDataDto>();
            foreach (var x in rowNumList)
            {
                if (salesOrderService.GetData(x))
                    dtoList.Add(salesOrderService.ToDto());
            }
            if (dtoList.Count == 0)
                dtoList.Add(new SalesOrderDataDto());
            return salesOrderDataDtoCsv.Export(dtoList);
        }


        public void Import(SalesOrderPayload payload, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                AddError("no files upload");
                return;
            }
            foreach (var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = salesOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.SalesOrder = item;
                    if (salesOrderService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in salesOrderService.Messages)
                            Messages.Add(msg);
                        salesOrderService.Messages.Clear();
                    }
                }
                if (payload.HasSalesOrder)
                    payload.SalesOrder = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(SalesOrderPayload payload, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                AddError("no files upload");
                return;
            }
            foreach (var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = salesOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.SalesOrder = item;
                    if (await salesOrderService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in salesOrderService.Messages)
                            Messages.Add(msg);
                        salesOrderService.Messages.Clear();
                    }
                }
                if (payload.HasSalesOrder)
                    payload.SalesOrder = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }
        public async Task<bool> CreateSalesOrderByChannelOrderIdAsync(string centralOrderUuid)
        {
            //Get CentralOrder by uuid
            bool ret = await _channelOrderSrv.GetDataByIdAsync(centralOrderUuid);

            var soDataList = new List<SalesOrderData>();

            if (ret)
            {
                //Get DCAssignments by uuid
                var dcAssigmentDataList = await _dcAssignmentSrv.GetByCentralOrderUuidAsync(centralOrderUuid);
                if (dcAssigmentDataList != null && dcAssigmentDataList.Count > 0)
                {
                    //Create SalesOrder

                    foreach (var dcAssigmentData in dcAssigmentDataList)
                    {
                        var coData = _channelOrderSrv.Data;
                        string dcUuid = dcAssigmentData.OrderDCAssignmentHeader.OrderDCAssignmentUuid;

                        bool exists = false;
                        using (var trs = new ScopedTransaction(dbFactory))
                        {
                            exists = await SalesOrderHelper.ExistOrderDCAssignmentUuidAsync(dcUuid);
                        }
                        if (!exists)
                        {
                            var soData = _soTransfer.FromChannelOrder(dcAssigmentData, coData);

                            if (soData != null)
                            {
                                soDataList.Add(soData);
                            }
                            else
                            {
                                ret = false;
                            }
                        }
                    }

                }
            }

            if (ret)
            {
                //Transfer ChannelOrder Data to SalesOrderData
                ret = await CreateSalesOrdersAsync(soDataList);
            }

            return ret;
        }

        public async Task<bool> CreateSalesOrdersAsync(IList<SalesOrderData> soDataList)
        {
            if (soDataList == null || soDataList.Count == 0)
            {
                return true;
            }

            foreach (var soData in soDataList)
            {
                salesOrderService.Add();

                //soData.CheckIntegrity();

                salesOrderService.AttachData(soData);

                if (await salesOrderService.SaveDataAsync() == false)
                {
                    return false;
                }
            }

            return true;
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




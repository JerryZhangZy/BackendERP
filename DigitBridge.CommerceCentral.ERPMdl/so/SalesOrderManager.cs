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
    public class SalesOrderManager :  ISalesOrderManager , IMessage
    {

        public SalesOrderManager() : base() {}

        public SalesOrderManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected SalesOrderService _salesOrderService;
        [XmlIgnore, JsonIgnore]
        public SalesOrderService salesOrderService
        {
            get
            {
                if (_salesOrderService is null)
                    _salesOrderService = new SalesOrderService();
                return _salesOrderService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected SalesOrderDataDtoCsv _salesOrderDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public SalesOrderDataDtoCsv salesOrderDataDtoCsv
        {
            get
            {
                if (_salesOrderDataDtoCsv is null)
                    _salesOrderDataDtoCsv = new SalesOrderDataDtoCsv();
                return _salesOrderDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected SalesOrderList _salesOrderList;
        [XmlIgnore, JsonIgnore]
        public SalesOrderList salesOrderList
        {
            get
            {
                if (_salesOrderList is null)
                    _salesOrderList = new SalesOrderList(dbFactory);
                return _salesOrderList;
            }
        }
        
        [XmlIgnore, JsonIgnore]
        protected InvoiceTransfer _salesOrderTransfer;
        [XmlIgnore, JsonIgnore]
        public InvoiceTransfer salesOrderTransfer
        {
            get
            {
                if (_salesOrderTransfer is null)
                    _salesOrderTransfer = new SalesOrderTransfer(this, string.Empty);
                return _salesOrderTransfer;
            }
        }

        private InvoiceTransfer _soTransfer;

        public async Task<byte[]> ExportAsync(SalesOrderPayload payload)
        {
            var rowNumList =await salesOrderList.GetRowNumListAsync(payload);
            var dtoList = new List<SalesOrderDataDto>();
           foreach(var x in rowNumList)
            {
                if (salesOrderService.GetData(x))
                    dtoList.Add(salesOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new SalesOrderDataDto());
            return salesOrderDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(SalesOrderPayload payload)
        {
            var rowNumList =salesOrderList.GetRowNumList(payload);
            var dtoList = new List<SalesOrderDataDto>();
            foreach (var x in rowNumList)
            {
                if (salesOrderService.GetData(x))
                    dtoList.Add(salesOrderService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new SalesOrderDataDto());
            return salesOrderDataDtoCsv.Export(dtoList);
        }

        public void Import(SalesOrderPayload payload, IFormFileCollection files)
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
                var list = salesOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
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
                AddInfo($"FIle:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(SalesOrderPayload payload, IFormFileCollection files)
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
                var list =salesOrderDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach(var item in list)
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

        /// <summary>
        /// Load Central Order and order assignment, create Sales order for each order assignment.
        /// </summary>
        /// <param name="centralOrderUuid"></param>
        /// <returns>Success Create Sales Order</returns>
        public async Task<bool> CreateSalesOrderByChannelOrderIdAsync(string centralOrderUuid)
        {
            //Get CentralOrder by uuid
            var coData = await GetChannelOrderAsync(centralOrderUuid);
            if (coData is null)
            {
                AddError($"ChannelOrder uuid {centralOrderUuid} not found.");
                return false;
            }

            var dcAssigmentDataList = await GetDCAssignmentAsync(centralOrderUuid);
            if (dcAssigmentDataList == null || dcAssigmentDataList.Count == 0)
            {
                AddError("ChannelOrder DC Assigment not found.");
                return false;
            }

            var soDataList = new List<SalesOrderData>();
            //Create SalesOrder
            foreach (var dcAssigmentData in dcAssigmentDataList)
            {
                var soData = await CreateSalesOrdersAsync(coData, dcAssigmentData);
                if (soData != null)
                    soDataList.Add(soData);
            }
            return soDataList.Count > 0;
        }

        /// <summary>
        /// Get ChannelOrderData by centralOrderUuid
        /// </summary>
        /// <param name="centralOrderUuid"></param>
        /// <returns>ChannelOrderData</returns>
        protected async Task<ChannelOrderData> GetChannelOrderAsync(string centralOrderUuid)
        {
            //Get CentralOrder by uuid
            var channelOrderSrv = new ChannelOrderService(dbFactory);

            if (!(await channelOrderSrv.GetDataByIdAsync(centralOrderUuid)))
                return null;
            return channelOrderSrv.Data;
        }

        /// <summary>
        /// Get DCAssignmentData list by centralOrderUuid
        /// </summary>
        /// <param name="centralOrderUuid"></param>
        /// <returns>list of ChannelOrderData</returns>
        protected async Task<IList<DCAssignmentData>> GetDCAssignmentAsync(string centralOrderUuid)
        {
            var dcAssignmentSrv = new DCAssignmentService(dbFactory);
            return await dcAssignmentSrv.GetByCentralOrderUuidAsync(centralOrderUuid);
        }

        /// <summary>
        /// Check DCAssignment is already exist in sales order
        /// </summary>
        /// <param name="orderDCAssignmentNum"></param>
        /// <returns>Exist or Not</returns>
        protected async Task<bool> ExistDCAssignmentInSalesOrderAsync(long orderDCAssignmentNum)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await SalesOrderHelper.ExistOrderDCAssignmentNumAsync(orderDCAssignmentNum);
        }

        /// <summary>
        /// Create one sales order from one ChannelOrder and one DCAssignment.
        /// </summary>
        /// <param name="coData"></param>
        /// <param name="dcAssigmentData"></param>
        /// <returns>Success Create Sales Order</returns>
        public async Task<SalesOrderData> CreateSalesOrdersAsync(ChannelOrderData coData, DCAssignmentData dcAssigmentData)
        {
            if ((await ExistDCAssignmentInSalesOrderAsync(dcAssigmentData.OrderDCAssignmentHeader.OrderDCAssignmentNum)))
            {
                AddError("ChannelOrder DC Assigment has transferred to sales order.");
                return null;
            }

            salesOrderService.DetachData(null);
            salesOrderService.Add();
            var soData = _soTransfer.FromChannelOrder(dcAssigmentData, coData);
            salesOrderService.AttachData(soData);
            salesOrderService.Data.CheckIntegrity();

            if (await salesOrderService.SaveDataAsync())
                return salesOrderService.Data;
            return null;
        }

        //public async Task<bool> CreateSalesOrdersAsync(IList<SalesOrderData> soDataList)
        //{
        //    if (soDataList == null || soDataList.Count == 0)
        //    {
        //        return true;
        //    }

        //    foreach (var soData in soDataList)
        //    {
        //        salesOrderService.Add();

        //        soData.CheckIntegrity();

        //        salesOrderService.AttachData(soData);

        //        if (await salesOrderService.SaveDataAsync() == false)
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

    }
}

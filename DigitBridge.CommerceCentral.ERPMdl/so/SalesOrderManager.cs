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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a SalesOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class SalesOrderManager : ISalesOrderManager, IMessage
    {

        public SalesOrderManager() : base() { }

        public SalesOrderManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region Service Property
        [XmlIgnore, JsonIgnore]
        protected SalesOrderService _salesOrderService;
        [XmlIgnore, JsonIgnore]
        public SalesOrderService salesOrderService
        {
            get
            {
                if (_salesOrderService is null)
                    _salesOrderService = new SalesOrderService(dbFactory);
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
        protected SalesOrderTransfer _salesOrderTransfer;
        [XmlIgnore, JsonIgnore]
        public SalesOrderTransfer salesOrderTransfer
        {
            get
            {
                if (_salesOrderTransfer is null)
                    _salesOrderTransfer = new SalesOrderTransfer(this, string.Empty);
                return _salesOrderTransfer;
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

        private CustomerService _customerService;
        public CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
            }
        }

        private InventoryService _inventoryService;
        public InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion


        public async Task<byte[]> ExportAsync(SalesOrderPayload payload)
        {
            var rowNumList = await salesOrderList.GetRowNumListAsync(payload);
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

        public byte[] Export(SalesOrderPayload payload)
        {
            var rowNumList = salesOrderList.GetRowNumList(payload);
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
                AddInfo($"FIle:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
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
        /// <returns>Success Create Sales Order, SalesOrder Numbers</returns>
        public async Task<(bool, List<string>)> CreateSalesOrderByChannelOrderIdAsync(string centralOrderUuid)
        {
            //Get CentralOrder by uuid
            var coData = await GetChannelOrderAsync(centralOrderUuid);
            if (coData is null)
            {
                AddError($"ChannelOrder uuid {centralOrderUuid} not found.");
                return (false, null);
            }

            var dcAssigmentDataList = await GetDCAssignmentAsync(centralOrderUuid);
            if (dcAssigmentDataList == null || dcAssigmentDataList.Count == 0)
            {
                AddError("ChannelOrder DC Assigment not found.");
                return (false, null);
            }
            var dcAssData = dcAssigmentDataList.Select(p => new
            {
                Uuid = p.OrderDCAssignmentHeader.OrderDCAssignmentUuid,
                Count = p.OrderDCAssignmentLine.Count()
            }).ToList();
            var dcAssDataNoLines = dcAssData.Where(p => p.Count == 0).ToList();
            if(dcAssDataNoLines.Count > 0)
            {
                AddError($"No DCAssigmentLine for DCAssingmentUuid {string.Join(",", dcAssDataNoLines.Select(p => p.Uuid))}.");
                return (false, null);
            }
            var soDataList = new List<SalesOrderData>();
            //Create SalesOrder
            foreach (var dcAssigmentData in dcAssigmentDataList)
            {
                var soData = await CreateSalesOrdersAsync(coData, dcAssigmentData);
                if (soData != null)
                    soDataList.Add(soData);
            }
            bool ret = soDataList.Count > 0;
            List<string> salesOrderUuids = soDataList.Select(p => p.SalesOrderHeader.SalesOrderUuid).ToList();
            return (ret, salesOrderUuids);
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

        protected async Task<string> GetWarehouseByDCAssignmentAsync(DCAssignmentData dcAssigmentData)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InventoryServiceHelper.GetWarehouseCodeByDistributionCenterNumAsync(
                    dcAssigmentData.OrderDCAssignmentHeader.DistributionCenterNum,
                    dcAssigmentData.OrderDCAssignmentHeader.MasterAccountNum,
                    dcAssigmentData.OrderDCAssignmentHeader.ProfileNum);
        }

        /// <summary>
        /// Create one sales order from one ChannelOrder and one DCAssignment.
        /// </summary>
        /// <param name="coData"></param>
        /// <param name="dcAssigmentData"></param>
        /// <returns>Success Create Sales Order</returns>
        public async Task<SalesOrderData> CreateSalesOrdersAsync(ChannelOrderData coData, DCAssignmentData dcAssigmentData)
        {
            long orderDCAssignmentNum = dcAssigmentData.OrderDCAssignmentHeader.OrderDCAssignmentNum;
            if ((await ExistDCAssignmentInSalesOrderAsync(orderDCAssignmentNum)))
            {
                AddError($"Channel OrderDCAssigmentNum {orderDCAssignmentNum} has transferred to sales order.");
                return null;
            }

            SalesOrderTransfer soTransfer = new SalesOrderTransfer(this, "");
            dcAssigmentData.WarehouseCode = await GetWarehouseByDCAssignmentAsync(dcAssigmentData);

            var soSrv = new SalesOrderService(dbFactory);

            soSrv.DetachData(null);
            soSrv.Add();

            // create SalesOrderData from DCAssignmentData and ChannelOrderData
            var soData = soTransfer.FromChannelOrder(dcAssigmentData, coData, soSrv);

            // load customer
            await CheckCustomerAsync(soSrv);

            // check sku and warehouse exist, otherwise add new SKU and Warehouse
            await CheckInventoryAsync(soSrv);

            soSrv.Data.CheckIntegrity();

            if (await soSrv.SaveCurrentDataAsync())
                return soSrv.Data;
            return null;
        }

        protected async Task<bool> CheckCustomerAsync(SalesOrderService service)
        {
            if (service == null || service.Data == null)
                return false;

            var data = service.Data;
            if (!string.IsNullOrEmpty(data.SalesOrderHeader.CustomerCode))
                return true;

            var find = new CustomerFindClass()
            {
                MasterAccountNum = data.SalesOrderHeader.MasterAccountNum,
                ProfileNum = data.SalesOrderHeader.ProfileNum,
                ChannelNum = data.SalesOrderHeaderInfo.ChannelNum,
                ChannelAccountNum = data.SalesOrderHeaderInfo.ChannelAccountNum
            };

            // if channel customer not exist, add new customer for this channel
            if (!(await customerService.GetCustomerByCustomerFindAsync(find)))
            {
                customerService.NewData();
                var newCustomer = customerService.Data;
                newCustomer.Customer.MasterAccountNum = data.SalesOrderHeader.MasterAccountNum;
                newCustomer.Customer.ProfileNum = data.SalesOrderHeader.ProfileNum;
                newCustomer.Customer.DatabaseNum = data.SalesOrderHeader.DatabaseNum;
                newCustomer.Customer.CustomerUuid = Guid.NewGuid().ToString();
                newCustomer.Customer.CustomerCode = $"Channel_{data.SalesOrderHeaderInfo.ChannelNum}_{data.SalesOrderHeaderInfo.ChannelAccountNum}";
                newCustomer.Customer.CustomerName = $"Customer Account for Channel {data.SalesOrderHeaderInfo.ChannelNum}, Channel Account {data.SalesOrderHeaderInfo.ChannelAccountNum}";
                newCustomer.Customer.CustomerType = (int)CustomerType.Channel;
                newCustomer.Customer.CustomerStatus = (int)CustomerStatus.Active;
                newCustomer.Customer.FirstDate = DateTime.UtcNow.Date;
                newCustomer.Customer.ChannelNum = data.SalesOrderHeaderInfo.ChannelNum;
                newCustomer.Customer.ChannelAccountNum = data.SalesOrderHeaderInfo.ChannelAccountNum;
                newCustomer.AddCustomerAddress(new CustomerAddress()
                {
                    AddressCode = AddressCodeType.Ship,
                    Name = data.SalesOrderHeaderInfo.ShipToName,
                    Company = data.SalesOrderHeaderInfo.ShipToCompany,
                    AddressLine1 = data.SalesOrderHeaderInfo.ShipToAddressLine1,
                    AddressLine2 = data.SalesOrderHeaderInfo.ShipToAddressLine2,
                    AddressLine3 = data.SalesOrderHeaderInfo.ShipToAddressLine3,
                    City = data.SalesOrderHeaderInfo.ShipToCity,
                    State = data.SalesOrderHeaderInfo.ShipToState,
                    StateFullName = data.SalesOrderHeaderInfo.ShipToStateFullName,
                    PostalCode = data.SalesOrderHeaderInfo.ShipToPostalCode,
                    PostalCodeExt = data.SalesOrderHeaderInfo.ShipToPostalCodeExt,
                    County = data.SalesOrderHeaderInfo.ShipToCounty,
                    Country = data.SalesOrderHeaderInfo.ShipToCountry,
                    Email = data.SalesOrderHeaderInfo.ShipToEmail,
                    DaytimePhone = data.SalesOrderHeaderInfo.ShipToDaytimePhone,
                    NightPhone = data.SalesOrderHeaderInfo.ShipToNightPhone,
                });
                await customerService.AddCustomerAsync(newCustomer);
            }

            data.SalesOrderHeader.CustomerUuid = customerService.Data.Customer.CustomerUuid;
            data.SalesOrderHeader.CustomerCode = customerService.Data.Customer.CustomerCode;
            data.SalesOrderHeader.CustomerName = customerService.Data.Customer.CustomerName;
            data.SalesOrderHeader.Terms = customerService.Data.Customer.Terms;
            data.SalesOrderHeader.TermsDays = customerService.Data.Customer.TermsDays;
            return true;
        }


        protected async Task<bool> CheckInventoryAsync(SalesOrderService service)
        {
            if (service == null || service.Data == null || service.Data.SalesOrderItems == null || service.Data.SalesOrderItems.Count == 0)
                return false;

            var data = service.Data;
            var header = data.SalesOrderHeader;
            var masterAccountNum = data.SalesOrderHeader.MasterAccountNum;
            var profileNum = data.SalesOrderHeader.ProfileNum;
            var find = data.SalesOrderItems.Select(x => new InventoryFindClass() { SKU = x.SKU, WarehouseCode = x.WarehouseCode }).ToList();
            var notExistSkus = await inventoryService.FindNotExistSkuWarehouseAsync(find, masterAccountNum, profileNum);
            if (notExistSkus == null || notExistSkus.Count == 0)
                return false;

            var rtn = false;
            foreach (var item in data.SalesOrderItems)
            {
                if (string.IsNullOrEmpty(item.SKU)) continue;
                if (notExistSkus.FindBySkuWarehouse(item.SKU, item.WarehouseCode) != null)
                {
                    await inventoryService.AddNewProductOrInventoryAsync(new ProductBasic()
                    {
                        DatabaseNum = header.DatabaseNum,
                        MasterAccountNum = header.MasterAccountNum,
                        ProfileNum = header.ProfileNum,
                        SKU = item.SKU,
                    });
                    rtn = true;
                }
            }
            return rtn;
        }


        public async Task<string> GetNextNumberAsync(int masterAccountNum, int profileNum, string customerUuid)
        {
                return await initNumbersService.GetNextNumberAsync(masterAccountNum, profileNum, customerUuid, "so");
        }
        public async Task<bool> UpdateInitNumberForCustomerAsync(int masterAccountNum, int profileNum, string customerUuid, string currentNumber)
        {
                return await initNumbersService.UpdateInitNumberForCustomerAsync(masterAccountNum, profileNum, customerUuid, "so", currentNumber);
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

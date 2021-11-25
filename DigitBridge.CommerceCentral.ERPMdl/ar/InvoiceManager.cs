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
    /// Represents a InvoiceService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InvoiceManager : IInvoiceManager, IMessage
    {

        public InvoiceManager() : base() { }

        public InvoiceManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region services

        [XmlIgnore, JsonIgnore]
        protected InvoiceService _invoiceService;
        [XmlIgnore, JsonIgnore]
        public InvoiceService invoiceService
        {
            get
            {
                if (_invoiceService is null)
                    _invoiceService = new InvoiceService(dbFactory);
                return _invoiceService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InvoiceDataDtoCsv _invoiceDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public InvoiceDataDtoCsv invoiceDataDtoCsv
        {
            get
            {
                if (_invoiceDataDtoCsv is null)
                    _invoiceDataDtoCsv = new InvoiceDataDtoCsv();
                return _invoiceDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InvoiceList _invoiceList;
        [XmlIgnore, JsonIgnore]
        public InvoiceList invoiceList
        {
            get
            {
                if (_invoiceList is null)
                    _invoiceList = new InvoiceList(dbFactory);
                return _invoiceList;
            }
        }

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
        protected OrderShipmentService _orderShipmentService;
        [XmlIgnore, JsonIgnore]
        public OrderShipmentService orderShipmentService
        {
            get
            {
                if (_orderShipmentService is null)
                    _orderShipmentService = new OrderShipmentService(dbFactory);
                return _orderShipmentService;
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

        [XmlIgnore, JsonIgnore]
        protected WarehouseService _warehouseService;
        [XmlIgnore, JsonIgnore]
        public WarehouseService warehouseService
        {
            get
            {
                if (_warehouseService is null)
                    _warehouseService = new WarehouseService(dbFactory);
                return _warehouseService;
            }
        }

        #endregion services

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

        #region import export 
        public async Task<byte[]> ExportAsync(InvoicePayload payload)
        {
            var rowNumList = await invoiceList.GetRowNumListAsync(payload);
            var dtoList = new List<InvoiceDataDto>();
            foreach (var x in rowNumList)
            {
                if (invoiceService.GetData(x))
                    dtoList.Add(invoiceService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceDataDto());
            return invoiceDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InvoicePayload payload)
        {
            var rowNumList = invoiceList.GetRowNumList(payload);
            var dtoList = new List<InvoiceDataDto>();
            foreach (var x in rowNumList)
            {
                if (invoiceService.GetData(x))
                    dtoList.Add(invoiceService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceDataDto());
            return invoiceDataDtoCsv.Export(dtoList);
        }

        public void Import(InvoicePayload payload, IFormFileCollection files)
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
                var list = invoiceDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.Invoice = item;
                    if (invoiceService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in invoiceService.Messages)
                            Messages.Add(msg);
                        invoiceService.Messages.Clear();
                    }
                }
                if (payload.HasInvoice)
                    payload.Invoice = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(InvoicePayload payload, IFormFileCollection files)
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
                var list = invoiceDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.Invoice = item;
                    if (await invoiceService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in invoiceService.Messages)
                            Messages.Add(msg);
                        invoiceService.Messages.Clear();
                    }
                }
                if (payload.HasInvoice)
                    payload.Invoice = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        #endregion import export 

        #region create invoice from orderShimentUuid 

        /// <summary>
        /// Load OrderShipment and SalesOrder, create Invoice for each OrderShipment.
        /// </summary>
        public async Task<string> CreateInvoiceByOrderShipmentIdAsync(string orderShimentUuid)
        {
            //Get OrderShipment by uuid
            var service = orderShipmentService;
            if (!await service.GetDataByIdAsync(orderShimentUuid))
            {
                this.Messages = this.Messages.Add(service.Messages);
                return null;
            }

            var invoiceUuid = await CreateInvoiceFromShipmentAsync(service.Data);
            //if (!string.IsNullOrEmpty(invoiceUuid))
            //{
            //    await service.UpdateProcessStatusAsync(orderShimentUuid, OrderShipmentProcessStatusEnum.InvoiceReady);
            //}
            return invoiceUuid;
        }
        #endregion

        #region create invoice from shipmentdata
        /// <summary>
        /// Load OrderShipment and SalesOrder, create Invoice for each OrderShipment.
        /// </summary>
        protected async Task<bool> ValidateShipmentForInvoiceAsync(OrderShipmentData shipmentData)
        {
            var orderShipmentUuid = shipmentData.OrderShipmentHeader.OrderShipmentUuid;

            // shipment must has SalesOrderUuid
            var salesOrderUuid = shipmentData.OrderShipmentHeader.SalesOrderUuid;
            if (salesOrderUuid.IsZero())
            {
                salesOrderUuid = await salesOrderService.GetSalesOrderUuidByDCAssignmentNumAsync(shipmentData.OrderShipmentHeader.OrderDCAssignmentNum.Value);
                shipmentData.OrderShipmentHeader.SalesOrderUuid = salesOrderUuid;
            }
            if (salesOrderUuid.IsZero())
            {
                AddError($"SalesOrder not found for OrderShipment. OrderShipmentUuid:{orderShipmentUuid}");
                return false;
            }

            // shipment exist invoiceUuid
            if (!string.IsNullOrEmpty(shipmentData.OrderShipmentHeader?.InvoiceUuid))
            {
                if (await invoiceService.ExistInvoiceUuidAsync(
                    shipmentData.OrderShipmentHeader?.InvoiceUuid,
                    (int)shipmentData.OrderShipmentHeader?.MasterAccountNum,
                    (int)shipmentData.OrderShipmentHeader?.ProfileNum)
                )
                {
                    AddError($"Shipment has been transferred to invoice.");
                    return false;
                }
            } else if (!string.IsNullOrEmpty(await invoiceService.GetInvoiceUuidByOrderShipmentUuidAsync(orderShipmentUuid)))
            {
                AddError($"Shipment has been transferred to invoice.");
                return false;
            }

            //if (await ExistSalesOrderInInvoiceAsync(salesOrderUuid))
            //{
            //    AddError($"SalesOrderUuid {salesOrderUuid} has been transferred to invoice. OrderShipmentUuid:{orderShipmentUuid}");
            //    return (true, "");
            //}

            return true;
        }
        /// <summary>
            /// Load OrderShipment and SalesOrder, create Invoice for each OrderShipment.
            /// </summary>
        public async Task<string> CreateInvoiceFromShipmentAsync(OrderShipmentData shipmentData)
        {
            if (!(await ValidateShipmentForInvoiceAsync(shipmentData)))
                return null;

            // load sales order data           
            if (!(await salesOrderService.ListAsync(shipmentData.OrderShipmentHeader.SalesOrderUuid)))
            {
                this.Messages.Add(salesOrderService.Messages);
                return null;
            }
            var salesOrderData = salesOrderService.Data;
            salesOrderService.DetachData(null);

            // Create Invoice from shipment and sales order
            var invoiceUuid = await CreateInvoiceAsync(shipmentData, salesOrderData);
            if (string.IsNullOrEmpty(invoiceUuid))
                return null;

            // if sales order include deposit, create invoice payment for deposit
            var soHeader = salesOrderData.SalesOrderHeader;
            if (soHeader.DepositAmount.IsZero() || soHeader.MiscInvoiceUuid.IsZero())
                return invoiceUuid;

            var paymentService = new InvoicePaymentService(dbFactory);
            if (!(await paymentService.AddAsync(invoiceUuid, soHeader.DepositAmount, soHeader.MiscInvoiceUuid)))
                this.Messages.Add(paymentService.Messages);

            return invoiceUuid;
        }

        /// <summary>
        /// Create one invoice from one orderShipment and one salesOrder.
        /// </summary>
        protected async Task<string> CreateInvoiceAsync(OrderShipmentData shipmentData, SalesOrderData salesOrderData)
        {
            InvoiceTransfer invoiceTransfer = new InvoiceTransfer(this, "");
            invoiceService.Add();
            invoiceService.NewData();

            var invoiceData = invoiceTransfer.FromOrderShipmentAndSalesOrder(shipmentData, salesOrderData, invoiceService.Data);
            await SetWarehouse(invoiceService.Data);

            //var mapper = new InvoiceDataDtoMapperDefault();
            //var dto = mapper.WriteDto(invoiceData, null);
            //var success = await invoiceService.SaveDataAsync();
            if (!(await invoiceService.SaveDataAsync()))
            {
                this.Messages.Add(invoiceService.Messages);
                return null;
            }

            //set InvoiceNumber back to shipment and update shipment to ready.
            shipmentData.OrderShipmentHeader.InvoiceNumber = invoiceService.Data.InvoiceHeader.InvoiceNumber;
            await orderShipmentService.UpdateProcessStatusAsync(
                shipmentData.OrderShipmentHeader.OrderShipmentUuid, 
                OrderShipmentProcessStatusEnum.InvoiceReady,
                invoiceService.Data.InvoiceHeader.InvoiceUuid,
                invoiceService.Data.InvoiceHeader.InvoiceNumber
            );
            return invoiceService.Data.InvoiceHeader.InvoiceUuid;
        }

        protected async Task<bool> SetWarehouse(InvoiceData data)
        {
            if (data == null) return false;
            var header = data.InvoiceHeader;
            var info = data.InvoiceHeaderInfo;
            var whs = await warehouseService.GetWarehouseDataByWarehouseCodeAsync(info.WarehouseCode, header.MasterAccountNum, header.ProfileNum);
            if (whs == null)
                return false;
            foreach (var item in data.InvoiceItems)
            {
                if (item == null || item.IsEmpty) continue;
                item.WarehouseCode = info.WarehouseCode;
                item.WarehouseUuid = whs.DistributionCenter.DistributionCenterUuid;
            }
            return true;
        }

        

        public async Task<string> GetNextNumberAsync(int masterAccountNum, int profileNum, string customerUuid)
        {
            return await initNumbersService.GetNextNumberAsync(masterAccountNum, profileNum, customerUuid, "invoice");
        }

        public async Task<bool> UpdateInitNumberForCustomerAsync(int masterAccountNum, int profileNum, string customerUuid, string currentNumber)
        {
            return await initNumbersService.UpdateInitNumberForCustomerAsync(masterAccountNum, profileNum, customerUuid, "invoice", currentNumber);
        }
        #endregion

        #region update fault invoice 

        /// <summary>
        /// Update fault invoice back to event process.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="faultEventProcess"></param>
        /// <returns></returns>
        public async Task<IList<FaultInvoiceResponsePayload>> UpdateFaultInvoicesAsync(InvoicePayload payload, IList<FaultInvoiceRequestPayload> faultInvoiceList)
        {
            var responseList = new List<FaultInvoiceResponsePayload>();
            var srv = new EventProcessERPService(dbFactory);
            foreach (var faultInvoice in faultInvoiceList)
            {
                srv.Messages = new List<MessageClass>();
                var eventDto = new EventProcessERPDataDto()
                {
                    EventProcessERP = new EventProcessERPDto()
                    {
                        ERPEventProcessType = (int)EventProcessTypeEnum.InvoiceToCommerceCentral,
                        ProcessStatus = (int)EventProcessProcessStatusEnum.Failed,
                        MasterAccountNum = payload.MasterAccountNum,
                        ProfileNum = payload.ProfileNum,
                        EventUuid = faultInvoice.EventUuid,
                        EventMessage = faultInvoice.Message.ToString(),
                    }
                };
                var success = await srv.UpdateAsync(eventDto);

                var response = new FaultInvoiceResponsePayload()
                {
                    EventUuid = faultInvoice.EventUuid,
                    Messages = srv.Messages,
                    Success = success,
                };
                responseList.Add(response);
            }
            return responseList;
        }
        #endregion
    }
}

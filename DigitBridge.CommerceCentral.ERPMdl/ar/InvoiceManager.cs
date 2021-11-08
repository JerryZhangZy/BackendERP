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


        #region create invoice from orderShimentUuid 

        /// <summary>
        /// Load OrderShipment and SalesOrder, create Invoice for each OrderShipment.
        /// </summary>
        /// <param name="centralOrderUuid"></param>
        /// <returns>Success Create Invoice, Invoice UUID</returns>
        public async Task<(bool, string)> CreateInvoiceByOrderShipmentIdAsync(string orderShimentUuid)
        {
            //Get OrderShipment by uuid
            var service = new OrderShipmentService(dbFactory);
            if (!await service.GetDataByIdAsync(orderShimentUuid))
            {
                this.Messages = this.Messages.Concat(service.Messages).ToList();
                return (false, "");
            }

            return await CreateInvoiceFromShipmentAsync(service.Data);
        }
        #endregion

        #region create invoice from shipmentdata
        /// <summary>
        /// Load OrderShipment and SalesOrder, create Invoice for each OrderShipment.
        /// </summary>
        /// <param name="centralOrderUuid"></param>
        /// <returns>Success Create Invoice, Invoice UUID</returns>
        public async Task<(bool, string)> CreateInvoiceFromShipmentAsync(OrderShipmentData shipmentData)
        {
            var mainTrackingNumber = shipmentData.OrderShipmentHeader.MainTrackingNumber;

            var orderDCAssignmentNum = shipmentData.OrderShipmentHeader.OrderDCAssignmentNum;
            if (orderDCAssignmentNum.IsZero())
            {
                AddError($"OrderDCAssignmentNum cannot be empty.");
                return (false, null);
            }
            //Get Sale by uuid
            var salesOrderUuid = await GetSalesOrderUuidAsync(orderDCAssignmentNum.Value);

            if (string.IsNullOrEmpty(salesOrderUuid))
            {
                AddError($"SalesOrder not found for OrderShipment.");
                return (false, null);
            }

            if (await ExistSalesOrderInInvoiceAsync(salesOrderUuid))
            {
                AddError($"SalesOrderUuid {salesOrderUuid} has been transferred to invoice.");
                return (false, null);
            }

            var salesorderService = new SalesOrderService(dbFactory);
            var success = await salesorderService.GetDataByIdAsync(salesOrderUuid);
            if (!success)
            {
                this.Messages = this.Messages.Concat(salesorderService.Messages).ToList();
                return (false, null);
            }

            string invoiceUuid;
            //Create Invoice 
            (success, invoiceUuid) = await CreateInvoiceAsync(shipmentData, salesorderService.Data);
            if (!success)
                return (false, null);

            var soHeader = salesorderService.Data.SalesOrderHeader;

            var paymentManager = new InvoicePaymentManager(dbFactory);
            success = await paymentManager.AddPaymentFromPrepayment(soHeader.MiscInvoiceUuid, invoiceUuid, soHeader.DepositAmount);

            return (success, invoiceUuid);
        }

        /// <summary>
        /// Get SalesOrderData by OrderDCAssignmentNum
        /// </summary>
        /// <param name="orderDCAssignmentNum"></param>
        /// <returns>SalesOrderData</returns>
        protected async Task<string> GetSalesOrderUuidAsync(long orderDCAssignmentNum)
        {
            //Get SalesOrderData by uuid
            using (var trs = new ScopedTransaction(dbFactory))
                return await SalesOrderHelper.GetSalesOrderUuidAsync(orderDCAssignmentNum);
        }
        /// <summary>
        /// Check SalesOrder is already exist in Invoice
        /// </summary>
        /// <param name="salesOrderUuid"></param>
        /// <returns>Exist or Not</returns>
        protected async Task<bool> ExistSalesOrderInInvoiceAsync(string salesOrderUuid)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceHelper.ExistSalesOrderUuidAsync(salesOrderUuid);
        }

        /// <summary>
        /// Create one invoice from one orderShipment and one salesOrder.
        /// </summary>
        /// <param name="coData"></param>
        /// <param name="dcAssigmentData"></param>
        /// <returns>Success Create Invoice</returns>
        protected async Task<(bool, string)> CreateInvoiceAsync(OrderShipmentData shipmentData, SalesOrderData salesOrderData)
        {
            InvoiceTransfer invoiceTransfer = new InvoiceTransfer(this, "");
            var invoiceData = invoiceTransfer.FromOrderShipmentAndSalesOrder(shipmentData, salesOrderData);
            var invoiceService = new InvoiceService(dbFactory);

            var mapper = new InvoiceDataDtoMapperDefault();
            var dto = mapper.WriteDto(invoiceData, null);
            var success = await invoiceService.AddAsync(dto);
            if (!success)
            {
                this.Messages = this.Messages.Concat(invoiceService.Messages).ToList();
                return (false, null);
            }

            //set InvoiceNumber back to shipment.
            shipmentData.OrderShipmentHeader.InvoiceNumber = invoiceService.Data.InvoiceHeader.InvoiceNumber;
            return (true, invoiceService.Data.InvoiceHeader.InvoiceUuid);
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
                var eventDto = new EventProcessERPDataDto()
                {
                    EventProcessERP = new EventProcessERPDto()
                    {
                        ERPEventProcessType = (int)EventProcessTypeEnum.InvoiceToChanel,
                        ActionStatus = (int)EventProcessActionStatusEnum.Failed,
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

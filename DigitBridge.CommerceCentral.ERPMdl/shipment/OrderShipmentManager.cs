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
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a OrderShipmentService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class OrderShipmentManager : IOrderShipmentManager, IMessage
    {

        public OrderShipmentManager() : base() { }

        public OrderShipmentManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }

        #region services
        [XmlIgnore, JsonIgnore]
        protected EventProcessERPService _eventProcessERPService;
        [XmlIgnore, JsonIgnore]
        public EventProcessERPService eventProcessERPService
        {
            get
            {
                if (_eventProcessERPService is null)
                    _eventProcessERPService = new EventProcessERPService(dbFactory);
                return _eventProcessERPService;
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
        protected OrderShipmentDataDtoCsv _orderShipmentDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public OrderShipmentDataDtoCsv orderShipmentDataDtoCsv
        {
            get
            {
                if (_orderShipmentDataDtoCsv is null)
                    _orderShipmentDataDtoCsv = new OrderShipmentDataDtoCsv();
                return _orderShipmentDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected OrderShipmentList _orderShipmentList;
        [XmlIgnore, JsonIgnore]
        public OrderShipmentList orderShipmentList
        {
            get
            {
                if (_orderShipmentList is null)
                    _orderShipmentList = new OrderShipmentList(dbFactory);
                return _orderShipmentList;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InvoiceManager _invoiceManager;
        [XmlIgnore, JsonIgnore]
        public InvoiceManager invoiceManager
        {
            get
            {
                if (_invoiceManager is null)
                    _invoiceManager = new InvoiceManager(dbFactory);
                return _invoiceManager;
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

        public async Task<byte[]> ExportAsync(OrderShipmentPayload payload)
        {
            var rowNumList = await orderShipmentList.GetRowNumListAsync(payload);
            var dtoList = new List<OrderShipmentDataDto>();
            foreach (var x in rowNumList)
            {
                if (orderShipmentService.GetData(x))
                    dtoList.Add(orderShipmentService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new OrderShipmentDataDto());
            return orderShipmentDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(OrderShipmentPayload payload)
        {
            var rowNumList = orderShipmentList.GetRowNumList(payload);
            var dtoList = new List<OrderShipmentDataDto>();
            foreach (var x in rowNumList)
            {
                if (orderShipmentService.GetData(x))
                    dtoList.Add(orderShipmentService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new OrderShipmentDataDto());
            return orderShipmentDataDtoCsv.Export(dtoList);
        }

        public void Import(OrderShipmentPayload payload, IFormFileCollection files)
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
                var list = orderShipmentDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.OrderShipment = item;
                    if (orderShipmentService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in orderShipmentService.Messages)
                            Messages.Add(msg);
                        orderShipmentService.Messages.Clear();
                    }
                }
                if (payload.HasOrderShipment)
                    payload.OrderShipment = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(OrderShipmentPayload payload, IFormFileCollection files)
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
                var list = orderShipmentDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.OrderShipment = item;
                    if (await orderShipmentService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in orderShipmentService.Messages)
                            Messages.Add(msg);
                        orderShipmentService.Messages.Clear();
                    }
                }
                if (payload.HasOrderShipment)
                    payload.OrderShipment = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        #endregion import export 

        #region Create shipment from API

        /// <summary>
        /// Create multiple shipment and invoice
        /// </summary>
        public async Task<List<OrderShipmentCreateResultPayload>> CreateShipmentListAsync(OrderShipmentPayload payload, IList<InputOrderShipmentType> wmsShipments)
        {
            var resultList = new List<OrderShipmentCreateResultPayload>();
            if (wmsShipments is null || wmsShipments.Count == 0)
            {
                resultList.Add(new OrderShipmentCreateResultPayload()
                {
                    Messages = this.AddError("shipment data is required."),
                });
                return resultList;
            }

            // loop for shipment list to create each shipment
            foreach (var wmsShipment in wmsShipments)
            {
                // for each shipment, create result object to hold shipment creating result
                var result = new OrderShipmentCreateResultPayload()
                {
                    Success = true,
                    MasterAccountNum = payload.MasterAccountNum,
                    ProfileNum = payload.ProfileNum,
                    ShipmentID = wmsShipment?.ShipmentHeader?.ShipmentID,
                };

                // first validate shipment data, then sent it to eventprocess and queue.
                if (await ValidateShipment(wmsShipment, result))
                {
                    var eventProcessERP = new EventProcessERP()
                    {
                        MasterAccountNum = result.MasterAccountNum,
                        ProfileNum = result.ProfileNum,
                        ProcessData = wmsShipment.ObjectToString(),
                        ProcessUuid = result.ShipmentID,
                        ERPEventProcessType = (int)EventProcessTypeEnum.ShipmentFromWMS,
                        ActionStatus = (int)EventProcessActionStatusEnum.Pending,
                    };
                    if (!await eventProcessERPService.AddEventAndQueueMessageAsync(eventProcessERP))
                    {
                        result.Success = false;
                        result.Messages.Add(eventProcessERPService.Messages);
                    }
                }

                resultList.Add(result);
            }
            return resultList;
        }

        ///// <summary>
        ///// Create multiple shipment and invoice
        ///// </summary>
        //public async Task<List<OrderShipmentCreateResultPayload>> CreateShipmentListAsync(OrderShipmentPayload payload, IList<InputOrderShipmentType> wmsShipments)
        //{
        //    var resultList = new List<OrderShipmentCreateResultPayload>();
        //    if (wmsShipments is null || wmsShipments.Count == 0)
        //    {
        //        resultList.Add(new OrderShipmentCreateResultPayload()
        //        {
        //            Messages = this.AddError("shipment data is required."),
        //        });
        //        return resultList;
        //    }

        //    // loop for shipment list to create each shipment
        //    foreach (var shipment in wmsShipments)
        //    {
        //        // for each shipment, create result object to hold shipment creating result
        //        var result = new OrderShipmentCreateResultPayload()
        //        {
        //            Success = true,
        //            MasterAccountNum = payload.MasterAccountNum,
        //            ProfileNum = payload.ProfileNum,
        //            ShipmentID = shipment?.ShipmentHeader?.ShipmentID,
        //        };

        //        // first validate shipment data, allow to create new shipment
        //        await CreateShipmentAsync(shipment, result);

        //        resultList.Add(result);
        //    }
        //    return resultList;
        //}

        protected async Task<bool> ValidateShipment(List<InputOrderShipmentType> wmsShipments, OrderShipmentCreateResultPayload result)
        {
            result.Success = true;
            foreach (var wmsShipment in wmsShipments)
            { }


            return result.Success;
        }

        /// <summary>
        /// Validate current shipment data 
        /// </summary>
        protected async Task<bool> ValidateShipment(InputOrderShipmentType wmsShipment, OrderShipmentCreateResultPayload result)
        {
            //if (payload is null)
            //{
            //    AddError("Request is invalid.");
            //    return response;
            //}
            result.Success = true;
            if (wmsShipment is null)
            {
                result.Messages.AddError("Shipment data cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.ShipmentHeader is null)
            {
                result.Messages.AddError("ShipmentHeader data cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.ShipmentHeader.SalesOrderUuid.IsZero())
            {
                result.Messages.AddError("SalesOrderUuid cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.ShipmentHeader.WarehouseCode.IsZero())
            {
                result.Messages.AddError("WarehouseCode cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.CanceledItems?.Where(i => i.SalesOrderItemsUuid.IsZero()).Count() > 0)
            {
                result.Messages.AddError("SalesOrderItemsUuid of CanceledItem cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.PackageItems?.SelectMany(i => i.ShippedItems.Where(j => j.SalesOrderItemsUuid.IsZero())).Count() > 0)
            {
                result.Messages.AddError("SalesOrderItemsUuid of ShippedItem cannot be empty.");
                result.Success = false;
            }

            if (wmsShipment.ShipmentHeader.ShipmentID.IsZero())
            {
                result.Messages.AddError("ShipmentID cannot be empty.");
                result.Success = false;
            }

            return result.Success;
        }

        /// <summary>
        /// Create and save one shipment, but set processStatus to -1 (pending)
        /// </summary>
        public async Task<bool> CreateShipmentAsync(InputOrderShipmentType wmsShipment, OrderShipmentCreateResultPayload result)
        {
            // create mapper, and transfer shipment payload ro ero shipment Dto
            var mapper = new WMSOrderShipmentMapper(result.MasterAccountNum, result.ProfileNum);
            var erpShipment = mapper.MapperToErpShipment(wmsShipment);
            // set shipment status to pending, this will not send to marketplace API
            erpShipment.OrderShipmentHeader.ProcessStatus = (int)OrderShipmentProcessStatusEnum.Pending;

            // load other info from salesOrder data
            if (!(await LoadSalesOrderToShipmentAsync(erpShipment, result)))
                return false;

            // create orderShipmentService and save new shipment
            var service = orderShipmentService;
            service.DetachData(null);
            if (!(await service.AddAsync(erpShipment)))
            {
                result.Messages.Add(service.Messages);
                result.Success = false;
                return result.Success;
            }

            // if shipment succes add, transfer shipment to invoice
            await CreateInvoiceAsync(orderShipmentService.Data, result);
            return result.Success;
        }
        protected async Task<bool> ShipmentIDExistAsync(string shipmentID, OrderShipmentCreateResultPayload result)
        {
            if (await orderShipmentService.ExistShipmentIDAsync(result.MasterAccountNum, result.ProfileNum, shipmentID))
            {
                result.Success = true;
            }
            return false;
        }
        protected async Task<bool> LoadSalesOrderToShipmentAsync(OrderShipmentDataDto erpShipment, OrderShipmentCreateResultPayload result)
        {
            // if shipment payload not include SalesOrderUuid, try to load SalesOrderUuid by OrderDCAssignmentNum
            if (string.IsNullOrEmpty(erpShipment.OrderShipmentHeader.SalesOrderUuid))
                erpShipment.OrderShipmentHeader.SalesOrderUuid =
                    await salesOrderService.GetSalesOrderUuidByDCAssignmentNumAsync(erpShipment.OrderShipmentHeader.OrderDCAssignmentNum.Value);

            //// load OrderNumber from salesOrder Uuid
            //if (string.IsNullOrEmpty(erpShipment.OrderShipmentHeader.SalesOrderUuid))
            //    erpShipment.OrderShipmentHeader.OrderNumber =
            //        await salesOrderService.GetSalesOrderNumberByUuidAsync(erpShipment.OrderShipmentHeader.SalesOrderUuid);

            if (string.IsNullOrEmpty(erpShipment.OrderShipmentHeader.SalesOrderUuid))
            {
                return false;
            }

            // load sales order data           
            if (!(await salesOrderService.ListAsync(erpShipment.OrderShipmentHeader.SalesOrderUuid)))
            {
                result.Messages.Add(salesOrderService.Messages);
                return false;
            }
            var salesOrderData = salesOrderService.Data;

            if (salesOrderData.SalesOrderHeaderInfo?.WarehouseCode != erpShipment.OrderShipmentHeader?.WarehouseCode)
            {
                result.Messages.AddError("WarehouseCode error.");
                return false;
            }

            salesOrderService.DetachData(null);

            // create mapper, and transfer shipment payload ro ero shipment Dto
            var mapper = new ShipmentTransfer(this, dbFactory, "");
            await mapper.LoadOthersDataFromSalesOrder(salesOrderData, erpShipment);
            return true;
        }


        /// <summary>
        /// After save one shipment, create invoice from this shipment and set processStatus to 0 (allow send to marketplace)
        /// </summary>
        protected async Task<bool> CreateInvoiceAsync(OrderShipmentData orderShipmentData, OrderShipmentCreateResultPayload result)
        {
            // create invoice and set invoicenumber back to shipmentdata. 
            var invoiceUuid = await invoiceManager.CreateInvoiceFromShipmentAsync(orderShipmentData);
            if (string.IsNullOrEmpty(invoiceUuid))
            {
                this.Messages.Add(invoiceManager.Messages);
                result.Success = false;
                return result.Success;
            }

            //// change orderShipment status to 0 (InvoiceReady, allow to upload)
            //await orderShipmentService.UpdateProcessStatusAsync(orderShipmentData.OrderShipmentHeader.OrderShipmentUuid, OrderShipmentProcessStatusEnum.InvoiceReady);
            return result.Success;
        }

        #endregion

        #region Create shipment from Sales order

        /// <summary>
        /// Validate current shipment data 
        /// </summary>
        protected async Task<bool> ValidateSalesOrderForShipmentAsync(SalesOrderData salesOrderData)
        {
            if (salesOrderData is null)
            {
                this.Messages.AddError("Sales order data cannot be empty.");
                return false;
            }

            if (!string.IsNullOrEmpty(
                await orderShipmentService.GetOrderShipmentUuidBySalesOrderUuidOrDCAssignmentNumAsync(
                    salesOrderData.SalesOrderHeader.SalesOrderUuid,
                    salesOrderData.SalesOrderHeader.OrderSourceCode
                )))
            {
                this.Messages.AddError("Sales Order has been transferred to shipment.");
                return false;
            }

            return true;
        }

        public async Task<string> CreateShipmentFromSalesOrderAsync(string salesOrderUuid)
        {
            if (string.IsNullOrEmpty(salesOrderUuid))
                return null;

            // load sales order data           
            if (!(await salesOrderService.ListAsync(salesOrderUuid)))
            {
                this.Messages.Add(salesOrderService.Messages);
                return null;
            }
            var salesOrderData = salesOrderService.Data;
            salesOrderService.DetachData(null);

            if (!(await ValidateSalesOrderForShipmentAsync(salesOrderData)))
                return null;

            // Create Invoice from shipment and sales order
            return await CreateShipmentFromSalesOrderAsync(salesOrderData);
        }


        /// <summary>
        /// Create and save one shipment, but set processStatus to -1 (pending)
        /// </summary>
        protected async Task<string> CreateShipmentFromSalesOrderAsync(SalesOrderData salesOrderData)
        {
            // create orderShipmentService and save new shipment
            var service = orderShipmentService;
            service.Add();
            service.NewData();

            // create mapper, and transfer shipment payload ro ero shipment Dto
            var mapper = new ShipmentTransfer(this, dbFactory, "");
            if (!(await mapper.FromSalesOrder(salesOrderData, service)))
                return null;

            if (!(await service.SaveDataAsync()))
            {
                this.Messages.Add(service.Messages);
                return null;
            }
            return service.Data.OrderShipmentHeader.OrderShipmentUuid;
        }

        public async Task<OrderShipmentData> CreateShipmentDataFromSalesOrderAsync(string salesOrderUuid)
        {
            if (string.IsNullOrEmpty(salesOrderUuid))
                return null;

            // load sales order data           
            if (!(await salesOrderService.ListAsync(salesOrderUuid)))
            {
                this.Messages.Add(salesOrderService.Messages);
                return null;
            }
            var salesOrderData = salesOrderService.Data;
            salesOrderService.DetachData(null);

            // Create Invoice from shipment and sales order
            return await CreateShipmentDataFromSalesOrderAsync(salesOrderData);
        }

        /// <summary>
        /// Create and save one shipment, but set processStatus to -1 (pending)
        /// </summary>
        protected async Task<OrderShipmentData> CreateShipmentDataFromSalesOrderAsync(SalesOrderData salesOrderData)
        {
            // create orderShipmentService and save new shipment
            var service = orderShipmentService;
            service.Add();
            service.NewData();

            // create mapper, and transfer shipment payload ro ero shipment Dto
            var mapper = new ShipmentTransfer(this, dbFactory, "");
            if (!(await mapper.FromSalesOrder(salesOrderData, service)))
                return null;

            return service.Data;
        }

        #endregion


        #region create shipment for wms trigger by queue

        /// <summary>
        /// create shipment by wmsShipmentID
        /// </summary>
        /// <param name="wmsShipment"></param>
        /// <param name="wmsShipmentID"></param>
        /// <returns></returns>
        public async Task<bool> CreateShipmentAsync(OrderShipmentPayload payload, string shipmentID)
        {
            var result = new OrderShipmentCreateResultPayload()
            {
                MasterAccountNum = payload.MasterAccountNum,
                ProfileNum = payload.ProfileNum,
                ShipmentID = shipmentID,
            };

            var success = true;

            var wmsShipment = await GetWmsShipment(result);
            if (wmsShipment == null)
            {
                result.Messages.AddError($"Data not found,ShipmentID:{result.ShipmentID}");
                success = false;
            }


            success = success && await ValidateShipment(wmsShipment, result);

            // validate is true and shipmentid not exist then create erp shipment.
            if (success && !await ShipmentIDExistAsync(wmsShipment.ShipmentHeader.ShipmentID, result))
                success = success && await CreateShipmentAsync(wmsShipment, result);


            result.Success = success;

            // update all result back to event process.
            return await UpdateProcessResultAsync(result);
        }

        /// <summary>
        /// update process result back to  event process 
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> UpdateProcessResultAsync(OrderShipmentCreateResultPayload result)
        {
            var processResult = new ProcessResult()
            {
                EventMessage = new JObject() { { "message", JArray.FromObject(result.Messages) } },
                ProcessUuid = result.ShipmentID,
                ProcessStatus = result.Success ? (int)EventProcessProcessStatusEnum.Success : (int)EventProcessProcessStatusEnum.Failed
            };

            var ackPayload = new AcknowledgeProcessPayload()
            {
                MasterAccountNum = result.MasterAccountNum,
                ProfileNum = result.ProfileNum,
                EventProcessType = EventProcessTypeEnum.ShipmentFromWMS,
                ProcessResults = new List<ProcessResult>()
                { processResult }
            };

            return await eventProcessERPService.UpdateProcessStatusAsync(ackPayload);
        }

        protected async Task<InputOrderShipmentType> GetWmsShipment(OrderShipmentCreateResultPayload result)
        {
            eventProcessERPService.List();
            var success = await eventProcessERPService.GetByProcessUuidAsync((int)EventProcessTypeEnum.ShipmentFromWMS, result.ShipmentID);
            if (!success)
            {
                result.Messages.Add(eventProcessERPService.Messages);
            }
            var wmsShipment = (eventProcessERPService.Data?.EventProcessERP?.ProcessData).StringToObject<InputOrderShipmentType>();
            return wmsShipment;
        }

        #endregion
    }
}

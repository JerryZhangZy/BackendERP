

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

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a default SalesOrderService Calculator class.
    /// </summary>
    public partial class SalesOrderServiceCalculatorDefault : ICalculator<SalesOrderData>, IMessage
    {
        protected ISalesOrderService _salesOrderService;
        public SalesOrderServiceCalculatorDefault(ISalesOrderService  salesOrderService, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = (IMessage)salesOrderService;
            this._salesOrderService = salesOrderService;
            this.dbFactory = dbFactory;
        }
        public SalesOrderServiceCalculatorDefault(IDataBaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected IDataBaseFactory dbFactory { get; set; }
        protected ISalesOrderService service { get; set; }

        //

        public virtual void PrepareData(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data == null || data.SalesOrderHeader == null)
                return;
        }

        private DateTime now = DateTime.UtcNow;

        #region Service Property

        private CustomerService _customerService;

        protected CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
            }
        }

        private InventoryService _inventoryService;

        protected InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion

        #region GetDataWithCache
        /// <summary>
        /// get inventory data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public virtual InventoryData GetInventoryData(SalesOrderData data, string sku)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + sku;
            return data.GetCache(key, () =>
            {
                if (inventoryService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, sku))
                    return inventoryService.Data;
                return null;
            });
        }
        /// <summary>
        /// get inventory data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public virtual InventoryData GetInventoryData_InventoryUuid(SalesOrderData data, string inventoryUuid)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + inventoryUuid;
            return data.GetCache(key, () =>
            {
                if (inventoryService.GetDataById(inventoryUuid))
                    return inventoryService.Data;
                return null;
            });
        }
        /// <summary>
        /// Get Customer Data by customerCode
        /// </summary>
        /// <param name="data"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public virtual CustomerData GetCustomerData(SalesOrderData data, string customerCode)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + customerCode;
            return data.GetCache(key, () =>
            {
                if (customerService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, customerCode))
                    return customerService.Data;
                return null;
            });
        }
        #endregion

        public virtual bool SetDefault(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add set default summary data logic
            //This is generated sample code 
            var sum = data.SalesOrderHeader;
            if (sum.OrderTime.IsZero()) sum.OrderTime = now.TimeOfDay;
            if (sum.OrderDate.IsZero())
            {
                sum.OrderDate = now.Date;
                sum.OrderTime = now.TimeOfDay;
            } 
            //EnterBy
            //UpdateBy
            if (processingMode == ProcessingMode.Add)
            {
                if (string.IsNullOrEmpty(sum.OrderNumber) ||
                    _salesOrderService.ExistOrderNumber(sum.OrderNumber, sum.MasterAccountNum, sum.ProfileNum)
                    )
                {
                    sum.OrderNumber = _salesOrderService.GetNextNumber(data.SalesOrderHeader.MasterAccountNum,data.SalesOrderHeader.ProfileNum);
                }
                //for Add mode, always reset data's uuid
                sum.SalesOrderUuid = Guid.NewGuid().ToString();
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                //todo 
            }

            // get customer data
            var customerData = GetCustomerData(data, sum.CustomerCode);
            if (customerData != null && customerData.Customer != null)
            {
                sum.CustomerUuid = customerData.Customer.CustomerUuid;
                sum.CustomerName = customerData.Customer.CustomerName;
                //if (string.IsNullOrEmpty(sum.Currency))
                //    sum.Currency = customerData.Customer.Currency;
            }
            // (int)( (SalesOrderStatus) sum.OrderStatus)
            sum.DueDate = sum.OrderDate.AddDays(sum.TermsDays);
             
            return true;
        }

        public virtual bool SetDefaultDetail(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null || data.SalesOrderItems == null || data.SalesOrderItems.Count == 0)
                return false;

            //TODO: add set default for detail list logic
            // This is generated sample code 
            foreach (var item in data.SalesOrderItems)
            {
                SetDefault(item, data, processingMode);
            }
            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool SetDefault(SalesOrderItems item, SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        { 
            if (item.ItemTime.IsZero()) item.ItemTime = now.TimeOfDay;
            if (item.ItemDate.IsZero())
            {
                item.ItemDate = now.Date;
                item.ItemTime = now.TimeOfDay;
            } 
            if (processingMode == ProcessingMode.Add)
            {
                item.SalesOrderItemsUuid = Guid.NewGuid().ToString();
            }

            //Set SKU info
            var inventoryData = GetInventoryData(data, item.SKU);
            // currency priority: user input > customer > Sku
            //if (string.IsNullOrEmpty(item.Currency))
            //    item.Currency = data.SalesOrderHeader.Currency;
            if (inventoryData == null)
                return false;

            item.ProductUuid = inventoryData.ProductBasic.ProductUuid;
            item.UOM = inventoryData.ProductExt?.UOM;

            var inventory = inventoryData.FindInventoryByWarhouse(item.WarehouseCode);
            if (inventory == null)
                return false;

            if (!string.IsNullOrEmpty(inventory.InventoryUuid)) item.InventoryUuid = inventory.InventoryUuid;
            if (!string.IsNullOrEmpty(inventory.WarehouseCode)) item.WarehouseCode = inventory.WarehouseCode;
            if (!string.IsNullOrEmpty(inventory.WarehouseUuid)) item.WarehouseUuid = inventory.WarehouseUuid;

            if (!string.IsNullOrEmpty(inventory.LotNum)) item.LotNum = inventory.LotNum;
            //if (string.IsNullOrEmpty(item.Currency))
            //    item.Currency = inventory.Currency;

            //var setting = new ERPSetting();
            //var sum = data.SalesOrderHeader;
            ////var prod = data.GetCache<ProductBasic>(ProductId);
            ////var inv = data.GetCache<Inventory>(InventoryId);
            ////var invCost = new ItemCostClass(inv);
            //var invCost = new ItemCostClass();

            ////InvoiceItemType
            ////InvoiceItemStatus 
            ////ShipDate
            ////EtaArrivalDate

            ////SKU 
            ////Description
            ////Notes  

            return true;
        }

        public virtual bool Calculate(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);

            return true;
        }
        public virtual bool CalculateSummary(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            var sum = data.SalesOrderHeader;
            sum.ShippingAmount = sum.ShippingAmount.ToAmount();
            sum.MiscAmount = sum.MiscAmount.ToAmount();
            sum.ChargeAndAllowanceAmount = sum.ChargeAndAllowanceAmount.ToAmount();

            // We support both discount rate and discount amount
            // if exist discount rate, it will apply to unit price and get after discount price
            sum.DiscountAmount = sum.DiscountAmount.ToAmount();
            var discountRateAmount = (decimal)0;
            // if exist DiscountRate, calculate discount amount
            if (!sum.DiscountRate.IsZero())
                discountRateAmount = (sum.SubTotalAmount * sum.DiscountRate.ToRate()).ToAmount();
            var totalDiscountAmount = discountRateAmount + sum.DiscountAmount;

            //manual input max discount amount is SubTotalAmount
            // tax calculate should deduct discount from taxable amount
            var discountRate = sum.SubTotalAmount != 0 ? (totalDiscountAmount / sum.SubTotalAmount) : 0;
            sum.TaxAmount = ((sum.TaxableAmount * (1 - discountRate)) * sum.TaxRate).ToAmount();

            var setting = new ERPSetting();
            if (setting.TaxForShippingAndHandling)
            {
                sum.ShippingTaxAmount = (sum.ShippingAmount * sum.TaxRate).ToAmount();
                sum.MiscTaxAmount = (sum.MiscAmount * sum.TaxRate).ToAmount();
                sum.TaxAmount += (sum.ShippingTaxAmount + sum.MiscTaxAmount);
            }

            sum.SalesAmount = (sum.SubTotalAmount - totalDiscountAmount).ToAmount();
            sum.TotalAmount = (
                (sum.SubTotalAmount - totalDiscountAmount) +
                sum.TaxAmount +
                sum.ShippingAmount +
                sum.MiscAmount
                //sum.ChargeAndAllowanceAmount
                ).ToAmount();

            sum.Balance = (sum.TotalAmount - sum.PaidAmount - sum.CreditAmount).ToAmount();

            return true;
        }


        public virtual bool CalculateDetail(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;
            //This is generated sample code

            var sum = data.SalesOrderHeader;
            sum.SubTotalAmount = 0;
            sum.SalesAmount = 0;
            sum.TotalAmount = 0;
            sum.TaxableAmount = 0;
            sum.NonTaxableAmount = 0;
            sum.TaxAmount = 0;
            sum.Balance = 0;
            sum.UnitCost = 0;
            sum.AvgCost = 0;
            sum.LotCost = 0;
            sum.TaxRate = sum.TaxRate.ToRate();
            sum.ChargeAndAllowanceAmount = 0;

            foreach (var item in data.SalesOrderItems)
            {
                if (item is null || item.IsEmpty)
                    continue;
                //var inv = GetInventoryData(data,item.ProductUuid);
                
                CalculateDetail(item, data, processingMode);
                if (!item.IsAr)
                {
                    sum.SubTotalAmount += item.ExtAmount;
                    sum.TaxableAmount += item.TaxableAmount;
                    sum.NonTaxableAmount += item.NonTaxableAmount;
                    sum.ChargeAndAllowanceAmount += item.ChargeAndAllowanceAmount;
                }
                sum.UnitCost += item.UnitCost;
                sum.AvgCost += item.AvgCost;
                sum.LotCost += item.LotCost;
            }

            return true;
        }


        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool CalculateDetail(SalesOrderItems item, SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            item.UnitCost = 0;
            item.AvgCost = 0;
            item.LotCost = 0;
            item.ItemTotalAmount = 0;
            item.ShipAmount = 0;
            item.CancelledAmount = 0;
            item.OpenAmount = 0;
            item.TaxAmount = 0;
            item.DiscountPrice = 0;
            item.ExtAmount = 0;
            item.TaxableAmount = 0;
            item.NonTaxableAmount = 0;
            item.ShippingTaxAmount = 0;
            item.MiscTaxAmount = 0;

            var setting = new ERPSetting();
            var sum = data.SalesOrderHeader;

            //TODO need get inventory object and load inventory cost
            var invData = GetInventoryData(data, item.SKU);
            var inv = (invData == null) ? null : invData.FindInventoryByWarhouse(item.WarehouseCode);
            var invCost = new ItemCostClass(inv);

            // format number var
            item.Price = item.Price.ToPrice();
            item.ShippingAmount = item.ShippingAmount.ToAmount();
            item.MiscAmount = item.MiscAmount.ToAmount();
            item.ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount.ToAmount();
            item.OrderQty = item.OrderQty.ToQty();
            item.ShipQty = item.ShipQty.ToQty();
            item.CancelledQty = item.CancelledQty.ToQty();
            item.OpenQty = item.OpenQty.ToQty();
            item.DiscountRate = item.DiscountRate.ToRate();
            item.DiscountAmount = item.DiscountAmount.ToAmount();

            //Sales by package function  
            item.PackType = string.Empty;
            if (string.IsNullOrEmpty(item.PackType) || item.PackType.EqualsIgnoreSpace(PackType.Each))
                item.PackQty = 1;

            if (item.PackQty > 1)
            {
                if (item.OrderPack < (item.ShipPack + item.CancelledPack))
                    item.OrderPack = (item.ShipPack + item.CancelledPack);

                item.OrderQty = item.OrderPack * item.PackQty;
                item.ShipQty = item.ShipPack * item.PackQty;
                item.CancelledQty = item.CancelledPack * item.PackQty;
                item.OpenQty = item.OpenPack * item.PackQty;
            }
            else
            {
                if (item.OrderQty < (item.ShipQty + item.CancelledQty))
                    item.OrderQty = (item.ShipQty + item.CancelledQty);

                item.OrderPack = item.OrderQty;
                item.ShipPack = item.ShipQty;
                item.CancelledPack = item.CancelledQty;
                item.OpenPack = item.OpenQty;
            }

            // We support both discount rate and discount amount
            // if exist discount rate, it will apply to unit price and get after discount price
            item.DiscountPrice = item.Price;
            if (!item.DiscountRate.IsZero())
            {
                item.DiscountPrice = (item.Price * (1 - item.DiscountRate.ToRate())).ToPrice();
            }
            // use after discount price to calculate ext. amount
            item.ExtAmount = (item.DiscountPrice * item.OrderQty).ToAmount();
            // discount amount will apply to total ext.amount
            item.ExtAmount -= item.DiscountAmount.ToAmount();

            item.ShipAmount = (item.DiscountPrice * item.ShipQty).ToAmount();
            item.CancelledAmount = (item.DiscountPrice * item.CancelledQty).ToAmount();
            item.OpenAmount = (item.DiscountPrice * item.OpenQty).ToAmount();

            // if item is taxable, need add item amount to TaxableAmount
            if (item.Taxable)
            {
                item.TaxableAmount = item.ExtAmount;
                item.NonTaxableAmount = 0;
                if (item.TaxRate.IsZero())
                    item.TaxRate = sum.TaxRate;
                item.TaxRate = item.TaxRate.ToRate();
            }
            else
            {
                item.TaxableAmount = 0;
                item.NonTaxableAmount = item.ExtAmount;
                item.TaxRate = 0;
            }
            item.TaxAmount = (item.TaxableAmount * item.TaxRate).ToAmount();

            // depend on erp setting, it may charge tax for shipping and handling amount
            if (setting.TaxForShippingAndHandling)
            {
                item.ShippingTaxAmount = (item.ShippingAmount * item.TaxRate).ToAmount();
                item.MiscTaxAmount = (item.MiscAmount * item.TaxRate).ToAmount();
            }

            // this item total amount is item reference total
            item.ItemTotalAmount = (
                item.ExtAmount +
                item.TaxAmount +
                item.ShippingAmount +
                item.ShippingTaxAmount +
                item.MiscAmount +
                item.MiscTaxAmount +
                item.ChargeAndAllowanceAmount
            ).ToAmount();

            // Only when add new sales order, or new item, load cost from inventory 
            if (processingMode == ProcessingMode.Add || item.IsNew)
            {
                item.UnitCost = invCost.UnitCost;
                item.AvgCost = invCost.AvgCost;
                item.LotCost = invCost.AvgCost;
            }

            // some item maybe not calculate cost or profilt
            if (item.Costable)
            {
                item.UnitCost = 0;
                item.AvgCost = 0;
                item.LotCost = 0;
            }
            else if (item.IsProfit)
            {
                item.UnitCost = item.ExtAmount;
                item.AvgCost = item.ExtAmount;
                item.LotCost = item.ExtAmount;
            }

            return true;
        }


        #region message
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (ServiceMessage != null)
                    return ServiceMessage.Messages;

                if (_Messages == null)
                    _Messages = new List<MessageClass>();
                return _Messages;
            }
            set
            {
                if (ServiceMessage != null)
                    ServiceMessage.Messages = value;
                else
                    _Messages = value;
            }
        }
        protected IList<MessageClass> _Messages;
        public IMessage ServiceMessage { get; set; }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddInfo(message, code) : Messages.AddInfo(message, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddWarning(message, code) : Messages.AddWarning(message, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddError(message, code) : Messages.AddError(message, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddFatal(message, code) : Messages.AddFatal(message, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddDebug(message, code) : Messages.AddDebug(message, code);

        #endregion message

    }
}




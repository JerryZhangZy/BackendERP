

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

        public SalesOrderServiceCalculatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = serviceMessage;
            this.dbFactory = dbFactory;
        }
        public SalesOrderServiceCalculatorDefault(IDataBaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected IDataBaseFactory dbFactory { get; set; }

        //

        public virtual void PrepareData(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data == null || data.SalesOrderHeader == null)
                return;
        }

        private DateTime now = DateTime.Now;

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
                inventoryService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, sku);
                return inventoryService.Data;
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
                customerService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, customerCode);
                return customerService.Data;
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
            sum.UpdateDateUtc = now;
            //EnterBy
            //UpdateBy
            if (processingMode == ProcessingMode.Add)
            {
                if (string.IsNullOrEmpty(sum.OrderNumber))
                {
                    sum.OrderNumber = NumberGenerate.Generate();
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

            if (data.SalesOrderHeaderInfo != null)
            {
                data.SalesOrderHeaderInfo.UpdateDateUtc = now;
            }
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
            item.UpdateDateUtc = now;
            if (item.ItemTime.IsZero()) item.ItemTime = now.TimeOfDay;
            if (item.ItemDate.IsZero())
            {
                item.ItemDate = now.Date;
                item.ItemTime = now.TimeOfDay;
            }
            item.UpdateDateUtc = now;
            if (processingMode == ProcessingMode.Add)
            {
                item.SalesOrderItemsUuid = Guid.NewGuid().ToString();
            }

            //Set SKU info
            var inventoryData = GetInventoryData(data, item.SKU);
            // currency priority: user input > customer > Sku
            //if (string.IsNullOrEmpty(item.Currency))
            //    item.Currency = data.SalesOrderHeader.Currency;
            if (inventoryData != null)
            {
                item.ProductUuid = inventoryData.ProductBasic.ProductUuid;
                var inventory = inventoryService.GetInventory(inventoryData, item);
                item.InventoryUuid = inventory.InventoryUuid;
                item.WarehouseCode = inventory.WarehouseCode;
                item.WarehouseUuid = inventory.WarehouseUuid;
                item.LotNum = inventory.LotNum;
                item.UOM = inventory.UOM;
                //if (string.IsNullOrEmpty(item.Currency))
                //    item.Currency = inventory.Currency;
            }


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

            //TODO: add calculate summary object logic
            //This is generated sample code

            var setting = new ERPSetting();
            var sum = data.SalesOrderHeader;

            sum.ShippingAmount = sum.ShippingAmount.ToAmount();
            sum.MiscAmount = sum.MiscAmount.ToAmount();
            sum.ChargeAndAllowanceAmount = sum.ChargeAndAllowanceAmount.ToAmount();

            // if exist DiscountRate, calculate discount amount, otherwise use entry discount amount
            if (!sum.DiscountRate.IsZero())
                sum.DiscountAmount = (sum.SubTotalAmount * sum.DiscountRate.ToRate()).ToAmount();
            else
                sum.DiscountRate = 0;

            sum.DiscountAmount = sum.DiscountAmount.ToAmount();
            //manual input max discount amount is SubTotalAmount

            // tax calculate should deduct discount from taxable amount
            //sum.TaxAmount = ((sum.TaxableAmount - sum.DiscountAmount * (sum.TaxableAmount / sum.SubTotalAmount).ToRate()) * sum.TaxRate).ToAmount();

            var discountRate = sum.SubTotalAmount != 0 ? (sum.DiscountAmount / sum.SubTotalAmount) : 0;
            sum.TaxAmount = (sum.TaxableAmount * (1 - discountRate)) * sum.TaxRate;
            sum.TaxAmount = sum.TaxAmount.ToAmount();


            if (setting.TaxForShippingAndHandling)
            {
                sum.ShippingTaxAmount = (sum.ShippingAmount * sum.TaxRate).ToAmount();
                sum.MiscTaxAmount = (sum.MiscAmount * sum.TaxRate).ToAmount();
                sum.TaxAmount = (sum.TaxAmount + sum.ShippingTaxAmount + sum.MiscTaxAmount).ToAmount();
            }

            sum.SalesAmount = (sum.SubTotalAmount - sum.DiscountAmount).ToAmount();
            sum.TotalAmount = (
                sum.SalesAmount +
                sum.TaxAmount +
                sum.ShippingAmount +
                sum.MiscAmount +
                sum.ChargeAndAllowanceAmount
                ).ToAmount();

            sum.Balance = (sum.TotalAmount - sum.PaidAmount - sum.CreditAmount).ToAmount();

            //sum.DueDate = sum.InvoiceDate.AddDays(sum.TermsDays);

            return true;
        }

        public virtual bool CalculateDetail(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;
            //TODO: add calculate summary object logic
            //This is generated sample code

            var sum = data.SalesOrderHeader;
            sum.SubTotalAmount = 0;
            sum.TaxableAmount = 0;
            sum.NonTaxableAmount = 0;
            sum.UnitCost = 0;
            sum.AvgCost = 0;
            sum.LotCost = 0;
            sum.TaxRate = sum.TaxRate.ToRate();

            foreach (var item in data.SalesOrderItems)
            {
                if (item is null || item.IsEmpty)
                    continue;
                //var inv = GetInventoryData(data,item.ProductUuid);

                CalculateDetail(item, data, processingMode);
                if (item.IsAr)
                {
                    sum.SubTotalAmount += item.ExtAmount;
                    sum.TaxableAmount += item.TaxableAmount;
                    sum.NonTaxableAmount += item.NonTaxableAmount;
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

            var setting = new ERPSetting();
            var sum = data.SalesOrderHeader;
            //var prod = data.GetCache<ProductBasic>(ProductId);
            //var inv = data.GetCache<Inventory>(InventoryId);
            //var invCost = new ItemCostClass(inv);
            var invCost = new ItemCostClass();

            item.Price = item.Price.ToPrice();
            item.ShippingAmount = item.ShippingAmount.ToAmount();
            item.MiscAmount = item.MiscAmount.ToAmount();
            item.ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount.ToAmount();
            item.OrderQty = item.OrderQty.ToQty();
            item.ShipQty = item.ShipQty.ToQty();
            item.CancelledQty = item.CancelledQty.ToQty();
            item.DiscountRate = item.DiscountRate.ToRate();
            //TODO check this logic
            //item.PackType = string.Empty;
            //if (string.IsNullOrEmpty(item.PackType) || item.PackType.EqualsIgnoreSpace(PackType.Each))
            //    item.PackQty = 1;

            //if (item.PackQty > 1)
            //{
            //    item.OrderQty = item.OrderPack * item.PackQty;
            //    item.ShipQty = item.ShipPack * item.PackQty;
            //    item.CancelledQty = item.CancelledPack * item.PackQty;
            //}
            //else
            //{
            //    item.OrderPack = item.OrderQty;
            //    item.ShipPack = item.ShipQty;
            //    item.CancelledPack = item.CancelledQty;
            //}

            //PriceRule
            // if exist DiscountRate, calculate after discount unit price
            if (!item.DiscountRate.IsZero())
            {
                item.DiscountPrice = (item.Price * item.DiscountRate).ToPrice();
                item.ExtAmount = (item.DiscountPrice * item.ShipQty).ToAmount();
                //DiscountAmount=Totalprice-ExtAmount
                item.DiscountAmount = (item.Price * item.ShipQty).ToAmount() - item.ExtAmount;
            }
            else
            {
                item.DiscountPrice = item.Price;//TODO Check this logic
                item.ExtAmount = (item.Price * item.ShipQty).ToAmount() - item.DiscountAmount.ToAmount();
            }

            if (item.Taxable)
            {
                item.TaxableAmount = item.ExtAmount;
                item.NonTaxableAmount = 0;
                if (item.TaxRate.IsZero())
                    item.TaxRate = sum.TaxRate;
            }
            else
            {
                item.TaxableAmount = 0;
                item.NonTaxableAmount = item.ExtAmount;
                item.TaxRate = 0;
            }
            item.TaxAmount = (item.TaxableAmount * item.TaxRate).ToAmount();

            if (setting.TaxForShippingAndHandling)
            {
                item.ShippingTaxAmount = (item.ShippingAmount * item.TaxRate).ToAmount();
                item.MiscTaxAmount = (item.MiscAmount * item.TaxRate).ToAmount();
            }

            item.ItemTotalAmount = (
                item.ExtAmount +
                item.TaxAmount +
                item.ShippingAmount +
                item.ShippingTaxAmount +
                item.MiscAmount +
                item.MiscTaxAmount +
                item.ChargeAndAllowanceAmount
                ).ToAmount();

            item.UnitCost = invCost.UnitCost;
            item.AvgCost = invCost.AvgCost;
            item.LotCost = invCost.AvgCost;
            if (!item.Costable)
            {
                item.UnitCost = 0;
                item.AvgCost = 0;
                item.LotCost = 0;
            }
            else if (!item.IsProfit)
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




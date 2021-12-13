

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
    /// Represents a default InvoiceService Calculator class.
    /// </summary>
    public partial class InvoiceServiceCalculatorDefault : ICalculator<InvoiceData>
    {
        protected IDataBaseFactory dbFactory { get; set; }
        private IInvoiceService _invoiceService;
        public InvoiceServiceCalculatorDefault(IInvoiceService  invoiceService, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = (IMessage)invoiceService;
            _invoiceService = invoiceService;
            this.dbFactory = dbFactory;
        }
        public InvoiceServiceCalculatorDefault(IDataBaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public virtual void PrepareData(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {

        }

        private DateTime now = DateTime.UtcNow;

        #region Service Property

        private SalesOrderService _salesOrderService;

        protected SalesOrderService salesOrderService
        {
            get
            {
                if (_salesOrderService is null)
                    _salesOrderService = new SalesOrderService(dbFactory);
                return _salesOrderService;
            }
        }

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
        public virtual InventoryData GetInventoryData(InvoiceData data, string sku)
        {
            var header = data.InvoiceHeader;
            var key = header.MasterAccountNum + "_" + header.ProfileNum + '_' + sku;
            return data.GetCache(key, () =>
            {
                if (inventoryService.GetByNumber(header.MasterAccountNum, header.ProfileNum, sku))
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
        public virtual CustomerData GetCustomerData(InvoiceData data, string customerCode)
        {
            var header = data.InvoiceHeader;
            var key = header.MasterAccountNum + "_" + header.ProfileNum + '_' + customerCode;
            return data.GetCache(key, () =>
            {
                if (customerService.GetByNumber(header.MasterAccountNum, header.ProfileNum, customerCode))
                    return customerService.Data;
                return null;
            });
        }
        public virtual SalesOrderData GetSalesOrderData(InvoiceData data, string orderNumber)
        {
            var header = data.InvoiceHeader;
            var key = header.MasterAccountNum + "_" + header.ProfileNum + '_' + orderNumber;
            return data.GetCache(key, () =>
            {
                if (salesOrderService.GetByNumber(header.MasterAccountNum, header.ProfileNum, orderNumber))
                    return salesOrderService.Data;
                return null;
            });
        }


        #endregion

        public virtual bool SetDefault(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add set default summary data logic
            //This is generated sample code
            var sum = data.InvoiceHeader;

            if (sum.InvoiceTime.IsZero()) sum.InvoiceTime = now.TimeOfDay;
            if (sum.InvoiceDate.IsZero())
            {
                sum.InvoiceDate = now.Date;
                sum.InvoiceTime = now.TimeOfDay;
            }

            if (processingMode == ProcessingMode.Add)
            {
                if (string.IsNullOrEmpty(sum.InvoiceNumber) || 
                    _invoiceService.ExistInvoiceNumber(sum.InvoiceNumber, sum.MasterAccountNum, sum.ProfileNum)
                )
                {
                    sum.InvoiceNumber =  _invoiceService.GetNextNumber(data.InvoiceHeader.MasterAccountNum, data.InvoiceHeader.ProfileNum);
                }

                //for Add mode, always reset uuid
                sum.InvoiceUuid = Guid.NewGuid().ToString();
            }

            // get customer data
            var customerData = GetCustomerData(data, sum.CustomerCode);
            if (customerData != null && customerData.Customer != null)
            {
                sum.CustomerUuid = customerData.Customer.CustomerUuid;
                //sum.CustomerName = customerData.Customer.CustomerName;
                //if (string.IsNullOrEmpty(sum.Currency))
                //    sum.Currency = customerData.Customer.Currency;
            }

            //Set salesorder info 
            var salesOrderData = GetSalesOrderData(data, sum.OrderNumber);
            if (salesOrderData != null && salesOrderData.SalesOrderHeader != null)
            {
                sum.SalesOrderUuid = salesOrderData.SalesOrderHeader.SalesOrderUuid;
                //TODO check this.
                //sum.SalesAmount = salesOrderData.SalesOrderHeader.SalesAmount;
            }

            sum.DueDate = sum.InvoiceDate.AddDays(sum.TermsDays);
            //EnterBy
            //UpdateBy
             
            return true;
        }

        public virtual bool SetDefaultDetail(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null || data.InvoiceItems == null || data.InvoiceItems.Count == 0)
                return false;

            //TODO: add set default for detail list logic
            // This is generated sample code

            foreach (var item in data.InvoiceItems)
            {
                SetDefault(item, data, processingMode);
            }

            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool SetDefault(InvoiceItems item, InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        { 
            if (item.ItemTime.IsZero()) item.ItemTime = now.TimeOfDay;
            if (item.ItemDate.IsZero())
            {
                item.ItemDate = now.Date;
                item.ItemTime = now.TimeOfDay;
            } 

            if (processingMode == ProcessingMode.Add)
            {
                item.InvoiceItemsUuid = Guid.NewGuid().ToString();
            }

            //if (string.IsNullOrEmpty(item.Currency))
            //    item.Currency = data.InvoiceHeader.Currency;
            //Set SKU info
            var inventoryData = GetInventoryData(data, item.SKU);
            if (inventoryData == null) 
                return false;

            item.ProductUuid = inventoryData.ProductBasic.ProductUuid;

            var inventory = inventoryData.FindInventoryByWarhouse(item.WarehouseCode);
            if (inventory == null)
                return false;

            if (!string.IsNullOrEmpty(inventory.InventoryUuid)) item.InventoryUuid = inventory.InventoryUuid;
            if (!string.IsNullOrEmpty(inventory.WarehouseCode)) item.WarehouseCode = inventory.WarehouseCode;
            if (!string.IsNullOrEmpty(inventory.WarehouseUuid)) item.WarehouseUuid = inventory.WarehouseUuid;

            item.LotNum = inventory.LotNum;
            item.UOM = inventory.UOM;

            //Set salesorder info 
            var salesOrderData = GetSalesOrderData(data, data.InvoiceHeader.OrderNumber);
            if (salesOrderData != null && salesOrderData.SalesOrderItems != null && salesOrderData.SalesOrderItems.Count > 0)
            {
                //salesOrderData.SalesOrderItems.Where(i => i.s == item.)
                //item.OrderAmount=
                //item.OrderPack
                //item.OrderQty 
            }

            return true;
        }


        public virtual bool Calculate(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            return true;
        }

        public virtual bool CalculateSummary(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            //This is generated sample code
            var sum = data.InvoiceHeader;

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
                + sum.ChargeAndAllowanceAmount
                ).ToAmount();

            sum.Balance = (sum.TotalAmount - sum.PaidAmount - sum.CreditAmount).ToAmount();

            // calculate commission amount
            // if has line commission, replace commission amount 1 with line commission amount 
            sum.CommissionAmount = (sum.SalesAmount * sum.CommissionRate).ToAmount();
            sum.CommissionAmount2 = (sum.SalesAmount * sum.CommissionRate2).ToAmount();
            sum.CommissionAmount3 = (sum.SalesAmount * sum.CommissionRate3).ToAmount();
            sum.CommissionAmount4 = (sum.SalesAmount * sum.CommissionRate4).ToAmount();
            if (!sum.TotalLineCommissionAmount.IsZero())
            {
                sum.CommissionAmount = sum.TotalLineCommissionAmount;
                sum.CommissionRate = (sum.CommissionAmount / sum.SalesAmount).ToRate();
            }

            return true;
        }

        public virtual bool CalculateDetail(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            //This is generated sample code

            var sum = data.InvoiceHeader;
            //sum.SubTotalAmount = 0;
            //sum.TaxableAmount = 0;
            //sum.NonTaxableAmount = 0;
            //sum.UnitCost = 0;
            //sum.AvgCost = 0;
            //sum.LotCost = 0;
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
            sum.ShippingTaxAmount = 0;
            sum.MiscTaxAmount = 0;
            sum.TaxRate = sum.TaxRate.ToRate();
            sum.ChargeAndAllowanceAmount = 0;
            sum.TotalLineCommissionAmount = 0;

            foreach (var item in data.InvoiceItems)
            {
                if (item is null || item.IsEmpty)
                    continue;
                SetDefault(item, data, processingMode);
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

                sum.TotalLineCommissionAmount += item.CommissionAmount;
            }
            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        public virtual bool CalculateDetail(InvoiceItems item, InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            item.UnitCost = 0;
            item.AvgCost = 0;
            item.LotCost = 0;
            item.ItemTotalAmount = 0;
            item.CancelledAmount = 0;
            item.OpenAmount = 0;
            item.MiscTaxAmount = 0;
            item.TaxAmount = 0;
            item.DiscountPrice = 0;
            item.ExtAmount = 0;
            item.TaxableAmount = 0;
            item.NonTaxableAmount = 0;
            item.ShippingTaxAmount = 0;

            var setting = new ERPSetting();
            var sum = data.InvoiceHeader;

            var invData = GetInventoryData(data, item.SKU);
            var inv = (invData == null) ? null : invData.FindInventoryByWarhouse(item.WarehouseCode);
            var invCost = new ItemCostClass(inv);

            item.Price = item.Price.ToPrice();
            item.ShippingAmount = item.ShippingAmount.ToAmount();
            item.MiscAmount = item.MiscAmount.ToAmount();
            item.ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount.ToAmount();
            item.OrderQty = item.OrderQty.ToQty();
            item.ShipQty = item.ShipQty.ToQty();
            item.CancelledQty = item.CancelledQty.ToQty();
            item.DiscountAmount = item.DiscountAmount.ToAmount();

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

            //PriceRule
            item.DiscountPrice = item.Price;
            if (!item.DiscountRate.IsZero())
            {
                item.DiscountPrice = (item.Price * (1 - item.DiscountRate.ToRate())).ToPrice();
            }
            // use after discount price to calculate ext. amount
            item.ExtAmount = (item.DiscountPrice * item.ShipQty).ToAmount();
            item.ExtAmount -= item.DiscountAmount.ToAmount();
             
            item.CancelledAmount = (item.Price * item.CancelledQty).ToAmount();
            item.OpenAmount = item.Price * item.OpenQty;

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

            // calculate line commission,
            item.CommissionAmount = (item.ExtAmount * item.CommissionRate).ToAmount();

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




    

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
    /// Represents a default CustomIOFormatService Calculator class.
    /// </summary>
    public partial class CustomIOFormatServiceCalculatorDefault : ICalculator<CustomIOFormatData>
    {
        protected IDataBaseFactory dbFactory { get; set; }
        private ICustomIOFormatService _customIOFormatService;
        public CustomIOFormatServiceCalculatorDefault(ICustomIOFormatService customIOFormatService , IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = (IMessage)customIOFormatService;
            this._customIOFormatService = customIOFormatService;
            this.dbFactory = dbFactory;
        }

        public virtual void PrepareData(CustomIOFormatData  data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            //if(data==null||data.SalesOrderHeader==null)
            //    return;
            //if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerUuid))
            //{
            //    using(var trx = new ScopedTransaction(dbFactory)){
            //      data.SalesOrderHeader.CustomerUuid = CustomerServiceHelper.GetCustomerUuidByCustomerCode(
            //          data.SalesOrderHeader.CustomerCode, data.SalesOrderHeader.MasterAccountNum,
            //          data.SalesOrderHeader.ProfileNum);
            //  }
            //}
            //// get customer data
            //GetCustomerData(data,data.SalesOrderHeader.CustomerUuid);

            //if (data.SalesOrderItems != null)
            //{
            //    var skuList = data.SalesOrderItems
            //        .Where(r => string.IsNullOrEmpty(r.ProductUuid) && !string.IsNullOrEmpty(r.SKU)).Select(r => r.SKU)
            //        .Distinct().ToList();
            //    using(var trx = new ScopedTransaction(dbFactory)){
            //      var list = InventoryServiceHelper.GetKeyInfoBySkus(skuList, data.SalesOrderHeader.MasterAccountNum,
            //          data.SalesOrderHeader.ProfileNum);
            //      foreach (var tuple in list)
            //      {
            //          data.SalesOrderItems.First(r => r.SKU == tuple.Item3).ProductUuid = tuple.Item2;
            //      }
            //    }

            //    // get inventory data
            //    foreach (var item in data.SalesOrderItems)
            //    {
            //        if (string.IsNullOrEmpty(item.ProductUuid)) continue;
            //        GetInventoryData(data, item.ProductUuid);
            //    }
            //}
        }
        
        #region Service Property

        //private CustomerService _customerService;
        //protected CustomerService customerService => _customerService ??= new CustomerService(dbFactory);

        //private InventoryService _inventoryService;
        //protected InventoryService inventoryService => _inventoryService ??= new InventoryService(dbFactory);

        #endregion

        #region GetDataWithCache

        //public virtual InventoryData GetInventoryData(SalesOrderData data, string productUuid)
        //{
        //    return data.GetCache(productUuid, () => inventoryService.GetDataById(productUuid) ? inventoryService.Data : null);
        //}

        //public virtual CustomerData GetCustomerData(SalesOrderData data, string customerUuid)
        //{
        //    return data.GetCache(customerUuid, () => customerService.GetDataById(customerUuid) ? customerService.Data : null);
        //}

        #endregion

        public virtual bool SetDefault(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;
            if (processingMode == ProcessingMode.Add)
            {
                //if (data.CustomIOFormat.FormatNumber==0 || _customerService.ExistCustomerCode(data.Customer.CustomerCode, data.Customer.MasterAccountNum, data.Customer.ProfileNum))
                if (data.CustomIOFormat.FormatNumber <= 0 )
                {

                    data.CustomIOFormat.FormatNumber = _customIOFormatService.GetFormatNumber(data.CustomIOFormat.MasterAccountNum, data.CustomIOFormat.ProfileNum, data.CustomIOFormat.FormatType.ToInt());
                }

            }
            //TODO: add set default summary data logic
            /* This is generated sample code
            var sum = data.CustomIOFormat;
            if (sum.InvoiceDate.IsZero()) sum.InvoiceDate = DateTime.UtcNow.Date;
            if (sum.InvoiceTime.IsZero()) sum.InvoiceTime = DateTime.UtcNow.TimeOfDay;

            //UpdateDateUtc
            //EnterBy
            //UpdateBy
            */

            return true;
        }

        public virtual bool SetDefaultDetail(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add set default for detail list logic
            /* This is generated sample code

            foreach (var item in data.InvoiceItems)
            {
                if (item is null || item.IsEmpty)
                    continue;
                SetDefault(item, data, processingMode);
            }

            */
            return true;
        }

        //TODO: add set default for detail line logic
        /* This is generated sample code
        protected virtual bool SetDefault(InvoiceItems item, CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            var setting = new ERPSetting();
            var sum = data.CustomIOFormat;
            //var prod = data.GetCache<ProductBasic>(ProductId);
            //var inv = data.GetCache<Inventory>(InventoryId);
            //var invCost = new ItemCostClass(inv);
            var invCost = new ItemCostClass();

            //InvoiceItemType
            //InvoiceItemStatus
            //ItemDate
            //ItemTime
            //ShipDate
            //EtaArrivalDate

            //SKU
            //ProductUuid
            //InventoryUuid
            //WarehouseUuid
            //LotNum
            //Description
            //Notes
            //UOM
            //Currency

            return true;
        }
        */


        public virtual bool Calculate(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            PrepareData(data);
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            return true;
        }

        public virtual bool CalculateSummary(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            /* This is generated sample code

            var setting = new ERPSetting();
            var sum = data.InvoiceHeader;

            sum.ShippingAmount = sum.ShippingAmount.ToAmount();
            sum.MiscAmount = sum.MiscAmount.ToAmount();
            sum.ChargeAndAllowanceAmount = sum.ChargeAndAllowanceAmount.ToAmount();

            // if exist DiscountRate, calculate discount amount, otherwise use entry discount amount
            if (!sum.DiscountRate.IsZero())
                sum.DiscountAmount = (sum.SubTotalAmount * sum.DiscountRate.ToRate()).ToAmount();
            else
                sum.DiscountRate = 0;

            // tax calculate should deduct discount from taxable amount
            sum.TaxAmount = ((sum.TaxableAmount - sum.DiscountAmount * (sum.TaxableAmount / sum.SubTotalAmount).ToRate()) * sum.TaxRate).ToAmount();

            if (setting.TaxForShippingAndHandling)
            {
                sum.ShippingTaxAmount = (sum.ShippingAmount * sum.TaxRate).ToAmount();
                sum.MiscTaxAmount = (sum.MiscAmount * sum.TaxRate).ToAmount();
                sum.TaxAmount = (sum.TaxAmount + sum.ShippingTaxAmount + sum.MiscTaxAmount).ToAmount();
            }

            sum.SalesAmount = (sum.SubTotalAmount - sum.DiscountAmount).ToAmount();
            sum.TotalAmount =(
                sum.SalesAmount +
                sum.TaxAmount +
                sum.ShippingAmount +
                sum.MiscAmount +
                sum.ChargeAndAllowanceAmount
                ).ToAmount();

            sum.Balance = (sum.TotalAmount - sum.PaidAmount - sum.CreditAmount).ToAmount();

            sum.DueDate = sum.InvoiceDate.AddDays(sum.TermsDays);

            */
            return true;
        }

        public virtual bool CalculateDetail(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            /* This is generated sample code

            var sum = data.CustomIOFormat;
            sum.SubTotalAmount = 0;
            sum.TaxableAmount = 0;
            sum.NonTaxableAmount = 0;
            sum.UnitCost = 0;
            sum.AvgCost = 0;
            sum.LotCost = 0;

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
                }
                sum.UnitCost += item.UnitCost;
                sum.AvgCost += item.AvgCost;
                sum.LotCost += item.LotCost;
            }

            */
            return true;
        }

        //TODO: add set default for detail line logic
        /* This is generated sample code
        protected virtual bool CalculateDetail(InvoiceItems item, CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            var setting = new ERPSetting();
            var sum = data.InvoiceHeader;
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

            item.PackType = string.Empty;
            if (string.IsNullOrEmpty(item.PackType) || item.PackType.EqualsIgnoreSpace(PackType.Each))
                item.PackQty = 1;

            if (item.PackQty > 1)
            {
                item.OrderQty = item.OrderPack * item.PackQty;
                item.ShipQty = item.ShipPack * item.PackQty;
                item.CancelledQty = item.CancelledPack * item.PackQty;
            }
            else
            {
                item.OrderPack = item.OrderQty;
                item.ShipPack = item.ShipQty;
                item.CancelledPack = item.CancelledQty;
            }

            //PriceRule
            // if exist DiscountRate, calculate after discount unit price
            if (!item.DiscountRate.IsZero())
            {
                item.DiscountPrice = (item.Price * (item.DiscountRate.ToRate() / 100)).ToPrice();
                item.ExtAmount = (item.DiscountPrice * item.ShipQty).ToAmount();
                item.DiscountAmount = (item.Price * item.ShipQty).ToAmount() - item.ExtAmount;
            }
            else
            {
                item.DiscountPrice = item.Price;
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

            item.ItemTotalAmount =(
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
        */
        
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




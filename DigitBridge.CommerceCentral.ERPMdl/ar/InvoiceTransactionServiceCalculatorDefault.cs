

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
    /// Represents a default InvoiceTransactionService Calculator class.
    /// </summary>
    public partial class InvoiceTransactionServiceCalculatorDefault : ICalculator<InvoiceTransactionData>
    {
        protected IDataBaseFactory dbFactory { get; set; }

        public InvoiceTransactionServiceCalculatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = serviceMessage;
            this.dbFactory = dbFactory;
        }

        public virtual void PrepareData(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {

        }

        private DateTime now = DateTime.Now;

        #region Service Property


        private InventoryService _inventoryService;
        protected InventoryService inventoryService => _inventoryService ??= new InventoryService(dbFactory);

        private InvoiceService _invoiceService;
        protected InvoiceService invoiceService => _invoiceService ??= new InvoiceService(dbFactory);

        protected CustomerService _customerService;
        protected CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
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
        public virtual InventoryData GetInventoryData(InvoiceTransactionData data, string sku)
        {
            var key = data.InvoiceTransaction.MasterAccountNum + "_" + data.InvoiceTransaction.ProfileNum + '_' + sku;
            return data.GetCache(key, () =>
            {
                inventoryService.GetByNumber(data.InvoiceTransaction.MasterAccountNum, data.InvoiceTransaction.ProfileNum, sku);
                return inventoryService.Data;
            });
        }

        public virtual InvoiceData GetInvoiceData(InvoiceTransactionData data, string invoiceNumber)
        {
            var key = data.InvoiceTransaction.MasterAccountNum + "_" + data.InvoiceTransaction.ProfileNum + '_' + invoiceNumber;
            return data.GetCache(key, () =>
            {
                invoiceService.GetByNumber(data.InvoiceTransaction.MasterAccountNum, data.InvoiceTransaction.ProfileNum, invoiceNumber);
                return invoiceService.Data;
            });
        }

        /// <summary>
        /// Get Customer Data by customerCode
        /// </summary>
        /// <param name="data"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public virtual CustomerData GetCustomerData(InvoiceTransactionData data, string customerCode)
        {
            var key = data.InvoiceTransaction.MasterAccountNum + "_" + data.InvoiceTransaction.ProfileNum + '_' + customerCode;
            return data.GetCache(key, () =>
            {
                customerService.GetByNumber(data.InvoiceTransaction.MasterAccountNum, data.InvoiceTransaction.ProfileNum, customerCode);
                return customerService.Data;
            });
        }
        #endregion

        public virtual bool SetDefault(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var sum = data.InvoiceTransaction;
            if (data is null || sum == null)
                return false;

            if (sum.TransTime.IsZero()) sum.TransTime = now.TimeOfDay;
            if (sum.TransDate.IsZero())
            {
                sum.TransDate = now.Date;
                sum.TransTime = now.TimeOfDay;
            } 

            if (processingMode == ProcessingMode.Add)
            {
                //set default tran num  
                if (sum.TransNum.IsZero())
                {
                    using (var tx = new ScopedTransaction(dbFactory))
                    {
                        sum.TransNum = InvoiceTransactionHelper.GetTranSeqNum(sum.InvoiceNumber, sum.ProfileNum.ToInt());
                    }
                }
                //for Add mode, always reset uuid
                sum.TransUuid = Guid.NewGuid().ToString();
            }

            //Set default for invoice
            var invoiceData = GetInvoiceData(data, sum.InvoiceNumber);
            if (invoiceData != null && invoiceData.InvoiceHeader != null)
            {
                sum.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
                //sum.BankAccountCode = invoiceData.InvoiceHeader 
                sum.Currency = invoiceData.InvoiceHeader.Currency;  
                sum.TaxRate = invoiceData.InvoiceHeader.TaxRate;
                //sum.TaxableAmount = invoiceData.InvoiceHeader.TaxableAmount;
                //sum.NonTaxableAmount = invoiceData.InvoiceHeader.NonTaxableAmount;
                //sum.ShippingAmount = invoiceData.InvoiceHeader.ShippingAmount;
                //sum.ShippingTaxAmount = invoiceData.InvoiceHeader.ShippingTaxAmount;
                //sum.MiscAmount = invoiceData.InvoiceHeader.MiscAmount;
                //sum.MiscTaxAmount = invoiceData.InvoiceHeader.MiscTaxAmount;
                //sum.ChargeAndAllowanceAmount = invoiceData.InvoiceHeader.ChargeAndAllowanceAmount;
                //sum.DiscountAmount = invoiceData.InvoiceHeader.DiscountAmount;//todo
                //sum.DiscountRate = invoiceData.InvoiceHeader.DiscountRate;//todo 
                //sum.SalesAmount = invoiceData.InvoiceHeader.SalesAmount; 

                //var customerData = GetCustomerData(data, invoiceData.InvoiceHeader.CustomerCode);
                //if (customerData != null && customerData.Customer != null)
                //{
                //    //sum.BankAccountCode
                //    //sum.BankAccountUuid
                //    //sum.CreditAccount= 
                //    //sum.PaidBy=
                //}
            }

            //EnterBy
            //UpdateBy 

            return true;
        }

        public virtual bool SetDefaultDetail(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null || data.InvoiceReturnItems == null || data.InvoiceReturnItems.Count == 0)
                return false;

            //TODO: add set default for detail list logic
            // This is generated sample code

            foreach (var item in data.InvoiceReturnItems)
            {
                SetDefault(item, data, processingMode);
            }
            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool SetDefault(InvoiceReturnItems item, InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        { 
            if (item.ReturnTime.IsZero()) item.ReturnTime = now.TimeOfDay;
            if (item.ReturnDate.IsZero())
            {
                item.ReturnDate = now.Date;
                item.ReturnTime = now.TimeOfDay;
            }

            if (processingMode == ProcessingMode.Add)
            {
                //for Add mode, always reset uuid
                item.ReturnItemUuid = Guid.NewGuid().ToString();
            }

            //Set default for invoice
            var invoiceData = GetInvoiceData(data, data.InvoiceTransaction.InvoiceNumber);
            if (invoiceData != null)
            {
                item.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
                //item.InvoiceDiscountPrice=invoiceData.InvoiceHeader.DiscountAmount
                var invoiceItem = invoiceData.InvoiceItems.Where(i => i.InvoiceItemsUuid == item.InvoiceItemsUuid).FirstOrDefault();
                if (invoiceItem != null)
                {
                    item.InvoiceWarehouseCode = invoiceItem.WarehouseCode;
                    item.InvoiceWarehouseUuid = invoiceItem.WarehouseUuid;
                    item.InvoiceDiscountPrice = invoiceItem.DiscountPrice.ToPrice();
                    item.Description = invoiceItem.Description;
                    item.InventoryUuid = item.InventoryUuid;
                    item.IsAr = item.IsAr;
                    item.LotNum = item.LotNum;
                    item.Notes = item.Notes;
                    item.PackType = invoiceItem.PackType;
                    item.PackQty = invoiceItem.PackQty;
                    item.Price = invoiceItem.Price;
                    item.ProductUuid = invoiceItem.ProductUuid;
                    item.TaxRate = invoiceItem.TaxRate;
                    item.UOM = invoiceItem.UOM;
                    item.SKU = invoiceItem.SKU;
                    item.Currency = invoiceItem.Currency;
                }
            }
            return true;
        }

        public virtual bool Calculate(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            PrepareData(data);
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            return true;
        }

        public virtual bool CalculateSummary(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            //This is generated sample code

            var setting = new ERPSetting();
            var sum = data.InvoiceTransaction;

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

            //sum.Balance = (sum.TotalAmount - sum.PaidAmount - sum.CreditAmount).ToAmount();

            //sum.DueDate = sum.InvoiceDate.AddDays(sum.TermsDays);

            return true;
        }

        public virtual bool CalculateDetail(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;
            //TODO: add calculate summary object logic
            //This is generated sample code

            var sum = data.InvoiceTransaction;
            //sum.SubTotalAmount = 0;
            //sum.TaxableAmount = 0;
            //sum.NonTaxableAmount = 0;
            sum.SubTotalAmount = 0;
            sum.SalesAmount = 0;
            sum.TotalAmount = 0;
            sum.TaxableAmount = 0;
            sum.NonTaxableAmount = 0;
            sum.TaxAmount = 0;
            sum.TaxRate = sum.TaxRate.ToRate();

            foreach (var item in data.InvoiceReturnItems)
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
                }
            }

            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool CalculateDetail(InvoiceReturnItems item, InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            var setting = new ERPSetting();
            var sum = data.InvoiceTransaction;
            //var prod = data.GetCache<ProductBasic>(ProductId);
            //var inv = data.GetCache<Inventory>(InventoryId);
            //var invCost = new ItemCostClass(inv);
            var invCost = new ItemCostClass();

            item.Price = item.Price.ToPrice();
            item.ShippingAmount = item.ShippingAmount.ToAmount();
            item.MiscAmount = item.MiscAmount.ToAmount();
            item.ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount.ToAmount();
            item.ReturnQty = item.ReturnQty < 0 ? 0 : item.ReturnQty.ToQty();
            item.ReceiveQty = item.ReceiveQty < 0 ? 0 : item.ReceiveQty.ToQty();
            item.StockQty = item.StockQty < 0 ? 0 : item.StockQty.ToQty();
            item.NonStockQty = item.NonStockQty < 0 ? 0 : item.StockQty.ToQty();
            item.Price = item.Price.ToPrice();
            //item.ReceiveQty= item.StockQty + item.NonStockQty // Are they equal.?

            item.PackType = string.Empty;
            if (string.IsNullOrEmpty(item.PackType) || item.PackType.EqualsIgnoreSpace(PackType.Each))
                item.PackQty = 1;

            if (item.PackQty > 1)
            {
                item.ReceiveQty = item.ReceivePack * item.PackQty;
                item.ReturnQty = item.ReturnPack * item.PackQty;
                //item.CancelledQty = item.CancelledPack * item.PackQty;
            }
            else
            {
                item.ReceivePack = item.ReceiveQty;
                item.ReturnPack = item.ReturnQty;
                //item.CancelledPack = item.CancelledQty;
            }

            item.ExtAmount = (item.InvoiceDiscountPrice * item.ReceiveQty).ToAmount();

            if (item.Taxable)
            {
                item.TaxableAmount = item.ExtAmount;
                item.NonTaxableAmount = 0;
            }
            else
            {
                item.TaxableAmount = 0;
                item.NonTaxableAmount = item.ExtAmount;
            }
            item.TaxAmount = (item.TaxableAmount * item.TaxRate).ToAmount();

            //TODO copy from invoice item  or calculate by percent receivedQty/shipQty
            //if (setting.TaxForShippingAndHandling)
            //{
            //    item.ShippingTaxAmount = (item.ShippingAmount * item.TaxRate).ToAmount();
            //    item.MiscTaxAmount = (item.MiscAmount * item.TaxRate).ToAmount();
            //}
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






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
    /// Represents a default PoTransactionService Calculator class.
    /// </summary>
    public partial class PoReceiveServiceCalculatorDefault : ICalculator<PoTransactionData>
    {
        protected IDataBaseFactory dbFactory { get; set; }

        public PoReceiveServiceCalculatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = serviceMessage;
            this.dbFactory = dbFactory;
        }

        public virtual void PrepareData(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {

        }

        private DateTime now = DateTime.UtcNow;

        public virtual bool SetDefault(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var sum = data.PoTransaction;
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
                        sum.TransNum = PoTransactionHelper.GetTranSeqNum(sum.VendorCode, sum.ProfileNum.ToInt());
                    }
                }
                //for Add mode, always reset uuid
                sum.TransUuid = Guid.NewGuid().ToString();
 
            }

 

            return true;
        }

        public virtual bool SetDefaultDetail(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null || data.PoTransactionItems == null || data.PoTransactionItems.Count == 0)
                return false;

            //TODO: add set default for detail list logic
            // This is generated sample code

            foreach (var item in data.PoTransactionItems)
            {
                SetDefault(item, data, processingMode);
            }
            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool SetDefault(PoTransactionItems item, PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            item.UpdateDateUtc = now;
            if (item.ItemDate.IsZero())
            {
                item.ItemDate = now.Date;
                item.ItemTime = now.TimeOfDay;
            }

            if (processingMode == ProcessingMode.Add)
            {
                //for Add mode, always reset uuid
                item.TransItemUuid = Guid.NewGuid().ToString();
            }

            //Set default for invoice
            //var poData = GetPurchaseOrderData(item.PoNum, data.PoTransaction.ProfileNum, data.PoTransaction.MasterAccountNum); 
            //if (poData != null)
            //{
            //    var poItem = poData.PoItems.FirstOrDefault(i => i.PoItemUuid == item.PoItemUuid);
            //    if (poItem != null)
            //    {
            //        item.PoUuid = poItem.PoUuid;
            //        item.InventoryUuid = item.InventoryUuid;
            //        item.ProductUuid = poItem.ProductUuid;
            //        item.SKU = poItem.SKU;
            //        item.Currency = poItem.Currency;
            //        item.LotNum = item.LotNum;
            //        if (item.WarehouseCode.IsZero())
            //        {
            //            item.WarehouseCode = poItem.WarehouseCode;
            //            item.WarehouseUuid = poItem.WarehouseUuid;
            //        }
            //    }
            //}

            return true;
        }

        public virtual bool Calculate(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            PrepareData(data);
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            CalculateCost(data);
            return true;
        }

        public virtual bool CalculateSummary(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            var setting = new ERPSetting();
            var sum = data.PoTransaction;

            sum.ShippingAmount = sum.ShippingAmount.ToAmount();
            sum.MiscAmount = sum.MiscAmount.ToAmount();
            sum.ChargeAndAllowanceAmount = sum.ChargeAndAllowanceAmount.ToAmount();

            // P/O level not support discount
            sum.DiscountAmount = 0;
            sum.DiscountRate = 0;

            // P/O level not support tax
            sum.TaxRate = 0;
            sum.TaxAmount = 0;

            sum.TotalAmount = (
                sum.SubTotalAmount - sum.DiscountAmount +
                sum.TaxAmount +
                sum.ShippingAmount +
                sum.MiscAmount +
                sum.ChargeAndAllowanceAmount
                ).ToAmount();

            return true;
        }

        public virtual bool CalculateDetail(PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            sum.SubTotalAmount = 0;

            foreach (var item in data.PoTransactionItems)
            {
                if (item is null || item.IsEmpty)
                    continue;

                SetDefault(item, data, processingMode);
                CalculateDetail(item, data, processingMode);
                sum.SubTotalAmount += item.ExtAmount;
            }

            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool CalculateDetail(PoTransactionItems item, PoTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            item.TaxAmount = 0;
            item.DiscountPrice = 0;
            item.ExtAmount = 0;
            item.ShippingTaxAmount = 0;
            item.MiscTaxAmount = 0;

            var setting = new ERPSetting();
            var sum = data.PoTransaction;
            //TODO need get inventory object and load inventory cost
            //var prod = data.GetCache<ProductBasic>(ProductId);
            //var inv = data.GetCache<Inventory>(InventoryId);
            //var invCost = new ItemCostClass(inv);
            var invCost = new ItemCostClass();

            // format number var
            item.Price = item.Price.ToPrice();
            item.ShippingAmount = item.ShippingAmount.ToAmount();
            item.MiscAmount = item.MiscAmount.ToAmount();
            item.ChargeAndAllowanceAmount = item.ChargeAndAllowanceAmount.ToAmount();
            item.TransQty = item.TransQty.ToQty();

            //PriceRule
            // if exist DiscountRate, calculate after discount unit price
            item.DiscountPrice = item.Price;
            if (!item.DiscountRate.IsZero())
                item.DiscountPrice = (item.Price * (1 - item.DiscountRate.ToRate())).ToPrice();

            // use after discount price to calculate ext. amount
            item.ExtAmount = (item.DiscountPrice * item.TransQty).ToAmount();
            item.ExtAmount -= item.DiscountAmount.ToAmount();

            // if item is taxable, need add taxAmount to extAmount
            if (!item.TaxRate.IsZero())
                item.TaxAmount = (item.ExtAmount * item.TaxRate).ToAmount();
            item.ExtAmount += item.TaxAmount.ToAmount();

            return true;
        }

        /// <summary>
        /// Calculate cost for each item, this cost will use to update inventory avg cost
        /// Item cost include P/O price, tax, shipping and misc for each item
        /// </summary>
        public virtual bool CalculateCost(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            // assign total shipping fee to each item
            if (sum.ShippingAmountAssign == (int)PoShipMiscAssignType.ByQty)
                CalculateShippingByQty(data);
            else if (sum.ShippingAmountAssign == (int)PoShipMiscAssignType.ByAmount)
                CalculateShippingByAmount(data);
            else
                CalculateShippingByAverage(data);

            // assign total misc fee to each item
            if (sum.MiscAmountAssign == (int)PoShipMiscAssignType.ByQty)
                CalculateMiscByQty(data);
            else if (sum.MiscAmountAssign == (int)PoShipMiscAssignType.ByAmount)
                CalculateMiscByAmount(data);
            else
                CalculateMiscByAverage(data);

            // calcuate cost for each item
            foreach (var item in items)
            {
                item.BaseCost = 0;
                item.UnitCost = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                var amt = (item.DiscountPrice * item.TransQty).ToAmount() - item.DiscountAmount.ToAmount();
                // base cost include after discount price, exclude tax amount
                item.BaseCost = (amt / item.TransQty).ToCost();
                // unit cost include base cost and tax, shipping, misc 
                item.UnitCost = item.BaseCost + item.TaxAmount + item.ShippingAmount + item.MiscAmount;
            }
            return true;
        }

        /// <summary>
        /// Shipping amount is in Receive batch level,
        /// This assign shipping amount to each item by qty ratio
        /// </summary>
        protected virtual bool CalculateShippingByQty(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalQty = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : x.TransQty);
            foreach (var item in items)
            {
                item.ShippingAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.ShippingAmount = (sum.ShippingAmount * (item.TransQty / totalQty)).ToAmount();
            }
            return true;
        }
        /// <summary>
        /// Misc amount is in Receive batch level,
        /// This assign Misc amount to each item by qty ratio
        /// </summary>
        protected virtual bool CalculateMiscByQty(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalQty = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : x.TransQty);
            foreach (var item in items)
            {
                item.MiscAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.MiscAmount = (sum.MiscAmount * (item.TransQty / totalQty)).ToAmount();
            }
            return true;
        }
        /// <summary>
        /// shipping amount is in Receive batch level,
        /// This assign shipping amount to each item by amount ratio
        /// </summary>
        protected virtual bool CalculateShippingByAmount(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalAmount = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : x.ExtAmount);
            foreach (var item in items)
            {
                item.ShippingAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.ShippingAmount = (sum.ShippingAmount * (item.ExtAmount / totalAmount)).ToAmount();
            }
            return true;
        }
        /// <summary>
        /// misc amount is in Receive batch level,
        /// This assign misc amount to each item by amount ratio
        /// </summary>
        protected virtual bool CalculateMiscByAmount(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalAmount = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : x.ExtAmount);
            foreach (var item in items)
            {
                item.MiscAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.MiscAmount = (sum.MiscAmount * (item.ExtAmount / totalAmount)).ToAmount();
            }
            return true;
        }
        /// <summary>
        /// Shipping amount is in Receive batch level,
        /// This assign shipping amount to each item count
        /// </summary>
        protected virtual bool CalculateShippingByAverage(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalCount = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : 1);
            foreach (var item in items)
            {
                item.ShippingAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.ShippingAmount = (sum.ShippingAmount * (1 / totalCount)).ToAmount();
            }
            return true;
        }
        /// <summary>
        /// misc amount is in Receive batch level,
        /// This assign misc amount to each item count
        /// </summary>
        protected virtual bool CalculateMiscByAverage(PoTransactionData data)
        {
            if (data is null)
                return false;

            var sum = data.PoTransaction;
            var items = data.PoTransactionItems;

            var totalCount = items.Sum(x => (x == null || x.IsEmpty || x.TransQty.IsZero()) ? 0 : 1);
            foreach (var item in items)
            {
                item.MiscAmount = 0;
                if (item == null || item.IsEmpty || item.TransQty.IsZero()) continue;
                item.MiscAmount = (sum.MiscAmount * (1 / totalCount)).ToAmount();
            }
            return true;
        }
        protected PurchaseOrderData GetPurchaseOrderData(string poNum, int profileNum, int masterAccountNum)
        {

            var poData = new PurchaseOrderData(dbFactory);
            var success = poData.GetByNumber(masterAccountNum, profileNum, poNum);
            if (!success)
                return null;
            else
                return poData;
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




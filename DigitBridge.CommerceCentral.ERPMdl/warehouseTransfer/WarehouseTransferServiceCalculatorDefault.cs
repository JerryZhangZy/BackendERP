    

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
    /// Represents a default WarehouseTransferService Calculator class.
    /// </summary>
    public partial class WarehouseTransferServiceCalculatorDefault : ICalculator<WarehouseTransferData>
    {
        protected IDataBaseFactory dbFactory { get; set; }

        public WarehouseTransferServiceCalculatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = serviceMessage;
            this.dbFactory = dbFactory;
        }

        public virtual void PrepareData(WarehouseTransferData  data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data == null || data.WarehouseTransferHeader == null)
                return;

            if (!string.IsNullOrEmpty(data.WarehouseTransferHeader.FromWarehouseUuid))
            {
                var warehouse = GetWarehouseData(data, data.WarehouseTransferHeader.FromWarehouseUuid);
                if (warehouse != null)
                {
                    data.WarehouseTransferHeader.FromWarehouseCode = warehouse.DistributionCenter.DistributionCenterCode;
                }
            }

            if (!string.IsNullOrEmpty(data.WarehouseTransferHeader.ToWarehouseUuid))
            {
                var warehouse = GetWarehouseData(data, data.WarehouseTransferHeader.ToWarehouseUuid);
                if (warehouse != null)
                {
                    data.WarehouseTransferHeader.ToWarehouseCode = warehouse.DistributionCenter.DistributionCenterCode;
                }
            }

            if (data.WarehouseTransferItems != null)
            {
                #region From
                var inventoryUuidList = data.WarehouseTransferItems.Where(r => !r.FromInventoryUuid.IsZero()).Select(r => r.FromInventoryUuid)
                    .Distinct().ToList();
                var productUuidList = new List<(string, string)>();
                using (var trx = new ScopedTransaction(dbFactory))
                {
                    productUuidList = InventoryServiceHelper.GetProductUuidsByInventoryUuids(inventoryUuidList, data.WarehouseTransferHeader.MasterAccountNum,
                        data.WarehouseTransferHeader.ProfileNum);
                }
                foreach (var tuple in productUuidList)
                {
                    var inventory = GetInventory(data, tuple.Item2, tuple.Item1);
                    if (inventory != null)
                    {
                        var items = data.WarehouseTransferItems.Where(i => i.FromInventoryUuid == inventory.InventoryUuid).ToList();
                        items.ForEach(x =>
                        {
                            x.SKU = inventory.SKU;
                            x.ProductUuid = inventory.ProductUuid;
                            x.FromWarehouseCode = inventory.WarehouseCode;
                            x.FromWarehouseUuid = inventory.WarehouseUuid;
                            x.FromInventoryUuid = inventory.InventoryUuid;
                        });
                    }
                }

                var tmpList = data.WarehouseTransferItems.Where(r => r.FromInventoryUuid.IsZero()).Select(r => new { r.SKU, WarehouseCode=r.FromWarehouseCode }).Distinct().ToList();
                foreach (var tuple in tmpList)
                {
                    //only for update items and init InventoryUuid;
                    var inventory = dbFactory.GetBy<Inventory>("where SKU=@0 AND WarehouseCode=@1", tuple.SKU.ToParameter("SKU"), tuple.WarehouseCode.ToParameter("WarehouseCode"));
                    if (inventory != null)
                    {
                        var items = data.WarehouseTransferItems.Where(i => i.FromWarehouseCode == inventory.WarehouseCode && i.SKU == inventory.SKU).ToList();
                        items.ForEach(x =>
                        {
                            x.SKU = inventory.SKU;
                            x.ProductUuid = inventory.ProductUuid;
                            x.FromWarehouseCode = inventory.WarehouseCode;
                            x.FromWarehouseUuid = inventory.WarehouseUuid;
                            x.FromInventoryUuid = inventory.InventoryUuid;
                        });
                    }
                }
                #endregion

                #region To
                inventoryUuidList = data.WarehouseTransferItems.Where(r => !r.ToInventoryUuid.IsZero()).Select(r => r.FromInventoryUuid)
                    .Distinct().ToList();
                productUuidList = new List<(string, string)>();
                using (var trx = new ScopedTransaction(dbFactory))
                {
                    productUuidList = InventoryServiceHelper.GetProductUuidsByInventoryUuids(inventoryUuidList, data.WarehouseTransferHeader.MasterAccountNum,
                        data.WarehouseTransferHeader.ProfileNum);
                }
                foreach (var tuple in productUuidList)
                {
                    var inventory = GetInventory(data, tuple.Item2, tuple.Item1);
                    if (inventory != null)
                    {
                        var items = data.WarehouseTransferItems.Where(i => i.ToInventoryUuid == inventory.InventoryUuid).ToList();
                        items.ForEach(x =>
                        {
                            x.SKU = inventory.SKU;
                            x.ToWarehouseCode = inventory.WarehouseCode;
                            x.ToWarehouseUuid = inventory.WarehouseUuid;
                            x.ToInventoryUuid = inventory.InventoryUuid;
                        });
                    }
                }

                tmpList = data.WarehouseTransferItems.Where(r => r.ToInventoryUuid.IsZero()).Select(r => new { r.SKU, WarehouseCode=r.ToWarehouseCode }).Distinct().ToList();
                foreach (var tuple in tmpList)
                {
                    //only for update items and init InventoryUuid;
                    var inventory = dbFactory.GetBy<Inventory>("where SKU=@0 AND WarehouseCode=@1", tuple.SKU.ToParameter("SKU"), tuple.WarehouseCode.ToParameter("WarehouseCode"));
                    if (inventory != null)
                    {
                        var items = data.WarehouseTransferItems.Where(i => i.ToWarehouseCode == inventory.WarehouseCode && i.SKU == inventory.SKU).ToList();
                        items.ForEach(x =>
                        {
                            x.SKU = inventory.SKU;
                            x.ToWarehouseCode = inventory.WarehouseCode;
                            x.ToWarehouseUuid = inventory.WarehouseUuid;
                            x.ToInventoryUuid = inventory.InventoryUuid;
                        });
                    }
                }
                #endregion
            }
        }

        #region Service Property

        private WarehouseService _warehouseService;
        protected WarehouseService warehouseService => _warehouseService ??= new WarehouseService(dbFactory);

        private InventoryService _inventoryService;
        protected InventoryService inventoryService => _inventoryService ??= new InventoryService(dbFactory);

        #endregion

        #region GetDataWithCache

        public virtual InventoryData GetInventoryData(WarehouseTransferData data, string productUuid)
        {
            return data.GetCache(productUuid, () => {
                inventoryService.NewData();
                if (inventoryService.GetDataById(productUuid))
                    return inventoryService.Data;
                return null;
            });
        }

        public virtual Inventory GetInventory(WarehouseTransferData data, string productUuid, string inventoryUuid)
        {
            var inventoryData = GetInventoryData(data, productUuid);
            System.Diagnostics.Debug.WriteLine($"ProductUuid:{productUuid},Data:{JsonConvert.SerializeObject(inventoryData)}");
            return inventoryData == null ? null : inventoryData.Inventory.First(i => i.InventoryUuid == inventoryUuid);
        }

        public virtual WarehouseData GetWarehouseData(WarehouseTransferData data, string warehouseUuid)
        {
            return data.GetCache(warehouseUuid, () => warehouseService.GetDataById(warehouseUuid) ? warehouseService.Data : null);
        }

        #endregion

        public virtual bool SetDefault(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add set default summary data logic
            //This is generated sample code
           var sum = data.WarehouseTransferHeader;
            if (sum.TransferDate.IsZero()) sum.TransferDate = DateTime.UtcNow.Date;
            if (sum.TransferTime.IsZero()) sum.TransferTime = DateTime.UtcNow.TimeOfDay;
            sum.UpdateDateUtc = DateTime.UtcNow;
            if (string.IsNullOrEmpty(sum.BatchNumber)) sum.BatchNumber = NumberGenerate.Generate();
            //UpdateDateUtc
            //EnterBy
            //UpdateBy


            return true;
        }

        public virtual bool SetDefaultDetail(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add set default for detail list logic

            foreach (var item in data.WarehouseTransferItems)
            {
                if (item is null || item.IsEmpty)
                    continue;
                SetDefault(item, data, processingMode);
            }

            return true;
        }

        //TODO: add set default for detail line logic
        protected virtual bool SetDefault(WarehouseTransferItems item, WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (item is null || item.IsEmpty)
                return false;

            var setting = new ERPSetting();
            var sum = data.WarehouseTransferHeader;
            //var prod = data.GetCache<ProductBasic>(ProductId);
            //var inv = data.GetCache<Inventory>(InventoryId);
            //var invCost = new ItemCostClass(inv);
            var invCost = new ItemCostClass(); 
            var prod = GetInventoryData(data, item.ProductUuid);
            var inv = GetInventory(data, item.ProductUuid, item.FromInventoryUuid);
            if (inv != null)
            {
                if (item.ItemDate.IsZero()) item.ItemDate = DateTime.UtcNow.Date;
                if (item.ItemTime.IsZero()) item.ItemTime = DateTime.UtcNow.TimeOfDay;
                item.LotNum = inv.LotNum;
                if (item.Description.IsZero()) item.Description = inv.LotDescription;
                if (item.Notes.IsZero()) item.Notes = inv.Notes;

                item.UOM = inv.UOM;
                item.PackType = inv.PackType;
                item.PackQty = inv.PackQty;
                item.UnitCost = inv.UnitCost;
                item.AvgCost = inv.AvgCost;
                //item.LotCost;
                item.LotInDate = inv.LotInDate;
                item.LotExpDate = inv.LotExpDate;
                item.FromBeforeInstockQty = inv.Instock;
            }
            inv = GetInventory(data, item.ProductUuid, item.ToInventoryUuid);
            if (inv != null)
            {
                item.ToBeforeInstockQty = inv.Instock;
            }

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


        public virtual bool Calculate(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            PrepareData(data);
            CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            return true;
        }

        public virtual bool CalculateSummary(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        public virtual bool CalculateDetail(WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            /* This is generated sample code

            var sum = data.WarehouseTransferHeader;
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
        protected virtual bool CalculateDetail(InvoiceItems item, WarehouseTransferData data, ProcessingMode processingMode = ProcessingMode.Edit)
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




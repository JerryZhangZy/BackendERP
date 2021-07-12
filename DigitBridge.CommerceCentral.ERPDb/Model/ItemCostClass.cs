using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    [Serializable()]
    public class ItemCostClass
    {
        public string ProductUuid { get; set; }
        public string InventoryUuid { get; set; }

        public decimal BaseCost { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal MiscAmount { get; set; }
        public decimal ChargeAndAllowanceAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }

        public decimal UnitCost { get; set; }
        public decimal AvgCost { get; set; }
        public decimal SalesCost { get; set; }
        public decimal Cost { get; set; }

        public decimal Instock { get; set; }
        public decimal Qty { get; set; }

        public ItemCostClass() { }
        public ItemCostClass(string productUuid, string inventoryUuid)
        {
            ProductUuid = productUuid;
            InventoryUuid = inventoryUuid;
        }
        //public ItemCostClass(Inventory inv) : this()
        //{
        //    ProductUuid = inv.ProductUuid;
        //    InventoryUuid = inv.InventoryUuid;

        //    BaseCost = inv.BaseCost;
        //    ShippingAmount = inv.ShippingAmount;
        //    MiscAmount = inv.MiscAmount;
        //    ChargeAndAllowanceAmount = inv.ChargeAndAllowanceAmount;
        //    TaxRate = inv.TaxRate;
        //    TaxAmount = inv.TaxAmount;

        //    UnitCost = inv.UnitCost;
        //    AvgCost = inv.AvgCost;
        //    SalesCost = inv.SalesCost;
        //    Cost = (AvgCost.IsZero()) ? UnitCost : AvgCost;

        //    Instock = inv.Instock;
        //}

        /// <summary>
        /// Calculate UnitCost, and return cost
        /// </summary>
        public ItemCostClass CalculateCost()
        {
            BaseCost = BaseCost.ToCost();
            TaxAmount = (BaseCost * TaxRate).ToCost();
            UnitCost = (BaseCost + TaxAmount + ShippingAmount + MiscAmount + ChargeAndAllowanceAmount).ToCost();
            if (AvgCost.IsZero())
                AvgCost = UnitCost;
            Cost = AvgCost;
            return this;
        }
        /// <summary>
        /// ReCalculate moving avg_cost
        /// </summary>
        public ItemCostClass CalculateAvgCost(ItemCostClass fromCost)
        {
            if (fromCost == null || !InventoryUuid.EqualsIgnoreSpace(fromCost.InventoryUuid)) return CalculateCost();
            if (fromCost.Qty <= 0) return CalculateCost();
            fromCost.CalculateCost();
            if (fromCost.AvgCost <= 0 && fromCost.UnitCost <= 0) return CalculateCost();

            this.CalculateCost();
            var qty = fromCost.Qty;
            var oldAvgCost = this.AvgCost;

            if (this.Instock > 0)
            {
                this.AvgCost = (fromCost.AvgCost * qty + oldAvgCost * this.Instock) / (qty + this.Instock);
            }
            else
            {
                this.AvgCost = fromCost.AvgCost;
            }
            this.AvgCost = this.AvgCost.ToCost();
            this.BaseCost = fromCost.BaseCost;
            this.ShippingAmount = fromCost.ShippingAmount;
            this.MiscAmount = fromCost.MiscAmount;
            this.TaxRate = fromCost.TaxRate;
            this.TaxAmount = fromCost.TaxAmount;

            this.CalculateCost();
            return this;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public class CalculateTestHelper
    {
        /// <summary>
        /// All item could be reset then write manual calculate result 
        /// </summary>
        /// <returns></returns>
        public static dynamic GetFreeItem()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0.3m,
                DiscountAmount = 0,
                ShipQty = 3,
                Price = 100,
                TaxRate = 1.5m,
                ShippingAmount = 10,
                MiscAmount = 10,
                ChargeAndAllowanceAmount = 10,
                //IsAr = true,
                //Taxable = false,
                //Costable = true,
                //IsProfit = true,
            };
            var result = new
            {
                ItemTotalAmount = 120, //100*0.3*3+10+10+10,
                ExtAmount = 90,//100*0.3*3
                TaxableAmount = 0
            };
            return new { Item = item, Result = result };
        }
        public static dynamic GetItem1()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0.1m, // make sure it is not zero.
                DiscountAmount = 100,// This item doesn't work.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.1m,// make sure it is not zero.
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
            };
            var result = new
            {
                // calculate manually  
                //formula:Price*DiscountRate*ShipQty + Price*DiscountRate*ShipQty*TaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount =1000 * 0.7m * 100 + 1000 * 0.7m * 100 * 0.2m + 100 + 100 + 10,
                ItemTotalAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty + item.Price * item.DiscountRate * item.ShipQty * item.TaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty,//1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
                TaxableAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty,//1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
            };
            return new { Item = item, Result = result };
        }
        // case DiscountAmount is not zero, and DiscountRate is zero.
        public static dynamic GetItem2()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 200,
                Price = 2000,
                TaxRate = 0.2m,// make sure it is not zero
                ShippingAmount = 200,
                MiscAmount = 200,
                ChargeAndAllowanceAmount = 20,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false 

            };
            var result = new
            {
                // calculate manually  
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case TaxRate is zero. and DiscountRate is not zero.
        public static dynamic GetItem3(decimal sumTaxRate)
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0.3m, // make sure it is not zero.
                DiscountAmount = 300,// This item doesn't work.
                ShipQty = 300,
                Price = 3000,
                TaxRate = 0,//editable:false; make sure it is zero.
                ShippingAmount = 300,
                MiscAmount = 300,
                ChargeAndAllowanceAmount = 30,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false  
            };
            var result = new
            {
                // calculate manually  
                ItemTotalAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty + item.Price * item.DiscountRate.ToRate() * item.ShipQty * sumTaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty,//(item.Price * item.DiscountRate * item.ShipQty)
                TaxableAmount = item.Price * item.DiscountRate.ToRate() * item.ShipQty,//(item.Price * item.DiscountRate * item.ShipQty)
            };
            return new { Item = item, Result = result };
        }
        //case TaxRate is zero. and DiscountRate is zero. DiscountAmount is not zero.
        public static dynamic GetItem4(decimal sumTaxRate)
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 400,//This item works.
                ShipQty = 400,
                Price = 4000,
                TaxRate = 0m,//editable:false, make sure it is zero
                ShippingAmount = 400,
                MiscAmount = 400,
                ChargeAndAllowanceAmount = 40,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false 
            };
            var result = new
            {
                // calculate manually  
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * sumTaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsAr is false.DiscountRate is zero.
        public static dynamic GetItem5()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 500,//This item works.
                ShipQty = 500,
                Price = 5000,
                TaxRate = 0.5m,//make sure it is not zero
                ShippingAmount = 500,
                MiscAmount = 500,
                ChargeAndAllowanceAmount = 50,
                //IsAr = false,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*item.TaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //ExtAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = 0,//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsAr is false, Taxable is false,DiscountRate is zero.
        public static dynamic GetItem6()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 600,//This item works.
                ShipQty = 600,
                Price = 6000,
                TaxRate = 0.6m,//make sure it is not zero
                ShippingAmount = 600,
                MiscAmount = 600,
                ChargeAndAllowanceAmount = 60,
                //IsAr = false,//editable:false
                //Taxable = false,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //ExtAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = 0,//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case Taxable is false.DiscountRate is zero.
        public static dynamic GetItem7()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 10,//This item works.
                ShipQty = 10,
                Price = 3,
                TaxRate = 0.7m,//make sure it is not zero
                ShippingAmount = 11,
                MiscAmount = 11,
                ChargeAndAllowanceAmount = 11,
                //IsAr = true,//editable:false
                //Taxable = false,//editable:false
                //Costable = true,//editable:false
                //IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount)   + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //ExtAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }

        //case Costable is false.DiscountRate is zero.
        public static dynamic GetItem8()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 800,//This item works.
                ShipQty = 800,
                Price = 8000,
                TaxRate = 0.8m,//make sure it is not zero
                ShippingAmount = 800,
                MiscAmount = 800,
                ChargeAndAllowanceAmount = 80,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = false,//editable:false
                //IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*item.TaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //ExtAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsProfit is false.DiscountRate is zero.
        public static dynamic GetItem9()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 900,//This item works.
                ShipQty = 900,
                Price = 9000,
                TaxRate = 0.9m,//make sure it is not zero
                ShippingAmount = 900,
                MiscAmount = 900,
                ChargeAndAllowanceAmount = 90,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = false,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*item.TaxRate.ToRate() + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //ExtAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate.ToRate() + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case DiscountRate is zero,DiscountAmount is zero.
        public static dynamic GetItem10()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 0,//editable:false  
                ShipQty = 1000,
                Price = 10000,
                TaxRate = 1m,//make sure it is not zero
                ShippingAmount = 1000,
                MiscAmount = 1000,
                ChargeAndAllowanceAmount = 100,
                //IsAr = true,//editable:false
                //Taxable = true,//editable:false
                //Costable = true,//editable:false
                //IsProfit = false,//editable:false

            };
            var result = new
            {
                ItemTotalAmount = (item.Price * item.ShipQty) + ((item.Price * item.ShipQty) * item.TaxRate.ToRate()) + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                ExtAmount = (item.Price * item.ShipQty),
                TaxableAmount = (item.Price * item.ShipQty),
            };
            return new { Item = item, Result = result };
        }
         
        public static dynamic GetAllItemCases(decimal sumTaxRate)
        {
            return new[] {
            GetItem1(),
            GetItem2(),
            GetItem3(sumTaxRate),
            GetItem4(sumTaxRate),
            GetItem5(),
            GetItem6(),
            GetItem7(),
            GetItem8(),
            GetItem9(),
            GetItem10(),
            GetFreeItem()
            };
        }

        public static dynamic GetExpectedData(dynamic sum, dynamic items)
        {
            decimal subTotalAmount = 0m;
            decimal taxableAmount = 0m;
            decimal itemTotalAmount = 0m;

            foreach (var item in items)
            {
                subTotalAmount += item.Result.ExtAmount;
                taxableAmount += item.Result.TaxableAmount;
                itemTotalAmount += item.Result.ItemTotalAmount;
            }
            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount + taxableAmount * sum.TaxRate.ToRate() + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items, ItemTotalAmount = itemTotalAmount, SubTotalAmount = subTotalAmount, TaxableAmount = taxableAmount };

        }

        /// <summary>
        /// 10 item cases and sum case (DiscountRate is not zero.DiscountAmount is zero.  TaxForShippingAndHandling is false)
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSumCase1()
        {
            var sum = new
            {
                MiscTaxAmount = 100,
                ShippingTaxAmount = 100,
                TaxRate = 0.1m,
                DiscountRate = 0,//make sure it is not zero.
                DiscountAmount = 0,
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 100,
                //sum.PaidAmount = 100;
                //sum.CreditAmount = 100;
            };

            decimal subTotalAmount = 0m;
            decimal taxableAmount = 0m;
            var items = GetAllItemCases(sum.TaxRate);
            foreach (var item in items)
            {
                subTotalAmount += item.Result.ExtAmount;
                taxableAmount += item.Result.TaxableAmount;
            }

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount + taxableAmount * sum.TaxRate.ToRate() + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items, SubTotalAmount = subTotalAmount, TaxableAmount = taxableAmount };
        }

        /// <summary>
        /// 10 item cases and sum case （DiscountRate is not zero.  TaxForShippingAndHandling is false.）
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSumCase2()
        {
            var sum = new
            {
                MiscTaxAmount = 100,
                ShippingTaxAmount = 100,
                TaxRate = 0.1m,
                DiscountRate = 0.9m,//make sure it is not zero.
                DiscountAmount = 10000,
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 100,
                //sum.PaidAmount = 100;
                //sum.CreditAmount = 100;
            };

            decimal subTotalAmount = 0m;
            decimal taxableAmount = 0m;
            var items = GetAllItemCases(sum.TaxRate);
            foreach (var item in items)
            {
                //ItemTotalAmount
                subTotalAmount += item.Result.ExtAmount;
                taxableAmount += item.Result.TaxableAmount;
            }

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount * (1 - sum.DiscountRate.ToRate()) + taxableAmount * (1 - sum.DiscountRate.ToRate()) * sum.TaxRate.ToRate() + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items };
        }

        /// <summary>
        /// 10 item cases and sum case( DiscountRate is zero. DiscountAmount is not zero. TaxForShippingAndHandling is false).
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSumCase3()
        {
            var sum = new
            {
                MiscTaxAmount = 100,
                ShippingTaxAmount = 100,
                TaxRate = 0.1m,
                DiscountRate = 0,//editable:false make sure it is zero.
                DiscountAmount = 10000,
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 100,
                //sum.PaidAmount = 100;
                //sum.CreditAmount = 100;
            };

            decimal subTotalAmount = 0m;
            decimal taxableAmount = 0m;
            var items = GetAllItemCases(sum.TaxRate);
            foreach (var item in items)
            {
                //ItemTotalAmount
                subTotalAmount += item.Result.ExtAmount;
                taxableAmount += item.Result.ExtAmount;
            }
            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = (subTotalAmount - sum.DiscountAmount) + (taxableAmount * (1 - sum.DiscountAmount / subTotalAmount)) * sum.TaxRate.ToRate() + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items };
        }

        public static void CopySum(dynamic sum, dynamic newSum)
        {
            sum.MiscTaxAmount = newSum.MiscTaxAmount;
            sum.ShippingTaxAmount = newSum.ShippingTaxAmount;
            sum.TaxRate = newSum.TaxRate;
            sum.DiscountRate = newSum.DiscountRate;
            sum.DiscountAmount = newSum.DiscountAmount;
            sum.ShippingAmount = newSum.ShippingAmount;
            sum.MiscAmount = newSum.MiscAmount;
            sum.ChargeAndAllowanceAmount = newSum.ChargeAndAllowanceAmount;
        }
        public static void CopyItem(dynamic item, dynamic newItem)
        {
            item.DiscountRate = newItem.DiscountRate;
            item.DiscountAmount = newItem.DiscountAmount;
            item.ShipQty = newItem.ShipQty;
            item.Price = newItem.Price;
            item.TaxRate = newItem.TaxRate;
            item.ShippingAmount = newItem.ShippingAmount;
            item.MiscAmount = newItem.MiscAmount;
            item.ChargeAndAllowanceAmount = newItem.ChargeAndAllowanceAmount;
            item.IsAr = newItem.IsAr;
            item.Taxable = newItem.Taxable;
            item.Costable = newItem.Costable;
            item.IsProfit = newItem.IsProfit;
        }
    }
}

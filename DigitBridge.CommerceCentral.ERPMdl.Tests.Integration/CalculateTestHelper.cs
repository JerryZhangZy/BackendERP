using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public class CalculateTestHelper
    {
        private static dynamic GetItem1()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0.7m, // make sure it is not zero.
                DiscountAmount = 200,// This item doesn't work.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,// make sure it is not zero.
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false 
            };
            var result = new
            {
                // calculate manually  
                //formula:Price*DiscountRate*ShipQty + Price*DiscountRate*ShipQty*TaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount =1000 * 0.7m * 100 + 1000 * 0.7m * 100 * 0.2m + 100 + 100 + 10,
                ItemTotalAmount = item.Price * item.DiscountRate * item.ShipQty + item.Price * item.DiscountRate * item.ShipQty * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = item.Price * item.DiscountRate * item.ShipQty,//1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
                TaxableAmount = item.Price * item.DiscountRate * item.ShipQty,//1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
            };
            return new { Item = item, Result = result };
        }
        // case DiscountAmount is not zero, and DiscountRate is zero.
        private static dynamic GetItem2()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,// make sure it is not zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*TaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case TaxRate is zero. and DiscountRate is not zero.
        private static dynamic GetItem3(decimal sumTaxRate)
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0.7m, // make sure it is not zero.
                DiscountAmount = 200,// This item doesn't work.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0,//editable:false; make sure it is zero.
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false 
            };
            var result = new
            {
                // calculate manually 
                //Price*DiscountRate * ShipQty + Price * DiscountRate*ShipQty * sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = 1000 * 0.7m * 100 + 1000 * 0.7m * 100 * sum.TaxRate + 100 + 100 + 10,
                //SubTotalAmount = 1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
                //TaxableAmount = 1000 * 0.7m * 100,//(item.Price * item.DiscountRate * item.ShipQty)
                ItemTotalAmount = item.Price * item.DiscountRate * item.ShipQty + item.Price * item.DiscountRate * item.ShipQty * sumTaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = item.Price * item.DiscountRate * item.ShipQty,//(item.Price * item.DiscountRate * item.ShipQty)
                TaxableAmount = item.Price * item.DiscountRate * item.ShipQty,//(item.Price * item.DiscountRate * item.ShipQty)
            };
            return new { Item = item, Result = result };
        }
        //case TaxRate is zero. and DiscountRate is zero. DiscountAmount is not zero.
        private static dynamic GetItem4(decimal sumTaxRate)
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0m,//editable:false, make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * sumTaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsAr is false.DiscountRate is zero.
        private static dynamic GetItem5()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = false,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = 0,//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsAr is false, Taxable is false,DiscountRate is zero.
        private static dynamic GetItem6()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = false,//editable:false
                Taxable = false,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = 0,//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case Taxable is false.DiscountRate is zero.
        private static dynamic GetItem7()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = false,//editable:false
                Costable = true,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = 0,//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }

        //case Costable is false.DiscountRate is zero.
        private static dynamic GetItem8()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = false,//editable:false
                IsProfit = true,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case IsProfit is false.DiscountRate is zero.
        private static dynamic GetItem9()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 200,//This item works.
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = false,//editable:false

            };
            var result = new
            {
                // calculate manually 
                //(Price*ShipQty-DiscountAmount) + (Price*ShipQty-DiscountAmount)*sumTaxRate + ShippingAmount + MiscAmount+ ChargeAndAllowanceAmount //setting.TaxForShippingAndHandling ShippingAmount*TaxRate+MiscAmount*TaxRate
                //ItemTotalAmount = (1000 * 100 - 200) + (1000 * 100 - 200) * 0.2m + 100 + 100 + 10,
                //SubTotalAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                //TaxableAmount = (1000 * 100 - 200),//(Price*ShipQty-DiscountAmount)
                ItemTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount) + (item.Price * item.ShipQty - item.DiscountAmount) * item.TaxRate + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
                TaxableAmount = (item.Price * item.ShipQty - item.DiscountAmount),//(Price*ShipQty-DiscountAmount)
            };
            return new { Item = item, Result = result };
        }
        //case DiscountRate is zero,DiscountAmount is zero.
        private static dynamic GetItem10()
        {
            var item = new
            {
                //OrderQty = 1000,
                //CancelledQty = 10,
                DiscountRate = 0, //editable:false,  make sure it is zero
                DiscountAmount = 0,//editable:false  
                ShipQty = 100,
                Price = 1000,
                TaxRate = 0.2m,//make sure it is zero
                ShippingAmount = 100,
                MiscAmount = 100,
                ChargeAndAllowanceAmount = 10,
                IsAr = true,//editable:false
                Taxable = true,//editable:false
                Costable = true,//editable:false
                IsProfit = false,//editable:false

            };
            var result = new
            {
                ItemTotalAmount = (item.Price * item.ShipQty) + ((item.Price * item.ShipQty) * item.TaxRate) + item.ShippingAmount + item.MiscAmount + item.ChargeAndAllowanceAmount,
                SubTotalAmount = (item.Price * item.ShipQty),
                TaxableAmount = (item.Price * item.ShipQty),
            };
            return new { Item = item, Result = result };
        }

        // case DiscountRate is not zero.DiscountAmount is zero.  TaxForShippingAndHandling is false.
        private static dynamic GetSum1(decimal subTotalAmount, decimal taxableAmount)
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

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount + taxableAmount * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount };
        }

        // case DiscountRate is not zero.  TaxForShippingAndHandling is false.
        private static dynamic GetSum2(decimal subTotalAmount, decimal taxableAmount)
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

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount * (1 - sum.DiscountRate) + taxableAmount * (1 - sum.DiscountRate) * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount };

        }
        // case DiscountRate is zero. DiscountAmount is not zero. TaxForShippingAndHandling is false.
        private static dynamic GetSum3(decimal subTotalAmount, decimal taxableAmount)
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
            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = (subTotalAmount - sum.DiscountAmount) + (taxableAmount * (1 - sum.DiscountAmount / subTotalAmount)) * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount };
        }

        public static dynamic GetItems(decimal sumTaxRate)
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
            GetItem10()
            };
        }
        /// <summary>
        /// 10 item cases and sum case (DiscountRate is not zero.DiscountAmount is zero.  TaxForShippingAndHandling is false)
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSum1()
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
            decimal itemTotalAmount = 0m;
            var items = GetItems(sum.TaxRate);
            foreach (var item in items)
            {
                //ItemTotalAmount
                subTotalAmount += item.Result.SubTotalAmount;
                taxableAmount += item.Result.SubTotalAmount;
                itemTotalAmount += item.Result.ItemTotalAmount;
            }

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount + taxableAmount * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items, ItemTotalAmount = itemTotalAmount };
        }

        /// <summary>
        /// 10 item cases and sum case （DiscountRate is not zero.  TaxForShippingAndHandling is false.）
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSum2()
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
            decimal itemTotalAmount = 0m;
            var items = GetItems(sum.TaxRate);
            foreach (var item in items)
            {
                //ItemTotalAmount
                subTotalAmount += item.Result.SubTotalAmount;
                taxableAmount += item.Result.SubTotalAmount;
                itemTotalAmount += item.Result.ItemTotalAmount;
            }

            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = subTotalAmount * (1 - sum.DiscountRate) + taxableAmount * (1 - sum.DiscountRate) * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items, ItemTotalAmount = itemTotalAmount };
        }

        /// <summary>
        /// 10 item cases and sum case( DiscountRate is zero. DiscountAmount is not zero. TaxForShippingAndHandling is false).
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSum3()
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
            decimal itemTotalAmount = 0m;
            var items = GetItems(sum.TaxRate);
            foreach (var item in items)
            {
                //ItemTotalAmount
                subTotalAmount += item.Result.SubTotalAmount;
                taxableAmount += item.Result.SubTotalAmount;
                itemTotalAmount += item.Result.ItemTotalAmount;
            }  
            //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
            var totalAmount = (subTotalAmount - sum.DiscountAmount) + (taxableAmount * (1 - sum.DiscountAmount / subTotalAmount)) * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount;
            return new { Sum = sum, TotalAmount = totalAmount, Items = items, ItemTotalAmount = itemTotalAmount };
        } 
        //// case DiscountRate is not zero.  TaxForShippingAndHandling is true.
        //public void GetSum2(decimal subTotalAmount, decimal taxableAmount)
        //{
        //    var sum = new
        //    {
        //        MiscTaxAmount = 100,
        //        ShippingTaxAmount = 100,
        //        TaxRate = 0.1m,
        //        DiscountRate = 0.9m,//editable:false make sure it is zero.
        //        DiscountAmount = 10000,
        //        ShippingAmount = 100,
        //        MiscAmount = 100,
        //        ChargeAndAllowanceAmount = 100,
        //        //sum.PaidAmount = 100;
        //        //sum.CreditAmount = 100;
        //    };
        //    var result = new
        //    {
        //        //extamount + taxamount + ShippingAmount +  MiscAmount + ChargeAndAllowanceAmount
        //        TotalAmount = subTotalAmount * (1 - sum.DiscountRate) + taxableAmount * (1 - sum.DiscountRate) * sum.TaxRate + sum.ShippingAmount + sum.MiscAmount + sum.ChargeAndAllowanceAmount

        //    };

        //}
    }
}

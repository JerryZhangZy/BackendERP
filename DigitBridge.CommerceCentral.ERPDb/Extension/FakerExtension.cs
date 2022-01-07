using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bogus;
using DigitBridge.Base.Utility;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public static class FakerExtension
    {
        public static string[] UOM = new[] { "EA", "CA", "BX", "LB", "KG", "PR" };
        public static string[] PackType = new[] { "EA", "CA", "BX", "DZ" };
        public static string[] PriceRule = new[] { "1", "2", "3", "4", "5", "6", "7", "8" };
        public static int[] InvoiceItemType = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int[] InvoiceItemStatus = new[] { 1, 2, 3, 9, 100 };
        public static decimal Decimal(this Randomizer r, decimal min = 0.0m, decimal max = 1.0m, int? decimals = null)
        {
            var value = r.Decimal(min, max);
            if (decimals.HasValue)
            {
                return Math.Round(value, decimals.Value);
            }
            return value;
        }

        public static string JsonString(this Randomizer r)
        {
            return r.JObject().ToString(Newtonsoft.Json.Formatting.None);
        }

        public static JObject JObject(this Randomizer r)
        {
            return new JObject()
            {
                { "ClassCode", r.AlphaNumeric(20) },
                { "DepartmentCode", r.AlphaNumeric(20) },
                { "ClassInt", r.Int(1, 100) },
                { "ClassDecimal", r.Decimal(1, 1000, 4) }
            };
        }
    }
}

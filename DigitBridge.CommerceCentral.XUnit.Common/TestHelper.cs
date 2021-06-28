using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.XUnit.Common
{
    public static class TestHelper
    {
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartupTest>();
                });
            //.ConfigureServices(
            //    services => services.AddHostedService<TestServiceBase>()
            //);
        }

        public static string[] UOM = new[] { "EA", "CA", "BX", "LB", "KG", "PR" };
        public static string[] PackType = new[] { "EA", "CA", "BX", "DZ" };
        public static string[] PriceRule = new[] { "1", "2", "3", "4", "5", "6", "7", "8" };
        public static int[] InvoiceItemType = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int[] InvoiceItemStatus = new[] { 1, 2, 3, 9, 100 };

        public static bool ObjectEquals<T>(T obj1, T obj2)
        {
            var json1 = obj1.ObjectToString(true, "yyyy-MM-dd");
            var json2 = obj2.ObjectToString(true, "yyyy-MM-dd");
            return json1.Equals(json2, StringComparison.CurrentCultureIgnoreCase);
        }

    }

    public static class ExtensionsForRandomizer
    {
        public static decimal Decimal(this Randomizer r, decimal min = 0.0m, decimal max = 1.0m, int? decimals = null)
        {
            var value = r.Decimal(min, max);
            if (decimals.HasValue)
            {
                return Math.Round(value, decimals.Value);
            }
            return value;
        }
    }

}

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// ProductFindClass use to find product data.
    /// 1. find by ProductUuid
    /// 2. find by CentralProductNum
    /// 3. find by SKU + MasterAccountNum + ProfileNum
    /// 4. find by UPC + MasterAccountNum + ProfileNum
    /// 5. find by EAN + MasterAccountNum + ProfileNum
    /// 6. find by ASIN + MasterAccountNum + ProfileNum
    /// 7. find by FNSku + MasterAccountNum + ProfileNum
    /// </summary>
    [Serializable]
    public class ProductFindClass
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }

        /// <summary>
        /// input SKU, item1 in SKUTable
        /// </summary>
        public string SKU { get; set; } = string.Empty;
        /// <summary>
        /// input Product Uuid, item2 in SKUTable
        /// </summary>
        public string ProductUuid { get; set; } = string.Empty;
        /// <summary>
        /// input CentralProductNum, item3 in SKUTable
        /// </summary>
        public int CentralProductNum { get; set; } = 0;
        /// <summary>
        /// input UPC, item4 in SKUTable
        /// </summary>
        public string UPC { get; set; } = string.Empty;
        /// <summary>
        /// input EAN, item5 in SKUTable
        /// </summary>
        public string EAN { get; set; } = string.Empty;
        /// <summary>
        /// input ASIN, item6 in SKUTable
        /// </summary>
        public string ASIN { get; set; } = string.Empty;
        /// <summary>
        /// input FNSku, item7 in SKUTable
        /// </summary>
        public string FNSku { get; set; } = string.Empty;

        /// <summary>
        /// Return found SKU
        /// </summary>
        public string FoundSKU { get; set; } = string.Empty;

    }
    public static class ProductFindClassExtensions
    {
        public static IList<StringArray> ToStringArray(this IList<ProductFindClass> lst)
        {
            return (lst == null || lst.Count == 0)
                ? null
                : lst.AsEnumerable().Select((x, index) => new StringArray()
                {
                    Item0 = index.ToString(),
                    Item1 = x.SKU,
                    Item2 = x.ProductUuid,
                    Item3 = x.CentralProductNum.ToString(),
                    Item4 = x.UPC,
                    Item5 = x.EAN,
                    Item6 = x.ASIN,
                    Item7 = x.FNSku,
                }
                ).ToList();
        }
        public static ProductFindClass FindBySku(this IList<ProductFindClass> lst, string sku)
        {
            return (lst == null || string.IsNullOrEmpty(sku))
                ? null
                : lst.AsEnumerable().FirstOrDefault(x => x.SKU.EqualsIgnoreSpace(sku));
        }
    }

}

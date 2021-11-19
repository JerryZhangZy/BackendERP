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
    public class InventoryFindClass
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }

        public string ProductUuid { get; set; }
        public int CentralProductNum { get; set; }
        public string SKU { get; set; }
        public string WarehouseCode { get; set; }
        public string LotNum { get; set; }
        public string LpnNum { get; set; }
    }

    public static class InventoryFindClassExtensions
    {
        public static IList<StringArray> ToStringArray(this IList<InventoryFindClass> lst)
        {
            return (lst == null || lst.Count == 0)
                ? null
                : lst.AsEnumerable().Select(x => new StringArray()
                    {
                        Item0 = x.SKU,
                        Item1 = x.WarehouseCode
                    }
                ).ToList();
        }

        public static InventoryFindClass FindBySkuWarehouse(this IList<InventoryFindClass> lst, string sku, string warehouseCode)
        {
            return (lst == null || string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(warehouseCode))
                ? null
                : lst.AsEnumerable().FirstOrDefault(x => x.SKU.EqualsIgnoreSpace(sku) && x.WarehouseCode.EqualsIgnoreSpace(warehouseCode));
        }

    }

}

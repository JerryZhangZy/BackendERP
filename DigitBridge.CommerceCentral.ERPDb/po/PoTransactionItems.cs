              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class PoTransactionItems
    {
        public virtual async Task<List<PoTransactionItems>> GetPoTransactionItemsItems(List<string> transUuids)
        {
            if (transUuids == null || transUuids.Count == 0) return null;
            var sql = $"SELECT * FROM PoTransactionItems where TransUuid in ('{string.Join("','", transUuids)}') ";
            return (await dbFactory.FindAsync<PoTransactionItems>(sql)).ToList();
        }


        /// <summary>
        /// po item po Qty
        /// </summary>
        public virtual decimal PoQty => (this.Parent.PurchaseOrderData?.PoItems?.FirstOrDefault(i => i.PoItemUuid == this.PoItemUuid)?.PoQty).ToQty();

        /// <summary>
        /// Same po same PoItemUuid total returned qty
        /// </summary>
        public virtual decimal ReceivedQty =>(this.Parent.PurchaseOrderData?.PoItems?.FirstOrDefault(i => i.PoItemUuid == this.PoItemUuid)?.ReceivedQty).ToQty();

        /// <summary>
        /// OpenQty => po item po Qty - po item  total received qty;
        /// </summary>
        public virtual decimal OpenQty => PoQty - ReceivedQty;

        #region 

        /// <summary>
        /// VendorCode
        /// </summary> 
        [JsonIgnore,XmlIgnore]
        public string VendorCode { get; set; }
        /// <summary>
        /// VendorName
        /// </summary> 
        [JsonIgnore, XmlIgnore]
        public string VendorName { get; set; }
        /// <summary>
        /// VendorUuid
        /// </summary> 
        [JsonIgnore, XmlIgnore]
        public string VendorUuid { get; set; }

        #endregion
    }
}




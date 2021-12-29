              
    

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

        public virtual  decimal GetPoQty(string poItemUuid)
        {
            var sql = $"SELECT PoQty FROM PoItems where PoItemUuid =@0";
            return  dbFactory.Db.ExecuteScalar<decimal>(sql,poItemUuid.ToSqlParameter("@0"));
        }

        /// <summary>
        /// po item po Qty
        /// </summary>
        public virtual decimal PoQty => ( GetPoQty(this.PoItemUuid)).ToQty();

        /// <summary>
        /// Same po same PoItemUuid total returned qty
        /// </summary>
        public virtual decimal ReceivedQty =>(this.Parent.PurchaseOrderData?.PoItems?.FirstOrDefault(i => i.PoItemUuid == this.PoItemUuid)?.ReceivedQty).ToQty();
        /// <summary>
        /// Same po same PoItemUuid total CancelledQty
        /// </summary>
        public virtual decimal CancelledQty => (this.Parent.PurchaseOrderData?.PoItems?.FirstOrDefault(i => i.PoItemUuid == this.PoItemUuid)?.CancelledQty).ToQty();

        /// <summary>
        /// OpenQty => po item po Qty - po item  total received qty;
        /// </summary>
        public virtual decimal OpenQty => PoQty - ReceivedQty-CancelledQty;
    }
}




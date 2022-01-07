              
    

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
        private PoItems poItem;
        public virtual async Task<List<PoTransactionItems>> GetPoTransactionItemsItems(List<string> transUuids)
        {
            if (transUuids == null || transUuids.Count == 0) return null;
            var sql = $"SELECT * FROM PoTransactionItems where TransUuid in ('{string.Join("','", transUuids)}') ";
            return (await dbFactory.FindAsync<PoTransactionItems>(sql)).ToList();
        }

        public virtual  PoItems GetPoItem(string poItemUuid)
        {
            if (poItem != null) return poItem;
            if (poItemUuid == null ) return new PoItems();
            var sql = $"SELECT * FROM PoItems  where PoItemUuid =@0 ";
            var poitems= ( dbFactory.Find<PoItems>(sql, poItemUuid.ToSqlParameter("@0"))).ToList();
            if (poitems == null || poitems.Count == 0) return new PoItems();
            poItem = poitems[0];
            return poItem;
        }

        //public virtual  decimal GetPoQty(string poItemUuid)
        //{
        //    var sql = $"SELECT PoQty FROM PoItems where PoItemUuid =@0";
        //    return  dbFactory.Db.ExecuteScalar<decimal>(sql,poItemUuid.ToSqlParameter("@0"));
        //}

        //public virtual decimal GetReceivedQty(string poItemUuid)
        //{
        //    var sql = $"SELECT ReceivedQty FROM PoItems where PoItemUuid =@0";
        //    return dbFactory.Db.ExecuteScalar<decimal>(sql, poItemUuid.ToSqlParameter("@0"));
        //}

        //public virtual decimal GetCancelledQtyQty(string poItemUuid)
        //{
        //    var sql = $"SELECT CancelledQty FROM PoItems where PoItemUuid =@0";
        //    return dbFactory.Db.ExecuteScalar<decimal>(sql, poItemUuid.ToSqlParameter("@0"));
        //}



        /// <summary>
        /// po item po Qty
        /// </summary>
        public virtual decimal PoQty => ( GetPoItem(this.PoItemUuid).PoQty).ToQty();

        /// <summary>
        /// Same po same PoItemUuid total returned qty
        /// </summary>
        public virtual decimal ReceivedQty => (GetPoItem(this.PoItemUuid).ReceivedQty).ToQty();
        /// <summary>
        /// Same po same PoItemUuid total CancelledQty
        /// </summary>
        public virtual decimal CancelledQty =>(GetPoItem(this.PoItemUuid).CancelledQty).ToQty();

        /// <summary>
        /// OpenQty => po item po Qty - po item  total received qty;
        /// </summary>
        public virtual decimal OpenQty => PoQty - ReceivedQty-CancelledQty;
    }
}




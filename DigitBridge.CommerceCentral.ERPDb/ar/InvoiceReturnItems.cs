



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
    public partial class InvoiceReturnItems
    {
        public virtual async Task<List<InvoiceReturnItems>> GetReturnItems(List<string> transUuids)
        {
            if (transUuids == null || transUuids.Count == 0) return null;
            var sql = $"SELECT * FROM InvoiceReturnItems where TransUuid in ('{string.Join("','", transUuids)}') ";
            return (await dbFactory.FindAsync<InvoiceReturnItems>(sql)).ToList();
        }

        /// <summary>
        /// Get total ReturnedQty for this sku except current item.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetReturnedQty()
        {
            var sql = $"SELECT  ReturnQty FROM InvoiceReturnItems  where InvoiceUuid = '{this.InvoiceUuid}' and SKU = '{this.SKU}' and RowNum<> {this.RowNum}";
            return dbFactory.GetValue<InvoiceReturnItems, decimal?>(sql).ToQty();
        }

        /// <summary>
        /// Invoice item ship Qty
        /// </summary>
        public virtual decimal ShipQty => (this.Parent.InvoiceData?.InvoiceItems?.Where(i => i.SKU == this.SKU).Sum(j => j.ShipQty)).ToQty();

        /// <summary>
        /// Same invoice same SKU total returned qty
        /// </summary>
        public virtual decimal ReturnedQty => GetReturnedQty();

        /// <summary>
        /// OpenQty => Invoice item ship Qty - Invoice item  total returned qty;
        /// </summary>
        public virtual decimal OpenQty => ShipQty - ReturnedQty;
    }

}




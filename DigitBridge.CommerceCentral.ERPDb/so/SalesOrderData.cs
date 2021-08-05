

//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;


namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class SalesOrderData
    {
        #region property
        /// <summary>
        /// Return all nonNull item uuids
        /// </summary>
        [JsonIgnore, XmlIgnore]
        public virtual IList<string> SalesOrderItemsUuids
        {
            get
            {
                var uuids = new List<string>();
                if (this.SalesOrderItems != null && this.SalesOrderItems.Count > 0)
                {
                    uuids = this.SalesOrderItems.Select(i => i.SalesOrderItemsUuid).ToList();
                }
                return uuids;
            }
        }

        #endregion

        /// <summary>
        /// Get row num by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<long?> GetRowNumAsync(string orderNumber)
        {
            return await dbFactory.GetValueAsync<SalesOrderHeader, long?>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
        }
        /// <summary>
        /// Get row num by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual long? GetRowNum(string orderNumber)
        {
            return dbFactory.GetValue<SalesOrderHeader, long?>($"SELECT TOP 1 RowNum FROM SalesOrderHeader where OrderNumber='{orderNumber}'");
        }

        /// <summary>
        /// Return all salesOrderItemsUuids existed in table SalesOrderItems
        /// </summary>
        /// <param name="salesOrderItemsUuids"></param>
        /// <returns></returns>
        public virtual async Task<string> DuplicateItemUuidsAsync()
        {
            if (SalesOrderItemsUuids == null || SalesOrderItemsUuids.Count == 0)
                return null;

            var allDuplicate = SalesOrderItemsUuids.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { SalesOrderItemsUuid = y.Key })
              .ToList();
            if (allDuplicate != null && allDuplicate.Count > 0)
            {
                return allDuplicate.ObjectToString();
            }
            return await dbFactory.GetValueAsync<SalesOrderItems, string>($"SELECT SalesOrderItemsUuid FROM SalesOrderItems where SalesOrderItemsUuid in ('{string.Join("','", SalesOrderItemsUuids)}') for json path ");
        }
        /// <summary>
        /// Return all salesOrderItemsUuids existed in table SalesOrderItems
        /// </summary>
        /// <param name="salesOrderItemsUuids"></param>
        /// <returns></returns>
        public virtual string DuplicateItemUuids()
        {
            if (SalesOrderItemsUuids == null || SalesOrderItemsUuids.Count == 0)
                return null;
            var allDuplicate = SalesOrderItemsUuids.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { SalesOrderItemsUuid = y.Key })
              .ToList();
            if (allDuplicate != null && allDuplicate.Count > 0)
            {
                return allDuplicate.ObjectToString();
            }
            return dbFactory.GetValue<SalesOrderItems, string>($"SELECT SalesOrderItemsUuid FROM SalesOrderItems where SalesOrderItemsUuid in ('{string.Join("','", SalesOrderItemsUuids)}') for json path ");
        }
    }
}




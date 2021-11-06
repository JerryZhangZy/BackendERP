

//-------------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;

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
        public virtual long? GetRowNum(string orderNumber, int masterAccountNum, int profileNum)
        {
            var sql = @"
SELECT TOP 1 RowNum FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",orderNumber)
            };

            return dbFactory.GetValue<SalesOrderHeader, long?>(sql, paras);
        }

        /// <summary>
        /// Get row num by order number
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public virtual async Task<long?> GetRowNumAsync(string orderNumber, int masterAccountNum, int profileNum)
        {
            var sql = @"
SELECT TOP 1 RowNum FROM SalesOrderHeader tbl
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",orderNumber)
            };

            return await dbFactory.GetValueAsync<SalesOrderHeader, long?>(sql, paras);
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

        public override bool GetByNumber(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM SalesOrderHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            var obj = dbFactory.GetBy<SalesOrderHeader>(sql, paras);
            if (obj is null) return false;
            SalesOrderHeader = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number)
        {
            var sql = @"
SELECT TOP 1 * FROM SalesOrderHeader 
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND OrderNumber = @2";
            var paras = new SqlParameter[]
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            }; 
            var obj = await dbFactory.GetByAsync<SalesOrderHeader>(sql, paras);
            if (obj is null) return false;
            SalesOrderHeader = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }

        public SalesOrderItems GetNewSalesOrderItems()
        {
            if (this.SalesOrderItems == null)
                this.SalesOrderItems = new List<SalesOrderItems>();
            var salesOrderItemData = SetDefaultSalesOrderItems(this.NewSalesOrderItems());
            AddSalesOrderItems(salesOrderItemData);
            return salesOrderItemData;
        }

        public SalesOrderItems SetDefaultSalesOrderItems(SalesOrderItems item)
        {
            item.RowNum = 0;
            item.IsAr = true;
            item.Costable = true;
            item.IsProfit = true;
            item.Stockable = true;
            return item;
        }

    }
}




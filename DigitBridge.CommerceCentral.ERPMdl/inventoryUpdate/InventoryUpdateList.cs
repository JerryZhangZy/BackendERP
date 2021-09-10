//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.Base.Utility.Enums;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.InventoryUpdateHeaderHelper;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.InventoryUpdateItemsHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryUpdateList : SqlQueryBuilder<InventoryUpdateQuery>
    {
        public InventoryUpdateList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InventoryUpdateList(IDataBaseFactory dbFactory, InventoryUpdateQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }
        
        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.BatchNumber()},
{Helper.InventoryUpdateUuid()},
{Helper.InventoryUpdateType()},
COALESCE(iut.text, '') inventoryUpdateTypeText, 
{ItemsHelper.InventoryUpdateItemsUuid()},
{ItemsHelper.Seq()},
{ItemsHelper.ItemDate()},
{ItemsHelper.ItemTime()},
{ItemsHelper.SKU()},
{ItemsHelper.ProductUuid()},
{ItemsHelper.WarehouseCode()},
{ItemsHelper.WarehouseUuid()},
{ItemsHelper.LotNum()},
{ItemsHelper.BeforeInstockQty()},
{ItemsHelper.CountPack()},
{ItemsHelper.UpdateQty()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN {ItemsHelper.TableName} {ItemsHelper.TableAllies} ON ({ItemsHelper.TableAllies}.InventoryUpdateUuid = {Helper.TableAllies}.InventoryUpdateUuid)
 LEFT JOIN @UpdateType iut ON ({Helper.TableAllies}.InventoryUpdateType = iut.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();

            //paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            paramList.Add("@UpdateType".ToEnumParameter<InventoryUpdateType>());

            return paramList.ToArray();
        }
        
        #endregion override methods
        
        public virtual InventoryUpdatePayload GetInventoryUpdateList(InventoryUpdatePayload payload)
        {
            if (payload == null)
                payload = new InventoryUpdatePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryUpdateListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.InventoryUpdateList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryUpdateListCount = 0;
                payload.InventoryUpdateList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<InventoryUpdatePayload> GetInventoryUpdateListAsync(InventoryUpdatePayload payload)
        {
            if (payload == null)
                payload = new InventoryUpdatePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryUpdateListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.InventoryUpdateList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryUpdateListCount = 0;
                payload.InventoryUpdateList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(InventoryUpdatePayload payload)
        {
            if (payload == null)
                payload = new InventoryUpdatePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = await SqlQuery.ExecuteAsync(
                        sql,
                        (long rowNum) => rowNum,
                        GetSqlParameters().ToArray()
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        public virtual IList<long> GetRowNumList(InventoryUpdatePayload payload)
        {
            if (payload == null)
                payload = new InventoryUpdatePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    rowNumList = SqlQuery.Execute(
                        sql,
                        (long rowNum) => rowNum,
                        GetSqlParameters().ToArray()
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }
    }
}

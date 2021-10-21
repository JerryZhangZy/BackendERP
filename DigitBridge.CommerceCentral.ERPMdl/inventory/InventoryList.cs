using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.ProductBasicHelper;
using ExHelper = DigitBridge.CommerceCentral.ERPDb.ProductExtHelper;
using InvHelper = DigitBridge.CommerceCentral.ERPDb.InventoryHelper;
using System.Data;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryList : SqlQueryBuilder<InventoryQuery>
    {
        public InventoryList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InventoryList(IDataBaseFactory dbFactory, InventoryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.ProductUuid()}, 
{Helper.SKU()}, 
{Helper.FNSku()}, 
{Helper.Condition()}, 
{Helper.Brand()}, 
{Helper.Manufacturer()}, 
{Helper.ProductTitle()}, 
{Helper.Subtitle()}, 
{Helper.ASIN()}, 
{Helper.UPC()}, 
{Helper.Price()}, 
{Helper.MSRP()}, 
{Helper.BundleType()}, 
{Helper.ProductType()}, 
{ExHelper.ClassCode()},
{ExHelper.SubClassCode()},
{ExHelper.DepartmentCode()},
{ExHelper.DivisionCode()},
{ExHelper.OEMCode()},
{ExHelper.AlternateCode()},
{ExHelper.Remark()},
{ExHelper.Model()},
{ExHelper.CategoryCode()},
{ExHelper.GroupCode()},
{ExHelper.SubGroupCode()},
{ExHelper.PriceRule()},
{ExHelper.UOM()},
{ExHelper.QtyPerCase()},
{ExHelper.SalesCost()},
{InvHelper.ColorPatternCode()},
{InvHelper.WarehouseCode()},
{InvHelper.InventoryUuid()},
{InvHelper.Instock()},
{InvHelper.AvaQty()}
";
            return this.SQL_Select;
        }
        
        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
LEFT JOIN {ExHelper.TableName} {ExHelper.TableAllies} ON ({Helper.TableAllies}.ProductUuid = {ExHelper.TableAllies}.ProductUuid)
LEFT JOIN {InvHelper.TableName} {InvHelper.TableAllies} ON ({Helper.TableAllies}.ProductUuid = {InvHelper.TableAllies}.ProductUuid)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            //paramList.Add("@SalesOrderStatus".ToEnumParameter<SalesOrderStatus>());
            //paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

            return paramList.ToArray();
        
        }

        #endregion override methods

        public virtual InventoryPayload GetProductList(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.InventoryList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryListCount = 0;
                payload.InventoryList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<InventoryPayload> GetIProductListAsync(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.InventoryListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.InventoryList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryListCount = 0;
                payload.InventoryList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.CentralProductNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.CentralProductNum 
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

        public virtual IList<long> GetRowNumList(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.CentralProductNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.CentralProductNum 
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

        private string GetExportSql()
        {
            var sql = $@"
SELECT prd.*,
(SELECT * FROM ProductExt prdx WHERE prd.ProductUuid=prdx.ProductUuid FOR JSON PATH) AS ProductExt,
(SELECT * FROM ProductExtAttributes prda WHERE prd.ProductUuid=prda.ProductUuid FOR JSON PATH) AS ProductExtAttributes,
(SELECT * FROM Inventory inv WHERE prd.ProductUuid=inv.ProductUuid FOR JSON PATH) AS Inventory,
(SELECT * FROM InventoryAttributes inva WHERE prd.ProductUuid=inva.ProductUuid FOR JSON PATH) AS InventoryAttributes
FROM ProductBasic prd
";
            return sql;
        }

        private string GetExportCommandText(InventoryPayload payload)
        {
            this.LoadRequestParameter(payload);
            return $@"
{GetExportSql()}
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.CentralProductNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
FOR JSON PATH
";
        }

        public virtual void GetExportJsonList(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.InventoryListCount = Count();
                StringBuilder sb = new StringBuilder();
                var result = ExcuteJson(sb, sql, GetSqlParameters().ToArray());
                if (result)
                    payload.InventoryDataList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryDataList = null;
                throw;
            }
        }

        public virtual async Task GetExportJsonListAsync(InventoryPayload payload)
        {
            if (payload == null)
                payload = new InventoryPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.InventoryListCount = await CountAsync();
                StringBuilder sb = new StringBuilder();
                var result = await ExcuteJsonAsync(sb,sql, GetSqlParameters().ToArray());
                if (result)
                    payload.InventoryDataList = sb;
            }
            catch (Exception ex)
            {
                payload.InventoryDataList = null;
                throw;
            }
        }

    }
}

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

        protected bool QueryRowNums = false;

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
        
        protected virtual string GetSQL_select_RowNum()
        {
            this.SQL_Select = $@"
SELECT distinct 
 {ProductBasicHelper.TableAllies}.RowNum 
";
            return this.SQL_Select;
        }

        public override void GetSQL_all()
        {
            if (QueryRowNums)
            {
                QueryObject.LoadJson = false;
                this.GetSQL_where();
                this.GetSQL_select_RowNum();
                this.GetSQL_from();
                this.SQL_WithoutOrder = $"{this.SQL_Select} {this.SQL_From} {this.SQL_Where} ";
                // set default order by
                this.AddDefaultOrderBy();
                this.GetSQL_orderBy();
            }
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

            QueryRowNums = false;
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
                payload.InventoryListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
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

            QueryRowNums = true;
            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            try
            {
                var reader = Excute();
                if (reader.data != null)
                {
                    foreach (var x in reader.data)
                    {
                        rowNumList.Add(x[0].ToLong());
                    }
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

            QueryRowNums = true;
            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            try
            {
                var reader = Excute();
                if (reader.data != null)
                {
                    foreach(var x in reader.data)
                    {
                        rowNumList.Add(x[0].ToLong());
                    }
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

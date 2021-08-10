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
{Helper.SelectAll(Helper.TableAllies)}, 
{ExHelper.ColorPatternCode()},
{ExHelper.SizeType()},
{ExHelper.SizeCode()},
{ExHelper.WidthCode()},
{ExHelper.LengthCode()},
{ExHelper.ClassCode()},
{ExHelper.SubClassCode()},
{ExHelper.DepartmentCode()},
{ExHelper.DivisionCode()},
{ExHelper.OEMCode()},
{ExHelper.AlternateCode()},
{ExHelper.Remark()},
{ExHelper.Model()},
{ExHelper.CatalogPage()},
{ExHelper.CategoryCode()},
{ExHelper.GroupCode()},
{ExHelper.SubGroupCode()},
{ExHelper.PriceRule()},
{ExHelper.Stockable()},
{ExHelper.Release()},
{ExHelper.Currency()},
{ExHelper.UOM()},
{ExHelper.QtyPerPallot()},
{ExHelper.QtyPerBox()},
{ExHelper.QtyPerCase()},
{ExHelper.PackQty()},
{ExHelper.PoSize()},
{ExHelper.MinStock()},
{ExHelper.SalesCost()},
{ExHelper.LeadTimeDay()},
{ExHelper.ProductYear()},
{ExHelper.DigitBridgeGuid()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
LEFT JOIN {ExHelper.TableName} {ExHelper.TableAllies} ON ({Helper.TableAllies}.ProductUuid = {ExHelper.TableAllies}.ProductUuid)
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

        public virtual ProductExPayload GetProductList(ProductExPayload payload)
        {
            if (payload == null)
                payload = new ProductExPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.ProductListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.ProductList = sb;
            }
            catch (Exception ex)
            {
                payload.ProductListCount = 0;
                payload.ProductList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<ProductExPayload> GetIProductListAsync(ProductExPayload payload)
        {
            if (payload == null)
                payload = new ProductExPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.ProductListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.ProductList = sb;
            }
            catch (Exception ex)
            {
                payload.ProductListCount = 0;
                payload.ProductList = null;
                return payload;
                throw;
            }
            return payload;
        }

    }
}

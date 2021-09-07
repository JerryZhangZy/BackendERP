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
using Helper = DigitBridge.CommerceCentral.ERPDb.DistributionCenterHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WarehouseList : SqlQueryBuilder<WarehouseQuery>
    {
        public WarehouseList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public WarehouseList(IDataBaseFactory dbFactory, WarehouseQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.DistributionCenterNum()},
{Helper.ProfileNum()},
{Helper.DistributionCenterName()},
{Helper.DistributionCenterCode()},
{Helper.DistributionCenterType()},
{Helper.DistributionCenterUuid()},
{Helper.Status()},
{Helper.DefaultLevel()},
{Helper.AddressLine1()},
{Helper.AddressLine2()},
{Helper.City()},
{Helper.State()},
{Helper.ZipCode()},
{Helper.CompanyName()},
{Helper.ContactName()},
{Helper.ContactEmail()},
{Helper.ContactPhone()},
{Helper.MainPhone()},
{Helper.Fax()},
{Helper.Website()},
{Helper.Email()},
{Helper.BusinessHours()},
{Helper.Notes()},
{Helper.Priority()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
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

        public virtual WarehousePayload GetWarehouseList(WarehousePayload payload)
        {
            if (payload == null)
                payload = new WarehousePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.WarehouseListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.WarehouseList = sb;
            }
            catch (Exception ex)
            {
                payload.WarehouseListCount = 0;
                payload.WarehouseList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<WarehousePayload> GetIWarehouseListAsync(WarehousePayload payload)
        {
            if (payload == null)
                payload = new WarehousePayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.WarehouseListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.WarehouseList = sb;
            }
            catch (Exception ex)
            {
                payload.WarehouseListCount = 0;
                payload.WarehouseList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(WarehousePayload payload)
        {
            if (payload == null)
                payload = new WarehousePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {Helper.TableAllies}.DistributionCenterNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.DistributionCenterNum
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using var trs = new ScopedTransaction(dbFactory);
                rowNumList = await SqlQuery.ExecuteAsync(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

        public virtual IList<long> GetRowNumList(WarehousePayload payload)
        {
            if (payload == null)
                payload = new WarehousePayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {Helper.TableAllies}.DistributionCenterNum 
{GetSQL_from()} 
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.DistributionCenterNum
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
";
            try
            {
                using var trs = new ScopedTransaction(dbFactory);
                rowNumList = SqlQuery.Execute(
                    sql,
                    (long rowNum) => rowNum,
                    GetSqlParameters().ToArray()
                );
            }
            catch (Exception ex)
            {
                throw;
            }
            return rowNumList;
        }

    }
}

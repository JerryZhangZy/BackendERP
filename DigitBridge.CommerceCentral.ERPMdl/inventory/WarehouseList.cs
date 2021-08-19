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

        protected bool QueryRowNums = false;

        protected virtual string GetSQL_select_RowNum()
        {
            this.SQL_Select = $@"
SELECT distinct 
 {Helper.TableAllies}.DistributionCenterNum,
 {Helper.TableAllies}.RowNum
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
                payload.WarehouseListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
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

            QueryRowNums = true;
            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            try
            {
                var reader = await ExcuteAsync();
                if (reader.data != null)
                {
                    foreach (var x in reader.data)
                    {
                        rowNumList.Add(x[0].ToInt());
                    }
                }
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
                        rowNumList.Add(x[0].ToInt());
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

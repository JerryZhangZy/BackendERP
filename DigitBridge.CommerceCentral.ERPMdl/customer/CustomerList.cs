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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CustomerList : SqlQueryBuilder<CustomerQuery>
    {
        public CustomerList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public CustomerList(IDataBaseFactory dbFactory, CustomerQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{ERPDb.CustomerHelper.SelectAll(ERPDb.CustomerHelper.TableAllies)}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {ERPDb.CustomerHelper.TableName} {ERPDb.CustomerHelper.TableAllies} 
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

        public virtual CustomerPayload GetCustomerList(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.CustomerListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.CustomerList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerListCount = 0;
                payload.CustomerList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<CustomerPayload> GetCustomerListAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.CustomerListCount = await CountAsync().ConfigureAwait(false);
                result = await ExcuteJsonAsync(sb).ConfigureAwait(false);
                if (result)
                    payload.CustomerList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerListCount = 0;
                payload.CustomerList = null;
                return payload;
                throw;
            }
            return payload;
        }

    }
}

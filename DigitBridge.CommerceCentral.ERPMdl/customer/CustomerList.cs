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
using Helper = DigitBridge.CommerceCentral.ERPDb.CustomerHelper;
using AdrHelper = DigitBridge.CommerceCentral.ERPDb.CustomerAddressHelper;

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
{Helper.Digit_seller_id()}, 
{Helper.RowNum()}, 
{Helper.CustomerUuid()}, 
{Helper.CustomerCode()}, 
{Helper.CustomerName()}, 
{Helper.Contact()}, 
{Helper.Contact2()}, 
{Helper.Contact3()}, 
{Helper.Phone1()}, 
{Helper.Phone2()}, 
{Helper.Phone3()}, 
{Helper.Phone4()}, 
{Helper.Email()}, 
{Helper.WebSite()}, 
{Helper.CustomerType()}, 
{Helper.CustomerStatus()}, 
COALESCE(st.text, '') customerStatusText, 
{Helper.BusinessType()}, 
{Helper.PriceRule()}, 
{Helper.FirstDate()}, 
{Helper.Currency()}, 
{Helper.CreditLimit()}, 
{Helper.TaxRate()}, 
{Helper.DiscountRate()}, 
{Helper.ShippingCarrier()}, 
{Helper.ShippingClass()}, 
{Helper.ShippingAccount()}, 
{Helper.Priority()}, 
{Helper.Area()}, 
{Helper.Region()}, 
{Helper.Districtn()}, 
{Helper.Zone()}, 
{Helper.TaxId()}, 
{Helper.ResaleLicense()}, 
{Helper.ClassCode()}, 
{Helper.DepartmentCode()}, 
{Helper.DivisionCode()}, 
{Helper.SourceCode()}, 
{Helper.Terms()}, 
{Helper.TermsDays()},
{AdrHelper.AddressCode()},
{AdrHelper.AddressType()},
{AdrHelper.Description()},
{AdrHelper.Name()},
{AdrHelper.Company()},
{AdrHelper.Attention()},
{AdrHelper.City()},
{AdrHelper.State()},
{AdrHelper.PostalCode()},
{AdrHelper.Country()},
{AdrHelper.DaytimePhone()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
FROM {Helper.TableName} {Helper.TableAllies} 
OUTER APPLY ( 
    SELECT TOP 1 i.* FROM {AdrHelper.TableName} i WHERE ({Helper.TableAllies}.CustomerUuid = i.CustomerUuid AND i.AddressCode = 'BILL')
) {AdrHelper.TableAllies}
LEFT JOIN @CustomerStatus st ON ({Helper.TableAllies}.CustomerStatus = st.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@CustomerStatus".ToEnumParameter<CustomerStatus>());
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

        public virtual async Task<IList<long>> GetRowNumListAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();

            var sql = $@"
SELECT distinct {CustomerHelper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
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

        public virtual IList<long> GetRowNumList(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            var rowNumList = new List<long>();
            var sql = $@"
SELECT distinct {CustomerHelper.TableAllies}.RowNum 
{GetSQL_from()} 
{GetSQL_where()}
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

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
{CustomerHelper.Digit_seller_id()}, 
{CustomerHelper.CustomerUuid()}, 
{CustomerHelper.CustomerCode()}, 
{CustomerHelper.CustomerName()}, 
{CustomerHelper.Contact()}, 
{CustomerHelper.Contact2()}, 
{CustomerHelper.Contact3()}, 
{CustomerHelper.Phone1()}, 
{CustomerHelper.Phone2()}, 
{CustomerHelper.Phone3()}, 
{CustomerHelper.Phone4()}, 
{CustomerHelper.Email()}, 
{CustomerHelper.WebSite()}, 
{CustomerHelper.CustomerType()}, 
{CustomerHelper.CustomerStatus()}, 
{CustomerHelper.BusinessType()}, 
{CustomerHelper.PriceRule()}, 
{CustomerHelper.FirstDate()}, 
{CustomerHelper.Currency()}, 
{CustomerHelper.CreditLimit()}, 
{CustomerHelper.TaxRate()}, 
{CustomerHelper.DiscountRate()}, 
{CustomerHelper.ShippingCarrier()}, 
{CustomerHelper.ShippingClass()}, 
{CustomerHelper.ShippingAccount()}, 
{CustomerHelper.Priority()}, 
{CustomerHelper.Area()}, 
{CustomerHelper.Region()}, 
{CustomerHelper.Districtn()}, 
{CustomerHelper.Zone()}, 
{CustomerHelper.TaxId()}, 
{CustomerHelper.ResaleLicense()}, 
{CustomerHelper.ClassCode()}, 
{CustomerHelper.DepartmentCode()}, 
{CustomerHelper.DivisionCode()}, 
{CustomerHelper.SourceCode()}, 
{CustomerHelper.Terms()}, 
{CustomerHelper.TermsDays()},
{CustomerAddressHelper.AddressCode()},
{CustomerAddressHelper.AddressType()},
{CustomerAddressHelper.Description()},
{CustomerAddressHelper.Name()},
{CustomerAddressHelper.FirstName()},
{CustomerAddressHelper.LastName()},
{CustomerAddressHelper.Suffix()},
{CustomerAddressHelper.Company()},
{CustomerAddressHelper.CompanyJobTitle()},
{CustomerAddressHelper.Attention()},
{CustomerAddressHelper.City()},
{CustomerAddressHelper.State()},
{CustomerAddressHelper.PostalCode()},
{CustomerAddressHelper.County()},
{CustomerAddressHelper.Country()},
{CustomerAddressHelper.DaytimePhone()},
{CustomerAddressHelper.NightPhone()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {CustomerHelper.TableName} {CustomerHelper.TableAllies} 
LEFT JOIN {CustomerAddressHelper.TableName} {CustomerAddressHelper.TableAllies} ON ({CustomerHelper.TableAllies}.CustomerUuid = {CustomerAddressHelper.TableAllies}.CustomerUuid)
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

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
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using Helper = DigitBridge.CommerceCentral.ERPDb.CustomerHelper;
using AdrHelper = DigitBridge.CommerceCentral.ERPDb.CustomerAddressHelper;
using AtrHelper = DigitBridge.CommerceCentral.ERPDb.CustomerAddressHelper;

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
{Helper.CustomerUuid()}, 
{Helper.CustomerCode()}, 
{Helper.CustomerName()}, 
{Helper.Contact()}, 
{Helper.Phone1()}, 
{Helper.Phone2()}, 
{Helper.Email()}, 
{Helper.WebSite()}, 
{Helper.CustomerType()}, 
{Helper.CustomerStatus()}, 
COALESCE(st.text, '') customerStatusText, 
{Helper.BusinessType()}, 
{Helper.PriceRule()}, 
{Helper.FirstDate()}, 
{Helper.CreditLimit()}, 
{Helper.Priority()}, 
{Helper.Area()}, 
{Helper.Region()}, 
{Helper.Districtn()}, 
{Helper.Zone()}, 
{Helper.ClassCode()}, 
{Helper.DepartmentCode()}, 
{Helper.DivisionCode()}, 
{Helper.SourceCode()}, 
{Helper.Terms()}, 
{Helper.TermsDays()},
{AdrHelper.City()},
{AdrHelper.State()},
{AdrHelper.PostalCode()},
{AdrHelper.Country()}
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


            return paramList.ToArray();
        }

        #endregion override methods

        public virtual void GetCustomerList(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.CustomerListCount = Count();
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.CustomerList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerListCount = 0;
                payload.CustomerList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task GetCustomerListAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.CustomerListCount = await CountAsync();
                payload.Success = await ExcuteJsonAsync(sb);
                if (payload.Success)
                    payload.CustomerList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerListCount = 0;
                payload.CustomerList = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

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

        public virtual IList<long> GetRowNumList(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

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

        //TODO where sql
        private string GetExportSql()
        {
            var sql = $@"
select JSON_QUERY((SELECT * FROM Customer i where i.RowNum={Helper.TableAllies}.RowNum FOR JSON PATH,WITHOUT_ARRAY_WRAPPER)) AS Customer,
(select {AdrHelper.TableAllies}.* from CustomerAddress {AdrHelper.TableAllies} where {Helper.TableAllies}.CustomerUuid={AdrHelper.TableAllies}.CustomerUuid FOR JSON PATH ) as CustomerAddress,
JSON_QUERY((select * from CustomerAttributes {AtrHelper.TableAllies} where {Helper.TableAllies}.CustomerUuid={AtrHelper.TableAllies}.CustomerUuid FOR JSON PATH ,WITHOUT_ARRAY_WRAPPER)) as CustomerAttributes 
from Customer {Helper.TableAllies}
";
            return sql;
        }

        private string GetExportCommandText(CustomerPayload payload)
        {
            this.LoadRequestParameter(payload);
            return $@"
{GetExportSql()}
{GetSQL_where()}
ORDER BY  {Helper.TableAllies}.RowNum  
OFFSET {payload.FixedSkip} ROWS FETCH NEXT {payload.FixedTop} ROWS ONLY
FOR JSON PATH
";
        }

        public virtual void GetExportJsonList(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.CustomerListCount = Count();
                StringBuilder sb = new StringBuilder();
                var result = ExcuteJson(sb, sql, GetSqlParameters().ToArray());
                if (result)
                    payload.CustomerDataList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerDataList = null;
                throw;
            }
        }

        public virtual async Task GetExportJsonListAsync(CustomerPayload payload)
        {
            if (payload == null)
                payload = new CustomerPayload();

            var sql = GetExportCommandText(payload);

            try
            {
                payload.CustomerListCount = await CountAsync();
                StringBuilder sb = new StringBuilder();
                var result = await ExcuteJsonAsync(sb, sql, GetSqlParameters().ToArray());
                if (result)
                    payload.CustomerDataList = sb;
            }
            catch (Exception ex)
            {
                payload.CustomerDataList = null;
                throw;
            }
        }
    }
}

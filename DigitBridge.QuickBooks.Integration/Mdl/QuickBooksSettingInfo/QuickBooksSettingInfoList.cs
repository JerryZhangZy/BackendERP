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
using Helper = DigitBridge.QuickBooks.Integration.QuickBooksChnlAccSettingHelper;

namespace DigitBridge.QuickBooks.Integration
{
    public class QuickBooksSettingInfoList : SqlQueryBuilder<QuickBooksSettingInfoQuery>
    {
        public QuickBooksSettingInfoList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public QuickBooksSettingInfoList(IDataBaseFactory dbFactory, QuickBooksSettingInfoQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }
        
        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.TableAllies}.*
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
        
        public virtual QuickBooksSettingInfoPayload GetQuickBooksSettingInfoList(QuickBooksSettingInfoPayload payload)
        {
            if (payload == null)
                payload = new QuickBooksSettingInfoPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.QuickBooksSettingInfoListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.QuickBooksSettingInfoList = sb;
            }
            catch (Exception ex)
            {
                payload.QuickBooksSettingInfoListCount = 0;
                payload.QuickBooksSettingInfoList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<QuickBooksSettingInfoPayload> GetQuickBooksSettingInfoListAsync(QuickBooksSettingInfoPayload payload)
        {
            if (payload == null)
                payload = new QuickBooksSettingInfoPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.QuickBooksSettingInfoListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.QuickBooksSettingInfoList = sb;
            }
            catch (Exception ex)
            {
                payload.QuickBooksSettingInfoListCount = 0;
                payload.QuickBooksSettingInfoList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(QuickBooksSettingInfoPayload payload)
        {
            if (payload == null)
                payload = new QuickBooksSettingInfoPayload();

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

        public virtual IList<long> GetRowNumList(QuickBooksSettingInfoPayload payload)
        {
            if (payload == null)
                payload = new QuickBooksSettingInfoPayload();

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
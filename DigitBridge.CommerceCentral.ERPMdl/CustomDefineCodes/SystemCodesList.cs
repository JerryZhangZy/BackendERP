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
using Helper = DigitBridge.CommerceCentral.ERPDb.SystemCodesHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SystemCodesList : SqlQueryBuilder<SystemCodesQuery>
    {
        public SystemCodesList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public SystemCodesList(IDataBaseFactory dbFactory, SystemCodesQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }
        
        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.RowNum()},
{Helper.SystemCodeUuid()},
{Helper.SystemCodeName()},
{Helper.Description()},
{Helper.InActive()},
{Helper.JsonFields()}
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
        
        public virtual SystemCodesPayload GetSystemCodesList(SystemCodesPayload payload)
        {
            if (payload == null)
                payload = new SystemCodesPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.SystemCodesListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.SystemCodesList = sb;
            }
            catch (Exception ex)
            {
                payload.SystemCodesListCount = 0;
                payload.SystemCodesList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<SystemCodesPayload> GetSystemCodesListAsync(SystemCodesPayload payload)
        {
            if (payload == null)
                payload = new SystemCodesPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.SystemCodesListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.SystemCodesList = sb;
            }
            catch (Exception ex)
            {
                payload.Success = false;
                payload.SystemCodesListCount = 0;
                payload.SystemCodesList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(SystemCodesPayload payload)
        {
            if (payload == null)
                payload = new SystemCodesPayload();

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

        public virtual IList<long> GetRowNumList(SystemCodesPayload payload)
        {
            if (payload == null)
                payload = new SystemCodesPayload();

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

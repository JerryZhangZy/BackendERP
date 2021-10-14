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
using Helper = DigitBridge.CommerceCentral.ERPDb.Event_ERPHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class EventERPList : SqlQueryBuilder<EventERPQuery>
    {
        public EventERPList(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public EventERPList(IDataBaseFactory dbFactory, EventERPQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }
        
        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.ChannelNum()},
{Helper.ChannelAccountNum()},
{Helper.ERPEventType()},
COALESCE(ordtp.text, '') erpEventTypeText, 
{Helper.ProcessSource()},
{Helper.ProcessUuid()},
{Helper.ProcessData()},
{Helper.ActionStatus()},
{Helper.ActionDateUtc()},
{Helper.EventMessage()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {Helper.TableName} {Helper.TableAllies} 
 LEFT JOIN @ERPEvent ordtp ON ({Helper.TableAllies}.ERPEventType = ordtp.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();

            paramList.Add("@ERPEvent".ToEnumParameter<ErpEventType>());
            //paramList.Add("@SalesOrderType".ToEnumParameter<SalesOrderType>());

            return paramList.ToArray();
        }
        
        #endregion override methods
        
        public virtual EventERPPayload GetEventERPList(EventERPPayload payload)
        {
            if (payload == null)
                payload = new EventERPPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.EventERPListCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.EventERPList = sb;
            }
            catch (Exception ex)
            {
                payload.EventERPListCount = 0;
                payload.EventERPList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<EventERPPayload> GetEventERPListAsync(EventERPPayload payload)
        {
            if (payload == null)
                payload = new EventERPPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.EventERPListCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.EventERPList = sb;
            }
            catch (Exception ex)
            {
                payload.EventERPListCount = 0;
                payload.EventERPList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<IList<long>> GetRowNumListAsync(EventERPPayload payload)
        {
            if (payload == null)
                payload = new EventERPPayload();

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

        public virtual IList<long> GetRowNumList(EventERPPayload payload)
        {
            if (payload == null)
                payload = new EventERPPayload();

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

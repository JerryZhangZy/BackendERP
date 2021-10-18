﻿using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoicePaymentSummaryInquiry : SqlQueryBuilder<InvoicePaymentSummaryQuery>
    {
        public InvoicePaymentSummaryInquiry(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }
        public InvoicePaymentSummaryInquiry(IDataBaseFactory dbFactory, InvoicePaymentSummaryQuery queryObject)
            : base(dbFactory, queryObject)
        {
        }

        #region override methods

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT  
COUNT(1) as [Count],
SUM(COALESCE({ERPDb.InvoiceTransactionHelper.TableAllies}.TotalAmount,0)) as Amount
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM {ERPDb.InvoiceTransactionHelper.TableName} {ERPDb.InvoiceTransactionHelper.TableAllies}
 JOIN {InvoiceHeaderHelper.TableName} {InvoiceHeaderHelper.TableAllies} ON ({InvoiceHeaderHelper.TableAllies}.InvoiceUuid = {ERPDb.InvoiceTransactionHelper.TableAllies}.InvoiceUuid)
";
            return this.SQL_From;
        } 
        #endregion override methods

        public async virtual Task InvoicePaymentSummaryAsync(InvoicePaymentPayload payload)
        {
            if (payload == null)
                payload = new InvoicePaymentPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            try
            {
                payload.Success = ExcuteJson(sb);
                if (payload.Success)
                    payload.InvoicePaymentSummary = sb;
            }
            catch (Exception ex)
            { 
                payload.InvoicePaymentSummary = null;
                AddError(ex.ObjectToString());
                payload.Messages = this.Messages;
            }
        }


    }
}

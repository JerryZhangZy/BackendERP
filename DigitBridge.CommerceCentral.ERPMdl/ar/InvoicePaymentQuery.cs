using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoicePaymentQuery : QueryObject<InvoicePaymentQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = ERPDb.InvoiceTransactionHelper.TableAllies;

        protected QueryFilter<int> _RMasterAccountNum = new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", PREFIX, FilterBy.eq, -1, Enable: true);
        protected QueryFilter<int> _RProfileNum = new QueryFilter<int>("ProfileNum", "ProfileNum", PREFIX, FilterBy.eq, -1, Enable: true);

        // Filter fields

        protected QueryFilter<int> _TransNum = new QueryFilter<int>("TransNum", "TransNum", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> TransNum => _TransNum;

        protected QueryFilter<string> _InvoiceNumber = new QueryFilter<string>("InvoiceNumber", "InvoiceNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumber => _InvoiceNumber;

        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> TransType => _TransType;

        protected QueryFilter<int> _TransStatus = new QueryFilter<int>("TransStatus", "TransStatus", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> TransStatus => _TransStatus;


        protected QueryFilter<DateTime> _TransDateFrom = new QueryFilter<DateTime>("TransDateFrom", "TransDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        public QueryFilter<DateTime> TransDateFrom => _TransDateFrom;

        protected QueryFilter<DateTime> _TransDateTo = new QueryFilter<DateTime>("TransDateTo", "TransDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);
        public QueryFilter<DateTime> TransDateTo => _TransDateTo;

        public InvoicePaymentQuery() : base()
        {
            RemoveFilter(_MasterAccountNum);
            RemoveFilter(_ProfileNum);
            AddFilter(_TransNum);
            AddFilter(_InvoiceNumber);
            AddFilter(_TransType);
            AddFilter(_TransStatus);
            AddFilter(_TransDateFrom);
            AddFilter(_TransDateTo);
            AddFilter(_RMasterAccountNum);
            AddFilter(_RProfileNum);
        }
        public override void InitQueryFilter()
        {
            _TransDateFrom.FilterValue = DateTime.Today.AddDays(-30);
           _TransDateTo.FilterValue = DateTime.Today.AddDays(7);
        }

    }
}

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceReturnSummaryQuery : QueryObject<InvoiceReturnSummaryQuery>
    {
        protected static string PREFIX = ERPDb.InvoiceTransactionHelper.TableAllies;

        // Filter fields 

        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransType => _TransType;

        protected QueryFilter<DateTime> _TransDateFrom = new QueryFilter<DateTime>("TransDateFrom", "TransDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateFrom => _TransDateFrom;

        protected QueryFilter<DateTime> _TransDateTo = new QueryFilter<DateTime>("TransDateTo", "TransDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateTo => _TransDateTo;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;


        public InvoiceReturnSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_TransDateFrom);
            AddFilter(_TransDateTo);
        }
        public override void InitQueryFilter()
        {
            _TransDateFrom.FilterValue = new DateTime(DateTime.UtcNow.Date.Year, 1, 1);
            _TransDateTo.FilterValue = DateTime.UtcNow.Date;
            _TransType.FilterValue = (int)TransTypeEnum.Return;
            LoadAll = true;
        }
        public override string GetOrderBySql(string prefix = null)
        {
            return string.Empty;
        }
    }
}

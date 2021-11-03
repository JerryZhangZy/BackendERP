using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class MiscInvoiceSummaryQuery : QueryObject<MiscInvoiceSummaryQuery>
    {  // Table prefix which use in this sql query
        protected static string PREFIX = MiscInvoiceHeaderHelper.TableAllies;

        // Filter fields 


        protected QueryFilter<DateTime> _MiscInvoiceDateFrom = new QueryFilter<DateTime>("MiscInvoiceDateFrom", "MiscInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> MiscInvoiceDateFrom => _MiscInvoiceDateFrom;

        protected QueryFilter<DateTime> _MiscInvoiceDateTo = new QueryFilter<DateTime>("MiscInvoiceDateTo", "MiscInvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> MiscInvoiceDateTo => _MiscInvoiceDateTo;


        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected EnumQueryFilter<MiscInvoiceStatusEnum> _MiscInvoiceStatus = new EnumQueryFilter<MiscInvoiceStatusEnum>("MiscInvoiceStatus", "MiscInvoiceStatus", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<MiscInvoiceStatusEnum> MiscInvoiceStatus => _MiscInvoiceStatus;

        public MiscInvoiceSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_MiscInvoiceDateFrom);
            AddFilter(_MiscInvoiceDateTo);
            AddFilter(_MiscInvoiceStatus);
        }
        public override void InitQueryFilter()
        { 
            _MiscInvoiceDateFrom.FilterValue = new DateTime(DateTime.Today.Year, 1, 1);
            _MiscInvoiceDateTo.FilterValue = DateTime.Today;
            LoadAll = true;
        }
        public override string GetOrderBySql(string prefix = null)
        {
            return string.Empty;
        }
    }
}

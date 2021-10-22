using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceSummaryQuery : QueryObject<InvoiceSummaryQuery>
    {  // Table prefix which use in this sql query
        protected static string PREFIX = InvoiceHeaderHelper.TableAllies;

        // Filter fields 


        protected QueryFilter<DateTime> _InvoiceDateFrom = new QueryFilter<DateTime>("InvoiceDateFrom", "InvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateFrom => _InvoiceDateFrom;

        protected QueryFilter<DateTime> _InvoiceDateTo = new QueryFilter<DateTime>("InvoiceDateTo", "InvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateTo => _InvoiceDateTo;


        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus = new EnumQueryFilter<InvoiceStatusEnum>("InvoiceStatus", "InvoiceStatus", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus => _InvoiceStatus;

        public InvoiceSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_InvoiceDateFrom);
            AddFilter(_InvoiceDateTo);
            AddFilter(_InvoiceStatus);
        }
        public override void InitQueryFilter()
        { 
            _InvoiceDateFrom.FilterValue = new DateTime(DateTime.Today.Year, 1, 1);
            _InvoiceDateTo.FilterValue = DateTime.Today;
            LoadAll = true;
        }
        public override string GetOrderBySql(string prefix = null)
        {
            return string.Empty;
        }
    }
}

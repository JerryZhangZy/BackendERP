using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderSummaryQuery : QueryObject<SalesOrderSummaryQuery>
    { // Table prefix which use in this sql query
        protected static string PREFIX = SalesOrderHeaderHelper.TableAllies;

        // Filter fields 

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<DateTime> _OrderDateFrom = new QueryFilter<DateTime>("OrderDateFrom", "OrderDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> OrderDateFrom => _OrderDateFrom;

        protected QueryFilter<DateTime> _OrderDateTo = new QueryFilter<DateTime>("OrderDateTo", "OrderDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> OrderDateTo => _OrderDateTo;

        protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;

        public SalesOrderSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_OrderDateFrom);
            AddFilter(_OrderDateTo);
            AddFilter(_OrderStatus);
        }
        public override void InitQueryFilter()
        {
            _OrderDateFrom.FilterValue = new DateTime(DateTime.UtcNow.Date.Year, 1, 1);
            _OrderDateTo.FilterValue = DateTime.UtcNow.Date;
            LoadAll = true;
        }
        public override string GetOrderBySql(string prefix = null)
        {
            return string.Empty;
        }
    }
}

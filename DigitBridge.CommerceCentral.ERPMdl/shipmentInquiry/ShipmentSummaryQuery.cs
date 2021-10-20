using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using System;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ShipmentSummaryQuery : QueryObject<ShipmentSummaryQuery>
    { // Table prefix which use in this sql query
        protected static string PREFIX = OrderShipmentHeaderHelper.TableAllies;

        // Filter fields 

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", SalesOrderHeaderHelper.TableAllies, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<DateTime> _ShipDateFrom = new QueryFilter<DateTime>("ShipDateFrom", "ShipmentDateUtc", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> ShipDateFrom => _ShipDateFrom;

        protected QueryFilter<DateTime> _ShipDateTo = new QueryFilter<DateTime>("ShipDateTo", "ShipmentDateUtc", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> ShipDateTo => _ShipDateTo;

        public ShipmentSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_ShipDateFrom);
            AddFilter(_ShipDateTo);
        }
        public override void InitQueryFilter()
        {
            _ShipDateFrom.FilterValue = new DateTime(DateTime.Today.Year, 1, 1);
            _ShipDateTo.FilterValue = DateTime.Today;
            LoadAll = true;
        }
        public override string GetOrderBySql(string prefix = null)
        {
            return string.Empty;
        }
    }
}

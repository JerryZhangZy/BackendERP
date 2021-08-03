using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderQuery : QueryObject<SalesOrderQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = SalesOrderHeaderHelper.TableAllies;
        protected static string PREFIX_CUSTOMER = CustomerHelper.TableAllies;
        protected static string PREFIX_DETAIL = SalesOrderItemsHelper.TableAllies;

        // Filter fields
        protected QueryFilter<string> _OrderNumberFrom = new QueryFilter<string>("OrderNumberFrom", "OrderNumber", PREFIX, FilterBy.ge, string.Empty);
        public QueryFilter<string> OrderNumberFrom => _OrderNumberFrom;

        protected QueryFilter<string> _OrderNumberTo = new QueryFilter<string>("OrderNumberTo", "OrderNumber", PREFIX, FilterBy.le, string.Empty);
        public QueryFilter<string> OrderNumberTo => _OrderNumberTo;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<DateTime> _OrderDateFrom = new QueryFilter<DateTime>("OrderDateFrom", "OrderDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        public QueryFilter<DateTime> OrderDateFrom => _OrderDateFrom;

        protected QueryFilter<DateTime> _OrderDateTo = new QueryFilter<DateTime>("OrderDateTo", "OrderDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);
        public QueryFilter<DateTime> OrderDateTo => _OrderDateTo;

        protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;

        protected EnumQueryFilter<SalesOrderType> _OrderType = new EnumQueryFilter<SalesOrderType>("OrderType", "OrderType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderType> OrderType => _OrderType;

        public SalesOrderQuery() : base()
        {
            _PREFIX = PREFIX;
            AddFilter(_OrderNumberFrom);
            AddFilter(_OrderNumberTo);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_OrderDateFrom);
            AddFilter(_OrderDateTo);
            AddFilter(_OrderStatus);
        }
        public override void InitQueryFilter()
        {
            _OrderDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _OrderDateTo.FilterValue = DateTime.Today.AddDays(7);
        }

    }
}

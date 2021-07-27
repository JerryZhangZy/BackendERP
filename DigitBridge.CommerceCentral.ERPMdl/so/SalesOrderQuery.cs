using System;
using System.Collections.Generic;
using System.Text;
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
        protected QueryFilter<string> _OrderNumberTo = new QueryFilter<string>("OrderNumberTo", "OrderNumber", PREFIX, FilterBy.le, string.Empty);
        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        protected QueryFilter<DateTime> _OrderDateFrom = new QueryFilter<DateTime>("OrderDateFrom", "OrderDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        protected QueryFilter<DateTime> _OrderDateTo = new QueryFilter<DateTime>("OrderDateTo", "OrderDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);

        public SalesOrderQuery() : base()
        {
            AddFilter(_OrderNumberFrom);
            AddFilter(_OrderNumberTo);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_OrderDateFrom);
            AddFilter(_OrderDateTo);
        }
        public override void InitQueryFilter()
        {
            _OrderDateFrom.FilterValue = DateTime.Today;
            _OrderDateTo.FilterValue = DateTime.Today;
        }

    }
}

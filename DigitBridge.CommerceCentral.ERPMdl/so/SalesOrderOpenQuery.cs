using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderOpenQuery : QueryObject<SalesOrderOpenQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = SalesOrderHeaderHelper.TableAllies;
        protected static string PREFIX_INFO = SalesOrderHeaderInfoHelper.TableAllies;

        // Filter fields
        #region SalesOrderHeader Filters

        protected QueryFilter<DateTime> _UpdateDateUtc = new QueryFilter<DateTime>("UpdateDateUtc", "UpdateDateUtc", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> UpdateDateUtc => _UpdateDateUtc;

        protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;

        #endregion 

        public SalesOrderOpenQuery() : base(PREFIX)
        {
            AddFilter(_UpdateDateUtc);
        }

        public override void InitQueryFilter()
        {
            _UpdateDateUtc.FilterValue = DateTime.Today.AddDays(-30);
            _OrderStatus.FilterValue = (int)SalesOrderStatus.Open;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ProductSummaryQuery : QueryObject<ProductSummaryQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = ProductBasicHelper.TableAllies;
        protected static string PREFIX_INVOICE = InvoiceHeaderHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<DateTime> _DateFrom = new QueryFilter<DateTime>("DateFrom", "InvoiceDate", PREFIX_INVOICE, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DateFrom => _DateFrom;

        protected QueryFilter<DateTime> _DateTo = new QueryFilter<DateTime>("DateTo", "InvoiceDate", PREFIX_INVOICE, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DateTo => _DateTo;
        //protected EnumQueryFilter<CustomerType> _CustomerType = new EnumQueryFilter<CustomerType>("CustomerType", "CustomerType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<CustomerType> CustomerType => _CustomerType;

        //protected EnumQueryFilter<BusinessType> _BusinessType = new EnumQueryFilter<BusinessType>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<BusinessType> BusinessType => _BusinessType;

        public ProductSummaryQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_DateFrom);
            AddFilter(_DateTo);
        }
        public override void InitQueryFilter()
        {
            _DateFrom.FilterValue = new DateTime(DateTime.Today.Year, 1, 1);
            _DateTo.FilterValue = DateTime.Today;
        }
    }
}

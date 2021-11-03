using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class VendorSummaryQuery : QueryObject<VendorSummaryQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = VendorHelper.TableAllies;
        protected static string PREFIX_INVOICE = InvoiceHeaderHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _VendorCode = new QueryFilter<string>("VendorCode", "VendorCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> VendorCode => _VendorCode;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;

        protected QueryFilter<DateTime> _DateFrom = new QueryFilter<DateTime>("DateFrom", "InvoiceDate", PREFIX_INVOICE, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DateFrom => _DateFrom;

        protected QueryFilter<DateTime> _DateTo = new QueryFilter<DateTime>("DateTo", "InvoiceDate", PREFIX_INVOICE, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DateTo => _DateTo;
        //protected EnumQueryFilter<VendorType> _VendorType = new EnumQueryFilter<VendorType>("VendorType", "VendorType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<VendorType> VendorType => _VendorType;

        //protected EnumQueryFilter<BusinessType> _BusinessType = new EnumQueryFilter<BusinessType>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<BusinessType> BusinessType => _BusinessType;

        public VendorSummaryQuery() : base(PREFIX)
        {
            AddFilter(_VendorCode);
            AddFilter(_VendorName);
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

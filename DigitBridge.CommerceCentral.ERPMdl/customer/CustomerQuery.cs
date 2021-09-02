using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CustomerQuery : QueryObject<CustomerQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = CustomerHelper.TableAllies;
        protected static string PREFIX_ADDRESS = CustomerAddressHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<string> _Area = new QueryFilter<string>("Area", "Area", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> Area => _Area;

        protected QueryFilter<string> _Region = new QueryFilter<string>("Region", "Region", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> Region => _Region;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        //protected EnumQueryFilter<CustomerType> _CustomerType = new EnumQueryFilter<CustomerType>("CustomerType", "CustomerType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<CustomerType> CustomerType => _CustomerType;

        //protected EnumQueryFilter<BusinessType> _BusinessType = new EnumQueryFilter<BusinessType>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<BusinessType> BusinessType => _BusinessType;

        public CustomerQuery() : base(PREFIX)
        {
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_Area);
            AddFilter(_Region);
            AddFilter(_ShippingCarrier);
        }
        public override void InitQueryFilter()
        {
        }

    }
}

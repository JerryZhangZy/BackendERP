using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WarehouseQuery : QueryObject<WarehouseQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = DistributionCenterHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _WarehouseName = new QueryFilter<string>("WarehouseName", "DistributionCenterName", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseName => _WarehouseName;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "DistributionCenterCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<int> _WarehouseType = new QueryFilter<int>("WarehouseType", "DistributionCenterType", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> WarehouseType => _WarehouseType;

        protected QueryFilter<string> _City = new QueryFilter<string>("City", "City", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> City => _City;

        protected QueryFilter<string> _State = new QueryFilter<string>("State", "State", PREFIX, FilterBy.cn, string.Empty, isNVarChar: true);
        public QueryFilter<string> State => _State;

        protected QueryFilter<string> _ZipCode = new QueryFilter<string>("ZipCode", "ZipCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ZipCode => _ZipCode;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<string> _CompayName = new QueryFilter<string>("CompayName", "CompayName", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CompayName => _CompayName;


        public WarehouseQuery() : base(PREFIX)
        {
            AddFilter(_WarehouseName);
            AddFilter(_WarehouseCode);
            AddFilter(_WarehouseType);
            AddFilter(_City);
            AddFilter(_State);
            AddFilter(_ZipCode);
            AddFilter(_ShippingCarrier);
            AddFilter(_CompayName);
        }
        public override void InitQueryFilter()
        {
        }

    }
}

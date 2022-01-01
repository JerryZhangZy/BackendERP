using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryLogQuery : QueryObject<InventoryLogQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = InventoryLogHelper.TableAllies;

        // Filter fields
        protected QueryFilter<DateTime> _LogDateFrom = new QueryFilter<DateTime>("LogDateFrom", "LogDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> LogDateFrom => _LogDateFrom;

        protected QueryFilter<DateTime> _LogDateTo = new QueryFilter<DateTime>("LogDateTo", "LogDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> LogDateTo => _LogDateTo;

        protected QueryFilter<long> _BatchNum = new QueryFilter<long>("BatchNum", "BatchNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> BatchNum => _BatchNum;

        protected QueryFilter<string> _LogUuid = new QueryFilter<string>("LogUuid", "LogUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LogUuid => _LogUuid;

        protected QueryFilter<string> _LogType = new QueryFilter<string>("LogType", "LogType", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LogType => _LogType;

        protected QueryFilter<string> _ProductUuid = new QueryFilter<string>("ProductUuid", "ProductUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ProductUuid => _ProductUuid;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _ColorPatternCode = new QueryFilter<string>("ColorPatternCode", "ColorPatternCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ColorPatternCode => _ColorPatternCode;

        protected QueryFilter<string> _SizeCode = new QueryFilter<string>("SizeCode", "SizeCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SizeCode => _SizeCode;

        protected QueryFilter<string> _WidthCode = new QueryFilter<string>("WidthCode", "WidthCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WidthCode => _WidthCode;

        protected QueryFilter<string> _LengthCode = new QueryFilter<string>("LengthCode", "LengthCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LengthCode => _LengthCode;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<string> _LpnNum = new QueryFilter<string>("LpnNum", "LpnNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LpnNum => _LpnNum;

        //protected EnumQueryFilter<CustomerType> _CustomerType = new EnumQueryFilter<CustomerType>("CustomerType", "CustomerType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<CustomerType> CustomerType => _CustomerType;

        //protected EnumQueryFilter<BusinessType> _BusinessType = new EnumQueryFilter<BusinessType>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<BusinessType> BusinessType => _BusinessType;

        public InventoryLogQuery() : base(PREFIX)
        {
            AddFilter(_LogDateFrom);
            AddFilter(_LogDateTo);
            AddFilter(_BatchNum);
            AddFilter(_LogUuid);
            AddFilter(_LogType);
            AddFilter(_ProductUuid);
            AddFilter(_SKU);
            AddFilter(_ColorPatternCode);
            AddFilter(_SizeCode);
            AddFilter(_WidthCode);
            AddFilter(_LengthCode);
            AddFilter(_WarehouseCode);
            AddFilter(_LotNum);
            AddFilter(_LpnNum);
        }
        public override void InitQueryFilter()
        {
            _LogDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _LogDateTo.FilterValue = DateTime.UtcNow.Date;
        }

    }
}

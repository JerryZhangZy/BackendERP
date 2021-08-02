using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryQuery : QueryObject<InventoryQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = ProductBasicHelper.TableAllies;
        protected static string PREFIX_PRODUCTEX = ProductExtHelper.TableAllies;
        protected static string PREFIX_DETAIL = ERPDb.InventoryHelper.TableAllies;

        protected QueryFilter<int> _RMasterAccountNum = new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", PREFIX, FilterBy.eq, -1, Enable: true);
        protected QueryFilter<int> _RProfileNum = new QueryFilter<int>("ProfileNum", "ProfileNum", PREFIX, FilterBy.eq, -1, Enable: true);

        // Filter fields

        protected QueryFilter<string> _ProductUuid = new QueryFilter<string>("ProductUuid", "ProductUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ProductUuid => _ProductUuid;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _Brand = new QueryFilter<string>("Brand", "Brand", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> Brand => _Brand;

        protected QueryFilter<string> _Manufacturer = new QueryFilter<string>("Manufacturer", "Manufacturer", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> Manufacturer => _Manufacturer;

        protected QueryFilter<string> _ProductTitle = new QueryFilter<string>("ProductTitle", "ProductTitle", PREFIX, FilterBy.cn, string.Empty, isNVarChar: true);
        public QueryFilter<string> ProductTitle => _ProductTitle;

        protected QueryFilter<string> _FNSku = new QueryFilter<string>("FNSku", "FNSku", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> FNSku => _FNSku;

        protected QueryFilter<string> _UPC = new QueryFilter<string>("UPC", "UPC", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> UPC => _UPC;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<string> _LpnNum = new QueryFilter<string>("LpnNum", "LpnNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LpnNum => _LpnNum;


        public InventoryQuery() : base()
        {
            RemoveFilter(_MasterAccountNum);
            RemoveFilter(_ProfileNum);
            AddFilter(_ProductUuid);
            AddFilter(_SKU);
            AddFilter(_Brand);
            AddFilter(_Manufacturer);
            AddFilter(_ProductTitle);
            AddFilter(_FNSku);
            AddFilter(_UPC);
            AddFilter(_WarehouseCode);
            AddFilter(_LotNum);
            AddFilter(_LpnNum);
            AddFilter(_RMasterAccountNum);
            AddFilter(_RProfileNum);
        }
        public override void InitQueryFilter()
        {
        }

    }
}

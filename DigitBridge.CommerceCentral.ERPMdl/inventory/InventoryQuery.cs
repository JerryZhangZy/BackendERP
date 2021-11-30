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
        protected static string PREFIX_DETAIL = InventoryHelper.TableAllies;

        // Filter fields
        #region ProductBasic
        protected QueryFilter<string> _ProductUuid = new QueryFilter<string>("ProductUuid", "ProductUuid", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> ProductUuid => _ProductUuid;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _Brand = new QueryFilter<string>("Brand", "Brand", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Brand => _Brand;

        protected QueryFilter<string> _Manufacturer = new QueryFilter<string>("Manufacturer", "Manufacturer", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Manufacturer => _Manufacturer;

        protected QueryFilter<string> _ProductTitle = new QueryFilter<string>("ProductTitle", "ProductTitle", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> ProductTitle => _ProductTitle;

        protected QueryFilter<string> _UPC = new QueryFilter<string>("UPC", "UPC", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> UPC => _UPC;
        #endregion

        #region ProductExt

        protected QueryFilter<string> _ClassCode = new QueryFilter<string>("ClassCode", "ClassCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> ClassCode => _ClassCode;

        protected QueryFilter<string> _SubClassCode = new QueryFilter<string>("SubClassCode", "SubClassCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> SubClassCode => _SubClassCode;

        protected QueryFilter<string> _DepartmentCode = new QueryFilter<string>("DepartmentCode", "DepartmentCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> DepartmentCode => _DepartmentCode;

        protected QueryFilter<string> _DivisionCode = new QueryFilter<string>("DivisionCode", "DivisionCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> DivisionCode => _DivisionCode;

        protected QueryFilter<string> _OEMCode = new QueryFilter<string>("OEMCode", "OEMCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> OEMCode => _OEMCode;

        protected QueryFilter<string> _AlternateCode = new QueryFilter<string>("AlternateCode", "AlternateCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> AlternateCode => _AlternateCode;

        protected QueryFilter<string> _Remark = new QueryFilter<string>("Remark", "Remark", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Remark => _Remark;

        protected QueryFilter<string> _Model = new QueryFilter<string>("Model", "Model", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Model => _Model;

        protected QueryFilter<string> _CategoryCode = new QueryFilter<string>("CategoryCode", "CategoryCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> CategoryCode => _CategoryCode;

        protected QueryFilter<string> _GroupCode = new QueryFilter<string>("GroupCode", "GroupCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> GroupCode => _GroupCode;

        protected QueryFilter<string> _SubGroupCode = new QueryFilter<string>("SubGroupCode", "SubGroupCode", PREFIX_PRODUCTEX, FilterBy.bw, string.Empty);
        public QueryFilter<string> SubGroupCode => _SubGroupCode;

        #endregion

        #region Inventory

        protected QueryFilter<string> _InventoryUuid = new QueryFilter<string>("InventoryUuid", "InventoryUuid", PREFIX_DETAIL, FilterBy.eq, string.Empty);
        public QueryFilter<string> InventoryUuid => _InventoryUuid;

        protected QueryFilter<string> _StyleCode = new QueryFilter<string>("StyleCode", "StyleCode", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> StyleCode => _StyleCode;

        protected QueryFilter<string> _ColorPatternCode = new QueryFilter<string>("ColorPatternCode", "ColorPatternCode", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> ColorPatternCode => _ColorPatternCode;

        protected QueryFilter<string> _SizeCode = new QueryFilter<string>("SizeCode", "SizeCode", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> SizeCode => _SizeCode;

        protected QueryFilter<string> _WidthCode = new QueryFilter<string>("WidthCode", "WidthCode", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> WidthCode => _WidthCode;

        protected QueryFilter<string> _LengthCode = new QueryFilter<string>("LengthCode", "LengthCode", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> LengthCode => _LengthCode;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX_DETAIL, FilterBy.eq, string.Empty);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<string> _LpnNum = new QueryFilter<string>("LpnNum", "LpnNum", PREFIX_DETAIL, FilterBy.bw, string.Empty);
        public QueryFilter<string> LpnNum => _LpnNum;

        #endregion

        public InventoryQuery() : base(PREFIX)
        {
            AddFilter(_ProductUuid);
            AddFilter(_SKU);
            AddFilter(_Brand);
            AddFilter(_Manufacturer);
            AddFilter(_ProductTitle);
            AddFilter(_UPC);

            AddFilter(_ClassCode);
            AddFilter(_SubClassCode);
            AddFilter(_DepartmentCode);
            AddFilter(_DivisionCode);
            AddFilter(_OEMCode);
            AddFilter(_AlternateCode);
            AddFilter(_Remark);
            AddFilter(_Model);
            AddFilter(_CategoryCode);
            AddFilter(_GroupCode);
            AddFilter(_SubGroupCode);

            AddFilter(_InventoryUuid);
            AddFilter(_StyleCode);
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
        }
        //public override void SetAvailableOrderByList(IList<string> orderByList)
        //{
        //    base.SetAvailableOrderByList();
        //    AddAvailableOrderByList(
        //        new KeyValuePair<string, string>("SKU", "sku"),
        //        new KeyValuePair<string, string>("Brand", "Brand"),
        //        new KeyValuePair<string, string>("StyleCode", "StyleCode"),
        //        new KeyValuePair<string, string>("ColorPatternCode", "ColorPatternCode"),
        //        new KeyValuePair<string, string>("ClassCode", "ClassCode"),
        //        new KeyValuePair<string, string>("DepartmentCode", "DepartmentCode"),
        //        new KeyValuePair<string, string>("CategoryCode", "CategoryCode"),
        //        new KeyValuePair<string, string>("GroupCode", "GroupCode")
        //        );
        //}
    }
}

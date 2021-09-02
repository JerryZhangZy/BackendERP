using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceReturnQuery : QueryObject<InvoiceReturnQuery>
    {
        // Table prefix which use in this sql query
        protected static string ReturnItemPREFIX = InvoiceReturnItemsHelper.TableAllies;
        protected static string TranSactionPREFIX = ERPDb.InvoiceTransactionHelper.TableAllies;
        // Filter fields


        protected QueryFilter<string> _TransUuid = new QueryFilter<string>("TransUuid", "TransUuid", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> TransUuid => _TransUuid;

        protected QueryFilter<string> _InvoiceUuid = new QueryFilter<string>("InvoiceUuid", "InvoiceUuid", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceUuid => _InvoiceUuid;

        protected QueryFilter<int> _ReturnItemType = new QueryFilter<int>("ReturnItemType", "ReturnItemType", ReturnItemPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ReturnItemType => _ReturnItemType;

        protected QueryFilter<int> _ReturnItemStatus = new QueryFilter<int>("ReturnItemStatus", "ReturnItemStatus", ReturnItemPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ReturnItemStatus => _ReturnItemStatus;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _ProductUuid = new QueryFilter<string>("ProductUuid", "ProductUuid", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ProductUuid => _ProductUuid;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<DateTime> _ReturnDateFrom = new QueryFilter<DateTime>("ReturnDateFrom", "ReturnDate", ReturnItemPREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> ReturnDateFrom => _ReturnDateFrom;

        protected QueryFilter<DateTime> _ReturnDateTo = new QueryFilter<DateTime>("ReturnDateTo", "ReturnDate", ReturnItemPREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> ReturnDateTo => _ReturnDateTo;

        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", TranSactionPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransType => _TransType;

        public InvoiceReturnQuery() : base(TranSactionPREFIX)
        {
            AddFilter(_TransUuid);
            AddFilter(_InvoiceUuid);
            AddFilter(_ReturnItemType);
            AddFilter(_ReturnItemStatus);
            AddFilter(_SKU);
            AddFilter(_ProductUuid);
            AddFilter(_WarehouseCode);
            AddFilter(_LotNum);
            AddFilter(_ReturnDateFrom);
            AddFilter(_ReturnDateTo);
            AddFilter(_TransType);
        }
        public override void InitQueryFilter()
        {
            _ReturnDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _ReturnDateTo.FilterValue = DateTime.Today;
            //TODO，make sure this won't be changed by user.
            _TransType.FilterValue = (int)TransTypeEnum.Return;
        }

    }
}

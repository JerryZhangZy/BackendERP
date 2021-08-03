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
        protected static string PREFIX = ERPDb.InvoiceReturnItemsHelper.TableAllies;

        protected QueryFilter<int> _RMasterAccountNum = new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", PREFIX, FilterBy.eq, -1, Enable: true);
        protected QueryFilter<int> _RProfileNum = new QueryFilter<int>("ProfileNum", "ProfileNum", PREFIX, FilterBy.eq, -1, Enable: true);

        // Filter fields


        protected QueryFilter<string> _TransUuid = new QueryFilter<string>("TransUuid", "TransUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> TransUuid => _TransUuid;

        protected QueryFilter<string> _InvoiceUuid = new QueryFilter<string>("InvoiceUuid", "InvoiceUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceUuid => _InvoiceUuid;

        protected QueryFilter<int> _ReturnItemType = new QueryFilter<int>("ReturnItemType", "ReturnItemType", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> ReturnItemType => _ReturnItemType;

        protected QueryFilter<int> _ReturnItemStatus = new QueryFilter<int>("ReturnItemStatus", "ReturnItemStatus", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> ReturnItemStatus => _ReturnItemStatus;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _ProductUuid = new QueryFilter<string>("ProductUuid", "ProductUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ProductUuid => _ProductUuid;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;
        
        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<DateTime> _ReturnDateFrom = new QueryFilter<DateTime>("ReturnDateFrom", "ReturnDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        public QueryFilter<DateTime> ReturnDateFrom => _ReturnDateFrom;

        protected QueryFilter<DateTime> _ReturnDateTo = new QueryFilter<DateTime>("ReturnDateTo", "ReturnDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);
        public QueryFilter<DateTime> ReturnDateTo => _ReturnDateTo;

        public InvoiceReturnQuery() : base()
        {
            RemoveFilter(_MasterAccountNum);
            RemoveFilter(_ProfileNum);
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
            AddFilter(_RMasterAccountNum);
            AddFilter(_RProfileNum);
        }
        public override void InitQueryFilter()
        {
            _ReturnDateFrom.FilterValue = DateTime.Today.AddDays(-30);
           _ReturnDateTo.FilterValue = DateTime.Today.AddDays(7);
        }

    }
}

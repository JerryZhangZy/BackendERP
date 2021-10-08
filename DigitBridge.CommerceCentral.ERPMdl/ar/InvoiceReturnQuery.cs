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

        protected QueryFilter<int> _TransNum = new QueryFilter<int>("TransNum", "TransNum", TranSactionPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransNum => _TransNum;

        protected QueryFilter<string> _InvoiceNumber = new QueryFilter<string>("InvoiceNumber", "InvoiceNumber", TranSactionPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumber => _InvoiceNumber;

        protected QueryFilter<int> _ReturnItemType = new QueryFilter<int>("ReturnItemType", "ReturnItemType", ReturnItemPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ReturnItemType => _ReturnItemType;

        protected QueryFilter<int> _ReturnItemStatus = new QueryFilter<int>("ReturnItemStatus", "ReturnItemStatus", ReturnItemPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ReturnItemStatus => _ReturnItemStatus;

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _LotNum = new QueryFilter<string>("LotNum", "LotNum", ReturnItemPREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> LotNum => _LotNum;

        protected QueryFilter<DateTime> _ReturnDateFrom = new QueryFilter<DateTime>("ReturnDateFrom", "TransDate", TranSactionPREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> ReturnDateFrom => _ReturnDateFrom;

        protected QueryFilter<DateTime> _ReturnDateTo = new QueryFilter<DateTime>("ReturnDateTo", "TransDate", TranSactionPREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> ReturnDateTo => _ReturnDateTo;

        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", TranSactionPREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransType => _TransType;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", InvoiceHeaderHelper.TableAllies, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;
        public InvoiceReturnQuery() : base(TranSactionPREFIX)
        {
            AddFilter(_TransNum);
            AddFilter(_InvoiceNumber);
            //AddFilter(_ReturnItemType);
            //AddFilter(_ReturnItemStatus);
            //AddFilter(_SKU);
            //AddFilter(_WarehouseCode);
            //AddFilter(_LotNum);
            AddFilter(_ReturnDateFrom);
            AddFilter(_ReturnDateTo);
            AddFilter(_TransType);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
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

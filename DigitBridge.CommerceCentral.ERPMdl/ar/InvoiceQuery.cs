using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceQuery : QueryObject<InvoiceQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = InvoiceHeaderHelper.TableAllies;
        protected static string PREFIX_INFO = InvoiceHeaderInfoHelper.TableAllies;
        protected static string PREFIX_DETAIL = InvoiceItemsHelper.TableAllies;

        protected QueryFilter<int> _RMasterAccountNum = new QueryFilter<int>("MasterAccountNum", "MasterAccountNum", PREFIX, FilterBy.eq, -1, Enable: true);
        protected QueryFilter<int> _RProfileNum = new QueryFilter<int>("ProfileNum", "ProfileNum", PREFIX, FilterBy.eq, -1, Enable: true);

        // Filter fields

        protected QueryFilter<string> _InvoiceNumber = new QueryFilter<string>("InvoiceNumber", "InvoiceNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumber => _InvoiceNumber;

        protected EnumQueryFilter<InvoiceStatus> _InvoiceStatus = new EnumQueryFilter<InvoiceStatus>("InvoiceStatus", "InvoiceStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<InvoiceStatus> InvoiceStatus => _InvoiceStatus;

        protected EnumQueryFilter<InvoiceType> _InvoiceType = new EnumQueryFilter<InvoiceType>("InvoiceType", "InvoiceType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<InvoiceType> InvoiceType => _InvoiceType;


        protected QueryFilter<DateTime> _InvoiceDateFrom = new QueryFilter<DateTime>("InvoiceDateFrom", "InvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        public QueryFilter<DateTime> InvoiceDateFrom => _InvoiceDateFrom;

        protected QueryFilter<DateTime> _InvoiceDateTo = new QueryFilter<DateTime>("InvoiceDateTo", "InvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);
        public QueryFilter<DateTime> InvoiceDateTo => _InvoiceDateTo;


        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.cn, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;


        public InvoiceQuery() : base()
        {
            RemoveFilter(_MasterAccountNum);
            RemoveFilter(_ProfileNum);
            AddFilter(_InvoiceNumber);
            AddFilter(_InvoiceType);
            AddFilter(_InvoiceStatus);
            AddFilter(_InvoiceDateFrom);
            AddFilter(_InvoiceDateTo);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_ShippingCarrier);
            AddFilter(_WarehouseCode);
            AddFilter(_RMasterAccountNum);
            AddFilter(_RProfileNum);
        }
        public override void InitQueryFilter()
        {
            _InvoiceDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _InvoiceDateTo.FilterValue = DateTime.Today.AddDays(7);
        }

    }
}

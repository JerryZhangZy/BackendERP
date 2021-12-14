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

        // Filter fields
        protected QueryFilter<string> _InvoiceUuid = new QueryFilter<string>("InvoiceUuid", "InvoiceUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceUuid => _InvoiceUuid;

        protected QueryFilter<string> _QboDocNumber = new QueryFilter<string>("QboDocNumber", "QboDocNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> QboDocNumber => _QboDocNumber;

        protected QueryFilter<string> _InvoiceNumberFrom = new QueryFilter<string>("InvoiceNumberFrom", "InvoiceNumber", PREFIX, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberFrom => _InvoiceNumberFrom;

        protected QueryFilter<string> _InvoiceNumberTo = new QueryFilter<string>("InvoiceNumberTo", "InvoiceNumber", PREFIX, FilterBy.le, string.Empty, isNVarChar: true);

        protected QueryFilter<DateTime> _InvoiceDateFrom = new QueryFilter<DateTime>("InvoiceDateFrom", "InvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateFrom => _InvoiceDateFrom;

        protected QueryFilter<DateTime> _InvoiceDateTo = new QueryFilter<DateTime>("InvoiceDateTo", "InvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateTo => _InvoiceDateTo;

        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;

        protected EnumQueryFilter<InvoiceType> _InvoiceType = new EnumQueryFilter<InvoiceType>("InvoiceType", "InvoiceType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<InvoiceType> InvoiceType => _InvoiceType;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus = new EnumQueryFilter<InvoiceStatusEnum>("InvoiceStatus", "InvoiceStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus => _InvoiceStatus; 

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;



        protected QueryFilter<long> _OrderShipmentNum = new QueryFilter<long>("OrderShipmentNum", "OrderShipmentNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> OrderShipmentNum => _OrderShipmentNum;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<long> _DistributionCenterNum = new QueryFilter<long>("DistributionCenterNum", "DistributionCenterNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> DistributionCenterNum => _DistributionCenterNum;

        protected QueryFilter<long> _CentralOrderNum = new QueryFilter<long>("CentralOrderNum", "CentralOrderNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _RefNum = new QueryFilter<string>("RefNum", "RefNum", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> RefNum => _RefNum;

        protected QueryFilter<string> _CustomerPoNum = new QueryFilter<string>("CustomerPoNum", "CustomerPoNum", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerPoNum => _CustomerPoNum; 

        protected QueryFilter<string> _ShipToName = new QueryFilter<string>("ShipToName", "ShipToName", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToName => _ShipToName;

        protected QueryFilter<string> _ShipToState = new QueryFilter<string>("ShipToState", "ShipToState", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToState => _ShipToState;

        protected QueryFilter<string> _ShipToPostalCode = new QueryFilter<string>("ShipToPostalCode", "ShipToPostalCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToPostalCode => _ShipToPostalCode;

        protected QueryFilterRawSql _OutstandingInvoiceOnly = new QueryFilterRawSql("OutstandingInvoiceOnly",
            $"{PREFIX}.InvoiceStatus IN (0,1,100)", 
            PREFIX, false);
        public QueryFilterRawSql OutstandingInvoiceOnly => _OutstandingInvoiceOnly;

        protected QueryFilter<string> _SalesRep = new QueryFilter<string>("SalesRep", "SalesRep", new List<string>() { "SalesRep2", "SalesRep3", "SalesRep4" }, PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> SalesRep => _SalesRep;

        public InvoiceQuery() : base(PREFIX)
        {
            AddFilter(_InvoiceUuid);
            AddFilter(_QboDocNumber);
            AddFilter(_InvoiceNumberFrom);
            AddFilter(_InvoiceNumberTo);
            AddFilter(_InvoiceDateFrom);
            AddFilter(_InvoiceDateTo); 
            AddFilter(_DueDateFrom);
            AddFilter(_DueDateTo); 
            AddFilter(_InvoiceType);
            AddFilter(_InvoiceStatus); 
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);

            AddFilter(_OrderShipmentNum);
            AddFilter(_ShippingCarrier);
            AddFilter(_DistributionCenterNum);
            AddFilter(_CentralOrderNum);
            AddFilter(_ChannelNum);
            AddFilter(_ChannelAccountNum);
            AddFilter(_ChannelOrderID);
            AddFilter(_WarehouseCode);
            AddFilter(_RefNum);
            AddFilter(_CustomerPoNum);
            AddFilter(_ShipToName);
            AddFilter(_ShipToState);
            AddFilter(_ShipToPostalCode);
            AddFilter(_OutstandingInvoiceOnly);

            AddFilter(_SalesRep);
        }
        public override void InitQueryFilter()
        {
            _InvoiceDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _InvoiceDateTo.FilterValue = DateTime.UtcNow.Date;
        }

        public void InitForNewPaymet(string customerCode)
        {
            //_InvoiceStatus.FilterValue = (int)InvoiceStatusEnum.Outstanding;
            OutstandingInvoiceOnly.Enable = true;
            _CustomerCode.FilterValue = customerCode;

            _InvoiceDateFrom.FilterValue = DateTime.UtcNow.Date.AddYears(-5);//TODO. this is a tmp begin date. make sure this logic.
            _InvoiceDateTo.FilterValue = DateTime.UtcNow.Date;
        }

        protected override void SetAvailableOrderByList()
        {
            base.SetAvailableOrderByList();
            AddAvailableOrderByList(
                new KeyValuePair<string, string>("InvoiceDate", "InvoiceDate DESC, RowNum DESC"),
                new KeyValuePair<string, string>("DueDate", "DueDate DESC, TransNum DESC, RowNum DESC"),
                new KeyValuePair<string, string>("InvoiceNumber", "InvoiceNumber DESC, RowNum DESC"),
                new KeyValuePair<string, string>("OrderNumber", "OrderNumber, RowNum DESC"),
                new KeyValuePair<string, string>("CustomerCode", "CustomerCode, RowNum DESC")
            );
        }

        public void DisableDate()
        {
            _InvoiceDateFrom.Enable = false;
            _InvoiceDateTo.Enable = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderQuery : QueryObject<SalesOrderQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = SalesOrderHeaderHelper.TableAllies;
        protected static string PREFIX_INFO = SalesOrderHeaderInfoHelper.TableAllies;
        //protected static string PREFIX_CUSTOMER = ERPDb.CustomerHelper.TableAllies;
        //protected static string PREFIX_DETAIL = SalesOrderItemsHelper.TableAllies;

        // Filter fields
        #region SalesOrderHeader Filters
        protected QueryFilter<string> _SalesOrderUuid = new QueryFilter<string>("SalesOrderUuid", "SalesOrderUuid", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> SalesOrderUuid => _SalesOrderUuid;

        protected QueryFilter<string> _OrderNumberFrom = new QueryFilter<string>("OrderNumberFrom", "OrderNumber", PREFIX, FilterBy.ge, string.Empty);
        public QueryFilter<string> OrderNumberFrom => _OrderNumberFrom;

        protected QueryFilter<string> _OrderNumberTo = new QueryFilter<string>("OrderNumberTo", "OrderNumber", PREFIX, FilterBy.le, string.Empty);
        public QueryFilter<string> OrderNumberTo => _OrderNumberTo;

        protected QueryFilter<DateTime> _OrderDateFrom = new QueryFilter<DateTime>("OrderDateFrom", "OrderDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime,isDate:true);
        public QueryFilter<DateTime> OrderDateFrom => _OrderDateFrom;

        protected QueryFilter<DateTime> _ShipDateTo = new QueryFilter<DateTime>("ShipDateTo", "ShipDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime,isDate:true);
        public QueryFilter<DateTime> ShipDateTo => _ShipDateTo;

        protected QueryFilter<DateTime> _ShipDateFrom = new QueryFilter<DateTime>("ShipDateFrom", "ShipDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime,isDate:true);
        public QueryFilter<DateTime> ShipDateFrom => _ShipDateFrom;

        protected QueryFilter<DateTime> _OrderDateTo = new QueryFilter<DateTime>("OrderDateTo", "OrderDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime,isDate:true);
        public QueryFilter<DateTime> OrderDateTo => _OrderDateTo;

        protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;

        protected EnumQueryFilter<SalesOrderType> _OrderType = new EnumQueryFilter<SalesOrderType>("OrderType", "OrderType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderType> OrderType => _OrderType;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;
        #endregion

        #region SalesOrderHeaderInfo


        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<string> _DistributionCenterNum = new QueryFilter<string>("DistributionCenterNum", "DistributionCenterNum", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> DistributionCenterNum => _DistributionCenterNum;

        protected QueryFilter<string> _CentralOrderNum = new QueryFilter<string>("CentralOrderNum", "CentralOrderNum", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _RefNum = new QueryFilter<string>("RefNum", "RefNum", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> RefNum => _RefNum;

        protected QueryFilter<string> _CustomerPoNum = new QueryFilter<string>("CustomerPoNum", "CustomerPoNum", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerPoNum => _CustomerPoNum;

        protected QueryFilter<string> _ShipToName = new QueryFilter<string>("ShipToName", "ShipToName", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToName => _ShipToName;

        protected QueryFilter<string> _ShipToState = new QueryFilter<string>("ShipToState", "ShipToState", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToState => _ShipToState;

        protected QueryFilter<string> _ShipToPostalCode = new QueryFilter<string>("ShipToPostalCode", "ShipToPostalCode", PREFIX_INFO, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToPostalCode => _ShipToPostalCode;

        #endregion

        public SalesOrderQuery() : base(PREFIX)
        {
            AddFilter(_SalesOrderUuid);
            AddFilter(_OrderNumberFrom);
            AddFilter(_OrderNumberTo);
            AddFilter(_OrderDateFrom);
            AddFilter(_OrderDateTo);
            AddFilter(_ShipDateFrom);
            AddFilter(_ShipDateTo);
            AddFilter(_OrderStatus);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);

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
        }
        public override void InitQueryFilter()
        {
            _OrderDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _OrderDateTo.FilterValue = DateTime.UtcNow.Date;
        }

        protected override void SetAvailableOrderByList()
        {
            base.SetAvailableOrderByList();
            AddAvailableOrderByList(
                new KeyValuePair<string, string>("OrderDate", "OrderDate DESC, RowNum DESC"),
                new KeyValuePair<string, string>("ShipDate", "ShipDate DESC, RowNum DESC"),
                new KeyValuePair<string, string>("CustomerCode", "CustomerCode, RowNum DESC"),
                new KeyValuePair<string, string>("OrderNumber", "OrderNumber DESC, RowNum DESC"),
                new KeyValuePair<string, string>("CentralOrderNum", $"{PREFIX_INFO}.CentralOrderNum DESC, RowNum DESC"),
                new KeyValuePair<string, string>("CustomerPoNum", $"{PREFIX_INFO}.CustomerPoNum, RowNum DESC"),
                new KeyValuePair<string, string>("RefNum", $"{PREFIX_INFO}.RefNum, RowNum DESC"),
                new KeyValuePair<string, string>("WarehouseCode", $"{PREFIX_INFO}.WarehouseCode, RowNum DESC")
            );
        }
    }
}

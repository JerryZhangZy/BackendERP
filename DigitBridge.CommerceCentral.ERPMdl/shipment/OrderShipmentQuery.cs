using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class OrderShipmentQuery : QueryObject<OrderShipmentQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = OrderShipmentHeaderHelper.TableAllies;
        protected static string PREFIX_PACKAGE = OrderShipmentPackageHelper.TableAllies;

        // Filter fields

        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", PREFIX, FilterBy.eq,0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<long> _OrderDCAssignmentNum = new QueryFilter<long>("OrderDCAssignmentNum", "OrderDCAssignmentNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> OrderDCAssignmentNum => _OrderDCAssignmentNum;

        protected QueryFilter<long> _CentralOrderNum = new QueryFilter<long>("CentralOrderNum", "CentralOrderNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        protected QueryFilter<string> _ShipmentID = new QueryFilter<string>("ShipmentID", "ShipmentID", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipmentID => _ShipmentID;

        protected QueryFilter<string> _WarehouseID = new QueryFilter<string>("WarehouseID", "WarehouseID", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseID => _WarehouseID;

        protected QueryFilter<string> _MainTrackingNumber = new QueryFilter<string>("MainTrackingNumber", "MainTrackingNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> MainTrackingNumber => _MainTrackingNumber;

        protected QueryFilter<string> _MainReturnTrackingNumber = new QueryFilter<string>("MainReturnTrackingNumber", "MainReturnTrackingNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> MainReturnTrackingNumber => _MainReturnTrackingNumber;

        protected QueryFilter<int> _ShipmentType = new QueryFilter<int>("ShipmentType", "ShipmentType", PREFIX, FilterBy.eq,0);
        public QueryFilter<int> ShipmentType => _ShipmentType;

        protected QueryFilter<int> _ProcessStatus = new QueryFilter<int>("ProcessStatus", "ProcessStatus", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ProcessStatus => _ProcessStatus;


        public OrderShipmentQuery() : base(PREFIX)
        {
            AddFilter(_ChannelNum);
            AddFilter(_ChannelAccountNum);
            AddFilter(_OrderDCAssignmentNum);
            AddFilter(_CentralOrderNum);
            AddFilter(_ChannelOrderID);
            AddFilter(_ShipmentID);
            AddFilter(_WarehouseID);
            AddFilter(_MainTrackingNumber);
            AddFilter(_MainReturnTrackingNumber);
            AddFilter(_ShipmentType);
            AddFilter(_ProcessStatus);
        }
        public override void InitQueryFilter()
        {
        }

    }
}

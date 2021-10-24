﻿using System;
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

        protected QueryFilter<string> _InvoiceNumberFrom = new QueryFilter<string>("InvoiceNumberFrom", "InvoiceNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberFrom => _InvoiceNumberFrom;

        protected QueryFilter<string> _InvoiceNumberTo = new QueryFilter<string>("InvoiceNumberTo", "InvoiceNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberTo => _InvoiceNumberTo;

        protected QueryFilter<DateTime> _InvoiceDateFrom = new QueryFilter<DateTime>("InvoiceDateFrom", "InvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateFrom => _InvoiceDateFrom;

        protected QueryFilter<DateTime> _InvoiceDateTo = new QueryFilter<DateTime>("InvoiceDateTo", "InvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> InvoiceDateTo => _InvoiceDateTo;

        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;

        protected EnumQueryFilter<InvoiceType> _InvoiceType = new EnumQueryFilter<InvoiceType>("InvoiceType", "InvoiceType", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceType> InvoiceType => _InvoiceType;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus = new EnumQueryFilter<InvoiceStatusEnum>("InvoiceStatus", "InvoiceStatus", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus => _InvoiceStatus; 

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.cn, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;



        protected QueryFilter<long> _OrderShipmentNum = new QueryFilter<long>("OrderShipmentNum", "OrderShipmentNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> OrderShipmentNum => _OrderShipmentNum;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<long> _DistributionCenterNum = new QueryFilter<long>("DistributionCenterNum", "DistributionCenterNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> DistributionCenterNum => _DistributionCenterNum;

        protected QueryFilter<long> _CentralOrderNum = new QueryFilter<long>("CentralOrderNum", "CentralOrderNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<long> _ChannelNum = new QueryFilter<long>("ChannelNum", "ChannelNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> ChannelNum => _ChannelNum;

        protected QueryFilter<long> _ChannelAccountNum = new QueryFilter<long>("ChannelAccountNum", "ChannelAccountNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _RefNum = new QueryFilter<string>("RefNum", "RefNum", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> RefNum => _RefNum;

        protected QueryFilter<string> _CustomerPoNum = new QueryFilter<string>("CustomerPoNum", "CustomerPoNum", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerPoNum => _CustomerPoNum;

        protected QueryFilter<string> _ShipToName = new QueryFilter<string>("ShipToName", "ShipToName", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToName => _ShipToName;

        protected QueryFilter<string> _ShipToState = new QueryFilter<string>("ShipToState", "ShipToState", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToState => _ShipToState;

        protected QueryFilter<string> _ShipToPostalCode = new QueryFilter<string>("ShipToPostalCode", "ShipToPostalCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToPostalCode => _ShipToPostalCode;

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
        }
        public override void InitQueryFilter()
        {
            _InvoiceDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _InvoiceDateTo.FilterValue = DateTime.Today;
        }

        public void InitForNewPaymet(string customerCode)
        {
            _InvoiceStatus.FilterValue = (int)InvoiceStatusEnum.Outstanding;
            _CustomerCode.FilterValue = customerCode;

            _InvoiceDateFrom.FilterValue = DateTime.Today.AddYears(-5);//TODO. this is a tmp begin date. make sure this logic.
            _InvoiceDateTo.FilterValue = DateTime.Today;
        }
    }
}

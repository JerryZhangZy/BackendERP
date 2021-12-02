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
        protected static string PREFIX = ERPDb.InvoiceTransactionHelper.TableAllies;

        // Table prefix which use in this sql query
        protected static string ReturnItemPREFIX = InvoiceReturnItemsHelper.TableAllies;
        protected static string TranSactionPREFIX = ERPDb.InvoiceTransactionHelper.TableAllies;
        // Filter fields 

        protected QueryFilter<string> _TransUuid = new QueryFilter<string>("TransUuid", "TransUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> TransUuid => _TransUuid;

        protected QueryFilter<DateTime> _TransDateFrom = new QueryFilter<DateTime>("TransDateFrom", "TransDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateFrom => _TransDateFrom;

        protected QueryFilter<DateTime> _TransDateTo = new QueryFilter<DateTime>("TransDateTo", "TransDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateTo => _TransDateTo;

        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransType => _TransType;

        protected QueryFilter<int> _TransStatus = new QueryFilter<int>("TransStatus", "TransStatus", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> TransStatus => _TransStatus;



        protected QueryFilter<string> _InvoiceUuid = new QueryFilter<string>("InvoiceUuid", "InvoiceUuid", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceUuid => _InvoiceUuid;

        protected QueryFilter<string> _QboDocNumber = new QueryFilter<string>("QboDocNumber", "QboDocNumber", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> QboDocNumber => _QboDocNumber;

        protected QueryFilter<string> _InvoiceNumberFrom = new QueryFilter<string>("InvoiceNumberFrom", "InvoiceNumber", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberFrom => _InvoiceNumberFrom;

        protected QueryFilter<string> _InvoiceNumberTo = new QueryFilter<string>("InvoiceNumberTo", "InvoiceNumber", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberTo => _InvoiceNumberTo;

        protected EnumQueryFilter<InvoiceType> _InvoiceType = new EnumQueryFilter<InvoiceType>("InvoiceType", "InvoiceType", InvoiceHeaderHelper.TableAllies, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceType> InvoiceType => _InvoiceType;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus = new EnumQueryFilter<InvoiceStatusEnum>("InvoiceStatus", "InvoiceStatus", InvoiceHeaderHelper.TableAllies, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus => _InvoiceStatus;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", InvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", InvoiceHeaderHelper.TableAllies, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;



        protected QueryFilter<long> _OrderShipmentNum = new QueryFilter<long>("OrderShipmentNum", "OrderShipmentNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<long> OrderShipmentNum => _OrderShipmentNum;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<long> _DistributionCenterNum = new QueryFilter<long>("DistributionCenterNum", "DistributionCenterNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<long> DistributionCenterNum => _DistributionCenterNum;

        protected QueryFilter<long> _CentralOrderNum = new QueryFilter<long>("CentralOrderNum", "CentralOrderNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<long> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _RefNum = new QueryFilter<string>("RefNum", "RefNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> RefNum => _RefNum;

        protected QueryFilter<string> _CustomerPoNum = new QueryFilter<string>("CustomerPoNum", "CustomerPoNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerPoNum => _CustomerPoNum;

        protected QueryFilter<string> _ShipToName = new QueryFilter<string>("ShipToName", "ShipToName", InvoiceHeaderInfoHelper.TableAllies, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToName => _ShipToName;

        protected QueryFilter<string> _ShipToState = new QueryFilter<string>("ShipToState", "ShipToState", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToState => _ShipToState;

        protected QueryFilter<string> _ShipToPostalCode = new QueryFilter<string>("ShipToPostalCode", "ShipToPostalCode", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShipToPostalCode => _ShipToPostalCode;

        public InvoiceReturnQuery() : base(TranSactionPREFIX)
        {
            //AddFilter(_TransNum);
            //AddFilter(_InvoiceNumber);
            AddFilter(_TransUuid);
            AddFilter(_TransDateFrom);
            AddFilter(_TransDateTo);
            AddFilter(_TransType);
            AddFilter(_TransStatus);  

            AddFilter(_InvoiceUuid);
            AddFilter(_QboDocNumber);
            AddFilter(_InvoiceNumberFrom);
            AddFilter(_InvoiceNumberTo); 
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
            _TransDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _TransDateTo.FilterValue = DateTime.UtcNow.Date;
            //TODO，make sure this won't be changed by user.
            _TransType.FilterValue = (int)TransTypeEnum.Return;
        }

        protected override void SetAvailableOrderByList()
        {
            base.SetAvailableOrderByList();
            AddAvailableOrderByList(
                new KeyValuePair<string, string>("TransDate", "TransDate DESC, RowNum DESC"),
                new KeyValuePair<string, string>("TransNum", "MiscInvoiceNumber DESC, TransNum DESC, RowNum DESC"),
                new KeyValuePair<string, string>("InvoiceNumber", "InvoiceNumber DESC, RowNum DESC"),
                new KeyValuePair<string, string>("PaidBy", "PaidBy, RowNum DESC")
            );
        }
    }
}

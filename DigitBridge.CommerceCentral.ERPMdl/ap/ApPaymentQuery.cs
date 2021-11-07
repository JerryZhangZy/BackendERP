using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ApPaymentQuery : QueryObject<ApPaymentQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = ERPDb.ApInvoiceTransactionHelper.TableAllies;

        // Filter fields

        //protected QueryFilter<int> _TransNum = new QueryFilter<int>("TransNum", "TransNum", PREFIX, FilterBy.eq, 0);
        //public QueryFilter<int> TransNum => _TransNum;

        //protected QueryFilter<string> _InvoiceNumber = new QueryFilter<string>("InvoiceNumber", "InvoiceNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        //public QueryFilter<string> InvoiceNumber => _InvoiceNumber;

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

        protected QueryFilter<int> _PaidBy = new QueryFilter<int>("PaidBy", "PaidBy", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> PaidBy => _PaidBy;



        protected QueryFilter<string> _InvoiceUuid = new QueryFilter<string>("ApInvoiceUuid", "ApInvoiceUuid", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceUuid => _InvoiceUuid;

        //protected QueryFilter<string> _QboDocNumber = new QueryFilter<string>("QboDocNumber", "QboDocNumber", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        //public QueryFilter<string> QboDocNumber => _QboDocNumber;

        protected QueryFilter<string> _InvoiceNumberFrom = new QueryFilter<string>("ApInvoiceNumFrom", "ApInvoiceNum", ApInvoiceHeaderHelper.TableAllies, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberFrom => _InvoiceNumberFrom;

        protected QueryFilter<string> _InvoiceNumberTo = new QueryFilter<string>("ApInvoiceNumTo", "ApInvoiceNum", ApInvoiceHeaderHelper.TableAllies, FilterBy.le, string.Empty, isNVarChar: true);
        public QueryFilter<string> InvoiceNumberTo => _InvoiceNumberTo;

        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", ApInvoiceHeaderHelper.TableAllies, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", ApInvoiceHeaderHelper.TableAllies, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;

        protected EnumQueryFilter<InvoiceType> _InvoiceType = new EnumQueryFilter<InvoiceType>("ApInvoiceType", "ApInvoiceType", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceType> InvoiceType => _InvoiceType;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus = new EnumQueryFilter<InvoiceStatusEnum>("ApInvoiceStatus", "ApInvoiceStatus", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, 0);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus => _InvoiceStatus;

        protected QueryFilter<string> _VendorNumFrom = new QueryFilter<string>("VendorNumFrom", "VendorNum", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorNumFrom => _VendorNumFrom;
        protected QueryFilter<string> _VendorNumTo = new QueryFilter<string>("VendorNumTo", "VendorNum", ApInvoiceHeaderHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorNumTo => _VendorNumTo;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", ApInvoiceHeaderHelper.TableAllies, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;


        //protected QueryFilter<long> _OrderShipmentNum = new QueryFilter<long>("OrderShipmentNum", "OrderShipmentNum", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        //public QueryFilter<long> OrderShipmentNum => _OrderShipmentNum;

        protected QueryFilter<string> _ShippingCarrier = new QueryFilter<string>("ShippingCarrier", "ShippingCarrier", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ShippingCarrier => _ShippingCarrier;

        protected QueryFilter<long> _DistributionCenterNum = new QueryFilter<long>("DistributionCenterNum", "DistributionCenterNum", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<long> DistributionCenterNum => _DistributionCenterNum;

        protected QueryFilter<long> _CentralOrderNum = new QueryFilter<long>("CentralOrderNum", "CentralOrderNum", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<long> CentralOrderNum => _CentralOrderNum;

        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", InvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<string> _ChannelOrderID = new QueryFilter<string>("ChannelOrderID", "ChannelOrderID", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ChannelOrderID => _ChannelOrderID;

        //protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        //public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected QueryFilter<string> _RefNum = new QueryFilter<string>("RefNum", "RefNum", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> RefNum => _RefNum;

        protected QueryFilter<string> _CustomerPoNum = new QueryFilter<string>("CustomerPoNum", "CustomerPoNum", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerPoNum => _CustomerPoNum;

        //protected QueryFilter<string> _ShipToName = new QueryFilter<string>("ShipToName", "ShipToName", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.bw, string.Empty, isNVarChar: true);
        //public QueryFilter<string> ShipToName => _ShipToName;

        //protected QueryFilter<string> _ShipToState = new QueryFilter<string>("ShipToState", "ShipToState", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        //public QueryFilter<string> ShipToState => _ShipToState;

        //protected QueryFilter<string> _ShipToPostalCode = new QueryFilter<string>("ShipToPostalCode", "ShipToPostalCode", ApInvoiceHeaderInfoHelper.TableAllies, FilterBy.eq, string.Empty, isNVarChar: true);
        //public QueryFilter<string> ShipToPostalCode => _ShipToPostalCode;


        public ApPaymentQuery() : base(PREFIX)
        {
            //AddFilter(_TransNum);
            //AddFilter(_InvoiceNumber);
            AddFilter(_TransUuid);
            AddFilter(_TransDateFrom);
            AddFilter(_TransDateTo);
            AddFilter(_TransType);
            AddFilter(_TransStatus);
            AddFilter(_PaidBy);


            AddFilter(_InvoiceUuid);
            //AddFilter(_QboDocNumber);
            AddFilter(_InvoiceNumberFrom);
            AddFilter(_InvoiceNumberTo);
            AddFilter(_DueDateFrom);
            AddFilter(_DueDateTo);
            AddFilter(_InvoiceType);
            AddFilter(_InvoiceStatus);
            AddFilter(_VendorNumFrom);
            AddFilter(_VendorNumTo);
            AddFilter(_VendorName);

            //AddFilter(_OrderShipmentNum);
            AddFilter(_ShippingCarrier);
            AddFilter(_DistributionCenterNum);
            AddFilter(_CentralOrderNum);
            AddFilter(_ChannelNum);
            AddFilter(_ChannelAccountNum);
            AddFilter(_ChannelOrderID);
            //AddFilter(_WarehouseCode);
            AddFilter(_RefNum);
            AddFilter(_CustomerPoNum);
            //AddFilter(_ShipToName);
            //AddFilter(_ShipToState);
            //AddFilter(_ShipToPostalCode);
        }
        public override void InitQueryFilter()
        {
            _TransDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _TransDateTo.FilterValue = DateTime.Today;
            //TODO，make sure this won't be changed by user.
            _TransType.FilterValue = (int)TransTypeEnum.Payment;
        }

    }
}

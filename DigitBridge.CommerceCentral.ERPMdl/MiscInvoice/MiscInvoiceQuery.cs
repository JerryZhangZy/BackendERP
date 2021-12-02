using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class MiscInvoiceQuery : QueryObject<MiscInvoiceQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = MiscInvoiceHeaderHelper.TableAllies;
        //protected static string PREFIX_INFO = MiscInvoiceHeaderInfoHelper.TableAllies;
        //protected static string PREFIX_DETAIL = MiscInvoiceItemsHelper.TableAllies;

        // Filter fields
        protected QueryFilter<string> _MiscInvoiceUuid = new QueryFilter<string>("MiscInvoiceUuid", "MiscInvoiceUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> MiscInvoiceUuid => _MiscInvoiceUuid;

        protected QueryFilter<string> _QboDocNumber = new QueryFilter<string>("QboDocNumber", "QboDocNumber", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> QboDocNumber => _QboDocNumber;

        protected QueryFilter<string> _MiscInvoiceNumber = new QueryFilter<string>("MiscInvoiceNumber", "MiscInvoiceNumber", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> MiscInvoiceNumber => _MiscInvoiceNumber;

        protected QueryFilter<string> _MiscInvoiceNumberFrom = new QueryFilter<string>("MiscInvoiceNumberFrom", "MiscInvoiceNumber", PREFIX, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> MiscInvoiceNumberFrom => _MiscInvoiceNumberFrom;

        protected QueryFilter<string> _MiscInvoiceNumberTo = new QueryFilter<string>("MiscInvoiceNumberTo", "MiscInvoiceNumber", PREFIX, FilterBy.le, string.Empty, isNVarChar: true);
        public QueryFilter<string> MiscInvoiceNumberTo => _MiscInvoiceNumberTo;

        protected QueryFilter<DateTime> _MiscInvoiceDateFrom = new QueryFilter<DateTime>("MiscInvoiceDateFrom", "MiscInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> MiscInvoiceDateFrom => _MiscInvoiceDateFrom;

        protected QueryFilter<DateTime> _MiscInvoiceDateTo = new QueryFilter<DateTime>("MiscInvoiceDateTo", "MiscInvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> MiscInvoiceDateTo => _MiscInvoiceDateTo;

        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;

        protected EnumQueryFilter<MiscInvoiceType> _MiscInvoiceType = new EnumQueryFilter<MiscInvoiceType>("MiscInvoiceType", "MiscInvoiceType", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<MiscInvoiceType> MiscInvoiceType => _MiscInvoiceType;

        protected EnumQueryFilter<MiscInvoiceStatusEnum> _MiscInvoiceStatus = new EnumQueryFilter<MiscInvoiceStatusEnum>("MiscInvoiceStatus", "MiscInvoiceStatus", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<MiscInvoiceStatusEnum> MiscInvoiceStatus => _MiscInvoiceStatus;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<string> _BankAccountUuid = new QueryFilter<string>("BankAccountUuid", "BankAccountUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> BankAccountUuid => _BankAccountUuid;

        protected QueryFilter<string> _BankAccountCode = new QueryFilter<string>("BankAccountCode", "BankAccountCode", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> BankAccountCode => _BankAccountCode;

        protected QueryFilter<int> _PaidBy = new QueryFilter<int>("PaidBy", "PaidBy", PREFIX, FilterBy.eq, 1);
        public QueryFilter<int> PaidBy => _PaidBy;

        protected QueryFilter<string> _CheckNum = new QueryFilter<string>("CheckNum", "CheckNum", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CheckNum => _CheckNum;
        
        protected QueryFilter<string> _AuthCode = new QueryFilter<string>("AuthCode", "AuthCode", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> AuthCode => _AuthCode;

        public MiscInvoiceQuery() : base(PREFIX)
        {
            AddFilter(_MiscInvoiceUuid);
            AddFilter(_MiscInvoiceNumber);
            AddFilter(_QboDocNumber);
            AddFilter(_MiscInvoiceType);
            AddFilter(_MiscInvoiceStatus);
            AddFilter(_MiscInvoiceDateFrom);
            AddFilter(_MiscInvoiceDateTo);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_PaidBy);
            AddFilter(_CheckNum);
            AddFilter(_AuthCode);

            AddFilter(_MiscInvoiceNumberFrom);
            AddFilter(_MiscInvoiceNumberTo);
            AddFilter(_DueDateFrom);
            AddFilter(_DueDateTo);

            AddFilter(_BankAccountUuid);
            AddFilter(_BankAccountCode);
        }
        public override void InitQueryFilter()
        {
            _MiscInvoiceDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _MiscInvoiceDateTo.FilterValue = DateTime.UtcNow.Date;
        }

        protected override void SetAvailableOrderByList()
        {
            base.SetAvailableOrderByList();
            AddAvailableOrderByList(
                new KeyValuePair<string, string>("MiscInvoiceDate", "MiscInvoiceDate DESC, RowNum DESC"),
                new KeyValuePair<string, string>("MiscInvoiceNumber", "MiscInvoiceNumber DESC, RowNum DESC"),
                new KeyValuePair<string, string>("CustomerCode", "CustomerCode, RowNum DESC")
            );
        }

        public void InitForNewPaymet(string customerCode)
        {
            _MiscInvoiceStatus.FilterValue = (int)MiscInvoiceStatusEnum.Outstanding;
            _CustomerCode.FilterValue = customerCode;

            _MiscInvoiceDateFrom.FilterValue = DateTime.UtcNow.Date.AddYears(-5);//TODO. this is a tmp begin date. make sure this logic.
            _MiscInvoiceDateTo.FilterValue = DateTime.UtcNow.Date;
        }
    }
}

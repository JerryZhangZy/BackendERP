using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using EventHelper = DigitBridge.CommerceCentral.ERPDb.EventProcessERPHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceUnprocessQuery : QueryObject<InvoiceUnprocessQuery>
    {
        // Table prefix which use in this sql query 
        protected static string PREFIX = EventHelper.TableAllies;
        protected static string PREFIX_Invoice = ERPDb.InvoiceHeaderHelper.TableAllies;


        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<int> _EventProcessActionStatus = new QueryFilter<int>("EventProcessActionStatus", "ActionStatus", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> EventProcessActionStatus => _EventProcessActionStatus;

        protected EnumQueryFilter<EventProcessTypeEnum> _ERPEventProcessType = new EnumQueryFilter<EventProcessTypeEnum>("ERPEventProcessType", "ERPEventProcessType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<EventProcessTypeEnum> ERPEventProcessType => _ERPEventProcessType;

        protected EnumQueryFilter<InvoiceStatusEnum> _InvoiceStatus_Void = new EnumQueryFilter<InvoiceStatusEnum>("InvoiceStatus_Void", "InvoiceStatus", PREFIX_Invoice, FilterBy.ne, -1);
        public EnumQueryFilter<InvoiceStatusEnum> InvoiceStatus_Void => _InvoiceStatus_Void;

        public InvoiceUnprocessQuery() : base(PREFIX)
        {
            AddFilter(_ChannelNum);
            AddFilter(_ChannelAccountNum);
            AddFilter(_EventProcessActionStatus);
        }
        public override void InitQueryFilter()
        {
            _EventProcessActionStatus.FilterValue = (int)EventProcessActionStatusEnum.Pending;
            _ERPEventProcessType.FilterValue = (int)EventProcessTypeEnum.InvoiceToCommerceCentral;
            _InvoiceStatus_Void.FilterValue = (int)InvoiceStatusEnum.Void;
        }
    }
}

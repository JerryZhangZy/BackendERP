using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceUnprocessQuery : QueryObject<InvoiceUnprocessQuery>
    {
        // Table prefix which use in this sql query 
        protected static string PREFIX_INFO = InvoiceHeaderInfoHelper.TableAllies;
        protected static string eventHelperTableAllies = "event";//TODO change to helper.


        protected QueryFilter<long> _ChannelNum = new QueryFilter<long>("ChannelNum", "ChannelNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> ChannelNum => _ChannelNum;

        protected QueryFilter<long> _ChannelAccountNum = new QueryFilter<long>("ChannelAccountNum", "ChannelAccountNum", PREFIX_INFO, FilterBy.eq, 0);
        public QueryFilter<long> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<int> _EventProcessActionStatus = new QueryFilter<int>("EventProcessActionStatus", "ActionStatus", eventHelperTableAllies, FilterBy.eq, 0);
        public QueryFilter<int> EventProcessActionStatus => _EventProcessActionStatus;

        public InvoiceUnprocessQuery() : base(eventHelperTableAllies)
        {
            AddFilter(_ChannelNum);
            AddFilter(_ChannelAccountNum);
            AddFilter(_EventProcessActionStatus);
        }
        public override void InitQueryFilter()
        {
            _EventProcessActionStatus.FilterValue = (int)EventProcessActionStatusEnum.Default;
            //_InvoiceDateTo.FilterValue = DateTime.Today;
        }
    }
}

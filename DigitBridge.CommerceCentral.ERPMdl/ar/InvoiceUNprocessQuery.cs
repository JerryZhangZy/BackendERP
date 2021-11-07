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
        protected static string PREFIX = "event"; //TODO change to helper. 


        protected QueryFilter<int> _ChannelNum = new QueryFilter<int>("ChannelNum", "ChannelNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ChannelNum => _ChannelNum;

        protected QueryFilter<int> _ChannelAccountNum = new QueryFilter<int>("ChannelAccountNum", "ChannelAccountNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> ChannelAccountNum => _ChannelAccountNum;

        protected QueryFilter<int> _EventProcessActionStatus = new QueryFilter<int>("EventProcessActionStatus", "ActionStatus", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> EventProcessActionStatus => _EventProcessActionStatus;

        public InvoiceUnprocessQuery() : base(PREFIX)
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

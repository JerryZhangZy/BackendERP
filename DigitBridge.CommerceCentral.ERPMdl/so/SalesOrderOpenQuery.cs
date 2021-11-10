using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class SalesOrderOpenQuery : QueryObject<SalesOrderOpenQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = SalesOrderHeaderHelper.TableAllies;
        protected static string PREFIX_INFO = SalesOrderHeaderInfoHelper.TableAllies;
        protected static string PREFIX_Event = ERPDb.EventProcessERPHelper.TableAllies;

        // Filter fields
        #region SalesOrderHeader Filters

        protected QueryFilter<DateTime> _UpdateDateUtc = new QueryFilter<DateTime>("UpdateDateUtc", "UpdateDateUtc", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> UpdateDateUtc => _UpdateDateUtc;

        protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;


        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", PREFIX_INFO, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;


        protected QueryFilter<int> _EventProcessActionStatus = new QueryFilter<int>("EventProcessActionStatus", "ActionStatus", PREFIX_Event, FilterBy.eq, -1);
        public QueryFilter<int> EventProcessActionStatus => _EventProcessActionStatus;

        protected EnumQueryFilter<EventProcessTypeEnum> _ERPEventProcessType = new EnumQueryFilter<EventProcessTypeEnum>("ERPEventProcessType", "ERPEventProcessType", PREFIX_Event, FilterBy.eq, -1);
        public EnumQueryFilter<EventProcessTypeEnum> ERPEventProcessType => _ERPEventProcessType;

        #endregion 

        public SalesOrderOpenQuery() : base(PREFIX)
        {
            AddFilter(_UpdateDateUtc);
            AddFilter(_WarehouseCode);
            AddFilter(_OrderStatus);
            AddFilter(_EventProcessActionStatus);
            AddFilter(_ERPEventProcessType);
        }

        public override void InitQueryFilter()
        {
            //_UpdateDateUtc.FilterValue = DateTime.Today.AddDays(-30);
            _OrderStatus.FilterValue = (int)SalesOrderStatus.Open;
            _EventProcessActionStatus.FilterValue = (int)EventProcessActionStatusEnum.Pending;
            _ERPEventProcessType.FilterValue = (int)EventProcessTypeEnum.SalesOrderToWMS;
        }
    }
}

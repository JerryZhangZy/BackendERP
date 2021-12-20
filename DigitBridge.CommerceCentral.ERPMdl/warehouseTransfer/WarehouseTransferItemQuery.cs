using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferItemsHelper;
using Helper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferHeaderHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WarehouseTransferItemQuery : QueryObject<WarehouseTransferItemQuery>
    {
        protected static string HEADERPREFIX = Helper.TableAllies;
        protected static string PREFIX = ItemsHelper.TableAllies;

        #region filters
        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", PREFIX, FilterBy.eq, string.Empty);
        protected QueryFilter<DateTime> _TransferDateFrom = new QueryFilter<DateTime>("TransferDateFrom", "TransferDate", HEADERPREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        protected QueryFilter<DateTime> _TransferDateTo = new QueryFilter<DateTime>("TransferDateTo", "TransferDate", HEADERPREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        protected QueryFilter<DateTime> _ReceiveDateFrom = new QueryFilter<DateTime>("ReceiveDateFrom", "ReceiveDate", HEADERPREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        protected QueryFilter<DateTime> _ReceiveDateTo = new QueryFilter<DateTime>("ReceiveDateTo", "ReceiveDate", HEADERPREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        protected QueryFilter<string> _FromWarehouseCode = new QueryFilter<string>("FromWarehouseCode", "FromWarehouseCode", PREFIX, FilterBy.eq, string.Empty);
        protected QueryFilter<string> _ToWarehouseCode = new QueryFilter<string>("ToWarehouseCode", "ToWarehouseCode", PREFIX, FilterBy.eq, string.Empty);
        #endregion

        #region filter properties
        public QueryFilter<string> SKU => _SKU;
        public QueryFilter<DateTime> TransferDateFrom => _TransferDateFrom;
        public QueryFilter<DateTime> TransferDateTo => _TransferDateTo;
        public QueryFilter<DateTime> ReceiveDateFrom => _ReceiveDateFrom;
        public QueryFilter<DateTime> ReceiveDateTo => _ReceiveDateTo;
        public QueryFilter<string> FromWarehouseCode => _FromWarehouseCode;
        public QueryFilter<string> ToWarehouseCode => _ToWarehouseCode;
        #endregion


        public WarehouseTransferItemQuery()
            : base(HEADERPREFIX)
        {
            AddFilter(_SKU);
            AddFilter(_TransferDateFrom); 
            AddFilter(_TransferDateTo); 
            AddFilter(_ReceiveDateFrom); 
            AddFilter(_ReceiveDateTo); 
            AddFilter(_FromWarehouseCode); 
            AddFilter(_ToWarehouseCode);
        }
    }
}
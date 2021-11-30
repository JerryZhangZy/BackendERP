              
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Helper = DigitBridge.CommerceCentral.ERPDb.PoHeaderHelper;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.PoItemsHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class PurchaseOrderQuery : QueryObject<PurchaseOrderQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;
        protected static string ITEMSPREFIX = ItemsHelper.TableAllies;

        // Filter fields
        protected QueryFilter<DateTime> _PoDateFrom = new QueryFilter<DateTime>("PoDateFrom", "PoDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime,isDate:true);
        public QueryFilter<DateTime> PoDateFrom => _PoDateFrom;

        protected QueryFilter<DateTime> _PoDateTo = new QueryFilter<DateTime>("PoDateTo", "PoDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime,isDate:true);
        public QueryFilter<DateTime> PoDateTo => _PoDateTo;

        protected EnumQueryFilter<PoStatus> _PoStatus = new EnumQueryFilter<PoStatus>("PoStatus", "PoStatus", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<PoStatus> PoStatus => _PoStatus;

        protected EnumQueryFilter<PoType> _PoType = new EnumQueryFilter<PoType>("PoType", "PoType", PREFIX, FilterBy.eq, -1);
        public EnumQueryFilter<PoType> PoType => _PoType;

        protected QueryFilter<string> _VendorCode = new QueryFilter<string>("VendorCode", "VendorCode", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> VendorCode => _VendorCode;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;

        protected QueryFilter<string> _PoNum = new QueryFilter<string>("PoNum", "PoNum", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoNum => _PoNum;
         
        
        public PurchaseOrderQuery() : base(PREFIX)
        {
            AddFilter(_PoDateFrom);
            AddFilter(_PoDateTo);
            AddFilter(_PoStatus);
            AddFilter(_PoType); 
            AddFilter(_VendorCode);
            AddFilter(_VendorName);
            AddFilter(_PoNum);
        }

        public override void InitQueryFilter()
        {
            _PoDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _PoDateTo.FilterValue = DateTime.UtcNow.Date.AddDays(7);
        }

        //public override void SetAvailableOrderByList(IList<string> orderByList)
        //{
        //    base.SetAvailableOrderByList();
        //    AddAvailableOrderByList(
        //        new KeyValuePair<string, string>("PoDate", "PoDate"),
        //        new KeyValuePair<string, string>("EtaArrivalDate", "EtaArrivalDate"),
        //        new KeyValuePair<string, string>("PoNum", "PoNum"),
        //        new KeyValuePair<string, string>("VendorCode", "VendorCode")
        //        );

        //}

    }
}

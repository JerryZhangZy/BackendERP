              
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

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class PurchaseOrderQuery : QueryObject<PurchaseOrderQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;

        // Filter fields
        //protected QueryFilter<DateTime> _OrderDateFrom = new QueryFilter<DateTime>("OrderDateFrom", "OrderDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime);
        //public QueryFilter<DateTime> OrderDateFrom => _OrderDateFrom;

        //protected QueryFilter<DateTime> _OrderDateTo = new QueryFilter<DateTime>("OrderDateTo", "OrderDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime);
        //public QueryFilter<DateTime> OrderDateTo => _OrderDateTo;

        //protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.eq, string.Empty);
        //public QueryFilter<string> CustomerCode => _CustomerCode;

        //protected EnumQueryFilter<SalesOrderStatus> _OrderStatus = new EnumQueryFilter<SalesOrderStatus>("OrderStatus", "OrderStatus", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<SalesOrderStatus> OrderStatus => _OrderStatus;

        //protected EnumQueryFilter<SalesOrderType> _OrderType = new EnumQueryFilter<SalesOrderType>("OrderType", "OrderType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<SalesOrderType> OrderType => _OrderType;

        public PurchaseOrderQuery() : base(PREFIX)
        {
            //AddFilter(_OrderDateFrom);
            //AddFilter(_OrderDateTo);
            //AddFilter(_CustomerCode);
            //AddFilter(_OrderStatus);
            //AddFilter(_OrderType);
        }

        public override void InitQueryFilter()
        {
            //_OrderDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            //_OrderDateTo.FilterValue = DateTime.Today.AddDays(7);
        }
    }
}

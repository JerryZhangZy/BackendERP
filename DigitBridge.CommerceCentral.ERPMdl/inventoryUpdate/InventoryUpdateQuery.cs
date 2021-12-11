              
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
using Helper = DigitBridge.CommerceCentral.ERPDb.InventoryUpdateHeaderHelper;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.InventoryUpdateItemsHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InventoryUpdateQuery : QueryObject<InventoryUpdateQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;
        protected static string ITEMSPREFIX = ItemsHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", ITEMSPREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", ITEMSPREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected EnumQueryFilter<InventoryUpdateType> _UpdateType = new EnumQueryFilter<InventoryUpdateType>("InventoryUpdateType", "InventoryUpdateType", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<InventoryUpdateType> UpdateType => _UpdateType;

        //protected EnumQueryFilter<SalesOrderType> _OrderType = new EnumQueryFilter<SalesOrderType>("OrderType", "OrderType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<SalesOrderType> OrderType => _OrderType;

        public InventoryUpdateQuery() : base(PREFIX)
        {
            AddFilter(_SKU);
            AddFilter(_WarehouseCode);
            AddFilter(_UpdateType);
            //AddFilter(_OrderDateFrom);
            //AddFilter(_OrderDateTo);
            //AddFilter(_CustomerCode);
            //AddFilter(_OrderStatus);
            //AddFilter(_OrderType);
        }

        public override void InitQueryFilter()
        {
            //_OrderDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            //_OrderDateTo.FilterValue = DateTime.UtcNow.Date.AddDays(7);
        }
    }
}
